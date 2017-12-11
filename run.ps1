#!/usr/bin/env pwsh
#requires -version 6

<#
.SYNOPSIS
Bootstrapper script for Leet.Build toolchain.

.DESCRIPTION
Performs initial steps in Leet.Build toolchain. Downloads all required prerequisites to run MSBuild scripts for the specified repository. There are two sources of the script configuration. First one is script command line which takes precedence over the second one which is a JSON configuration file. Name of the configuration is 'buildtools.json' and shall be located in the $ProjectDirectory.

.PARAMETER Command
The name of the Leet.Build command to execute.

.PARAMETER ProjectDirectory
The path to the folder which contains project to build.

.PARAMETER ToolsVersion
Version of the Leet.Build tools to use.

.PARAMETER ToolsFeed
Path to the tools version feed to be used.

.PARAMETER SdkVersion
Version of the .NET Core SDK to use.

.PARAMETER DotNetHome
The directory where .NET Core tools will be stored.

.PARAMETER Arguments
Arguments to be passed to the command

.NOTES
The 'buildtools.json' is expected to be an JSON file. It is optional, and the configuration values in it are optional as well. Any options set in the file are overridden by command line parameters.

.EXAMPLE
Example config file:
```json
{
  "$schema": "https://raw.githubusercontent.com/Leet/BuildTools/dev/schemas/buildtools.schema.json",
  "ToolsVersion": "0.0.1"
}
```
#>
[CmdletBinding(PositionalBinding = $False)]
param(
    [Parameter(Position = 0)]
    [String]   $Command          = 'verify',
    [String]   $ProjectDirectory = $PSScriptRoot,
    [String]   $ToolsVersion     = '',
    [String]   $ToolsFeed        = '',
    [String]   $SdkVersion       = '',
    [String]   $DotNetHome       = '',
    [Parameter(ValueFromRemainingArguments = $True)]
    [String[]] $Arguments
)

Set-StrictMode -Version 2

$ErrorActionPreference   = 'Stop'
$WarningActionPreference = 'Continue'

$DiagnosticColor       = 'DarkGray'
$InstallationColor     = 'Magenta'
$SuccessColor          = 'DarkGreen'

<#
.SYNOPSIS
Combines a path with a sequence of child paths into a single path.

.DESCRIPTION
The Join-Paths cmdlet combines a path and sequence of child-paths into a single path. The provider supplies the path delimiters.

.PARAMETER Path
Specifies the main path (or paths) to which the child-path is appended. Wildcards are permitted.

The value of Path determines which provider joins the paths and adds the path delimiters. The Path parameter is required, although the parameter name ("Path") is optional.

.PARAMETER ChildPaths
Specifies the elements to append to the value of the Path parameter. Wildcards are permitted. The ChildPaths parameter is required, although the parameter name ("ChildPaths") is optional.

.NOTES
The cmdlets that contain the Path noun (the Path cmdlets) manipulate path names and return the names in a concise format that all Windows PowerShell providers can interpret. They are designed for use in programs and scripts where you want to display all or part of a path name in a particular format. Use them like you would use Dirname, Normpath, Realpath, Join, or other path manipulators.

You can use the path cmdlets with several providers, including the FileSystem, Registry, and Certificate providers.

This cmdlet is designed to work with the data exposed by any provider. To list the providers available in your session, type Get-PSProvider. For more information, see about_Providers.

.EXAMPLE
# This function call returns 'C:\First\Second\Third\Fourth.file'
Join-Paths 'C:' ('First\', '\Second', '\Third\', 'Fourth.file')
#>
function Join-Paths ([string]   $Path,
                     [string[]] $ChildPaths) {
    $ChildPaths | ForEach-Object { $Path = Join-Path $Path $_ }
    return $Path
}

<#
.SYNOPSIS
Provides a mechanism for a script block to retry its execution upon an error.

.DESCRIPTION
Invokes the specified script block. If there are errors reported during the execution this method waits specified number of seconds and retries the execution. If the speciied script block execution was retried specified number of times without success this method throws all reported errors.

.PARAMETER ScriptBlock
Script block to execute with retry.

.PARAMETER MaxAttempts
Max number of the execution attempts for the specified script block.

.PARAMETER SecondsBetweenAttempts
Number of seconds to wait between each attempt.
#>
function Invoke-WithRetry ([ScriptBlock] $ScriptBlock,
                           [int]         $MaxAttempts            = 1,
                           [int]         $SecondsBetweenAttempts = 1) {
    $exceptions = @()
    for ($attempt = 0; ++$attempt -le $MaxAttempts; Start-Sleep $SecondsBetweenAttempts) {
        try   { return $ScriptBlock.Invoke() }
        catch { $exceptions += $_            }
    }
    
    throw $exceptions
}

<#
.SYNOPSIS
Writes a specified message string to the shell host with optional indentation and line wraps.

.PARAMETER Message
Message to be written by the host.

.PARAMETER ForegroundColor
Color of the message font to be used.

.PARAMETER BackgroundColor
Color of the background of the message to be used.

.PARAMETER SplitLines
Specified whether the message shall be splitted by lines to match current window buffer width.

.PARAMETER Indentation
Indentation to applay to each line.
#>
function Write-Message ([string]       $Message,
                        [ConsoleColor] $ForegroundColor = [Console]::ForegroundColor,
                        [Switch]       $SplitLines      = $True,
                        [int]          $Indentation     = 0,
                        [Switch]       $IndentFirstLine = $True                     ){
    if (-not $Message) { Write-Host; return }

    $indentationText = ' ' * $Indentation
    $index           = 0

    if ($SplitLines) { $limit = (get-host).UI.RawUI.BufferSize.Width - $Indentation - 1 }
    else             { $limit = $Message.Length                                         }
    
    while ($index -lt $Message.Length) {
        $limit = ($limit, ($Message.Length - $index) | Measure -Min).Minimum
        $messageLine = $Message.Substring($index, $limit)
        if (($index -gt 0) -or ($IndentFirstLine)) { $messageLine = $indentationText + $messageLine }
        Write-Host -ForegroundColor $ForegroundColor $messageLine
        $index += $limit
    }
}

<#
.SYNOPSIS
Obtains a specified remote file using hypertext transfer protocol (HTTP) or from network location.

.DESCRIPTION
If the remote file path is specified using HTTP protocol and there are problems obtaining the file this function performs 10 download attempts before failing.

.PARAMETER SourcePath
Path to the source file to obtain.

.PARAMETER DestinationPath
Path to the file as which the obtained file shall be saved.

.EXAMPLE
# This function calls copy the files from the network location.
# If there are problems obtaining the file this calls fail immediately.
Get-RemoteFile '\\server\share\file.ext' 'C:\'

.EXAMPLE
# This function call copy the file hosted using HTTP protocol.
# If there are problems obtaining the file this function performs 10 download attempts before failing.
Get-RemoteFile 'https:\\server\share\file.ext' 'C:\'
#>
function Get-RemoteFile ([string] $SourcePath,
                         [string] $DestinationPath) {
    if ($SourcePath -notlike 'http*') {
        Copy-Item $SourcePath $DestinationPath
        return
    }
    
    Invoke-WithRetry({
        try   { Invoke-WebRequest -UseBasicParsing -Uri $SourcePath -OutFile $DestinationPath }
        catch { throw "Could not download specified file: $_"                                 }
    })
}

<#
.SYNOPSIS
Initializes the script by loading parameter values from configuration file.

.DESCRIPTION
If the script parameter values are not specified they may be loaded from Leet.Build.json configuration file. This function tries to load available commands from it.
#>
function Initialize-ScriptConfiguration {
    Initialize-ToolsVersion
    Initialize-ToolsFeed
    Initialize-DotNetHome

    $script:BuildToolsDirectoryPath = Join-Path $script:DotNetHome              'buildtools'
    $script:LeetBuildDirectoryPath  = Join-Path $script:BuildToolsDirectoryPath 'Leet.Build'
    $script:LeetBuildModuleRoot     = Join-Path $script:LeetBuildDirectoryPath  $ToolsVersion
    $script:LeetBuildModulePath     = Join-Path $script:LeetBuildModuleRoot     'Leet.Build.psd1'
    
    Write-Verbose 'Using parameters:'
	Write-Verbose "  -Command          = '$script:Command'"
	Write-Verbose "  -ProjectDirectory = '$script:ProjectDirectory'"
	Write-Verbose "  -ToolsVersion     = '$script:ToolsVersion'"
    Write-Verbose "  -ToolsFeed        = '$script:ToolsFeed'"
	Write-Verbose "  -DotNetHome       = '$script:DotNetHome'"
	Write-Verbose "  -SdkVersion       = '$script:SdkVersion'"
	Write-Verbose "  -Arguments        = '$script:Arguments'"
}

<#
.SYNOPSIS
Initializes $script:ToolsVersion parameter value if not specified by the caller.

.DESCRIPTION
If the value for the $script:ToolsVersion parameter has not been specified this function tries to read a value for it from Leet.Build.json configuration file.
#>
function Initialize-ToolsVersion {
    if ($script:ToolsVersion) { return }
    
    $configFilePath = Join-Paths $script:ProjectDirectory ('build', 'Leet.Build.json')
    
    try {
        $configFileContent = Get-Content -Raw -Encoding UTF8 -Path $configFilePath
        $config = ConvertFrom-Json $configFileContent
    }
    catch { throw "Could not find Leet.Build configuration file or its content is invalid.`n$PSItem" }
    
    if (!(Get-Member -Name 'buildToolsVersion' -InputObject $config)) {
        throw "Could not find 'buildToolsVersion' member in Leet.Build.json configuration file."
    }
    
    $script:ToolsVersion = $config.buildToolsVersion
}

<#
.SYNOPSIS
Initializes $script:ToolsFeed parameter value if not specified by the caller.

.DESCRIPTION
If the value for the $script:ToolsFeed parameter has not been specified this function tries to read a value for it from Leet.Build.json configuration file.
If the value is not found in the configuration file this function assigns a default value to the configuration variable.
#>
function Initialize-ToolsFeed {
    if ($script:ToolsFeed) { return }
    
    $configFilePath = Join-Paths $script:ProjectDirectory ('build', 'Leet.Build.json')
    
    try {
        $configFileContent = Get-Content -Raw -Encoding UTF8 -Path $configFilePath
        $config = ConvertFrom-Json $configFileContent
    }
    catch { $script:ToolsFeed = 'https://github.com/Leet/BuildTools/releases/download'; return }
    
    if (!(Get-Member -Name 'buildToolsFeed' -InputObject $config)) {
        $script:ToolsFeed = 'https://github.com/Leet/BuildTools/releases/download'
    } else {
        $script:ToolsFeed = $config.buildToolsFeed
    }
}

<#
.SYNOPSIS
Initializes $script:DotNetHome parameter value if not specified by the caller.

.DESCRIPTION
If the value for the $script:DotNetHome parameter has not been specified this function tries to assign to it a path to the '.dotnet' directory under user home or script root folder.
#>
function Initialize-DotNetHome {
    if ($script:DotNetHome) { return }

    $script:DotNetHome = `
    if     ($env:DOTNET_HOME) { $env:DOTNET_HOME                    } `
    elseif ($env:USERPROFILE) { Join-Path $env:USERPROFILE '.dotnet'} `
    elseif ($env:HOME)        { Join-Path $env:HOME        '.dotnet'} `
    else                      { Join-Path $PSScriptRoot    '.dotnet'}
}

<#
.SYNOPSIS
Installs Leet.Build tools in a version specified by script parameters and returns a path to its directory.

.DESCRIPTION
If the Leet.Build tools in a specified version are already present in the target directory this returns.
Otherwise this function downloads them from GitHub releases and place zip content in the target directory before returning.
#>
function Install-LeetBuild {
    $leetBuildExtractionRoot = Join-Path $LeetBuildDirectoryPath "$ToolsVersion"
    $tempFilePath            = Join-Path $LeetBuildDirectoryPath "Leet.Build-$([guid]::NewGuid()).zip"
    $leetBuildRemotePath     = "$ToolsFeed/$ToolsVersion/Leet.Build.zip"
    
    Write-Debug 'Installing Leet.Build tools...'
    if (Test-Path $LeetBuildModulePath -PathType Leaf) { return }

    Write-Message -ForegroundColor $InstallationColor "Downloading Leet.Build tools from '$LeetBuildRemotePath'."
    if (-not (Test-Path $leetBuildModuleRoot)) { 
        New-Item -ItemType Directory -Path $leetBuildModuleRoot
        $folderToRemoveOnError = $leetBuildModuleRoot
    }
    
    if     (-not (Test-Path $DotNetHome             )) { $FolderToRemoveOnError = $DotNetHome              }
    elseif (-not (Test-Path $BuildToolsDirectoryPath)) { $FolderToRemoveOnError = $BuildToolsDirectoryPath }
    elseif (-not (Test-Path $LeetBuildDirectoryPath )) { $FolderToRemoveOnError = $LeetBuildDirectoryPath  }
    elseif (-not (Test-Path $LeetBuildModuleRoot    )) { $FolderToRemoveOnError = $LeetBuildModuleRoot     }

    try {
        Get-RemoteFile $LeetBuildRemotePath $tempFilePath
        
        try { Expand-Archive -Path $TempFilePath -DestinationPath $leetBuildExtractionRoot        }
        catch { Remove-Item -Recurse -Force $leetBuildExtractionRoot -ErrorAction Continue; throw }
    }
    catch   { Remove-Item -Recurse -Force $folderToRemoveOnError -ErrorAction Continue; throw }
    finally { Remove-Item $tempFilePath -ErrorAction Continue                                 }

    #Move-Item $leetBuildExtractionRoot $LeetBuildModuleRoot
    Write-Message -ForegroundColor $SuccessColor "Leet.Build tools successfully installed to '$LeetBuildModuleRoot'."
}

<#
.SYNOPSIS
Imports Leet.Build module from its install location.
#>
function Import-LeetBuildModule {
    Write-Debug "Importing 'Leet.Build' module"
    
    if (Get-Module 'Leet.Build') { Remove-LeetBuildModule }
    
	Write-Message -ForegroundColor $DiagnosticColor "Importing 'Leet.Build' module..."
    Import-Module -Force -Scope Global $LeetBuildModulePath                                   `
                  -ArgumentList $VerbosePreference, $DebugPreference, $InformationPreference, `
                                $ProjectDirectory, $SdkVersion, $DotNetHome, $ToolsVersion
}

<#
.SYNOPSIS
Unloads Leet.Build module.
#>
function Remove-LeetBuildModule {
    Write-Debug "Removing 'Leet.Build' module"
    Remove-Module 'Leet.Build' -ErrorAction Continue
}

######################################################################################################################
# Main
######################################################################################################################

try {
    Initialize-ScriptConfiguration
    Install-LeetBuild
    Import-LeetBuildModule

    try     { Invoke-LeetBuildCommand $Command $Arguments }
    finally { Remove-LeetBuildModule                      }

} finally {
    Write-Host
}
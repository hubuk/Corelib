@ECHO OFF
SETLOCAL enableDelayedExpansion

SET "PowerShellVersion=6.0.0-rc"
SET "ArchiveFileName=PowerShell-%PowerShellVersion%-win-x64.zip"
SET "ArchiveDownloadPath=https://github.com/PowerShell/PowerShell/releases/download/v%PowerShellVersion%/%ArchiveFileName%"
SET "ArchiveDestinationPath=%TEMP%\%ArchiveFileName%"
SET "InstallationPath=%userprofile%\PowerShell\%PowerShellVersion%"

CALL :FindPowerShellCore
SET "pwshPath=%result%"

IF [%pwshPath%]==[] (
  IF NOT EXIST %InstallationPath%\pwsh.exe (
    CALL :DownloadArchive %ArchiveDownloadPath% %ArchiveDestinationPath%
    IF %ERRORLEVEL% NEQ 0 (
	  ECHO Could not download PowerShell Core v%PowerShellVersion%.
      EXIT /B 1
    )
    
    CALL :ExtractArchive %ArchiveDestinationPath% %InstallationPath%
    IF %ERRORLEVEL% NEQ 0 (
	  DEL /F /Q %ArchiveDestinationPath%
	  ECHO Could not extract PowerShell Core v%PowerShellVersion%.
      EXIT /B 2
    )
	
	DEL /F /Q %ArchiveDestinationPath%
  )

  IF NOT EXIST %InstallationPath%\pwsh.exe (
    ECHO Could not install PowerShell Core v%PowerShellVersion%.
    EXIT /B 3
  )
  
  SET "pwshPath=%InstallationPath%\pwsh.exe"
)

%pwshPath% "%~dp0run.ps1" %*
GOTO End

:DownloadArchive
SETLOCAL
SET "ArchivePath=%1"
SET "DestinationPath=%2"
SET "PowerShellCommand="
SET "PowerShellCommand=%PowerShellCommand% [System.Threading.Thread]::CurrentThread.CurrentCulture = '' ;"
SET "PowerShellCommand=%PowerShellCommand% [System.Threading.Thread]::CurrentThread.CurrentUICulture = '' ;"
SET "PowerShellCommand=%PowerShellCommand% $ProgressPreference = 'SilentlyContinue' ;"
SET "PowerShellCommand=%PowerShellCommand% Invoke-WebRequest %ArchivePath% -OutFile %DestinationPath% ;"
SET "PowerShellCommand=%PowerShellCommand% exit $LASTEXITCODE"

ECHO Downloading PowerShell Core to %DestinationPath%...
powershell.exe -NoProfile -NoLogo -ExecutionPolicy unrestricted -Command "%PowerShellCommand%"

ENDLOCAL
GOTO End

:ExtractArchive
SETLOCAL
SET "ArchivePath=%1"
SET "DestinationPath=%2"
SET "PowerShellCommand="
SET "PowerShellCommand=%PowerShellCommand% [System.Threading.Thread]::CurrentThread.CurrentCulture = '' ;"
SET "PowerShellCommand=%PowerShellCommand% [System.Threading.Thread]::CurrentThread.CurrentUICulture = '' ;"
SET "PowerShellCommand=%PowerShellCommand% $ProgressPreference = 'SilentlyContinue' ;"
SET "PowerShellCommand=%PowerShellCommand% Add-Type -A 'System.IO.Compression.FileSystem' ;"
SET "PowerShellCommand=%PowerShellCommand% Expand-Archive -Path %ArchivePath% -DestinationPath %DestinationPath% -Force ;"
SET "PowerShellCommand=%PowerShellCommand% exit $LASTEXITCODE"

ECHO Extracting PowerShell Core archive to %DestinationPath%...
powershell.exe -NoProfile -NoLogo -ExecutionPolicy unrestricted -Command "%PowerShellCommand%"

ENDLOCAL
GOTO End

:FindPowerShellCore
SETLOCAL
SET pwshPath=
where /q pwsh.exe > NUL
IF %ERRORLEVEL% EQU 0 (
  FOR /F "tokens=*" %%p IN ('where pwsh.exe') DO (
    FOR /F "tokens=2" %%q IN ('"%%p" --version') DO (
      IF %%q==v%PowerShellVersion% SET pwshPath="%%p"
    )
  )
)

ENDLOCAL & SET result=%pwshPath%
GOTO End

:End
#!/usr/bin/env bash

current_folder="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
power_shell_version="6.0.0-rc"

function main {
    find_pwsh pwsh_path
    if [ -z "$pwsh_path" ]; then
	    install_power_shell_package

        find_pwsh pwsh_path
        if [ -z "$pwsh_path" ]; then
            echo "Could not install PowerShell v$power_shell_version."
            return 2
        fi
    fi

    $pwsh_path "$current_folder/run.ps1" "$@"
}

function find_pwsh() {
    local __resultvar=$1
    if machine_has pwsh; then
        paths=($(whereis pwsh))
        for path in ${paths[@]:1}
        do
            version=$($path --version | cut -d' ' -f2-)
            if [ "$version" == "v$power_shell_version" ]; then
                eval $__resultvar="'$path'"
                return 0
            fi
        done
		
		path=$(which pwsh)
		if [ ! -z "$path" ]; then
		    version=$($path --version | cut -d' ' -f2-)
            if [ "$version" == "v$power_shell_version" ]; then
                eval $__resultvar="'$path'"
            fi
		fi
    fi
}

function install_power_shell_package() {
    local file_name_suffix="-1.ubuntu.14.04_amd64.deb" && [[ "$(uname)" = "Darwin" ]] && file_name_suffix="-osx.10.12-x64.pkg"
    local file_name_version_connectior="_" && [[ "$(uname)" = "Darwin" ]] && file_name_version_connectior="-"
    local file_name="powershell$file_name_version_connectior$power_shell_version$file_name_suffix"
    local destination_path="/tmp/$file_name"
    local download_path="https://github.com/PowerShell/PowerShell/releases/download/v$power_shell_version/$file_name";

    echo "Downloading PowerShell Core to $destination_path..."
    local failed=false
    if machine_has wget; then
        wget --tries 10 --quiet -O "$destination_path" "$download_path" || failed=true
    else
        failed=true
    fi

    if [ "$failed" = true ] && machine_has curl; then
        failed=false
        curl --retry 10 -sSL -f --create-dirs -o "$destination_path" "$download_path" || failed=true
    fi

    if [ "$failed" = true ]; then
        echo "Could not download PowerShell Core v$power_shell_version" 1>&2
        return 1
    fi

    echo "Instaling PowerShell Core package..."
    if [ "$(uname)" = "Darwin" ]; then
        sudo installer -pkg $destination_path -target /
    else
        sudo dpkg -i $destination_path
        sudo apt-get install -f
    fi

    rm -f $destination_path
}


function machine_has() {
    hash "$1" > /dev/null 2>&1
    return $?
}

main

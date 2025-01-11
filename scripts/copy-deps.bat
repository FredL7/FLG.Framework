:: Copy the FLG.Framework build dir into the dependecies folder of your current project
:: Uses relative paths, so ensure current working directory is FLG.Framework/scripts

@echo off
if "%~1"=="" (
    echo Usage: copy-deps.bat "path/to/output/dir"
    exit /b 1
)

set TARGET_DIR=%~1
set TARGET_DIR_DEBUG="%TARGET_DIR%\Debug"
set TARGET_DIR_RELEASE="%TARGET_DIR%\Release"

if not exist "%TARGET_DIR%" (
    echo Error: Target dir does not exists
    exit /b 1
)

del /Q /F /S "%TARGET_DIR%\*"
for /d %%p in ("%TARGET_DIR%\*") do rd /s /q "%%p"

if not exist "%TARGET_DIR_DEBUG%" (
    mkdir %TARGET_DIR_DEBUG%
)

if not exist "%TARGET_DIR_RELEASE%" (
    mkdir %TARGET_DIR_RELEASE%
)

xcopy /E /I /Y "..\_build\bin\Debug\*" %TARGET_DIR_DEBUG%
xcopy /E /I /Y "..\_build\bin\Release\*.dll" %TARGET_DIR_RELEASE%

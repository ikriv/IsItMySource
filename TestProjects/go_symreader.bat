@echo off
setlocal enabledelayedexpansion

if NOT "%1" == "" goto :OneFile

for %%f IN (^
	Vs2015Test\ClrClassLibrary\bin\Debug\ClrClassLibrary.dll^
	Vs2015Test\ClrClassLibrary\bin\Release\ClrClassLibrary.dll^
        Vs2015Test\ClrConsoleApp\bin\Debug\ClrConsoleApp.exe^
        Vs2015Test\ManagedCppConsoleApp\Win32\Debug\ManagedCppConsoleApp.exe^
        Vs2015Test\ClrConsoleApp\bin\Release\ClrConsoleApp.exe)^
DO call go_symreader %%f
goto :End

:OneFile
set EXE=..\src\IsItMySource\bin\Debug\IsItMySource.exe
set INFILE=%1
set OUTFILE=sym_%INFILE:\=_%.txt
echo %INFILE%
%EXE% %INFILE% >%OUTFILE%

:End



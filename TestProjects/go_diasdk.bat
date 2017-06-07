@echo off
setlocal enabledelayedexpansion

if NOT "%1" == "" goto :OneFile

for %%f IN (^
	Vs2015Test\ClrClassLibrary\bin\Debug\ClrClassLibrary.pdb^
	Vs2015Test\ClrClassLibrary\bin\Release\ClrClassLibrary.pdb^
        Vs2015Test\ClrConsoleApp\bin\Debug\ClrConsoleApp.pdb^
        Vs2015Test\ClrConsoleApp\bin\Release\ClrConsoleApp.pdb^
        Vs2015Test\ManagedCppConsoleApp\Win32\Debug\ManagedCppConsoleApp.pdb^
        Vs2015Test\Win32ConsoleApp\Debug\Win32ConsoleApp.pdb^
        Vs2015Test\Win32ConsoleApp\Release\Win32ConsoleApp.pdb)^
DO call go_diasdk %%f
goto :End

:OneFile
set EXE=..\src\IsItMySource\bin\Debug\IsItMySource.exe
set INFILE=%1
set OUTFILE=dia_%INFILE:\=_%.txt
echo %INFILE%
%EXE% --native %INFILE% >%OUTFILE%

:End



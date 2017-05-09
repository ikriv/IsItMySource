@echo off

for %%f IN (^
	Vs2015Test\ClrClassLibrary\bin\Debug\ClrClassLibrary.pdb^
	Vs2015Test\ClrClassLibrary\bin\Release\ClrClassLibrary.pdb^
        Vs2015Test\ClrConsoleApp\bin\Debug\ClrConsoleApp.pdb^
        Vs2015Test\ClrConsoleApp\bin\Release\ClrConsoleApp.pdb^
        Vs2015Test\Win32ConsoleApp\Debug\Win32ConsoleApp.pdb^
        Vs2015Test\Win32ConsoleApp\Release\Win32ConsoleApp.pdb)^
DO call go1 %%f

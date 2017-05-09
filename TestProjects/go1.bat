@echo off
setlocal enabledelayedexpansion
set EXE=..\src\UsingDiaSdk\IsItMySource\bin\Debug\IsItMySource.exe
set INFILE=%1
set OUTFILE=%INFILE:\=_%
echo %INFILE%
%EXE% %INFILE% >%OUTFILE%

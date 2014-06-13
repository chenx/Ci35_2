@echo off

REM cls

echo Initialize MSSQL database for your C#.NET website ...

REM Get command line parameters.
set db=%1

REM if "" == %db% goto LErrBat
REM if "%db%" == "" goto LErrBat

REM ---------------------------------------------------------------------------------
REM -- Change the value of "db" if you want to use a different name for the database.
REM ---------------------------------------------------------------------------------
set db="CM" 

echo.
echo Create database ...
sqlcmd -E -b -S localhost -i makedb.sql -v dbname=%db%
IF ERRORLEVEL 1 goto LErrSql

REM echo.
REM echo Load data into database ...
REM sqlcmd -E -b -S localhost -i load_data.sql -v dbname=%db%
REM IF ERRORLEVEL 1 goto LErrSql

goto LExit

:LErrBat
echo Usage: init.bat [dbname]
REM echo.
goto LExit

:LErrSql
echo Sql execution error. 
goto LExit

:LExit
echo.
REM pause 


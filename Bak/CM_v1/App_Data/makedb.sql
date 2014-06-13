--
-- @Decription: 
--   This T-SQL script (for MSSQL 2008) create a default database for the Ci35 C#.NET project.
--   If the database already exists, it will be dropped and recreated from scratch.
-- @Usage: sqlcmd -E -b -S localhost -i makedb.sql
--   
-- @Help: Use sqlcmd /? to see meaning of switches. 
--   -E: trusted connection
--   -b: On error batch abort
--   -S: server
--   -i: input file
--   -o: output file
--   -e: echo input
--   -v var="value" ...: input parameter.
--
-- @By: X.C.
-- @First created on: 5/28/2014
-- @Last modified on: 5/30/2014
--
-- Reference:
-- - Create a database in T-SQL:
--   http://msdn.microsoft.com/en-us/library/ms176061.aspx
-- - Database recovery mode:
--   http://msdn.microsoft.com/en-us/library/ms189272.aspx
--

-- :setvar dbname "CM"

--
-- Create database.
--

USE MASTER
GO

IF db_name() != 'MASTER'
BEGIN
    RAISERROR( 'Unable to use database MASTER', 20, 2 ) WITH LOG -- 20: severity, 2: state
    --EXIT -- This will cause this script to abort no matter what. Don't use this.
    RETURN -- exit the execution of this file immediately
END


-- DROP DATABASE if exists.
IF EXISTS (SELECT * FROM sys.databases WHERE name = '$(dbname)')
BEGIN
    PRINT 'Database $(dbname) already exists. '
    --PRINT 'Abort.'
    --RETURN
    --ALTER DATABASE $(dbname) SET OFFLINE WITH ROLLBACK IMMEDIATE   -- This does not work.
    ALTER DATABASE $(dbname) SET SINGLE_USER WITH ROLLBACK IMMEDIATE -- close connections.
    
    DROP DATABASE $(dbname)
    PRINT 'Database $(dbname) dropped'
END


-- CREATE DATABASE if not already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$(dbname)')
BEGIN
    CREATE DATABASE $(dbname)
    PRINT 'Database $(dbname) created'
END

-- ALTER DATABASE CM SET RECOVERY SIMPLE -- simple/full/bulk-logged.
ALTER DATABASE $(dbname) SET RECOVERY SIMPLE

PRINT 'Database $(dbname) has been created.'
GO

--
-- Create tables in the database.
--

-- Change current database to the new database.
USE $(dbname)
GO

PRINT 'Creat table User ...'
IF OBJECT_ID('User', 'U') IS NOT NULL 
    DROP TABLE [User]
GO
CREATE TABLE [User] (
    ID         int NOT NULL IDENTITY(1, 1), -- AUTO_INCREMENT,
    first_name varchar(50) NOT NULL,
    last_name  varchar(50) NOT NULL,
    email      varchar(100) NOT NULL,
    login      varchar(50) NOT NULL UNIQUE,
    passwd     varbinary(16) NOT NULL,             -- MD5 value, At least 16 bits. Must be varbinary!
    note       varchar(50),
    gid        int NOT NULL DEFAULT 1,            -- UserGroup ID.
	[create_user_id] [int] NULL,
	[create_datetime] [datetime] NULL,
	[last_update_user_id] [int] NULL,
	[last_update_datetime] [datetime] NULL,
	[disabled] [bit] NULL DEFAULT '0'
    CONSTRAINT PK_User PRIMARY KEY CLUSTERED  (ID)
)
GO
-- OR: ALTER TABLE [User] ADD ID int NOT NULL IDENTITY (1,1) PRIMARY KEY


PRINT 'Creat table UserGroup ...'
IF OBJECT_ID('UserGroup', 'U') IS NOT NULL 
    DROP TABLE [UserGroup]
GO
CREATE TABLE [UserGroup] (
    ID int NOT NULL PRIMARY KEY,
    [name] varchar(20) -- 'group' is a reserved word.
)
GO


--
-- Load data
--

PRINT 'Load data into table User ...'
INSERT INTO [User] (first_name, last_name, email, login, passwd, note, gid) VALUES (
  'Demo', 'Admin', 'admin@gmail.com', 'admin', HASHBYTES('MD5', 'password'), '', 1
)
INSERT INTO [User] (first_name, last_name, email, login, passwd, note, gid) VALUES (
  'Demo', 'User', 'test@gmail.com', 'user', HASHBYTES('MD5', 'password'), 'Test special char <''>".', 2
)


PRINT 'Load data into table UserGroup ...'
INSERT INTO UserGroup (ID, [name]) VALUES (1, 'admin')
INSERT INTO UserGroup (ID, [name]) VALUES (2, 'user')


--SET IDENTITY_INSERT Client ON
--SET IDENTITY_INSERT Client OFF


PRINT 'Load data finished.'
GO
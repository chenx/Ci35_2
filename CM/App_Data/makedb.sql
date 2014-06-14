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
-- @First created on: 6/20/2014
-- @Last modified on: 6/20/2014
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


PRINT 'Creat table ClientType ...'
IF OBJECT_ID('ClientType', 'U') IS NOT NULL 
    DROP TABLE ClientType
GO
CREATE TABLE [ClientType] (
    ID int NOT NULL PRIMARY KEY,
    [name] varchar(20) -- 'group' is a reserved word.
)
GO


PRINT 'Creat table Client ...'
IF OBJECT_ID('Client', 'U') IS NOT NULL 
    DROP TABLE [Client]
GO
CREATE TABLE [dbo].[Client](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [Case_Id] [int] NOT NULL UNIQUE,
    [Client_Type] [int] NULL,
    [First_Name] [varchar](100) NULL,
    [Last_Name] [varchar](100) NULL,
    [Attorney] [varchar](100) NULL,
    [Paralegal] [varchar](100) NULL,
    [Date_Of_Injury] [date] NULL,
    [Statute_Of_Limitation] [varchar](100) NULL,
    [Phone_Number] [varchar](100) NULL,
    [Address] [varchar](100) NULL,
    [Date_Of_Birth] [date] NULL,
    [Social_Security_Number] [varchar](11) NULL,
    [Case_Type] [varchar](100) NULL,
    [At_Fault_Party] [varchar](100) NULL,
    [Settlement_Type] [varchar](100) NULL,
    [Settlement_Amount] [varchar](100) NULL,
    [Disposition] [varchar](100) NULL,
    [Case_Notes] [varchar](max) NULL,
    [Date_For_Perspective_Client] [date] NULL,
    [create_user_id] [int] NULL,
    [create_datetime] [datetime] NULL,
    [last_update_user_id] [int] NULL,
    [last_update_datetime] [datetime] NULL,
    [disabled] [bit] NULL DEFAULT '0'
 CONSTRAINT [PK__Client__3214EC270519C6AF] PRIMARY KEY CLUSTERED 
(
    [Case_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


PRINT 'Creat table Attorney ...'
IF OBJECT_ID('Attorney', 'U') IS NOT NULL 
    DROP TABLE [Attorney]
GO
CREATE TABLE [Attorney] (
    ID int NOT NULL PRIMARY KEY,
    [name] varchar(20) -- 'group' is a reserved word.
)
GO


PRINT 'Creat table Paralegal ...'
IF OBJECT_ID('Paralegal', 'U') IS NOT NULL 
    DROP TABLE [Paralegal]
GO
CREATE TABLE [Paralegal] (
    ID int NOT NULL PRIMARY KEY,
    [name] varchar(20) -- 'group' is a reserved word.
)
GO


PRINT 'Creat view V_Client ...'
IF EXISTS(select * FROM sys.views where name = '')
    DROP VIEW [V_Client]
Go
CREATE VIEW [dbo].[V_Client]
AS
SELECT C.ID, C.Case_Id, C.Client_Type, T.name AS Client_Type_Name, C.First_Name, C.Last_Name, 
       A.name AS Attorney, P.name AS Paralegal, C.Date_Of_Injury, 
       C.Statute_Of_Limitation, C.Phone_Number, C.Address, C.Date_Of_Birth, C.Social_Security_Number, 
       C.Case_Type, C.At_Fault_Party, C.Settlement_Type, 
       C.Settlement_Amount, C.Disposition, C.Case_Notes, C.Date_For_Perspective_Client, C.disabled
FROM   dbo.Client AS C LEFT OUTER JOIN
       dbo.ClientType AS T ON C.Client_Type = T.ID LEFT OUTER JOIN
       dbo.Attorney AS A ON C.Attorney = A.ID LEFT OUTER JOIN
       dbo.Paralegal AS P ON C.Paralegal = P.ID
GO


--
-- Load data
--

PRINT 'Load data into table User ...'
INSERT INTO [User] (first_name, last_name, email, login, passwd, note, gid) VALUES 
('Demo', 'Admin', 'txchen@gmail.com', 'admin', HASHBYTES('MD5', 'password'), '', 1),
('Demo', 'User', 'test@gmail.com', 'user', HASHBYTES('MD5', 'password'), 'Test special char <''>".', 2)


PRINT 'Load data into table UserGroup ...'
INSERT INTO UserGroup (ID, [name]) VALUES 
(1, 'admin'),
(2, 'user')


PRINT 'Load data into table ClientType ...'
INSERT INTO ClientType (ID, [name]) VALUES 
(1, 'Prospective client'),
(2, 'Current client'),
(3, 'Former client')


PRINT 'Load data into table Client ...'
--SET IDENTITY_INSERT Client ON

INSERT INTO Client (Case_Id, Client_Type, First_Name, Last_Name, Attorney, Paralegal,
Date_Of_Injury, Statute_Of_Limitation, Phone_Number, Address, Date_Of_Birth, Social_Security_Number,
Case_Type, At_Fault_Party, Settlement_Type, Settlement_Amount, Disposition, Case_Notes,
Date_For_Perspective_Client)
values
('1000', '1', 'Abel', 'Tim', '1', '1', '2011/3/2', 'limitation',
'808-222-3333', '123 Ali St.', '1970/2/3', '111-22-3333', 'case type 1', 'faulty party',
'settle type 1', '$22000', 'dismissed', 'notes', '2014/7/6'),
('1001', '2', 'Bob', 'Tham', '2', '1', '2011/3/2', 'limitation',
'808-222-3333', '123 Ali St.', '1970/2/3', '111-22-3333', 'case type 2', 'faulty party',
'settle type 2', '$22000', 'dismissed', 'notes', '2014/7/6'),
('1002', '3', 'Carol', 'Tham', '3', '2', '2011/3/2', 'limitation',
'808-222-3333', '123 Ali St.', '1970/2/3', '111-22-3333', 'case type 3', 'faulty party',
'settle type 3', '$22000', 'dismissed', 'notes', '2014/7/6')

--SET IDENTITY_INSERT Client OFF


PRINT 'Load data into table Attorney ...'
INSERT INTO Attorney (ID, [name]) VALUES 
(1, 'J.D.Y'),
(2, 'W.K.S'),
(3, 'J.T.L')


PRINT 'Load data into table Paralegal ...'
INSERT INTO Paralegal (ID, [name]) VALUES 
(1, 'Y.B.'),
(2, 'L.E.')


PRINT 'Load data finished.'
GO
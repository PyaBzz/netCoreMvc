
IF NOT EXISTS (SELECT * FROM master.sys.databases WHERE name = '##dbname##')
BEGIN
    PRINT 'Creating ##dbname##'
    CREATE DATABASE ##dbname##
END
ELSE
BEGIN
    PRINT 'DB already exists'
END

GO

USE ##dbname##
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'DummiesA')
BEGIN
    print 'Creating DummiesA'
    CREATE TABLE DummiesA
    (
        Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(20),
        RefId UNIQUEIDENTIFIER NOT NULL,
        NullableRefId UNIQUEIDENTIFIER,
        Qty INT,
    )
END
ELSE
BEGIN
    PRINT 'DummiesA already exists'
END

USE ##dbname##
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'WorkPlans')
BEGIN
    print 'Creating WorkPlans'
    CREATE TABLE WorkPlans
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(20)
    )
END
ELSE
BEGIN
    PRINT 'WorkPlans already exists'
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'WorkItems')
BEGIN
    print 'Creating WorkItems'
    CREATE TABLE WorkItems
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Reference VARCHAR(20),
        Priority INT,
        Name VARCHAR(20),
        WorkPlanId  UNIQUEIDENTIFIER
    )
END
ELSE
BEGIN
    PRINT 'WorkItems already exists'
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'Users')
BEGIN
    print 'Creating Users'
    CREATE TABLE Users
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(20),
        DateOfBirth DATE,
        Role VARCHAR(20),
        Salt VARBINARY(128),
        Hash VARCHAR(90)
    )
END
ELSE
BEGIN
    PRINT 'Users already exists'
END

GO

PRINT N'Database initialised'
GO
WAITFOR DELAY '00:00:01'

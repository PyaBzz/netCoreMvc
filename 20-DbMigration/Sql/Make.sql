
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
        MandatoryRefId UNIQUEIDENTIFIER NOT NULL,
        Qty INT NOT NULL,
    )
END
ELSE
BEGIN
    PRINT 'DummiesA already exists'
END

USE ##dbname##
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'Products')
BEGIN
    print 'Creating Products'
    CREATE TABLE Products
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Name VARCHAR(20)
    )
END
ELSE
BEGIN
    PRINT 'Products already exists'
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'Orders')
BEGIN
    print 'Creating Orders'
    CREATE TABLE Orders
    (
        Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
        Reference VARCHAR(20),
        Priority INT,
        Name VARCHAR(20),
        ProductId  UNIQUEIDENTIFIER NOT NULL,
        FOREIGN KEY (ProductId) REFERENCES Products(Id)
    )
END
ELSE
BEGIN
    PRINT 'Orders already exists'
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

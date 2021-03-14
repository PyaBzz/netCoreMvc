
IF NOT EXISTS (SELECT * FROM master.sys.databases WHERE name = 'bazDb')
BEGIN
    PRINT 'Creating bazDb'
    CREATE DATABASE bazDb
END
ELSE
BEGIN
    PRINT 'DB already exists'
END

GO

USE bazDb
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'WorkPlans')
BEGIN
    print 'Creating WorkPlans'
    CREATE TABLE WorkPlans
    (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
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
        Id UNIQUEIDENTIFIER PRIMARY KEY,
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
        id INT,
        Name VARCHAR(20),
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

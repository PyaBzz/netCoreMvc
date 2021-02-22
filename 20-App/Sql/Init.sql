
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

INSERT INTO WorkPlans VALUES ('DA98695E-A126-44E0-984B-DBFAEE50031B', 'WorkPlanA')
INSERT INTO WorkPlans VALUES ('DC2A8B2C-80FD-4344-8D30-67E94E4E77E6', 'WorkPlanB')

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.tables t WHERE t.TABLE_NAME = 'WorkItems')
BEGIN
    print 'Creating WorkItems'
    CREATE TABLE WorkItems
    (
        student_id INT,
        course_id INT,
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


IF EXISTS (SELECT * FROM master.sys.databases WHERE name = 'bazDb')
BEGIN
    PRINT 'Destroying DB'
    EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
    EXEC sp_msforeachtable 'DROP TABLE bazDb.?'
    
    USE MASTER
    ALTER DATABASE bazDb SET single_user WITH ROLLBACK IMMEDIATE
    DROP DATABASE bazDb
END
ELSE
BEGIN
    PRINT 'DB not found'
END

PRINT N'Database destroyed'
GO
WAITFOR DELAY '00:00:01'

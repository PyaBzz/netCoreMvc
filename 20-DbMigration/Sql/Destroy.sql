
IF EXISTS (SELECT * FROM master.sys.databases WHERE name = '##dbname##')
BEGIN
    PRINT 'Destroying DB'
    EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
    EXEC sp_msforeachtable 'DROP TABLE ##dbname##.?'
    
    USE MASTER
    ALTER DATABASE ##dbname## SET single_user WITH ROLLBACK IMMEDIATE
    DROP DATABASE ##dbname##
END
ELSE
BEGIN
    PRINT 'DB not found'
END

PRINT N'Database destroyed'
GO
WAITFOR DELAY '00:00:01'

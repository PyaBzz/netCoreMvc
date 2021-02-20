DECLARE @DbName nvarchar(50)
SET @DbName = N'bazDb'

IF EXISTS (SELECT * FROM master.sys.databases WHERE name = @DbName)
    BEGIN
    EXEC ('USE ' + @DbName)
    EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
    EXEC sp_msforeachtable 'DROP ?'
    
    USE MASTER
    EXEC('ALTER DATABASE ' + @DbName + ' SET single_user WITH ROLLBACK IMMEDIATE')
    EXEC('DROP DATABASE ' + @DbName)
end

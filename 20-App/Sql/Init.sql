DECLARE @DbName nvarchar(50)
SET @DbName = N'bazDb'

IF NOT EXISTS (SELECT * FROM master.sys.databases WHERE name = @DbName)
BEGIN
    DECLARE @SQL varchar(max)
    SET @SQL = REPLACE('CREATE DATABASE {DbName}', '{DbName}', @DbName)
    EXEC(@SQL)
    
    SET @SQL = REPLACE('USE {DbName}', '{DbName}', @DbName) --Todo: simplify with string concatenation
    EXEC(@SQL)

    CREATE TABLE WorkPlans
    (
        id INT PRIMARY KEY,
        name VARCHAR(20)
    )

    CREATE TABLE WorkItems
    (
        student_id INT,
        course_id INT,
    )

    CREATE TABLE Users
    (
        id INT,
        Name VARCHAR(20),
    )

    PRINT N'Database initialised'
END



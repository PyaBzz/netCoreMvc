IF NOT EXISTS (SELECT * FROM master.sys.databases WHERE name = 'baz')
    CREATE DATABASE baz;

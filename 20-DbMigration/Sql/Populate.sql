USE ##dbname##

INSERT INTO WorkPlans VALUES ('60f9fc29-083f-4ed2-a3e2-3948b503c25f', 'Plan1')
INSERT INTO WorkPlans VALUES ('53c88402-4092-4834-8e7f-6ce70057cdc5', 'Plan2')

-- INSERT INTO WorkItems VALUES ('7073ad87-4695-4a0b-b2c3-fa794d5ffa21', 'Wi11', 1, 'FirstItem', '60f9fc29-083f-4ed2-a3e2-3948b503c25f')
-- INSERT INTO WorkItems VALUES ('5fc4bfcf-24e0-430a-8889-03b2f31387e1', 'Wi12', 2, 'SecondItem', '60f9fc29-083f-4ed2-a3e2-3948b503c25f')
-- INSERT INTO WorkItems VALUES ('eb66287b-1cde-421e-868e-a0df5b21a90d', 'Wi21', 3, 'ThirdItem', '53c88402-4092-4834-8e7f-6ce70057cdc5')

GO

PRINT N'Database populated'
GO
WAITFOR DELAY '00:00:01'

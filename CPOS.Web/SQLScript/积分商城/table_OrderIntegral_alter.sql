
ALTER TABLE dbo.OrderIntegral ADD
	OrderNo varchar(50) NULL,
	IsSynced int NOT NULL CONSTRAINT DF_OrderIntegral_IsSynced DEFAULT 0,
	SyncCount int NOT NULL CONSTRAINT DF_OrderIntegral_SyncCount DEFAULT 0
GO
/****** Object:  StoredProcedure [dbo].[DynamicVipFormLoad]    Script Date: 07/24/2014 13:56:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DynamicVipFormLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DynamicVipFormLoad]
GO

/****** Object:  StoredProcedure [dbo].[DynamicVipFormLoad]    Script Date: 07/24/2014 13:56:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-24
-- Description:	Load Dynamic Vip Form
-- =============================================
CREATE PROCEDURE [dbo].[DynamicVipFormLoad] --'4c564ea8-b2e5-46aa-a5c4-835b0c434943', 'e703dbedadd943abacf864531decdac1'
	
	@FormID VARCHAR(50),
	@CustomerID VARCHAR(50),
	@DEBUG INT = 0
	
AS
BEGIN
	
	SELECT * INTO #Scene FROM Options WHERE OptionName = 'MobileModuleScene' AND IsDelete = 0
	SELECT * INTO #ControlType FROM Options WHERE OptionName = 'ClientBussinessDefinedControlType' AND IsDelete = 0
	
	SELECT ModuleName FROM [MobileModule] WHERE CustomerID = @CustomerID AND CAST(MobileModuleID AS VARCHAR(50)) = @FormID

	--declare @FormID varchar(50) ='F9323FFA-80F7-4262-885F-9435D8413E78', @CustomerID varchar(50) = 'e703dbedadd943abacf864531decdac1'
	SELECT * FROM
	(
		SELECT cbd.ClientBussinessDefinedID, ModuleName, MobileBussinessDefinedID, ISNULL(mbd.ColumnDesc, cbd.ColumnDesc) AS ColumnDesc, ISNULL(mbd.ControlType, cbd.ControlType) AS ControlType, ISNULL(ISNULL(mbd.DisplayType, cbd.DisplayType), 0) AS DisplayType, ISNULL(ISNULL(mbd.IsDefaultProp, cbd.IsDefaultProp), 0) AS IsDefaultProp, ISNULL(ISNULL(mbd.IsMustDo, cbd.IsMustDo), 0) AS IsMustDo, ISNULL(ISNULL(mbd.EditOrder, cbd.EditOrder), 9999) AS EditOrder, CASE WHEN mpb.MobilePageBlockID IS NULL THEN 0 ELSE 1 END AS IsUsed
			FROM [MobileModule] mm
				LEFT JOIN MobilePageBlock mpb ON mm.MobileModuleID = mpb.MobileModuleID AND mm.CustomerID = @CustomerID AND CAST(mm.MobileModuleID AS VARCHAR(50)) = @FormID AND mpb.[Type] = 2
				LEFT JOIN MobileBussinessDefined mbd ON mpb.MobilePageBlockID = mbd.MobilePageBlockID AND mbd.CustomerID = @CustomerID AND mbd.IsDelete = 0
				RIGHT JOIN ClientBussinessDefined cbd ON mbd.ColumnName = cbd.ColumnName
			WHERE cbd.ClientID = @CustomerID AND cbd.IsDelete = 0 AND cbd.TableName = 'VIP'
	) A
	ORDER BY EditOrder
	DECLARE @TEMP TABLE(id int identity, columnname varchar(5))
	DECLARE @i INT = 1

	WHILE @i <= 50
	BEGIN
		INSERT INTO @TEMP (columnname) VALUES ('col' + CAST(@i AS VARCHAR(3)))
		SET @i = @i + 1
	END
	DECLARE @EXISTCOUNT INT;
	SELECT @EXISTCOUNT = COUNT(columnname) FROM  @TEMP t WHERE NOT EXISTS (SELECT 1 FROM ClientBussinessDefined cbd WHERE t.columnname = cbd.ColumnName AND cbd.TableName = 'VIP' AND cbd.IsDelete = 0) 
	--declare @FormID varchar(50) ='F9323FFA-80F7-4262-885F-9435D8413E78', @CustomerID varchar(50) = 'e703dbedadd943abacf864531decdac1'
	SELECT sc.[OptionValue] AS SceneValue, sc.OptionText AS SceneName, CASE WHEN mmom.MobileModuleID IS NULL THEN 0 ELSE 1 END AS IsSelected 
		FROM MobileModuleObjectMapping mmom
			RIGHT JOIN #Scene sc ON mmom.ObjectID = CAST(sc.[OptionValue] AS VARCHAR(10)) AND CAST(mmom.MobileModuleID AS VARCHAR(50)) = @FormID AND mmom.CustomerID = @CustomerID AND mmom.IsDelete = 0
		ORDER BY OptionsID
	SELECT @EXISTCOUNT AS EXISTCOUNT;	
	IF @DEBUG = 1
	BEGIN
		SELECT * FROM #Scene
		SELECT * FROM #ControlType
	END

END

GO
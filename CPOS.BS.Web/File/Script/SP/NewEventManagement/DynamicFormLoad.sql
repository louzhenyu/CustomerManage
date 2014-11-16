/****** Object:  StoredProcedure [dbo].[DynamicFormLoad]    Script Date: 07/24/2014 13:56:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DynamicFormLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DynamicFormLoad]
GO

/****** Object:  StoredProcedure [dbo].[DynamicFormLoad]    Script Date: 07/24/2014 13:56:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-31
-- Description:	Load Dynamic Form
-- =============================================
CREATE PROCEDURE [dbo].[DynamicFormLoad] --'af46c7d0-76df-4a28-b09c-3480d9155545', 'e703dbedadd943abacf864531decdac1', 'LEventSignUp'
	
	@FormID VARCHAR(50),
	@CustomerID VARCHAR(50),
	@TableName VARCHAR(50)
	
AS
BEGIN
	
	SELECT * INTO #ControlType FROM Options WHERE OptionName = 'ClientBussinessDefinedControlType' AND IsDelete = 0 AND ([CustomerID] IS NULL OR [CustomerID] = '' OR [CustomerID] = @CustomerID)
	SELECT * INTO #Hierarchy FROM Options WHERE OptionName = 'ClientBussinessDefinedHierarchy' AND IsDelete = 0 AND ([CustomerID] IS NULL OR [CustomerID] = '' OR [CustomerID] = @CustomerID)
	
	SELECT ModuleName FROM [MobileModule] WHERE CustomerID = @CustomerID AND CAST(MobileModuleID AS VARCHAR(50))= @FormID

	--declare @FormID varchar(50) ='af46c7d0-76df-4a28-b09c-3480d9155545', @CustomerID varchar(50) = 'e703dbedadd943abacf864531decdac1', @TableName varchar(50) = 'LEventSignUp'
	SELECT * FROM
	(
		SELECT cbd.ClientBussinessDefinedID, ModuleName, MobileBussinessDefinedID, ISNULL(mbd.ColumnDesc, cbd.ColumnDesc) AS ColumnDesc, ISNULL(mbd.ControlType, cbd.ControlType) AS ControlType, ISNULL(ISNULL(mbd.DisplayType, cbd.DisplayType), 0) AS DisplayType, ISNULL(ISNULL(mbd.IsDefaultProp, cbd.IsDefaultProp), 0) AS IsDefaultProp, ISNULL(ISNULL(mbd.IsMustDo, cbd.IsMustDo), 0) AS IsMustDo, ISNULL(ISNULL(mbd.EditOrder, cbd.EditOrder), 9999) AS EditOrder, CASE WHEN mpb.MobilePageBlockID IS NULL THEN 0 ELSE 1 END AS IsUsed, h.OptionText AS Hierarchy
			FROM [MobileModule] mm
				LEFT JOIN MobilePageBlock mpb ON mm.MobileModuleID = mpb.MobileModuleID AND mm.CustomerID = @CustomerID AND CAST(mm.MobileModuleID AS VARCHAR(50)) = @FormID AND mpb.[Type] = 2
				LEFT JOIN MobileBussinessDefined mbd ON mpb.MobilePageBlockID = mbd.MobilePageBlockID AND mbd.CustomerID = @CustomerID AND mbd.IsDelete = 0 AND mbd.TableName = @TableName
				RIGHT JOIN ClientBussinessDefined cbd ON mbd.ColumnName = cbd.ColumnName
				LEFT JOIN #Hierarchy h ON cbd.[HierarchyID] = h.OptionValue
			WHERE cbd.ClientID = @CustomerID AND cbd.IsDelete = 0 AND cbd.TableName = @TableName
	) A
	ORDER BY EditOrder

END

GO
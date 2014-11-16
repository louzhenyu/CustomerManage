/****** Object:  StoredProcedure [dbo].[DynamicFormSave]    Script Date: 07/24/2014 13:56:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DynamicFormSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DynamicFormSave]
GO

/****** Object:  StoredProcedure [dbo].[DynamicFormSave]    Script Date: 07/24/2014 13:56:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-08-19
-- Description:	Save Dynamic Form Settings
-- =============================================
CREATE PROCEDURE [dbo].[DynamicFormSave] --'F9323FFA-80F7-4262-885F-9435D8413E78', 'e703dbedadd943abacf864531decdac1'
	
	@FormID VARCHAR(50),
	@TableName VARCHAR(50),
	@CustomerID VARCHAR(50),
	@UserID VARCHAR(50),
	@FieldList DynamicVipFormField READONLY,
	@Debug INT = 0
	
AS
BEGIN

	DECLARE @MobilePageBlockID VARCHAR(50)
	
	SET XACT_ABORT ON	--ROLLBACK ON FAILURE
	BEGIN TRAN

		SELECT @MobilePageBlockID = MobilePageBlockID FROM MobilePageBlock WHERE MobileModuleID = @FormID AND [Type] = 2
		
		IF @MobilePageBlockID IS NULL
		BEGIN
			
			DECLARE @ParentMobilePageBlockID VARCHAR(50)
			SET @ParentMobilePageBlockID = NEWID()
			
			INSERT INTO MobilePageBlock (
				[MobilePageBlockID]
				,[TableName]
			  ,[Title]
			  ,[Type]
			  ,[Sort]
			  ,[ParentID]
			  ,[CustomerID]
			  ,[CreateBy]
			  ,[CreateTime]
			  ,[IsDelete]
			  ,[MobileModuleID]) 
			VALUES (@ParentMobilePageBlockID, @TableName, '页', 1, 1, NULL, @CustomerID, @UserID, GETDATE(), 0, @FormID)
			
			INSERT INTO MobilePageBlock (
				[TableName]
			  ,[Title]
			  ,[Type]
			  ,[Sort]
			  ,[ParentID]
			  ,[CustomerID]
			  ,[CreateBy]
			  ,[CreateTime]
			  ,[IsDelete]
			  ,[MobileModuleID]) 
			VALUES (@TableName, '块', 2, 1, @ParentMobilePageBlockID, @CustomerID, @UserID, GETDATE(), 0, @FormID)
			
			SELECT @MobilePageBlockID = MobilePageBlockID FROM MobilePageBlock WHERE MobileModuleID = @FormID AND [Type] = 2
			
		END
		
		--UPDATE USED CONTROLS
		UPDATE mbd
			SET mbd.ColumnDesc = t.ColumnDesc, mbd.ControlType = t.ControlType, mbd.IsMustDo = t.IsMustDo, mbd.EditOrder = t.EditOrder, mbd.LastUpdateBy = @UserID, mbd.LastUpdateTime = GETDATE()
		--SELECT *
			FROM MobileBussinessDefined mbd
				LEFT JOIN @FieldList t ON CAST(mbd.MobileBussinessDefinedID AS VARCHAR(50)) = t.FormControlID
			WHERE mbd.MobilePageBlockID = @MobilePageBlockID

		--DELTE NOT USED CONTROLS
		UPDATE mbd SET IsDelete = 1 
			FROM MobileBussinessDefined mbd 
				WHERE mbd.TableName = @TableName AND mbd.MobilePageBlockID = @MobilePageBlockID AND mbd.CustomerID = @CustomerID AND IsDelete = 0
					AND NOT EXISTS (SELECT 1 FROM @FieldList t WHERE CAST(mbd.MobileBussinessDefinedID AS VARCHAR(50)) = t.FormControlID)
		
		--INSERT NEW CONTROLS
		INSERT INTO MobileBussinessDefined (TableName, MobilePageBlockID, ColumnDesc, ColumnDescEn, ColumnName, CorrelationValue, ControlType, MinLength, MaxLength, IsMustDo, EditOrder, CustomerID, CreateBy, CreateTime, IsDelete)
			(
				SELECT @TableName, @MobilePageBlockID, cbd.ColumnDesc, cbd.ColumnDescEn, cbd.ColumnName, cbd.CorrelationValue, cbd.ControlType, cbd.MinLength, cbd.MaxLength, t.IsMustDo, t.EditOrder, @CustomerID, @UserID, GETDATE(), 0
					FROM @FieldList t
						LEFT JOIN ClientBussinessDefined cbd ON t.PublicControlID = CAST(cbd.ClientBussinessDefinedID AS VARCHAR(50))
					WHERE NOT EXISTS (SELECT 1 FROM MobileBussinessDefined mbd WHERE cbd.ColumnName = mbd.ColumnName AND mbd.CustomerID = @CustomerID AND mbd.MobilePageBlockID = @MobilePageBlockID AND mbd.IsDelete = 0)
			)
			
		IF @Debug = 1
			BEGIN
			
				SELECT * FROM @FieldList
			
				SELECT @MobilePageBlockID
				
				--UPDATE USED CONTROLS
				SELECT *
					FROM MobileBussinessDefined mbd
						LEFT JOIN @FieldList t ON CAST(mbd.MobileBussinessDefinedID AS VARCHAR(50)) = t.FormControlID
					WHERE mbd.MobilePageBlockID = @MobilePageBlockID
			
				--DELTE NOT USED CONTROLS
				SELECT *		
					FROM MobileBussinessDefined mbd 
						WHERE mbd.TableName = @TableName AND mbd.MobilePageBlockID = @MobilePageBlockID AND mbd.CustomerID = @CustomerID AND IsDelete = 0
							AND NOT EXISTS (SELECT 1 FROM @FieldList t WHERE CAST(mbd.MobileBussinessDefinedID AS VARCHAR(50)) = t.FormControlID)
				
				--INSERT NEW CONTROLS
				SELECT @TableName, @MobilePageBlockID, cbd.ColumnDesc, cbd.ColumnDescEn, cbd.ColumnName, cbd.CorrelationValue, cbd.ControlType, cbd.MinLength, cbd.MaxLength, t.IsMustDo, t.EditOrder, @CustomerID, @UserID, GETDATE(), 0
					FROM @FieldList t
						LEFT JOIN ClientBussinessDefined cbd ON t.PublicControlID = CAST(cbd.ClientBussinessDefinedID AS VARCHAR(50))
					WHERE NOT EXISTS (SELECT 1 FROM MobileBussinessDefined mbd WHERE cbd.ColumnName = mbd.ColumnName AND mbd.CustomerID = @CustomerID AND mbd.MobilePageBlockID = @MobilePageBlockID AND mbd.IsDelete = 0)
			
			END

		SELECT 1
	
	COMMIT TRAN

END
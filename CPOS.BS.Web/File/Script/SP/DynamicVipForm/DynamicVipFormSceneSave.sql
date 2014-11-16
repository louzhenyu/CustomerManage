/****** Object:  StoredProcedure [dbo].[DynamicVipFormSceneSave]    Script Date: 07/28/2014 15:29:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DynamicVipFormSceneSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DynamicVipFormSceneSave]
GO

/****** Object:  StoredProcedure [dbo].[DynamicVipFormSceneSave]    Script Date: 07/28/2014 15:29:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-28
-- Description:	Save Dynamic Vip Form Scene Settings
-- =============================================
CREATE PROCEDURE [dbo].[DynamicVipFormSceneSave] --'F9323FFA-80F7-4262-885F-9435D8413E78', 'e703dbedadd943abacf864531decdac1'
	
	@FormID VARCHAR(50),
	@CustomerID VARCHAR(50),
	@UserID VARCHAR(50),
	@DataTable TableParameterCommon READONLY,
	@Debug int = 0
	
AS
BEGIN
	
	SET XACT_ABORT ON	--ROLLBACK ON FAILURE
	BEGIN TRAN

		--UPDATE mmom
		--	SET IsDelete = CASE t.Column3 WHEN '1' THEN 0 ELSE 1 END
		--	, LastUpdateBy = @UserID, LastUpdateTime = GETDATE()
		----SELECT *
		--	FROM MobileModuleObjectMapping mmom
		--		INNER JOIN @DataTable t ON CAST(mmom.ObjectID AS VARCHAR(50)) = t.Column1
		--	WHERE mmom.MobileModuleID = @FormID
		UPDATE mmom
			SET IsDelete = CASE WHEN  t.Column3='1' AND mmom.MobileModuleID=@FormID THEN 0
								WHEN  t.Column3<>'1' AND mmom.MobileModuleID=@FormID THEN 1
								WHEN  t.Column3='1' AND mmom.MobileModuleID<>@FormID THEN 1
								ELSE mmom.IsDelete
							END
			, LastUpdateBy = @UserID, LastUpdateTime = GETDATE()		
			FROM MobileModuleObjectMapping mmom
				INNER JOIN @DataTable t ON CAST(mmom.ObjectID AS VARCHAR(50)) = t.Column1
				WHERE mmom.CustomerID=@CustomerID	
		--SELECT @MobilePageBlockID = MobilePageBlockID FROM MobilePageBlock WHERE MobileModuleID = @FormID AND [Type] = 2
		
		INSERT INTO MobileModuleObjectMapping ([ObjectType]
			  ,[ObjectID]
			  ,[MobileModuleID]
			  ,[CustomerID]
			  ,[CreateBy]
			  ,[CreateTime]
			  ,[IsDelete])
			(
				SELECT 1, t.Column1, @FormID, @CustomerID, @UserID, GETDATE(), 0
					FROM @DataTable t
						LEFT JOIN MobileModuleObjectMapping mmom ON t.Column1 = CAST(mmom.ObjectID AS VARCHAR(50)) AND mmom.CustomerID = @CustomerID AND mmom.MobileModuleID = @FormID AND mmom.IsDelete = 0
					WHERE mmom.MappingID IS NULL AND t.Column3 = 1
			)
			
		IF @Debug = 1
		BEGIN
			SELECT * FROM @DataTable
			
			SELECT *
				FROM MobileModuleObjectMapping mmom
					INNER JOIN @DataTable t ON CAST(mmom.ObjectID AS VARCHAR(50)) = t.Column1
					WHERE mmom.MobileModuleID = @FormID
					
			SELECT 1, t.Column1, @FormID, @CustomerID, @UserID, GETDATE(), 0
					FROM @DataTable t
						LEFT JOIN MobileModuleObjectMapping mmom ON t.Column1 = CAST(mmom.ObjectID AS VARCHAR(50)) AND mmom.CustomerID = @CustomerID AND mmom.MobileModuleID = @FormID AND mmom.IsDelete = 0
					WHERE mmom.MappingID IS NULL AND t.Column3 = 1
					
		END

		SELECT 1
	
	COMMIT TRAN

END



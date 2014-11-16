/****** Object:  StoredProcedure [dbo].[DynamicVipPropertySave]    Script Date: 08/08/2014 10:20:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DynamicVipPropertySave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DynamicVipPropertySave]
GO

/****** Object:  StoredProcedure [dbo].[DynamicVipPropertySave]    Script Date: 08/08/2014 10:20:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-08-06
-- Description:	Save Property
-- =============================================
CREATE PROCEDURE [dbo].[DynamicVipPropertySave] --'3A310E4A-6EDD-4BC0-B65B-AD84C53C957F', 'e703dbedadd943abacf864531decdac1'
	
	@PropertyID VARCHAR(50) = '',
	@DisplayType VARCHAR(50),
	@PropertyName VARCHAR(50),
	@TableName VARCHAR(50),
	@CustomerID VARCHAR(50),
	@UserID VARCHAR(50),
	@Debug INT = 0,
	@TableParameterCommon TableParameterCommon READONLY
	
AS
BEGIN
	
	SET XACT_ABORT ON
	BEGIN TRAN
		--ADD OPTIONS
		DECLARE @OptionName VARCHAR(50)

		IF @Debug = 1
			SELECT * FROM @TableParameterCommon

		DECLARE @c INT = 0
		SELECT @c = COUNT(1) FROM @TableParameterCommon
		IF @c > 0
		BEGIN

			CREATE TABLE #Option ([OptionValue] INT NOT NULL IDENTITY, [OptionText] VARCHAR(50))
			DECLARE @DefinedID INT

			IF @PropertyID IS NULL OR @PropertyID = ''
				BEGIN
				
					SELECT @DefinedID = MAX(DefinedID) + 1 FROM Options WHERE IsDelete = 0
					IF @DefinedID IS NULL
						SET @DefinedID = 1

					SET @OptionName = @TableName + CAST(@DefinedID AS VARCHAR(5))

					INSERT INTO #Option ([OptionText])
						SELECT Column1 FROM @TableParameterCommon

				END
			ELSE
				BEGIN
					--DECLARE @CustomerID VARCHAR(50) = 'e703dbedadd943abacf864531decdac1', @PropertyID VARCHAR(50) = '3A310E4A-6EDD-4BC0-B65B-AD84C53C957F'
					SELECT o.OptionValue, o.OptionText, o.DefinedID, o.OptionName 
						INTO #tmp_Option
						FROM ClientBussinessDefined cbd 
							LEFT JOIN Options o ON cbd.CorrelationValue = o.OptionName 
						WHERE cbd.ClientBussinessDefinedID = @PropertyID
							AND cbd.IsDelete = 0 AND o.IsDelete = 0 AND cbd.ClientID = @CustomerID AND o.CustomerID = @CustomerID

					DECLARE @MaxOptionValue INT = 0
					SELECT @MaxOptionValue = MAX(OptionValue) + 1, @DefinedID = MAX(DefinedID), @OptionName = MAX(OptionName) FROM #tmp_Option

					DBCC CHECKIDENT (#Option, RESEED, @MaxOptionValue)

					INSERT INTO #Option ([OptionText])
						SELECT tpc.Column1
							FROM @TableParameterCommon tpc
							WHERE NOT EXISTS (SELECT 1 FROM #tmp_Option o WHERE tpc.Column1 = o.OptionText)

				END

			INSERT INTO Options ([DefinedID], [OptionName], [OptionValue], [OptionText], [CreateBy], [IsDelete], [CustomerID])
				SELECT @DefinedID, @OptionName, [OptionValue], [OptionText], @UserID, 0, @CustomerID
					FROM #Option

			IF @Debug = 1
			BEGIN
				SELECT @DefinedID AS DefinedID, @OptionName AS OptionName, [OptionValue], [OptionText], @UserID AS UserID, 0, @CustomerID AS CustomerID
					FROM #Option
			END
		END
	
		--ADD PROPERTY
		IF @PropertyID IS NULL OR @PropertyID = ''
		BEGIN
			--GET AVAILABLE COLUMNNAME
			DECLARE @TEMP TABLE(id int identity, columnname varchar(5))
			DECLARE @i INT = 1

			WHILE @i <= 50
			BEGIN
				INSERT INTO @TEMP (columnname) VALUES ('col' + CAST(@i AS VARCHAR(3)))
				SET @i = @i + 1
			END

			DECLARE @ColumnName VARCHAR(5)
			SELECT TOP 1 @ColumnName = t.columnname FROM @TEMP t WHERE NOT EXISTS (SELECT 1 FROM ClientBussinessDefined cbd WHERE t.columnname = cbd.ColumnName AND clientid=@CustomerID AND cbd.TableName = @TableName AND cbd.IsDelete = 0) ORDER BY t.id

			IF @Debug = 1
				SELECT @ColumnName AS ColumnName
	
			IF @ColumnName IS NOT NULL
                    AND @ColumnName <> ''
                    BEGIN
						IF EXISTS ( SELECT *
                                     FROM   ClientBussinessDefined cbd
                                     WHERE  cbd.TableName = @TableName
                                            AND cbd.IsDelete = 0
                                            AND clientid = @CustomerID
											AND ColumnDesc=@PropertyName)
						BEGIN
							SELECT 3;--属性名称已经存在
						END
						ELSE
						BEGIN
							INSERT  INTO [ClientBussinessDefined] ( ClientBussinessDefinedID,
																  [TableName],
																  [ColumnName],
																  [ColumnType],
																  [ControlType],
																  [ColumnDesc],
																  [CorrelationValue],
																  [ClientID],
																  [CreateBy],
																  [CreateTime],
																  [IsDelete],
																  [DisplayType],
																  [IsDefaultProp] )
							VALUES  ( REPLACE(NEWID(), '-', ''), @TableName,
									  @ColumnName, @DisplayType, @DisplayType,
									  @PropertyName, @OptionName, @CustomerID,
									  @UserID, GETDATE(), 0, @DisplayType, 0 )

							SELECT  1 --成功
						END

                    END
                ELSE
                    SELECT  2 --超过限制
		END

	COMMIT TRAN

END


GO
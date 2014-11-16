/****** Object:  StoredProcedure [dbo].[CardBulkInsert]    Script Date: 07/10/2014 10:53:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardBulkInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CardBulkInsert]
GO

/****** Object:  StoredProcedure [dbo].[CardBulkInsert]    Script Date: 07/10/2014 10:53:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-09
-- Description:	Make New Cards
-- =============================================
CREATE PROCEDURE [dbo].[CardBulkInsert]	--'BB8B86C7-D120-420A-B9CA-0609448171A8', 5000, 100, 1000, '1', '92a251898d63474f96b2145fcee2860c'
	
	@ChannelID VARCHAR(50),
	@Amount DECIMAL(18,2),
	@Bonus DECIMAL(18,2),
	@Qty INT,
	@UserID VARCHAR(50),
	@CustomerID VARCHAR(50),
	@Password TableParameter readonly
	
AS
BEGIN

	DECLARE @PreLength INT
	DECLARE @SuffixLength INT
	DECLARE @I INT = 0
	DECLARE @Code VARCHAR(64)
	DECLARE @MaxCardNo NVARCHAR(64)
	DECLARE @CardNoSeed INT
	DECLARE @BatchID VARCHAR(64) = NEWID()
	
	SELECT @PreLength = SettingValue FROM CustomerBasicSetting WHERE CustomerID = @CustomerID AND SettingCode = 'CardNoPreLength'
	SELECT @SuffixLength = SettingValue FROM CustomerBasicSetting WHERE CustomerID = @CustomerID AND SettingCode = 'CardNoSuffixLength'
	SELECT @Code = ISNULL(ChannelCode, '') FROM CardChannelInfo WHERE ChannelId = @ChannelID
	
	--GET MAX CARD NO
	SELECT TOP 1 @MaxCardNo = CardNo FROM CardDeposit WHERE CardNo LIKE @Code + '%' ORDER BY CardNo DESC
	IF @MaxCardNo IS NULL
		SET @CardNoSeed = 0
	ELSE
		SET @CardNoSeed = CAST(SUBSTRING(REPLACE(@MaxCardNo, @Code, ''), 1, @PreLength) AS INT)
	
	DECLARE @Multiple INT
	SET @Multiple = CAST(LEFT('1' + '0000000000', @SuffixLength + 1) AS INT)

	--SELECT @Multiple

	--TO GET TABLE STRUCTURE
	DECLARE @TMP TABLE
	(
		[CardNo] [nvarchar](64) NOT NULL,
		[CardPassword] VARBINARY(100) NOT NULL
	)
	
	--SELECT @CardNoSeed
	--SELECT @PreLength, @Multiple
	
	WHILE @I < @Qty
	BEGIN
		SET @CardNoSeed = @CardNoSeed + 1
		SET @MaxCardNo = @Code + RIGHT('0000000000000000000' + CAST(@CardNoSeed AS VARCHAR(10)), @PreLength) + RIGHT('0000000000' + CAST(FLOOR(RAND() * @Multiple) AS VARCHAR(10)), @SuffixLength)
		INSERT INTO @TMP SELECT @MaxCardNo, Column1 FROM @Password WHERE ID = @I + 1
		SET @I = @I + 1
	END
	
	--SELECT * FROM @TMP
	
	INSERT INTO CardDeposit (CardNo, CardPassword, CustomerId, BatchId, ChannelId, Amount, Bonus, CardStatus, UseStatus, IsDelete, CreateBy, CreateTime) 
		SELECT CardNo, CardPassword, @CustomerID, @BatchID, @ChannelID, @Amount, @Bonus, 0, 0, 0, @UserID, GETDATE() FROM @TMP
	
	SELECT @BatchID

END
GO
/****** Object:  StoredProcedure [dbo].[CardDepositPagedSearch]    Script Date: 07/10/2014 13:55:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDepositPagedSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CardDepositPagedSearch]
GO

/****** Object:  StoredProcedure [dbo].[CardDepositPagedSearch]    Script Date: 07/10/2014 13:55:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-10
-- Description:	Search cards by page
-- =============================================
CREATE PROCEDURE [dbo].[CardDepositPagedSearch]	--'BB8B86C7-D120-420A-B9CA-0609448171A8', 5000, 100, 1000, '1', '92a251898d63474f96b2145fcee2860c'
	
	@ChannelID VARCHAR(50) = NULL,
	@CardNoStart INT = NULL,
	@CardNoEnd INT = NULL,
	@CardStatus INT = NULL,
	@UseStatus INT = NULL,
	@Amount DECIMAL = NULL,
	@DateRange INT = NULL,
	@CreateTimeStart DATETIME = NULL,
	@CreateTimeEnd DATETIME = NULL,
	@CardNo VARCHAR(50) = NULL,
	@PageIndex INT = 0,
	@PageSize INT = 0,
	@CustomerID VARCHAR(50),
	@Debug INT = 0
	
AS
BEGIN

	DECLARE @PreLength INT = 0
	DECLARE @SuffixLength INT = 0
	DECLARE @Code VARCHAR(64) = NULL
	DECLARE @PageNo INT

	IF @ChannelID IS NOT NULL AND LEN(@ChannelID) > 0
		SELECT @Code = ISNULL(ChannelCode, '') FROM CardChannelInfo WHERE ChannelId = @ChannelID
	
	IF @CardNoStart > 0 OR @CardNoEnd > 0
	BEGIN
	
		SELECT @PreLength = SettingValue FROM CustomerBasicSetting WHERE CustomerID = @CustomerID AND SettingCode = 'CardNoPreLength'
		SELECT @SuffixLength = SettingValue FROM CustomerBasicSetting WHERE CustomerID = @CustomerID AND SettingCode = 'CardNoSuffixLength'
		
	END
	
	--DateRange	1：今天；2：昨天；3：本周；4：本月；5：自定义
	IF @DateRange = 1
		BEGIN
			SET @CreateTimeStart = GETDATE()
			SET @CreateTimeEnd = DATEADD(DAY, 1, GETDATE())
		END
	ELSE IF @DateRange = 2
		BEGIN
			SET @CreateTimeStart = DATEADD(DAY, -1, GETDATE())
			SET @CreateTimeEnd = GETDATE()
		END
	ELSE IF @DateRange = 3
		BEGIN
			SET @CreateTimeStart = DATEADD(WK,  DATEDIFF(WK, 0, GETDATE()),  0) 
			SET @CreateTimeEnd = DATEADD(WK,  DATEDIFF(WK, 0, GETDATE()),  7) 
		END
	ELSE IF @DateRange = 4
		BEGIN
			--DECLARE @CreateTimeStart DATETIME, @CreateTimeEND DATETIME
			SET @CreateTimeStart = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()),  0)
			SET @CreateTimeEnd = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1,  0)
			--SELECT @CreateTimeStart, @CreateTimeEnd
		END
	ELSE IF @DateRange = 5 AND @CreateTimeEnd IS NOT NULL
		BEGIN
			SET @CreateTimeEnd = DATEADD(DAY, 1, @CreateTimeEnd)
		END
		
	--SELECT @ChannelID, @CardNoStart, @CardNoEnd, @CardStatus, @UseStatus, @Amount, @CreateTimeStart, @CreateTimeEnd, @CardNo, @CustomerID, @Code

	SELECT CardChannelInfo.ChannelTitle, CardDeposit.*
		INTO #TEMP 
		FROM CardDeposit
			LEFT JOIN CardChannelInfo ON CardDeposit.ChannelId = CardChannelInfo.ChannelId
		WHERE (ISNULL(@ChannelID, '') = '' OR CardDeposit.ChannelId = @ChannelID)
			AND (ISNULL(@CardNoStart, 0) = 0 OR @CardNoStart <= CAST(SUBSTRING(CardNo, LEN(@Code) + 1, @PreLength) AS INT))
			AND (ISNULL(@CardNoEnd, 0) = 0 OR @CardNoEnd >= CAST(SUBSTRING(CardNo, LEN(@Code) + 1, @PreLength) AS INT))
			AND (@CardStatus IS NULL OR CardStatus = @CardStatus)
			AND (@UseStatus IS NULL OR UseStatus = @UseStatus)
			AND (ISNULL(@Amount, 0) = 0 OR CardDeposit.Amount = @Amount)
			AND (ISNULL(@CreateTimeStart, '') = '' OR DATEDIFF(DAY, @CreateTimeStart, CardDeposit.CreateTime) >= 0)
			AND (ISNULL(@CreateTimeEnd, '') = '' OR DATEDIFF(DAY, CardDeposit.CreateTime, @CreateTimeEnd ) > 0)
			AND (ISNULL(@CardNo, '') = '' OR CardDeposit.CardNo = @CardNo)
			AND CardDeposit.CustomerId = @CustomerID
	
	--SELECT * FROM #TEMP

	SET @PageNo = @PageIndex + 1
	IF @PageSize = 0
	BEGIN
		SELECT @PageSize = COUNT(1) FROM #TEMP
		IF @PageSize = 0
			SET @PageSize = 10
	END

	exec CommonPagination '#temp', '*', @PageSize, @PageNo, 'CardNo'
	
END

GO
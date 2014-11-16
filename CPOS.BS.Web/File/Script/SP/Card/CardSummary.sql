/****** Object:  StoredProcedure [dbo].[CardSummary]    Script Date: 07/14/2014 22:28:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CardSummary]
GO

/****** Object:  StoredProcedure [dbo].[CardSummary]    Script Date: 07/14/2014 22:28:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-14
-- Description:	Card Summary
-- =============================================
CREATE PROCEDURE [dbo].[CardSummary]	--0, 0, '92a251898d63474f96b2145fcee2860c'
	
	@PageIndex INT = 0,
	@PageSize INT = 0,
	@CustomerID VARCHAR(50)

AS
BEGIN

	SELECT ISNULL(a.ChannelTitle, b.ChannelTitle) AS ChannelTitle, CAST(ISNULL(TotalCount, 0) AS VARCHAR(50)) + '(' + CAST(ISNULL(TotalAmount, 0) AS VARCHAR(50)) + ')' AS Amount, CAST(ISNULL(UsedTotalCount, 0) AS VARCHAR(50)) + '(' + CAST(ISNULL(UsedTotalAmount, 0) AS VARCHAR(50)) + ')' AS ActivatedAmount 
		INTO #TEMP
		FROM
		(
			SELECT cci.ChannelTitle, COUNT(1) AS TotalCount, SUM(ISNULL(Amount, 0) + ISNULL(Bonus, 0)) AS TotalAmount
				FROM CardDeposit cp
					LEFT JOIN CardChannelInfo cci ON cp.ChannelId = cci.ChannelId
				WHERE cp.CustomerId = @CustomerID AND (cp.CardStatus = 0 OR cp.CardStatus = 1)
				GROUP BY cci.ChannelTitle
		) a
		LEFT JOIN
		(	
			SELECT cci.ChannelTitle, COUNT(1) AS UsedTotalCount, SUM(ISNULL(Amount, 0) + ISNULL(Bonus, 0)) AS UsedTotalAmount
				FROM CardDeposit cp
					LEFT JOIN CardChannelInfo cci ON cp.ChannelId = cci.ChannelId
				WHERE cp.UseStatus > 0 AND cp.CustomerId = @CustomerID
				GROUP BY cci.ChannelTitle, cp.UseStatus
		) b	ON a.ChannelTitle = b.ChannelTitle

	DECLARE @PageNo INT		
	SET @PageNo = @PageIndex + 1
	IF @PageSize = 0
		SELECT @PageSize = COUNT(1) FROM #TEMP

	EXEC CommonPagination '#temp', '*', @PageSize, @PageNo, 'ChannelTitle'
END
GO



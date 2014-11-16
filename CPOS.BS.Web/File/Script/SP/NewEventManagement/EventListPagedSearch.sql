/****** Object:  StoredProcedure [dbo].[EventListPagedSearch]    Script Date: 07/24/2014 13:56:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventListPagedSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EventListPagedSearch]
GO

/****** Object:  StoredProcedure [dbo].[EventListPagedSearch]    Script Date: 07/24/2014 13:56:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-08-19
-- Description:	List events
-- =============================================
CREATE PROCEDURE [dbo].[EventListPagedSearch] --@CustomerID = 'e703dbedadd943abacf864531decdac1'
	
	@BeginTime VARCHAR(50) = NULL,
	@EndTime VARCHAR(50) = NULL,
	@EventTypeID VARCHAR(50) = NULL,
	@Organizer VARCHAR(50) = NULL,
	@EventStatus VARCHAR(50) = NULL,
	@Title VARCHAR(50) = NULL,
	@CustomerID VARCHAR(50),
	@PageSize INT = 15,
	@PageIndex INT = 0
	
AS
BEGIN
	
	SELECT * INTO #EventStatus FROM Options WHERE OptionName = 'EventStatus' AND IsDelete = 0 AND ([CustomerID] IS NULL OR [CustomerID] = '' OR [CustomerID] = @CustomerID)
	SELECT * INTO #Sponsor FROM Options WHERE OptionName = 'EventSponsor' AND IsDelete = 0 AND ([CustomerID] IS NULL OR [CustomerID] = '' OR [CustomerID] = @CustomerID)
	
	SELECT le.*, CONVERT(VARCHAR(10), BeginTime, 120) AS EventBeginTime, CONVERT(VARCHAR(10), EndTime, 120) AS EventEndTime, e.OptionText AS EventStatusText, s.OptionText AS Sponsor 
	INTO #TEMP
	FROM LEvents le
		LEFT JOIN #EventStatus e ON le.EventStatus = e.OptionValue
		LEFT JOIN #Sponsor s ON le.Organizer = s.OptionValue
	WHERE (@BeginTime IS NULL OR @BeginTime = '' OR CONVERT(VARCHAR(10), le.BeginTime, 120) >= CONVERT(VARCHAR(10), @BeginTime, 120))
		AND (@EndTime IS NULL OR @EndTime = '' OR CONVERT(VARCHAR(10), le.EndTime, 120) <= CONVERT(VARCHAR(10), @EndTime, 120))
		AND (@EventTypeID IS NULL OR @EventTypeID = '' OR le.EventTypeID = @EventTypeID)
		AND (@Organizer IS NULL OR @Organizer = '' OR le.Organizer = @Organizer)
		AND (@EventStatus IS NULL OR @EventStatus = '' OR le.EventStatus = @EventStatus)
		AND (@Title IS NULL OR @Title = '' OR CHARINDEX(@Title, le.Title) > 0)
		AND le.CustomerID = @CustomerID
		AND le.IsDelete = 0

	DECLARE @PageNo INT
	SET @PageNo = @PageIndex + 1
	IF @PageSize = 0
	BEGIN
		SELECT @PageSize = COUNT(1) FROM #TEMP
		IF @PageSize = 0
			SET @PageSize = 10
	END

	exec CommonPagination '#temp', '*', @PageSize, @PageNo, 'CreateTime desc'

END

GO
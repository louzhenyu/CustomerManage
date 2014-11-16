/****** Object:  StoredProcedure [dbo].[GetEventVipJoinVip]    Script Date: 02/25/2014 17:13:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventVipJoinVip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventVipJoinVip]
GO

/****** Object:  StoredProcedure [dbo].[GetEventVipJoinVip]    Script Date: 02/25/2014 17:13:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Willie Yan>
-- Create date: <2014-01-09>
-- Description:	<Get EventVip join Vip>
-- =============================================

CREATE PROCEDURE [dbo].[GetEventVipJoinVip] --@EventId = '783467B7A9F1425CA3DFC0B03A36B713'
	
	@VipName NVARCHAR(200) = NULL,
	@Phone NVARCHAR(200) = NULL,
	@IsRegistered VARCHAR(10) = NULL,
	@EventId VARCHAR(200),
	@IsSigned VARCHAR(10) = NULL
	, @PageSize INT = 15
	, @PageNo INT = 1
	, @Debug INT = 0

AS
BEGIN

	IF @VipName = ''
		SET @VipName = NULL

	IF @Phone = ''
		SET @Phone = NULL
		
	IF @IsRegistered = ''
		SET @IsRegistered = NULL
		
	IF @IsSigned = ''
		SET @IsSigned = NULL
		
	SELECT UserID, EventID, MIN(CreateTime) AS SignTime
		INTO #temp_ValidWEventUserMapping
		FROM WEventUserMapping WHERE EventID = @EventId GROUP BY UserID, EventID
	
	SELECT * 
		INTO #temp
		FROM
		(	
			SELECT fs.*, v.VIPID AS VipVipID
				, CASE WHEN v.VIPID IS NOT NULL THEN 1 ELSE 0 END AS IsRegistered
				, CASE WHEN eum.SignTime IS NOT NULL THEN 1 ELSE 0 END AS IsSigned	--AND v.[Status] = 2 
				FROM EventVip fs 
					LEFT JOIN Vip v ON fs.Phone = v.Phone AND v.IsDelete = 0
					LEFT JOIN #temp_ValidWEventUserMapping eum ON eum.UserID = v.VIPID AND eum.EventID = @EventId
				WHERE (@VipName IS NULL OR fs.VipName like '%' + @VipName + '%')
					AND (@Phone IS NULL OR fs.Phone like '%' + @Phone + '%')
					--AND eum.EventID = @EventId
					AND fs.IsDelete = 0
		) A
		WHERE (A.IsRegistered = @IsRegistered OR @IsRegistered IS NULL)
			AND (A.IsSigned = @IsSigned OR @IsSigned IS NULL)
		
	--select * from WEventUserMapping where userid = '9258b78ab16a4437992a4373b4ddef57'
	
	IF @Debug = 1
		BEGIN
			SELECT * FROM #temp
		END
	
	exec CommonPagination '#temp', '*', @PageSize, @PageNo, 'VipName', @Debug = @Debug
	
		
END



GO



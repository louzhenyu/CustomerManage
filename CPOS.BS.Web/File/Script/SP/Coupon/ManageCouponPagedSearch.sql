/****** Object:  StoredProcedure [dbo].[ManageCouponPagedSearch]    Script Date: 07/10/2014 10:53:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ManageCouponPagedSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ManageCouponPagedSearch]
GO

/****** Object:  StoredProcedure [dbo].[ManageCouponPagedSearch]    Script Date: 07/10/2014 10:53:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-09-22
-- Description:	Manage coupon
-- Update date: 2014-10-14
-- Update by:   Mali
-- =============================================
CREATE PROCEDURE [dbo].[ManageCouponPagedSearch]	--'BB8B86C7-D120-420A-B9CA-0609448171A8', 5000, 100, 1000, '1', '92a251898d63474f96b2145fcee2860c'
	
	@CouponTypeID VARCHAR(50) = NULL,
	@CouponName NVARCHAR(50) = NULL,
	@BeginTime DATETIME = NULL,
	@EndTime DATETIME = NULL,
	@CouponUseStatus VARCHAR(5) = NULL,
	@CouponStatus VARCHAR(5) = NULL,
	@CouponCode NVARCHAR(50) = NULL,
	@CustomerID VARCHAR(50),
	@Comment NVARCHAR(500)=NULL,
	@UseTime DATETIME =NULL,
	@UseEndTime DATETIME =NULL,
	@CreateByName NVARCHAR(150)=NULL,
	@PageSize INT = 10,
	@PageIndex INT = 0,
	@Debug INT = 0
	
AS
BEGIN

	SELECT c.*, c.CoupnName AS CouponName, ct.CouponTypeName,c.IsDelete as IsDel, CASE WHEN vcm.VipID IS NULL THEN 0 ELSE CASE WHEN c.Status = 1 THEN 2 ELSE 1 END END AS CouponUseStatus,v.user_name as CreateByName,c.Col1 as Comment,c.Col2 as UseTime  
		INTO #CouponTemp
		FROM Coupon c
			LEFT JOIN CouponType ct ON c.CouponTypeID = ct.CouponTypeID
			LEFT JOIN VipCouponMapping vcm ON c.CouponID = vcm.CouponID
			--LEFT JOIN CouponUse u on  c.CouponID=u.CouponID
			LEFT JOIN t_user v on convert(nvarchar(50),c.Col3)=v.user_id
		WHERE c.CustomerID = @CustomerID
		
	SELECT * INTO #TEMP FROM #CouponTemp
		WHERE ( @CouponUseStatus IS NULL OR @CouponUseStatus = '' OR CouponUseStatus = @CouponUseStatus )
			and (@CouponTypeID IS NULL OR @CouponTypeID = '' OR CouponTypeID = @CouponTypeID)   
			and (@CouponName IS NULL OR @CouponName = '' OR CouponName like '%'+ @CouponName+'%')
			and (@BeginTime IS NULL OR @BeginTime = ''  OR BeginDate>=@BeginTime  )
			and (@EndTime IS NULL OR @EndTime = '' OR EndDate<=DateAdd(day, 1, @EndTime))
			and (@CouponCode IS NULL OR @CouponCode = '' OR CouponCode = @CouponCode)
			and (@CouponStatus IS NULL OR @CouponStatus = '' OR IsDel = @CouponStatus)
			and (@Comment IS NULL OR @Comment = '' OR Comment=@Comment)
			and (@UseTime IS NULL OR @UseTime = ''  OR UseTime>=@UseTime)
			and (@UseEndTime IS NULL OR @UseEndTime = ''  OR UseTime<=DateAdd(day, 1, @UseEndTime) )
			and (@CreateByName IS NULL OR @CreateByName = '' OR CreateByName like '%'+@CreateByName+'%')
	
	IF @Debug = 1
		SELECT * FROM #TEMP

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
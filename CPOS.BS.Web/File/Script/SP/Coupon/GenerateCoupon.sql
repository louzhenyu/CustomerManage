/****** Object:  StoredProcedure [dbo].[GenerateCoupon]    Script Date: 07/10/2014 10:53:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenerateCoupon]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GenerateCoupon]
GO

/****** Object:  StoredProcedure [dbo].[GenerateCoupon]    Script Date: 07/10/2014 10:53:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-07-09
-- Description:	Generate coupons
-- =============================================
CREATE PROCEDURE [dbo].[GenerateCoupon]	--'BB8B86C7-D120-420A-B9CA-0609448171A8', 5000, 100, 1000, '1', '92a251898d63474f96b2145fcee2860c'
	
	@CouponTypeID VARCHAR(50),
	@CouponName NVARCHAR(50),
	@BeginTime DATETIME = NULL,
	@EndTime DATETIME = NULL,
	@Description NVARCHAR(200) = NULL,
	@Qty INT,
	@UserID VARCHAR(50),
	@CustomerID VARCHAR(50)
	
AS
BEGIN

	DECLARE @TMP TABLE
	(
		SEED INT
	)

	DECLARE @I INT = 0

	WHILE @I < @Qty
	BEGIN

		INSERT INTO @TMP VALUES (@I)
		SET @I = @I + 1

	END

	INSERT INTO Coupon ([CouponDesc]
      ,[BeginDate]
      ,[EndDate]
      ,[Status]
      ,[CreateTime]
      ,[CreateBy]
      ,[IsDelete]
      ,[CouponTypeID]
      ,[CoupnName]
	  , CustomerID)
		SELECT @Description, @BeginTime, @EndTime, 0, GETDATE(), @UserID, 0, @CouponTypeID, @CouponName, @CustomerID FROM @TMP

	SELECT 1

END
GO
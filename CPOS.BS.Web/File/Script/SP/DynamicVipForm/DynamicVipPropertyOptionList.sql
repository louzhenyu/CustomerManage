/****** Object:  StoredProcedure [dbo].[DynamicVipPropertyOptionList]    Script Date: 08/08/2014 10:20:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DynamicVipPropertyOptionList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DynamicVipPropertyOptionList]
GO

/****** Object:  StoredProcedure [dbo].[DynamicVipPropertyOptionList]    Script Date: 08/08/2014 10:20:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Willie Yan
-- Create date: 2014-08-06
-- Description:	Load Property Option
-- =============================================
CREATE PROCEDURE [dbo].[DynamicVipPropertyOptionList] --'3A310E4A-6EDD-4BC0-B65B-AD84C53C957F', 'e703dbedadd943abacf864531decdac1'
	
	@PropertyID VARCHAR(50),
	@CustomerID VARCHAR(50)
	
AS
BEGIN
		
	SELECT o.OptionText FROM ClientBussinessDefined cbd LEFT JOIN Options o ON cbd.CorrelationValue = o.OptionName
		WHERE cbd.ClientBussinessDefinedID = @PropertyID AND cbd.IsDelete = 0 AND o.IsDelete = 0
			AND (cbd.ClientID IS NULL OR cbd.ClientID = @CustomerID) AND (o.ClientID IS NULL OR o.ClientID = @CustomerID)

END


GO
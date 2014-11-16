/****** Object:  StoredProcedure [dbo].[CheckVipEventQRCode]    Script Date: 02/21/2014 15:45:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckVipEventQRCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckVipEventQRCode]
GO


/****** Object:  StoredProcedure [dbo].[CheckVipEventQRCode]    Script Date: 02/21/2014 15:45:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Willie Yan>
-- Create date: <2014-02-20>
-- Description:	<Check whether the vip scanned the event QRCode>
-- =============================================
CREATE PROCEDURE [dbo].[CheckVipEventQRCode]
	
	@VipId VARCHAR(50),
	@EventId VARCHAR(50)
	
AS
BEGIN

	SELECT 1 
		FROM QRCodeScanLog qrl
			LEFT JOIN WQRCodeManager qrm ON CAST(qrm.QRCodeId AS VARCHAR(50)) = qrl.QRCodeID
		WHERE qrm.ObjectId = @EventId AND qrl.VipID = @VipId

END

GO



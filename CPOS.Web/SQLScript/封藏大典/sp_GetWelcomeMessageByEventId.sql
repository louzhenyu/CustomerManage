/****** Object:  StoredProcedure [dbo].[GetWelcomeMessageByEventId]    Script Date: 02/21/2014 13:50:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWelcomeMessageByEventId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetWelcomeMessageByEventId]
GO


/****** Object:  StoredProcedure [dbo].[GetWelcomeMessageByEventId]    Script Date: 02/21/2014 13:50:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Willie Yan>
-- Create date: <2014-02-21>
-- Description:	<GetWelcomeMessageByEventId>
-- =============================================
CREATE PROCEDURE [dbo].[GetWelcomeMessageByEventId]

	@EventId VARCHAR(50)

AS
BEGIN

	SELECT * 
		FROM [WMaterialWriting] mw
			LEFT JOIN [WModelWritingMapping] mwm ON mw.WritingId = mwm.WritingId
			LEFT JOIN [LEvents] e ON mwm.ModelId = e.ModelId
		WHERE e.EventID = @EventId

END

GO



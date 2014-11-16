IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__EventVip__EventV__6CB8F890]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventVip] DROP CONSTRAINT [DF__EventVip__EventV__6CB8F890]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventVip_CanLottery]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventVip] DROP CONSTRAINT [DF_EventVip_CanLottery]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__EventVip__IsChec__6DAD1CC9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventVip] DROP CONSTRAINT [DF__EventVip__IsChec__6DAD1CC9]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__EventVip__Create__6EA14102]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventVip] DROP CONSTRAINT [DF__EventVip__Create__6EA14102]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__EventVip__LastUp__6F95653B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventVip] DROP CONSTRAINT [DF__EventVip__LastUp__6F95653B]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__EventVip__IsDele__70898974]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventVip] DROP CONSTRAINT [DF__EventVip__IsDele__70898974]
END

GO


/****** Object:  Table [dbo].[EventVip]    Script Date: 02/25/2014 18:20:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVip]') AND type in (N'U'))
DROP TABLE [dbo].[EventVip]
GO


/****** Object:  Table [dbo].[EventVip]    Script Date: 02/25/2014 18:20:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventVip](
	[EventVipID] [uniqueidentifier] NOT NULL,
	[VipId] [nvarchar](50) NULL,
	[VipName] [nvarchar](50) NULL,
	[VipCompany] [nvarchar](100) NULL,
	[VipPost] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Seat] [nvarchar](50) NULL,
	[Profile] [nvarchar](max) NULL,
	[HeadImage] [nvarchar](100) NULL,
	[CustomerId] [nvarchar](50) NULL,
	[DCodeImageUrl] [nvarchar](100) NULL,
	[CanLottery] [int] NOT NULL,
	[EventId] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateBy] [nvarchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
	[LastUpdateBy] [nvarchar](50) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK__EventVip__600444396AD0B01E] PRIMARY KEY CLUSTERED 
(
	[EventVipID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventVip] ADD  CONSTRAINT [DF__EventVip__EventV__6CB8F890]  DEFAULT (newid()) FOR [EventVipID]
GO

ALTER TABLE [dbo].[EventVip] ADD  CONSTRAINT [DF_EventVip_CanLottery]  DEFAULT ((1)) FOR [CanLottery]
GO

ALTER TABLE [dbo].[EventVip] ADD  CONSTRAINT [DF__EventVip__Create__6EA14102]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[EventVip] ADD  CONSTRAINT [DF__EventVip__LastUp__6F95653B]  DEFAULT (getdate()) FOR [LastUpdateTime]
GO

ALTER TABLE [dbo].[EventVip] ADD  CONSTRAINT [DF__EventVip__IsDele__70898974]  DEFAULT ((0)) FOR [IsDelete]
GO
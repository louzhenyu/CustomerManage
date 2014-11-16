USE [cpos_bs_hotels]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CardChannelInfo_ChannelId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CardChannelInfo] DROP CONSTRAINT [DF_CardChannelInfo_ChannelId]
END

GO

USE [cpos_bs_hotels]
GO

/****** Object:  Table [dbo].[CardChannelInfo]    Script Date: 07/21/2014 22:38:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardChannelInfo]') AND type in (N'U'))
DROP TABLE [dbo].[CardChannelInfo]
GO

USE [cpos_bs_hotels]
GO

/****** Object:  Table [dbo].[CardChannelInfo]    Script Date: 07/21/2014 22:38:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CardChannelInfo](
	[ChannelId] [uniqueidentifier] NOT NULL,
	[ChannelCode] [nvarchar](64) NULL,
	[ChannelTitle] [nvarchar](128) NULL,
	[CustomerId] [varchar](64) NULL,
	[DisplayIndex] [int] NULL,
	[IsDelete] [int] NULL,
	[CreateBy] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[LastUpdateBy] [nvarchar](64) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_CChannleInfo] PRIMARY KEY CLUSTERED 
(
	[ChannelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CardChannelInfo] ADD  CONSTRAINT [DF_CardChannelInfo_ChannelId]  DEFAULT (newid()) FOR [ChannelId]
GO



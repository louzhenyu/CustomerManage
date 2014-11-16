IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ProductTraceLog_LogId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProductTraceLog] DROP CONSTRAINT [DF_ProductTraceLog_LogId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ProductTraceLog_IsValid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProductTraceLog] DROP CONSTRAINT [DF_ProductTraceLog_IsValid]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ProductTraceLog_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProductTraceLog] DROP CONSTRAINT [DF_ProductTraceLog_CreateTime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ProductTraceLog_LastUpdateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProductTraceLog] DROP CONSTRAINT [DF_ProductTraceLog_LastUpdateTime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ProductTraceLog_IsDelete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProductTraceLog] DROP CONSTRAINT [DF_ProductTraceLog_IsDelete]
END

GO


/****** Object:  Table [dbo].[ProductTraceLog]    Script Date: 02/27/2014 15:31:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductTraceLog]') AND type in (N'U'))
DROP TABLE [dbo].[ProductTraceLog]
GO

/****** Object:  Table [dbo].[ProductTraceLog]    Script Date: 02/27/2014 15:31:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductTraceLog](
	[LogId] [uniqueidentifier] NOT NULL,
	[TraceCode] [varchar](50) NOT NULL,
	[VipId] [varchar](50) NOT NULL,
	[IsValid] [int] NULL,
	[RequestIP] [varchar](50) NULL,
	[RequestDevice] [varchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NOT NULL,
	[LastUpdateBy] [varchar](50) NULL,
	[IsDelete] [int] NOT NULL,
 CONSTRAINT [PK_ProductTraceLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductTraceLog] ADD  CONSTRAINT [DF_ProductTraceLog_LogId]  DEFAULT (newid()) FOR [LogId]
GO

ALTER TABLE [dbo].[ProductTraceLog] ADD  CONSTRAINT [DF_ProductTraceLog_IsValid]  DEFAULT ((0)) FOR [IsValid]
GO

ALTER TABLE [dbo].[ProductTraceLog] ADD  CONSTRAINT [DF_ProductTraceLog_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[ProductTraceLog] ADD  CONSTRAINT [DF_ProductTraceLog_LastUpdateTime]  DEFAULT (getdate()) FOR [LastUpdateTime]
GO

ALTER TABLE [dbo].[ProductTraceLog] ADD  CONSTRAINT [DF_ProductTraceLog_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO



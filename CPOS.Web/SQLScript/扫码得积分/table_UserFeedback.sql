/****** Object:  Table [dbo].[UserFeedback]    Script Date: 02/27/2014 12:56:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserFeedback]') AND type in (N'U'))
DROP TABLE [dbo].[UserFeedback]
GO


/****** Object:  Table [dbo].[UserFeedback]    Script Date: 02/27/2014 12:56:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserFeedback](
	[FeedbackID] [uniqueidentifier] NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Description] [nvarchar](2000) NULL,
	[CustomerId] [varchar](50) NOT NULL,
	[Field1] [nvarchar](500) NULL,
	[Field2] [nvarchar](500) NULL,
	[Field3] [nvarchar](500) NULL,
	[Field4] [nvarchar](500) NULL,
	[Field5] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [varchar](50) NOT NULL,
	[LastUpdateBy] [varchar](50) NULL,
	[LastUpdateTime] [datetime] NULL,
	[IsDelete] [int] NOT NULL,
 CONSTRAINT [PK_UserFeedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[UserFeedback] ADD  CONSTRAINT [DF_UserFeedback_FeedbackID]  DEFAULT (newid()) FOR [FeedbackID]
GO

ALTER TABLE [dbo].[UserFeedback] ADD  CONSTRAINT [DF__UserFeedb__Creat__07E124C1]  DEFAULT (getdate()) FOR [CreateTime]
GO

ALTER TABLE [dbo].[UserFeedback] ADD  CONSTRAINT [DF__UserFeedb__LastU__08D548FA]  DEFAULT (getdate()) FOR [LastUpdateTime]
GO

ALTER TABLE [dbo].[UserFeedback] ADD  CONSTRAINT [DF__UserFeedb__IsDel__09C96D33]  DEFAULT ((0)) FOR [IsDelete]
GO



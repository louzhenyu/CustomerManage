USE [cpos_bs_hotels]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CardDeposit_CardDepositId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CardDeposit] DROP CONSTRAINT [DF_CardDeposit_CardDepositId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CardDeposit_IsDelete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CardDeposit] DROP CONSTRAINT [DF_CardDeposit_IsDelete]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CardDeposit_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CardDeposit] DROP CONSTRAINT [DF_CardDeposit_CreateTime]
END

GO

USE [cpos_bs_hotels]
GO

/****** Object:  Table [dbo].[CardDeposit]    Script Date: 07/21/2014 22:56:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDeposit]') AND type in (N'U'))
DROP TABLE [dbo].[CardDeposit]
GO

USE [cpos_bs_hotels]
GO

/****** Object:  Table [dbo].[CardDeposit]    Script Date: 07/21/2014 22:56:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CardDeposit](
	[CardDepositId] [uniqueidentifier] NOT NULL,
	[CardNo] [nvarchar](64) NOT NULL,
	[CardPassword] [varbinary](100) NOT NULL,
	[SerialNo] [nvarchar](64) NULL,
	[VerifyCode] [nvarchar](64) NULL,
	[CustomerId] [varchar](64) NULL,
	[BatchId] [varchar](50) NULL,
	[UnitId] [varchar](64) NULL,
	[ChannelId] [varchar](64) NULL,
	[DepositTime] [datetime] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Bonus] [decimal](18, 2) NULL,
	[ConsumedAmount] [decimal](18, 2) NULL,
	[VipId] [varchar](64) NULL,
	[CouponQty] [int] NULL,
	[CardStatus] [int] NULL,
	[UseStatus] [int] NULL,
	[IsDelete] [int] NULL,
	[CreateBy] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastUpdateBy] [nvarchar](64) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_CDepositCard] PRIMARY KEY CLUSTERED 
(
	[CardDepositId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardDeposit', @level2type=N'COLUMN',@level2name=N'UnitId'
GO

ALTER TABLE [dbo].[CardDeposit] ADD  CONSTRAINT [DF_CardDeposit_CardDepositId]  DEFAULT (newid()) FOR [CardDepositId]
GO

ALTER TABLE [dbo].[CardDeposit] ADD  CONSTRAINT [DF_CardDeposit_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO

ALTER TABLE [dbo].[CardDeposit] ADD  CONSTRAINT [DF_CardDeposit_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO



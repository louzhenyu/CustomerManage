/****** Object:  UserDefinedTableType [dbo].[TableParameterCommon]    Script Date: 07/28/2014 15:41:11 ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TableParameterCommon' AND ss.name = N'dbo')
DROP TYPE [dbo].[TableParameterCommon]
GO

/****** Object:  UserDefinedTableType [dbo].[TableParameterCommon]    Script Date: 07/28/2014 15:41:11 ******/
CREATE TYPE [dbo].[TableParameterCommon] AS TABLE(
	
	[Column1] [NVARCHAR](max) NULL,
	[Column2] [NVARCHAR](max) NULL,
	[Column3] [NVARCHAR](max) NULL,
	[Column4] [NVARCHAR](max) NULL,
	[Column5] [NVARCHAR](max) NULL
	--,	[Column6] [varbinary](100) NULL
)
GO
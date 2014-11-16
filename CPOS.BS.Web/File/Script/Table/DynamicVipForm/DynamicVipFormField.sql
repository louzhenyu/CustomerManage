/****** Object:  UserDefinedTableType [dbo].[DynamicVipFormField]    Script Date: 07/25/2014 11:37:08 ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'DynamicVipFormField' AND ss.name = N'dbo')
DROP TYPE [dbo].[DynamicVipFormField]
GO

/****** Object:  UserDefinedTableType [dbo].[DynamicVipFormField]    Script Date: 07/25/2014 11:37:08 ******/
CREATE TYPE [dbo].[DynamicVipFormField] AS TABLE(
	PublicControlID varchar(50) NULL,
	FormControlID varchar(50) NULL,
	ColumnDesc nvarchar(100) NULL,
	ControlType int null,
	DisplayType int null,
	IsMustDo int null,
	EditOrder int null,
	IsUsed int null,
	Hierarchy varchar(50) null
)
GO
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'TableParameter' AND ss.name = N'dbo')
	DROP TYPE [dbo].[TableParameter]
GO

-- Create the data type
CREATE TYPE [dbo].[TableParameter] AS TABLE 
(
	ID INT NOT NULL,
	[Column1] VARBINARY(100) NULL
)
GO

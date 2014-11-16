/****** Object:  StoredProcedure [dbo].[CommonPagination]    Script Date: 02/25/2014 17:13:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonPagination]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommonPagination]
GO


/****** Object:  StoredProcedure [dbo].[CommonPagination]    Script Date: 02/25/2014 17:13:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[CommonPagination] --'information i left join [user] u on i.LastUpdateBy = u.ID', 'i.*, u.LoginName', 2, 1, 'i.informationtype=1', 'sortorder', 1, 1

	@TableName	 nvarchar(3000),	 -- ����
	@ReturnFields	nvarchar(3000) = '*',	-- ��Ҫ���ص��� 
	@PageSize	 int = 10,	 -- ÿҳ��¼��
	@PageNo	 int = 1,	 -- ��ǰҳ��
	@OrderField	 nvarchar(200),	 -- �����ֶ��� ���ΪΨһ����
	@Where	 nvarchar(3000) = '',	 -- ��ѯ����, ��Ҫ��where
	--@OrderType	 int = 1,	 -- �������� 1:���� ����Ϊ����
	@Debug INT = 0

AS
BEGIN

	DECLARE @TotalRecord int
	DECLARE @TotalPage int
	DECLARE @StartPageSize int
	DECLARE @EndPageIndex int
	DECLARE @OrderBy nvarchar(255)
	DECLARE @CountSql nvarchar(MAX) 
	DECLARE @RowNumberSql nvarchar(MAX) 
	DECLARE @Sql nvarchar(MAX) 

	--if @OrderType = 1
	--	BEGIN
			set @OrderBy = ' Order by '	+ @OrderField -- + REPLACE(@OrderField,',',' desc,') + ' desc '
	--	END
	--else
	--	BEGIN
	--		set @OrderBy = ' Order by ' + REPLACE(@OrderField,',',' asc,') + ' asc '	
	--	END

	-- �ܼ�¼
	set @CountSql='SELECT @TotalRecord=Count(1) From '+@TableName
	
	IF @Where <> ''
		set @CountSql = @CountSql + ' where ' + @Where
	
	IF @Debug =  1
		SELECT @CountSql
		
	execute sp_executesql @CountSql,N'@TotalRecord int out',@TotalRecord out

	SET @TotalPage=(@TotalRecord-1)/@PageSize+1
	SET @StartPageSize=(@PageNo-1)*@PageSize
	set @EndPageIndex=@PageNo*@PageSize

	set @RowNumberSql = 'SELECT ROW_NUMBER() OVER ('+ @OrderBy +') AS RowNo, '+@ReturnFields+' FROM '+@TableName
	
	IF @Where <> ''
		set @RowNumberSql = @RowNumberSql + ' where ' + @Where
	
	IF @Debug =  1
		SELECT @RowNumberSql

	SET @Sql = 'SELECT * FROM ('+ @RowNumberSql + ') AS TempTable WHERE TempTable.RowNo > ' + CAST(@StartPageSize AS VARCHAR(10))+' and TempTable.RowNo<= '+ CAST(@EndPageIndex AS VARCHAR(10))
	
	IF @Debug =  1
		SELECT @Sql
	
	execute sp_executesql @Sql

	-- ������ҳ�����ܼ�¼
	SELECT @TotalPage as PageCount,@TotalRecord as RecordCount

END

GO



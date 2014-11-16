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

	@TableName	 nvarchar(3000),	 -- 表名
	@ReturnFields	nvarchar(3000) = '*',	-- 需要返回的列 
	@PageSize	 int = 10,	 -- 每页记录数
	@PageNo	 int = 1,	 -- 当前页码
	@OrderField	 nvarchar(200),	 -- 排序字段名 最好为唯一主键
	@Where	 nvarchar(3000) = '',	 -- 查询条件, 不要加where
	--@OrderType	 int = 1,	 -- 排序类型 1:降序 其它为升序
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

	-- 总记录
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

	-- 返回总页数和总记录
	SELECT @TotalPage as PageCount,@TotalRecord as RecordCount

END

GO



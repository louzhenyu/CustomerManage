/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 15:46:07
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表LNews的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LNewsDAO : Base.BaseCPOSDAO, ICRUDable<LNewsEntity>, IQueryable<LNewsEntity>
    {
        #region 消息集合
        /// <summary>
        /// 消息集合
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet getNewsList(LNewsEntity entity, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getNewsListSql(entity);
            sql += " select *,(SELECT COUNT(*) FROM dbo.LNews x WHERE x.parentNewsId = a.newsid) replyCount From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public int getNewsListCount(LNewsEntity entity)
        {
            string sql = getNewsListSql(entity);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }
        public string getNewsListSql(LNewsEntity entity)
        {
            string sql = "select a.*,b.user_name CreateUserName ";
            sql += " ,displayIndex=row_number() over(order by a.createTime desc) ";
            sql += " into #tmp from LNews a ";
            sql += " left join t_user b on a.createBy=b.user_id ";
            sql += " where a.isDelete='0' ";
            if (entity.ParentNewsId == null || entity.ParentNewsId.Equals(""))
            {
                sql += " and (a.parentNewsId IS NULL or a.parentNewsId = '') ";
            }
            else
            {
                sql += " and a.parentNewsId = '" + entity.ParentNewsId + "' ";
            }
            sql += " order by displayIndex ";
            return sql;
        }
        /// <summary>
        /// 获取首页消息
        /// </summary>
        /// <returns></returns>
        public LNewsEntity[] GetIndexNewsList(string customerid)
        {
            List<LNewsEntity> list = new List<LNewsEntity> { };
            string sql = "select top 5 * From LNews where IsDelete = '0' and customerid='{0}' and imageurl is not null and imageurl <>'' and contenturl is not null and content <>'' order by PublishTime desc";
            using (var rd = this.SQLHelper.ExecuteReader(string.Format(sql, customerid)))
            {
                while (rd.Read())
                {
                    LNewsEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray(); ;
        }
        #endregion

        #region 获取课程新闻 Jermyn20131012

        public int GetNewsByCourseCount(string courseTypeId)
        {
            string sql = GetNewsByCourse(courseTypeId);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public DataSet GetNewsByCourseList(string courseTypeId, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = GetNewsByCourse(courseTypeId);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取课程新闻
        /// </summary>
        /// <param name="courseTypeId">课程类型标识</param>
        /// <returns></returns>
        private string GetNewsByCourse(string courseTypeId)
        {
            string sql = string.Empty;
            sql = "SELECT a.NewsId,a.NewsTitle title,convert(nvarchar(10), PublishTime,120) TIME,a.imageURL ,displayIndex=row_number() over(order by a.createTime desc) "
                + " into #tmp FROM dbo.LNews a "
                + " INNER JOIN dbo.ZCourseNewsMapping b ON(a.NewsId = b.NewsId) "
                + " INNER JOIN dbo.ZCourse c ON(b.CourseId = c.CourseId) where  c.CourseTypeId = '" + courseTypeId + "' and a.Isdelete = '0' "
                + " ORDER BY a.PublishTime DESC";
            return sql;
        }
        #endregion


        #region  查询新闻资讯
        public DataSet GetLNewsList(string cid, string typeId)
        {
            string sql = string.Format(@"select a.NewsTitle,a.Content,b.NewsTypeName from LNews a inner join LNewsType b on a.NewsType=b.NewsTypeID
 where a.IsDelete=0 and a.customerid='{0}' and a.NewsType='{1}'", cid, typeId);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region  查询新闻资讯 包括查询图片。
        /// <summary>
        /// 获取新闻列表。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="newTypeID"></param>
        /// <returns></returns>
        public DataSet GetNews(string customerID, int pageSize, int pageIndex)
        {
            if (pageIndex < 1) pageIndex = 1;
            int beginSize = (pageIndex - 1) * pageSize + 1;
            int endSize = pageIndex * pageSize;

            string sql = string.Format(@"select * from ( 
                select a.NewsId,a.NewsTitle,a.Content,a.NewsSubTitle,a.ImageUrl,a.ContentUrl,b.NewsTypeName,a.PublishTime,ROW_NUMBER() over(order by PUblishTime) RowNumber
                from LNews a inner join LNewsType b on a.NewsType=b.NewsTypeID where a.IsDelete='0' and a.CustomerID='{0}') t 
                where t.RowNumber between '{1}' and '{2}'", customerID, beginSize, endSize);

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取新闻详细
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public DataSet GetNewDetail(string customerID, string newID)
        {
            string sql = string.Format(@"select a.NewsId,a.NewsTitle,a.Content,a.NewsSubTitle,a.ImageUrl,b.NewsTypeName,a.PublishTime 
                            from LNews a inner join LNewsType b on a.NewsType=b.NewsTypeID
                             where a.IsDelete=0 and a.CustomerID='{0}' and a.NewsId='{1}'", customerID, newID);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region  查询新闻资讯类型
        public DataSet GetLNewsTypeList(string cid)
        {
            string sql = string.Format(@"select NewsTypeId,NewsTypeName from LNewsType where customerid='{0}'", cid);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 执行分页查询

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<LNewsEntity> PagedQueryNews(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat(@" select NewsId ,NewsType ,IsDefault,IsTop,NewsTitle ,NewsSubTitle ,CONVERT(varchar(30), PublishTime, 23) PublishTime,
                                    ContentUrl ,Content ,ImageUrl ,ThumbnailImageUrl ,APPId ,ClickCount = 0 ,CreateTime ,CreateBy ,
                                    LastUpdateBy ,LastUpdateTime ,IsDelete, CONVERT(varchar(30), PublishTime, 23) StrPublishTime,NewsTypeName
                                    ,BrowseNum,BookmarkNum,PraiseNum,ShareNum");
            pagedSql.AppendFormat(" from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat("isnull({0},0) {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [NewsId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(@") as ___rn,ln.* ,lnt.NewsTypeName,es.BrowseNum,es.BookmarkNum,es.PraiseNum,es.ShareNum
                                  from [LNews] as ln left join LNewsType as lnt
                                  on lnt.NewsTypeId=ln.NewsType 
                                  left join EventStats as  es
                                  on es.ObjectID=ln.NewsId and es.IsDelete=ln.IsDelete
                                  where ln.isdelete=0 and ln.CustomerId='{0}' "
                                  , this.CurrentUserInfo.ClientID);
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [LNews] where isdelete=0 and CustomerId='{0}' ", this.CurrentUserInfo.ClientID);
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<LNewsEntity> result = new PagedQueryResult<LNewsEntity>();
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
                    this.LoadData(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        #endregion

        #region 装载实体

        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void LoadData(SqlDataReader pReader, out LNewsEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new LNewsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["NewsId"] != DBNull.Value)
            {
                pInstance.NewsId = Convert.ToString(pReader["NewsId"]);
            }
            if (pReader["NewsType"] != DBNull.Value)
            {
                pInstance.NewsType = Convert.ToString(pReader["NewsType"]);
            }
            if (pReader["NewsTitle"] != DBNull.Value)
            {
                pInstance.NewsTitle = Convert.ToString(pReader["NewsTitle"]);
            }
            if (pReader["NewsSubTitle"] != DBNull.Value)
            {
                pInstance.NewsSubTitle = Convert.ToString(pReader["NewsSubTitle"]);
            }
            if (pReader["PublishTime"] != DBNull.Value)
            {
                pInstance.PublishTime = Convert.ToDateTime(pReader["PublishTime"]);
            }
            if (pReader["ContentUrl"] != DBNull.Value)
            {
                pInstance.ContentUrl = Convert.ToString(pReader["ContentUrl"]);
            }
            if (pReader["Content"] != DBNull.Value)
            {
                pInstance.Content = Convert.ToString(pReader["Content"]);
            }
            if (pReader["ImageUrl"] != DBNull.Value)
            {
                pInstance.ImageUrl = Convert.ToString(pReader["ImageUrl"]);
            }
            if (pReader["ThumbnailImageUrl"] != DBNull.Value)
            {
                pInstance.ThumbnailImageUrl = Convert.ToString(pReader["ThumbnailImageUrl"]);
            }
            if (pReader["APPId"] != DBNull.Value)
            {
                pInstance.APPId = Convert.ToString(pReader["APPId"]);
            }
            if (pReader["ClickCount"] != DBNull.Value)
            {
                pInstance.ClickCount = Convert.ToInt32(pReader["ClickCount"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["NewsTypeName"] != DBNull.Value)
            {
                pInstance.NewsTypeName = Convert.ToString(pReader["NewsTypeName"]);
            }
            if (pReader["StrPublishTime"] != DBNull.Value)
            {
                pInstance.StrPublishTime = Convert.ToString(pReader["StrPublishTime"]);
            }
            if (pReader["IsDefault"] != DBNull.Value)
            {
                pInstance.IsDefault = Convert.ToInt32(pReader["IsDefault"]);
            }
            if (pReader["IsTop"] != DBNull.Value)
            {
                pInstance.IsTop = Convert.ToInt32(pReader["IsTop"]);
            }
            if (pReader["BrowseNum"] != DBNull.Value)
            {
                pInstance.BrowseNum = Convert.ToInt32(pReader["BrowseNum"]);
            }
            if (pReader["BookmarkNum"] != DBNull.Value)
            {
                pInstance.BookmarkNum = Convert.ToInt32(pReader["BookmarkNum"]);
            }
            if (pReader["PraiseNum"] != DBNull.Value)
            {
                pInstance.PraiseNum = Convert.ToInt32(pReader["PraiseNum"]);
            }
            if (pReader["ShareNum"] != DBNull.Value)
            {
                pInstance.ShareNum = Convert.ToInt32(pReader["ShareNum"]);
            }
        }

        #endregion

        #region 根据新闻类别获取课程新闻集合 Jermyn20131017

        public int GetNewsListByTypeCount(string newsTypeId)
        {
            string sql = GetNewsListByTypeSql(newsTypeId);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public DataSet GetNewsListByType(string newsTypeId, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = GetNewsListByTypeSql(newsTypeId);
            sql += " select * From #tmp a where 1=1 and a.displayIndex between '" + beginSize + "' and '" + endSize + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取课程新闻
        /// </summary>
        /// <param name="courseTypeId">课程类型标识</param>
        /// <returns></returns>
        private string GetNewsListByTypeSql(string newsTypeId)
        {
            string sql = string.Empty;
            sql = "SELECT a.NewsId,a.NewsTitle,a.content,convert(nvarchar(10), PublishTime,120) PublishTime,a.imageURL ,displayIndex=row_number() over(order by a.createTime desc) "
                + " into #tmp FROM dbo.LNews a "
                + " where  a.newsType = '" + newsTypeId + "' and a.Isdelete = '0' "
                + " ORDER BY a.PublishTime DESC";
            return sql;
        }
        #endregion

        #region Jermyn20140103
        /// <summary>
        /// 泸州老窖咨询新闻数量集合
        /// </summary>
        /// <param name="NewsTypeId"></param>
        /// <param name="BusinessType"></param>
        /// <returns></returns>
        public int getNewsListWeeklyCount(string NewsTypeId, string BusinessType)
        {
            string sql = getNewsListWeeklySql(NewsTypeId, BusinessType);
            sql += " select count(*) count From #tmp a ";
            DataSet ds = new DataSet();
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public DataSet getNewsListWeekly(string NewsTypeId, string BusinessType, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = (page - 1) * pageSize + pageSize;

            string sql = getNewsListWeeklySql(NewsTypeId, BusinessType);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexRow between '" + beginSize + "' and '" + endSize + "' order by a.DisplayIndexRow";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public string getNewsListWeeklySql(string NewsTypeId, string BusinessType)
        {
            BusinessType = NewsTypeId + "Node" + BusinessType;

            string sql = "select * "
                    + " ,DisplayIndexRow=row_number() over(order by x.displayindexOrder desc) into #tmp "
                    + " From ( "
                    + " select * "
                    + " ,CONVERT(decimal(18,4), case when a.NewsLevel = '2' "
                    + " then  convert(nvarchar(5),isnull((select displayindex From LNews x where x.NewsId = a.ParentNewsId),0)) + '.0' + convert(nvarchar(5),a.displayindex) "
                    + " else convert(nvarchar(4), convert(nvarchar(4),a.displayindex)+'.1') "
                    + " end) DisplayindexOrder "
                    + " From LNews a "
                    + " where a.IsDelete = '0' "
                    + " and a.CustomerId = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' "
                    + " and (NewsType = '" + NewsTypeId + "' or NewsType = '" + BusinessType + "') "
                    + " ) x ";
            return sql;
        }
        #endregion

        public DataSet GetNewsList(string customerId, string newsTypeId, string newsName, int pageIndex, int pageSize)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter {ParameterName = "@pNewsTypeId", Value = newsTypeId},
                new SqlParameter {ParameterName = "@pNewsName", Value = newsName},
            };

            var sqlWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(newsTypeId))
            {
                sqlWhere.Append(" and b.newsTypeId = @pNewsTypeId");
            }
            if (!string.IsNullOrEmpty(newsName))
            {
                sqlWhere.Append(" and a.newsTitle like +'%'+ @pNewsName + '%'");
            }
            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.Append(" select  row_number()over(order by a.displayIndex desc) _row,");
            sql.Append(" a.NewsId,isnull(NewsType,'') NewsTypeId,ISNULL(b.NewsTypeName,'') NewsTypeName,");
            sql.Append(" isnull(a.NewsTitle ,'') NewsName,isnull(a.PublishTime,'') PublishTime");
            sql.Append(" from LNews a ,LNewsType b where a.NewsType = b.NewsTypeId");
            sql.Append(" and a.isdelete = 0 and b.isdelete = 0 and a.customerId = @pCustomerId");
            sql.Append(sqlWhere);
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public int GetNewsListCount(string customerId, string newsTypeId, string newsName)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter {ParameterName = "@pNewsTypeId", Value = newsTypeId},
                new SqlParameter {ParameterName = "@pNewsName", Value = newsName},
            };

            var sqlWhere = new StringBuilder();

            if (!string.IsNullOrEmpty(newsTypeId))
            {
                sqlWhere.Append(" and b.newsTypeId = @pNewsTypeId");
            }
            if (!string.IsNullOrEmpty(newsName))
            {
                sqlWhere.Append(" and a.newsTitle like +'%'+ @pNewsName + '%'");
            }
            var sql = new StringBuilder();

            sql.Append(" select isnull(count(1),0) as num ");
            sql.Append(" from LNews a ,LNewsType b where a.NewsType = b.NewsTypeId");
            sql.Append(" and a.isdelete = 0 and b.isdelete = 0 and a.customerId = @pCustomerId");
            sql.Append(sqlWhere);


            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));
        }

        /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||【新版资讯管理】Alan |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
        #region Alan 2014-08-14
        /// <summary>
        /// 获取新闻资讯列表 Add By Alan 2014-08-14
        /// </summary>
        /// <param name="lNewsEn">新闻资讯实体类</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="sortOrder">排序方式：0 升序 1 降序</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="isMappingNews">是否取未关联的资讯</param>
        /// <returns>DataSet 数据集</returns>
        public DataSet GetNewsList(LNewsEntity lNewsEn, string startDate, string endDate, string keyword, string sortField, int? sortOrder, int? pageIndex, int? pageSize, bool isMappingNews)
        {
            //Instance Object
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbCond = new StringBuilder();
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            //Build Select Condition
            if (!string.IsNullOrEmpty(lNewsEn.NewsType))
            {
                sbCond.AppendFormat("and N.NewsType='{0}' ", lNewsEn.NewsType);
            }
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                sbCond.AppendFormat("and PublishTime between '{0}' and '{1}' ", startDate, endDate);
            }
            else
            {
                if (!string.IsNullOrEmpty(startDate))
                {
                    sbCond.AppendFormat("and PublishTime >= '{0}' ", startDate);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    sbCond.AppendFormat("and PublishTime <= '{0}' ", endDate);
                }
            }
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sbCond.AppendFormat("and NewsTitle like '%{0}%' ", keyword);
            }
            //isMappingNews是否取未关联的资讯判断
            if (isMappingNews)
            {
                sbCond.AppendFormat("and NewsId not in (select NewsId from LNewsMicroMapping where CustomerId = '{0}')", CurrentUserInfo.ClientID);
            }

            //Build SQL Text
            //sbSQL.Append("select * from LNews a,LNewsType b where a.NewsType=b.NewsTypeId and a.CreateBy='Alan' and a.CustomerId='ef10dfb65a004b88a6d3f547366c56b7'");

            sbSQL.Append("select NewsId,PublishTime,NewsTitle,NewsTypeName,BrowseCount,PraiseCount,CollCount from(");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by N.{0} {1}) RowNum,NewsId,PublishTime,NewsTitle,NewsTypeName,BrowseCount,PraiseCount,CollCount from LNews N ", sortField, sort);
            sbSQL.Append("inner join LNewsType T on T.IsDelete=0 and T.CustomerId=N.CustomerId and T.NewsTypeId=N.NewsType ");
            sbSQL.AppendFormat("where N.IsDelete=0 and N.CustomerId='{0}' {1} ", CurrentUserInfo.ClientID, sbCond.ToString());
            sbSQL.Append(") as Res ");
            sbSQL.AppendFormat("where Res.RowNum between {0} and {1} ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.Append("select COUNT(NewsId) from LNews N ");
            sbSQL.Append("inner join LNewsType T on T.IsDelete=0 and T.CustomerId=N.CustomerId and T.NewsTypeId=N.NewsType ");
            sbSQL.AppendFormat("where N.IsDelete=0 and N.CustomerId='{0}' {1} ;", CurrentUserInfo.ClientID, sbCond.ToString());

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// 新增新闻资讯
        /// </summary>
        /// <param name="lNewsEn">新闻资讯实体</param>
        /// <param name="microNumberID">刊号ID</param>
        /// <param name="microTypeID">类别ID</param>
        /// <param name="labelIds">标签ID</param>
        public void AddNewsInfo(LNewsEntity lNewsEn, string microNumberID, string microTypeID, string[] labelIds)
        {
            using (var trans = SQLHelper.CreateTransaction())
            {
                try
                {
                    //Add News Info
                    lNewsEn.NewsId = Guid.NewGuid().ToString();
                    lNewsEn.CustomerId = CurrentUserInfo.ClientID;
                    lNewsEn.BrowseCount = 0;
                    lNewsEn.PraiseCount = 0;
                    lNewsEn.CollCount = 0;

                    Create(lNewsEn, trans);

                    //Add Map Info
                    LNewsMicroMappingEntity newsMicroMap = new LNewsMicroMappingEntity();
                    newsMicroMap.NewsId = lNewsEn.NewsId;
                    newsMicroMap.MicroNumberId = microNumberID;
                    newsMicroMap.MicroTypeId = microTypeID;
                    newsMicroMap.CustomerId = CurrentUserInfo.ClientID;

                    new LNewsMicroMappingDAO(CurrentUserInfo).Create(newsMicroMap, trans);

                    //更新期数版块关联
                    setNumberTypeMapping(microNumberID, microTypeID);

                    //Commit
                    trans.Commit();
                }
                catch
                {
                    //Rollback
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 更新新闻资讯
        /// </summary>
        /// <param name="lNewsEn">新闻资讯实体</param>
        /// <param name="mappingId">新闻期刊关联标识ID</param>
        /// <param name="microNumberID">刊号ID</param>
        /// <param name="microTypeID">类别ID</param>
        /// <param name="labelIds">标签ID</param>
        /// /// <param name="isRelMicro">资讯是否关联微刊</param>
        public void UpdateNewsInfo(LNewsEntity lNewsEn, string mappingId, string oldNumberId, string oldTypeId, string microNumberID, string microTypeID, string[] labelIds, bool isRelMicro)
        {
            //Instance Obj
            LNewsMicroMappingDAO privateLNMDao = new LNewsMicroMappingDAO(CurrentUserInfo);

            using (var trans = SQLHelper.CreateTransaction())
            {
                try
                {
                    //Update News Info
                    lNewsEn.CustomerId = CurrentUserInfo.ClientID;

                    Update(lNewsEn, false, trans);

                    //Update Map Info
                    LNewsMicroMappingEntity newsMicroMap = new LNewsMicroMappingEntity();
                    newsMicroMap.NewsId = lNewsEn.NewsId;
                    newsMicroMap.MicroNumberId = microNumberID;
                    newsMicroMap.MicroTypeId = microTypeID;
                    newsMicroMap.CustomerId = CurrentUserInfo.ClientID;

                    //更新时处理旧的关联 by yehua
                    if (oldNumberId != microNumberID || oldTypeId != microTypeID)
                        privateLNMDao.SetOldNewsMap(lNewsEn.NewsId, oldNumberId, oldTypeId, trans);

                    if (isRelMicro)
                    {
                        privateLNMDao.SetNewsMap(newsMicroMap, trans);
                    }
                    else
                    {
                        privateLNMDao.DelNewsMap(newsMicroMap.NewsId, trans);
                    }

                    //更新期数版块关联
                    setNumberTypeMapping(microNumberID, microTypeID);

                    //Commit
                    trans.Commit();
                }
                catch
                {
                    //Rollback
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 添加或更新资讯的时候
        /// 如果关联了期数和版块，那么需要增加一个关联 add by yehua
        /// </summary>
        private void setNumberTypeMapping(string numberId, string typeId)
        {
            string sql = string.Format("if not exists (select top 1 1 from LNumberTypeMapping where NumberId='{0}' and TypeId = '{1}') insert into LNumberTypeMapping (NumberId,TypeId,TypeCount,CustomerId) values ('{0}','{1}',1,'{2}')", numberId, typeId, CurrentUserInfo.ClientID);
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        #endregion

        #region 获取新闻资讯详细信息
        /// <summary>
        /// 获取新闻资讯详细信息 Add By Alan 2014-08-14
        /// </summary>
        /// <param name="newsId">新闻资讯ID</param>
        /// <returns></returns>
        public DataSet GetNewsDetail(string newsId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select N.NewsId,N.NewsType NewsTypeId,NT.NewsTypeName,NewsTitle,N.Intro,Content,PublishTime,ImageUrl,ThumbnailImageUrl,Author,MappingId,NMM.MicroNumberID,MicroNumber,NMM.MicroTypeID,MicroTypeName from LNews N ");
            sbSQL.Append("left join LNewsType NT on N.NewsType = NT.NewsTypeId ");
            sbSQL.Append("left join LNewsMicroMapping NMM on NMM.IsDelete = 0 and NMM.CustomerId=N.CustomerId and NMM.NewsId = N.NewsId ");
            sbSQL.Append("left join EclubMicroNumber EMN on EMN.MicroNumberID = NMM.MicroNumberId ");
            sbSQL.Append("left join EclubMicroType EMT on EMT.MicroTypeID = NMM.MicroTypeId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and N.CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and N.NewsId = '{0}' ", newsId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        #endregion

        #region 微刊（新闻资讯表）信息收集
        /// <summary>
        /// 获取微刊（新闻资讯表）详细信息 Add By Alan 2014-09-01
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns></returns>
        public DataSet GetMicroNewsDetail(string newsId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select NewsId, NewsType, NewsTitle, NewsSubTitle, Intro, Content, PublishTime, ContentUrl, ImageUrl, ThumbnailImageUrl, APPId, NewsLevel, ParentNewsId, IsDefault, IsTop, Author, BrowseCount, PraiseCount, CollCount, CreateTime, CreateBy, LastUpdateBy, LastUpdateTime, IsDelete, CustomerId, DisplayIndex from LNews ");
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and NewsId='{0}' ;", newsId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// 获取微刊（新闻资讯表）分页信息 Add By Alan 2014-09-01
        /// </summary>
        /// <param name="numberId">期数Id</param>
        /// <param name="typeId">类别Id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortOrder">排序方式：0 倒序，1 顺序</param>
        /// <returns></returns>
        public DataSet GetMicroNewsPageList(string numberId, string typeId, int pageIndex, int pageSize, int sortOrder, string sortField)
        {
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }

            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select *,Convert(varchar(10),PublishTime,121) AS PublishDate from (");
            //关联过来的资讯可以手工控制排序
            //因此这里默认第一排序按照设定的Sequence
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by NMM.Sequence DESC,N.{0} {1}) rowNum,N.*,NMM.MicroNumberId,NMM.MicroTypeId from LNews N ", sortField, sort);
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and N.NewsId=NMM.NewsId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and N.CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and MicroNumberId='{0}' and MicroTypeId='{1}' ", numberId, typeId);
            sbSQL.Append(") as res ");
            sbSQL.AppendFormat("where res.rowNum between {0} and {1} ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.Append("select COUNT(N.NewsId) from LNews N ");
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and N.NewsId=NMM.NewsId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and N.CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and MicroNumberId='{0}' and MicroTypeId='{1}' ;", numberId, typeId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// 获取微刊（新闻资讯表）姊妹信息标识 Add By Alan 2014-09-01
        /// </summary>
        /// <param name="numberId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public DataSet GetMicroNewsSiblingsId(string numberId, string typeId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select N.NewsId from LNews N ");
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and N.NewsId=NMM.NewsId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and N.CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and MicroNumberId='{0}' and MicroTypeId='{1}' ;", numberId, typeId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        //标记微刊（新闻资讯表）阅读、浏览、转发数
        public int SetMicroNewsColl(string newsId, string fildName)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update LNews set {0} = {0} + 1 ", fildName);
            sbSQL.AppendFormat("where IsDelete=0 and NewsId = '{0}';", newsId);

            //Access DB 
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }

        /// <summary>
        /// 取微刊阅读、分享、浏览量
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <param name="field">字段：BrowseCount, PraiseCount, CollCount</param>
        /// <returns></returns>
        public int GetMicroNewsStats(string newsId, string field)
        {
            int res = 0;
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("select top 1 {0} from LNews where IsDelete=0 and NewsId = '{1}'", field, newsId);

            object o = this.SQLHelper.ExecuteScalar(sbSQL.ToString()) ?? string.Empty;
            //Access DB 
            int.TryParse(o.ToString(), out res);
            return res;
        }
        #endregion

    }
}

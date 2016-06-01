/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
    /// 表WMaterialText的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WMaterialTextDAO : Base.BaseCPOSDAO, ICRUDable<WMaterialTextEntity>, IQueryable<WMaterialTextEntity>
    {
        public bool CheckName(string appId, string name, string textId)
        {
            string sql = "select count(1) from WMaterialText where ApplicationId=@appId and isdelete=0 and Title=@name and TextId!=@textId";
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@appId", appId));
            pList.Add(new SqlParameter("@name", name));
            pList.Add(new SqlParameter("@textId", textId));
            int result = (int)this.SQLHelper.ExecuteScalar(CommandType.Text, sql, pList.ToArray());
            return result > 0;
        }
        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebListCount(WMaterialTextEntity entity)
        {
            string sql = GetWebListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebList(WMaterialTextEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebListSql(WMaterialTextEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.[displayindex]) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " into #tmp ";
            sql += " from [WMaterialText] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " inner join [WModelTextMapping] c on (c.isDelete='0' and a.TextId=c.TextId) ";
            sql += " inner join [WModel] d on (d.isDelete='0' and c.ModelId=d.ModelId) ";
            sql += " where a.IsDelete='0' ";

            sql += " and d.ModelId = '" + entity.ModelId + "' ";

            return sql;
        }
        #endregion

        #region 根据ID获取图文消息

        /// <summary>
        /// 根据ID获取图文消息
        /// </summary>
        /// <param name="textId">消息ID</param>
        /// <returns></returns>
        public DataSet GetMaterialTextByID(string textId)
        {
            string sql = " SELECT TOP 10 * FROM dbo.WMaterialText WHERE IsDelete = 0 "
                + " AND (TextId = '" + textId + "' OR ParentTextId = '" + textId + "') "
                + " ORDER BY DisplayIndex ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 获取图文消息集合 
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="ObjectDataFrom">1=关键字，2=菜单</param>
        /// <returns></returns>
        public DataSet GetMaterialTextByIDJermyn(string objectId, int ObjectDataFrom)
        {
            string sql = string.Empty;
            switch (ObjectDataFrom)
            {
                case 1:
                    sql = "select top 10 a.* From WMaterialText a "
                        + " inner join WMenuMTextMapping b on(a.TextId = b.TextId) "
                        + " inner join WKeywordReply c on(b.MenuId = c.ReplyId) "
                        + " where a.IsDelete = '0' and b.IsDelete = '0' and c.IsDelete = '0' "
                        + " and c.ReplyId = '" + objectId + "' "
                        + " order by b.DisplayIndex";
                    break;
                case 2:
                    sql = "select top 10 a.* From WMaterialText a "
                        + " inner join WMenuMTextMapping b on(a.TextId = b.TextId) "
                        + " inner join WMenu c on(b.MenuId = c.ID) "
                        + " where a.IsDelete = '0' and b.IsDelete = '0' and c.IsDelete = '0' "
                        + " and c.ID ='" + objectId + "' "
                        + " order by b.DisplayIndex";
                    break;
                default:
                    sql = "";
                    break;

            }
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 根据模板标识获取图文消息集合 Jermyn20131209
        public DataSet GetMaterialTextListByModelId(string ModelId)
        {
            string sql = "select a.* From WMaterialText a inner join WModelTextMapping b on(a.TextId = b.TextId) "
                        + " where a.IsDelete = '0' and b.IsDelete = '0' and b.ModelId = '" + ModelId + "'  "
                        + " order by a.DisplayIndex asc ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        public WMaterialTextEntity[] GetWMaterialTextList(string pCustomerID, string pName, string pMaterialTextId, string typeId, int pPageSize, int pPageIndex)
        {
            List<WMaterialTextEntity> list = new List<WMaterialTextEntity> { };
            StringBuilder sub = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter> { };
            if (!string.IsNullOrEmpty(pName))
            {
                sub.AppendFormat(" and a.title like +'%'+ @LikeCondition +'%'");
                paras.Add(new SqlParameter() { ParameterName = "@LikeCondition", Value = pName });
            }
            if (!string.IsNullOrEmpty(pMaterialTextId))
            {
                sub.AppendFormat(" and a.TextId = @TextId ");
                paras.Add(new SqlParameter() { ParameterName = "@TextId", Value = pMaterialTextId });
            }
            if (!string.IsNullOrEmpty(typeId))
            {
                sub.AppendFormat(" and c.ModelId = @TypeId ");
                paras.Add(new SqlParameter() { ParameterName = "@TypeId", Value = typeId });
            }


            paras.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = pCustomerID });
            //   ,咱是
            string sql = string.Format(@"select * from 
                                                (select ROW_NUMBER()over(order by a.CreateTime) _row,a.*,c.ModelId from 
                                                 WMaterialText a 
                                                 join WApplicationInterface b on a.ApplicationId=b.ApplicationId
                                                 join WModelTextMapping c on a.textId=c.textId
                                                where a.isdelete=0 and b.isdelete=0 and b.CustomerId=@CustomerId {0}) t 
                                        where t._row>{1}*{2} and t._row<=({1}+1)*{2}", sub, pPageIndex, pPageSize);
            using (var rd = this.SQLHelper.ExecuteReader(CommandType.Text, sql, paras.ToArray()))
            {
                while (rd.Read())
                {
                    WMaterialTextEntity m;
                    this.Load(rd, out m);
                    m.ModelId = rd["ModelId"].ToString();
                    list.Add(m);
                }
            }
            return list.ToArray();
        }


        public int GetWMaterialTextListCount(string pCustomerID, string pName, string pMaterialTextId, string typeId)
        {

            var sub = new StringBuilder();
            var paras = new List<SqlParameter> { };
            if (!string.IsNullOrEmpty(pName))
            {
                sub.AppendFormat(" and a.title like  +'%'+ @LikeCondition +'%' ");
                paras.Add(new SqlParameter() { ParameterName = "@LikeCondition", Value = pName });
            }
            if (!string.IsNullOrEmpty(pMaterialTextId))
            {
                sub.AppendFormat(" and a.TextId = @TextId ");
                paras.Add(new SqlParameter() { ParameterName = "@TextId", Value = pMaterialTextId });
            }
            if (!string.IsNullOrEmpty(typeId))
            {
                sub.AppendFormat(" and c.ModelId = @TypeId ");
                paras.Add(new SqlParameter() { ParameterName = "@TypeId", Value = typeId });
            }

            paras.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = pCustomerID });
            string sql = string.Format(@"select isnull(count(1),0) from WMaterialText a join WApplicationInterface b 
                        on a.ApplicationId=b.ApplicationId
                        join WModelTextMapping c on a.textId=c.textId
                        where a.isdelete=0 and b.isdelete=0 and b.CustomerId=@CustomerId {0}
                                        ", sub.ToString());
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, paras.ToArray()));

        }


        #region GetWMaterialTextPage
        /// <summary>
        /// 获取图文素材管理列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="typeID"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public DataSet GetWMaterialTextPage(string title, string ID, int? pPageSize, int? pPageIndex)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
                select a.TextId,ParentTextId,Title,Author,CoverImageUrl,Text,OriginalUrl,DisplayIndex,a.TypeId,a.LastUpdateTime,d.user_name as LastUpdateBy
                ,ROW_NUMBER() Over (Order By a.lastupdatetime desc)ROW_NUMBER 
                into #wmTemp
                From WMaterialText a
                inner join WModelTextMapping b on(a.TextId = b.TextId)
              
                left join T_User d on a.LastUpdateBy=d.user_id
                inner join WApplicationInterface w on w.ApplicationId=a.ApplicationId
                where a.IsDelete = '0' 
                and b.IsDelete = '0'
 
             and w.CustomerId='{0}'
             ", this.CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(title))
            {
                strb.AppendFormat(" and a.Title like '%" + title + "%'");
            }
            if (!string.IsNullOrEmpty(ID))
            {
                strb.AppendFormat(" and b.ModelId like '" + ID + "'");
            }
            strb.AppendLine(GetEvPageSQL(pPageSize, pPageIndex).ToString());
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;
        }

        #region GetEvPageSQL
        /// <summary>
        /// 返回分页SQL
        /// </summary>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public StringBuilder GetEvPageSQL(int? pPageSize, int? pPageIndex)
        {
            StringBuilder pageSql = new StringBuilder();
            pageSql.AppendLine(string.Format(@"
            declare @PageIndex int ={0}
            declare @PageSize int={1}
            declare @PageCount int
            declare @RowsCount int
            declare @PageStart int
            declare @PageEnd int
            SELECT @RowsCount=COUNT(1) FROM #wmTemp
            if(@RowsCount%@PageSize=0)
                begin
                    set @PageCount=@RowsCount/@PageSize
                end
            else
                begin
                    set @PageCount=@RowsCount/@PageSize+1
                end
            if(@PageIndex<0)
                begin
                    set @PageIndex=0
                end
            else if(@PageIndex>=@PageCount)
                begin
                    set @PageIndex=@PageCount
                end
            set @PageStart=@PageIndex*@PageSize
            set @PageEnd=@PageStart+@PageSize
            set @PageEnd=@PageStart+@PageSize
            SELECT * FROM #wmTemp WHERE ROW_NUMBER between  @PageStart+1 and @PageEnd order by ROW_NUMBER
            SELECT @RowsCount RowsCount,@PageCount PageCount
            DROP TABLE #wmTemp", pPageIndex, pPageSize));
            return pageSql;//新加了 order by ROW_NUMBER
        }
        #endregion
        #endregion

        #region MyRegion

        public DataSet GetWmType()
        {
            StringBuilder strb = new StringBuilder();
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.AppendFormat("select ModelId,ModelName  from WModel where IsDelete='0' and CustomerId='{0}'", this.CurrentUserInfo.ClientID).ToString());
            return ds;
        }
        #endregion

        public DataSet GetMaterialTextTitleList(string textId, string customerId)
        {
            var paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pTextId", Value = textId });
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });

            var sql = new StringBuilder();
            sql.Append(
                // "select isnull(a.Text,'')Text,isnull(a.Title,'')Title from WMaterialText  a,WApplicationInterface b");
               "select isnull(a.Text,'')Text,isnull(a.Title,'')Title,a.CoverImageUrl,a.Author from WMaterialText  a,WApplicationInterface b");
            sql.Append(" where a.ApplicationId = b.ApplicationId");
            sql.Append(" and a.IsDelete = 0 and b.IsDelete = 0");
            sql.Append(" and b.CustomerId = @pCustomerId");
            sql.Append(" and a.TextId = @pTextId");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

        }

    }
}

/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/27 20:34:17
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
    /// 表WModel的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WModelDAO : Base.BaseCPOSDAO, ICRUDable<WModelEntity>, IQueryable<WModelEntity>
    {
        #region
        public DataSet GetWModelListByAppId(string AppId)
        {
            DataSet ds = new DataSet();
            string sql = " SELECT * FROM dbo.WModel WHERE IsDelete = '0' AND applicationId = '" + AppId + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 通过微信ID获取“被添加自动回复”素材类型和关注素材ID

        /// <summary>
        /// 通过微信ID获取“被添加自动回复”素材类型和关注素材ID
        /// </summary>
        /// <param name="weixinId">微信ID</param>
        /// <returns></returns>
        public DataSet GetMaterialByWeixinId(string weixinId)
        {
            string sql = string.Empty;
            sql += " SELECT MaterialTypeId, MaterialId FROM dbo.WModel ";
            sql += " WHERE IsDelete = 0 AND ModelId = ( ";
            sql += " 	SELECT TOP 1 ModelId FROM dbo.WAutoReply a ";
            sql += " 	INNER JOIN dbo.WApplicationInterface b ON a.ApplicationId = b.ApplicationId ";
            sql += " 	WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND b.WeiXinID = '" + weixinId + "') ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 通过微信ID获取“被添加自动回复”信息 Jermyn20140512
        /// </summary>
        /// <param name="weixinId"></param>
        /// <param name="KeyworkType">1=关键字回复 2=关注回复 3=自动回复</param>
        /// <returns></returns>
        public DataSet GetMaterialByWeixinIdJermyn(string weixinId,int KeyworkType)
        {
            string sql = string.Empty;
            sql = "select top 1 a.* From WKeywordReply a "
                + " inner join WApplicationInterface b on(a.ApplicationId = b.ApplicationId) "
                + " where a.IsDelete = '0' and b.IsDelete = '0' "
                + " and a.KeywordType = '" + KeyworkType + "' and b.WeiXinID = '" + weixinId + "' "
                + " order by DisplayIndex ";

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(WModelEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(WModelEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(WModelEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.ApplicationId, a.[ModelName] asc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " into #tmp ";
            sql += " from [WModel] a ";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " where a.IsDelete='0' ";
            if (entity.ModelName != null && entity.ModelName.Trim().Length > 0)
            {
                sql += " and (a.ModelName like '%" + entity.ModelName.Trim() + "%' or a.ModelCode like '%" + entity.ModelName.Trim() + "%') ";
            }
            if (entity.ApplicationId != null && entity.ApplicationId.Trim().Length > 0)
            {
                sql += " and a.ApplicationId = '" + entity.ApplicationId.Trim() + "' ";
            }
            if (entity.CustomerId != null && entity.CustomerId.Trim().Length > 0)
            {
                sql += " and a.CustomerId = '" + entity.CustomerId.Trim() + "' ";
            }
            return sql;
        }
        #endregion

        public bool IsExist(string ApplicationId, string ModelName, string ModelId)
        {
            string sql = "select count(*) From WModel where 1=1 and ModelName = '" + ModelName + "'";
            sql += " and ApplicationId='" + ApplicationId + "' ";
            if (ModelId != null && ModelId.Trim().Length > 0)
            {
                sql += " and ModelId != '" + ModelId.Trim() + "' ";
            }
            int n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return n > 0 ? true : false;
        }


        public DataSet GetWModelList(string customerId,string applicationId, int pageIndex, int pageSize)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter() {ParameterName = "@pApplicationId", Value = applicationId}
            };
            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.Append(
                "select  row_number()over(order by createTime desc) as _row, ModelId,ModelName from WModel where IsDelete=0 and customerId = @pCustomerId and applicationId = @pApplicationId ");
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

    }
}

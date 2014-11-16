/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:01:06
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
    /// 表BrandDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class BrandDetailDAO : Base.BaseCPOSDAO, ICRUDable<BrandDetailEntity>, IQueryable<BrandDetailEntity>
    {
        #region 获取同步福利品牌

        /// <summary>
        /// 获取同步福利品牌
        /// </summary>
        /// <param name="latestTime">最后同步时间</param>
        /// <returns></returns>
        public DataSet GetSynWelfareBrandList(string latestTime)
        {
            string sql = string.Empty;
            sql += " SELECT brandid = a.BrandId ";
            sql += " , brandname = a.BrandName ";
            sql += " , brandcode = a.BrandCode ";
            sql += " , branddesc = a.BrandDesc ";
            sql += " , brandengname = a.BrandEngName ";
            sql += " , displayIndex = row_number() over(order by a.BrandName) ";
            sql += " , brandlogourl = a.BrandLogoURL ";
            sql += " , tel = a.Tel ";
            sql += " FROM dbo.BrandDetail a WHERE 1 = 1 and customerId='"+this.CurrentUserInfo.CurrentUser.customer_id+"' ";

            if (!string.IsNullOrEmpty(latestTime))
            {
                sql += " AND LastUpdateTime >= '" + latestTime + "' ";
            }

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebBrandDetailCount(BrandDetailEntity entity)
        {
            string sql = GetWebBrandDetailSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebBrandDetail(BrandDetailEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebBrandDetailSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebBrandDetailSql(BrandDetailEntity entity)
        {
            var orderBy = "a.DisplayIndex asc";
            
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by " + orderBy + ") ";
            sql += " ,b.User_Name CreateByName ";
            sql += " into #tmp ";
            sql += " from [BrandDetail] a ";
            sql += " left join [t_user] b on a.CreateBy=b.user_id ";
            sql += " where a.isDelete='0' and customerId='" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.BrandName != null && entity.BrandName.Trim().Length > 0)
            {
                sql += " and a.BrandName like '%" + entity.BrandName.Trim() + "%' ";
            }
            if (entity.BrandCode != null && entity.BrandCode.Trim().Length > 0)
            {
                sql += " and a.BrandCode like '%" + entity.BrandCode.Trim() + "%' ";
            }
            //sql += " order by a.[Prop_Name] asc ";
            return sql;
        }
        #endregion

    }
}

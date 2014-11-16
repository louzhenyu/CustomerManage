/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:20
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
    /// 表ESales的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ESalesDAO : Base.BaseCPOSDAO, ICRUDable<ESalesEntity>, IQueryable<ESalesEntity>
    {
        #region 管理系统接口
        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="salesInfo">参数对象</param>
        /// <param name="Page">页码</param>
        /// <param name="PageSize">页面数量</param>
        /// <param name="TotalCount">返回结果集总数量</param>
        /// <returns></returns>
        public DataSet GetSalesList(ESalesEntity salesInfo, int Page, int PageSize, out int TotalCount)
        {
            DataSet ds = new DataSet();
            TotalCount = 0;
            #region 获取集合
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            string sql = GetSalesListSql(salesInfo);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            #endregion
            #region 获取数量
            string sql1 = GetSalesListSql(salesInfo);
            sql1 = sql1 + " select count(*) as icount From #tmp; ";
            TotalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql1));
            #endregion
            return ds;
        }
        /// <summary>
        /// 获取查询的公共sql方法
        /// </summary>
        /// <param name="salesInfo"></param>
        /// <returns></returns>
        private string GetSalesListSql(ESalesEntity salesInfo)
        {
            string sql = string.Empty;
            #region
            sql = "SELECT a.* "
                + " ,(SELECT x.EnterpriseCustomerName FROM dbo.EEnterpriseCustomers x WHERE a.EnterpriseCustomerId = x.EnterpriseCustomerId AND a.IsDelete = '0') EnterpriseCustomerName "
                + " ,(SELECT x.ECSourceName FROM dbo.EEnterpriseCustomerSource x WHERE a.ECSourceId = x.ECSourceId AND x.IsDelete = '0') ECSourceName "
                + " ,(SELECT x.SalesProductName FROM dbo.ESalesProduct x WHERE a.SalesProductId = x.SalesProductId AND x.IsDelete = '0' ) SalesProductName "
                + " ,(SELECT x.user_name FROM dbo.T_User x WHERE x.user_id = a.SalesVipId AND x.user_status = '1') SalesVipName "
                + " ,(SELECT x.user_name FROM dbo.T_User x WHERE a.CreateBy = x.user_id) CreateUserName "
                + " ,(SELECT x.StageName FROM dbo.ESalesStage x WHERE x.StageId = a.StageId AND x.IsDelete = '0') StageName "
                + " ,DisplayIndex = row_number() over(order by a.LastUpdateTime desc ) "
                + " into #tmp "
                + " FROM dbo.ESales a "
                + " WHERE a.IsDelete = '0' AND a.CustomerId = '"+this.CurrentUserInfo.CurrentUser.customer_id+"'";
            #endregion
            #region
            if (salesInfo.SalesName != null && salesInfo.SalesName.Trim().Length > 0)
            {
                sql += " and a.SalesName like '%" + salesInfo.SalesName + "%' ";
            }
            if (salesInfo.SalesProductId != null && salesInfo.SalesProductId.Trim().Length > 0)
            {
                sql += " and a.SalesProductId = '" + salesInfo.SalesProductId + "' ";
            }
            if (salesInfo.SalesVipId != null && salesInfo.SalesVipId.Trim().Length > 0)
            {
                sql += " and a.SalesVipId = '" + salesInfo.SalesVipId + "' ";
            }
            if (salesInfo.ECSourceId != null && salesInfo.ECSourceId.Trim().Length > 0)
            {
                sql += " and a.ECSourceId = '" + salesInfo.ECSourceId + "' ";
            }
            if (salesInfo.EnterpriseCustomerId != null && salesInfo.EnterpriseCustomerId.Trim().Length > 0)
            {
                sql += " and a.EnterpriseCustomerId = '" + salesInfo.EnterpriseCustomerId + "' ";
            }
            if (salesInfo.StageId != null && salesInfo.StageId.Trim().Length > 0)
            {
                sql += " and a.StageId = '" + salesInfo.StageId + "' ";
            }
            #endregion
            return sql;
        }
        #endregion

        #region 获取销售负责人
        public DataSet GetESalesChargeVipList()
        {
            string sql = string.Empty;
            sql = "SELECT DISTINCT a.user_id SalesChargeVipId ,a.user_name SalesChargeVipName FROM dbo.T_User a "
                + " INNER JOIN dbo.T_User_Role b "
                + " ON(a.user_id = b.user_id) "
                + " INNER JOIN dbo.T_Role c "
                + " ON(b.role_id = c.role_id) "
                + " WHERE a.customer_id = c.customer_id "
                + " AND a.user_status = '1' "
                + " AND c.status = '1' "
                + " AND c.role_code = 'ESalesCharge' "
                + " AND a.customer_id = '"+this.CurrentUserInfo.CurrentUser.customer_id+"'";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
        #endregion
    }
}

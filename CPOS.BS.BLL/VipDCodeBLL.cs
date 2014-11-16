/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/12 18:24:54
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipDCodeBLL:BaseService
    {
       
        public System.Data.SqlClient.SqlTransaction GetTran()
        {
            return _currentDAO.GetTran();
        }

        ///// <summary>
        ///// 计算返现金额 计算方式：返现金额 = 基数【策率表】 + （销售金额 × 系数【策率表】）
        ///// </summary>
        //public decimal GetReturnAmount(string SalesAmount, string customerId, LoggingSessionInfo loggingSession)
        //{
        //    WXSalesPolicyRateBLL bll = new WXSalesPolicyRateBLL(loggingSession);
        //    decimal retumount = 0;
        //    var entity = bll.QueryByEntity(new WXSalesPolicyRateEntity { CustomerId = customerId, IsDelete = 0 }, null);
        //    if (string.IsNullOrWhiteSpace(SalesAmount))
        //        SalesAmount = "0.0";
        //    if (entity != null && entity.Length > 0)
        //    {
        //        retumount = Convert.ToDecimal(entity.FirstOrDefault().CardinalNumber + (Convert.ToDecimal(SalesAmount) * entity.FirstOrDefault().Coefficient).ToString());
        //    }
        //    else
        //    {
        //        DataSet ds = bll.getReturnAmount(customerId);
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRow row = ds.Tables[0].Rows[0];
        //            retumount = Convert.ToDecimal(Convert.ToDecimal(row["CardinalNumber"].ToString()) + (Convert.ToDecimal(SalesAmount) * Convert.ToDecimal(row["Coefficient"].ToString())));
        //        }
        //    }
        //    return Convert.ToDecimal(retumount.ToString("0.00"));
        //}
    }

}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 10:21:46
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表CustomerWithdrawalDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerWithdrawalDetailDAO : BaseCPOSDAO, ICRUDable<CustomerWithdrawalDetailEntity>, IQueryable<CustomerWithdrawalDetailEntity>
    {
        /// <summary>
        /// 获取支付明细ID
        /// </summary>
        /// <returns></returns>
        public string GetWithdrawalDetailByOrderPayId(string pCustomerId, string WithdrawalId)
        {
            DataSet ds = new DataSet();
            string OrderPayId = string.Empty;
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("select * from CustomerWithdrawalDetail where CustomerId='{0}' and WithdrawalId='{1}'  and IsDelete=0 ", pCustomerId,WithdrawalId);
            ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                OrderPayId = ds.Tables[0].Rows[0]["OrderPayId"].ToString();
            }
            return OrderPayId;
        }
    }
}

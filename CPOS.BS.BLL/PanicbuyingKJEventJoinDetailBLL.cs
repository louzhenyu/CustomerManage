/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:47
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class PanicbuyingKJEventJoinDetailBLL
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        public DataSet GetHelperList(string EventId, string KJEventJoinId, string SkuId, int PageSize, int PageIndex)
        {
            return this._currentDAO.GetHelperList(EventId, KJEventJoinId, SkuId, PageSize, PageIndex);
        }

        ///// <summary>
        ///// 获取砍价参与表最小的金额
        ///// </summary>
        ///// <param name="KJEventJoinId"></param>
        ///// <returns></returns>
        //public decimal GetMinMomentSalesPrice(string KJEventJoinId)
        //{
        //    return this._currentDAO.GetMinMomentSalesPrice(KJEventJoinId);
        //}
    }
}
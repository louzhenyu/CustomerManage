/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 16:50:40
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
    /// 表MarketNamedApply的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketNamedApplyDAO : Base.BaseCPOSDAO, ICRUDable<MarketNamedApplyEntity>, IQueryable<MarketNamedApplyEntity>
    {
        /// <summary>
        /// 获取申请活动的会员
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetVipByMarketNamedApply(string customerId)
        {
            string sql = string.Format(@"select b.vipName as VipName
            ,b.Phone as Phone
            ,a.QRCodeUrl as QRCodeUrl
            from MarketNamedApply a 
            inner join vip b on a.VipID = b.VIPID where a.Status = 20 and a.CustomerId = '{0}'", customerId);

            return this.SQLHelper.ExecuteDataset(sql);
        }

    }
}

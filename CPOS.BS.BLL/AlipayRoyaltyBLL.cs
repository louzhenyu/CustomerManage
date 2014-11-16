/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-08-27 15:33
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
    public partial class AlipayRoyaltyBLL
    {  
        /// <summary>
        /// 根据商户网站唯一订单号获取分润信息
        /// </summary>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        public DataSet GetAlipayRoyalty(string outTradeNo)
        {
            return this._currentDAO.GetAlipayRoyalty(outTradeNo);
        }
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-05-31 20:42
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
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class AlipayWapTradeResponseBLL
    {  
        /// <summary>
        /// 根据商户网站唯一订单号更新支付宝交易记录
        /// </summary>
        /// <param name="pEntity"></param>
        public void UpdateAlipayWapTrade(AlipayWapTradeResponseEntity pEntity)
        {
            this._currentDAO.UpdateAlipayWapTrade(pEntity, false);
        }

        /// <summary>
        /// 根据商户网站唯一订单号更新支付宝交易状态
        /// </summary>
        /// <param name="outTradeNo">商户网站唯一订单号</param>
        /// <param name="status">状态值 1：创建交易  2：交易成功  3：交易失败</param>
        public void UpdateAlipayWapTradeStatus(string outTradeNo, string status)
        {
            this._currentDAO.UpdateAlipayWapTradeStatus(outTradeNo, status);
        }
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/18 10:28:46
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VipOrderSubRunObjectMappingBLL
    {
        /// <summary>
        /// 处理会员与分润方关系存储过程
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="vipId">会员ID</param>
        /// <param name="subRunObjectId">分润方标识
        /// 可选值为：
        /// 1：上线会员；2：分享会员；3：会籍店；
        /// </param>
        /// <param name="subRunObjectValue">
        /// 对应分润方值,根据分润方标识：
        /// 标识为1时，为对应上线会员ID;
        /// 为2时，为对应分享会员ID;
        /// 为3时，为对应会籍店ID;
        /// </param>
        /// <returns></returns>
        public dynamic SetVipOrderSubRun(string customerId, string vipId,
            int subRunObjectId, string subRunObjectValue)
        {
             return this._currentDAO.SetVipOrderSubRun(customerId, vipId, subRunObjectId, subRunObjectValue);
        }
    }
}
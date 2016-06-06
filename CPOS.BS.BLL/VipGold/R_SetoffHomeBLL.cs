/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/18 14:40:24
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
    public partial class R_SetoffHomeBLL
    {
        /// <summary>
        /// 获取 最近 统计日 的 一条 记录信息
        /// </summary>
        /// <param name="customerId">商户编号</param>
        /// <param name="SetoffToolTypeId">工具类型</param>
        /// <param name="SetoffTypeId">类型</param>
        /// <returns></returns>
        public DataSet GetReceiveSetoffHomeByCustomerId(string customerId, string SetoffToolTypeId, int SetoffTypeId)
        {
            return this._currentDAO.GetReceiveSetoffHomeByCustomerId(customerId, SetoffToolTypeId,SetoffTypeId);
        }
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 16:29:54
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
    public partial class APPVersionManagerBLL
    {
        /// <summary>
        /// 获取当前客户客户端版本信息
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="channel">渠道</param>
        /// <param name="plat">平台</param>
        /// <returns></returns>
        public APPVersionManagerEntity GetAppVersion(string customerId, int channel, string plat)
        {
            return this._currentDAO.GetAppVersion(customerId, channel, plat);
        }
    }
}
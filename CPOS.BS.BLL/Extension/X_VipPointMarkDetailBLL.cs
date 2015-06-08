/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-6 16:15:14
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
    public partial class X_VipPointMarkDetailBLL
    {
        /// <summary>
        /// 本周获得点标个数（判断是否可继续答题；大于0表示本周有答题）
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="startWeek"></param>
        /// <param name="endWeek"></param>
        /// <returns></returns>
        public X_VipPointMarkDetailEntity GetPointMarkByWeek(string vipId, DateTime startWeek, DateTime endWeek)
        {
            return this._currentDAO.GetPointMarkByWeek(vipId, startWeek, endWeek);
        }
    }
}
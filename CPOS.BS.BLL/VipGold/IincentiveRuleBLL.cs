/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 14:15:31
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
    /// 业务处理：  获取激励信息
    /// </summary>
    public partial class IincentiveRuleBLL
    {
        /// <summary>
        /// 获取激励信息
        /// </summary>
        /// <param name="SetoffType">分类  1=会员集客  2=员工集客</param>
        /// <returns></returns>
        public DataSet GetIncentiveRule(string SetoffType)
        {
            return _currentDAO.GetIncentiveRule(SetoffType);
        }
    }
}
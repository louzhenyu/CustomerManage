/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/5 18:19:19
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
    public partial class MHCategoryAreaGroupBLL
    {
        public int GetMaxGroupId()
        {
            return this._currentDAO.GetMaxGroupId();
        }
        /// <summary>
        /// 获取首页每个模块配置信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetLayoutList(string pCustomerId)
        {
            return this._currentDAO.GetLayoutList(pCustomerId);
        }
    }
}
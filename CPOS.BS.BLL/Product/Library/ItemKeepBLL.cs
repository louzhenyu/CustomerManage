/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:34
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
    public partial class ItemKeepBLL
    {
        #region 获取用户收藏
        /// <summary>
        /// 获取用户收藏
        /// </summary>
        /// <param name="pItemId"></param>
        /// <param name="pVipId"></param>
        /// <returns></returns>
        public ItemKeepEntity GetItemKeepByUser(string pItemId, string pVipId)
        {
            return this._currentDAO.GetItemKeepByUser(pItemId, pVipId);
        }
        #endregion
    }
}
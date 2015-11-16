/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    public partial class VipCardBalanceChangeBLL
    {
        #region 会员卡余额变动记录
        /// <summary>
        /// 获取卡状态变更记录查询
        /// </summary>
        /// <param name="VipCardCode">卡号</param>
        /// <param name="pOrderBys">排序条件</param>
        /// <param name="pPageSize">叶大小</param>
        /// <param name="pCurrentPageIndex">叶索引</param>
        /// <returns>集合</returns>
        public PagedQueryResult<VipCardBalanceChangeEntity> GetVipCardBalanceChangeList(string VipCardCode, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipCardBalanceChangeList(VipCardCode, pPageSize, pCurrentPageIndex);
        }
        #endregion
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:49
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
    public partial class R_SRT_RTProductTopBLL
    {
        /// <summary>
        /// 获取商品排行信息
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="SortName"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        public List<R_SRT_RTProductTopEntity> GetSRT_RTProductTopList(string CustomerID, string SortName, string SortOrder)
        {
            return _currentDAO.GetSRT_RTProductTopList(CustomerID, SortName, SortOrder);
        }
    }
}
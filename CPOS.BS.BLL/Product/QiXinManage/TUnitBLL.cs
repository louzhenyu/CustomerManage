/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 10:37:49
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
    public partial class TUnitBLL
    {
        /// <summary>
        /// 获取直接子部门
        /// </summary>
        /// <param name="pParentUnitID"></param>
        /// <returns></returns>
        public DataTable GetDirectSubDept(object pParentUnitID)
        {
            return this._currentDAO.GetDirectSubDept(pParentUnitID);
        }

        /// <summary>
        /// 获取部门-管理
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <param name="pTypeID"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetUnitList(string pUnitID, string pTypeID, int pPageIndex, int pPageSize, string pUnitName, out int totalPage)
        {
            return this._currentDAO.GetUnitList(pUnitID, pTypeID, pPageIndex, pPageSize, pUnitName, out totalPage);
        }
    }
}
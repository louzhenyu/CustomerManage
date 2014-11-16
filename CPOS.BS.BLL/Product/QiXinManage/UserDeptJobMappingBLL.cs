/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 16:26:27
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
    public partial class UserDeptJobMappingBLL
    {
        /// <summary>
        /// 根据UserID获取实例
        /// </summary>
        /// <param name="UserID">UserID</param>
        public UserDeptJobMappingEntity GetByUserID(object pUserID)
        {
            return _currentDAO.GetByUserID(pUserID);
        }

        /// <summary>
        /// 获取部门直接成员
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public DataTable GetDirectPersMembers(object pUnitID)
        {
            return this._currentDAO.GetDirectPersMembers(pUnitID);
        }
    }
}
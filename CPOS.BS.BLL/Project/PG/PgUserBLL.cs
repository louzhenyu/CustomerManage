/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/17 14:32:42
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
    public partial class PgUserBLL
    {
        #region 验证用户是否是特定城市的工会主席
        /// <summary>
        /// 验证用户是否是特定城市的工会主席
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pCustomerID"></param>
        /// <param name="pCityName"></param>
        /// <returns></returns>
        public bool VerifyIsLocalLuOwner(string pUserID, string pCustomerID, string pCityName)
        {
            return _currentDAO.VerifyIsLocalLuOwner(pUserID, pCustomerID, pCityName);
        }
        #endregion
    }
}
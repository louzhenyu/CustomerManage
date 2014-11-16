/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 12:26:34
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
    /// 用户与云通讯用户关联表TUserThirdPartyMapping业务处理：  
    /// </summary>
    public partial class TUserThirdPartyMappingBLL
    {
        #region 获取云通讯VoipAccount集合
        /// <summary>
        /// 获取云通讯VoipAccount集合
        /// </summary>
        /// <param name="userIDList">企信用户ID集合</param>
        /// <returns></returns>
        public List<string> GetVoipAccountList(List<string> userIDList)
        {
            return _currentDAO.GetVoipAccountList(userIDList);
        }
        #endregion

        #region 根据企信用户帐号返回对应云通讯上的帐号
        /// <summary>
        /// 根据企信用户帐号返回对应云通讯上的帐号
        /// </summary>
        /// <param name="userIDList">企信userID</param>
        /// <returns></returns>
        public DataTable GetCloudUserList(List<string> userIDList)
        {
            return _currentDAO.GetCloudUserList(userIDList);
        }
        #endregion
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class EclubValidationBLL
    {

        #region 设置指定用户验证码过期
        /// <summary>
        /// 设置指定用户验证码过期
        /// </summary>
        /// <param name="vipID">用户ID</param>
        /// <param name="tran"></param>
        public void TimeOutByValidation(string vipID, SqlTransaction tran = null)
        {
            _currentDAO.TimeOutByValidation(vipID, tran);
        }
        #endregion

        //获取当前登录
        public object GetUserInfo(string userName, string password)
        {
            //校验用户登录信息
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return _currentDAO.GetUserInfo(userName, password);
        }
        /// <summary>
        /// 获取用户通行证：VipId
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>通行证</returns>
        public object GetUserID(string email)
        {
            return _currentDAO.GetUserID(email);
        }
    }
}
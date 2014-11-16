using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;

namespace JIT.CPOS.BS.BLL.Product.CW
{
    public class UserServiceBLL
    {
        public LoggingSessionInfo CurrentUserInfo;
        public UserService _currentDAO;

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UserServiceBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new UserService(pUserInfo);
        }
        #endregion

        #region GetUserInfoByEmail
        /// <summary>
        /// GetUserInfoByEmail
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserInfoByEmail(string email)
        {
            return _currentDAO.GetUserInfoByEmail(email);
        }



        #endregion
    }
}

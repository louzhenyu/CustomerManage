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
using JIT.CPOS.BS.DataAccess.Project.HuaAn;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 预定支付业务逻辑类定义。
    /// </summary>
    public class ReserveBLL
    {
        public LoggingSessionInfo CurrentUserInfo;
        public ReserveDAO _currentDAO;

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ReserveBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new ReserveDAO(pUserInfo);
        }
        #endregion

        #region VerificationIsRegister
        /// <summary>
        /// 获取获取Vip用户信息
        /// </summary>
        /// <returns></returns>
        public int GetVipInfo(string userID, string customerID)
        {
            return _currentDAO.GetVipInfo(userID, customerID);
        }
        #endregion
    }
}

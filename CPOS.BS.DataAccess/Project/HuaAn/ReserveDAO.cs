using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;


namespace JIT.CPOS.BS.DataAccess.Project.HuaAn
{
    /// <summary>
    /// 预定支付类定义。
    /// </summary>
    public partial class ReserveDAO : BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public ReserveDAO(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 验证用户是否注册
        /// <summary>
        ///验证用户是否注册:（1已注册，0未注册）
        /// </summary>
        /// <returns></returns>
        public int GetVipInfo(string userID, string customerID)
        {
            string sql = string.Format("select isnull(Phone,'') as Phone from vip where VIPID='{0}' and ClientID='{1}' and IsDelete='0' ", userID, customerID);
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql);
            string phone = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                phone = ds.Tables[0].Rows[0]["Phone"].ToString();
            }

            if (!string.IsNullOrEmpty(phone)) return 1;

            return 0;
        }
        #endregion
    }
}

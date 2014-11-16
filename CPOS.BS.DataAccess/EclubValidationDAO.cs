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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表EclubValidation的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubValidationDAO : Base.BaseCPOSDAO, ICRUDable<EclubValidationEntity>, IQueryable<EclubValidationEntity>
    {

        #region 设置指定用户验证码过期
        /// <summary>
        /// 设置指定用户验证码过期
        /// </summary>
        /// <param name="vipID">用户ID</param>
        /// <param name="tran"></param>
        public void TimeOutByValidation(string vipID, SqlTransaction tran)
        {
            string sql = string.Format("update EclubValidation set LoginStatus=2 where LoginStatus=0 and VipID='{0}'", vipID);
            if (tran == null)
                this.SQLHelper.ExecuteNonQuery(sql);
            else
                this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
        }
        #endregion


        //获取当前登录
        public object GetUserInfo(string userName, string password)
        {
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select top 1 VipID from Vip ");
            sbSQL.AppendFormat("where IsDelete = 0 and ClientID = '{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            sbSQL.AppendFormat("and VipName = '{0}' ", userName);
            sbSQL.AppendFormat("and VipPasswrod='{0}' ;", password);

            //Execute
            return this.SQLHelper.ExecuteScalar(sbSQL.ToString());
        }

        /// <summary>
        /// 获取用户通行证：VipId
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>通行证</returns>
        public object GetUserID(string email)
        {
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select top 1 VipID from Vip ");
            sbSQL.AppendFormat("where IsDelete = 0 and ClientID = '{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            sbSQL.AppendFormat("and Col38 = '{0}' ", email);

            //Execute
            return this.SQLHelper.ExecuteScalar(sbSQL.ToString());
        }
    }
}

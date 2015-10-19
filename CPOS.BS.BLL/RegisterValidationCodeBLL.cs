/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/25 11:53:15
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
using System.Configuration;
using JIT.Utility.Log;
using System.Data.SqlClient;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class RegisterValidationCodeBLL
    {
        public string SendCode(string mobile)
        {
            string result = "200";
            VipBLL vipBLL = new VipBLL(CurrentUserInfo);

            var vip = vipBLL.Query(new IWhereCondition[] { 
                new EqualsCondition(){ FieldName = "Phone", Value = mobile}
                , new EqualsCondition(){ FieldName = "ClientID", Value = CurrentUserInfo.ClientID}
            }, null).FirstOrDefault();

            if (vip != null)
            {
                Random rd = new Random();
                string code = rd.Next(100000, 999999).ToString();

                string sign = "";
                sign = vipBLL.GetSettingValue(CurrentUserInfo.ClientID);

                string msg = "";
                if (!Utils.SendSMSCode(CurrentUserInfo.ClientID, mobile, code, sign, out msg))//发送短信
                {
                    throw new Exception("短信发送失败：" + msg);
                }
                else
                {
                    string message = (ConfigurationManager.AppSettings["ValidationMessage"] ?? "{0}").ToString();
                    message = string.Format(message, code);
                    _currentDAO.InsertSMS(mobile, message, sign);
                }

                this.Create(new RegisterValidationCodeEntity()
                {
                    CodeID = JIT.CPOS.Common.Utils.NewGuid(),
                    Mobile = mobile,
                    Code = code,
                    IsValidated = 0
                });
            }
            else
                result = "102";

            return result;
        }

        #region HS_SendCode
        /// <summary>
        /// 华硕校园发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public string HS_SendCode(string mobile, string code, LoggingSessionInfo loggingSessionInfo)
        {
            string result = "200";
            VipBLL vipBLL = new VipBLL(loggingSessionInfo);
            var vip = vipBLL.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "Phone", Value = mobile }, new EqualsCondition() { FieldName = "ClientID", Value = this.CurrentUserInfo.ClientID } }, null).FirstOrDefault();
            if (vip == null)
            {
                this.Create(new RegisterValidationCodeEntity()
                {
                    CodeID = JIT.CPOS.Common.Utils.NewGuid(),
                    Mobile = mobile,
                    Code = code,
                    IsValidated = 0
                });
            }
            return result;
        }
        #endregion

        public void DeleteByMobile(string pMobile, int isSuccess = 0, SqlTransaction tran = null)
        {
            this._currentDAO.DeleteByMobile(pMobile, isSuccess, tran);
        }

        public RegisterValidationCodeEntity GetByMobile(string pMobile, SqlTransaction tran = null)
        {
            var temp = this._currentDAO.GetByMobile(pMobile, tran);
            if (temp.Length > 0)
                return temp[0];
            else
                return null;
        }
    }
}
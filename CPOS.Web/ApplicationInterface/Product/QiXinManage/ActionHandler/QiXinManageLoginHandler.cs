using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    /// QiXinManageLogin的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "QiXinManageLogin")]
    public class QiXinManageLoginHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return QiXinManageLogin(pRequest);
        }

        public string QiXinManageLogin(string pRequest)
        {
            var rd = new APIResponse<QiXinManageLoginRD>();
            var rdData = new QiXinManageLoginRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<QiXinManageLoginRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            T_UserBLL tubll = new T_UserBLL(loggingSessionInfo);
            DataSet ds = tubll.ManageUserInfo(rp.Parameters.Email);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string pwd = ds.Tables[0].Rows[0]["UserPassword"] == null ? "" : ds.Tables[0].Rows[0]["UserPassword"].ToString();
                if (pwd.ToLower() == rp.Parameters.Pwd.ToLower())
                {
                    rdData.LoginUserInfo = DataTableToObject.ConvertToObject<ManageUserInfo>(ds.Tables[0].Rows[0]);
                    rd.ResultCode = 0;
                }
                else
                {
                    rd.ResultCode = 101;
                    rd.Message = "密码错误";
                }
            }
            else
            {
                rd.ResultCode = 101;
                rd.Message = "管理用户不存在";
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 企信管理登录
    public class QiXinManageLoginRP : IAPIRequestParameter
    {
        public string Email { set; get; }
        public string Pwd { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
                throw new APIException("【Email】不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Pwd))
                throw new APIException("【Pwd】不能为空") { ErrorCode = 102 };
        }
    }
    public class QiXinManageLoginRD : IAPIResponseData
    {
        public ManageUserInfo LoginUserInfo { set; get; }
    }
    #endregion
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.Web.GoHigh
{
    public partial class Data : System.Web.UI.Page
    {
        string customerId = "29E11BDC6DAC439896958CC6866FF64E";
        string customerId_Lj = "e703dbedadd943abacf864531decdac1";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["action"].ToString().Trim();
                switch (dataType)
                {
                    case "setUser":      //1.同步用户新建修改接口
                        content = setUser();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }

            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region 同步用户新建修改接口
        public string setUser()
        {
            string content = string.Empty;
            var respData = new setUserRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setUser: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setUserReqData>();
                reqObj = reqObj == null ? new setUserReqData() : reqObj;
                #region 处理参数
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setUserReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.userId == null || reqObj.special.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.userCode == null || reqObj.special.userCode.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userCode不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.userName == null || reqObj.special.userName.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userName不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.password == null || reqObj.special.password.Length == 0)
                {
                    respData.code = "2201";
                    respData.description = "password不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region 处理业务
                cUserService userService = new cUserService(loggingSessionInfo);
                var user = new UserInfo();
                user.customer_id = customerId;
                user.User_Id = reqObj.special.userId;
                user.User_Code = reqObj.special.userCode;
                user.User_Name = reqObj.special.userName;
                user.User_Password = reqObj.special.password;
                user.User_Status = "1";
                user.Fail_Date = "9999-12-31";
                user.User_Telephone = reqObj.special.telephone;
                user.User_Email = reqObj.special.email;
                user.userRoleInfoList = new List<UserRoleInfo>();
                user.userRoleInfoList.Add(new UserRoleInfo() { 
                    Id = Utils.NewGuid(),
                    RoleId = "",
                    UserId = reqObj.special.userId,
                    Status = "1"
                });
                user.Create_Time = Utils.GetNow();
                user.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                user.Modify_Time = Utils.GetNow();
                user.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                string error = "";
                var flag = userService.SetUserInfo(user, user.userRoleInfoList, out error);
                if (!flag)
                {
                    respData.code = "103";
                    respData.description = error;
                    return respData.ToJSON().ToString();
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        #region
        public class setUserRespData : Default.LowerRespData
        {
        }
        public class setUserRespContentData
        {
        }
        public class setUserReqData : ReqData
        {
            public setUserReqSpecialData special;
        }
        public class setUserReqSpecialData
        {
            public string customerId { get; set; }
            public string userId { get; set; }
            public string userCode { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public string telephone { get; set; }
            public string email { get; set; }
        }
        #endregion
        #endregion
    }

    #region ReqData

    public class ReqData
    {
        public ReqCommonData common;
    }
    public class ReqCommonData
    {
        public string locale;
        public string userId;
        public string openId;
        public string signUpId;
        public string customerId;
    }

    #endregion


}
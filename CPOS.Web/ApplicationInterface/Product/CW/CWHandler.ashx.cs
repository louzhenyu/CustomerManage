using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.Product;
using JIT.CPOS.BS.BLL.Product.CW;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Product.CW;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Product.CW
{
    /// <summary>
    /// 企信Hanler定义。
    /// </summary>
    public class CWHandler : BaseGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var rst = string.Empty;
            switch (pAction)
            {
                case "Login":
                    rst = Login(pRequest);
                    break;
                case "GetUsers":
                    rst = GetUsers(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }
            return rst;
        }

        #region  登录
        /// <summary>
        /// 登录Action。 
        ///1. 验证用户信息
        ///2. 登录 
        // 登录： 1)如果登录成功，验证则调用云通讯接口：创建子账号。 并将返回的信息保存在（2050用户与第三方用户关联表TUserThirdPartyMapping)TUserThirdPartyMapping中，
        // 
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string Login(string reqContent)
        {
            var rd = new APIResponse<UserInfoRD>();
            try
            {
                var rp = reqContent.DeserializeJSONTo<APIRequest<UserInfoRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                DataSet ds = bll.GetUserInfoByEmail(rp.Parameters.Email);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var user = DataTableToObject.ConvertToList<T_UserEntity>(ds.Tables[0]).FirstOrDefault();
                    if (user.user_password.Equals(MD5Helper.Encryption(rp.Parameters.Password)))  //
                    {
                        var userID = user.user_id;
                        //验证是否在第三方注册
                        TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(loggingSessionInfo);
                        TUserThirdPartyMappingEntity tutpmEntiy = tutpmBll.GetByID(userID);

                        UserViewModel userinfo = null;
                        if (tutpmEntiy == null)
                        {
                            ThirdUserViewModel token = null;
                            CloudRequestFactory factory = new CloudRequestFactory();
                            //调用云通讯创建子账户
                            Dictionary<string, object> retData = factory.CreateSubAccount("sandboxapp.cloopen.com", "8883", "ff8080813bbcae3f013bcc39c18a0022", "8f32e2023d804e1390a3b0b8b36d6e28", "aaf98f893e7df943013e8728b2b400c7", "user1234544rr656678");
                            string jsonData = getDictionaryData(retData);
                            token = CWHelper.Deserialize<ThirdUserViewModel>(jsonData);
                            if (token.statusCode == MessageStatusCode.Success)
                            {
                                tutpmEntiy = CreateThirdUser(tutpmEntiy, token, userID);
                                tutpmBll.Create(tutpmEntiy);
                            }

                            userinfo = new UserViewModel
                            {
                                UserID = userID,
                                VoipAccount = token.SubAccount.voipAccount,
                                UserName = user.user_name,
                                UserImgURL = ""
                            };
                        }

                        var rdData = new UserInfoRD();
                        rdData.UserInfo = userinfo;
                        rd.Data = rdData;
                        rd.Message = "登录成功";
                    }
                    else
                    {
                        rd.ResultCode = 300;
                        rd.Message = "密码错误";
                    }
                }
                else
                {
                    rd.ResultCode = 300;
                    rd.Message = "邮箱不存在";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rd.ToJSON();
        }

        /// <summary>
        /// 创建第三方用户。
        /// </summary>
        /// <param name="tutpmEntiy"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private TUserThirdPartyMappingEntity CreateThirdUser(TUserThirdPartyMappingEntity tutpmEntiy, ThirdUserViewModel token, string userID)
        {
            tutpmEntiy = new TUserThirdPartyMappingEntity();
            tutpmEntiy.UserId = userID;
            tutpmEntiy.SubAccountSid = token.SubAccount.subAccountSid;
            tutpmEntiy.SubToken = token.SubAccount.subToken;
            tutpmEntiy.StatusCode = token.statusCode;

            tutpmEntiy.DateCreated = token.SubAccount.dateCreated;
            tutpmEntiy.VoipAccount = token.SubAccount.voipAccount;
            tutpmEntiy.VoipPwd = token.SubAccount.voipPwd;

            return tutpmEntiy;
        }
        #endregion

        #region   获取用户列表
        private string GetUsers(string reqContent)
        {
            var rd = new APIResponse<UserInfoListRD>();
            try
            {
                var rp = reqContent.DeserializeJSONTo<APIRequest<UserInfoListRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                DataSet ds = bll.GetUserInfo(rp.CustomerID, rp.Parameters.Email, rp.Parameters.Phone, rp.Parameters.UserCode, rp.Parameters.UserName, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var rdData = new UserInfoListRD();
                    rdData.UserInfo = DataTableToObject.ConvertToList<T_UserEntity>(ds.Tables[0]);
                    rd.Data = rdData;
                    rd.Message = "获取成功";
                }
                else
                    rd.Message = "获取失败";
            }
            catch (Exception)
            {
                throw;
            }
            return rd.ToJSON();
        }
        #endregion

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (var item in data)
            {
                return item.Value.ToString();
            }
            return ret;
        }

    }

    #region 定义接口的请求参数及响应结果的数据结构

    #region 用户登陆验证
    public class UserInfoRP : IAPIRequestParameter
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public string FriendlyName { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new APIException("请输入邮件地址");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new APIException("请输入密码");
            }
        }
    }

    public class UserInfoRD : IAPIResponseData
    {
        public UserViewModel UserInfo { set; get; }

    }
    #endregion

    #region 用户列表
    public class UserInfoListRP : IAPIRequestParameter
    {
        public string Email { set; get; }
        public string Phone { set; get; }
        public string UserName { set; get; }
        public string UserCode { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public void Validate()
        {
            PageIndex = 0;
            PageSize = 15;
        }
    }
    public class UserInfoListRD : IAPIResponseData
    {
        public List<T_UserEntity> UserInfo { set; get; }
    }
    #endregion

    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Configuration;
using System.Data;
using JIT.Utility;

namespace JIT.CPOS.Web.ApplicationInterface.Customer
{
    /// <summary>
    /// CloudGateway 的摘要说明
    /// </summary>
    public class CloudGateway : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            {
                string rst;
                switch (pAction)
                {
                    case "GetCardBag":  //获取我的卡包
                        rst = GetCardBag(pRequest);
                        break;
                    case "SetPassWord":
                        rst = this.SetPassWord(pRequest);
                        break;
                    default:
                        throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                        {
                            ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                        };
                }
                return rst;
            }
        }
        /// <summary>
        /// 获取我的卡包
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCardBag(string pRequest)
        {
            try
            {
                string cloudCustomerId = ConfigurationManager.AppSettings["CloudCustomerId"];//云店标识
                var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                var vipBll = new VipBLL(loggingSessionInfo);
                var vipInfo = vipBll.GetByID(rp.UserID);
                string userId = string.Empty;
                var rd = new CardBagRD();
                if (vipInfo != null)
                {
                    if (!string.IsNullOrEmpty(vipInfo.WeiXinUserId))
                    {
                        DataSet dsVip = vipBll.GetCardBag(vipInfo.WeiXinUserId,cloudCustomerId);
                        if (dsVip.Tables[0].Rows.Count > 0)
                        {
                            rd.CardBagList = DataTableToObject.ConvertToList<CardBag>(dsVip.Tables[0]);
                        }
                    }
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }


            private string SetPassWord(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetPassWordRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            string error = "";
            string pNewPass = MD5Helper.Encryption( rp.Parameters.pNewPWD);
            //pOldPWD = MD5Helper.Encryption(pOldPWD);
            rp.Parameters.pOldPWD = EncryptManager.Hash(rp.Parameters.pOldPWD, HashProviderType.MD5);
            string res = "{\"success\":\"false\",\"msg\":\"保存失败\"}";
            //组装参数
            JIT.CPOS.BS.Entity.User.UserInfo entity = new JIT.CPOS.BS.Entity.User.UserInfo();
            var serviceBll = new cUserService(loggingSessionInfo);
            entity = serviceBll.GetUserById(loggingSessionInfo, rp.Parameters.pID);
            string apPwd = serviceBll.GetPasswordFromAP(loggingSessionInfo.ClientID, rp.Parameters.pID);
            //if (pOldPWD == entity.User_Password)
            if (rp.Parameters.pOldPWD == apPwd)
            {
                entity.userRoleInfoList = new cUserService(loggingSessionInfo).GetUserRoles(rp.Parameters.pID);//, PageBase.JITPage.GetApplicationId()
                entity.User_Password = pNewPass;
                entity.ModifyPassword = true;
                //new cUserService(CurrentUserInfo).SetUserInfo(entity, entity.userRoleInfoList, out error);
                bool bReturn = serviceBll.SetUserPwd(loggingSessionInfo, pNewPass, out error);
                res = "{\"success\":\"false\",\"msg\":\"" + error + "\"}";
            }
            else
            {
                res = "{\"success\":\"false\",\"msg\":\"旧密码不正确\"}";
            }
            return res;
        }
  

        public class CardBagRD : IAPIResponseData
        {
            public List<CardBag> CardBagList { get; set; }
        }
        public class CardBag
        {
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string ImageUrl { get; set; }
            public string UserId { get; set; }
            private string targetUrl;
            public string TargetUrl
            {
                get
                {
                    //微商城地址
                    return ConfigurationManager.AppSettings["website_url"] + "HtmlApps/html/public/index/index_shop_app.html?customerId=" + CustomerID + "&userId=" + UserId + "&APP_TYPE=SUBSCIBE&FromYun=1";
                }
                set { targetUrl = value; }
            }
        }



    }

 public class SetPassWordRP : IAPIRequestParameter
    {
        public string pID { get; set; }
        public string pOldPWD { get; set; }
        public string pNewPWD { get; set; }


        public void Validate()
        {
        }
    }





    public class SetPassWordRD : IAPIResponseData
    {
        //public VipCardInfo VipCardInfo { get; set; }
        //public VipInfo[] RelateVipList { get; set; }

    }
}
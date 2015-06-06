using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL.WX;
using System.Web.Security;

namespace JIT.CPOS.Web.ApplicationInterface
{
    /// <summary>
    /// CustomerGateway 的摘要说明
    /// </summary>
    public class CustomerGateway : BaseGateway
    {
        public string GetCustomerBasicSetting(string pRequest)
        {
            CustomerBasicSettingRD rd = new CustomerBasicSettingRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var bll = new CustomerBasicSettingBLL(loggingSessionInfo);
                var ds = bll.GetCustomerBaiscSettingInfo(rp.CustomerID);  //获取CusertomerBaiscSettingInfo中配置的数据
                List<CustomerImageInfo> list = new List<CustomerImageInfo> { };
                Dictionary<string, string> DicVersion = new Dictionary<string, string>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow row = ds.Tables[0].Rows[i];
                            if (row["SettingCode"].ToString().Equals("AboutUs"))
                            {
                                rd.AboutUs = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("BrandRelated"))
                            {
                                rd.BrandRelated = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("BrandStory"))
                            {
                                rd.BrandStory = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("IntegralAmountPer"))
                            {
                                rd.IntegralAmountPer = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("SMSSign"))
                            {
                                rd.SMSSign = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }

                            if (row["SettingCode"].ToString().Equals("ForwardingMessageLogo"))
                            {
                                rd.ForwardingMessageLogo = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("ForwardingMessageTitle"))
                            {
                                rd.ForwardingMessageTitle = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("ForwardingMessageSummary"))
                            {
                                rd.ForwardingMessageSummary = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("WhatCommonPoints"))
                            {
                                rd.WhatCommonPoints = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("GetPoints"))
                            {
                                rd.GetPoints = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                            if (row["SettingCode"].ToString().Equals("SetSalesPoints"))
                            {
                                rd.SetSalesPoints = DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString();
                            }
                        }
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[1].Rows)
                        {
                            var ImageList = new CustomerImageInfo()
                            {
                                ImageId = item["ImageId"].ToString(),
                                ImageUrl = item["ImageUrl"].ToString()
                            };
                            list.Add(ImageList);
                        }
                    }
                    rd.ImageList = list.ToArray();  //图片集合
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);

                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
        public string GetCustomerId(string pRequest)
        {
            try
            {
                CustomerCodeRD rd = new CustomerCodeRD();
                var rp = pRequest.DeserializeJSONTo<APIRequest<CustomerCodeRq>>();
                var code = rp.Parameters.CustomerCode;
                var loggingSessionInfo = Default.GetLoggingSession();
                var bll = new t_customerBLL(loggingSessionInfo);
                var entity = bll.GetCustomer(code);
                var id = entity == null ? "" : entity.customer_id;
                rd.CustomerId = id;
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }
        }
        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <returns></returns>
        public string getJsApiTicket(string customerId, string appid, string appSecret)
        {
            return new CommonBLL().GetJsApiTicketByCache(appid, appSecret, Default.GetBSLoggingSession(customerId, "1")).ticket;
        }
        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <returns></returns>
        public string getJsApiSignature(string ticket, string noncestr, string timestamp, string url)
        {
            string paramater = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ticket, noncestr, timestamp, url);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(paramater, "SHA1").ToLower();
        }
        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <returns></returns>
        public string getJsApiConfig(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<WeiXinConfigRq>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var appService = new WApplicationInterfaceBLL(loggingSessionInfo);
            var appEntity = appService.QueryByEntity(new WApplicationInterfaceEntity() { CustomerId = rp.CustomerID }, null)[0];
            string timestamp = ((long)((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds)).ToString();
            string nonceStr = Guid.NewGuid().ToString("N").Substring(0, 16);
            XeiXinJsApiConfig config = new XeiXinJsApiConfig()
            {
                debug = rp.Parameters.debug,
                appId = appEntity.AppID,
                timestamp = timestamp,
                nonceStr = nonceStr,
                signature = this.getJsApiSignature(this.getJsApiTicket(rp.CustomerID, appEntity.AppID, appEntity.AppSecret), nonceStr, timestamp, rp.Parameters.url),
                jsApiList = new List<string>()
            };
            return new SuccessResponse<IAPIResponseData>(config).ToJSON();
        }
        /// <summary>
        /// 获取微信统一凭证接口
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var appService = new WApplicationInterfaceBLL(loggingSessionInfo);


            var appEntity = appService.QueryByEntity(new WApplicationInterfaceEntity() { CustomerId = rp.CustomerID }, null)[0];

            var tempAccessToke = new CommonBLL().GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, loggingSessionInfo);
            //重新取一次
            appEntity = appService.QueryByEntity(new WApplicationInterfaceEntity() { CustomerId = rp.CustomerID }, null)[0];

            WxAccessToken accessToken = new WxAccessToken()
            {
                access_token = tempAccessToke.access_token,//appEntity.RequestToken,
                expires_in = tempAccessToke.expires_in,
                ExpirationTime = appEntity.ExpirationTime.ToString()
            };
            return new SuccessResponse<IAPIResponseData>(accessToken).ToJSON();
        }
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetCustomerBasicSetting":
                    rst = GetCustomerBasicSetting(pRequest);
                    break;
                case "GetCustomerIdByCode":
                    rst = GetCustomerId(pRequest);
                    break;
                case "getJsApiConfig":
                    rst = this.getJsApiConfig(pRequest);
                    break;
                case "GetAccessToken":
                    rst = this.GetAccessToken(pRequest);
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
    public class CustomerCodeRq : IAPIRequestParameter
    {
        public string CustomerCode { get; set; }

        public void Validate()
        {

        }
    }
    public class CustomerCodeRD : IAPIResponseData
    {
        public string CustomerId { get; set; }
    }
    public class CustomerBasicSettingRD : IAPIResponseData
    {
        public string AboutUs { get; set; }   //关于我们
        public string BrandRelated { get; set; }  //品牌相关
        public string BrandStory { get; set; }//品牌故事
        public string IntegralAmountPer { get; set; }  //积分抵用金额的比率
        public string SMSSign { get; set; }  //手机短信签名
        public string ForwardingMessageLogo { get; set; }  //转发消息图标
        public string ForwardingMessageTitle { get; set; } //转发消息默认标题
        public string ForwardingMessageSummary { get; set; } //转发消息摘要文字
        public string WhatCommonPoints { get; set; }  //什么是通用积分
        public string GetPoints { get; set; }  //如何获得积分
        public string SetSalesPoints { get; set; }  //如何消费积分
        public CustomerImageInfo[] ImageList { get; set; }  //图片集合
    }
    public class CustomerImageInfo
    {
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
    }


    public class WeiXinConfigRq : IAPIRequestParameter
    {
        public bool debug { get; set; }
        public string url { get; set; }
        public string code { get; set; }
        public void Validate()
        {

        }
    }
    public class XeiXinJsApiConfig : IAPIResponseData
    {
        public bool debug { get; set; }
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
        public List<string> jsApiList { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class WxAccessToken : IAPIResponseData
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string ExpirationTime { get; set; }
    }
}
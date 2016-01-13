using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Customer
{
    public class SetBusinessBasisConfigAH : BaseActionHandler<SetBusinessBasisConfigRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetBusinessBasisConfigRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);
            //ap库配置
            //var APloggingSessionInfo = Default.GetBSLoggingSession(loggingSessionInfo.ClientID, "1");
            //APloggingSessionInfo.CurrentLoggingManager.Connection_String = ConfigurationManager.AppSettings["Conn_ap"];
            //var t_customerBLL = new t_customerBLL(APloggingSessionInfo);


            #region 基础配置集合赋值
            var list = new List<CustomerBasicSettingEntity>();

            if (!string.IsNullOrWhiteSpace(para.CustomerShortName))
            {
                //var APCustomerData = t_customerBLL.GetByID(APloggingSessionInfo.ClientID);
                //if (APCustomerData != null)
                //{
                //    APCustomerData.customer_name = para.customer_name;
                //    t_customerBLL.Update(APCustomerData);
                //}

                list.Add(new CustomerBasicSettingEntity()
                {//商户简称
                    SettingCode = "CustomerShortName",
                    SettingValue = para.CustomerShortName
                });
            }
            if (!string.IsNullOrWhiteSpace(para.WebLogo))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//商户Logo
                    //SettingCode = "BusinessLogo",
                    SettingCode = "WebLogo",
                    SettingValue = para.WebLogo
                });
            }
            if (!string.IsNullOrWhiteSpace(para.CustomerPhone))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//客服电话
                    SettingCode = "CustomerPhone",
                    SettingValue = para.CustomerPhone
                });
            }
            if (!string.IsNullOrWhiteSpace(para.ForwardingMessageTitle))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//分享标题
                    //SettingCode = "ShareTitle",
                    SettingCode = "ForwardingMessageTitle",
                    SettingValue = para.ForwardingMessageTitle
                });
            }
            if (!string.IsNullOrWhiteSpace(para.ForwardingMessageLogo))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//分享图片
                    //SettingCode = "ShareImageUrl",
                    SettingCode = "ForwardingMessageLogo",
                    SettingValue = para.ForwardingMessageLogo
                });
            }
            if (!string.IsNullOrWhiteSpace(para.ForwardingMessageSummary))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//分享摘要内容
                    //SettingCode = "ShareContent",
                    SettingCode = "ForwardingMessageSummary",
                    SettingValue = para.ForwardingMessageSummary
                });
            }
            if (!string.IsNullOrWhiteSpace(para.GuideLinkUrl))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//引导链接
                    SettingCode = "GuideLinkUrl",
                    SettingValue = para.GuideLinkUrl
                });
            }
            if (!string.IsNullOrWhiteSpace(para.GuideQRCode))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//引导二维码
                    SettingCode = "GuideQRCode",
                    SettingValue = para.GuideQRCode
                });
            }
            if (!string.IsNullOrWhiteSpace(para.CustomerGreeting))
            {
                list.Add(new CustomerBasicSettingEntity()
                {//客服欢迎语
                    SettingCode = "CustomerGreeting",
                    SettingValue = para.CustomerGreeting
                });
            }
            #endregion
            if (list.Count > 0)
                customerBasicSettingBLL.SaveustomerBasicrInfo(list);


            return rd;
        }

    }
}
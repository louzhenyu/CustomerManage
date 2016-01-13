using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.Customer.Request;
using JIT.CPOS.DTO.Module.Basic.Customer.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Customer
{
    public class GetBusinessBasisConfigInfoAH : BaseActionHandler<GetBusinessBasisConfigInfoRP, GetBusinessBasisConfigInfoRD>
    {
        protected override GetBusinessBasisConfigInfoRD ProcessRequest(DTO.Base.APIRequest<GetBusinessBasisConfigInfoRP> pRequest)
        {
            var rd = new GetBusinessBasisConfigInfoRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

            #region 商户基础信息
            DataRow dr = customerBasicSettingBLL.GetCustomerInfo(loggingSessionInfo.ClientID).Tables[0].Rows[0];
            rd.customer_name = dr["customer_name"].ToString();//商户全称
            //
            var ResultList = customerBasicSettingBLL.GetBusinessBasisConfigInfo(loggingSessionInfo.ClientID);
            //var Data1 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("BusinessLogo"));
            var Data = ResultList.FirstOrDefault(m => m.SettingCode.Equals("CustomerShortName"));
            rd.CustomerShortName = Data == null ? "" : Data.SettingValue;//商户简称
            var Data1 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("WebLogo"));
            rd.WebLogo = Data1 == null ? "" : Data1.SettingValue;//商户Logo
            var Data2 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("CustomerPhone"));
            rd.CustomerPhone = Data2 == null ? "" : Data2.SettingValue;//客户电话
            //var Data3 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("ShareTitle"));
            var Data3 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("ForwardingMessageTitle"));
            rd.ForwardingMessageTitle = Data3 == null ? "" : Data3.SettingValue;//分享标题
            //var Data4 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("ShareImageUrl"));
            var Data4 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("ForwardingMessageLogo"));
            rd.ForwardingMessageLogo = Data4 == null ? "" : Data4.SettingValue;//分享图片
            //var Data5 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("ShareContent"));
            var Data5 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("ForwardingMessageSummary"));
            rd.ForwardingMessageSummary = Data5 == null ? "" : Data5.SettingValue;//分享摘要内容
            var Data6 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("GuideLinkUrl"));
            rd.GuideLinkUrl = Data6 == null ? "" : Data6.SettingValue;//引导链接
            var Data7 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("GuideQRCode"));
            rd.GuideQRCode = Data7 == null ? "" : Data7.SettingValue;//引导二维码
            var Data8 = ResultList.FirstOrDefault(m => m.SettingCode.Equals("CustomerGreeting"));
            rd.CustomerGreeting = Data8 == null ? "" : Data8.SettingValue;//欢迎去

            #endregion

            return rd;
        }
    }
}
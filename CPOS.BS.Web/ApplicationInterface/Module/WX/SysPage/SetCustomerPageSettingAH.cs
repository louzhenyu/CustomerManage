using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using System.Configuration;
using JIT.Utility.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Customer;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class SetCustomerPageSettingAH : BaseActionHandler<SetCustomerPageSettingRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetCustomerPageSettingRP> pRequest)
        {
           EmptyResponseData rdata = new EmptyResponseData();
           SysPageBLL sysPageBLL = new SysPageBLL(this.CurrentUserInfo);
           for (int i = 0; i < pRequest.Parameters.Node.Length; i++)
           {
               int Res = sysPageBLL.SetCustomerPageSetting(CurrentUserInfo.ClientID, pRequest.Parameters.MappingId, pRequest.Parameters.PageKey, pRequest.Parameters.Node[i].ToString(), pRequest.Parameters.NodeValue[i].ToString(), CurrentUserInfo.UserID);
           }

           //发布到js文件里
           EmptyRequest commonRequest = new EmptyRequest();
           commonRequest.Parameters = new EmptyRequestParameter();
           commonRequest.UserID = "";
           commonRequest.CustomerID = CurrentUserInfo.ClientID;
           commonRequest.OpenID = "";
           commonRequest.Token = Guid.NewGuid().ToString();
           commonRequest.ChannelId = "";
           commonRequest.Locale = "1";

           //发布
           var url = ConfigurationManager.AppSettings["interfacehost"] + "/ApplicationInterface/Gateway.ashx";  //正式
           //var url = "http://121.199.42.125:5012/Gateway.ashx";        //测试

           if (string.IsNullOrEmpty(url))
               throw new Exception("未配置平台接口URL:interfacehost");

           url += "?type=Product&action=WX.SysPage.CreateCustomerConfig";
           var postContent = string.Format("req={0}", commonRequest.ToJSON());
           var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
           var aldRsp = strAldRsp.DeserializeJSONTo<CreateCustomerConfigReponse>();

           if (aldRsp == null || aldRsp.ResultCode != 0 || !aldRsp.IsSuccess)
           {
               throw new Exception("发布配置到文件失败");
           }

           return rdata;
        }
    }
}
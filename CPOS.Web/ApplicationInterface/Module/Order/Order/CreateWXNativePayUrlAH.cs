using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class CreateWXNativePayUrlAH : BaseActionHandler<CreateWXNativePayUrlRP, CreateWXNativePayUrlRD>
    {
        protected override CreateWXNativePayUrlRD ProcessRequest(DTO.Base.APIRequest<CreateWXNativePayUrlRP> pRequest)
        {
            CreateWXNativePayUrlRD rd = new CreateWXNativePayUrlRD();
            //支付中心URL
            var url = System.Configuration.ConfigurationManager.AppSettings["paymentcenterUrl"];
            //url = "http://localhost:1266/Gateway.ashx";//本机测试,正式需注释此行
            url = "http://121.199.42.125:6002/DevPayTest.ashx";
            string productID = HttpUtility.UrlEncode(string.Format("{0}{1}", pRequest.Parameters.Type, new Guid(pRequest.Parameters.ObjectID).CompressTo24Chars()));
            var para = new
            {
                PayChannelID = pRequest.Parameters.PayChannelID,//	int	支付通道ID
                ProductID = productID//string	商品ID或者订单ID
            };
            var request = new
            {
                AppID = 1,
                ClientID = pRequest.CustomerID,
                Parameters = para
            };
            string json = string.Format("action=CreateWXNativePayUrl&request={0}", request.ToJSON());
            var response = JIT.Utility.Web.HttpClient.PostQueryString(url, json);
            var dic = response.DeserializeJSONTo<Dictionary<string, object>>();
            if (Convert.ToInt32(dic["ResultCode"]) < 100)
            {
                var tempdic = dic["Datas"].ToJSON().DeserializeJSONTo<Dictionary<string, string>>();
                if (tempdic.ContainsKey("NativePayUrl"))
                    rd.PayUrl = tempdic["NativePayUrl"];
            }
            else
            {
                throw new APIException("调用接口CreateWXNativePayUrl失败:" + dic["Message"]) { ErrorCode = 333 };
            }
            return rd;
        }
    }
}
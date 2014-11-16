using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using NUnit.Framework;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Order.Order
{
    [TestFixture]
    public class TestCreateWXNativePayUrlAH
    {
        string customerID = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void CreateWXNativePayUrl()
        {
            APIRequest<CreateWXNativePayUrlRP> req = new APIRequest<CreateWXNativePayUrlRP>();
            req.CustomerID = customerID;
            req.UserID = "6bc0501735ca40458de17d77a53943ce";
            req.OpenID = "6bc0501735ca40458de17d77a53943ce";
            req.Parameters.PayChannelID = 9;
            req.Parameters.ObjectID = "018924b47de04ac2839920813e6b61c9";
            req.Parameters.Type = 2;
            string queryString = string.Format("?type=Product&action=Order.Order.CreateWXNativePayUrl&req={0}", HttpUtility.UrlEncode(req.ToJSON()));
            var strRsp1 = APIClientProxy.CallAPI(queryString, string.Empty);
            Console.WriteLine(strRsp1);
        }
    }
}

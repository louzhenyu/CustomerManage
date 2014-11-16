using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.Web.ApplicationInterface;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.AppVersion
{
    [TestFixture]
    public class TestAPI
    {
        private string customerId = "6c1ce52aa43441a3a13c87b41fcafd54";
        private string vipId = "00069b0f-bdf7-4888-8080-24d8c8c9424d";
        [Test]
        public void TestGetAPPVersion()
        {
            var rpd = new AppVersionRP();
            var rp = new APIRequest<AppVersionRP>();
            rp.CustomerID = customerId;
            rp.Parameters = rpd;
            rp.Parameters.CurrentVersionNo = "2.0.5";
            rp.Parameters.Channel = 1;
            rp.Parameters.Plat = "ios";
            var rsp = APIClientProxy.CallAPI<AppVersionRP, AppVersionRD>(APITypes.Product, "GetAPPVersion", rp);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void TestAddVersionDownloadLog()
        {
            var rpd = new AppVersionDownloadLogRP();
            var rp = new APIRequest<AppVersionDownloadLogRP>();
            rp.CustomerID = customerId;
            rp.Parameters = rpd;
            rp.Parameters.Version = "2.0.5";
            rp.Parameters.Channel = 1;
            rp.Parameters.Plat = "ios";
            rp.Parameters.VipId = vipId;
            rp.Parameters.DownloadURL = "http://jlfaljfa/faljf";
            var rsp = APIClientProxy.CallAPI<AppVersionDownloadLogRP, EmptyResponseData>(APITypes.Product, "AddVersionDownLoadLog", rp);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

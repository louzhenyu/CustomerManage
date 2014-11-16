using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Request;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.AppConfig.HomePageConfig
{
    [TestFixture]
    public class TestAppConfigAH
    {
        //public string customerId = "86a575e616044da3ac2c3ab492e44445";  //逸马商学院
        //public string customerId = "1d5039568a204391a417734cccd14fa4";  //郴州生源百货
        //public string customerId = "6c1ce52aa43441a3a13c87b41fcafd54";
        //public string customerId = "e703dbedadd943abacf864531decdac1";  //泸州老窖
        //public string customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";  //Demo
        public string customerId = "13010a73777d4db2b2a84f01f6f29de0";
        [Test]
        public void TestAPI()
        {
            var HomePageRP = new HomePageConfigRP();
            var request = new APIRequest<HomePageConfigRP>();
            request.CustomerID = customerId;
            request.Parameters = HomePageRP;
            string str = "{\"Locale\":null,\"CustomerID\":\"f6a7da3d28f74f2abedfc3ea0cf65c01\",\"UserID\":\"549fc9abef5142988cb7b8228e9c34ab\",\"OpenID\":\"o8Y7EjsdyMR7Jz8XC6Ut3amcgk6E\",\"Token\":null,\"Parameters\":{}}";
            request = str.DeserializeJSONTo<APIRequest<HomePageConfigRP>>();
            var rsp = APIClientProxy.CallAPI<HomePageConfigRP, HomePageConfigRD>(APITypes.Product, "AppConfig.HomePageConfig.HomePageConfig", request);
            Assert.IsTrue(rsp.IsSuccess);
            Console.WriteLine(rsp.ToJSON());
        }


        [Test]
        public void TestSetEventItemAPI()
        {
            var rp = new SetEventItemRP();
            var request = new APIRequest<SetEventItemRP>();
            request.CustomerID = customerId;
            request.Parameters = rp;
            var rsp = APIClientProxy.CallAPI<SetEventItemRP, SetEventItemRD>(APITypes.Product, "AppConfig.HomePageConfig.SetEventItem", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSetEventAdAPI()
        {
            var rp = new SetEventAdRP();
            var request = new APIRequest<SetEventAdRP>();
            request.CustomerID = customerId;
            request.Parameters = rp;
            rp.OperateType = 1;
            Guid homeId = new Guid("D6350CAB-C3FA-4F16-846E-39103C5AC3F1");
            Guid adAreaId = Guid.NewGuid();
            rp.AdAreaInfoPara = new AdAreaInfoPara[] { };


            List<AdAreaInfoPara> para = new List<AdAreaInfoPara>();


            rp.AdAreaInfoPara[0].AdAreaId = adAreaId;
            rp.AdAreaInfoPara[0].HomeID = homeId;
            rp.AdAreaInfoPara[0].ImageUrl = "xxx";
            rp.AdAreaInfoPara[0].ObjectID = "xxx";
            rp.AdAreaInfoPara[0].ObjectTypeID = 2;
            rp.AdAreaInfoPara[0].DisplayIndex = 1;
            rp.AdAreaInfoPara[0].Url = "xxxx";

            var rsp = APIClientProxy.CallAPI<SetEventAdRP, SetEventAdRD>(APITypes.Product, "AppConfig.HomePageConfig.SetEventAd", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

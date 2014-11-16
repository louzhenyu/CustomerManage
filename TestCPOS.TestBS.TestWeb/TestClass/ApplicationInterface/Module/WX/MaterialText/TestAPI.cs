using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.MaterialText
{
    [TestFixture]
    public class TestAPI
    {
        private const string CustomerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void TestGetMaterialTextTypeList()
        {
            var rp = new GetMaterialTextTypeListRP();
            //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<GetMaterialTextTypeListRP> { CustomerID = CustomerId };


            var rsp = APIClientProxy.CallAPI<GetMaterialTextTypeListRP, GetMaterialTextTypeListRD>(APITypes.Product,
                "WX.MaterialText.GetMaterialTextTypeList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSetMaterialText()
        {
            var paras = new List<object> { };
            paras.Add(new { Key = "eventId", Value = "" });
            var rp = new SetMaterialTextRP()
            {
                MaterialText = new MaterialTextInfo()
                    {
                        Title = "aaa",
                        ImageUrl = "aaaa",
                        ApplicationId = "386D08D106C849A9ACAA6E493D23E853",
                        PageID = new Guid("900209A3-791F-48DC-9D29-D9B8954A974B"),
                        PageParamJson = paras.ToJSON()
                    }
            };
            var request = new APIRequest<SetMaterialTextRP>() { CustomerID = CustomerId };
            request.Parameters = rp;
            var reqstr = "{\"Locale\":null,\"CustomerID\":\"e703dbedadd943abacf864531decdac1\",\"UserID\":null,\"OpenID\":null,\"Token\":null,\"Parameters\":{\"MaterialText\":{\"UnionTypeId\":\"3\",\"TextId\":\"\",\"Title\":\"sfsfdsf\",\"Author\":\"\",\"ImageUrl\":\"http://dev.o2omarketing.cn:9004/Framework/Javascript/Other/kindeditor/asp.net/../attached/image/20140508/Thumb20140508181530_1536.jpg\",\"OriginalUrl\":null,\"Text\":null,\"ApplicationId\":\"386D08D106C849A9ACAA6E493D23E853\",\"TypeId\":\"D76030FDBA7140B89F804D68EDC3AE38\",\"PageId\":\"fcb5d3845146461eaadfa0ebce2b9cfc\",\"ModuleName\":\"微官网\",\"PageParamJson\":\"[{}]\"}}}";
            reqstr = "{\"Parameters\":{\"MaterialText\":{\"UnionTypeId\":\"3\",\"TextId\":\"\",\"Title\":\"we sdf\",\"Author\":\"\",\"ImageUrl\":\"/Framework/Javascript/Other/kindeditor/asp.net/../attached/image/20140512/Thumb20140512121035_9317.jpg\",\"OriginalUrl\":null,\"Text\":null,\"ApplicationId\":\"386D08D106C849A9ACAA6E493D23E853\",\"TypeId\":\"D76030FDBA7140B89F804D68EDC3AE38\",\"PageId\":\"9aff5764ab2c4dd28fd05d158bb3719a\",\"ModuleName\":\"活动列表\",\"PageParamJson\":\"[{\\\"key\\\":\\\"eventType\\\",\\\"value\\\":\\\"1AB5D938-319E-4EC3-BB1F-7A4C55EBAD07\\\"},{\\\"key\\\":\\\"pageModule\\\",\\\"value\\\":{\\\"PageID\\\":\\\"9aff5764ab2c4dd28fd05d158bb3719a\\\",\\\"ModuleName\\\":\\\"活动列表\\\",\\\"PageCode\\\":\\\"EventList\\\",\\\"URLTemplate\\\":\\\"/HtmlApps/html/_pageName_?eventType={eventType}\\\"}},{\\\"key\\\":\\\"pageType\\\",\\\"value\\\":{\\\"EventTypeId\\\":\\\"1AB5D938-319E-4EC3-BB1F-7A4C55EBAD07\\\",\\\"EventTypeName\\\":\\\"体育爱好\\\"}}]\"}},\"CustomerId\":\"e703dbedadd943abacf864531decdac1\"}";
            request = reqstr.DeserializeJSONTo<APIRequest<SetMaterialTextRP>>();
            var rsp = APIClientProxy.CallAPI<SetMaterialTextRP, SetMaterialTextRD>(APITypes.Product, "WX.MaterialText.SetMaterialText", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetMaterialTextList()
        {
            var rp = new GetMaterialTextListRP();
            var request = new APIRequest<GetMaterialTextListRP> { CustomerID = CustomerId, Parameters = rp };

            var requestStr = "{\"Parameters\":{\"MaterialTextId\":\"940F9A908BAC43F9BE96EE2BA9D8B448\",\"PageSize\":1,\"PageIndex\":0},\"CustomerId\":\"e703dbedadd943abacf864531decdac1\"}";
            request = requestStr.DeserializeJSONTo<APIRequest<GetMaterialTextListRP>>();
            var rsp = APIClientProxy.CallAPI<GetMaterialTextListRP, GetMaterialTextListRD>(APITypes.Product,
                "WX.MaterialText.GetMaterialTextList", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

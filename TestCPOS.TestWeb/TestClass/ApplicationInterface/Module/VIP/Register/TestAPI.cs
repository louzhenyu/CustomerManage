using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using NUnit.Framework;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.Register
{
    [TestFixture]
    public class TestAPI
    {
        string customerId = "92a251898d63474f96b2145fcee2860c";
        [Test]
        public void TestPageList()
        {
            var RP = new GetRegisterFormItemsRP();
            //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<GetRegisterFormItemsRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            request.Parameters.EventCode = "Online005";
            var rsp = APIClientProxy.CallAPI<GetRegisterFormItemsRP, GetRegisterFormItemsRD>(APITypes.Product, "VIP.Register.GetRegisterFormItems", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void TestSetRegisterFormItems()
        {
            var RP = new SetRegisterFormItemsRP();
            //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<SetRegisterFormItemsRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;

            CommitFormItemInfo[] commitFormItemInfo = new CommitFormItemInfo[]
                {
                    new CommitFormItemInfo(){ ID= "8754A2B9-F823-4DDC-8808-0798BFFA887F",IsMustDo = true,Value = "1871122"},
                    new CommitFormItemInfo(){ ID= "8F8CBDD0-E8E8-46E1-AF63-153E4B212012",IsMustDo = true,Value = "xxx"}
                };
            RP.ItemList = commitFormItemInfo;

            request = "{\"Locale\":null,\"CustomerID\":\"8c6979db4acb4dd3909f5be67f433e67\",\"UserID\":\"e025b4bf885c4b7d8ecfc3373557cb20\",\"OpenID\":null,\"Token\":null,\"Parameters\":{\"ItemList\":[{\"ID\":\"791923cc-d94f-4bfe-b6f1-5c982975ad88\",\"IsMustDo\":false,\"Value\":\"阿萨德\"},{\"ID\":\"daf7bcdf-d904-4d05-854c-5fadbea42f0f\",\"IsMustDo\":false,\"Value\":\"阿萨德\"},{\"ID\":\"acacc0ef-6a97-43d6-bf7f-6c5e57ede5a3\",\"IsMustDo\":false,\"Value\":\"再不行弄死你\"},{\"ID\":\"1d5f307c-d07a-4776-acee-a81ed8f99e32\",\"IsMustDo\":false,\"Value\":\"实得分\"},{\"ID\":\"26dba0fa-71b2-4913-b8cc-c16e751e7d45\",\"IsMustDo\":false,\"Value\":\"丰盛的\"},{\"ID\":\"4c5adfa8-5f15-437b-97a5-ebbd6228529f\",\"IsMustDo\":false,\"Value\":\"1\"}],\"VipSource\":3}}".DeserializeJSONTo<APIRequest<SetRegisterFormItemsRP>>();
            var rsp = APIClientProxy.CallAPI<SetRegisterFormItemsRP, SetRegisterFormItemsRD>(APITypes.Product, "VIP.Register.SetRegisterFormItems", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestMergeVipInfo()
        {
            var req = HttpUtility.UrlDecode("%7B%22Locale%22%3Anull%2C%22CustomerID%22%3A%2257374a5f53f54191a728810618ec8276%22%2C%22UserID%22%3A%228f5595ac7c3541f39be5e7c6ff415d04%22%2C%22OpenID%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22Token%22%3Anull%2C%22Parameters%22%3A%7B%22Mobile%22%3A%2218616153083%22%2C%22AuthCode%22%3A%22%22%7D%7D").DeserializeJSONTo<APIRequest<MergeVipInfoRP>>();
            var rsp = APIClientProxy.CallAPI<MergeVipInfoRP, MergeVipInfoRD>(APITypes.Product, "VIP.Register.MergeVipInfo", req);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

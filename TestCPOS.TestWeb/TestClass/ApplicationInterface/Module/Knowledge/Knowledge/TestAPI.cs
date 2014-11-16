using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Request;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Knowledge.Knowledge
{
    [TestFixture]
    public class TestAPI
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void Search()
        {
            var RP = new SearchRP();
            RP.Key = "1";
            var request = new APIRequest<SearchRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            string str = "{\"Locale\":null,\"CustomerID\":\"e703dbedadd943abacf864531decdac1\",\"UserID\":\"8f3b4eb9733e45bc9982a86d81302048\",\"OpenID\":\"oxbbcjvo1rNFZq5s4GfbgBXEKOdc\",\"Token\":null,\"Parameters\":{\"Key\":\"\",\"Type\":\"\",\"PageSize\":9,\"PageIndex\":1}}";
            request = str.DeserializeJSONTo<APIRequest<SearchRP>>();
            var rsp = APIClientProxy.CallAPI<SearchRP, SearchRD>(APITypes.Product, "Knowledge.Knowledge.Search", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void GetKnowledgeDetail()
        {
            var RP = new GetKnowledgeDetailRP();
            RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<GetKnowledgeDetailRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetKnowledgeDetailRP, GetKnowledgeDetailRD>(APITypes.Product, "Knowledge.Knowledge.GetKnowledgeDetail", request);
            Console.WriteLine(rsp.ToJSON());
        }

   
        [Test]
        public void TreadKnowledge()
        {
            var RP = new TreadKnowledgeRP();
            RP.ID = Guid.Parse("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<TreadKnowledgeRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<TreadKnowledgeRP, TreadKnowledgeRD>(APITypes.Product, "Knowledge.Knowledge.TreadKnowledge", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void KeepKnowledge()
        {
            var RP = new KeepKnowledgeRP();
            RP.ID = Guid.Parse("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<KeepKnowledgeRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<KeepKnowledgeRP, KeepKnowledgeRD>(APITypes.Product, "Knowledge.Knowledge.KeepKnowledge", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void PraiseKnowledge()
        {
            var RP = new PraiseKnowledgeRP();
            RP.ID = Guid.Parse("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<PraiseKnowledgeRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<PraiseKnowledgeRP, PraiseKnowledgeRD>(APITypes.Product, "Knowledge.Knowledge.PraiseKnowledge", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

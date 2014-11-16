using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Request;
using JIT.CPOS.DTO.Module.WeiXin.KeyWord.Response;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.Keyword
{
    [TestFixture]
    public class TestAPI
    {
        private const string CustomerId = "e703dbedadd943abacf864531decdac1";

        [Test]
        public void TestSearchKeyWord()
        {
            var rp = new SearchKeyWordRP { ApplicationId = "386D08D106C849A9ACAA6E493D23E853" };

            var request = new APIRequest<SearchKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<SearchKeyWordRP, SearchKeyWordRD>(APITypes.Product, "WX.KeyWord.SearchKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSetKeyWord()
        {
            var keywordList = new KeyWordInfo();
            keywordList.ReplyId = "101E5AFA0D4E4CA9B738323A4B108357";
            keywordList.KeyWord = "团购test";
            keywordList.BeLinkedType = 2;
            keywordList.ApplicationId = "386D08D106C849A9ACAA6E493D23E853";
            keywordList.ReplyType = 1;
            keywordList.DisplayIndex = 10;
            keywordList.Text = "xxxxx";

            var rp = new SetKeyWordRP {  };
            rp.KeyWordList = keywordList;
            var request = new APIRequest<SetKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<SetKeyWordRP, SetKeyWordRD>(APITypes.Product, "WX.KeyWord.SetKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSetKeyWord1()
        {
            var textidInfo = new List<MaterialTextIdInfo>
            {
                new MaterialTextIdInfo() {TestId = "009C41874D634C888547C9EA35F1ADCA"},
                new MaterialTextIdInfo() {TestId = "08CCF3FF2C2E4884B3839EEC2AA4B2BC"}
            };
            var keywordList = new KeyWordInfo
            {
                ReplyId = "101E5AFA0D4E4CA9B738323A4B108357",
                KeyWord = "团购test",
                BeLinkedType = 2,
                ApplicationId = "386D08D106C849A9ACAA6E493D23E853",
                ReplyType = 3,
                DisplayIndex = 10,
                MaterialTextIds = textidInfo.ToArray()
            };


            var rp = new SetKeyWordRP { };
            rp.KeyWordList = keywordList;
            var request = new APIRequest<SetKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<SetKeyWordRP, SetKeyWordRD>(APITypes.Product, "WX.KeyWord.SetKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void TestSetKeyWord2()
        {
            var textidInfo = new List<MaterialTextIdInfo>
            {
                new MaterialTextIdInfo() {TestId = "009C41874D634C888547C9EA35F1ADCA"},
                new MaterialTextIdInfo() {TestId = "08CCF3FF2C2E4884B3839EEC2AA4B2BC"}
            };
            var keywordList = new KeyWordInfo
            {
                KeyWord = "团购test",
                BeLinkedType = 2,
                ApplicationId = "386D08D106C849A9ACAA6E493D23E853",
                ReplyType = 3,
                DisplayIndex = 10,
                MaterialTextIds = textidInfo.ToArray()
            };
            

            var rp = new SetKeyWordRP { };
            rp.KeyWordList = keywordList;

            var request = new APIRequest<SetKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<SetKeyWordRP, SetKeyWordRD>(APITypes.Product, "WX.KeyWord.SetKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }


        [Test]
        public void TestDeleteKeyWord()
        {
            var rp = new DeleteKeyWordRP { ReplyId = "4B1E70E278004497AE292C15FC3440D2" };

            var request = new APIRequest<DeleteKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<DeleteKeyWordRP, DeleteKeyWordRD>(APITypes.Product, "WX.KeyWord.DeleteKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetKeyWord()
        {
            var rp = new GetKeyWordRP { ReplyId = "4B1E70E278004497AE292C15FC3440D2" };

            var request = new APIRequest<GetKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetKeyWordRP, GetKeyWordRD>(APITypes.Product, "WX.KeyWord.GetKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }



        [Test]
        public void TestGetDefaultKeyWord()
        {
            var rp = new GetDefaultKeyWordRP { ApplicationId = "386D08D106C849A9ACAA6E493D23E853", KeywordType = 2};

            var request = new APIRequest<GetDefaultKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetDefaultKeyWordRP, GetDefaultKeyWordRD>(APITypes.Product, "WX.KeyWord.GetDefaultKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void TestGetDefaultKeyWord1()
        {
            var rp = new GetDefaultKeyWordRP { ApplicationId = "386D08D106C849A9ACAA6E493D23E853", KeywordType = 3 };

            var request = new APIRequest<GetDefaultKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetDefaultKeyWordRP, GetDefaultKeyWordRD>(APITypes.Product, "WX.KeyWord.GetDefaultKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSaveDefaultKeyWord()
        {
            var textidInfo = new List<MaterialTextIdInfo>
            {
                new MaterialTextIdInfo() {TestId = "009C41874D634C888547C9EA35F1ADCA"},
                new MaterialTextIdInfo() {TestId = "08CCF3FF2C2E4884B3839EEC2AA4B2BC"}
            };
            var keywordList = new KeyWordInfo
            {
                ReplyId = "265B1657733C4DF38C9DB567CBA1105B",
                KeyWord = "分享团购test",
                BeLinkedType = 2,
                ApplicationId = "386D08D106C849A9ACAA6E493D23E853",
                ReplyType = 3,
                DisplayIndex = 10,
                KeywordType = 2,
                MaterialTextIds = textidInfo.ToArray()
            };


            var rp = new SetKeyWordRP { };
            rp.KeyWordList = keywordList;
            var request = new APIRequest<SetKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<SetKeyWordRP, SetKeyWordRD>(APITypes.Product, "WX.KeyWord.SaveDefaultKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSaveDefaultKeyWord1()
        {
            var textidInfo = new List<MaterialTextIdInfo>
            {
                new MaterialTextIdInfo() {TestId = "009C41874D634C888547C9EA35F1ADCA"},
                new MaterialTextIdInfo() {TestId = "08CCF3FF2C2E4884B3839EEC2AA4B2BC"}
            };
            var keywordList = new KeyWordInfo
            {
                KeyWord = "xxxxtest",
                BeLinkedType = 2,
                ApplicationId = "386D08D106C849A9ACAA6E493D23E853",
                ReplyType = 1,
                DisplayIndex = 10,
                KeywordType = 3,
                MaterialTextIds = textidInfo.ToArray()
            };


            var rp = new SetKeyWordRP { };
            rp.KeyWordList = keywordList;
            var request = new APIRequest<SetKeyWordRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<SetKeyWordRP, SetKeyWordRD>(APITypes.Product, "WX.KeyWord.SaveDefaultKeyWord",
                request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

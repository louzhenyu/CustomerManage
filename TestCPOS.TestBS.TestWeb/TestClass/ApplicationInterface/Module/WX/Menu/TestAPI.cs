using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Request;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.Menu
{
    [TestFixture]
    public class TestAPI
    {
        private const string CustomerId = "e703dbedadd943abacf864531decdac1";

        [Test]
        public void GetMenuList()
        {
            var rp = new GetMenuListRP { ApplicationId = "386D08D106C849A9ACAA6E493D23E853" };

            var request = new APIRequest<GetMenuListRP> {CustomerID = CustomerId, Parameters = rp};

            var rsp = APIClientProxy.CallAPI<GetMenuListRP, GetMenuListRD>(APITypes.Product, "WX.Menu.GetMenuList",
                request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetMenuDetail()
        {
            var rp = new GetMenuDetailRP { MenuId = "A899BC366FA34A608D5C60592A84FB23" };

            var request = new APIRequest<GetMenuDetailRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetMenuDetailRP, GetMenuDetailRD>(APITypes.Product, "WX.Menu.GetMenuDetail",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSetMenuList()
        {
            var paras = new List<object> {new {Key = "eventId", Value = "adjkfjoioajfkljekloofa"}};

            var rp = new SetMenuRP
            {
                MenuId = "CD57B6CBAC8044E293FD7F0772A51B8C",
                DisplayColumn = "1",
                Name = "我的战绩",
                Level = 2,
                ApplicationId = "386D08D106C849A9ACAA6E493D23E853",
                ImageUrl = "xxx",
                ParentId = "D13D8BFBF77F40748E95A702FD4DECE9",
                MenuUrl = "http://www.baidu.com",
                MessageType = "1",
                Status = 1,
                Text = "xxx",
                PageParamJson = paras.ToJSON(),
                UnionTypeId = 1,
                MaterialTextIds = null,
                PageId = new Guid("9AFF5764-AB2C-4DD2-8FD0-5D158BB3719A")
            };

            var request = new APIRequest<SetMenuRP> {CustomerID = CustomerId, Parameters = rp};

            var rsp = APIClientProxy.CallAPI<SetMenuRP, SetMenuRD>(APITypes.Product, "WX.Menu.SetMenu",
                request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestDeleteMenu()
        {
            var rp = new DeleteMenuRP { MenuId = "FF478255E101478AA498910C500BF073" };

            var request = new APIRequest<DeleteMenuRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<DeleteMenuRP, DeleteMenuRD>(APITypes.Product, "WX.Menu.DeleteMenu",
                request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

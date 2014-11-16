using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Request;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Response;
using JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.UnitAndItem.Unit
{
    [TestFixture]
    public class TestSearchStoreListAH
    {
        public string customerId = "6c1ce52aa43441a3a13c87b41fcafd54";
        //public string customerId = "2582cf87839a414584294f57d3fccfbe";
        [Test]
        public void SearchStoreListAH()
        {
            //var RP = new SearchStoreListRP();
            //RP.CityCode = "-00";
            //RP.Position = "121.473704,31.230393";
            //var request = new APIRequest<SearchStoreListRP>();
            //string str = "{\"Locale\":\"ch\",\"CustomerID\":\"2582cf87839a414584294f57d3fccfbe\",\"UserID\":\"add73ef71c2c480c89b5a6941cb0dfc9\",\"OpenID\":\"ofUHqjgSme_qaNQN0oohrM7kX_Ck\",\"Token\":null,\"Parameters\":{\"type\":\"Product\",\"NameLike\":\"\",\"CityName\":\"附近\",\"Position\":\"31.230393,121.473704\",\"PageIndex\":1,\"PageSize\":99,\"StoreID\":\"\",\"IncludeHQ\":\"\"}}";
            //request = str.DeserializeJSONTo <APIRequest<SearchStoreListRP>>();
            //request.CustomerID = customerId;
            //request.Parameters = RP;
            //var rsp = APIClientProxy.CallAPI<SearchStoreListRP, SearchStoreListRD>(DTO.ValueObject.APITypes.Product, "UnitAndItem.Unit.SearchStoreList", request);
            //Console.WriteLine(rsp.ToJSON());     

            var RP = new SearchStoreListRP();
            RP.CityCode = "-00";
            RP.Position = "121.441240,31.227995";
            RP.PageIndex = 0;
            RP.PageSize = 15;
            var request = new APIRequest<SearchStoreListRP>();
           // request.CustomerID = "29E11BDC6DAC439896958CC6866FF64E";
            request.CustomerID = "e703dbedadd943abacf864531decdac1";
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<SearchStoreListRP, SearchStoreListRD>(DTO.ValueObject.APITypes.Product, "UnitAndItem.Unit.SearchStoreList", request);
            Console.WriteLine(rsp.ToJSON());  
        }
        [Test]
        public void GetDisplaynoneAH()
        {
            var RP = new GetDisplaynoneRP();
            var request = new APIRequest<GetDisplaynoneRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            request.CustomerID = "e703dbedadd943abacf864531decdac1";
            var rsp = APIClientProxy.CallAPI<GetDisplaynoneRP, GetDisplaynoneRD>(DTO.ValueObject.APITypes.Product, "UnitAndItem.Unit.GetDisplaynone", request);
            Console.WriteLine(rsp.ToJSON());     
        }
        [Test]
        public void getItemDetail()
        {
            var Rp = new Item.getItemDetailRP();
            Rp.itemId = "C30EE1A174BB4103BAC652F574FA96CD";
            Rp.Lng = "121.441240";
            Rp.Dim = "31.227995";
            var request = new APIRequest<Item.getItemDetailRP>();
            request.Parameters = Rp;
            request.CustomerID = "3f3b439e206145148e8f5f74724f83e5";  //格力
            var rsp = APIClientProxy.CallAPI<Item.getItemDetailRP, Item.getItemDetailRespData>(DTO.ValueObject.APITypes.Product, "getItemDetail", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetItemCategoryList()
        {
            var request = new APIRequest<EmptyRequestParameter>();
            request.CustomerID = "e703dbedadd943abacf864531decdac1";  //老窖
            var rsp = APIClientProxy.CallAPI<EmptyRequestParameter, JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item.GetItemCategoryListRD>(DTO.ValueObject.APITypes.Product, "GetItemCategoryList", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetItemCategoryByParentId()
        {
            var request = new APIRequest<GetCategoryByParentIdRP>();
            request.CustomerID = "e703dbedadd943abacf864531decdac1";  //老窖
            request.Parameters.ParentID = "27516921-0BDD-43EC-B709-EABEB386ADA2"; 
            var rsp = APIClientProxy.CallAPI<JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item.GetCategoryByParentIdRP, JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item.GetItemCategoryListRD>(DTO.ValueObject.APITypes.Product, "GetItemCategoryByParentId", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Web.ApplicationInterface.Vip;
using NUnit.Framework;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request;
using JIT.CPOS.DTO.Module.WeiXin.Module.Request;
using JIT.CPOS.DTO.Module.WeiXin.Module.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.Entity;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.Module
{
    [TestFixture]
    public class TestAPI
    {
        private const string customerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void TestSetMaterialText()
        {
            var paras = new List<object> { };
            paras.Add(new { Key = "eventId", Value = "" });
            var rp = new GetSysModuleListRP();
            var request = new APIRequest<GetSysModuleListRP>() { CustomerID = customerId };
            request.Parameters = rp;
            var rsp = APIClientProxy.CallAPI<GetSysModuleListRP, GetSysModuleListRD>(APITypes.Product, "WX.Module.SetMaterialText", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]  
        public void GetCustomerPageSetting()  //获取客户化配置
        {
            var rp = new GetCustomerPageSettingRP { PageKey = "MyOrder" };
            var request = new APIRequest<GetCustomerPageSettingRP> { CustomerID = customerId, Parameters = rp };
            var rsp = APIClientProxy.CallAPI<GetCustomerPageSettingRP, GetCustomerPageSettingRD>(APITypes.Product, "WX.SysPage.GetCustomerPageSetting",request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]  
        public void SetCustomerPageSetting()  //设置客户化配置
        {
            var rp = new SetCustomerPageSettingRP();
            rp.MappingId = "f3e9e8ee-000a-4bc0-bf9c-6a33bbcc94e5";
            rp.PageKey = "$HomeIndex";
            rp.Node = new string[] { "1", "2", "3" };
            rp.NodeValue = new string[] { "微官网", "1", "{\"title\":\"微官网\",\"logo\":\"../../../images/public/cars_default/logo.jpg\",\"backgroundImage\":\"../../../images/public/cars_default/bg.jpg\",\"animateDirection\":\"up\",\"links\":\"[{\\\"title\\\":\\\"入口111\\\",\\\"english\\\":\\\"Entry\\\",\\\"toUrl\\\":\\\"www.jitmarketing.cn\\\"},{\\\"title\\\":\\\"入口\\\",\\\"english\\\":\\\"Entry\\\",\\\"toUrl\\\":\\\"www.jitmarketing.cn\\\"},{\\\"title\\\":\\\"入口\\\",\\\"english\\\":\\\"Entry\\\",\\\"toUrl\\\":\\\"www.jitmarketing.cn\\\"},{\\\"title\\\":\\\"入口\\\",\\\"english\\\":\\\"Entry\\\",\\\"toUrl\\\":\\\"www.jitmarketing.cn\\\"},{\\\"title\\\":\\\"入口\\\",\\\"english\\\":\\\"Entry\\\",\\\"toUrl\\\":\\\"www.jitmarketing.cn\\\"}]\"}" };
            var request = new APIRequest<SetCustomerPageSettingRP> { CustomerID = customerId,UserID="1", Parameters = rp };
            var rsp = APIClientProxy.CallAPI<SetCustomerPageSettingRP,EmptyResponseData>(APITypes.Product, "WX.SysPage.SetCustomerPageSetting", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test] //获取模板页列表
        public void GetCustomerPageList()
        {
            GetSysPageListRP RP = new GetSysPageListRP();
            //RP.Key = "EventList";
            RP.PageIndex = 0;
            RP.PageSize = 2;
            var request = new APIRequest<GetSysPageListRP>() { CustomerID = "7ba0d0bc2c13403892deb6499d2c7266" };
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetSysPageListRP, GetSysPageListRD>(APITypes.Product, "WX.SysPage.GetCustomerPageList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test] //生成客户Config文件
        public void CreateCustomerConfig()
        {
            CreateCustomerConfigRP RP = new CreateCustomerConfigRP();
            var request = new APIRequest<CreateCustomerConfigRP>() { CustomerID = "7ba0d0bc2c13403892deb6499d2c7266" };
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<CreateCustomerConfigRP, CreateCustomerConfigRD>(APITypes.Product, "WX.SysPage.CreateCustomerConfig", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetVipTotal()
        {
            EmptyRequestParameter RP = new EmptyRequestParameter();
            var request = new APIRequest<EmptyRequestParameter>();// { CustomerID = customerId };
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<EmptyRequestParameter, GetVipTotalRD>(APITypes.Product, "GetVipTotal", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetVipInfoList()
        {
            var pr = new VipSearchListRP();
            pr.PageIndex = 1;
            pr.PageSize = 10;
            pr.OrderBy = "VipName";
            pr.SortType = "ASC";
            var searchColumns = new List<SearchColumn>();
            //searchColumns.Add(new SearchColumn { ColumnName = "VipRealName", ColumnValue1 = "王", ControlType = 1 });
            //searchColumns.Add(new SearchColumn { ColumnName = "CouponInfo", ColumnValue1 = "559530955d704a54a5121e5fe588f78d", ControlType = 205 });
            //searchColumns.Add(new SearchColumn { ColumnName = "VipSourceId", ColumnValue1 = "3", ControlType = 5 });
            //searchColumns.Add(new SearchColumn { ColumnName = "Phone", ColumnValue1 = "189", ControlType = 7 });
            //searchColumns.Add(new SearchColumn { ColumnName = "VipLevel", ColumnValue1 = "2", ControlType = 5 });
            //searchColumns.Add(new SearchColumn { ColumnName = "Birthday", ColumnValue1 = "1973-01-23", ColumnValue2 = "1980-06-01", ControlType = 6 });
            searchColumns.Add(new SearchColumn { ColumnName = "Age",  ColumnValue1 = "30", ControlType = 2 });
            pr.SearchColumns = searchColumns.ToArray();
            var tags = new List<VipSearchTag>();
            //tags.Add(new VipSearchTag { LeftBracket="(", EqualFlag = "include", TagId = "4F15D3B2C3C94402A45F5F6F18891943", AndOrStr = "or", RightBracket = "" });
            //tags.Add(new VipSearchTag { LeftBracket="", EqualFlag = "include", TagId = "D3A9CAB819444E17B5B97BF3068133C3", AndOrStr = "", RightBracket = ")" });
            pr.VipSearchTags = tags.ToArray();
            var request = new APIRequest<VipSearchListRP>() { CustomerID = "e703dbedadd943abacf864531decdac1", UserID = "7c292994c45143028cbf0b60c9555aec" };
            request.Parameters = pr;
            var rsp = APIClientProxy.CallAPI<VipSearchListRP, VipSearchListRD>(APITypes.Product, "GetVipInfoList", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetExistVipInfo()
        {
            var pr = new GetVipDetailInfoRP();
            pr.VipId = "0025F661-B03A-4341-8123-51833F8D25A8";
            var request = new APIRequest<GetVipDetailInfoRP>() { CustomerID = "e703dbedadd943abacf864531decdac1", UserID = "B87FBC7A6D664F67B65F9AD747C5E5DD" };
            request.Parameters = pr;
            var rsp = APIClientProxy.CallAPI<GetVipDetailInfoRP,GetExistVipInfoRD>(APITypes.Product, "GetExistVipInfo", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void AddVip()
        {
            var pr = new VipEntityRP();
            var searchColumns = new List<SearchColumn>();
            searchColumns.Add(new SearchColumn() { ColumnName = "VipName", ColumnValue1 = "EthanTest", ControlType = 1 });
            searchColumns.Add(new SearchColumn() { ColumnName = "VipCode", ColumnValue1 = "Vip-EthanTest", ControlType = 1 });
            searchColumns.Add(new SearchColumn() { ColumnName = "Gender", ColumnValue1 = "2", ControlType = 2 });
            pr.Columns = searchColumns.ToArray();
            var request = new APIRequest<VipEntityRP>() { CustomerID = "e703dbedadd943abacf864531decdac1", UserID = "B87FBC7A6D664F67B65F9AD747C5E5DD" };
            request.Parameters = pr;
            var rsp = APIClientProxy.CallAPI<VipEntityRP, EmptyResponseData>(APITypes.Product, "AddVip", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void UpdateVipInfo()
        {
            var pr = new VipEntityRP();
            pr.VipId = "003e30e7121741beb749a230eebecfe0";
            var columns = new List<SearchColumn>();
            columns.Add(new SearchColumn() { ColumnName = "VipName", ColumnValue1 = "EthanTestUpate", ControlType = 1 });
            columns.Add(new SearchColumn() { ColumnName = "Qq", ColumnValue1 = "13434515", ControlType = 1 });
            pr.Columns = columns.ToArray();
            var request = new APIRequest<VipEntityRP>() { CustomerID = "e703dbedadd943abacf864531decdac1", UserID = "B87FBC7A6D664F67B65F9AD747C5E5DD" };
            request.Parameters = pr;
            var rsp = APIClientProxy.CallAPI<VipEntityRP, EmptyResponseData>(APITypes.Product, "UpdateVipInfo", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetVipOrderList()
        {
            var pr = new GetVipDetailInfoRP();
            pr.PageIndex = 1;
            pr.PageSize = 10;
            pr.VipId = "000516ED-2272-4696-A8FE-1B4962300AA4";
            var request = new APIRequest<GetVipDetailInfoRP>{ CustomerID = "e703dbedadd943abacf864531decdac1", UserID = "B87FBC7A6D664F67B65F9AD747C5E5DD" };
            request.Parameters = pr;
            var rsp = APIClientProxy.CallAPI<GetVipDetailInfoRP, VipOrderInfoRD>(APITypes.Product, "GetVipOrderList", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void GetVipDetail()
        {
            var pr = new GetVipDetailInfoRP();
            pr.VipId = "9f09e98214ed4f17added11c13a8bd7f";
            var request = new APIRequest<GetVipDetailInfoRP> { CustomerID = "e703dbedadd943abacf864531decdac1", UserID = "B87FBC7A6D664F67B65F9AD747C5E5DD" };
            request.Parameters = pr;
            var rsp = APIClientProxy.CallAPI<GetVipDetailInfoRP, GetVipDetailInfoRD>(APITypes.Product, "GetVipDetail", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

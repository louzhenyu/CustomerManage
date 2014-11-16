
/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/09 14:43
 * Description	:设置模块页
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.WX.SysPage
{
    [TestFixture]
    public class TestAPI
    {
       // public string customerId = "e703dbedadd943abacf864531decdac1";
        #region 构造函数
        public TestAPI()
        { 
        
        }
        #endregion
        [Test]   // 设置模块页 "pageKey":"JiFenShop",     "pageDes":"积分商城页面",     "htmls":[         {             "id":1,             "path":"public/jifen/jifen_shop.html",             "css":[                 "base/global","public/jifen/style_jifen"             ],             "des":"积分商城皮肤一"         }     ],     "defaultHtml":1,     "isEntry":1,     "pageCode":"Default",     "NeedAuth":1,     "customerId":"null",     "title":"积分商城",     "plugin":[         "mustache"     ],     "script":[         "public/jifen/jifen_shop"     ],     "urlTemplete":"/HtmlApps/html/_pageName_",     "params":[     ] 
        public void SetSysPageAH()
        {
            SetSysPageRP RP = new SetSysPageRP();
            
           //// RP.PageId=Guid.Parse("697F68C6-B039-4760-86E1-1DFDD0AAB41B"); //微信商城首页
           // RP.Author = "SYSTestAdd";
           // RP.PageJson = "{      \"pageKey\":\"IndexShop\",      \"pageDes\":\"微商城首页\",      \"htmls\":[          {              \"id\":1,              \"path\":\"public/index/index_shop.html\",              \"css\":[                  \"base/global\",\"public/index/style\"              ],              \"des\":\"微商城首页皮肤一\"          }      ],      \"defaultHtml\":1,      \"isEntry\":1,      \"pageCode\":\"Default\",      \"NeedAuth\":1,      \"customerId\":\"null\",      \"title\":\"会员卡\",      \"plugin\":[          \"mustache\",\"iscroll\"      ],      \"script\":[          \"public/index/index_shop\"      ],      \"urlTemplete\":\"/HtmlApps/html/_pageName_\",      \"params\":[      ]  }  ";
           // RP.Version="1.0.1";
            //RP.ModuleName = "微商城首页TestAdd";

            RP.PageId = Guid.Parse("99C3B0C3-F78A-4B54-AD2E-07C7D2AD34D4");//积分商城
            RP.PageJson = "{     \"pageKey\":\"JiFenShop\",     \"pageDes\":\"积分商城页面\",     \"htmls\":[         {             \"id\":1,             \"path\":\"public/jifen/jifen_shop.html\",             \"css\":[                 \"base/global\",\"public/jifen/style_jifen\"             ],             \"des\":\"积分商城皮肤一\"         }     ],     \"defaultHtml\":1,     \"isEntry\":1,     \"pageCode\":\"Default\",     \"NeedAuth\":1,     \"customerId\":\"null\",     \"title\":\"积分商城\",     \"plugin\":[         \"mustache\"     ],     \"script\":[         \"public/jifen/jifen_shop\"     ],     \"urlTemplete\":\"/HtmlApps/html/_pageName_\",     \"params\":[     ] }";
            var request = new APIRequest<SetSysPageRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<SetSysPageRP, SetSysPageRD>(APITypes.Product, "WX.SysPage.SetSysPage", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test] //获取模板页列表
        public void GetSysPageListAH()
        {
            GetSysPageListRP RP = new GetSysPageListRP();
            RP.Key = "";
            RP.Name = "前";
            //RP.PageIndex = 0;
            //RP.PageSize = 2;
            var request = new APIRequest<GetSysPageListRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetSysPageListRP, GetSysPageListRD>(APITypes.Product, "WX.SysPage.GetSysPageList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test] //获取模板页明细
        public void GetSysPageDetailAH()
        {
            GetSysPageDetailRP RP = new GetSysPageDetailRP();
            RP.PageId = "99C3B0C3-F78A-4B54-AD2E-07C7D2AD34D4"; //积分商城
            var request = new APIRequest<GetSysPageDetailRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetSysPageDetailRP, GetSysPageDetailRD>(APITypes.Product, "WX.SysPage.GetSysPageDetail", request);
            Console.WriteLine(rsp.ToJSON());
        }

        /// <summary>
        /// 获取套餐列表
        /// </summary>
        /// 
       [Test]
        public void GetVocationVersionMappingListAH()
        {
            GetVocationVersionMappingListRP RP = new GetVocationVersionMappingListRP();
            //RP.PageIndex = 0;
           // RP.PageSize = 5;
            var request = new APIRequest<GetVocationVersionMappingListRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetVocationVersionMappingListRP, GetVocationVersionMappingListRD>(APITypes.Product, "WX.SysPage.GetVocationVersionMappingList", request);
        }
        //应用到套餐
        [Test]
        public void SetModulePageMappingAH()
        {
            SetModulePageMappingRP RP = new SetModulePageMappingRP();
            RP.PageId = "46b6a6ba-8c2b-4809-9185-de895de57589";  //门店详
            RP.VocaVerMappingID =new string[]{"28A18577-38CB-486D-861C-0279B638807D"};
            var request = new APIRequest<SetModulePageMappingRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<SetModulePageMappingRP,EmptyResponseData>(APITypes.Product, "WX.SysPage.SetModulePageMapping", request);
        }

        //设置客户模板
        [Test]
        public void SetCustomerModuleMappingAH()
        {
            SetCustomerModuleMappingRP RP = new SetCustomerModuleMappingRP();
            RP.VocaVerMappingID ="28A18577-38CB-486D-861C-0279B638807D";
            RP.CustomerID = "92a251898d63474f96b2145fcee2860c"; //花间堂
            var request = new APIRequest<SetCustomerModuleMappingRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<SetCustomerModuleMappingRP, SetCustomerModuleMappingRD>(APITypes.Product, "WX.SysPage.SetCustomerModuleMapping", request);
        }

        /// <summary>
        /// 获取客户包含套餐
        /// </summary>
        /// 
        [Test]
        public void GetCustomerModuleMappingAH()
        {
            GetCustomerModuleMappingRP RP = new GetCustomerModuleMappingRP();
            //RP.CustomerId = "92a251898d63474f96b2145fcee2860c"; //花间堂
            RP.CustomerId = "0c68326c4b1f423eb52ea2972ab0426d";  //罗伯家族
            var request = new APIRequest<GetCustomerModuleMappingRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetCustomerModuleMappingRP, GetCustomerModuleMappingRD>(APITypes.Product, "WX.SysPage.GetCustomerModuleMapping", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

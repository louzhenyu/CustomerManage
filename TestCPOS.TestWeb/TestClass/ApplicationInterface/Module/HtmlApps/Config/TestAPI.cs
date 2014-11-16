/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/18 17:28:01
 * Description	:
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
using System.Text;

using NUnit.Framework;
using JIT.CPOS.DTO.Module.HtmlApp.Config.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;


namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.HtmlApps.Config
{
    /// <summary>
    /// TestAPI  
    /// </summary>
    [TestFixture]
    public class TestAPI
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestAPI()
        {
        }
        #endregion

        [Test]
        public void TestGetConfig()
        {
            EmptyRequest req = new EmptyRequest();
            req.CustomerID = "4638b8b9d8c1435e8618f892d44a17a1";
            var rsp = APIClientProxy.CallAPI<EmptyRequestParameter, GetConfigRD>(APITypes.Product, "HtmlApp.Config.GetConfig",req);
            Assert.IsTrue(rsp.IsSuccess);
        }
    }
}

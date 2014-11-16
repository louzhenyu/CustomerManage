/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/17 16:01:26
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
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;

using JIT.CPOS.DTO.Project.LZLJ.Activity.Activity.Response;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Project.LZLJ.Activity.Activity
{
    /// <summary>
    /// TestActivity  
    /// </summary>
    [TestFixture]
    public class TestActivity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestActivity()
        {
        }
        #endregion

        [Test]
        public void TestGetHomePageActivityListAH()
        {
            var req = new EmptyRequest();
            req.CustomerID = "e703dbedadd943abacf864531decdac1";
            req.JSONP = "test";
            var rd = APIClientProxy.CallAPI<EmptyRequestParameter, GetHomePageActivityListRD>(APITypes.Project, "LZLJ.Activity.Activity.GetHomePageActivityList", req);
            Assert.IsTrue(rd != null);
            Assert.IsTrue(rd.Data.Items != null);
        }
    }
}

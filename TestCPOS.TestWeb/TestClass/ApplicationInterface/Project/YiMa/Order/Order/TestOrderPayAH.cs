/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/28 16:57:46
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
using System.Web;

using NUnit.Framework;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.DTO.Project.YiMa.Order.Order.Request;
using JIT.CPOS.DTO.Project.YiMa.Order.Order.Response;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Project.YiMa.Order.Order
{
    /// <summary>
    /// TestOrderPayAH  
    /// </summary>
    [TestFixture]
    public class TestOrderPayAH
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestOrderPayAH()
        {
        }
        #endregion

        [Test]
        public void TestProcessRequest()
        {
            APIRequest<OrderPayRP> req = new APIRequest<OrderPayRP>();
            req.Parameters.MobileNO = "13011111111";
            req.Parameters.OrderDesc = "测试订单";
            req.Parameters.OrderID = "YiMaOrd0001";
            req.Parameters.PayChannelID = 1;

            var rsp1 = APIClientProxy.CallAPI<OrderPayRP, OrderPayRD>(APITypes.Project, "YiMa.Order.Order.OrderPay", req);
            Assert.IsTrue(rsp1 != null);
            Assert.IsTrue(rsp1.IsSuccess);
            Assert.IsTrue(rsp1.Data.OrderID == req.Parameters.OrderID);
        }
    }
}

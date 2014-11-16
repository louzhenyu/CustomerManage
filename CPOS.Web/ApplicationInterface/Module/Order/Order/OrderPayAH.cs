/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:40:00
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

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    /// <summary>
    /// 订单支付API请求处理 
    /// </summary>
    public class OrderPayAH:BaseActionHandler<OrderPayRP,OrderPayRD>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrderPayAH()
        {
        }
        #endregion

        #region 接口的业务处理失败错误码定义 取值范围为：300-399
        /// <summary>
        /// 业务处理失败,默认错误
        /// </summary>
        const string BUSINESS_PROCESS_FAILED = "300";

        /// <summary>
        /// 业务处理失败 - 订单不存在
        /// </summary>
        const string BUSINESS_PROCESS_FAILED_ORDER_NOT_EXISTS = "302";
        #endregion

        #region 处理接口请求
        /// <summary>
        /// 处理接口请求
        /// </summary>
        /// <param name="pVersion">接口版本号</param>
        /// <param name="pRequest">接口请求</param>
        /// <returns>处理结果</returns>
        protected override OrderPayRD ProcessRequest(APIRequest<OrderPayRP> pRequest)
        {
            OrderPayRD rsp = new OrderPayRD();
            //TODO:执行业务处理
            rsp.OrderID =pRequest.Parameters.OrderID;
            //
            return rsp;
        }
        #endregion
    }
}

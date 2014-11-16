/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/28 16:59:53
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

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Project.YiMa.Order.Order.Response
{
    /// <summary>
    /// 下单支付的响应数据 
    /// </summary>
    public class OrderPayRD : IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrderPayRD()
        {
        }
        #endregion

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 支付URL
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// 二维码URL
        /// </summary>
        public string QrCodeUrl { get; set; }
    }
}

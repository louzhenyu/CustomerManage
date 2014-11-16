/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 15:56:20
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

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    /// <summary>
    /// 下单支付的请求参数 
    /// </summary>
    public class OrderPayRP:IAPIRequestParameter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrderPayRP()
        {
        }
        #endregion

        /// <summary>
        /// 支付通道ID
        /// </summary>
        public int? PayChannelID { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 订单描述
        /// </summary>
        public string OrderDesc { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNO { get; set; }

        #region IAPIRequestParameter 成员

        public void Validate()
        {
            
        }

        #endregion
    }
}

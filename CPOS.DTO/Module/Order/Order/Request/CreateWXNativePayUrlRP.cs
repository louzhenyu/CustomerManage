using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class CreateWXNativePayUrlRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 支付通道ID
        /// </summary>
        public int PayChannelID { get; set; }
        /// <summary>
        /// Native支付URL类型，1-商品，2-订单
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 商品ID或者订单ID
        /// </summary>
        public string ObjectID { get; set; }
        #endregion

        public void Validate()
        {

        }
    }
}

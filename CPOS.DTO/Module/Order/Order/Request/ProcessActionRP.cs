using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
   public class ProcessActionRP:IAPIRequestParameter
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 订单操作码(当前以订单状态码作为订单操作码)
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 订单操作参数，预留字段，当前可传空，包含此操作需要更新的数据，数据内容为json，一种订单操作码对应一套应该更新的数据结构
        /// </summary>
        public string ActionParameter { get; set; }

        public string DeliverOrder { get; set; }//配送单号
        public string DeliverCompany { get; set; }//配送商
        public string VipID { get; set; }//app上传来vipID

        public void Validate()
        {
           
        }
    }
}

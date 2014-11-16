using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderMsgInfo
    {
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderStatus { get; set; }
        public string OrderStatusCode { get; set; }
        public string VipId { get; set; }
        public string UserId { get; set; }
        public string VipName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public Int32 IsCallSMSPush { get; set; }
        public Int32 IsCallEmailPush { get; set; }
        public string CallUserId { get; set; }
        public string CallUserEmail { get; set; }
        public string CallUserPhone { get; set; }
        public string CallUserName { get; set; }
        public string create_time { get; set; }
        public decimal ActualAmount { get; set; }
    }

}

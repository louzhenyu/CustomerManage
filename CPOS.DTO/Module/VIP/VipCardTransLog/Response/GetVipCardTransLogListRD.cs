using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipCardTransLog.Response
{
    public class GetVipCardTransLogListRD : IAPIResponseData
    {
        public List<VipCardTransLogInfo> VipCardTransLogList { get; set; }
    }
    /// <summary>
    /// 消费记录
    /// </summary>
    public class VipCardTransLogInfo
    {
        public string BillNo { get; set; }
        public decimal Cash { get; set; }
        public decimal Points { get; set; }
        public decimal Bonus { get; set; }
        public string UnitCode { get; set; }
        public string TransTime { get; set; }
        /// <summary>
        /// 当前卡号
        /// </summary>
        public string VipCardCode { get; set; }
        /// <summary>
        /// 老卡号
        /// </summary>
        public string OldVipCardCode { get; set; }
    }
}

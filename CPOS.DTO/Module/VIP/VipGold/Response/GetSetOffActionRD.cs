using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetSetOffActionRD : IAPIResponseData
    {
        public List<GetSetOffActionInfo> GetSetOffActionInfoList { get; set; }
    }

    public class GetSetOffActionInfo
    {
        public string SetoffEventID { get; set; }
        /// <summary>
        /// 1 会员集客
        ///2 员工集客
        /// </summary>
        public int SetoffType { get; set; }
        /// <summary>
        /// 1:现金 2：积分
        /// </summary>
        public int SetoffRegAwardType { get; set; }

        public decimal SetoffRegPrize { get; set; }
        public decimal SetoffOrderPer { get; set; }
        /// <summary>
        /// 0=单单有效，1=首单有效
        /// </summary>
        public int SetoffOrderTimers { get; set; }
        /// <summary>
        /// 10：使用中 90：失效
        /// </summary>
        public int IincentiveRuleStatus { get; set; }
        public List<GetSetoffToolsInfo> GetSetoffToolsInfoList { get; set; }
    }

    public class GetSetoffToolsInfo
    {
        public string SetoffToolID { get; set; }
        /// <summary>
        ///  CTW：创意仓库,Coupon：优惠券,SetoffPoster：集客报
        /// </summary>
        public string ToolType { get; set; }
        public string Name { get; set; }
        public string BeginData { get; set; }
        public string EndData { get; set; }
        public int SurplusCount { get; set; }
        public int ServiceLife { get; set; }
        /// <summary>
        /// 集客海报Url
        /// </summary>
        public string SetoffPosterUrl { get; set; }

        public string ObjectId { get; set; }
    }
}

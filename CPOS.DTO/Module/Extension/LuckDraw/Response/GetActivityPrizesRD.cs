using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.LuckDraw.Response
{
    public class GetActivityPrizesRD : IAPIResponseData
    {
        public Guid? ActivityID { get; set; }
        public string ActivityName { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? JoinLimit { get; set; }
        public int? LowestPointLimit { get; set; }
        public ActivityPrizesInfo[] PrizesList { get; set; }
    }
    public class ActivityPrizesInfo
    {
        public string PrizesID { get; set; }
        public string PrizesName { get; set; }
        public string ImageUrl { get; set; }
        public string CouponTypeID { get; set; }
        public int TotalCount { get; set; }
        public int WeekCount { get; set; }
        public int RemainingQty { get; set; }
        public int LowestPointLimit { get; set; }
        public int UsePoint { get; set; }
        public decimal Probability { get; set; }
    }
}

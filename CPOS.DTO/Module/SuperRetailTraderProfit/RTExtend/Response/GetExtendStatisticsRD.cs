using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response
{
    public class GetExtendStatisticsRD : IAPIResponseData
    {
        #region 代码注释
        //public GetExtendStatisticsRD(List<R_SRT_ShareCountEntity> dbList1, R_SRT_HomeEntity dbEntity2)
        //{
        //    List = new List<StatisticsInfo>();
        //    var tmp1 = dbList1.Select(x => new StatisticsInfo()
        //    {
        //        Type = GetMyType(x.SRTToolType),
        //        Count = x.Day30ShareCount,
        //        LinkRelativeCount = x.Day30ShareCountIGrowth
        //    });
        //    List.AddRange(tmp1);
        //    var tmp2 = new StatisticsInfo()
        //    {
        //        Type = 1,
        //        Count = GetMyCount(from m in tmp1
        //                           select m.Count),
        //        LinkRelativeCount = GetMyCount(from m in tmp1
        //                                       select m.LinkRelativeCount)
        //    };
        //    List.Add(tmp2);
        //    if (dbEntity2 != null)
        //    {
        //        var tmp3 = new StatisticsInfo()
        //        {
        //            Type = 4,
        //            Count = dbEntity2.Day30AddRTCount,
        //            LinkRelativeCount = dbEntity2.Day30AddRTCount != null && dbEntity2.LastDay30AddRTCount != null ? dbEntity2.Day30AddRTCount - dbEntity2.LastDay30AddRTCount : null
        //        };
        //        List.Add(tmp3);
        //    }
        //    List = List.OrderBy(x => x.Type).ToList();
        //}
        //public int GetMyCount(IEnumerable<int?> v)
        //{
        //    int result = 0;
        //    foreach (var m in v)
        //    {
        //        if (m != null)
        //        {
        //            result += Convert.ToInt32(m);
        //        }
        //    }
        //    return result;
        //}
        //private int GetMyType(string key)
        //{
        //    switch (key)
        //    {
        //        case "CTW":
        //            return 3;
        //        case "Coupon":
        //            return 5;
        //        case "SetoffPoster":
        //            return 6;
        //        case "Material":
        //            return 2;
        //        default:
        //            return 0;
        //    }
        //}   
        #endregion
        public List<StatisticsInfo> List { get; set; }
    }
    public class StatisticsInfo
    {
        /// <summary>
        /// 效果类型:1近30天拓展工具推送次数 2微信图文 3活动 4近30天新增分销商 5优惠券 6招募海报
        /// </summary>
        public int Type { get; set; }
        public int? Count { get; set; }
        public int? LinkRelativeCount { get; set; }
    }
}

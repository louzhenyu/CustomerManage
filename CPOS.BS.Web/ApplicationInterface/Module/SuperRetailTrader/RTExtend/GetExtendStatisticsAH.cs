using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.RTExtend
{
    /// <summary>
    /// 分销扩展统计
    /// </summary>
    public class GetExtendStatisticsAH : BaseActionHandler<EmptyRequestParameter, GetExtendStatisticsRD>
    {
        protected override GetExtendStatisticsRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
            {
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_RESPONSE };
            }
            R_SRT_HomeBLL homeBll = new R_SRT_HomeBLL(loggingSessionInfo);
            R_SRT_ShareCountBLL shareBll = new R_SRT_ShareCountBLL(loggingSessionInfo);
            var r1 = homeBll.GetNearest1DayEntity();
            var r2 = shareBll.GetStatisticList();
            var result = TransModel(r2, r1);
            return result;
        }
        /// <summary>
        /// 模型转换
        /// </summary>
        /// <param name="dbList1"></param>
        /// <param name="dbEntity2"></param>
        /// <returns></returns>
        public GetExtendStatisticsRD TransModel(List<R_SRT_ShareCountEntity> dbList1, R_SRT_HomeEntity dbEntity2)
        {
            GetExtendStatisticsRD rd = new GetExtendStatisticsRD();
            rd.List = new List<StatisticsInfo>();
            var List = rd.List;
            //Type 2 3 5 6
            var tmp1 = dbList1.Select(x => new StatisticsInfo()
            {
                Type = GetMyType(x.SRTToolType),
                Count = x.Day30ShareCount,
                LinkRelativeCount = x.Day30ShareCountIGrowth
            });
            List.AddRange(tmp1);
            //Type 2
            var tmp2 = new StatisticsInfo()
            {
                Type = 1,
                Count = GetMyCount(from m in tmp1
                                   select m.Count),
                LinkRelativeCount = GetMyCount(from m in tmp1
                                               select m.LinkRelativeCount)
            };
            List.Add(tmp2);
            //Type 4
            if (dbEntity2 != null)
            {
                var tmp3 = new StatisticsInfo()
                {
                    Type = 4,
                    Count = dbEntity2.Day30AddRTCount,
                    LinkRelativeCount = dbEntity2.Day30AddRTCount != null && dbEntity2.LastDay30AddRTCount != null ? dbEntity2.Day30AddRTCount - dbEntity2.LastDay30AddRTCount : null
                };
                List.Add(tmp3);
            }
            List = List.OrderBy(x => x.Type).ToList();
            return rd;
        }
        /// <summary>
        /// IEnumerable<int?>转IEnumerable<int>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private int GetMyCount(IEnumerable<int?> v)
        {
            int result = 0;
            foreach (var m in v)
            {
                if (m != null)
                {
                    result += Convert.ToInt32(m);
                }
            }
            return result;
        }
        /// <summary>
        /// 通过数据库字符串 返回ViewMomodel Type
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetMyType(string key)
        {
            switch (key)
            {
                case "CTW":
                    return 3;
                case "Coupon":
                    return 5;
                case "SetoffPoster":
                    return 6;
                case "Material":
                    return 2;
                default:
                    return 0;
            }
        }
    }


}
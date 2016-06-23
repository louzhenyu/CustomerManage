using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.RetailTrader.Response
{
    public class GetSuperRetailTraderListRD : IAPIResponseData
    {
        public List<SuperRetailTraderListInfo> SuperRetailTraderList { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public GetSuperRetailTraderListRD()
        {
            SuperRetailTraderList = new List<SuperRetailTraderListInfo>();
        }
    }

    public class SuperRetailTraderListInfo
    {
        /// <summary>
        /// 分销商姓名
        /// </summary>
        public string SuperRetailTraderName { get; set; }
        /// <summary>
        /// 分销商手机
        /// </summary>
        public string SuperRetailTraderPhone { get; set; }
        /// <summary>
        /// 分销商来源
        /// </summary>
        public string SuperRetailTraderFrom { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>

        public int? OrderCount { get; set; }
        /// <summary>
        /// 提现总数
        /// </summary>
        public int? WithdrawCount { get; set; }
        /// <summary>
        /// 提现总金额
        /// </summary>

        public decimal? WithdrawTotalMoney { get; set; }
        /// <summary>
        /// 下线人数
        /// </summary>
        public int? NumberOffline { get; set; }
        /// <summary>
        /// 加盟时间
        /// </summary>
        public string JoinTime { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public Guid? SuperRetailTraderID { get; set; }
    }
}

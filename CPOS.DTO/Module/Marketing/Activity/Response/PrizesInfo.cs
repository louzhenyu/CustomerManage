using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Response
{
    public class PrizesInfo
    {
        /// <summary>
        /// 奖品ID
        /// </summary>
        public string PrizesID { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 奖品类型
        /// </summary>
        public int PrizesType { get; set; }
        /// <summary>
        /// 奖品明细集合
        /// </summary>
        public List<PrizesDetailInfo> PrizesDetailList { get; set; }
    }

    public class PrizesDetailInfo
    {
        /// <summary>
        /// 奖品明细ID
        /// </summary>
        public string PrizesDetailID { get; set; }
        /// <summary>
        /// 券ID
        /// </summary>
        public string CouponTypeID { get; set; }
        /// <summary>
        /// 券名称
        /// </summary>
        public string CouponTypeName { get; set; }
        /// <summary>
        /// 每人赠送个数
        /// </summary>
        public int NumLimit { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }

        /// <summary>
        /// bug# 2838 有效期
        /// </summary>
        public string ValidityPeriod { get; set; }
        /// <summary>
        /// 券描述
        /// </summary>
        public string CouponTypeDesc { get; set; }
    }
}

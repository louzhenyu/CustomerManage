using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.SysVipCardType.Response
{
    public class GetSysVipCardTypeListsRD : IAPIResponseData
    {
        public List<SysVipCardTypeInfos> SysVipCardTypeList { get; set; }
    }
    public class SysVipCardTypeInfos
    {
        /// <summary>
        /// 卡类别ID
        /// </summary>
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string VipCardTypeCode { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal? CardDiscount { get; set; }
        /// <summary>
        /// 积分倍数
        /// </summary>
        public int? PointsMultiple { get; set; }
        /// <summary>
        /// 充值满n
        /// </summary>
        public decimal? ChargeFull { get; set; }
        /// <summary>
        /// 充值送n
        /// </summary>
        public decimal? ChargeGive { get; set; }
        /// <summary>
        /// 售卡金额
        /// </summary>
        public decimal? Price { get; set; }
    }
}

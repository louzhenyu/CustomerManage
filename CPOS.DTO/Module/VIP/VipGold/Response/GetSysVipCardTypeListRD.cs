using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetSysVipCardTypeListRD : IAPIResponseData
    {
        public List<SysVipCardTypeInfo> SysVipCardTypeList { get; set; }

    }

    public class SysVipCardTypeInfo
    {
        /// <summary>
        /// 卡类型标志
        /// </summary>
        public int? VipCardTypeID { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 是否可充值 （1=是，0=否）
        /// </summary>
        public int? IsPrepaid { get; set; }

        /// <summary>
        /// 是否购买升级
        /// </summary>
        public int IsPurchaseUpgrade { get; set; }

        /// <summary>
        /// 是否充值升级
        /// </summary>
        public int IsRecharge { get; set; }
        /// <summary>
        /// 是否消费升级
        /// </summary>

        public int IsBuyUpgrade { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetVipCardTypeRD : IAPIResponseData
    {
        public List<VipCardRelateInfo> VipCardTypeList { get; set; }
    }
    public class VipCardRelateInfo
    {
        /// <summary>
        /// 实体卡会员VIPID
        /// </summary>
        public string BindVipID { get; set; }
        /// <summary>
        /// 卡等级ID
        /// </summary>
        public int VipCardTypeID { get; set; }
        /// <summary>
        /// 卡等级名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡折扣
        /// </summary>
        public decimal? CardDiscount { get; set; }
        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal? TotalAmount { get; set; }
        public int Integration { get; set; }
    }
}

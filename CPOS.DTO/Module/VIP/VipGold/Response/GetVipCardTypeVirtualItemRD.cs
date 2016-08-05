using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetVipCardTypeVirtualItemRD : IAPIResponseData
    {
        /// <summary>
        /// 会员头像
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 当前会员卡等级
        /// </summary>
        public int? VipCardLevel { get; set; }
        /// <summary>
        /// 会员级别名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 当前剩余积分
        /// </summary>
        public int Integration { get; set; }
        /// <summary>
        /// 会员消费总金额
        /// </summary>
        public string VipConsumptionAmount { get; set; }
        /// <summary>
        /// 虚拟商品关联卡信息列表
        /// </summary>
        public List<VipCardTypeRelateInfo> VipCardTypeItemList { get; set; }
    }
}

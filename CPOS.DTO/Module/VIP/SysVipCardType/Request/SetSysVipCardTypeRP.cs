using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request
{
    public class SetSysVipCardTypeRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡类型ID（编辑必填）
        /// </summary>
        public int? VipCardTypeID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string VipCardTypeCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// 卡类型图片
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal CardDiscount { get; set; }
        /// <summary>
        /// 积分倍数
        /// </summary>
        public int PointsMultiple { get; set; }
        /// <summary>
        /// 充值满n
        /// </summary>
        public decimal ChargeFull { get; set; }
        /// <summary>
        /// 充值送n
        /// </summary>
        public decimal ChargeGive { get; set; }

        /// <summary>
        /// 卡类型（0=会员卡；1=储值卡；2=消费卡；）
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 是否启用密码(0=否;1=是)
        /// </summary>
        public int IsPassword { get; set; }
        /// <summary>
        /// 卡等级(1=等级1;2=等级2……等级越高，级别越高)
        /// </summary>
        public int VipCardLevel { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public decimal Prices { get; set; }
        /// <summary>
        /// 买卡是否可补差价（0=不可补；1=可补）
        /// </summary>
        public int? IsExtraMoney { get; set; }
        /// <summary>
        /// 兑换积分
        /// </summary>
        public int ExchangeIntegral { get; set; }
        /// <summary>
        /// 消费n元赠送1积分
        /// </summary>
        public decimal PaidGivePoints { get; set; }
        /// <summary>
        /// 按订单金额的N%获得返现
        /// </summary>
        public decimal ReturnAmountPer { get; set; }
        /// <summary>
        /// 自动升级累计金额
        /// </summary>
        public decimal UpgradeAmount { get; set; }
        /// <summary>
        /// 单次消费金额自动升级
        /// </summary>
        public decimal UpgradeOnceAmount { get; set; }
        /// <summary>
        /// 自动升级累计积分
        /// </summary>
        public int UpgradePoint { get; set; }


        public void Validate()
        {

        }
    }
}

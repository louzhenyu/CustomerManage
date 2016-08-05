using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Response
{
    public class AuthCodeLoginRD : IAPIResponseData
    {
        public MemberInfo MemberInfo { get; set; }
    }

    public class MemberInfo
    {
        public string VipID { get; set; }
        public string VipName { get; set; }
        public string VipRealName { get; set; }
        public string VipNo { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string VipLevelName { get; set; }  //会员卡名称
        public string CardTypeImageUrl { get; set; }//会员卡图片
        /// <summary>
        /// 是否有付费的卡0=没有；1=有
        /// </summary>
        public int IsCostCardType { get; set; }
        /// <summary>
        /// 当前会员卡价格
        /// </summary>
        public decimal CardTypePrice { get; set; }
        /// <summary>
        ///  买卡是否可补差价（0=不可补；1=可补）
        /// </summary>
        public int? IsExtraMoney { get; set; }
        public string ImageUrl { get; set; }//String	会员图片URL
        public decimal Balance { get; set; }//decimal	余额
        public decimal ReturnAmount { get; set; }//返现
        public decimal Integration { get; set; }//int	积分
        public int IntegrationUsed { get; set; }//int	已兑换积分
        public int UnfinishedOrdersCount { get; set; }//int	未完成订单数
        public int CouponsCount { get; set; }//int	优惠券数量
        /// <summary>
        /// 会员状态，1-未注册，2-注册
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 获取会员权益，从表CustomerBasicSetting 键值对SettingCode=‘MemberBenefits’，获取SettingValue的值，该值是HTML5字符窜
        /// </summary>
        public string[] MemberBenefits { get; set; }
        /// <summary>
        /// 是否已绑定
        /// </summary>
        public bool IsActivate { get; set; }

        /// <summary>
        /// 是否是经销商 0=不是；1=是
        /// </summary>
        public int IsDealer { get; set; }
        /// <summary>
        /// 是否可以成为超级分销商 0=不可以成为超级分销商，1=可以成为
        /// </summary>
        public int CanBeSuperRetailTrader { get; set; }
        /// <summary>
        /// 是否是超级分销商  有值表示为超级分销商/无值表示不是超级分销商
        /// </summary>
        public string SuperRetailTraderID { get; set; }
        /// <summary>
        /// 卡折扣
        /// </summary>
        public decimal CardDiscount { get; set; }
        /// <summary>
        /// 是否需要领卡 (0=需要领卡，1=不需要领卡)
        /// </summary>
        public int IsNeedCard { get; set; }
        /// <summary>
        /// 红利金额
        /// </summary>
        public decimal ProfitAmount { get; set; }
        /// <summary>
        /// 还需多少金额升级(消费升级：目前用的累积升级)
        /// </summary>
        public decimal UpGradeNeedMoney { get; set; }
        /// <summary>
        /// 升级提示 UpGradeNeedMoney>0时有提示
        /// </summary>
        public string UpgradePrompt { get; set; }
        /// <summary>
        /// 购物车数量
        /// </summary>
        public int ShopCartCount { get; set; }
        /// <summary>
        /// 当前会员所持卡是否可储值(0=不可储值,1=可储值)
        /// </summary>
        public int? IsPrepaid { get; set; }
    }
}

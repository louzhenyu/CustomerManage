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
        /// 是否是超级分销商  有值表示为超级分销商/无值表示不是超级分销商
        /// </summary>
        public string SuperRetailTraderID { get; set; }
    }
}

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
        public string VipLevelName { get; set; }
        public string ImageUrl { get; set; }//String	会员卡图片URL
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
    }
}

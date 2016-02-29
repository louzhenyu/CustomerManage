using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Request
{
    public class SetSalesSettingRP : IAPIRequestParameter
    {
        public void Validate()
        {

        }
        /// <summary>
        /// 类型(0=按订单；1=按商品)
        /// </summary>
        public int? SocialSalesType { get; set; }
        /// <summary>
        /// 启用员工分销设置(0=不启用；1=启用)
        /// </summary>
        public int? EnableEmployeeSales { get; set; }
        /// <summary>
        /// 员工的商品分销价比例
        /// </summary>
        public double? EDistributionPricePer { get; set; }
        /// <summary>
        /// 员工的订单金额提成比例
        /// </summary>
        public double? EOrderCommissionPer { get; set; }
        /// <summary>
        /// 启用会员分销设置(0=不启用；1=启用)
        /// </summary>
        public int? EnableVipSales { get; set; }
        /// <summary>
        /// 会员的商品分销价比例
        /// </summary>
        public double? VDistributionPricePer { get; set; }
        /// <summary>
        /// 会员的订单金额提成比例
        /// </summary>
        public double? VOrderCommissionPer { get; set; }
        /// <summary>
        /// 集客分成比例
        /// </summary>
        public double? GetVipUserOrderPer { get; set; }
        /// <summary>
        /// 邀请小伙伴获得积分	
        /// </summary>
        public int? InvitePartnersPoints { get; set; }
    }
}

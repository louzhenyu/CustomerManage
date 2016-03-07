/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-3-6 11:18:15
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class SysVipCardTypeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysVipCardTypeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Category { get; set; }

		/// <summary>
		/// 卡类型编码
		/// </summary>
		public String VipCardTypeCode { get; set; }

		/// <summary>
		/// 卡类型名称
		/// </summary>
		public String VipCardTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsPassword { get; set; }

		/// <summary>
		/// 累计充值
		/// </summary>
		public Decimal? AddUpAmount { get; set; }

		/// <summary>
		/// 是否扩展到会员
		/// </summary>
		public Int32? IsExpandVip { get; set; }

		/// <summary>
		/// 充值优惠
		/// </summary>
		public Decimal? PreferentialAmount { get; set; }

		/// <summary>
		/// 消费优惠
		/// </summary>
		public Decimal? SalesPreferentiaAmount { get; set; }

		/// <summary>
		/// 积分倍数
		/// </summary>
		public Int32? IntegralMultiples { get; set; }

		/// <summary>
		/// 是否可储值
		/// </summary>
		public Int32? Isprepaid { get; set; }

		/// <summary>
		/// 是否可积分
		/// </summary>
		public Int32? IsPoints { get; set; }

		/// <summary>
		/// 是否可打折
		/// </summary>
		public Int32? IsDiscount { get; set; }

		/// <summary>
		/// 是否可线上充值
		/// </summary>
		public Int32? IsOnlineRecharge { get; set; }

		/// <summary>
		/// 是否可记名
		/// </summary>
		public Int32? IsRegName { get; set; }

		/// <summary>
		/// 是否可同用优惠券
		/// </summary>
		public Int32? IsUseCoupon { get; set; }

		/// <summary>
		/// 是否绑定电子卡
		/// </summary>
		public Int32? IsBindECard { get; set; }

		/// <summary>
		/// 卡类型图片
		/// </summary>
		public String PicUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? UpgradeAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? UpgradeOnceAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UpgradePoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ExchangeIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Prices { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsExtraMoney { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:26
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
    public partial class VipEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
		/// 会员名称
		/// </summary>
		public String VipName { get; set; }

		/// <summary>
		/// 会员级别
		/// </summary>
		public Int32? VipLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCode { get; set; }

		/// <summary>
		/// 微信公众号
		/// </summary>
		public String WeiXin { get; set; }

		/// <summary>
		/// 微信OpenID
		/// </summary>
		public String WeiXinUserId { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public Int32? Gender { get; set; }

		/// <summary>
		/// 年龄
		/// </summary>
		public Int32? Age { get; set; }

		/// <summary>
		/// 手机号
		/// </summary>
		public String Phone { get; set; }

		/// <summary>
		/// 证件类型
		/// </summary>
		public String IDType { get; set; }

		/// <summary>
		/// 身份证号
		/// </summary>
		public String IDNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SinaMBlog { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TencentMBlog { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Birthday { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Qq { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipSourceId { get; set; }

		/// <summary>
		/// 积分
		/// </summary>
		public Decimal? Integration { get; set; }

		/// <summary>
		/// 客户标识
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// 最近消费时间
		/// </summary>
		public DateTime? RecentlySalesTime { get; set; }

		/// <summary>
		/// 注册时间
		/// </summary>
		public DateTime? RegistrationTime { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String APPID { get; set; }

		/// <summary>
		/// 上线会员标识
		/// </summary>
		public String HigherVipID { get; set; }

		/// <summary>
		/// 二维码卡会员
		/// </summary>
		public String QRVipCode { get; set; }

		/// <summary>
		/// 城市
		/// </summary>
		public String City { get; set; }

		/// <summary>
		/// 优惠券URL
		/// </summary>
		public String CouponURL { get; set; }

		/// <summary>
		/// 优惠券信息（会籍店）
		/// </summary>
		public String CouponInfo { get; set; }

		/// <summary>
		/// 会籍店
		/// </summary>
		public String MembershipStore { get; set; }

		/// <summary>
		/// 购买金额
		/// </summary>
		public Decimal? PurchaseAmount { get; set; }

		/// <summary>
		/// 购买次数
		/// </summary>
		public Int32? PurchaseCount { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		public String DeliveryAddress { get; set; }

		/// <summary>
		/// 经度
		/// </summary>
		public String Longitude { get; set; }

		/// <summary>
		/// 维度
		/// </summary>
		public String Latitude { get; set; }

		/// <summary>
		/// 会员密码
		/// </summary>
		public String VipPasswrod { get; set; }

		/// <summary>
		/// 头像URL
		/// </summary>
		public String HeadImgUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col5 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col6 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col7 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col9 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col10 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col11 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col12 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col13 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col14 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col15 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col16 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col17 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col18 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col19 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col20 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col21 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col22 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col23 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col24 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col25 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col26 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col27 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col28 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col29 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col30 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col31 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col32 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col33 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col34 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col35 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col36 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col37 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col38 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col39 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col40 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col41 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col42 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col43 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col44 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col45 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col46 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col47 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col48 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col49 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Col50 { get; set; }

		/// <summary>
		/// 真实姓名
		/// </summary>
		public String VipRealName { get; set; }

		/// <summary>
		/// 是否激活
		/// </summary>
		public Int32? IsActivate { get; set; }

		/// <summary>
		/// 会员导入标识
		/// </summary>
		public String VIPImportID { get; set; }

		/// <summary>
		/// 分享会员标识
		/// </summary>
		public String ShareVipId { get; set; }

		/// <summary>
		/// 集客员工标识
		/// </summary>
		public String SetoffUserId { get; set; }

		/// <summary>
		/// 分享员工标识
		/// </summary>
		public String ShareUserId { get; set; }

		/// <summary>
		/// 是否小店
		/// </summary>
		public Int32? IsSotre { get; set; }


        #endregion

    }
}
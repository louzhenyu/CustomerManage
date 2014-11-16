/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/14 19:49:53
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
    public partial class OrderIntegralEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrderIntegralEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// 订单ID
		/// </summary>
		public String OrderIntegralID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public String OrderNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public String ItemID { get; set; }

		/// <summary>
        /// 兑换数量
		/// </summary>
		public Int32? Quantity { get; set; }

		/// <summary>
        /// 商品积分
		/// </summary>
		public Decimal? Integral { get; set; }

		/// <summary>
        /// 总积分
		/// </summary>
		public Decimal? IntegralAmmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
        /// 收货人
		/// </summary>
		public String LinkMan { get; set; }

		/// <summary>
        /// 收货电话
		/// </summary>
		public String LinkTel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CityID { get; set; }

		/// <summary>
        /// 收货地址
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
        /// 下单时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
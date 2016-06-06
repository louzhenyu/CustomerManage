/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 13:59:40
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
    public partial class SetoffToolUserViewEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SetoffToolUserViewEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? SetoffToolUserViewID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SetoffEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SetoffToolID { get; set; }

		/// <summary>
		/// CTW：创意仓库   Coupon：优惠券   SetoffPoster：集客报
		/// </summary>
		public String ToolType { get; set; }

		/// <summary>
		/// ToolType=CTW，创意活动ID，T_CTW_LEvent.CTWEventId   ToolType=Coupon：优惠券种，CouponType.CouponTypeID   ToolType=SetoffPoster：集客海报，SetoffPoster.SetoffPosterID
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 1 微信用户   2 APP员工   
		/// </summary>
		public Int32? NoticePlatformType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// 0：未打开   1：已打开
		/// </summary>
		public Int32? IsOpen { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? OpenTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

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
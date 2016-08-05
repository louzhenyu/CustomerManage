/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/13 11:56:02
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
    public partial class C_ActivityEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public C_ActivityEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 1=生日活动   2=普通活动
		/// </summary>
		public Int32? ActivityType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ActivityName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 0=不是；1=是
		/// </summary>
		public Int32? IsLongTime { get; set; }

		/// <summary>
		/// 大于0表示启用和倍数
		/// </summary>
		public Decimal? PointsMultiple { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendCouponQty { get; set; }

		/// <summary>
		/// 0=正常；   1=暂停；   未开始、运行中和结束状态均根据开始时间和结束时间判断   (1=暂停；未开始=2；运行中=3；已结束=4；)
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 0=不是；1=是
		/// </summary>
		public Int32? IsAllVipCardType { get; set; }

		/// <summary>
		/// 0=不是；1=是
		/// </summary>
		public Int32? IsVipGrouping { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TargetCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
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
		/// 0=正常状态；1=删除状态
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
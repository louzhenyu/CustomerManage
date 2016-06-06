/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/20 9:22:52
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
    /// ʵ�壺  
    /// </summary>
    public partial class Agg_SetoffForToolEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public Agg_SetoffForToolEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ToolType=CTW������ID��T_CTW_LEvent.CTWEventId   ToolType=Coupon���Ż�ȯ�֣�CouponType.CouponTypeID   ToolType=SetoffPoster�����ͺ�����SetoffPoster.SetoffPosterID   ����
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ObjectName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ShareCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SetoffCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OrderAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// CTW������ֿ�   Coupon���Ż�ȯ   SetoffPoster�����ͱ�   Goods����Ʒ   ����
		/// </summary>
		public String SetoffToolType { get;set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ID { get; set; }


        #endregion

    }
}
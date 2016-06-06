/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/18 15:10:35
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
    public partial class Agg_SetoffEffectDetailEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public Agg_SetoffEffectDetailEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserName { get; set; }

		/// <summary>
		/// CTW������ֿ�   Coupon���Ż�ȯ   SetoffPoster�����ͱ�   Goods����Ʒ
		/// </summary>
		public String SetoffToolType { get; set; }

		/// <summary>
		/// ToolType=CTW������ID��T_CTW_LEvent.CTWEventId   ToolType=Coupon���Ż�ȯ�֣�CouponType.CouponTypeID   ToolType=SetoffPoster�����ͺ�����SetoffPoster.SetoffPosterID
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
		/// 1��Ա��   2���ͷ�   3����Ա
		/// </summary>
		public Int32? SetoffRole { get; set; }

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
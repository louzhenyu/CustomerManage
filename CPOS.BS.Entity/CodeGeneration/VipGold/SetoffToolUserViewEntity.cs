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
    /// ʵ�壺  
    /// </summary>
    public partial class SetoffToolUserViewEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SetoffToolUserViewEntity()
        {
        }
        #endregion     

        #region ���Լ�
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
		/// CTW������ֿ�   Coupon���Ż�ȯ   SetoffPoster�����ͱ�
		/// </summary>
		public String ToolType { get; set; }

		/// <summary>
		/// ToolType=CTW������ID��T_CTW_LEvent.CTWEventId   ToolType=Coupon���Ż�ȯ�֣�CouponType.CouponTypeID   ToolType=SetoffPoster�����ͺ�����SetoffPoster.SetoffPosterID
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 1 ΢���û�   2 APPԱ��   
		/// </summary>
		public Int32? NoticePlatformType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// 0��δ��   1���Ѵ�
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
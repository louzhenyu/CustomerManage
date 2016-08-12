/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/5 16:49:40
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
    public partial class C_MessageSendEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public C_MessageSendEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? SendID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? MessageID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// SMS=����   WeChat=΢��   Email=�ʼ�
		/// </summary>
		public String MessageType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Phone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ActualSendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Priority { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 0=δ���ͣ�   1=�����У�   2=�ѷ��ͣ�   3=����ʧ��
		/// </summary>
		public Int32? IsSend { get; set; }

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
		/// 0=����״̬��1=ɾ��״̬
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DateDiffDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CouponCategory { get; set; }


        #endregion

    }
}
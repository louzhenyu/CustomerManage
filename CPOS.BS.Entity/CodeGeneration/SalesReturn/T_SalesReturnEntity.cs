/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-10 18:24:48
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
    public partial class T_SalesReturnEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SalesReturnEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? SalesReturnID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SalesReturnNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// 1=�˻�   2=����
		/// </summary>
		public Int32? ServicesType { get; set; }

		/// <summary>
		/// 1=�ͻ��ŵꣻ   2=����ͻ�
		/// </summary>
		public Int32? DeliveryType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SkuID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Qty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ActualQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? RefundAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ConfirmAmount { get; set; }

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
		public String UnitTel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Contacts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Phone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 1.�����   2.ȡ������   3.��˲�ͨ��   4.���ջ������ͨ����   5.�ܾ��ջ�   6.����ɣ����˿   7.����ɣ����˿
		/// </summary>
		public Int32? Status { get; set; }

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
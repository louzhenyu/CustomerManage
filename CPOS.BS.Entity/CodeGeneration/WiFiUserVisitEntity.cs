/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
    /// �û�����WiFi 
    /// </summary>
    public partial class WiFiUserVisitEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WiFiUserVisitEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ����
		/// </summary>
		public Guid? VisitID { get; set; }

		/// <summary>
        /// �û�ID
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
        /// �ŵ�ID
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
        /// ��ǰ�豸(�����Ӳ�ͬ�豸ʱ����Ҫ���´��ֶ�)
		/// </summary>
		public Guid? CurrentDeviceID { get; set; }

		/// <summary>
        /// ����
		/// </summary>
		public DateTime? CurrentDate { get; set; }

		/// <summary>
        /// ����ʱ��
		/// </summary>
		public DateTime? BeginTime { get; set; }

		/// <summary>
        /// �˳�ʱ��(���ƶ�����һλ��ʱ����Ҫ�����ֶ��ÿ�)
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
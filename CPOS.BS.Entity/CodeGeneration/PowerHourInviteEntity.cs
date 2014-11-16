/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/14 14:03:25
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
    public partial class PowerHourInviteEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PowerHourInviteEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��ʶ
		/// </summary>
		public Guid? PowerHourInviteID { get; set; }

		/// <summary>
		/// PowerHour(���)
		/// </summary>
		public String PowerHourID { get; set; }

		/// <summary>
		/// �����˵�UserID(���)
		/// </summary>
		public String InvitorUserID { get; set; }

		/// <summary>
		/// �������˵�UserID(���)
		/// </summary>
		public String StaffUserID { get; set; }

		/// <summary>
		/// ����״̬
		/// </summary>
		public Int32? AcceptState { get; set; }

		/// <summary>
		/// ��ϯ״̬
		/// </summary>
		public Int32? Attendence { get; set; }

		/// <summary>
		/// �ͻ�ID
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
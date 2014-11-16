/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
    public partial class EclubValidationEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EclubValidationEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ����ID
		/// </summary>
		public Guid? ValidationID { get; set; }

		/// <summary>
        /// �û�ID
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
        /// ��֤�룬6λ������֤��
		/// </summary>
		public String Code { get; set; }

		/// <summary>
        /// 1Ϊ��½�ɹ���2Ϊ���ڣ�0Ϊδ����
		/// </summary>
		public Int32? LoginStatus { get; set; }

        /// <summary>
        /// �Ƿ�Ϊ�ֻ�
        /// </summary>
        public Int32? IsPhone { get; set; }

        /// <summary>
        /// ��¼���ֻ��Ż�����
        /// </summary>
        public String LoginName { get; set; }

		/// <summary>
        /// �ͻ�ID
		/// </summary>
		public String CustomerId { get; set; }

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
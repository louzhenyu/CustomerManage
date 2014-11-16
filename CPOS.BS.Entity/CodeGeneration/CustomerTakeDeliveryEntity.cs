/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/10/21 17:38:06
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
    public partial class CustomerTakeDeliveryEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CustomerTakeDeliveryEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// Id
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// �̻�ID
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public Int32? StockUpPeriod { get; set; }

		/// <summary>
		/// �ŵ깤����ʼʱ��
		/// </summary>
		public DateTime? BeginWorkTime { get; set; }

		/// <summary>
		/// �ŵ깤������ʱ��
		/// </summary>
		public DateTime? EndWorkTime { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public Int32? MaxDelivery { get; set; }

		/// <summary>
		/// ״̬1����0ͣ��
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// �޸���
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
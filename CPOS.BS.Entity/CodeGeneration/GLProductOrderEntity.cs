/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/8 11:20:05
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
    public partial class GLProductOrderEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public GLProductOrderEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String ProductOrderID { get; set; }

		/// <summary>
		/// ������ˮ�ţ������ã�
		/// </summary>
		public String ProductOrderSN { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// �ͻ�����
		/// </summary>
		public String CustomerName { get; set; }

		/// <summary>
		/// �ͻ���ַ
		/// </summary>
		public String CustomerAddress { get; set; }

		/// <summary>
		/// �ͻ��绰
		/// </summary>
		public String CustomerPhone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CustomerGender { get; set; }

		/// <summary>
		/// CustomerID
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// ɾ����ʶ
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/14 19:49:53
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
    public partial class OrderIntegralEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public OrderIntegralEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ����ID
		/// </summary>
		public String OrderIntegralID { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public String OrderNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public String ItemID { get; set; }

		/// <summary>
        /// �һ�����
		/// </summary>
		public Int32? Quantity { get; set; }

		/// <summary>
        /// ��Ʒ����
		/// </summary>
		public Decimal? Integral { get; set; }

		/// <summary>
        /// �ܻ���
		/// </summary>
		public Decimal? IntegralAmmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
        /// �ջ���
		/// </summary>
		public String LinkMan { get; set; }

		/// <summary>
        /// �ջ��绰
		/// </summary>
		public String LinkTel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CityID { get; set; }

		/// <summary>
        /// �ջ���ַ
		/// </summary>
		public String Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
        /// �µ�ʱ��
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
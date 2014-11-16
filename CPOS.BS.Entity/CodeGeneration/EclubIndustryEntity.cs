/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 15:25:07
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
    public partial class EclubIndustryEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EclubIndustryEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ����ID
		/// </summary>
		public Guid? IndustryID { get; set; }

		/// <summary>
        /// �û�ID
		/// </summary>
		public String ParentID { get; set; }

		/// <summary>
        /// ��ҵ�ȼ����ֱ�Ϊ1.��һ��,2.�ڶ���
		/// </summary>
		public Int32? IndustryType { get; set; }

		/// <summary>
        /// ��ҵ����
		/// </summary>
		public String IndustryName { get; set; }

		/// <summary>
        /// ��ҵ����
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
        /// ��ҵ����
		/// </summary>
		public String Description { get; set; }

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
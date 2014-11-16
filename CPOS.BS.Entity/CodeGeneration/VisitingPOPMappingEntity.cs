/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:48
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
    /// ʵ�壺 �ݷö���(�ŵ�/������) 
    /// </summary>
    public partial class VisitingPOPMappingEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingPOPMappingEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? MappingID { get; set; }

		/// <summary>
		/// �ݷ�������(����VisitingTask��)
		/// </summary>
		public Guid? VisitingTaskID { get; set; }

		/// <summary>
		/// �ݷö���(�ն˻�����)
		/// </summary>
		public string POPID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete { get; set; }


        #endregion

    }
}
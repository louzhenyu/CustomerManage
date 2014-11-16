/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/23 18:00:11
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
    /// ʵ�壺 �ݷò����еĶ��� 
    /// </summary>
    public partial class VisitingTaskStepObjectEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingTaskStepObjectEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? ObjectID { get; set; }

		/// <summary>
		/// �ݷò�����(����VisitingTaskStep��)
		/// </summary>
		public Guid? VisitingTaskStepID { get; set; }

		/// <summary>
		/// �ݷö�����(�����ŵ�/��Ʒ/Ʒ��/���ȱ�)
		/// </summary>
		public string Target1ID { get; set; }

		/// <summary>
		/// �ݷö�����(���ڰݷö���ΪƷ��Ĳ���:Ʒ��/���ȱ�)
		/// </summary>
		public string Target2ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Level { get; set; }

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
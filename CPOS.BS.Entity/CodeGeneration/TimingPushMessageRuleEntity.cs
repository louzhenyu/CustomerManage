/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 14:13:25
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
    /// ʵ�壺 ��ʱ���͹��� 
    /// </summary>
    public partial class TimingPushMessageRuleEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TimingPushMessageRuleEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// TimingPushMessageRuleID
		/// </summary>
		public Guid? TimingPushMessageRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CSPipelineID { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// ��ػID
		/// </summary>
		public String EventID { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? ActiveTime { get; set; }

		/// <summary>
		/// ClientID
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// CreateBy
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// CreateTime
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// LastUpdateBy
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// LastUpdateTime
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// IsDelete
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
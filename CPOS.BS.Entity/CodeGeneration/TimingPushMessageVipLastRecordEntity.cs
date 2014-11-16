/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 14:13:26
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
    /// ʵ�壺 ����ͼ�¼ 
    /// </summary>
    public partial class TimingPushMessageVipLastRecordEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TimingPushMessageVipLastRecordEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? TimingPushMessageVipLastRecordID { get; set; }

		/// <summary>
		/// TimingPushMessageRuleID
		/// </summary>
		public Guid? TimingPushMessageRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CSPipelineID { get; set; }

		/// <summary>
		/// �û�ID
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
		/// ���ģ��ID
		/// </summary>
		public String LastModelID { get; set; }

		/// <summary>
		/// �������ID
		/// </summary>
		public String LastContentID { get; set; }

		/// <summary>
		/// ����������
		/// </summary>
		public Int32? LastContentIndex { get; set; }

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
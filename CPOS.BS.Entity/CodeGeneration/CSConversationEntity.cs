/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/5 18:00:39
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
    /// ʵ�壺 ������¼ 
    /// </summary>
    public partial class CSConversationEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public CSConversationEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? CSConversationID { get; set; }

		/// <summary>
		/// CSMessageID
		/// </summary>
		public Guid? CSMessageID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? CSQueueID { get; set; }

		/// <summary>
		/// ��Ϣ����
		/// </summary>
		public Int32? MessageTypeID { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// ������ID
		/// </summary>
		public String PersonID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String Person { get; set; }

		/// <summary>
		/// �Ƿ��ǿͷ�
		/// </summary>
		public Int32? IsCS { get; set; }

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public Int32? IsPush { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public Int32? ContentTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String HeadImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int64? TimeStamp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OpenId { get; set; }


        #endregion

    }
}
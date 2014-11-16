/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/11 13:28:17
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
    public partial class LNewsCommentEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LNewsCommentEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ����ID
		/// </summary>
		public Guid? NewsCommentId { get; set; }

		/// <summary>
        /// ��ѯID
		/// </summary>
		public string NewsId { get; set; }

		/// <summary>
        /// ������ID
		/// </summary>
		public string VIPId { get; set; }

		/// <summary>
        /// ��������
		/// </summary>
		public string Content { get; set; }

		/// <summary>
        /// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
        /// ������
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
        /// ����޸���
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
        /// ����޸�ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
        /// �Ƿ�ɾ��
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
        /// �ͻ�ID
		/// </summary>
		public string CustomerId { get; set; }


        #endregion

    }
}
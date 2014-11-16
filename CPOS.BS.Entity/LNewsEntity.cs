/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 15:46:07
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
    public partial class LNewsEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ���� 
        /// </summary>
        public Int64 displayIndex { get; set; }

        public Decimal DisplayindexOrder { get; set; }

        public Int64 DisplayIndexRow { get; set; }
        /// <summary>
        /// ������ 
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// ���� 
        /// </summary>
        public IList<LNewsEntity> EntityList { get; set; }
        /// <summary>
        /// ������ 
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public Int32? ClickCount { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string NewsTypeName { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string StrPublishTime { get; set; }
        /// <summary>
        /// �ظ�����
        /// </summary>
        public int ReplyCount { get; set; }
        /// <summary>
        /// ��ǩ�ַ���
        /// </summary>
        public string StrTags { get; set; }
      
     

        /// <summary>
        /// �����
        /// </summary>
        public int? BrowseNum { get; set; }
        /// <summary>
        /// �ղ���
        /// </summary>
        public int? BookmarkNum { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int? PraiseNum { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int? ShareNum { get; set; }
        #endregion
    }
}
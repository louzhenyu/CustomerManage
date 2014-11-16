/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 9:59:18
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
    public partial class ZLargeForumEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// DisplayIndex
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// �γ̱�ʶ
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// ��ʼʱ���ַ���
        /// </summary>
        public string StartTimeText { get; set; }
        /// <summary>
        /// ��ֹʱ���ַ���
        /// </summary>
        public string EndTimeText { get; set; }
        /// <summary>
        /// ForumTypeIds
        /// </summary>
        public string[] ForumTypeIds { get; set; }
        /// <summary>
        /// CourseIds
        /// </summary>
        public string[] CourseIds { get; set; }
        /// <summary>
        /// CourseName
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// ImageUrl
        /// </summary>
        public string ImageUrl { get; set; }

        public string ForumTypeName { get; set; }
        #endregion
    }
}
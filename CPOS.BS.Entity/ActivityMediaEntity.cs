/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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
    public partial class ActivityMediaEntity : BaseEntity ,IExtensionable
    {
        #region ���Լ�
        /// <summary>
        /// ����⣬�������
        /// </summary>
        public string ActivityTitle { get; set; }
        /// <summary>
        /// �ļ���������������
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// �ļ�·��������������
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// ý�������ı�
        /// </summary>
        public string MediaTypeText { get; set; }
        /// <summary>
        /// ���� 
        /// </summary>
        public Int64 displayIndex { get; set; }
        /// <summary>
        /// ������ 
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// ���� 
        /// </summary>
        public IList<ActivityMediaEntity> EntityList { get; set; }
        #endregion
    }
}
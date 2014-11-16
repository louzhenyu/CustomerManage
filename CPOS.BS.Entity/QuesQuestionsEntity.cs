/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/7 10:56:10
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
    public partial class QuesQuestionsEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ����ѡ���
        /// </summary>
        public IList<QuesOptionEntity> QuesOptionEntityList { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// ��ǰ�û��Ƿ��ύ�˸�����
        /// </summary>
        public int IsFinished { get; set; }
        public string QuestionTypeDesc { get; set; }
        public int OptionsCount { get; set; }
        #endregion
    }
}
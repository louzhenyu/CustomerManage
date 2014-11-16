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
    /// 实体：  
    /// </summary>
    public partial class QuesQuestionsEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 问题选项集合
        /// </summary>
        public IList<QuesOptionEntity> QuesOptionEntityList { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// 当前用户是否提交了该问题
        /// </summary>
        public int IsFinished { get; set; }
        public string QuestionTypeDesc { get; set; }
        public int OptionsCount { get; set; }
        #endregion
    }
}
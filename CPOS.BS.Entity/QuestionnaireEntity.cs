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
    public partial class QuestionnaireEntity : BaseEntity 
    {
        #region 属性集
        /// <summary>
        /// 问题数量
        /// </summary>
        public int QuestionCount { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 当前用户是否完成了该问卷。 0，1
        /// </summary>
        public int IsFinished { get; set; }

        /// <summary>
        /// 问题集合
        /// </summary>
        public IList<QuesQuestionsEntity> QuesQuestionEntityList { get; set; }
        /// <summary>
        /// 答案集合
        /// </summary>
        public IList<QuesOptionEntity> QuesOptionEntityList { get; set; }
        #endregion
    }
}
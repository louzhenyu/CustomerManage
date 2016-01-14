using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Request
{
    public class GetQuestionnaireListRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 问卷名称
        /// </summary>
        public string QuestionnaireName { get; set; }
        /// <summary>
        /// 类别，1-问答，2-投票，3-测试，4-报名
        /// </summary>
        public int QuestionnaireType { get; set; }

        /// <summary>
        /// 每页记录数，默认15
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码，默认0
        /// </summary>
        public int PageIndex { get; set; }

        #endregion

        public void Validate()
        {
            
        } 
    }
}

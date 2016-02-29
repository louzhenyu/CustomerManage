using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Request
{
    public class DelQuestionnaireAnswerRecordRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 标识
        /// </summary>
        public string[] VipIDs { get; set; }

        #endregion

        public void Validate()
        {
           
        }
    }
}

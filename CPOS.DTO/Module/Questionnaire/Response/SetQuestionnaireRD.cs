using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Response
{
    public class SetQuestionnaireRD : IAPIResponseData
    {

        /// <summary>
        /// 标识
        /// </summary>
        public Guid? QuestionnaireID { get; set; } 
       

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Request
{
    public class DelQuestionnaireRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 问卷标识
        /// </summary>
        public string QuestionnaireID { get; set; }

        #endregion




        
        public void Validate()
        {
            if (QuestionnaireID == null && QuestionnaireID == "")
            {
                throw new NotImplementedException("问卷标识错误！");
            }
        } 
            
       
    }
}

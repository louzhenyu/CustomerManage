using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Response
{
    public class GetQuestionListRD : IAPIResponseData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 问卷列表集合
        /// </summary>
        public List<Request.Question> QuestionnaireList { get; set; }
    }


}

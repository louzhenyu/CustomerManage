using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Extension.Question.Response
{
    public class GetQuestionListRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
        public QuestionInfo[] QuestionList { get; set; }
    }

    public class QuestionInfo
    {
        public Guid? QuestionID { get; set; }
        public string Name { get; set; }
        public string NameUrl { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option1ImageUrl { get; set; }
        public string Option2ImageUrl { get; set; }
        public string Option3ImageUrl { get; set; }
        public string Option4ImageUrl { get; set; }
        public int? Answer { get; set; }
        public int? IsMultiple { get; set; }
    }

}

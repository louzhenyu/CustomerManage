using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Response
{
    public class GetQuestionnaireListRD : IAPIResponseData
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
        public List<Questionnaireinfo> QuestionnaireList { get; set; } 
    }

    public class Questionnaireinfo
    {
        /// <summary>
        /// 问卷id
        /// </summary>
        public String QuestionnaireID { get; set; }

        /// <summary>
        /// 问卷名称
        /// </summary>
        public String QuestionnaireName { get; set; }

       

        /// <summary>
        /// 问卷类型
        /// </summary>
        public int QuestionnaireType { get; set; }

        /// <summary>
        /// 问卷类型
        /// </summary>
        public string QuestionnaireTypeName {
            get{
                String TypeName="";
                switch(QuestionnaireType)
                {
                    case 1: TypeName = "问答"; break;
                    case 2: TypeName = "投票"; break;
                    case 3: TypeName = "测试"; break;
                    case 4: TypeName = "报名"; break;
                    default: TypeName = ""; break;
                }
                return TypeName;
            }
            set { 
                
            } 
        }
       
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

       
    }



    
}

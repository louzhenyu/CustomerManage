using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Response
{
    public class GetScoreRecoveryInformationListRD : IAPIResponseData
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
        public List<ScoreRecoveryInformation> ScoreRecoveryInformationList { get; set; } 
    }

    public class ScoreRecoveryInformation
    {
        /// <summary>
        /// 标识
        /// </summary>
        public String ScoreRecoveryInformationID { get; set; }


        /// <summary>
        /// 问卷id
        /// </summary>
        public String QuestionnaireID { get; set; }

        /// <summary>
        /// 最小分值
        /// </summary>
        public int MinScore { get; set; }



        /// <summary>
        /// 最大分值    
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// 回复类型
        /// </summary>
        public int RecoveryType { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public String RecoveryContent { get; set; }

        /// <summary>
        /// 回复图片
        /// </summary>
        public String RecoveryImg { get; set; }

       
       
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }


    }
}

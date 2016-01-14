using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Response
{
    public class GetScoreRecoveryInformationRD : IAPIResponseData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? ScoreRecoveryInformationID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionnaireID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? MinScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? MaxScore { get; set; }

        /// <summary>
        /// 1,文字   2,图片
        /// </summary>
        public Int32? RecoveryType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RecoveryContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RecoveryImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

       

        #endregion
    }
}

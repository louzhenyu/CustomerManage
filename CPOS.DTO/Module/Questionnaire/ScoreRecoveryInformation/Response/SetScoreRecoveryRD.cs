using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.ScoreRecoveryInformation.Response
{
    public class SetScoreRecoveryRD : IAPIResponseData
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid? ScoreRecoveryInformationID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int TesultValue { get; set; } 
    }
}

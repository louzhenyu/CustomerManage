using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.ScoreRecoveryInformation.Request
{
    public class DelScoreRecoveryRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 标识
        /// </summary>
        public string ScoreRecoveryInformationID { get; set; }

        #endregion





        public void Validate()
        {
            if (ScoreRecoveryInformationID == null && ScoreRecoveryInformationID == "")
            {
                throw new NotImplementedException("标识错误！");
            }
        } 
    }
}

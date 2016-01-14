using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.ActivityQuestionnaireMapping.Response
{
    public class GetActivityIDAndQuestionnaireIDRD : IAPIResponseData
    {
        #region 属性集

        /// <summary>
        /// 
        /// </summary>
        public string QuestionnaireName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataTable ResultData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TitleName[] TitleData { get; set; }

        #endregion
    }

    public class TitleName {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NameID { get; set; }

        #endregion
    }
}

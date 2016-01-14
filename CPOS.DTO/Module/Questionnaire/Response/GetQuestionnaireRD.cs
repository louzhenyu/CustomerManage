using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.Response
{
    public class GetQuestionnaireRD : IAPIResponseData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Int32? QuestionnaireType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QRegular { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsShowQRegular { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ButtonName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String BGImageSrc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String StartPageBtnBGColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String StartPageBtnTextColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QResultTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QResultBGImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QResultImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QResultBGColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QResultBtnTextColor { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public Int32? ModelType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionnaireName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? QuestionnaireID { get; set; }





        #endregion

    }
}

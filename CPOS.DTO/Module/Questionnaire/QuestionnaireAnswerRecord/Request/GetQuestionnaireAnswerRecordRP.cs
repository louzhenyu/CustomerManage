using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Request
{
    public class GetQuestionnaireAnswerRecordRP : IAPIRequestParameter
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? QuestionnaireAnswerRecordID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionnaireID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionnaireName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ActivityID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ActivityName { get; set; }


        


        /// <summary>
        /// 
        /// </summary>
        public List<QuestionnaireAnswerRecord> QuestionnaireAnswerRecordlist { get; set; }

        

        #endregion

        public void Validate()
        {
            
        }
    }

    public class option
    {
        /// <summary>
        /// 
        /// </summary>
        public String OptionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OptionName { get; set; }
    }

    public class QuestionnaireAnswerRecord
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? QuestionnaireAnswerRecordID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionnaireID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionnaireName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ActivityID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ActivityName { get; set; }

        /// <summary>
        /// 1.单行输入   2.多行输入   3.单选   4.多选   5.下拉框   6.手机号   7.地址   8.日期   9.图片单选   10.图片多选
        /// </summary>
        public Int32 QuestionidType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerOption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerProvince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerCity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerCounty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AnswerAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? QuestionScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SumScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<option> optionlist { get; set; }

        #endregion
    }
}

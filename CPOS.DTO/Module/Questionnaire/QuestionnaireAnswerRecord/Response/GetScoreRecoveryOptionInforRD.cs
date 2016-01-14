using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Response
{
    public class GetScoreRecoveryOptionInforRD : IAPIResponseData
    {

        /// <summary>
        /// 问题集合
        /// </summary>
        public List<Question> Questionlist { get; set; }
    }

    public class Question 
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? Questionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }


        /// <summary>
        /// 1.单行输入   2.多行输入   3.单选   4.多选   5.下拉框   6.手机号   7.地址   8.日期   9.图片单选   10.图片多选
        /// </summary>
        public Int32 QuestionidType { get; set; }

        /// <summary>
        /// 选项集合
        /// </summary>
        public List<Option> Optionlist { get; set; }

        #endregion
    }

    public class Option
    {
        #region 属性集

        /// <summary>
        /// 
        /// </summary>
        public string OptionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OptionName { get; set; }

        
         /// <summary>
        /// 
        /// </summary>
        public int SelectedCount { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string SelectedPercent { get; set; }

        #endregion
    }
}

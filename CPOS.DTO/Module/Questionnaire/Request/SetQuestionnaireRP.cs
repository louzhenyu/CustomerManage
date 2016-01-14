using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.DTO.Module.Questionnaire.Request
{
    public class SetQuestionnaireRP : IAPIRequestParameter
    {
        #region 属性

        /// <summary>
        /// 标识
        /// </summary>
        public string QuestionnaireID { get; set; }

        /// <summary>
        /// 第几步
        /// </summary>
        public int step { get; set; }

        /// <summary>
        /// 问卷名称
        /// </summary>
        public string QuestionnaireName { get; set; }
        /// <summary>
        /// 类别，1-问答，2-投票，3-测试，4-报名
        /// </summary>
        public int QuestionnaireType { get; set; }

        /// <summary>
        /// 模版类型 1.单题样式 2.瀑布样式
        /// </summary>
        public int ModelType { get; set; }

        /// <summary>
        /// 是否显示规则
        /// </summary>
        public int IsShowQRegular { get; set; } 

        /// <summary>
        /// 问卷规则
        /// </summary>
        public string QRegular { get; set; }

        /// <summary>
        /// 开始页按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 开始页背景图片地址
        /// </summary>
        public string BGImageSrc { get; set; }

        /// <summary>
        /// 开始页按钮背景颜色
        /// </summary>
        public string StartPageBtnBGColor { get; set; }

        /// <summary>
        /// 开始页按钮字体颜色
        /// </summary>
        public string StartPageBtnTextColor { get; set; }

        /// <summary>
        /// 问卷结果标题
        /// </summary>
        public string QResultTitle { get; set; }

        /// <summary>
        /// 问卷结果背景图片
        /// </summary>
        public string QResultBGImg { get; set; }

        /// <summary>
        /// 问卷结果插图
        /// </summary>
        public string QResultImg { get; set; }

        /// <summary>
        /// 问卷结果按钮背景颜色
        /// </summary>
        public string QResultBGColor { get; set; }

        /// <summary>
        /// 问卷结果按钮字体颜色
        /// </summary>
        public string QResultBtnTextColor { get; set; }







        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerID { get; set; }


        /// <summary>
        /// 题目集合
        /// </summary>
        public List<Question> Questiondatalist { get; set; }

        

        /// <summary>
        /// 删除题目集合
        /// </summary>
        public List<QuestionDel> QuestionDelDatalist { get; set; }

        #endregion

        public void Validate()
        {

        }
    }

    public class QuestionDel
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? Questionid { get; set; }

        #endregion
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
        public Int32 QuestionidType {get;set;}

        /// <summary>
        /// 
        /// </summary>
        public String DefaultValue { get; set; }

        /// <summary>
        /// 1,选中选项获得选项分数   2.答对几项的几分，答错不得分   3.全部答对才得分
        /// </summary>
        public Int32 ScoreStyle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? MinScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? MaxScore { get; set; }

        /// <summary>
        /// 1,必填   0，非必填
        /// </summary>
        public Int32 IsRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsValidateMinChar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 MinChar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsValidateMaxChar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 MaxChar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsShowProvince { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsShowCity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsShowCounty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsShowAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 NoRepeat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsValidateStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 IsValidateEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Isphone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Src { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        public string QuestionPicID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 0,否   1,是
        /// </summary>
        public Int32 IsDelete { get; set; }

        /// <summary>
        /// 选项集合
        /// </summary>
        public List<Option> Optionlist { get; set; }

        /// <summary>
        /// 删除选项集合
        /// </summary>
        public List<OptionDel> OptionDelDatalist { get; set; }



        #endregion
    }

    public class OptionDel
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? OptionID { get; set; }

        #endregion
    }

    public class Option
    {
        #region 属性集

        /// <summary>
        /// 
        /// </summary>
        public Guid? OptionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String QuestionID { get; set; }

        /// <summary>
        /// 1.单行输入   2.多行输入   3.单选   4.多选   5.下拉框   6.手机号   7.地址   8.日期   9.图片单选   10.图片多选
        /// </summary>
        public Int32 QuestionidType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OptionContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OptionPicSrc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 NoOptionScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 YesOptionScore { get; set; }

        /// <summary>
        /// 0,否   1,是
        /// </summary>
        public Int32 IsRightValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }


        /// <summary>
        /// 0,否   1,是
        /// </summary>
        public Int32 IsDelete { get; set; }

        #endregion
    }
}

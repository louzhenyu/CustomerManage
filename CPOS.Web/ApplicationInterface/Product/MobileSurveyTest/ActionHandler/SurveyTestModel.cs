using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler
{
    #region 考试列表项
    /// <summary>
    /// 考试列表项
    /// </summary>
    public class SurveyTestItem
    {
        /// <summary>
        /// 考试ID
        /// </summary>
        public string SurveyTestId { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string QuestionnaireName { set; get; }
        /// <summary>
        /// 类型
        /// Test:1 Survey:0
        /// </summary>
        public int? Type { set; get; }
        /// <summary>
        /// 考试简介
        /// </summary>
        public string QuestionnaireDesc { set; get; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string SurfaceImage { set; get; }
        /// <summary>
        /// 是否允许重做
        /// </summary>
        public int? Redoable { set; get; }
        /// <summary>
        /// 参加人数
        /// </summary>
        public int? JoinNum { set; get; }
        /// <summary>
        /// 通过人数
        /// </summary>
        public int? PassNum { set; get; }
        /// <summary>
        /// 过关分数
        /// </summary>
        public int? PassScore { set; get; }
        /// <summary>
        /// 是否通过
        /// True通过 false未通过
        /// </summary>
        //public bool IsPass { set; get; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? ReleaseTime { set; get; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? EndTime { set; get; }
        /// <summary>
        /// 是否已经截止
        /// True截止 false未截止
        /// </summary>
        public int? IsEnd { set; get; }
        /// <summary>
        /// 当前用户测试状态
        /// 0未考 1通过 2未通过
        /// </summary>
        public int TestStatus { set; get; }
    }
    #endregion

    #region 考题信息
    /// <summary>
    /// 考题信息
    /// </summary>
    public class QuesQuestionItem
    {
        /// <summary>
        /// 考题ID
        /// </summary>
        public string QuestionID { set; get; }
        /// <summary>
        /// 考题索引
        /// </summary>
        public int? DisplayIndexNo { set; get; }
        /// <summary>
        /// 考题文本
        /// </summary>
        public string QuestionDesc { set; get; }
        /// <summary>
        /// 考题类型：单选，多选，主观选择题，填空题，标准打分题
        /// </summary>
        public int? QuestionType { set; get; }
        /// <summary>
        /// 考题媒体
        /// </summary>
        public string QuestionMedia { set; get; }
        /// <summary>
        /// 媒体类型
        /// </summary>
        public int? MediaType { set; get; }
        /// <summary>
        /// 选项列表
        /// </summary>
        public List<QuesOptionItem> QuesOptionList { set; get; }
    }
    #endregion

    #region 考题选项
    /// <summary>
    /// 考题选项
    /// </summary>
    public class QuesOptionItem
    {
        /// <summary>
        /// 选项索引
        /// </summary>
        public string OptionIndex { set; get; }
        /// <summary>
        /// 选项内容
        /// </summary>
        public string OptionsText { set; get; }
        /// <summary>
        /// 选项媒体
        /// </summary>
        public string OptionMedia { set; get; }
        /// <summary>
        /// 媒体类型
        /// </summary>
        public int MediaType { set; get; }

        //public int DisplayIndex { set; get; }
    }
    #endregion

    #region 答题项
    /// <summary>
    /// 答题项
    /// </summary>
    public class AnswerItem
    {
        ///考题ID
        public string QuestionId { set; get; }
        /// <summary>
        /// 用户提交结果
        /// </summary>
        public string Answer { set; get; }
        /// <summary>
        /// 是否正确：1正确 0错误
        /// </summary>
        //public int IsCorrect { set; get; }
        /// <summary>
        /// 考题类型
        /// </summary>
        //public int QuestionType { set; get; }
        /// <summary>
        /// 题索引
        /// </summary>
        //public int DisplayIndexNo { set; get; }
    }
    #endregion

    #region 答题结果项
    /// <summary>
    /// 答题项
    /// </summary>
    public class AnswerResultItem
    {
        ///考题ID
        public string QuestionId { set; get; }
        /// <summary>
        /// 用户提交结果
        /// </summary>
        public string Answer { set; get; }
        /// <summary>
        /// 是否正确：1正确 0错误
        /// </summary>
        public int IsCorrect { set; get; }
        /// <summary>
        /// 考题类型
        /// </summary>
        public int QuestionType { set; get; }
        /// <summary>
        /// 题索引
        /// </summary>
        public int DisplayIndexNo { set; get; }
    }
    #endregion
}
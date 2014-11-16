using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler
{
    #region 课程类别
    /// <summary>
    /// 课程类别
    /// </summary>
    public class CategoryItem
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        public int? CategoryID { set; get; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CategoryName { set; get; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string CategoryNameEn { set; get; }
    }
    #endregion

    #region 课程信息
    /// <summary>
    /// 课程信息
    /// </summary>
    public class OnlineCourse
    {
        public string OnlineCourseId { set; get; }
        public string Topic { set; get; }
        public string Introduction { set; get; }
        public string Icon { set; get; }
        public int AccessCount { set; get; }
        public decimal AverageStar { set; get; }
        public int CourseType { set; get; }
        /// <summary>
        /// 是否收藏 
        /// 0未收藏 1收藏
        /// </summary>
        public string KeepType { set; get; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { set; get; }
    }
    #endregion

    #region 课程详细
    /// <summary>
    /// 课程详细
    /// </summary>
    public class CourseDetail
    {
        /// <summary>
        /// 课程信息
        /// </summary>
        public OnlineCourse OnlineCourse { set; get; }
        /// <summary>
        /// 课件列表
        /// </summary>
        public List<CourseWare> CourseWareList { set; get; }
    }

    #endregion

    #region 课件信息
    /// <summary>
    /// 课件信息
    /// </summary>
    public class CourseWare
    {
        /// <summary>
        /// 课件ID
        /// </summary>
        public string CourseWareId { set; get; }
        /// <summary>
        /// 课件名称
        /// </summary>
        public string CourseWareFile { set; get; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OriginalName { set; get; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtName { set; get; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { set; get; }
        /// <summary>
        /// 可否下载
        /// </summary>
        public int? Downloadable { set; get; }
        /// <summary>
        /// 课件全文本ID
        /// </summary>
        public string ContentId { set; get; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { set; get; }
    }
    #endregion

    #region 评论
    /// <summary>
    /// 评论
    /// </summary>
    public class CommentItem
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public string ItemCommentId { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Topic { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string CommentContent { set; get; }
        /// <summary>
        /// 评分
        /// </summary>
        public int? Star { set; get; }
        /// <summary>
        /// 评论者
        /// </summary>
        public string Reviewer { set; get; }
        /// <summary>
        /// 评论者ID
        /// </summary>
        public string ReviewerID { set; get; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public string CommentDate { set; get; }
    }
    #endregion

    #region 当前用户评论
    /// <summary>
    /// 当前用户评论
    /// </summary>
    public class CurrentCourseComment : CommentItem
    {
        /// <summary>
        /// 是否评论 
        /// True评论 False未评论
        /// </summary>
        public bool IsComment { set; get; }
    }
    #endregion

}
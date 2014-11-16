using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler.LibraryMock
{
    /// <summary>
    /// GetComment的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetComment")]
    public class GetCommentHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetComment(pRequest);
        }

        public string GetComment(string pRequest)
        {
            var rd = new APIResponse<GetCommentRD>();
            var rdData = new GetCommentRD();
            CurrentCourseComment comment = new CurrentCourseComment
            {
                ItemCommentId = "1",
                CommentContent = "评论内容",
                Reviewer = "评论人",
                ReviewerID = "评论人ID",
                Star = 2,
                Topic = "标题",
                IsComment = true
            };
            rdData.CurrentCourseComment = comment;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 查询当前用户对特定课程发表的评论
    public class GetCommentRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetCommentRD : IAPIResponseData
    {
        public CurrentCourseComment CurrentCourseComment { set; get; }
    }
    #endregion
}
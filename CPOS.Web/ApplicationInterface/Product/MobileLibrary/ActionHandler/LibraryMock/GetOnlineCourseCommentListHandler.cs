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
    /// GetOnlineCourseCommentList的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseCommentList")]
    public class GetOnlineCourseCommentListHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseCommentList(pRequest);
        }

        public string GetOnlineCourseCommentList(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseCommentListRD>();
            var rdData = new GetOnlineCourseCommentListRD();
            List<CommentItem> list = new List<CommentItem>();
            CommentItem comment = new CommentItem
            {
                ItemCommentId = "1",
                CommentContent = "评论内容",
                Reviewer = "评论人",
                ReviewerID = "评论人ID",
                Star = 2,
                Topic = "标题"
            };
            list.Add(comment);
            rdData.CommentList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 查询课程评论列表
    public class GetOnlineCourseCommentListRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetOnlineCourseCommentListRD : IAPIResponseData
    {
        public List<CommentItem> CommentList { set; get; }
    }
    #endregion
}
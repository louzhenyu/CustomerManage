using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler
{
    /// <summary>
    /// GetOnlineCourseCommentList的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseCommentList")]
    public class GetOnlineCourseCommentListHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseCommentList(pRequest);
        }

        public string GetOnlineCourseCommentList(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseCommentListRD>();
            var rdData = new GetOnlineCourseCommentListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetOnlineCourseCommentListRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                ItemCommentBLL commentBll = new ItemCommentBLL(loggingSessionInfo);
                DataTable dTbl = commentBll.GetCourseComment(rp.Parameters.OnlineCourseID, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (dTbl != null)
                    rdData.CommentList = DataTableToObject.ConvertToList<CommentItem>(dTbl);
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 查询课程评论列表
    public class GetOnlineCourseCommentListRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
            if (PageSize <= 0) { PageSize = 15; }
        }
    }
    public class GetOnlineCourseCommentListRD : IAPIResponseData
    {
        public List<CommentItem> CommentList { set; get; }
    }
    #endregion
}
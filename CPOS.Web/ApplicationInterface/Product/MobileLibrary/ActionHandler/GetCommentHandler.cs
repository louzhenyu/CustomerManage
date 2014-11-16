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
    /// GetComment的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetComment")]
    public class GetCommentHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetComment(pRequest);
        }

        public string GetComment(string pRequest)
        {
            var rd = new APIResponse<GetCommentRD>();
            var rdData = new GetCommentRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCommentRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                ItemCommentBLL commentBll = new ItemCommentBLL(loggingSessionInfo);
                DataTable dTbl = commentBll.GetItemCommentByUser(rp.Parameters.OnlineCourseID, rp.UserID);
                if (dTbl != null && dTbl.Rows.Count > 0)
                {
                    rdData.CurrentCourseComment = DataTableToObject.ConvertToObject<CurrentCourseComment>(dTbl.Rows[0]);
                    rdData.CurrentCourseComment.IsComment = true;
                }
                else
                    rdData.CurrentCourseComment = new CurrentCourseComment() { IsComment = false };
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

    #region 查询当前用户对特定课程发表的评论
    public class GetCommentRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetCommentRD : IAPIResponseData
    {
        public CurrentCourseComment CurrentCourseComment { set; get; }
    }
    #endregion
}
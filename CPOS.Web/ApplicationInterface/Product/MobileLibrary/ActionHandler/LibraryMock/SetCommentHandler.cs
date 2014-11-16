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
    /// SetComment的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "SetComment")]
    public class SetCommentHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SetComment(pRequest);
        }

        public string SetComment(string pRequest)
        {
            var rd = new APIResponse<SetCommentRD>();
            var rdData = new SetCommentRD();
            rdData.IsSuccess = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 发表评论
    public class SetCommentRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public string ID { set; get; }
        public string Topic { set; get; }
        public string CommentContent { set; get; }
        public int? Star { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Topic)) throw new APIException("Topic不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(CommentContent)) throw new APIException("CommentContent不能为空") { ErrorCode = 102 };
            if (Star == null) throw new APIException("Star不能为空") { ErrorCode = 102 };
        }
    }
    public class SetCommentRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}
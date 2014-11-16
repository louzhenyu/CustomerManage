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
    /// GetOnlineCourseTestInfo的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseTestInfo")]
    public class GetOnlineCourseTestInfoHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseTestInfo(pRequest);
        }

        public string GetOnlineCourseTestInfo(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseTestInfoRD>();
            var rdData = new GetOnlineCourseTestInfoRD();
            rdData.SurveyTestId = "1";
            rdData.SurveyTestDesc = "desc";
            rdData.SurveyTestRemark = "remark";
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 查询课程关联的考试
    public class GetOnlineCourseTestInfoRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetOnlineCourseTestInfoRD : IAPIResponseData
    {
        /// <summary>
        /// 考试ID
        /// </summary>
        public string SurveyTestId { set; get; }
        /// <summary>
        /// 考试说明
        /// </summary>
        public string SurveyTestDesc { set; get; }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string SurveyTestRemark { set; get; }
    }
    #endregion
}
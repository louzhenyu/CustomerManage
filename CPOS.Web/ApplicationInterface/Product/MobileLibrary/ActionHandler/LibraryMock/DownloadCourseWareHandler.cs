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
    /// DownloadCourseWare的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "DownloadCourseWare")]
    public class DownloadCourseWareHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DownloadCourseWare(pRequest);
        }

        public string DownloadCourseWare(string pRequest)
        {
            var rd = new APIResponse<DownloadCourseWareRD>();
            var rdData = new DownloadCourseWareRD();
            rdData.Url = "www.baidu.com/1.doc";
            rdData.DocumentType = "word";
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 查询课程的分类方案
    public class DownloadCourseWareRP : IAPIRequestParameter
    {
        public string CourseWareId { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(CourseWareId)) throw new APIException("CourseWareId不能为空") { ErrorCode = 102 };
        }
    }
    public class DownloadCourseWareRD : IAPIResponseData
    {
        public string Url { set; get; }
        public string DocumentType { set; get; }
    }
    #endregion
}
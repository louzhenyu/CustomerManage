using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler
{
    /// <summary>
    /// DownloadCourseWare的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "DownloadCourseWare")]
    public class DownloadCourseWareHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return DownloadCourseWare(pRequest);
        }

        public string DownloadCourseWare(string pRequest)
        {
            var rd = new APIResponse<DownloadCourseWareRD>();
            var rdData = new DownloadCourseWareRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<DownloadCourseWareRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                MLCourseWareBLL wareBll = new MLCourseWareBLL(loggingSessionInfo);
                MLCourseWareEntity entity = wareBll.GetByID(rp.Parameters.CourseWareId);
                if (entity != null)
                {
                    rdData.Url = entity.CourseWareFile;
                    rdData.DocumentType = entity.ExtName;
                }
                else
                {
                    throw new APIException("未发现下载文件") { ErrorCode = 103 };
                }
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
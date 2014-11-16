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
    /// SearchCourse的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "SearchCourse")]
    public class SearchCourseHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SearchCourse(pRequest);
        }

        public string SearchCourse(string pRequest)
        {
            var rd = new APIResponse<SearchCourseRD>();
            var rdData = new SearchCourseRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<SearchCourseRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                MLOnlineCourseBLL courseBll = new MLOnlineCourseBLL(loggingSessionInfo);
                DataTable dTbl = courseBll.SearchOnlineCourse(rp.Parameters.Keyword, 0, 3000);
                if (dTbl != null)
                    rdData.CourseList = DataTableToObject.ConvertToList<OnlineCourse>(dTbl);
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

    #region 根据关键字（在课程名中）模糊查询课程
    public class SearchCourseRP : IAPIRequestParameter
    {
        public string Keyword { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Keyword)) throw new APIException("Keyword不能为空") { ErrorCode = 102 };
        }
    }
    public class SearchCourseRD : IAPIResponseData
    {
        public List<OnlineCourse> CourseList { set; get; }
    }
    #endregion
}
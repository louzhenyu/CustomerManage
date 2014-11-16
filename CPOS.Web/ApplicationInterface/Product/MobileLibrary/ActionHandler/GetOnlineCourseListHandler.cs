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
    /// GetOnlineCourseList的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseList")]
    public class GetOnlineCourseListHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseList(pRequest);
        }
        public string GetOnlineCourseList(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseListRD>();
            var rdData = new GetOnlineCourseListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetOnlineCourseListRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                //判断：courseType<=0查询所有
                string courseType = string.Empty;
                if (rp.Parameters.CourseType > 0)
                    courseType = rp.Parameters.CourseType.ToString();

                MLOnlineCourseBLL courseBll = new MLOnlineCourseBLL(loggingSessionInfo);
                DataTable dTbl = courseBll.GetOnlineCourse(courseType, rp.Parameters.SortKey, rp.Parameters.SortOrientation, rp.Parameters.PageIndex, rp.Parameters.PageSize);
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

    #region 查询课程列表
    public class GetOnlineCourseListRP : IAPIRequestParameter
    {
        public int CourseType { set; get; }
        public string SortKey { set; get; }
        public string SortOrientation { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SortKey)) throw new APIException("SortKey不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(SortOrientation)) throw new APIException("SortOrientation不能为空") { ErrorCode = 102 };
            if (PageSize <= 0) { PageSize = 15; }
        }
    }
    public class GetOnlineCourseListRD : IAPIResponseData
    {
        public List<OnlineCourse> CourseList { set; get; }
    }
    #endregion
}
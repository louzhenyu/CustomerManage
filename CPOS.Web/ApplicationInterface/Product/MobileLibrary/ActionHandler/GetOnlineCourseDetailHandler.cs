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
    /// GetOnlineCourseDetail的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseDetail")]
    public class GetOnlineCourseDetailHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseDetail(pRequest);
        }

        public string GetOnlineCourseDetail(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseDetailRD>();
            var rdData = new GetOnlineCourseDetailRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetOnlineCourseDetailRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                MLOnlineCourseBLL courseBll = new MLOnlineCourseBLL(loggingSessionInfo);

                //课程信息
                DataTable dTbl = courseBll.SearchOnlineCourseDetail(rp.Parameters.OnlineCourseID);
                OnlineCourse entity = null;
                if (dTbl != null && dTbl.Rows.Count > 0)
                    entity = DataTableToObject.ConvertToObject<OnlineCourse>(dTbl.Rows[0]);

                //课程附件列表
                List<CourseWare> list = new List<CourseWare>();
                MLCourseWareBLL wareBll = new MLCourseWareBLL(loggingSessionInfo);
                DataTable dTblWare = wareBll.GetCourseWare(rp.Parameters.OnlineCourseID);
                if (dTblWare != null && dTblWare.Rows.Count > 0)
                    list = DataTableToObject.ConvertToList<CourseWare>(dTblWare);

                //构建CourseDetail对象
                CourseDetail detail = new CourseDetail
                {
                    OnlineCourse = entity,
                    CourseWareList = list
                };
                rdData.CourseDetail = detail;

                //添加访问次数
                try
                {
                    MLOnlineCourseEntity courseEntity = courseBll.GetByID(rp.Parameters.OnlineCourseID);
                    if (courseEntity != null)
                    {
                        courseEntity.AccessCount++;
                        courseBll.Update(courseEntity);
                    }
                }
                catch (Exception ex)
                { }
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

    #region 查询课程详情
    public class GetOnlineCourseDetailRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetOnlineCourseDetailRD : IAPIResponseData
    {
        public CourseDetail CourseDetail { set; get; }

    }
    #endregion
}
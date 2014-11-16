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
    /// GetOnlineCourseDetail的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseDetail")]
    public class GetOnlineCourseDetailHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseDetail(pRequest);
        }

        public string GetOnlineCourseDetail(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseDetailRD>();
            var rdData = new GetOnlineCourseDetailRD();
            OnlineCourse course = new OnlineCourse
            {
                OnlineCourseId = "1",
                Topic = "大数据的得与失",
                Icon = "icon",
                AccessCount = 10,
                AverageStar = 66,
                CourseType = 1,
                KeepType = "1"
            };

            List<CourseWare> list = new List<CourseWare>();
            CourseWare ware = new CourseWare
            {
                ContentId = "1",
                CourseWareId = "1",
                CourseWareFile = "大数据的得与失.doc",
                Downloadable = 1,
                ExtName = ".doc",
                Icon = "icon",
                OriginalName = "",
                Size = "2.3M"
            };
            list.Add(ware);
            CourseDetail detail = new CourseDetail
            {
                OnlineCourse = course,
                CourseWareList = list
            };
            rdData.CourseDetail = detail;
            rd.Data = rdData;
            rd.ResultCode = 0;
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
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
    /// SearchCourse的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "SearchCourse")]
    public class SearchCourseHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SearchCourse(pRequest);
        }

        public string SearchCourse(string pRequest)
        {
            var rd = new APIResponse<SearchCourseRD>();
            var rdData = new SearchCourseRD();
            List<OnlineCourse> list = new List<OnlineCourse>();
            OnlineCourse category = new OnlineCourse
            {
                OnlineCourseId = "1",
                Topic = "大数据的得与失",
                Icon = "icon",
                AccessCount = 10,
                AverageStar = 66,
                CourseType = 1,
                KeepType = "0"
            };
            list.Add(category);
            rdData.CourseList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
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
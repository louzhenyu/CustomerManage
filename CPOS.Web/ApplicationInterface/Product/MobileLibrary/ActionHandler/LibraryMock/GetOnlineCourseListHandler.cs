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
    /// GetOnlineCourseList的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseList")]
    public class GetOnlineCourseListHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseList(pRequest);
        }
        public string GetOnlineCourseList(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseListRD>();
            var rdData = new GetOnlineCourseListRD();
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

    #region 查询课程列表
    public class GetOnlineCourseListRP : IAPIRequestParameter
    {
        public int? CourseType { set; get; }
        public string SortKey { set; get; }
        public string SortOrientation { set; get; }
        public int? PageIndex { set; get; }
        public int? PageSize { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SortKey)) throw new APIException("SortKey不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(SortOrientation)) throw new APIException("SortOrientation不能为空") { ErrorCode = 102 };
            if (PageIndex == null) { PageIndex = 0; }
            if (PageSize == null) { PageSize = 15; }
        }
    }
    public class GetOnlineCourseListRD : IAPIResponseData
    {
        public List<OnlineCourse> CourseList { set; get; }
    }
    #endregion
}
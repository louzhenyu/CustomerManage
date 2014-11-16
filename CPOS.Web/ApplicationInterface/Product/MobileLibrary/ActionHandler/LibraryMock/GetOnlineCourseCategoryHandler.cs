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
    /// GetOnlineCourseCategory的摘要说明
    /// </summary>
    [Export(typeof(ILibraryMockRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseCategory")]
    public class GetOnlineCourseCategoryHandler : ILibraryMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseCategory(pRequest);
        }

        public string GetOnlineCourseCategory(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseCategoryRD>();
            var rdData = new GetOnlineCourseCategoryRD();
            List<CategoryItem> list = new List<CategoryItem>();
            CategoryItem category = new CategoryItem
            {
                CategoryID = 1,
                CategoryName = "科普类",
                CategoryNameEn = "kepulei"
            };
            list.Add(category);
            rdData.CategoryList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 查询课程的分类方案
    public class GetOnlineCourseCategoryRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetOnlineCourseCategoryRD : IAPIResponseData
    {
        public List<CategoryItem> CategoryList { set; get; }
    }
    #endregion
}
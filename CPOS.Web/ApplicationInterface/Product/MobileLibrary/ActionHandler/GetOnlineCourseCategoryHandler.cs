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
    /// GetOnlineCourseCategory的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseCategory")]
    public class GetOnlineCourseCategoryHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseCategory(pRequest);
        }

        public string GetOnlineCourseCategory(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseCategoryRD>();
            var rdData = new GetOnlineCourseCategoryRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetOnlineCourseCategoryRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                OptionsBLL opBll = new OptionsBLL(loggingSessionInfo);
                string optionName = "MLCourseCategory";
                DataTable dTbl = opBll.GetCategory(optionName);
                if (dTbl != null)
                    rdData.CategoryList = DataTableToObject.ConvertToList<CategoryItem>(dTbl);
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
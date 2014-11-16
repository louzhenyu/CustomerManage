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
    /// GetOnlineCourseTestInfo的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "GetOnlineCourseTestInfo")]
    public class GetOnlineCourseTestInfoHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetOnlineCourseTestInfo(pRequest);
        }

        public string GetOnlineCourseTestInfo(string pRequest)
        {
            var rd = new APIResponse<GetOnlineCourseTestInfoRD>();
            var rdData = new GetOnlineCourseTestInfoRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetOnlineCourseTestInfoRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                MLSurveyRelationBLL surveyBll = new MLSurveyRelationBLL(loggingSessionInfo);
                DataTable dTbl = surveyBll.GetSurveyTestInfo(rp.Parameters.OnlineCourseID);
                if (dTbl != null && dTbl.Rows.Count > 0)
                {
                    rdData = DataTableToObject.ConvertToObject<GetOnlineCourseTestInfoRD>(dTbl.Rows[0]);
                }
                else
                {
                    throw new APIException("未发现与课程关联的考试") { ErrorCode = 103 };
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

    #region 查询课程关联的考试
    public class GetOnlineCourseTestInfoRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
        }
    }
    public class GetOnlineCourseTestInfoRD : IAPIResponseData
    {
        /// <summary>
        /// 考试ID
        /// </summary>
        public string SurveyTestId { set; get; }
        /// <summary>
        /// 考试说明
        /// </summary>
        public string SurveyTestDesc { set; get; }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string SurveyTestRemark { set; get; }
    }
    #endregion
}
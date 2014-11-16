using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler
{
    /// <summary>
    /// GetSurveyTestDetail的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestRequestHandler))]
    [ExportMetadata("Action", "GetSurveyTestDetail")]
    public class GetSurveyTestDetailHandler : ISurveyTestRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetSurveyTestDetail(pRequest);
        }
        public string GetSurveyTestDetail(string pRequest)
        {
            var rd = new APIResponse<GetSurveyTestDetailRD>();
            var rdData = new GetSurveyTestDetailRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetSurveyTestDetailRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                SurveyTestDataAccess surveyTestManager = new SurveyTestDataAccess(loggingSessionInfo);
                rdData.SurveyTestInfo = surveyTestManager.GetQuestionnaireDetail(rp.Parameters.SurveyTestId);
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

    #region 查询考试详情
    public class GetSurveyTestDetailRP : IAPIRequestParameter
    {
        public string SurveyTestId { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SurveyTestId)) throw new APIException("SurveyTestId不能为空") { ErrorCode = 102 };
        }
    }
    public class GetSurveyTestDetailRD : IAPIResponseData
    {
        public SurveyTestItem SurveyTestInfo { set; get; }
    }
    #endregion
}
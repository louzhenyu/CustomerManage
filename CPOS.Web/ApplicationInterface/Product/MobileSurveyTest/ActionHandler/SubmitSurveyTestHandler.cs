using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler
{
    /// <summary>
    /// SubmitSurveyTest的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestRequestHandler))]
    [ExportMetadata("Action", "SubmitSurveyTest")]
    public class SubmitSurveyTestHandler : ISurveyTestRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SubmitSurveyTest(pRequest);
        }

        public string SubmitSurveyTest(string pRequest)
        {
            var rd = new APIResponse<SubmitSurveyTestRD>();
            var rdData = new SubmitSurveyTestRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<SubmitSurveyTestRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                decimal last = 0;
                SurveyTestDataAccess surveyTestManager = new SurveyTestDataAccess(loggingSessionInfo);
                int result = surveyTestManager.SaveAnswerSheet(rp.Parameters.SurveyTestId, rp.UserID, rp.Parameters.AnswerList, out last);
                rdData.IsPassed = result;
                rdData.Score = last;
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

    #region 交卷
    public class SubmitSurveyTestRP : IAPIRequestParameter
    {
        public string SurveyTestId { set; get; }

        public List<AnswerItem> AnswerList { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SurveyTestId)) throw new APIException("SurveyTestId不能为空") { ErrorCode = 102 };
            if (AnswerList == null || AnswerList.Count <= 0) throw new APIException("AnswerList不能为空") { ErrorCode = 102 };
        }
    }
    public class SubmitSurveyTestRD : IAPIResponseData
    {
        public int IsPassed { set; get; }
        public decimal? Score { set; get; }
    }
    #endregion
}
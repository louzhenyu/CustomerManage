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
    /// VerifySingleAnswer的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestRequestHandler))]
    [ExportMetadata("Action", "VerifySingleAnswer")]
    public class VerifySingleAnswerHandler : ISurveyTestRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return VerifySingleAnswer(pRequest);
        }

        public string VerifySingleAnswer(string pRequest)
        {
            var rd = new APIResponse<VerifySingleAnswerRD>();
            var rdData = new VerifySingleAnswerRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<VerifySingleAnswerRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                SurveyTestDataAccess surveyTestManager = new SurveyTestDataAccess(loggingSessionInfo);
                int result = surveyTestManager.VerifySingleAnswer(rp.Parameters.QuestionId, rp.Parameters.Answer);
                rdData.IsCorrect = result;
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

    #region 单题验证对错
    public class VerifySingleAnswerRP : IAPIRequestParameter
    {
        public string QuestionId { set; get; }

        public string Answer { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(QuestionId)) throw new APIException("QuestionId不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(Answer)) throw new APIException("Answer不能为空") { ErrorCode = 102 };
        }
    }
    public class VerifySingleAnswerRD : IAPIResponseData
    {
        /// <summary>
        /// 是否正确
        /// </summary>
        public int IsCorrect { set; get; }
    }
    #endregion
}
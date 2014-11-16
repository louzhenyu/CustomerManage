using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler.SurveyTestMock
{
    /// <summary>
    /// VerifySingleAnswer的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestMockRequestHandler))]
    [ExportMetadata("Action", "VerifySingleAnswer")]
    public class VerifySingleAnswerHandler : ISurveyTestMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return VerifySingleAnswer(pRequest);
        }

        public string VerifySingleAnswer(string pRequest)
        {
            var rd = new APIResponse<VerifySingleAnswerRD>();
            var rdData = new VerifySingleAnswerRD();
            rdData.IsCorrect = true;
            rd.Data = rdData;
            rd.ResultCode = 0;
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
        public bool IsCorrect { set; get; }
    }
    #endregion
}
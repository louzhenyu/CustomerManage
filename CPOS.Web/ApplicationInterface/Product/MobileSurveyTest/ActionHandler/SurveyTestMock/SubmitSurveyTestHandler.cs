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
    /// SubmitSurveyTest的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestMockRequestHandler))]
    [ExportMetadata("Action", "SubmitSurveyTest")]
    public class SubmitSurveyTestHandler : ISurveyTestMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SubmitSurveyTest(pRequest);
        }

        public string SubmitSurveyTest(string pRequest)
        {
            var rd = new APIResponse<SubmitSurveyTestRD>();
            var rdData = new SubmitSurveyTestRD();
            rdData.IsPass = true;
            rdData.Score = 98;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 交卷
    public class SubmitSurveyTestRP : IAPIRequestParameter
    {
        public string SurveyTestId { set; get; }

        public List<AnswerItem> AnserList { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SurveyTestId)) throw new APIException("SurveyTestId不能为空") { ErrorCode = 102 };
            if (AnserList == null || AnserList.Count <= 0) throw new APIException("AnserList不能为空") { ErrorCode = 102 };
        }
    }
    public class SubmitSurveyTestRD : IAPIResponseData
    {
        public bool IsPass { set; get; }
        public decimal? Score { set; get; }
    }
    #endregion
}
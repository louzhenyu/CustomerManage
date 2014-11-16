using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler
{
    /// <summary>
    /// GetLastAnswerSheet的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestRequestHandler))]
    [ExportMetadata("Action", "GetLastAnswerSheet")]
    public class GetLastAnswerSheetHandler : ISurveyTestRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetLastAnswerSheet(pRequest);
        }

        public string GetLastAnswerSheet(string pRequest)
        {
            var rd = new APIResponse<GetLastAnswerSheetRD>();
            var rdData = new GetLastAnswerSheetRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetLastAnswerSheetRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                SurveyTestDataAccess surveyTestManager = new SurveyTestDataAccess(loggingSessionInfo);
                MLAnswerSheetEntity entity = surveyTestManager.GetAnswerSheet(rp.Parameters.SurveyTestId, rp.UserID);
                if (entity != null)
                {
                    rdData.TestStatus = entity.IsPassed;
                    rdData.Score = entity.Score;
                    rdData.AnswerList = surveyTestManager.GetAnswerSheetItem(entity.AnswerSheetId, rp.UserID);
                }
                else
                    rdData.TestStatus = -1;
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

    #region 查询考试列表
    public class GetLastAnswerSheetRP : IAPIRequestParameter
    {
        public string SurveyTestId { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(SurveyTestId)) throw new APIException("SurveyTestId不能为空") { ErrorCode = 102 };
        }
    }
    public class GetLastAnswerSheetRD : IAPIResponseData
    {
        /// <summary>
        /// 考试状态（0没考过，1通过，2未通过）
        /// </summary>
        public int? TestStatus { set; get; }
        /// <summary>
        /// 得分结果
        /// </summary>
        public decimal? Score { set; get; }
        /// <summary>
        /// 答题结果列表
        /// </summary>
        public List<AnswerResultItem> AnswerList { set; get; }
    }
    #endregion
}
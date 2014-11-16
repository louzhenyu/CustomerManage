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
    /// GetLastAnswerSheet的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestMockRequestHandler))]
    [ExportMetadata("Action", "GetLastAnswerSheet")]
    public class GetLastAnswerSheetHandler : ISurveyTestMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetLastAnswerSheet(pRequest);
        }

        public string GetLastAnswerSheet(string pRequest)
        {
            var rd = new APIResponse<GetLastAnswerSheetRD>();
            var rdData = new GetLastAnswerSheetRD();

            List<AnswerItem> list = new List<AnswerItem>();
            AnswerItem item = new AnswerItem()
            {
                QuestionId = "1",
                Answer = "A"
            };
            list.Add(item);
            item = new AnswerItem()
            {
                QuestionId = "2",
                Answer = "B"
            };
            list.Add(item);
            rdData.AnswerList = list;
            rdData.Score = 90;
            rdData.TestStatus = 1;

            rd.Data = rdData;
            rd.ResultCode = 0;
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
        public List<AnswerItem> AnswerList { set; get; }
    }
    #endregion
}
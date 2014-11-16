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
    /// GetTestPaper的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestMockRequestHandler))]
    [ExportMetadata("Action", "GetTestPaper")]
    public class GetTestPaperHandler : ISurveyTestMockRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetTestPaper(pRequest);
        }

        public string GetTestPaper(string pRequest)
        {
            var rd = new APIResponse<GetTestPaperRD>();
            var rdData = new GetTestPaperRD();
            List<QuesOptionItem> optionList = new List<QuesOptionItem>();
            QuesOptionItem optionItem = new QuesOptionItem
            {
                OptionIndex = "A",
                OptionsText = "Acontent",
                MediaType = 1,
                OptionMedia = "文本"
            };
            optionList.Add(optionItem);
            optionItem = new QuesOptionItem
            {
                OptionIndex = "B",
                OptionsText = "Bcontent",
                MediaType = 1,
                OptionMedia = "文本"
            };
            optionList.Add(optionItem);
            optionItem = new QuesOptionItem
            {
                OptionIndex = "C",
                OptionsText = "Ccontent",
                MediaType = 1,
                OptionMedia = "文本"
            };
            optionList.Add(optionItem);
            optionItem = new QuesOptionItem
            {
                OptionIndex = "D",
                OptionsText = "Dcontent",
                MediaType = 1,
                OptionMedia = "文本"
            };
            optionList.Add(optionItem);

            QuesQuestionItem item = new QuesQuestionItem
            {
                DisplayIndexNo = 1,
                QuestionDesc = "考题文本",
                QuestionID = "1",
                QuestionType = 1,
                QuesOptionList = optionList
            };
            List<QuesQuestionItem> list = new List<QuesQuestionItem>();
            list.Add(item);
            rdData.QuesQuestionList = list;
            rd.Data = rdData;
            rd.ResultCode = 0;
            return rd.ToJSON();
        }
    }

    #region 获取考卷
    public class GetTestPaperRP : IAPIRequestParameter
    {
        public string SurveyTestId { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(SurveyTestId)) throw new APIException("SurveyTestId不能为空") { ErrorCode = 102 };
        }
    }
    public class GetTestPaperRD : IAPIResponseData
    {
        public List<QuesQuestionItem> QuesQuestionList { set; get; }
    }
    #endregion
}
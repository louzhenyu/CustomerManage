using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;


namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler
{ /// <summary>
    /// GetTestPaper的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestRequestHandler))]
    [ExportMetadata("Action", "GetTestPaper")]
    public class GetTestPaperHandler : ISurveyTestRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetTestPaper(pRequest);
        }

        public string GetTestPaper(string pRequest)
        {
            var rd = new APIResponse<GetTestPaperRD>();
            var rdData = new GetTestPaperRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetTestPaperRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                //获取考题
                QuesQuestionsBLL queBll = new QuesQuestionsBLL(loggingSessionInfo);
                DataTable dTbl = queBll.GetQuesQuestions(rp.Parameters.SurveyTestId);
                List<QuesQuestionItem> quesList = new List<QuesQuestionItem>();
                if (dTbl != null)
                    quesList = DataTableToObject.ConvertToList<QuesQuestionItem>(dTbl);

                //循环获取考题选项
                QuesOptionBLL quesOpBll = new QuesOptionBLL(loggingSessionInfo);
                List<QuesOptionItem> quesOptionList = new List<QuesOptionItem>();
                DataTable dTblOption = null;
                foreach (var qItem in quesList)
                {
                    quesOptionList = new List<QuesOptionItem>();
                    dTblOption = quesOpBll.GetQuesOptions(qItem.QuestionID);
                    if (dTblOption != null && dTblOption.Rows.Count > 0)
                        quesOptionList = DataTableToObject.ConvertToList<QuesOptionItem>(dTblOption);
                    qItem.QuesOptionList = quesOptionList;
                }
                rdData.QuesQuestionList = quesList;
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
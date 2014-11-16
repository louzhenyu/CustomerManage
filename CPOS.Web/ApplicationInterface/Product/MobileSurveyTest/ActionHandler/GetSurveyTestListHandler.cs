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
    /// GetSurveyTestList的摘要说明
    /// </summary>
    [Export(typeof(ISurveyTestRequestHandler))]
    [ExportMetadata("Action", "GetSurveyTestList")]
    public class GetSurveyTestListHandler : ISurveyTestRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return GetSurveyTestList(pRequest);
        }
        public string GetSurveyTestList(string pRequest)
        {
            var rd = new APIResponse<GetSurveyTestListRD>();
            var rdData = new GetSurveyTestListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetSurveyTestListRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                QuestionnaireBLL quesBll = new QuestionnaireBLL(loggingSessionInfo);
                DataTable dTbl = quesBll.GetQuestionnaire(rp.Parameters.Type.ToString(), rp.Parameters.PageIndex, rp.Parameters.PageSize);
                if (dTbl != null)
                    rdData.SurveyTestList = DataTableToObject.ConvertToList<SurveyTestItem>(dTbl);
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

    #region 查询考试列表
    public class GetSurveyTestListRP : IAPIRequestParameter
    {
        public int Type { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }

        public void Validate()
        {
            if (PageSize <= 0) PageSize = 15;
        }
    }
    public class GetSurveyTestListRD : IAPIResponseData
    {
        public List<SurveyTestItem> SurveyTestList { set; get; }
    }
    #endregion
}
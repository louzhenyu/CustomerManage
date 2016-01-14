using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Questionnaire.Request;
using JIT.CPOS.DTO.Module.Questionnaire.Response;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class GetQuestionnaireAH : BaseActionHandler<GetQuestionnaireIDRP, GetQuestionnaireRD>
    {

        protected override GetQuestionnaireRD ProcessRequest(APIRequest<GetQuestionnaireIDRP> pRequest)
        {
            var rd = new GetQuestionnaireRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var QuestionnaireBLL = new T_QN_QuestionnaireBLL(loggingSessionInfo);


            

            var tempList = QuestionnaireBLL.GetByID(para.QuestionnaireID);

            if (tempList != null)
            {
                rd.QuestionnaireType = tempList.QuestionnaireType;
                rd.QRegular = tempList.QRegular;
                rd.IsShowQRegular = tempList.IsShowQRegular;
                rd.QRegular = tempList.QRegular;
                rd.ButtonName = tempList.ButtonName;
                rd.BGImageSrc = tempList.BGImageSrc;
                rd.StartPageBtnBGColor = tempList.StartPageBtnBGColor;
                rd.StartPageBtnTextColor = tempList.StartPageBtnTextColor;
                rd.QResultTitle = tempList.QResultTitle;
                rd.QResultBGImg = tempList.QResultBGImg;
                rd.QResultImg = tempList.QResultImg;
                rd.QResultBGColor = tempList.QResultBGColor;
                rd.QResultBtnTextColor = tempList.QResultBtnTextColor;
                rd.QuestionnaireName = tempList.QuestionnaireName;
                rd.QuestionnaireID = tempList.QuestionnaireID;
                rd.ModelType = tempList.ModelType;
                
            }
            return rd;
        }
    }
}
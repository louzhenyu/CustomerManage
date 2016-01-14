using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Questionnaire.Request;
using JIT.CPOS.DTO.Module.Questionnaire.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class GetQuestionnaireAH : BaseActionHandler<GetActivityIDRP, GetQuestionnaireRD>
    {
        protected override GetQuestionnaireRD ProcessRequest(APIRequest<GetActivityIDRP> pRequest)
        {
            var rd = new GetQuestionnaireRD();
            var para = pRequest.Parameters;
            var QuestionnaireBLL = new T_QN_QuestionnaireBLL(this.CurrentUserInfo);




            var tempList = QuestionnaireBLL.GetByAID(para.ActivityID);

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
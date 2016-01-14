using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;


using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Questionnaire.ActivityQuestionnaireMapping.Request;
using JIT.CPOS.DTO.Module.Questionnaire.ActivityQuestionnaireMapping.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class GetQuestionnaireInforAH : BaseActionHandler<GetActivityIDAndQuestionnaireIDRP, GetActivityIDAndQuestionnaireIDRD>
    {

        protected override GetActivityIDAndQuestionnaireIDRD ProcessRequest(APIRequest<GetActivityIDAndQuestionnaireIDRP> pRequest)
        {
            DataTable dt = new DataTable();
            var rd=new GetActivityIDAndQuestionnaireIDRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var QARMBLL = new T_QN_QuestionnaireAnswerRecordBLL(loggingSessionInfo);
            var QuestionBLL = new T_QN_QuestionBLL(loggingSessionInfo);

            var AQMmodel = new T_QN_ActivityQuestionnaireMappingBLL(loggingSessionInfo).GetByAID(para.ActivityID);


            if (AQMmodel != null)
            {
                rd.QuestionnaireName = AQMmodel.QuestionnaireName;
                var tempList = QARMBLL.GetModelList(AQMmodel.ActivityID, AQMmodel.QuestionnaireID);

                var questionlist = QuestionBLL.getList(AQMmodel.QuestionnaireID);

                rd.TitleData = new TitleName[questionlist.Length+1];
                for (int j = 0; j < questionlist.Length; j++)
                {
                    DataColumn dc = new DataColumn(questionlist[j].Questionid.ToString());
                    if (rd.TitleData[j] == null)
                    {
                        rd.TitleData[j] = new TitleName();
                    }
                    rd.TitleData[j].Name = questionlist[j].Name;
                    rd.TitleData[j].NameID = questionlist[j].Questionid.ToString();
                    dt.Columns.Add(dc);
                }

                if (rd.TitleData[questionlist.Length] == null)
                {
                    rd.TitleData[questionlist.Length] = new TitleName();
                }
                rd.TitleData[questionlist.Length].Name = "提交时间";
                rd.TitleData[questionlist.Length].NameID = "submitdate";
                DataColumn dcsubmitdate = new DataColumn("submitdate");
                dt.Columns.Add(dcsubmitdate);

                var vipdata = QARMBLL.GetUserModelList(AQMmodel.ActivityID, AQMmodel.QuestionnaireID);



                for (int k = 0; k < vipdata.Length; k++)
                {
                    DataRow dr = dt.NewRow();
                    var _vipdata = vipdata[k];
                    var datetime = new DateTime();
                    for (int j = 0; j < questionlist.Length; j++)
                    {
                        var _questindata = questionlist[j];
                        for (int i = 0; i < tempList.Length; i++)
                        {
                            var _data = tempList[i];
                            if (_vipdata.ToString() == _data.VipID.ToString())
                            {
                                if (_questindata.Questionid.ToString() == _data.QuestionID)
                                {
                                    dr[_questindata.Questionid.ToString()] = (_data.AnswerText + _data.AnswerOption + _data.AnswerDate + _data.AnswerProvince + _data.AnswerCity + _data.AnswerCounty + _data.AnswerAddress).TrimEnd(',');
                                    datetime = _data.CreateTime.Value;
                                }
                            }
                        }
                    }
                    dr["submitdate"] = datetime;
                    dt.Rows.Add(dr);
                }



                rd.ResultData = dt;
            }

            return rd;
        }
    }
}
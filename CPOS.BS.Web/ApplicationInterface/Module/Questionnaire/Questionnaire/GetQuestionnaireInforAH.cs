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

            var AQMmodel = new T_QN_ActivityQuestionnaireMappingBLL(loggingSessionInfo).GetByAID(para.ActivityID);


            if (AQMmodel != null)
            {
                rd.QuestionnaireName = AQMmodel.QuestionnaireName;
                rd.QuestionnaireID = AQMmodel.QuestionnaireID;

                TitleName[] TitleData;
                rd.ResultData = GetQuestionnaireInfor(AQMmodel.ActivityID, AQMmodel.QuestionnaireID, out TitleData);
                rd.TitleData = TitleData;
            }

            return rd;
        }

        /// <summary>
        /// 获取答题记录详细信息
        /// </summary>
        /// <param name="ActivityID">活动id</param>
        /// <param name="QuestionnaireID">问卷id</param>
        /// <returns></returns>
        public static DataTable GetQuestionnaireInfor(string ActivityID, string QuestionnaireID, out TitleName[] TitleData)
        {

            DataTable dt = new DataTable();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var QARMBLL = new T_QN_QuestionnaireAnswerRecordBLL(loggingSessionInfo);
            var QuestionBLL = new T_QN_QuestionBLL(loggingSessionInfo);

            var tempList = QARMBLL.GetModelList(ActivityID, QuestionnaireID);

            var questionlist = QuestionBLL.getList(QuestionnaireID);

             TitleData = new TitleName[questionlist.Length + 2];
            for (int j = 0; j < questionlist.Length; j++)
            {
                DataColumn dc = new DataColumn(questionlist[j].Questionid.ToString());
                if (TitleData[j] == null)
                {
                    TitleData[j] = new TitleName();
                }
                TitleData[j].Name = questionlist[j].Name;
                TitleData[j].NameID = questionlist[j].Questionid.ToString();
                dt.Columns.Add(dc);
            }

            if (TitleData[questionlist.Length] == null)
            {
                TitleData[questionlist.Length] = new TitleName();
            }
            TitleData[questionlist.Length].Name = "提交时间";
            TitleData[questionlist.Length].NameID = "submitdate";
            DataColumn dcsubmitdate = new DataColumn("submitdate");
            dt.Columns.Add(dcsubmitdate);

            if (TitleData[questionlist.Length + 1] == null)
            {
                TitleData[questionlist.Length + 1] = new TitleName();
            }
            TitleData[questionlist.Length + 1].Name = "标识";
            TitleData[questionlist.Length + 1].NameID = "ID";
            DataColumn dcID = new DataColumn("ID");
            dt.Columns.Add(dcID);

            var vipdata = QARMBLL.GetUserModelList(ActivityID,QuestionnaireID);



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
                dr["ID"] = _vipdata;
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
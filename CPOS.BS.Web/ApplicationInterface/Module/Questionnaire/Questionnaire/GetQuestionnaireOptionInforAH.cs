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
using JIT.CPOS.DTO.Module.Questionnaire.ActivityQuestionnaireMapping.Request;
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class GetQuestionnaireOptionInforAH : BaseActionHandler<GetActivityIDAndQuestionnaireIDRP, GetScoreRecoveryOptionInforRD>
    {
        protected override GetScoreRecoveryOptionInforRD ProcessRequest(APIRequest<GetActivityIDAndQuestionnaireIDRP> pRequest)
        {
            var rd = new GetScoreRecoveryOptionInforRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var QOCMBLL = new T_QN_QuestionnaireOptionCountBLL(loggingSessionInfo);
            var QuestionBLL = new T_QN_QuestionBLL(loggingSessionInfo);
            var OptionBLL = new T_QN_OptionBLL(loggingSessionInfo);

            //查询活动下的问卷信息
            var AQMmodel = new T_QN_ActivityQuestionnaireMappingBLL(loggingSessionInfo).GetByAID(para.ActivityID);

            if (AQMmodel != null)
            {
                //获取答题选项统计列表
                var QOCList = QOCMBLL.GetList(AQMmodel.QuestionnaireID, para.ActivityID);

                //获取选择类型题目数据集合
                var QuestionEntitys = QuestionBLL.getOptionQuestionList(AQMmodel.QuestionnaireID);

                if (rd.Questionlist == null)
                {
                    rd.Questionlist = new List<Question>();
                }

                //向返回列表增加题目集合
                for (int i = 0; i < QuestionEntitys.Length; i++)
                {
                    var QuestionEntity = QuestionEntitys[i];
                    Question q = new Question();
                    q.Name = QuestionEntity.Name;
                    q.Questionid = QuestionEntity.Questionid;
                    q.QuestionidType = QuestionEntity.QuestionidType.Value;

                    var QuestionOptionlist = OptionBLL.GetListByQuestionID(q.Questionid.ToString());

                    if (q.Optionlist == null)
                    {
                        q.Optionlist = new List<Option>();
                    }

                    int sum = 0;
                    //向返回列表增加题目的选项集合
                    foreach (var _QuestionOption in QuestionOptionlist)
                    {
                        var OptionList = (from list in QOCList
                                          where list.ActivityID == para.ActivityID
                                          where list.OptionID == _QuestionOption.OptionID.ToString()
                                          select new
                                          {
                                              ActivityName = list.ActivityName,
                                              OptionID = list.OptionID,
                                              OptionName = list.OptionName,
                                              QuestionnaireName = list.QuestionnaireName,
                                              QuestionName = list.QuestionName,
                                              SelectedCount = list.SelectedCount
                                          }).ToList();
                        if (OptionList.Count > 0)
                        {
                            Option _Option = new Option();
                            _Option.OptionID = OptionList[0].OptionID;
                            _Option.OptionName = OptionList[0].OptionName;
                            _Option.SelectedCount = OptionList[0].SelectedCount.Value;
                            sum += OptionList[0].SelectedCount.Value;
                            q.Optionlist.Add(_Option);
                        }
                        else
                        {
                            Option _Option = new Option();
                            _Option.OptionID = _QuestionOption.OptionID.ToString();
                            _Option.OptionName = _QuestionOption.OptionContent;
                            _Option.SelectedCount = 0;
                            _Option.SelectedPercent = "0";
                            q.Optionlist.Add(_Option);
                        }
                        
                        

                    }

                    for (int k = 0; k < q.Optionlist.Count; k++)
                    {
                        if (sum >0)
                        {
                            q.Optionlist[k].SelectedPercent = string.Format("{0:0}", Convert.ToDouble(q.Optionlist[k].SelectedCount) * 100 / Convert.ToDouble(sum));
                        }
                    }



                    rd.Questionlist.Add(q);
                }
            }
            return rd;
        }

    }
}
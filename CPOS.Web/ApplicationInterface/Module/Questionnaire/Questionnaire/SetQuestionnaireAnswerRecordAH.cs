using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Request;
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Questionnaire.Questionnaire
{
    public class SetQuestionnaireAnswerRecordAH : BaseActionHandler<GetQuestionnaireAnswerRecordRP, GetScoreRecoveryInformationRD>
    {
        protected override GetScoreRecoveryInformationRD ProcessRequest(APIRequest<GetQuestionnaireAnswerRecordRP> pRequest)
        {
            var rd = new GetScoreRecoveryInformationRD();
            var para = pRequest.Parameters;
            T_QN_QuestionnaireAnswerRecordEntity model_QuestionnaireAnswerRecordEntity = null;


            var QuestionnaireAnswerRecordBLL = new T_QN_QuestionnaireAnswerRecordBLL(this.CurrentUserInfo);
            var QuestionnaireOptionCountBLL = new T_QN_QuestionnaireOptionCountBLL(this.CurrentUserInfo);

            var ScoreRecoveryInformationBLL = new T_QN_ScoreRecoveryInformationBLL(this.CurrentUserInfo);


            if (para != null)
            {
                if (para.VipID == null || para.VipID == "")
                {
                    
                    para.VipID = Guid.NewGuid().ToString();
                   
                }
                var SumScore = 0;
                for (int i = 0; i < para.QuestionnaireAnswerRecordlist.Count; i++)
                {
                    #region 新增答题记录
                    var qarl = para.QuestionnaireAnswerRecordlist[i];
                    model_QuestionnaireAnswerRecordEntity = new T_QN_QuestionnaireAnswerRecordEntity
                    {
                        CustomerID = this.CurrentUserInfo.ClientID,
                        ActivityID = para.ActivityID,
                        ActivityName = para.ActivityName,
                        AnswerAddress = qarl.AnswerAddress,
                        AnswerCity = qarl.AnswerCity,
                        AnswerCounty = qarl.AnswerCounty,
                        AnswerDate = qarl.AnswerDate,
                        AnswerOption = qarl.AnswerOption,
                        AnswerProvince = qarl.AnswerProvince,
                        AnswerText = qarl.AnswerText,
                        QuestionID = qarl.QuestionID,
                        QuestionidType = qarl.QuestionidType,
                        QuestionnaireID = para.QuestionnaireID,
                        QuestionnaireName = para.QuestionnaireName,
                        QuestionName = qarl.QuestionName,
                        QuestionScore = qarl.QuestionScore,
                        VipID = para.VipID,
                        Status = 1

                    };
                    SumScore += qarl.SumScore;

                    QuestionnaireAnswerRecordBLL.Create(model_QuestionnaireAnswerRecordEntity);

                    #endregion

                    #region 答题选项统计
                    if (qarl.optionlist != null)
                    {
                        for (int j = 0; j < qarl.optionlist.Count; j++)
                        {

                            var option = qarl.optionlist[j];
                            if (!(option.OptionID == null || option.OptionID == "" || option.OptionName == null || option.OptionName == ""))
                            {

                                var model_qoce = QuestionnaireOptionCountBLL.isExist(option.OptionID,para.ActivityID);

                                if (model_qoce != null)
                                {
                                    model_qoce.SelectedCount += 1;
                                }
                                else
                                {

                                    model_qoce = new T_QN_QuestionnaireOptionCountEntity();
                                    model_qoce.CustomerID = this.CurrentUserInfo.ClientID;
                                    model_qoce.ActivityID = para.ActivityID;
                                    model_qoce.ActivityName = para.ActivityName;
                                    model_qoce.QuestionID = qarl.QuestionID;
                                    model_qoce.QuestionnaireID = para.QuestionnaireID;
                                    model_qoce.QuestionnaireName = para.QuestionnaireName;
                                    model_qoce.QuestionName = qarl.QuestionName;
                                    model_qoce.OptionID = option.OptionID;
                                    model_qoce.OptionName = option.OptionName;
                                    model_qoce.SelectedCount = 1;

                                }


                                if (model_qoce.QuestionnaireOptionCountID == null)
                                {
                                    QuestionnaireOptionCountBLL.Create(model_qoce);
                                }
                                else
                                {
                                    QuestionnaireOptionCountBLL.Update(model_qoce);
                                }
                            }
                        }
                    }

                    #endregion

                }



                var QuestionnaireAnswerRecordID = model_QuestionnaireAnswerRecordEntity.QuestionnaireAnswerRecordID;

                #region 获取结束页展示数据
                if ( SumScore >0)
                {
                    T_QN_ScoreRecoveryInformationEntity[] list_ScoreRecoveryInformation = ScoreRecoveryInformationBLL.getScoreRecoveryInformationByScore(para.QuestionnaireID, SumScore.ToString());

                    if (list_ScoreRecoveryInformation.Length > 0)
                    {
                        T_QN_ScoreRecoveryInformationEntity tempentity = list_ScoreRecoveryInformation[0];

                        rd.RecoveryContent = tempentity.RecoveryContent;
                        rd.RecoveryImg = tempentity.RecoveryImg;
                        rd.RecoveryType = tempentity.RecoveryType;
                    }
                }
                #endregion
            }
                




            return rd;
        }
    }
}
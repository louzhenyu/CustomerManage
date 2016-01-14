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
    public class SetQuestionnaireListAH : BaseActionHandler<SetQuestionnaireRP, SetQuestionnaireRD>
    {
        protected override SetQuestionnaireRD ProcessRequest(APIRequest<SetQuestionnaireRP> pRequest)
        {

            var rd = new SetQuestionnaireRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_QN_QuestionnaireEntity model_QuestionnaireEntity = null;


            var QuestionBLL = new T_QN_QuestionBLL(loggingSessionInfo);
            var QuestionNaireQuestionMappingBLL = new T_QN_QuestionNaireQuestionMappingBLL(loggingSessionInfo);
            var OptionBLL = new T_QN_OptionBLL(loggingSessionInfo);

            var QuestionPicBLL = new T_QN_QuestionPicBLL(loggingSessionInfo);

            if (para != null )
            {

                #region 问卷编辑

                Guid? tempguid = null;
                if (para.QuestionnaireID != "")
                {
                    tempguid = new System.Guid(para.QuestionnaireID);
                }
                model_QuestionnaireEntity = new T_QN_QuestionnaireEntity
                {
                    BGImageSrc = para.BGImageSrc,
                    QuestionnaireID = tempguid,
                    ButtonName = para.ButtonName,
                    CustomerID = loggingSessionInfo.ClientID,
                    IsShowQRegular = para.IsShowQRegular,
                    ModelType = para.ModelType,
                    QRegular = para.QRegular,
                    QResultBGColor = para.QResultBGColor,
                    QResultBGImg = para.QResultBGImg,
                    QResultBtnTextColor = para.QResultBtnTextColor,
                    QResultImg = para.QResultImg,
                    QResultTitle = para.QResultTitle,
                    QuestionnaireName = para.QuestionnaireName,
                    QuestionnaireType = para.QuestionnaireType,
                    StartPageBtnBGColor = para.StartPageBtnBGColor,
                    StartPageBtnTextColor = para.StartPageBtnTextColor,
                    Sort=999,
                    Status=1

                };

                var QuestionnaireBLL = new T_QN_QuestionnaireBLL(loggingSessionInfo);

                if (para.QuestionnaireID != null && para.QuestionnaireID != "")
                {
                    QuestionnaireBLL.Update(model_QuestionnaireEntity);
                }
                else
                {
                    QuestionnaireBLL.Create(model_QuestionnaireEntity);
                }

                #endregion 

                rd.QuestionnaireID = model_QuestionnaireEntity.QuestionnaireID;


                if (para.step == 3 || para.step == 4)
                {

                    #region 问卷题目删除
                    if (para.step == 3&&para.QuestionDelDatalist!=null)
                    {
                        object[] dels=new object[para.QuestionDelDatalist.Count];
                        for(int j=0;j<para.QuestionDelDatalist.Count;j++)
                        {
                            dels[j]=para.QuestionDelDatalist[j].Questionid;
                        }

                        QuestionBLL.Delete(dels);
                    }
                    #endregion

                    #region 问卷题目编辑
                    if (para.Questiondatalist != null)
                    {
                        for (int i = 0; i < para.Questiondatalist.Count; i++)
                        {
                            Question ques = para.Questiondatalist[i];
                            T_QN_QuestionEntity QuestionEntity = new T_QN_QuestionEntity();

                            T_QN_QuestionPicEntity QuestionPicEntity = new T_QN_QuestionPicEntity();

                            
                            if (para.step == 3)
                            {
                                QuestionEntity.CustomerID = loggingSessionInfo.ClientID;
                                QuestionEntity.DefaultValue = getValue(ques.DefaultValue);
                                QuestionEntity.Isphone = getValue(ques.Isphone);
                                QuestionEntity.IsRequired = getValue(ques.IsRequired);
                                QuestionEntity.IsShowAddress = getValue(ques.IsShowAddress);
                                QuestionEntity.IsShowCity = getValue(ques.IsShowCity);
                                QuestionEntity.IsShowCounty = getValue(ques.IsShowCounty);
                                QuestionEntity.IsShowProvince = getValue(ques.IsShowProvince);
                                QuestionEntity.IsValidateEndDate = getValue(ques.IsValidateEndDate);
                                QuestionEntity.IsValidateMaxChar = getValue(ques.IsValidateMaxChar);
                                QuestionEntity.IsValidateMinChar = getValue(ques.IsValidateMinChar);
                                QuestionEntity.IsValidateStartDate = getValue(ques.IsValidateStartDate);
                                QuestionEntity.MaxChar = getValue(ques.MaxChar);
                                QuestionEntity.MinChar = getValue(ques.MinChar);
                                QuestionEntity.Name = getValue(ques.Name);
                                QuestionEntity.NoRepeat = getValue(ques.NoRepeat);
                                QuestionEntity.Questionid = getValue(ques.Questionid);
                                QuestionEntity.QuestionidType = getValue(ques.QuestionidType);
                                QuestionEntity.StartDate = getValue(ques.StartDate);
                                QuestionEntity.EndDate = getValue(ques.EndDate);
                                QuestionEntity.Sort = i+1;
                                QuestionEntity.Status = getValue(ques.Status);



                            }

                            if (para.step == 4)
                            {
                                QuestionEntity.Questionid = getValue(ques.Questionid);
                                QuestionEntity.ScoreStyle = getValue(ques.ScoreStyle);

                                if (QuestionEntity.ScoreStyle == 3)
                                {
                                    QuestionEntity.MaxScore = getValue(ques.MaxScore);
                                    QuestionEntity.MinScore = getValue(ques.MinScore);
                                }
                            }

                            if (QuestionEntity.Questionid == null || QuestionEntity.Questionid.Value.ToString() == "")
                            {
                                QuestionEntity.ScoreStyle = 1;
                                QuestionBLL.Create(QuestionEntity);
                            }
                            else
                            {
                                QuestionBLL.Update(QuestionEntity,false);
                            }

                            #region 题目图片编辑

                            if(para.step==3)
                            {
                                
                                QuestionPicEntity.Src = getValue(ques.Src);
                                QuestionPicEntity.QuestionID = QuestionEntity.Questionid.Value.ToString();
                                QuestionPicEntity.CustomerID = QuestionEntity.CustomerID;
                                Guid? tempguid1 = null;
                                if (ques.QuestionPicID != "")
                                {
                                    tempguid1 = new System.Guid(ques.QuestionPicID);
                                }
                                QuestionPicEntity.QuestionPicID = tempguid1;
                                if (ques.QuestionPicID != null && ques.QuestionPicID != "")
                                {
                                    QuestionPicBLL.Update(QuestionPicEntity);
                                }
                                else
                                {
                                    QuestionPicBLL.Create(QuestionPicEntity);
                                }
                            }
                            #endregion


                            if (ques.Optionlist != null)
                            {

                                #region 选项编辑
                                for (int j = 0; j < ques.Optionlist.Count; j++)
                                {
                                    Option opt = ques.Optionlist[j];
                                    T_QN_OptionEntity OptionEntity = new T_QN_OptionEntity();

                                    
                                    if (para.step == 3)
                                    {
                                        OptionEntity.CustomerID = loggingSessionInfo.ClientID;
                                        OptionEntity.OptionContent = getValue(opt.OptionContent);
                                        OptionEntity.OptionPicSrc = getValue(opt.OptionPicSrc);
                                        OptionEntity.QuestionID = getValue(QuestionEntity.Questionid.ToString());
                                        OptionEntity.QuestionidType = getValue(QuestionEntity.QuestionidType);
                                        OptionEntity.Sort = j + 1;
                                        OptionEntity.OptionID = getValue(opt.OptionID);
                                        OptionEntity.Status = getValue(opt.Status);
                                    }

                                    if (para.step == 4)
                                    {
                                        OptionEntity.IsRightValue = getValue(opt.IsRightValue);
                                        OptionEntity.NoOptionScore = getValue(opt.NoOptionScore);
                                        OptionEntity.OptionID = getValue(opt.OptionID);
                                        OptionEntity.YesOptionScore = getValue(opt.YesOptionScore);
                                    }

                                    if (OptionEntity.OptionID == null || OptionEntity.OptionID.Value.ToString() == "")
                                    {
                                        OptionEntity.IsRightValue = 0;
                                        OptionEntity.NoOptionScore = 0;
                                        OptionEntity.YesOptionScore = 0;
                                        OptionBLL.Create(OptionEntity);
                                    }
                                    else
                                    {
                                        OptionBLL.Update(OptionEntity, false);
                                    }


                                }

                                #endregion

                                #region 选项删除
                                if (para.step == 3 && ques.OptionDelDatalist!= null)
                                    {
                                        object[] dels = new object[ques.OptionDelDatalist.Count];
                                        for (int j = 0; j < ques.OptionDelDatalist.Count; j++)
                                        {
                                            dels[j] = ques.OptionDelDatalist[j].OptionID;
                                        }

                                        OptionBLL.Delete(dels);
                                    }

                                #endregion
                            }


                            #region 问卷关联题目添加
                            T_QN_QuestionNaireQuestionMappingEntity QuestionNaireQuestionMappingEntity = new T_QN_QuestionNaireQuestionMappingEntity()
                            {
                                CustomerID = loggingSessionInfo.ClientID,
                                QuestionID = QuestionEntity.Questionid.ToString(),
                                QuestionnaireID = model_QuestionnaireEntity.QuestionnaireID.ToString(),
                                Sort = 999,
                                Status = 0
                            };

                            QuestionNaireQuestionMappingBLL.Create(QuestionNaireQuestionMappingEntity);
                            #endregion

                        }
                    }
                    #endregion

                }
            }

            

           

            return rd;
        }

        public T getValue<T>(T item)
        {
            try
            {

                if (item != null)
                {
                    return item;
                }
                
            }
            catch (Exception ex)
            {
                return default(T);
            }
            return default(T);
        }
    }
}
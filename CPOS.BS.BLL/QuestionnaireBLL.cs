/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/7 10:56:10
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class QuestionnaireBLL
    {
        #region ΢�wapҳ��Ľӿ�
        #region 4.2	����������ݻ�ȡ Jermyn20130523
        /// <summary>
        /// ����������ݻ�ȡ
        /// </summary>
        /// <param name="EventID">���ʶ</param>
        /// <returns></returns>
        public GetResponseParams<QuestionnaireEntity> getEventApplyQues(string EventID)
        {
            #region
            if (EventID == null)
            {
                return new GetResponseParams<QuestionnaireEntity>
                {
                    Flag = "0",
                    Code = "404",
                    Description = "���ʶΪ��",
                };
            }
            #endregion
            GetResponseParams<QuestionnaireEntity> response = new GetResponseParams<QuestionnaireEntity>
            {
                Flag = "1",
                Code = "200",
                Description = "�ɹ�"
            };
            try
            {
                QuestionnaireEntity quesInfo = new QuestionnaireEntity();
                #region ҵ����
                //1.��ȡ��������
                quesInfo.QuestionCount = _currentDAO.GetEventApplyQuesCount(EventID);
                //2.��ȡ�����б�
                if (quesInfo.QuestionCount > 0)
                {
                    DataSet ds = new DataSet();
                    ds = _currentDAO.GetEventApplyQuesList(EventID);
                    IList<QuesQuestionsEntity> questionList = new List<QuesQuestionsEntity>();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        questionList = DataTableToObject.ConvertToList<QuesQuestionsEntity>(ds.Tables[0]);
                        if (questionList != null && questionList.Count > 0)
                        {
                            //3.��ȡ����ѡ��
                            foreach (QuesQuestionsEntity questionInfo in questionList)
                            {
                                DataSet ds1 = new DataSet();
                                ds1 = _currentDAO.GetEventApplyOptionList(questionInfo.QuestionID);
                                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                                {
                                    IList<QuesOptionEntity> optionList = new List<QuesOptionEntity>();
                                    optionList = DataTableToObject.ConvertToList<QuesOptionEntity>(ds1.Tables[0]);
                                    questionInfo.QuesOptionEntityList = optionList;
                                }
                            }
                            quesInfo.QuesQuestionEntityList = questionList;
                        }
                    }
                }

                #endregion
                response.Params = quesInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "ʧ��:" + ex.ToString();
                return response;
            }

        }
        #endregion
        #endregion

        #region ������������ύ
        /// <summary>
        /// ������������ύ
        /// </summary>
        public GetResponseParams<bool> WEventSubmitEventApply(string openID, string EventID, string UserID,
             IList<QuesAnswerEntity> quesAnswerList, string UserName) //WEventUserMappingEntity userMappingEntity,
        {
            #region �ж϶�����Ϊ��
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "���ʶΪ��",
                };
            }

            if (quesAnswerList == null || quesAnswerList.Count == 0)
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "��ش����⼯��Ϊ��",
                };
            }
            #endregion

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Flag = "1";
            response.Code = "200";
            //response. = 0;
            response.Description = "�ɹ�";
            try
            {
                var quesAnswerBLL = new QuesAnswerBLL(CurrentUserInfo);



                if (quesAnswerList != null)
                {
                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = string.Format(
                    //        "submitEventApply--WEventSubmitEventApply:{0}", "true")
                    //});
                    string createBy = System.Guid.NewGuid().ToString().Replace("-", "");
                    foreach (var quesAnswerItem in quesAnswerList)
                    {
                        //Loggers.Debug(new DebugLogInfo()
                        //{
                        //    Message = string.Format(
                        //        "submitEventApply--WEventSubmitEventApply:{0}", "ѭ��" + quesAnswerItem.QuestionValue)
                        //});
                        quesAnswerBLL.SubmitQuesQuestionAnswerWEvent(openID, EventID, UserName,
                            quesAnswerItem.QuestionID, quesAnswerItem.QuestionValue, createBy);
                    }
                }
                //else {
                //    Loggers.Debug(new DebugLogInfo()
                //    {
                //        Message = string.Format(
                //            "submitEventApply--WEventSubmitEventApply:{0}", "false")
                //    });
                //}

                response.Params = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "����:" + ex.ToString();
                return response;
            }

        }
        #endregion

        #region Web�б��ȡ
        /// <summary>
        /// Web�б��ȡ
        /// </summary>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public IList<QuestionnaireEntity> GetWebQuestionnaires(QuestionnaireEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<QuestionnaireEntity> list = new List<QuestionnaireEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebQuestionnaires(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<QuestionnaireEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetWebQuestionnairesCount(QuestionnaireEntity entity)
        {
            return _currentDAO.GetWebQuestionnairesCount(entity);
        }
        #endregion


        #region ��ȡ�����б� Add by changjian.tian
        public DataSet GetCommentList(string pQuestionnaireID,string pUserId)
        {
            return _currentDAO.GetCommentList(pQuestionnaireID,pUserId);
        }
        #endregion
    }
}
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
    /// 业务处理：  
    /// </summary>
    public partial class QuestionnaireBLL
    {
        #region 微活动wap页面的接口
        #region 4.2	活动报名表数据获取 Jermyn20130523
        /// <summary>
        /// 活动报名表数据获取
        /// </summary>
        /// <param name="EventID">活动标识</param>
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
                    Description = "活动标识为空",
                };
            }
            #endregion
            GetResponseParams<QuestionnaireEntity> response = new GetResponseParams<QuestionnaireEntity>
            {
                Flag = "1",
                Code = "200",
                Description = "成功"
            };
            try
            {
                QuestionnaireEntity quesInfo = new QuestionnaireEntity();
                #region 业务处理
                //1.获取问题数量
                quesInfo.QuestionCount = _currentDAO.GetEventApplyQuesCount(EventID);
                //2.获取问题列表
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
                            //3.获取问题选项
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
                response.Description = "失败:" + ex.ToString();
                return response;
            }

        }
        #endregion
        #endregion

        #region 活动报名表数据提交
        /// <summary>
        /// 活动报名表数据提交
        /// </summary>
        public GetResponseParams<bool> WEventSubmitEventApply(string openID, string EventID, string UserID,
             IList<QuesAnswerEntity> quesAnswerList, string UserName) //WEventUserMappingEntity userMappingEntity,
        {
            #region 判断对象不能为空
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "活动标识为空",
                };
            }

            if (quesAnswerList == null || quesAnswerList.Count == 0)
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "活动回答问题集合为空",
                };
            }
            #endregion

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Flag = "1";
            response.Code = "200";
            //response. = 0;
            response.Description = "成功";
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
                        //        "submitEventApply--WEventSubmitEventApply:{0}", "循坏" + quesAnswerItem.QuestionValue)
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
                response.Description = "错误:" + ex.ToString();
                return response;
            }

        }
        #endregion

        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
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
        /// 列表数量获取
        /// </summary>
        public int GetWebQuestionnairesCount(QuestionnaireEntity entity)
        {
            return _currentDAO.GetWebQuestionnairesCount(entity);
        }
        #endregion


        #region 获取评论列表 Add by changjian.tian
        public DataSet GetCommentList(string pQuestionnaireID,string pUserId)
        {
            return _currentDAO.GetCommentList(pQuestionnaireID,pUserId);
        }
        #endregion
    }
}
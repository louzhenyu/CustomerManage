using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileSurveyTest.ActionHandler
{
    public class SurveyTestDataAccess
    {
        private readonly LoggingSessionInfo _loggingSessionInfo;
        public SurveyTestDataAccess(LoggingSessionInfo loggingSessionInfo)
        {
            _loggingSessionInfo = loggingSessionInfo;
        }

        #region 考卷列表
        /// <summary>
        /// 考卷列表
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public List<SurveyTestItem> GetQuestionnaires(string pType, int pPageIndex, int pPageSize)
        {
            List<SurveyTestItem> list = new List<SurveyTestItem>();
            QuestionnaireBLL quesBll = new QuestionnaireBLL(_loggingSessionInfo);
            DataTable dTbl = quesBll.GetQuestionnaire(pType, pPageIndex, pPageSize);
            if (dTbl != null)
                list = DataTableToObject.ConvertToList<SurveyTestItem>(dTbl);
            return list;
        }
        #endregion

        #region 获取考卷详情
        /// <summary>
        /// 获取考卷详情
        /// </summary>
        /// <param name="pSurveyTestId"></param>
        /// <returns></returns>
        public SurveyTestItem GetQuestionnaireDetail(string pSurveyTestId)
        {
            List<SurveyTestItem> list = new List<SurveyTestItem>();
            QuestionnaireBLL quesBll = new QuestionnaireBLL(_loggingSessionInfo);
            DataTable dTbl = quesBll.GetQuestionnaireDetail(pSurveyTestId);
            if (dTbl != null)
            {
                list = DataTableToObject.ConvertToList<SurveyTestItem>(dTbl);
                return list[0];
            }
            return null;
        }
        #endregion

        #region 获取用户考卷信息
        /// <summary>
        /// 获取用户考卷信息
        /// </summary>
        /// <param name="pSurveyTestId"></param>
        /// <param name="pUserId"></param>
        /// <returns></returns>
        public MLAnswerSheetEntity GetAnswerSheet(string pSurveyTestId, string pUserId)
        {
            MLAnswerSheetBLL answerSheetBll = new MLAnswerSheetBLL(_loggingSessionInfo);
            var answerSheetList = answerSheetBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "QuestionnaireID", Value =pSurveyTestId },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "UserId", Value =pUserId},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =_loggingSessionInfo.CurrentUser.customer_id },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "IsDelete", Value = "0"}
                    }, new OrderBy[] { new OrderBy() { FieldName = "LastUpdateTime", Direction = OrderByDirections.Desc } });
            if (answerSheetList != null && answerSheetList.Length > 0)
                return answerSheetList[0];
            return null;
        }
        #endregion

        #region 获取用户考卷答题列表
        /// <summary>
        /// 获取用户考卷答题列表
        /// </summary>
        /// <param name="pSurveyTestId"></param>
        /// <param name="pUserId"></param>
        /// <returns></returns>
        public List<AnswerResultItem> GetAnswerSheetItem(string pAnswerSheetId, string pUserId)
        {
            List<AnswerResultItem> answerList = new List<AnswerResultItem>();
            MLAnswerSheetItemBLL answerBll = new MLAnswerSheetItemBLL(_loggingSessionInfo);
            DataTable dTbl = answerBll.GetAnswerSheetItem(pAnswerSheetId, pUserId);
            if (dTbl != null)
                answerList = DataTableToObject.ConvertToList<AnswerResultItem>(dTbl);
            return answerList;
        }
        #endregion

        #region 单题验证
        /// <summary>
        /// 单题验证（只验证QuestionType:1单选,2多选）
        /// -1无标准答案 0错 1对
        /// </summary>
        /// <param name="pQuestionId"></param>
        /// <param name="pAnswer"></param>
        /// <returns></returns>
        public int VerifySingleAnswer(string pQuestionId, string pAnswer)
        {
            QuesQuestionsBLL quesBll = new QuesQuestionsBLL(_loggingSessionInfo);
            QuesQuestionsEntity entity = quesBll.GetByID(pQuestionId);
            if (entity != null)
            {
                QuesOptionBLL quesOpBll = new QuesOptionBLL(_loggingSessionInfo);
                DataTable dTblOption = quesOpBll.GetQuesOptions(pQuestionId);
                //1单选，2多选，3主观选择题，4填空题，5标准打分题
                int questionType = (int)entity.QuestionType;
                DataRow[] drs = null;
                if (questionType == 1)//1单选
                {
                    if (dTblOption != null && dTblOption.Rows.Count > 0)
                    {
                        drs = dTblOption.Select("IsAnswer=1");
                        if (drs != null && drs.Length > 0)
                        {
                            if (drs[0]["OptionIndex"].ToString().ToLower().Equals(pAnswer.ToLower()))
                                return 1;
                            else
                                return 0;
                        }
                    }
                }
                else
                    if (questionType == 2)//2多选
                    {
                        string[] answerArr = pAnswer.ToLower().Split(',');
                        if (dTblOption != null && dTblOption.Rows.Count > 0)
                        {
                            drs = dTblOption.Select("IsAnswer=1");
                            int index = 0;
                            foreach (DataRow row in drs)
                            {
                                for (int i = 0; i < answerArr.Length; i++)
                                {
                                    if (row["OptionIndex"].ToString().ToLower().Equals(answerArr[i]))
                                        index++;
                                }
                            }
                            if (index != answerArr.Length)
                                return 1;
                            else
                                return 0;
                        }
                    }
                    else
                        return -1;
            }
            return -1;
        }

        public string GetSingleAnswer(string pQuestionId, string pAnswer)
        {
            string ret = "-1";
            QuesQuestionsBLL quesBll = new QuesQuestionsBLL(_loggingSessionInfo);
            QuesQuestionsEntity entity = quesBll.GetByID(pQuestionId);
            if (entity != null)
            {
                QuesOptionBLL quesOpBll = new QuesOptionBLL(_loggingSessionInfo);
                DataTable dTblOption = quesOpBll.GetQuesOptions(pQuestionId);
                //1单选，2多选，3主观选择题，4填空题，5标准打分题
                int questionType = (int)entity.QuestionType;
                DataRow[] drs = null;
                if (questionType == 1)//1单选
                {
                    if (dTblOption != null && dTblOption.Rows.Count > 0)
                    {
                        drs = dTblOption.Select("IsAnswer=1");
                        if (drs != null && drs.Length > 0)
                        {
                            if (drs[0]["OptionIndex"].ToString().ToLower().Equals(pAnswer.ToLower()))
                                ret = "1";
                            else
                                ret = "0";
                        }
                    }
                }
                else
                    if (questionType == 2)//2多选
                    {
                        string[] answerArr = pAnswer.ToLower().Split(',');
                        if (dTblOption != null && dTblOption.Rows.Count > 0)
                        {
                            drs = dTblOption.Select("IsAnswer=1");
                            int index = 0;
                            foreach (DataRow row in drs)
                            {
                                for (int i = 0; i < answerArr.Length; i++)
                                {
                                    if (row["OptionIndex"].ToString().ToLower().Equals(answerArr[i]))
                                        index++;
                                }
                            }
                            if (index != answerArr.Length)
                                ret = "1";
                            else
                                ret = "0";
                        }
                    }
                    else
                        ret = "-1";
                ret += "|" + entity.QuestionValue;
            }
            else
                ret += "|题不存在";
            return ret;
        }

        #endregion

        #region 保存用户考题
        /// <summary>
        /// 保存用户考题
        /// 返回
        /// </summary>
        /// <param name="pSurveyTestId"></param>
        /// <param name="pAnswerList"></param>
        /// <returns></returns>
        public int SaveAnswerSheet(string pSurveyTestId, string pUserId, List<AnswerItem> pAnswerList, out decimal lastScore)
        {
            int isPassed = 0;
            decimal score = 0;
            decimal outSocre = 0;
            int singleIsCorrect = -1;

            MLAnswerSheetBLL sheetBll = new MLAnswerSheetBLL(_loggingSessionInfo);
            string answerSheetId = Guid.NewGuid().ToString().Replace("-", "");

            foreach (var item in pAnswerList)
            {
                //保存答题
                singleIsCorrect = SaveAnswerSheetItem(answerSheetId, item.QuestionId, item.Answer, out outSocre);
                if (singleIsCorrect == 1)
                    score += outSocre;
            }

            //判断是否通过
            //条件：score>=PassScore
            QuestionnaireBLL quesBll = new QuestionnaireBLL(_loggingSessionInfo);
            QuestionnaireEntity quesEntity = quesBll.GetByID(pSurveyTestId);
            if (quesEntity != null)
            {
                if (score >= quesEntity.PassScore)
                    isPassed = 1;
                else
                    isPassed = 0;
            }

            lastScore = score;

            //保存考卷
            MLAnswerSheetEntity sheetEntity = new MLAnswerSheetEntity()
            {
                AnswerSheetId = answerSheetId,
                AnswerTime = DateTime.Now,
                CustomerID = _loggingSessionInfo.CurrentUser.customer_id,
                IsDelete = 0,
                IsPassed = isPassed,
                QuestionnaireID = pSurveyTestId,
                Score = score,
                UserId = pUserId
            };
            sheetBll.Create(sheetEntity);
            return isPassed;
        }
        #endregion

        public int SaveAnswerSheetItem(string pAnswerSheetId, string pQuestionId, string pAnswer, out decimal pScore)
        {
            //验证对错和获取题目分值
            int isCorrect = -1;
            string[] singleArr = GetSingleAnswer(pQuestionId, pAnswer).Split('|');
            pScore = TryParse(singleArr[1]);
            if (singleArr[0] == "1")
                isCorrect = 1;
            else if (singleArr[0] == "0")
                isCorrect = 0;
            //保存考题
            MLAnswerSheetItemBLL itemBll = new MLAnswerSheetItemBLL(_loggingSessionInfo);
            string itemId = Guid.NewGuid().ToString().Replace("-", "");
            MLAnswerSheetItemEntity entity = new MLAnswerSheetItemEntity()
            {
                AnswerSheetItemId = itemId,
                AnswerSheetId = pAnswerSheetId,
                QuestionId = pQuestionId,
                Answer = pAnswer,
                IsCorrect = isCorrect,
                CustomerID = _loggingSessionInfo.CurrentUser.customer_id,
                Score = pScore,
                IsDelete = 0
            };
            itemBll.Create(entity);

            return isCorrect;
        }

        public decimal TryParse(string s)
        {
            decimal d = 0;
            decimal.TryParse(s, out d);
            return d;
        }
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 17:23:13
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class WEventUserMappingBLL
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
                Description = "成功."
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
                response.Description = "失败";// +":" + ex.ToString();
                return response;
            }

        }
        #endregion
        #endregion

        #region 获取报名人员数量
        public int GetEventSignInCount(string EventId)
        {
            return _currentDAO.GetEventSignInCount(EventId);
        }
        #endregion

        #region 用户是否已经注册活动
        public int GetUserSignIn(string EventId, string userId)
        {
            return _currentDAO.GetUserSignIn(EventId, userId);
        }
        #endregion

        #region 获取活动人员列表
        /// <summary>
        /// 获取活动人员列表
        /// </summary>
        /// <param name="EventID">活动标识【必须】</param>
        /// <param name="SeachInfo">查询条件:AND [小朋友姓名] like ''%某%'' AND [报名项目] = ''烘焙''</param>
        /// <returns></returns>
        public DataSet SearchEventUserList(string EventID, string SeachInfo)
        {
            DataSet ds = new DataSet();
            ds = _currentDAO.SearchEventUserList(EventID, SeachInfo);
            return ds;
        }
        ///// <summary>
        ///// 获取活动人员列表（EMBA）
        ///// </summary>
        ///// <param name="EventID">活动标识【必须】</param>
        ///// <returns></returns>
        //public DataSet SearchEventUserList(string EventID)
        //{
        //    DataSet ds = new DataSet();
        //    ds = _currentDAO.SearchEventUserList(EventID);
        //    return ds;
        //}
        #endregion

        #region 获取CookieName对应的描述
        /// <summary>
        /// 获取CookieName对应的描述
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public string GetQuestionsDesc(string EventID, string CookieName)
        {
            if (CookieName.Equals("CreateTime"))
            {
                return "报名时间";
            }
            else
            {
                return _currentDAO.GetQuestionsDesc(EventID, CookieName);
            }
        }
        #endregion
    }
}
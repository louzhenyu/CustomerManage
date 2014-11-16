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
    /// ҵ����  
    /// </summary>
    public partial class WEventUserMappingBLL
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
                Description = "�ɹ�."
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
                response.Description = "ʧ��";// +":" + ex.ToString();
                return response;
            }

        }
        #endregion
        #endregion

        #region ��ȡ������Ա����
        public int GetEventSignInCount(string EventId)
        {
            return _currentDAO.GetEventSignInCount(EventId);
        }
        #endregion

        #region �û��Ƿ��Ѿ�ע��
        public int GetUserSignIn(string EventId, string userId)
        {
            return _currentDAO.GetUserSignIn(EventId, userId);
        }
        #endregion

        #region ��ȡ���Ա�б�
        /// <summary>
        /// ��ȡ���Ա�б�
        /// </summary>
        /// <param name="EventID">���ʶ�����롿</param>
        /// <param name="SeachInfo">��ѯ����:AND [С��������] like ''%ĳ%'' AND [������Ŀ] = ''�決''</param>
        /// <returns></returns>
        public DataSet SearchEventUserList(string EventID, string SeachInfo)
        {
            DataSet ds = new DataSet();
            ds = _currentDAO.SearchEventUserList(EventID, SeachInfo);
            return ds;
        }
        ///// <summary>
        ///// ��ȡ���Ա�б�EMBA��
        ///// </summary>
        ///// <param name="EventID">���ʶ�����롿</param>
        ///// <returns></returns>
        //public DataSet SearchEventUserList(string EventID)
        //{
        //    DataSet ds = new DataSet();
        //    ds = _currentDAO.SearchEventUserList(EventID);
        //    return ds;
        //}
        #endregion

        #region ��ȡCookieName��Ӧ������
        /// <summary>
        /// ��ȡCookieName��Ӧ������
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="CookieName"></param>
        /// <returns></returns>
        public string GetQuestionsDesc(string EventID, string CookieName)
        {
            if (CookieName.Equals("CreateTime"))
            {
                return "����ʱ��";
            }
            else
            {
                return _currentDAO.GetQuestionsDesc(EventID, CookieName);
            }
        }
        #endregion
    }
}
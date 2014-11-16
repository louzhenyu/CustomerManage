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
    public partial class QuesAnswerBLL
    {
        /// <summary>
        /// �ʾ��������ύ ΢�
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="QuestionID"></param>
        /// <param name="QuestionValue"></param>
        /// <returns></returns>
        public GetResponseParams<bool> SubmitQuesQuestionAnswerWEvent(string openID, string eventID, string UserID, string QuestionID, string QuestionValue, string CreateBy)
        {
            #region �ж϶�����Ϊ��
            if (UserID == null || UserID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "405",
                    Description = "�����û�Ϊ��",
                };
            }
            if (QuestionID == null || QuestionID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "421",
                    Description = "��������Ϊ��",
                };
            }

            if (QuestionValue == null || QuestionValue.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "422",
                    Description = "�����Ϊ��",
                };
            }
            #endregion

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "ok";
            try
            {
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format(
                //        "submitEventApply--SubmitQuesQuestionAnswerWEvent:{0}", "true")
                //});
                response.Params = _currentDAO.SubmitQuesQuestionAnswerWEvent(UserID, QuestionID, QuestionValue, CreateBy);

                var marketQuesAnswerBll = new MarketQuesAnswerBLL(CurrentUserInfo);
                marketQuesAnswerBll.SubmitQuestions(openID, eventID, QuestionID, QuestionValue);
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format(
                //        "submitEventApply--SubmitQuestions:{0}", "true")
                //});
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

        #region SubmitQuesQuestionAnswerWEvent
        /// <summary>
        /// �ʾ��������ύ ΢�
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="QuestionID"></param>
        /// <param name="QuestionValue"></param>
        /// <returns></returns>
        public GetResponseParams<bool> SubmitQuesQuestionAnswerWEvent(string UserID, string QuestionID, string QuestionValue, string CreateBy)
        {
            #region �ж϶�����Ϊ��
            if (UserID == null || UserID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "405",
                    Description = "�û�Ϊ��",
                };
            }
            if (QuestionID == null || QuestionID.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "421",
                    Description = "�����ʶΪ��",
                };
            }

            if (QuestionValue == null || QuestionValue.ToString().Equals(""))
            {
                return new GetResponseParams<bool>
                {
                    Flag = "0",
                    Code = "422",
                    Description = "��Ϊ��",
                };
            }
            #endregion

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "�ɹ�";
            try
            {
               
                response.Params = _currentDAO.SubmitQuesQuestionAnswerWEvent(UserID, QuestionID, QuestionValue, CreateBy);
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "ʧ��" + ":" + ex.ToString();
                return response;
            }
        }
        #endregion

        public bool DeleteQuesAnswerByEventID(string eventId,string userId)
        {
            return _currentDAO.DeleteQuesAnswerByEventID(eventId, userId);
        }
    }
}
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
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Request;
using JIT.CPOS.DTO.Module.Questionnaire.QuestionnaireAnswerRecord.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.QuestionnaireAnswerRecord
{
    public class DelQuestionnaireAnswerRecordAH : BaseActionHandler<DelQuestionnaireAnswerRecordRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<DelQuestionnaireAnswerRecordRP> pRequest)
        {

            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            List<string> AnswerOptionIDs = new List<string>();


            var QuestionnaireAnswerRecordBLL = new T_QN_QuestionnaireAnswerRecordBLL(loggingSessionInfo);

            var QuestionnaireOptionCountBLL = new T_QN_QuestionnaireOptionCountBLL(this.CurrentUserInfo);

            for (int i = 0; i < para.VipIDs.Length; i++)
            {
                string[] tempAnswerOptionID = QuestionnaireAnswerRecordBLL.GetVipIDModelList(para.VipIDs[0]);

                ArrangeValue(tempAnswerOptionID, ref AnswerOptionIDs);
            }

            #region 修改记录数
            var pTran = QuestionnaireAnswerRecordBLL.GetTran();

            using (pTran.Connection)
            {
                try
                {
                    QuestionnaireOptionCountBLL.UpdateSelectedCount(AnswerOptionIDs,para.ActivityID, pTran);
                    if (para.VipIDs != null)
                    {
                        QuestionnaireAnswerRecordBLL.DeletevipIDs(para.VipIDs);
                    }
                    pTran.Commit();//提交事物
                }
                catch (Exception ex)
                {
                    pTran.Rollback();

                    throw new APIException(ex.Message);
                }
            }

            #endregion

            return rd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OldValue"></param>
        /// <returns></returns>
        private void ArrangeValue(string[] OldValue, ref List<string> NewsValue)
        {
            for (int j = 0; j < OldValue.Length; j++)
            {
                if (OldValue[j] != "")
                {
                    string[] tempvalue = OldValue[j].Split(',');

                    for (int k = 0; k < tempvalue.Length; k++)
                    {
                        if (tempvalue[k] != "" && tempvalue[k] != null)
                        {
                            NewsValue.Add(tempvalue[k]);
                        }
                    }
                }
            }

        }
    }
}
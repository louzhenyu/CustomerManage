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
using JIT.CPOS.DTO.Module.Questionnaire.ScoreRecoveryInformation.Request;
using JIT.CPOS.DTO.Module.Questionnaire.ScoreRecoveryInformation.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Questionnaire.ScoreRecoveryInformation
{
    public class SetScoreRecoveryAH : BaseActionHandler<SetScoreRecoveryRP, SetScoreRecoveryRD>
    {
        protected override SetScoreRecoveryRD ProcessRequest(APIRequest<SetScoreRecoveryRP> pRequest)
        {
            var rd = new SetScoreRecoveryRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_QN_ScoreRecoveryInformationEntity model_ScoreRecoveryInformationEntity = null;



            var ScoreRecoveryBLL = new T_QN_ScoreRecoveryInformationBLL(loggingSessionInfo);
            if (para != null)
            {
                #region 验证分值段

                bool isok = true;
                T_QN_ScoreRecoveryInformationEntity[] ScoreRecoveryInformationEntitylist = ScoreRecoveryBLL.getList(para.QuestionnaireID);
                for (int i = 0; i < ScoreRecoveryInformationEntitylist.Length; i++)
                { 
                    T_QN_ScoreRecoveryInformationEntity modelone=ScoreRecoveryInformationEntitylist[i];
                    if (!(para.ScoreRecoveryInformationID != null && modelone.ScoreRecoveryInformationID == para.ScoreRecoveryInformationID))
                    {
                        if ((para.MinScore + 1) > modelone.MinScore && (para.MinScore - 1) < modelone.MaxScore)
                        {
                            isok = false;
                            break;
                        }

                        if ((para.MaxScore + 1) > modelone.MinScore && (para.MaxScore - 1) < modelone.MaxScore)
                        {
                            isok = false;
                            break;
                        }

                        if (para.MinScore < modelone.MinScore && para.MaxScore > modelone.MaxScore)
                        {
                            isok = false;
                            break;
                        }
                    }
                }

                #endregion

                if (isok)
                {

                    model_ScoreRecoveryInformationEntity = new T_QN_ScoreRecoveryInformationEntity
                    {
                        MaxScore = para.MaxScore,
                        QuestionnaireID = para.QuestionnaireID,
                        MinScore = para.MinScore,
                        CustomerID = loggingSessionInfo.ClientID,
                        RecoveryContent = para.RecoveryContent,
                        RecoveryImg = para.RecoveryImg,
                        RecoveryType = para.RecoveryType,
                        ScoreRecoveryInformationID = para.ScoreRecoveryInformationID,
                        Status = 1

                    };





                    if (para.ScoreRecoveryInformationID != null && para.ScoreRecoveryInformationID.ToString() != "")
                    {
                        ScoreRecoveryBLL.Update(model_ScoreRecoveryInformationEntity);
                    }
                    else
                    {
                        ScoreRecoveryBLL.Create(model_ScoreRecoveryInformationEntity);
                    }

                    rd.ScoreRecoveryInformationID = model_ScoreRecoveryInformationEntity.ScoreRecoveryInformationID.Value;
                    rd.TesultValue = 0;
                }
                else
                {
                    rd.ScoreRecoveryInformationID = null;
                    rd.TesultValue = 1;
                }

               
           }

            return rd;
        }
    }
}
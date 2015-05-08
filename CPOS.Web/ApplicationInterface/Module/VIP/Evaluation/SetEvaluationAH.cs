using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Evaluation.Request;
using JIT.CPOS.DTO.Module.VIP.Evaluation.Response;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Evaluation
{
    public class SetEvaluationAH : BaseActionHandler<SetEvaluationRP, SetEvaluationRD>
    {

        #region 错误码
        #endregion

        protected override SetEvaluationRD ProcessRequest(APIRequest<SetEvaluationRP> pRequest)
        {
            //SetEvaluationRD rd = new SetEvaluationRD();
            //SetEvaluationRP rp = pRequest.Parameters;
            //var bll = new ObjectEvaluationBLL(CurrentUserInfo);
            //var entity = new ObjectEvaluationEntity()
            //{
            //    ClientID = pRequest.CustomerID,
            //    MemberID = rp.MemberID,
            //    ItemEvaluationID = Guid.NewGuid().ToString("N"),
            //    ObjectID = rp.ObjectID,
            //    Content = rp.Content,
            //    Platform = rp.Platform
            //};
            //bll.Create(entity);
            //return rd;
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Evaluation.Request;
using JIT.CPOS.DTO.Module.VIP.Evaluation.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Evaluation
{
    public class GetEvaluationListAH : BaseActionHandler<GetEvaluationListRP, GetEvaluationListRD>
    {
        protected override GetEvaluationListRD ProcessRequest(APIRequest<GetEvaluationListRP> pRequest)
        {
            GetEvaluationListRD rd = new GetEvaluationListRD();
            var bll = new ObjectEvaluationBLL(CurrentUserInfo);
            var list = bll.GetByVIPAndObject(pRequest.CustomerID, pRequest.Parameters.MemberID, pRequest.Parameters.ObjectID.ToString(), pRequest.Parameters.PageIndex, pRequest.Parameters.PageSize);
            if (list.Length == 0)
                return rd;
            rd.Count = list[0].Count;
            rd.EvaluationList = list.Select(t => new EvaluationInfo
            {
                Content = t.Content,
                EvaluationID = t.ItemEvaluationID,
                EvaluationTime = t.CreateTime.Value.To19FormatString(),
                MemberID = t.MemberID,
                MemberName = t.MemberName,
                StarLevel = t.StarLevel.Value.ToString()
            }).ToArray();
            return rd;
        }
    }
}
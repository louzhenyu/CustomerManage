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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Evaluation
{
    public class GetEvaluationListAH : BaseActionHandler<GetEvaluationListRP, GetEvaluationListRD>
    {
        protected override GetEvaluationListRD ProcessRequest(APIRequest<GetEvaluationListRP> pRequest)
        {
            GetEvaluationListRD rd = new GetEvaluationListRD();
            var evaluationBll = new ObjectEvaluationBLL(CurrentUserInfo);

            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "ObjectID", Value = pRequest.Parameters.ObjectID });
            //根据好评/中评/差评查询
            if (pRequest.Parameters.StarLevel > 0)
                complexCondition.Add(new EqualsCondition() { FieldName = "StarLevel", Value = pRequest.Parameters.StarLevel });

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            //默认根据评论时间倒序
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            var tempEvaluationList = evaluationBll.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), pRequest.Parameters.PageSize, pRequest.Parameters.PageIndex + 1);
            int goodCount = evaluationBll.GetEvaluationCount(pRequest.Parameters.ObjectID, 3);
            rd.TotalPageCount = tempEvaluationList.PageCount;
            rd.Count = tempEvaluationList.RowCount;
            rd.GoodPer = rd.TotalPageCount == 0 ? "0" : (goodCount /rd.Count) + "%";
            rd.EvaluationList = tempEvaluationList.Entities.Select(t => new EvaluationInfo()
            {
                EvaluationID = t.EvaluationID,
                VipID = t.VipID,
                VipName = t.VipName,
                Content = t.Content,
                StarLevel = t.StarLevel,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-hh"),
                IsAnonymity = t.IsAnonymity,
                Remark = t.Remark
            }).ToArray();
            return rd;
        }
    }
}
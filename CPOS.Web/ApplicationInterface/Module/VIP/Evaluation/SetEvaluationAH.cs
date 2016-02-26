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
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Evaluation
{
    public class SetEvaluationAH : BaseActionHandler<SetEvaluationRP, SetEvaluationRD>
    {

        #region 错误码
        #endregion

        protected override SetEvaluationRD ProcessRequest(APIRequest<SetEvaluationRP> pRequest)
        {
            SetEvaluationRD rd = new SetEvaluationRD();
            var oeBll = new ObjectEvaluationBLL(CurrentUserInfo);
            var pTran = oeBll.GetTran();//事务
            using (pTran.Connection)
            {
                try
                {
                    SetEvaluationRP rp = pRequest.Parameters;

                    //查询参数
                    List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                    complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pRequest.CustomerID });
                    complexCondition.Add(new EqualsCondition() { FieldName = "ObjectID", Value = rp.ObjectID });
                    complexCondition.Add(new EqualsCondition() { FieldName = "Type", Value = 4 });
                    complexCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pRequest.UserID });
                    complexCondition.Add(new DirectCondition(" (CreateTime between '" + DateTime.Now.Date + "' and '" + DateTime.Now.AddDays(1).Date + "' )"));
                    var tempList = oeBll.Query(complexCondition.ToArray(), null);

                    //var oeEntitys = oeBll.QueryByEntity(new ObjectEvaluationEntity() { VipID = rp.ObjectID,CustomerID = pRequest.CustomerID }, null);
                    if (tempList.Length > 0)
                    {
                        throw new APIException("一天只可以评论一次!") { ErrorCode = 103 };
                    }
                    //评论
                    var entity = new ObjectEvaluationEntity()
                    {
                        EvaluationID = Guid.NewGuid().ToString(),
                        CustomerID = pRequest.CustomerID,
                        VipID = pRequest.UserID,
                        ObjectID = rp.ObjectID,
                        Type = rp.Type,
                        Content = rp.Content,
                        StarLevel = rp.StarLevel,
                        StarLevel1 = rp.StarLevel1,
                        StarLevel2 = rp.StarLevel2,
                        StarLevel3 = rp.StarLevel3,
                        StarLevel4 = rp.StarLevel4,
                        StarLevel5 = rp.StarLevel5,
                        Platform = rp.Platform,
                        IsAnonymity = rp.IsAnonymity
                    };
                    oeBll.Create(entity, pTran);

                    
                    pTran.Commit();//提交事务

                }
                catch (Exception ex)
                {
                    pTran.Rollback();
                    throw new APIException(ex.Message);
                }
            }

            return rd;
        }
    }
}
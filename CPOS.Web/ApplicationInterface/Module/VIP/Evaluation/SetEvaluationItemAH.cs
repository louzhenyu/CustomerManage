using JIT.CPOS.BLL;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Evaluation.Request;
using JIT.CPOS.DTO.Module.VIP.Evaluation.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Evaluation
{
    public class SetEvaluationItemAH : BaseActionHandler<SetEvaluationItemRP, SetEvaluationRD>
    {

        #region 错误码
        #endregion
        protected override SetEvaluationRD ProcessRequest(APIRequest<SetEvaluationItemRP> pRequest)
        {
            SetEvaluationRD rd = new SetEvaluationRD();
            var bll = new ObjectEvaluationBLL(CurrentUserInfo);
            var pTran = bll.GetTran();//事务
            using (pTran.Connection)
            {
                try
                {
                    SetEvaluationItemRP rp = pRequest.Parameters;
                    var inoutBll = new T_InoutBLL(CurrentUserInfo);
                    //评论订单
                    var entity = new ObjectEvaluationEntity()
                    {
                        EvaluationID = Guid.NewGuid().ToString(),
                        CustomerID = pRequest.CustomerID,
                        VipID = pRequest.UserID,
                        ObjectID = rp.OrderID,
                        OrderID = rp.OrderID,
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
                    bll.Create(entity, pTran);

                    //批量评论商品
                    ObjectEvaluationEntity evaluation = null;
                    foreach (var item in rp.ItemEvaluationInfo)
                    {
                        evaluation = new ObjectEvaluationEntity();
                        evaluation.ObjectID = item.ObjectID;
                        evaluation.StarLevel = item.StarLevel;
                        evaluation.Content = item.Content;
                        evaluation.Remark = item.Remark;
                        evaluation.CustomerID = pRequest.CustomerID;
                        evaluation.IsAnonymity = rp.IsAnonymity;
                        evaluation.OrderID = rp.OrderID;
                        evaluation.Type = rp.Type;
                        evaluation.VipID = pRequest.UserID;
                        bll.Create(evaluation, pTran);
                    }
                    //修改订单评论状态
                    var order = inoutBll.GetByID(rp.OrderID);
                    if (order != null)
                    {
                        order.IsEvaluation = 1;
                        inoutBll.Update(order, pTran);
                    }
                    pTran.Commit();//提交事物

                    #region 评论触点活动奖励
                    var bllPrize = new LPrizesBLL(CurrentUserInfo);
                    bllPrize.CheckIsWinnerForShare(CurrentUserInfo.UserID, "", "Comment");
                    #endregion
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
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Activity.Request;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class SetPrizesAH : BaseActionHandler<SetPrizesRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetPrizesRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ActivityBLL = new C_ActivityBLL(loggingSessionInfo);
            var PrizesBLL = new C_PrizesBLL(loggingSessionInfo);
            var PrizesDetailBLL = new C_PrizesDetailBLL(loggingSessionInfo);
            var pTran = ActivityBLL.GetTran();
            using (pTran.Connection)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(para.ActivityID))
                        throw new APIException("必须输入活动编号!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    if (string.IsNullOrWhiteSpace(para.PrizesID))
                    {//新增
                        C_PrizesEntity AddPrizesData = new C_PrizesEntity();
                        AddPrizesData.ActivityID = new Guid(para.ActivityID);
                        AddPrizesData.PrizesType = para.PrizesType;
                        AddPrizesData.PrizesName = para.PrizesDetailList.FirstOrDefault().CouponTypeName;
                        AddPrizesData.CustomerID = loggingSessionInfo.ClientID;
                        AddPrizesData.SendDate = DateTime.Now;
                        List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                        complexCondition.Add(new EqualsCondition() { FieldName = "ActivityID", Value = para.ActivityID });
                        if(PrizesBLL.Query(complexCondition.ToArray(), null).ToList().Count > 0)
                            throw new APIException("不能重复创建奖品!") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        PrizesBLL.Create(AddPrizesData);
                        
                        Guid? PrizesID = PrizesBLL.Query(complexCondition.ToArray(), null).ToList().FirstOrDefault().PrizesID;
                        foreach (var itemes in para.PrizesDetailList)
                        {
                            C_PrizesDetailEntity AddDetailData = new C_PrizesDetailEntity();
                            AddDetailData.PrizesID = PrizesID;
                            AddDetailData.CouponTypeID = new Guid(itemes.CouponTypeID);
                            AddDetailData.CustomerID = loggingSessionInfo.ClientID;
                            AddDetailData.NumLimit = itemes.NumLimit;
                            AddDetailData.CouponTypeName = itemes.CouponTypeName;
                            AddDetailData.CouponTypeDesc = itemes.CouponTypeDesc;
                            PrizesDetailBLL.Create(AddDetailData, pTran);
                        }
                    }
                    else
                    {//编辑奖品明细
                        foreach (var itemes in para.PrizesDetailList)
                        {
                            C_PrizesDetailEntity AddDetailData = new C_PrizesDetailEntity();
                            AddDetailData.PrizesID = new Guid(para.PrizesID);
                            AddDetailData.CouponTypeID = new Guid(itemes.CouponTypeID);
                            AddDetailData.CustomerID = loggingSessionInfo.ClientID;
                            AddDetailData.NumLimit = itemes.NumLimit;
                            AddDetailData.CouponTypeName = itemes.CouponTypeName;
                            AddDetailData.CouponTypeDesc = itemes.CouponTypeDesc;
                            if (string.IsNullOrWhiteSpace(itemes.PrizesDetailID))
                            {
                                PrizesDetailBLL.Create(AddDetailData, pTran);
                            }
                            else
                            {
                                AddDetailData.PrizesDetailID = new Guid(itemes.PrizesDetailID);
                                if (itemes.IsEnable == 1)
                                {                                    
                                    PrizesDetailBLL.Update(AddDetailData, pTran);
                                }
                                else
                                {
                                    PrizesDetailBLL.Delete(AddDetailData, pTran);
                                }
                            }
                        }
                    }
                    pTran.Commit();
                    if (ActivityBLL.IsActivityValid(para.ActivityID))
                    {
                        var activity = ActivityBLL.GetByID(para.ActivityID);
                        if (activity != null)
                        {
                            activity.Status = 0;
                        }
                        ActivityBLL.Update(activity);
                    }
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }              
            }
            return rd;
        }
    }
}
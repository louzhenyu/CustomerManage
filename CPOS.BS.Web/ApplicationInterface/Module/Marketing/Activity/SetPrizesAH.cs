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
            //事务
            var pTran = ActivityBLL.GetTran();

            using (pTran.Connection)
            {
                try
                {
                    switch (para.OperationType)
                    {
                        case 1:

                            #region 添加奖品，明细


                            foreach (var item in para.PrizesInfoList)
                            {
                                if (!string.IsNullOrWhiteSpace(item.PrizesID))
                                {
                                    #region 添加奖品明细
                                    //明细
                                    foreach (var itemes in item.PrizesDetailList)
                                    {
                                        C_PrizesDetailEntity AddDetailData = new C_PrizesDetailEntity();
                                        AddDetailData.PrizesID =new Guid(item.PrizesID);
                                        AddDetailData.CouponTypeID = new Guid(itemes.CouponTypeID);
                                        AddDetailData.Qty = 0;
                                        AddDetailData.RemainingQty = 0;
                                        AddDetailData.CustomerID = loggingSessionInfo.ClientID;
                                        PrizesDetailBLL.Create(AddDetailData, pTran);//执行
                                    }
                                    break;
                                    #endregion
                                }
                                else
                                {
                                    #region 添加奖品，奖品明细
                                    C_PrizesEntity AddPrizesData = new C_PrizesEntity();
                                    AddPrizesData.ActivityID = new Guid(para.ActivityID);
                                    AddPrizesData.PrizesType = item.PrizesType;
                                    AddPrizesData.PrizesName = item.PrizesDetailList.FirstOrDefault().CouponTypeName;
                                    AddPrizesData.Qty = 0;
                                    AddPrizesData.RemainingQty = 0;
                                    AddPrizesData.AmountLimit = item.AmountLimit == null ? 0 : item.AmountLimit.Value;
                                    AddPrizesData.IsAutoIncrease = 0;
                                    AddPrizesData.DisplayIndex = 0;
                                    AddPrizesData.IsCirculation = item.IsCirculation;
                                    AddPrizesData.SendDate = DateTime.Now;
                                    AddPrizesData.CustomerID = loggingSessionInfo.ClientID;
                                    PrizesBLL.Create(AddPrizesData, pTran);//执行

                                    //明细
                                    foreach (var itemes in item.PrizesDetailList)
                                    {
                                        C_PrizesDetailEntity AddDetailData = new C_PrizesDetailEntity();
                                        //AddDetailData.PrizesDetailID = System.Guid.NewGuid();
                                        AddDetailData.PrizesID = AddPrizesData.PrizesID;
                                        AddDetailData.CouponTypeID = new Guid(itemes.CouponTypeID);
                                        AddDetailData.Qty = 0;
                                        AddDetailData.RemainingQty = 0;
                                        AddDetailData.CustomerID = loggingSessionInfo.ClientID;
                                        PrizesDetailBLL.Create(AddDetailData, pTran);//执行
                                    }
                                    break;
                                    #endregion
                                }
                            }

                            #endregion

                            break;
                        case 2:
                            #region 编辑奖品 or 删除奖品，明细
                            //活动修改
                            C_ActivityEntity UpdateActivityData = ActivityBLL.GetByID(para.ActivityID);
                            if (UpdateActivityData == null)
                                throw new APIException("活动对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            UpdateActivityData.PointsMultiple = para.PointsMultiple == null ? 0 : para.PointsMultiple.Value;

                            //修改奖品集合 
                            List<C_PrizesEntity> UpdataList = new List<C_PrizesEntity>();
                            //删除奖品明细集合
                            List<Guid> DelPrizesDetailList = new List<Guid>();
                            //删除奖品集合
                            //List<Guid> DelPrizesList = new List<Guid>();

                            #region 操作数据处理
                            foreach (var item in para.PrizesInfoList)
                            {
                                //奖品
                                C_PrizesEntity Data = PrizesBLL.GetByID(item.PrizesID);
                                if (Data == null)
                                    throw new APIException("奖品对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                //是否启用
                                if (item.IsEnable.Value)
                                {
                                    Data.AmountLimit = item.AmountLimit == null ? 0 : item.AmountLimit.Value;
                                    Data.IsCirculation = item.IsCirculation;
                                    UpdataList.Add(Data);
                                }
                                else
                                {

                                    List<C_PrizesDetailEntity> DelResultList = PrizesDetailBLL.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "PrizesID", Value = item.PrizesID } }, null).ToList();
                                    if (DelResultList.Count > 0)
                                    {
                                        foreach (var Delitem in DelResultList)
                                        {
                                            DelPrizesDetailList.Add(Delitem.PrizesDetailID.Value);
                                        }
                                    }
                                    ////奖品，明细
                                    //foreach (var itemes in item.PrizesDetailList)
                                    //{
                                    //    if (!string.IsNullOrWhiteSpace(itemes.PrizesDetailID))
                                    //        DelPrizesDetailList.Add(new Guid(itemes.PrizesDetailID));
                                    //    else
                                    //        throw new APIException("参数奖品明细标识对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                    //}
                                    //奖品
                                    //DelPrizesList.Add(Data.PrizesID.Value);
                                }
                            }
                            #endregion


                            #region 执行
                            //活动修改
                            ActivityBLL.Update(UpdateActivityData, pTran);
                            //删除明细
                            if (DelPrizesDetailList.Count > 0)
                            {
                                foreach (var item in DelPrizesDetailList)
                                {
                                    PrizesDetailBLL.Delete(item, pTran);
                                }
                            }
                            //if (DelPrizesList.Count > 0)
                            //{
                            //    foreach (var item in DelPrizesList)
                            //    {
                            //        PrizesBLL.Delete(item, pTran);
                            //    }
                            //}
                            if (UpdataList.Count > 0)
                            {
                                foreach (var item in UpdataList)
                                {
                                    PrizesBLL.Update(item, pTran);
                                }
                            }



                            #endregion

                            #endregion
                            break;
                        default:
                            throw new APIException("当前操作类型不匹配！") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };
                            break;
                    }
                    pTran.Commit();
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                //catch (Exception ex)
                //{
                //    pTran.Rollback();//回滚事务
                //    throw new APIException(ex.Message);
                //}

            }
            return rd;
        }
    }
}
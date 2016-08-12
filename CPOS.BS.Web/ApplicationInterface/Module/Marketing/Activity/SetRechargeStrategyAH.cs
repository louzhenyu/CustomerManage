using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Activity.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Activity
{
    public class SetRechargeStrategyAH : BaseActionHandler<SetRechargeStrategyRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetRechargeStrategyRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rechargeStrategyBll = new RechargeStrategyBLL(loggingSessionInfo);
            //事物
            var pTran = rechargeStrategyBll.GetTran();
            using (pTran.Connection)
            {
                try
                {
                    //新增充值策略集合
                    var AddDataList = new List<RechargeStrategyEntity>();
                    //编辑充值策略集合
                    var UpdateDataList = new List<RechargeStrategyEntity>();
                    //删除充值策略集合
                    var DelList = new List<Guid>();
                    #region 数据处理
                    foreach (var item in para.RechargeStrategyInfoList)
                    {
                        if (string.IsNullOrWhiteSpace(item.RechargeStrategyId))
                        {
                            #region 新增
                            RechargeStrategyEntity AddData = new RechargeStrategyEntity();
                            AddData.ActivityID = new Guid(para.ActivityID);
                            AddData.RuleType = item.RuleType;
                            AddData.RechargeAmount = item.RechargeAmount;
                            AddData.GiftAmount = item.GiftAmount;
                            AddData.CustomerId = loggingSessionInfo.ClientID;
                            AddDataList.Add(AddData);
                            #endregion
                        }
                        else
                        {
                            RechargeStrategyEntity Data = rechargeStrategyBll.GetByID(item.RechargeStrategyId);
                            if (Data == null)
                                throw new APIException("充值策略对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            if (item.IsEnable == 1)
                            {
                                //更新
                                Data.RechargeAmount = item.RechargeAmount;
                                Data.GiftAmount = item.GiftAmount;
                                Data.RuleType = item.RuleType;
                                UpdateDataList.Add(Data);
                            }
                            else
                            {
                                //删除
                                DelList.Add(Data.RechargeStrategyId.Value);
                            }
                        }

                    }
                    #endregion

                    #region 执行
                    if (AddDataList.Count > 0)
                    {
                        //新增
                        foreach (var item in AddDataList)
                        {
                            rechargeStrategyBll.Create(item, pTran);
                        }

                    }

                    if (UpdateDataList.Count > 0)
                    {
                        //编辑
                        foreach (var item in UpdateDataList)
                        {
                            rechargeStrategyBll.Update(item, pTran);
                        }
                    }
                    if (DelList.Count > 0)
                    {
                        //删除
                        foreach (var item in DelList)
                        {
                            rechargeStrategyBll.Delete(item, pTran);
                        }
                    }

                    pTran.Commit();
                    #endregion

                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                catch (Exception ex)
                {
                    pTran.Rollback();//回滚事务
                    throw new APIException(ex.Message);
                }
            }
            return rd;
        }
    }
}
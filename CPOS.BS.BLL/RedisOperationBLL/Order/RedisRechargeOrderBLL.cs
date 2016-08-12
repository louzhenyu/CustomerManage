
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using JIT.CPOS.BS.Entity;
using RedisOpenAPIClient.Models.CC.OrderReward;
using System.Data;
using JIT.CPOS.BS.DataAccess.Base;
using System.Data.SqlClient;
using System.Configuration;
using JIT.Utility.DataAccess;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.Order
{
    public class RedisRechargeOrderBLL
    {
        /// <summary>
        /// 入充值订单队列
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderInfo"></param>
        public void SetRedisToRechargeOrder(LoggingSessionInfo loggingSessionInfo, RechargeOrderEntity orderInfo)
        {
            var response = RedisOpenAPI.Instance.CCRechargeOrder().SetRechargeOrder(new CC_Order
            {
                CustomerID = loggingSessionInfo.ClientID,
                OrderId = orderInfo.OrderID.ToString(),
                OrderInfo = orderInfo.JsonSerialize()

            });
            if (response.Code == ResponseCode.Fail)
            {
                //直接计算
                RechargeOrderBLL rechargeOrderBll = new RechargeOrderBLL(loggingSessionInfo);
                rechargeOrderBll.CalculateRechargeOrder(loggingSessionInfo, orderInfo);
            }
        }
        /// <summary>
        /// 充值订单循环队列计算
        /// </summary>
        public void CalculateRechargeOrderJob()
        {
            var numCount = 50;
            var customerIDs = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in customerIDs)
            {
                string connString = customer.Value;
                var count = RedisOpenAPI.Instance.CCRechargeOrder().GetRechargeOrderLength(new CC_Order
                {
                    CustomerID = customer.Key
                });
                if (count.Code != ResponseCode.Success)
                {
                    BaseService.WriteLog("从redis获取充值订单数据失败");
                    continue;
                }
                if (count.Result <= 0)
                {
                    continue;
                }
                if (count.Result < numCount)
                {
                    numCount = Convert.ToInt32(count.Result);
                }
                var loggingSessionInfo = CustomerBLL.Instance.GetBSLoggingSession(customer.Key, "RedisSystem");
                VipCardProfitRuleBLL bllVipCardProfitRule = new VipCardProfitRuleBLL(loggingSessionInfo);
                VipCardProfitRuleEntity[] entityVipCardProfitRule = null;
				var bllVipCardGradeChangeLog = new VipCardGradeChangeLogBLL(loggingSessionInfo);
				VipAmountBLL bllVipAmount = new VipAmountBLL(loggingSessionInfo);
				VipAmountDetailBLL bllVipAmountDetail = new VipAmountDetailBLL(loggingSessionInfo);
				T_SplitProfitRecordBLL bllSplitProfitRecord = new T_SplitProfitRecordBLL(loggingSessionInfo);

				for (var i = 0; i < numCount; i++)
                {
                    var response = RedisOpenAPI.Instance.CCRechargeOrder().GetRechargeOrder(new CC_Order
                    {
                        CustomerID = customer.Key
                    });
                    if (response.Code == ResponseCode.Success)
                    {
						var rechargeOrderInfo = response.Result.OrderInfo.JsonDeserialize<RechargeOrderEntity>();

						entityVipCardProfitRule = bllVipCardProfitRule.QueryByEntity(new VipCardProfitRuleEntity() { VipCardTypeID = rechargeOrderInfo.VipCardTypeId, IsDelete = 0 }, null);
                        if (entityVipCardProfitRule != null)
                        {
                            //DataTable dtAmountDetail = CreateTableAmountDetail();
                            //DataTable dtAmount = CreateTableAmount();
                            //DataTable dtSplitProfitRecord = CreateTableSplitProfitRecord();



							VipAmountEntity entityVipAmount = new VipAmountEntity();
							VipAmountDetailEntity entityVipAmountDetail = new VipAmountDetailEntity();
							T_SplitProfitRecordEntity entitySplitProfitRecord = new T_SplitProfitRecordEntity();

							foreach (var ProfitRule in entityVipCardProfitRule)
                            {
                                decimal amount = 0;
                                string strAmountSourceId = string.Empty;
                                string strVipId = string.Empty;
                                string strUserType = string.Empty;
                                if (ProfitRule.IsApplyAllUnits == 0)
                                {
                                    VipCardProfitRuleUnitMappingBLL bllVipCardProfitRuleUnitMapping = new VipCardProfitRuleUnitMappingBLL(loggingSessionInfo);
                                    var vipCardProfitRuleUnitMapping = bllVipCardProfitRuleUnitMapping.QueryByEntity(new VipCardProfitRuleUnitMappingEntity() { CardBuyToProfitRuleId = ProfitRule.CardBuyToProfitRuleId, UnitID = rechargeOrderInfo.UnitId, IsDelete = 0, CustomerID = loggingSessionInfo.ClientID }, null).SingleOrDefault();
                                    if (vipCardProfitRuleUnitMapping == null)
                                    {
                                        continue;
                                    }
                                }
                                if (ProfitRule.ProfitOwner == "Employee")
                                {
                                    strAmountSourceId = "38";
                                    strVipId = rechargeOrderInfo.UserId;
                                    strUserType = "User";
                                }
                                if (ProfitRule.ProfitOwner == "Unit")
                                {
                                    strAmountSourceId = "42";
                                    strVipId = rechargeOrderInfo.UnitId;
                                    strUserType = "Unit";
                                }
								var vipAmountDetail=bllVipAmountDetail.QueryByEntity(new VipAmountDetailEntity() { ObjectId = rechargeOrderInfo.OrderID.ToString(), AmountSourceId = strAmountSourceId},null);
								if(vipAmountDetail!=null && vipAmountDetail.Length>0) {
									continue;
								}
								if (rechargeOrderInfo.OrderDesc == "Upgrade")//首充
                                {
                                    amount = (decimal)ProfitRule.FirstRechargeProfitPct * (decimal)rechargeOrderInfo.ActuallyPaid * (decimal)0.01;
                                }
                                if (rechargeOrderInfo.OrderDesc == "ReRecharge")//续充
                                {
                                    VipCardReRechargeProfitRuleBLL bllVipCardReRechargeProfitRule = new VipCardReRechargeProfitRuleBLL(loggingSessionInfo);
									var entityVipCardReRechargeProfitRule = bllVipCardReRechargeProfitRule.QueryByEntity(new VipCardReRechargeProfitRuleEntity() { VipCardTypeID = rechargeOrderInfo.VipCardTypeId, CardBuyToProfitRuleId = ProfitRule.CardBuyToProfitRuleId, IsDelete = 0 }, null);
                                    if (entityVipCardReRechargeProfitRule != null)
                                    {
                                        decimal discount = 0;
                                        foreach (var ReRechargeProfitRule in entityVipCardReRechargeProfitRule.OrderByDescending(a=>a.LimitAmount))
                                        {
                                            if (ReRechargeProfitRule.ProfitType == "Superposition")
                                            {
												discount = (decimal)(ReRechargeProfitRule.LimitAmount * ReRechargeProfitRule.ProfitPct) * (decimal)0.01;
												amount = (decimal)(rechargeOrderInfo.ActuallyPaid / ReRechargeProfitRule.LimitAmount) * discount;
											}
											if (ReRechargeProfitRule.ProfitType == "Step")
                                            {
												if (rechargeOrderInfo.ActuallyPaid >= ReRechargeProfitRule.LimitAmount) {
													if (discount == 0) {
														discount = (decimal)ReRechargeProfitRule.ProfitPct * (decimal)0.01;
														amount = (decimal)rechargeOrderInfo.ActuallyPaid * discount;
													}
												}
                                            }
                                        }
									}
								}
                                //入库
                                if (amount > 0)
                                {
                                    IDbTransaction tran = new JIT.CPOS.BS.DataAccess.Base.TransactionHelper(loggingSessionInfo).CreateTransaction();
									try {
										//                           DataRow dr_AmountDetail = dtAmountDetail.NewRow();
										//                           dr_AmountDetail["VipAmountDetailId"] = Guid.NewGuid();
										//                           dr_AmountDetail["VipId"] = strVipId;
										//                           dr_AmountDetail["VipCardCode"] = "";
										//                           dr_AmountDetail["UnitID"] = "";
										//                           dr_AmountDetail["UnitName"] = "";
										//                           dr_AmountDetail["SalesAmount"] = 0;
										//                           dr_AmountDetail["Amount"] = amount;
										//                           dr_AmountDetail["UsedReturnAmount"] = 0;
										//                           dr_AmountDetail["Reason"] = "充值";
										//                           dr_AmountDetail["EffectiveDate"] = DateTime.Now;
										//                           dr_AmountDetail["DeadlineDate"] = Convert.ToDateTime("9999-12-31 23:59:59");
										//                           dr_AmountDetail["AmountSourceId"] = strAmountSourceId;
										//                           dr_AmountDetail["ObjectId"] = rechargeOrderInfo.OrderID.ToString();
										//							 dr_AmountDetail["Remark"] = "充值";
										//                           dr_AmountDetail["IsValid"] = 0;
										//                           dr_AmountDetail["IsWithdrawCash"] = 0;
										//                           dr_AmountDetail["CustomerID"] = loggingSessionInfo.ClientID;
										//                           dr_AmountDetail["CreateTime"] = DateTime.Now;
										//                           dr_AmountDetail["CreateBy"] = loggingSessionInfo.ClientID;
										//                           dr_AmountDetail["LastUpdateBy"] = loggingSessionInfo.ClientID;
										//                           dr_AmountDetail["LastUpdateTime"] = DateTime.Now;
										//                           dr_AmountDetail["IsDelete"] = 0;
										//                           //dr_AmountDetail["IsCalculated"] = 1;
										//                           dtAmountDetail.Rows.Add(dr_AmountDetail);

										//DataRow dr_SplitProfitRecord = dtSplitProfitRecord.NewRow();
										//dr_SplitProfitRecord["Id"] = Guid.NewGuid();
										//dr_SplitProfitRecord["SourceType"] = "Amount";
										//dr_SplitProfitRecord["SourceId"] = strAmountSourceId;
										//dr_SplitProfitRecord["ObjectId"] = rechargeOrderInfo.OrderID.ToString();
										//dr_SplitProfitRecord["UserType"] = strUserType;
										//dr_SplitProfitRecord["UserId"] = rechargeOrderInfo.UserId;
										//dr_SplitProfitRecord["SplitAmount"] = amount;
										//dr_SplitProfitRecord["SplitSattus"] = "10";
										//dr_SplitProfitRecord["CustomerId"] = loggingSessionInfo.ClientID;
										//dr_SplitProfitRecord["CreateTime"] = DateTime.Now;
										//dr_SplitProfitRecord["CreateBy"] = loggingSessionInfo.ClientID;
										//dr_SplitProfitRecord["LastUpdateTime"] = DateTime.Now;
										//dr_SplitProfitRecord["LastUpdateBy"] = loggingSessionInfo.ClientID;
										//dr_SplitProfitRecord["IsDelete"] = 0;
										//dtSplitProfitRecord.Rows.Add(dr_SplitProfitRecord);

										entitySplitProfitRecord = new T_SplitProfitRecordEntity() {
											ID= Guid.NewGuid().ToString(),
											SourceType="Amount",
											SourceId= strAmountSourceId,
											ObjectId= rechargeOrderInfo.OrderID.ToString(),
											UserType=strUserType,
											UserId= rechargeOrderInfo.UserId,
											SplitAmount=amount,
											SplitSattus="10",
											CustomerID= loggingSessionInfo.ClientID
										};
										bllSplitProfitRecord.Create(entitySplitProfitRecord, tran);


										entityVipAmountDetail = new VipAmountDetailEntity {
											VipAmountDetailId = Guid.NewGuid(),
											VipId = strVipId,
											Amount = amount,
											UsedReturnAmount = 0,
											EffectiveDate = DateTime.Now,
											DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
											AmountSourceId = strAmountSourceId,
											ObjectId = rechargeOrderInfo.OrderID.ToString(),
											CustomerID = loggingSessionInfo.ClientID,
											Reason = "充值",
											IsWithdrawCash = 0
										};
										bllVipAmountDetail.Create(entityVipAmountDetail, (SqlTransaction)tran);


										entityVipAmount = bllVipAmount.QueryByEntity(new VipAmountEntity() { VipId = strVipId, IsDelete = 0}, null).SingleOrDefault();
										if (entityVipAmount == null) {
											entityVipAmount = new VipAmountEntity {
												VipId = strVipId,
												BeginAmount = 0,
												InAmount = amount,
												OutAmount = 0,
												EndAmount = amount,
												TotalAmount = amount,
												BeginReturnAmount = 0,
												InReturnAmount = 0,
												OutReturnAmount = 0,
												ReturnAmount = 0,
												ImminentInvalidRAmount = 0,
												InvalidReturnAmount = 0,
												ValidReturnAmount = 0,
												TotalReturnAmount = 0,
												IsLocking = 0,
												CustomerID = loggingSessionInfo.ClientID,
												VipCardCode = ""

											};
											bllVipAmount.Create(entityVipAmount, tran);

											//DataRow dr_Amount = dtAmount.NewRow();
											//dr_Amount["VipId"] = strVipId;
											//dr_Amount["VipCardCode"] = "";
											//dr_Amount["BeginAmount"] = 0;
											//dr_Amount["InAmount"] = amount;
											//dr_Amount["OutAmount"] = 0;
											//dr_Amount["EndAmount"] = amount;
											//dr_Amount["TotalAmount"] = amount;
											//dr_Amount["BeginReturnAmount"] = 0;
											//dr_Amount["InReturnAmount"] = 0;
											//dr_Amount["OutReturnAmount"] = 0;
											//dr_Amount["ReturnAmount"] = 0;
											//dr_Amount["ImminentInvalidRAmount"] = 0;
											//dr_Amount["InvalidReturnAmount"] = 0;
											//dr_Amount["ValidReturnAmount"] = 0;
											//dr_Amount["TotalReturnAmount"] = 0;
											//dr_Amount["PayPassword"] = "";
											//dr_Amount["IsLocking"] = 0;
											//dr_Amount["CustomerID"] = loggingSessionInfo.ClientID;
											//dr_Amount["CreateTime"] = DateTime.Now;
											//dr_Amount["CreateBy"] = loggingSessionInfo.ClientID;
											//dr_Amount["LastUpdateBy"] = loggingSessionInfo.ClientID;
											//dr_Amount["LastUpdateTime"] = DateTime.Now;
											//dr_Amount["IsDelete"] = 0;
											//dtAmount.Rows.Add(dr_Amount);
										}
										else {

											entityVipAmount.InReturnAmount = (entityVipAmount.InReturnAmount == null ? 0 : entityVipAmount.InReturnAmount.Value) + amount;
											entityVipAmount.TotalReturnAmount = (entityVipAmount.TotalReturnAmount == null ? 0 : entityVipAmount.TotalReturnAmount.Value) + amount;

											entityVipAmount.ValidReturnAmount = (entityVipAmount.ValidReturnAmount == null ? 0 : entityVipAmount.ValidReturnAmount.Value) + amount;
											entityVipAmount.ReturnAmount = (entityVipAmount.ReturnAmount == null ? 0 : entityVipAmount.ReturnAmount.Value) + amount;
											bllVipAmount.Update(entityVipAmount);
										}
										tran.Commit();
									}
									catch (Exception) {
										tran.Rollback();
										throw;
									}

								}
                            }
                            //if (dtAmount.Rows.Count > 0 && dtAmountDetail.Rows.Count == dtAmount.Rows.Count)
                            //{
                            //    Utils.SqlBulkCopy(connString, dtAmount, "VipAmount");
                            //}
                            //if (dtAmountDetail.Rows.Count > 0 && dtSplitProfitRecord.Rows.Count > 0)
                            //{
                            //    Utils.SqlBulkCopy(connString, dtSplitProfitRecord, "T_SplitProfitRecord");
                            //    Utils.SqlBulkCopy(connString, dtAmountDetail, "VipAmountDetail");

                            //}
                        }
                    }
                }
            }
        }


        public DataTable CreateTableAmountDetail()
        {
            DataTable dtAmountDetail = new DataTable();
            dtAmountDetail.Columns.Add("VipAmountDetailId", typeof(Guid));
            dtAmountDetail.Columns.Add("VipId", typeof(string));
            dtAmountDetail.Columns.Add("VipCardCode", typeof(string));
            dtAmountDetail.Columns.Add("UnitID", typeof(string));
            dtAmountDetail.Columns.Add("UnitName", typeof(string));
            dtAmountDetail.Columns.Add("SalesAmount", typeof(decimal));
            dtAmountDetail.Columns.Add("Amount", typeof(decimal));
            dtAmountDetail.Columns.Add("UsedReturnAmount", typeof(decimal));
            dtAmountDetail.Columns.Add("Reason", typeof(string));
            dtAmountDetail.Columns.Add("EffectiveDate", typeof(DateTime));
            dtAmountDetail.Columns.Add("DeadlineDate", typeof(DateTime));
            dtAmountDetail.Columns.Add("AmountSourceId", typeof(string));
            dtAmountDetail.Columns.Add("ObjectId", typeof(string));
            dtAmountDetail.Columns.Add("Remark", typeof(string));
            dtAmountDetail.Columns.Add("IsValid", typeof(string));
            dtAmountDetail.Columns.Add("IsWithdrawCash", typeof(string));
            dtAmountDetail.Columns.Add("CustomerID", typeof(string));
            dtAmountDetail.Columns.Add("CreateTime", typeof(DateTime));
            dtAmountDetail.Columns.Add("CreateBy", typeof(string));
            dtAmountDetail.Columns.Add("LastUpdateBy", typeof(string));
            dtAmountDetail.Columns.Add("LastUpdateTime", typeof(DateTime));
            dtAmountDetail.Columns.Add("IsDelete", typeof(int));
            //dtAmountDetail.Columns.Add("IsCalculated", typeof(int));

            return dtAmountDetail;
        }
        public DataTable CreateTableAmount()
        {
            DataTable dtAmount = new DataTable();
            dtAmount.Columns.Add("VipId", typeof(string));
            dtAmount.Columns.Add("VipCardCode", typeof(string));
            dtAmount.Columns.Add("BeginAmount", typeof(string));
            dtAmount.Columns.Add("InAmount", typeof(string));
            dtAmount.Columns.Add("OutAmount", typeof(decimal));
            dtAmount.Columns.Add("EndAmount", typeof(decimal));
            dtAmount.Columns.Add("TotalAmount", typeof(decimal));
            dtAmount.Columns.Add("BeginReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("InReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("OutReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("ReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("ImminentInvalidRAmount", typeof(decimal));
            dtAmount.Columns.Add("InvalidReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("ValidReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("TotalReturnAmount", typeof(decimal));
            dtAmount.Columns.Add("PayPassword", typeof(string));
            dtAmount.Columns.Add("IsLocking", typeof(string));
            dtAmount.Columns.Add("CustomerID", typeof(string));
            dtAmount.Columns.Add("CreateTime", typeof(DateTime));
            dtAmount.Columns.Add("CreateBy", typeof(string));
            dtAmount.Columns.Add("LastUpdateBy", typeof(string));
            dtAmount.Columns.Add("LastUpdateTime", typeof(DateTime));
            dtAmount.Columns.Add("IsDelete", typeof(int));

            return dtAmount;
        }
        public DataTable CreateTableSplitProfitRecord()
        {
            DataTable dtSplitProfitRecord = new DataTable();
            dtSplitProfitRecord.Columns.Add("ID", typeof(Guid));
            dtSplitProfitRecord.Columns.Add("SourceType", typeof(string));
            dtSplitProfitRecord.Columns.Add("SourceId", typeof(string));
            dtSplitProfitRecord.Columns.Add("ObjectId", typeof(string));
            dtSplitProfitRecord.Columns.Add("UserType", typeof(string));
            dtSplitProfitRecord.Columns.Add("UserId", typeof(string));
            dtSplitProfitRecord.Columns.Add("SplitAmount", typeof(string));
            dtSplitProfitRecord.Columns.Add("SplitSattus", typeof(string));
            dtSplitProfitRecord.Columns.Add("CustomerID", typeof(string));
            dtSplitProfitRecord.Columns.Add("CreateTime", typeof(DateTime));
            dtSplitProfitRecord.Columns.Add("CreateBy", typeof(string));
            dtSplitProfitRecord.Columns.Add("LastUpdateTime", typeof(DateTime));
            dtSplitProfitRecord.Columns.Add("LastUpdateBy", typeof(string));
            dtSplitProfitRecord.Columns.Add("IsDelete", typeof(int));
            return dtSplitProfitRecord;
        }


        //VipAmountDetailEntity entityVipAmountDetail = new VipAmountDetailEntity
        //{
        //    VipAmountDetailId = Guid.NewGuid(),
        //    VipId = strVipId,
        //    Amount = amount,
        //    UsedReturnAmount = 0,
        //    EffectiveDate = DateTime.Now,
        //    DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
        //    AmountSourceId = strAmountSourceId,
        //    ObjectId = rechargeOrderInfo.OrderID.ToString(),
        //    CustomerID = loggingSessionInfo.ClientID,
        //    Reason = "超级分销商"
        //};
        //bllVipAmountDetail.Create(entityVipAmountDetail, (SqlTransaction)tran);
        //entityVipAmount = new VipAmountEntity
        //{
        //    VipId = strVipId,
        //    BeginAmount = 0,
        //    InAmount = amount,
        //    OutAmount = 0,
        //    EndAmount = amount,
        //    TotalAmount = amount,
        //    BeginReturnAmount = 0,
        //    InReturnAmount = 0,
        //    OutReturnAmount = 0,
        //    ReturnAmount = 0,
        //    ImminentInvalidRAmount = 0,
        //    InvalidReturnAmount = 0,
        //    ValidReturnAmount = 0,
        //    TotalReturnAmount = 0,
        //    IsLocking = 0,
        //    CustomerID = loggingSessionInfo.ClientID,
        //    VipCardCode = ""

        //};
        //bllVipAmount.Create(entityVipAmount, tran);
    }
}

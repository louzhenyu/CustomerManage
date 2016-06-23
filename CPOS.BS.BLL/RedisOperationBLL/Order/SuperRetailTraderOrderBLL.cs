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
    public class SuperRetailTraderOrderBLL
    {
        public void SetRedisToSuperRetailTraderOrder(LoggingSessionInfo loggingSessionInfo, T_InoutEntity orderInfo)
        {
            var response = RedisOpenAPI.Instance.CCSuperRetailTraderOrder().SetSuperRetailTraderOrder(new CC_Order
            {
                CustomerID = loggingSessionInfo.ClientID,
                OrderId=orderInfo.order_id,
                LogSession = loggingSessionInfo.JsonSerialize(),
                OrderInfo = orderInfo.JsonSerialize()

            });
            if (response.Code == ResponseCode.Fail)
            {
                //直接计算
                 T_InoutBLL inoutBLL=new T_InoutBLL(loggingSessionInfo);
                 inoutBLL.CalculateSuperRetailTraderOrder(loggingSessionInfo, orderInfo);
            }
        }
        /// <summary>
        /// 计算超级分销商佣金，分润用到的批处理
        /// </summary>
        public void CalculateSuperRetailTraderOrderJob()
        {
            var numCount = 50;

             var customerIDs = CustomerBLL.Instance.GetCustomerList();
             foreach (var customer in customerIDs)
             {
                 string connString = customer.Value;
                 var count = RedisOpenAPI.Instance.CCSuperRetailTraderOrder().GetSuperRetailTraderOrderLength(new CC_Order
                 {
                     CustomerID = customer.Key
                 });
                 if (count.Code != ResponseCode.Success)
                 {
                     BaseService.WriteLog("从redis获取待绑定优惠券数量失败");
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
                     DataTable dtProfitDetail = new DataTable();
                     dtProfitDetail.Columns.Add("Id", typeof(Guid));
                     dtProfitDetail.Columns.Add("SuperRetailTraderProfitConfigId", typeof(Guid));
                     dtProfitDetail.Columns.Add("SuperRetailTraderID", typeof(Guid));
                     dtProfitDetail.Columns.Add("Level", typeof(int));
                     dtProfitDetail.Columns.Add("ProfitType", typeof(string));
                     dtProfitDetail.Columns.Add("Profit", typeof(decimal));
                     dtProfitDetail.Columns.Add("OrderType", typeof(string));
                     dtProfitDetail.Columns.Add("OrderId", typeof(string));
                     dtProfitDetail.Columns.Add("OrderNo", typeof(string));
                     dtProfitDetail.Columns.Add("OrderDate", typeof(DateTime));
                     dtProfitDetail.Columns.Add("OrderActualAmount", typeof(decimal));
                     dtProfitDetail.Columns.Add("SalesId", typeof(string));
                     dtProfitDetail.Columns.Add("VipId", typeof(string));
                     dtProfitDetail.Columns.Add("CreateBy", typeof(string));
                     dtProfitDetail.Columns.Add("CreateTime", typeof(DateTime));
                     dtProfitDetail.Columns.Add("LastUpdateBy", typeof(string));
                     dtProfitDetail.Columns.Add("LastUpdateTime", typeof(DateTime));
                     dtProfitDetail.Columns.Add("CustomerId", typeof(string));
                     dtProfitDetail.Columns.Add("IsDelete", typeof(string));

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
                     dtAmountDetail.Columns.Add("IsDelete", typeof(string));


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
                     dtAmount.Columns.Add("IsDelete", typeof(string));

                     for (var i = 0; i < numCount; i++)
                     {
                         var response = RedisOpenAPI.Instance.CCSuperRetailTraderOrder().GetSuperRetailTraderOrder(new CC_Order
                        {
                            CustomerID = customer.Key
                        });
                          if (response.Code == ResponseCode.Success)
                          {
                              var orderInfo = response.Result.OrderInfo.JsonDeserialize<T_InoutEntity>();
                              //var loggingSessionInfo = response.Result.LogSession.JsonDeserialize<LoggingSessionInfo>();
                              var loggingSessionInfo = CustomerBLL.Instance.GetBSLoggingSession(customer.Key, "1");
                              T_InoutBLL inoutBLL = new T_InoutBLL(loggingSessionInfo);
                              //inoutBLL.CalculateSuperRetailTraderOrder(loggingSessionInfo, orderInfo);
                              if (orderInfo != null)
                              {
                                  

                                  if (orderInfo.data_from_id == "35" || orderInfo.data_from_id == "36")
                                  {
                                      T_SuperRetailTraderBLL bllSuperRetailTrader = new T_SuperRetailTraderBLL(loggingSessionInfo);
                                      DataSet dsAllFather = bllSuperRetailTrader.GetAllFather(orderInfo.sales_user);
                                      if (dsAllFather != null && dsAllFather.Tables.Count > 0 && dsAllFather.Tables[0].Rows.Count > 0)
                                      {

                                          T_SuperRetailTraderProfitConfigBLL bllSuperRetailTraderProfitConfig = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
                                          T_SuperRetailTraderConfigBLL bllSuperRetailTraderConfig = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
                                          T_SuperRetailTraderProfitDetailBLL bllSuperRetailTraderProfitDetail = new T_SuperRetailTraderProfitDetailBLL(loggingSessionInfo);

                                          VipAmountBLL bllVipAmount = new VipAmountBLL(loggingSessionInfo);
                                          VipAmountDetailBLL bllVipAmountDetail = new VipAmountDetailBLL(loggingSessionInfo);

                                          var entitySuperRetailTraderProfitConfig = bllSuperRetailTraderProfitConfig.QueryByEntity(new T_SuperRetailTraderProfitConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0, Status = "10" }, null);
                                          var entityConfig = bllSuperRetailTraderConfig.QueryByEntity(new T_SuperRetailTraderConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).SingleOrDefault();
                                          if (entityConfig != null && entitySuperRetailTraderProfitConfig != null)
                                          {

                                              //佣金比列
                                              decimal SkuCommission = Convert.ToDecimal(entityConfig.SkuCommission) * Convert.ToDecimal(0.01);
                                              //商品分润比列
                                              decimal DistributionProfit = Convert.ToDecimal(entityConfig.DistributionProfit) * Convert.ToDecimal(0.01);


                                              foreach (DataRow dr in dsAllFather.Tables[0].Rows)
                                              {
                                                  decimal amount = 0;
                                                  string strAmountSourceId = string.Empty;
                                                  T_SuperRetailTraderProfitConfigEntity singlProfitConfig = new T_SuperRetailTraderProfitConfigEntity();
                                                  if (dr["level"].ToString() == "1")//佣金
                                                  {
                                                      strAmountSourceId = "34";
                                                      singlProfitConfig = entitySuperRetailTraderProfitConfig.Where(a => a.Level == Convert.ToInt16(dr["level"].ToString())).SingleOrDefault();
                                                      if (singlProfitConfig != null)
                                                      {
                                                          if (singlProfitConfig.ProfitType == "Percent")
                                                          {
                                                              amount = Convert.ToDecimal(orderInfo.actual_amount) * SkuCommission;
                                                          }
                                                      }
                                                  }
                                                  else//分润
                                                  {
                                                      strAmountSourceId = "33";
                                                      singlProfitConfig = entitySuperRetailTraderProfitConfig.Where(a => a.Level == Convert.ToInt16(dr["level"].ToString())).SingleOrDefault();
                                                      if (singlProfitConfig != null)
                                                      {
                                                          if (singlProfitConfig.ProfitType == "Percent")
                                                          {
                                                              //amount = Convert.ToDecimal(orderInfo.actual_amount) * DistributionProfit * Convert.ToDecimal(singlProfitConfig.Profit) * Convert.ToDecimal(0.01);
                                                              amount = Convert.ToDecimal(orderInfo.actual_amount)* Convert.ToDecimal(singlProfitConfig.Profit) * Convert.ToDecimal(0.01);
                                                          }
                                                      }
                                                  }
                                                  if (amount > 0)
                                                  {
                                                      IDbTransaction tran = new JIT.CPOS.BS.DataAccess.Base.TransactionHelper(loggingSessionInfo).CreateTransaction();
                                                      try
                                                      {
                                                          //T_SuperRetailTraderProfitDetailEntity entitySuperRetailTraderProfitDetail = new T_SuperRetailTraderProfitDetailEntity()
                                                          //{
                                                          //    SuperRetailTraderProfitConfigId = singlProfitConfig.SuperRetailTraderProfitConfigId,
                                                          //    SuperRetailTraderID = new Guid(dr["SuperRetailTraderID"].ToString()),
                                                          //    Level = Convert.ToInt16(dr["level"].ToString()),
                                                          //    ProfitType = "Cash",
                                                          //    Profit = amount,
                                                          //    OrderType = "Order",
                                                          //    OrderId = orderInfo.order_id,
                                                          //    OrderDate = Convert.ToDateTime(orderInfo.order_date),
                                                          //    VipId = orderInfo.vip_no,
                                                          //    OrderActualAmount = orderInfo.actual_amount,
                                                          //    SalesId = new Guid(orderInfo.sales_user),
                                                          //    OrderNo = orderInfo.order_no,
                                                          //    CustomerId = loggingSessionInfo.ClientID
                                                          //};
                                                          //bllSuperRetailTraderProfitDetail.Create(entitySuperRetailTraderProfitDetail, (SqlTransaction)tran);

                                                          DataRow dr_ProfitDetail = dtProfitDetail.NewRow();
                                                          dr_ProfitDetail["Id"] = Guid.NewGuid();
                                                          dr_ProfitDetail["SuperRetailTraderProfitConfigId"] = new Guid(singlProfitConfig.SuperRetailTraderProfitConfigId.ToString());
                                                          dr_ProfitDetail["SuperRetailTraderID"] = new Guid(dr["SuperRetailTraderID"].ToString());
                                                          dr_ProfitDetail["Level"] = Convert.ToInt16(dr["level"].ToString());
                                                          dr_ProfitDetail["ProfitType"] = "Cash";
                                                          dr_ProfitDetail["Profit"] = amount;
                                                          dr_ProfitDetail["OrderType"] = "Order";
                                                          dr_ProfitDetail["OrderId"] = orderInfo.order_id;
                                                          dr_ProfitDetail["OrderNo"] = orderInfo.order_no;
                                                          dr_ProfitDetail["OrderDate"] = Convert.ToDateTime(orderInfo.order_date);
                                                          dr_ProfitDetail["OrderActualAmount"] = orderInfo.actual_amount;
                                                          dr_ProfitDetail["SalesId"] = new Guid(orderInfo.sales_user);
                                                          dr_ProfitDetail["VipId"] = orderInfo.vip_no;
                                                          dr_ProfitDetail["CreateBy"] = loggingSessionInfo.ClientID;
                                                          dr_ProfitDetail["CreateTime"] = DateTime.Now;
                                                          dr_ProfitDetail["LastUpdateBy"] = loggingSessionInfo.ClientID;
                                                          dr_ProfitDetail["LastUpdateTime"] = DateTime.Now;
                                                          dr_ProfitDetail["CustomerId"] = loggingSessionInfo.ClientID;
                                                          dr_ProfitDetail["IsDelete"] = 0;

                                                          dtProfitDetail.Rows.Add(dr_ProfitDetail);

                                                          VipAmountDetailEntity entityVipAmountDetail = new VipAmountDetailEntity();
                                                          VipAmountEntity entityVipAmount = new VipAmountEntity();
                                                          //entityVipAmountDetail = new VipAmountDetailEntity
                                                          //{
                                                          //    VipAmountDetailId = Guid.NewGuid(),
                                                          //    VipId = dr["SuperRetailTraderID"].ToString(),
                                                          //    Amount = amount,
                                                          //    UsedReturnAmount = 0,
                                                          //    EffectiveDate = DateTime.Now,
                                                          //    DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
                                                          //    AmountSourceId = strAmountSourceId,
                                                          //    ObjectId = orderInfo.order_id,
                                                          //    CustomerID = loggingSessionInfo.ClientID,
                                                          //    Reason = "超级分销商"
                                                          //};
                                                          //bllVipAmountDetail.Create(entityVipAmountDetail, (SqlTransaction)tran);

                                                          DataRow dr_AmountDetail = dtAmountDetail.NewRow();
                                                          dr_AmountDetail["VipAmountDetailId"] = Guid.NewGuid();
                                                          dr_AmountDetail["VipId"] = dr["SuperRetailTraderID"].ToString();
                                                          dr_AmountDetail["VipCardCode"] = "";
                                                          dr_AmountDetail["UnitID"] = "";
                                                          dr_AmountDetail["UnitName"] = "";
                                                          dr_AmountDetail["SalesAmount"] = 0;
                                                          dr_AmountDetail["Amount"] = amount;
                                                          dr_AmountDetail["UsedReturnAmount"] = 0;
                                                          dr_AmountDetail["Reason"] = "超级分销商";
                                                          dr_AmountDetail["EffectiveDate"] = DateTime.Now;
                                                          dr_AmountDetail["DeadlineDate"] = Convert.ToDateTime("9999-12-31 23:59:59");
                                                          dr_AmountDetail["AmountSourceId"] = strAmountSourceId;
                                                          dr_AmountDetail["ObjectId"] = orderInfo.order_id;
                                                          dr_AmountDetail["Remark"] = "超级分销商";
                                                          dr_AmountDetail["IsValid"] = 0;
                                                          dr_AmountDetail["IsWithdrawCash"] = 0;
                                                          dr_AmountDetail["CustomerID"] = loggingSessionInfo.ClientID;
                                                          dr_AmountDetail["CreateTime"] = DateTime.Now;
                                                          dr_AmountDetail["CreateBy"] = loggingSessionInfo.ClientID;
                                                          dr_AmountDetail["LastUpdateBy"] = loggingSessionInfo.ClientID;
                                                          dr_AmountDetail["LastUpdateTime"] = DateTime.Now;
                                                          dr_AmountDetail["IsDelete"] = 0;

                                                          dtAmountDetail.Rows.Add(dr_AmountDetail);

                                                          entityVipAmount = bllVipAmount.QueryByEntity(new VipAmountEntity() { VipId = dr["SuperRetailTraderID"].ToString(), IsDelete = 0, CustomerID = loggingSessionInfo.ClientID }, null).SingleOrDefault();
                                                          if (entityVipAmount == null)
                                                          {
                                                              //    entityVipAmount = new VipAmountEntity
                                                              //    {
                                                              //        VipId = dr["SuperRetailTraderID"].ToString(),
                                                              //        BeginAmount = 0,
                                                              //        InAmount = amount,
                                                              //        OutAmount = 0,
                                                              //        EndAmount = amount,
                                                              //        TotalAmount = amount,
                                                              //        BeginReturnAmount = 0,
                                                              //        InReturnAmount = 0,
                                                              //        OutReturnAmount = 0,
                                                              //        ReturnAmount = 0,
                                                              //        ImminentInvalidRAmount = 0,
                                                              //        InvalidReturnAmount = 0,
                                                              //        ValidReturnAmount = 0,
                                                              //        TotalReturnAmount = 0,
                                                              //        IsLocking = 0,
                                                              //        CustomerID = loggingSessionInfo.ClientID,
                                                              //        VipCardCode = ""

                                                              //    };
                                                              //    bllVipAmount.Create(entityVipAmount, tran);
                                                              DataRow dr_Amount = dtAmount.NewRow();
                                                              dr_Amount["VipId"] = dr["SuperRetailTraderID"].ToString();
                                                              dr_Amount["VipCardCode"] = "";
                                                              dr_Amount["BeginAmount"] = 0;
                                                              dr_Amount["InAmount"] = amount;
                                                              dr_Amount["OutAmount"] = 0;
                                                              dr_Amount["EndAmount"] = amount;
                                                              dr_Amount["TotalAmount"] = amount;
                                                              dr_Amount["BeginReturnAmount"] = 0;
                                                              dr_Amount["InReturnAmount"] = 0;
                                                              dr_Amount["OutReturnAmount"] = 0;
                                                              dr_Amount["ReturnAmount"] = 0;
                                                              dr_Amount["ImminentInvalidRAmount"] = 0;
                                                              dr_Amount["InvalidReturnAmount"] = 0;
                                                              dr_Amount["ValidReturnAmount"] = 0;
                                                              dr_Amount["TotalReturnAmount"] = 0;
                                                              dr_Amount["PayPassword"] = "";
                                                              dr_Amount["IsLocking"] = 0;
                                                              dr_Amount["CustomerID"] = loggingSessionInfo.ClientID;
                                                              dr_Amount["CreateTime"] = DateTime.Now;
                                                              dr_Amount["CreateBy"] = loggingSessionInfo.ClientID;
                                                              dr_Amount["LastUpdateBy"] =loggingSessionInfo.ClientID;
                                                              dr_Amount["LastUpdateTime"] = DateTime.Now;
                                                              dr_Amount["IsDelete"] = 0;
                                                              dtAmount.Rows.Add(dr_Amount);
                                                          }
                                                          else
                                                          {

                                                              entityVipAmount.InReturnAmount = (entityVipAmount.InReturnAmount == null ? 0 : entityVipAmount.InReturnAmount.Value) + amount;
                                                              entityVipAmount.TotalReturnAmount = (entityVipAmount.TotalReturnAmount == null ? 0 : entityVipAmount.TotalReturnAmount.Value) + amount;

                                                              entityVipAmount.ValidReturnAmount = (entityVipAmount.ValidReturnAmount == null ? 0 : entityVipAmount.ValidReturnAmount.Value) + amount;
                                                              entityVipAmount.ReturnAmount = (entityVipAmount.ReturnAmount == null ? 0 : entityVipAmount.ReturnAmount.Value) + amount;

                                                              bllVipAmount.Update(entityVipAmount);
                                                          }
                                                          tran.Commit();
                                                      }
                                                      catch (Exception)
                                                      {
                                                          tran.Rollback();
                                                          throw;
                                                      }
                                                  }

                                              }

                                          }
                                      }
                                  }
                              }
                          }
                     }
                     if (dtAmount.Rows.Count > 0)
                     {
                         Utils.SqlBulkCopy(connString, dtAmount, "VipAmount");
                     }
                     if (dtAmountDetail.Rows.Count>0 && dtProfitDetail.Rows.Count>0)
                     {
                         Utils.SqlBulkCopy(connString, dtProfitDetail, "T_SuperRetailTraderProfitDetail");
                         Utils.SqlBulkCopy(connString, dtAmountDetail, "VipAmountDetail");
                         
                     }
             }
        }

        /// <param name="table">准备更新的DataTable新数据</param>
        /// <param name="TableName">对应要更新的数据库表名</param>
        /// <param name="primaryKeyName">对应要更新的数据库表的主键名</param>
        /// <param name="columnsName">对应要更新的列的列名集合</param>
        /// <param name="limitColumns">需要在ＳＱＬ的ＷＨＥＲＥ条件中限定的条件字符串，可为空。</param>
        /// <param name="onceUpdateNumber">每次往返处理的行数</param>
        /// <returns>返回更新的行数</returns>
        public static int Update(string strCon,DataTable table, string TableName, string primaryKeyName, string[] columnsName, string limitWhere, int onceUpdateNumber)
        {
            if (string.IsNullOrEmpty(TableName)) return 0;
            if (string.IsNullOrEmpty(primaryKeyName)) return 0;
            if (columnsName == null || columnsName.Length <= 0) return 0;
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            int result = 0;
            using (SqlConnection sqlconn = new SqlConnection(strCon))
            {
                sqlconn.Open();

                //使用加强读写锁事务   
                SqlTransaction tran = sqlconn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //所有行设为修改状态   
                        dr.SetModified();
                    }
                    //为Adapter定位目标表   
                    SqlCommand cmd = new SqlCommand(string.Format("select * from {0} where {1}", TableName, limitWhere), sqlconn, tran);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(da);
                    da.AcceptChangesDuringUpdate = false;
                    string columnsUpdateSql = "";
                    SqlParameter[] paras = new SqlParameter[columnsName.Length];
                    //需要更新的列设置参数是,参数名为"@+列名"
                    for (int i = 0; i < columnsName.Length; i++)
                    {
                        //此处拼接要更新的列名及其参数值
                        columnsUpdateSql += ("[" + columnsName[i] + "]" + "=@" + columnsName[i] + ",");
                        paras[i] = new SqlParameter("@" + columnsName[i], columnsName[i]);
                    }
                    if (!string.IsNullOrEmpty(columnsUpdateSql))
                    {
                        //此处去掉拼接处最后一个","
                        columnsUpdateSql = columnsUpdateSql.Remove(columnsUpdateSql.Length - 1);
                    }
                    //此处生成where条件语句
                    string limitSql = ("[" + primaryKeyName + "]" + "=@" + primaryKeyName);
                    SqlCommand updateCmd = new SqlCommand(string.Format(" UPDATE [{0}] SET {1} WHERE {2} ", TableName, columnsUpdateSql, limitSql));
                    //不修改源DataTable   
                    updateCmd.UpdatedRowSource = UpdateRowSource.None;
                    da.UpdateCommand = updateCmd;
                    da.UpdateCommand.Parameters.AddRange(paras);
                    da.UpdateCommand.Parameters.Add("@" + primaryKeyName, primaryKeyName);
                    //每次往返处理的行数
                    da.UpdateBatchSize = onceUpdateNumber;
                    result = da.Update(ds, TableName);
                    ds.AcceptChanges();
                    tran.Commit();

                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    sqlconn.Dispose();
                    sqlconn.Close();
                }


            }
            return result;
        }
    }
}

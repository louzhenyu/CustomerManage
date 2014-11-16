using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.IO;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using JIT.Utility;

using JIT.Utility.DataAccess.Query;
using System.Configuration;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.ApplicationInterface.WithdrawalAndBooking
{
    /// <summary>
    /// WithdrawalGateway 的摘要说明
    /// </summary>
    public class WithdrawalGateway : BaseGateway
    {


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetCustomerWithdrawalList":
                    rst = this.GetCustomerWithdrawalList(pRequest);
                    break;
                case "GetCustomerOrderPayStatus":
                    rst = this.GetCustomerOrderPayStatus(pRequest);
                    break;
                case "getCustomerOrderPayPage":
                    rst = this.GetCustomerOrderPayPage(pRequest);
                    break;
                case "GetOrderSource":
                    rst = this.GetOrderSource();
                    break;
                case "GetCustomerWithdrawal":
                    rst = this.GetCustomerWithdrawal(pRequest); //获取客户提现信息
                    break;
                case "SetWithdrawalPwd":
                    rst = this.SetWithdrawalPwd(pRequest); //修改密码
                    break;
                case "ApplyForWithdrawal":
                    rst = this.ApplyForWithdrawal(pRequest); //申请提现
                    break;
                case "GetOptionsStatus":
                    rst = this.GetOptionsStatus(pRequest);
                    break;
                case "BatchPay":
                    rst = GetTradeCenterPay(pRequest);
                    break;
                case "Pay":
                    rst = GetTradeCenterPay(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        /// <summary>
        /// 获取客户提现信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCustomerWithdrawal(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCustomerWithdrawalInfoRP>>();
            var customerId = loggingSessionInfo.ClientID;

            var rd = new GetCustomerWithdrawalInfoRD();


            var bll = new CustomerWithdrawalBLL(loggingSessionInfo);
            var ds = bll.GetCustomerWithrawalInfo(customerId);

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                GetCustomerWithdrawalInfo customerWithdrawalInfo = new GetCustomerWithdrawalInfo();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    customerWithdrawalInfo.CustomerName = row["CustomerName"].ToString(); //客户名称
                    customerWithdrawalInfo.ReceivingBank = row["ReceivingBank"].ToString();//收款银行
                    if (row["BankAccount"] != DBNull.Value)
                    {
                        int i = 4;
                        string strBankAccount = row["BankAccount"].ToString();
                        string strLeft = strBankAccount.Substring(0, i);
                        string strRight = strBankAccount.Substring(strBankAccount.Length - i, i);
                        customerWithdrawalInfo.BankAccount = strLeft + "**********" + strRight;//收款账号
                    }
                    customerWithdrawalInfo.OpenBank = row["OpenBank"].ToString();  //开户行
                }
                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    customerWithdrawalInfo.CountWithdrawalAmount = row["CountWithdrawalAmount"] != DBNull.Value ? Convert.ToDecimal(row["CountWithdrawalAmount"].ToString()) : 0;  //提现总金额
                    customerWithdrawalInfo.BeenAmount = row["BeenAmount"] != DBNull.Value ? Convert.ToDecimal(row["BeenAmount"].ToString()) : 0;//已到账金额
                    customerWithdrawalInfo.WaitForAmount = row["WaitForAmount"] != DBNull.Value ? Convert.ToDecimal(row["WaitForAmount"].ToString()) : 0;//待出账金额
                    customerWithdrawalInfo.CanWithdrawalAmount = row["WithdrawalAmount"] != DBNull.Value ? Convert.ToDecimal(row["WithdrawalAmount"].ToString()) : 0;//可提现金额
                    if (row["LastWithdrawalTime"] != DBNull.Value)
                    {
                        customerWithdrawalInfo.LastWithdrawalTime = DateTimeExtensionMethods.To19FormatString(Convert.ToDateTime(row["LastWithdrawalTime"]));//上次提现时间 ;
                    }
                    customerWithdrawalInfo.CautionMoney = row["CautionMoney"] != DBNull.Value ? Convert.ToDecimal(row["CautionMoney"].ToString()) : 0; //保证金
                    customerWithdrawalInfo.RefundAmount = row["RefundAmount"] != DBNull.Value ? Convert.ToDecimal(row["RefundAmount"].ToString()) : 0;//退款金额

                }
                //结算信息
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    customerWithdrawalInfo.PaypalRate = row["PaypalRate"] != DBNull.Value ? Convert.ToDecimal(row["PaypalRate"].ToString()) : 0;
                    customerWithdrawalInfo.CUPRate = row["CUPRate"] != DBNull.Value ? Convert.ToDecimal(row["CUPRate"].ToString()) : 0;
                    customerWithdrawalInfo.OffPeriod = row["OffPeriod"] != DBNull.Value ? Convert.ToInt32(row["OffPeriod"].ToString()) : 0;
                    customerWithdrawalInfo.MinAmount = row["MinAmount"] != DBNull.Value ? Convert.ToInt32(row["MinAmount"].ToString()) : 0;
                    customerWithdrawalInfo.PayRemark = row["PayRemark"].ToString();
                }
                rd.GetCustomerWithdrawal = customerWithdrawalInfo;
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 修改提现密码
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetWithdrawalPwd(string pRequest)
        {
            var rsp = new SuccessResponse<IAPIResponseData>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetWithdrawalPwdRP>>();
            var customerId = loggingSessionInfo.ClientID;
            var Oldpwd = rp.Parameters.OldWithdrawalPassword; //提现密码
            var NewPwd = rp.Parameters.NewWithdrawalPassword; //新密码
            var rd = new SetWithdrawalPwdRD();
            var bll = new CustomerBackBLL(loggingSessionInfo);
            CustomerBackEntity entity = new CustomerBackEntity();
            entity.CustomerId = customerId;  //客户ID
            entity.BackStatus = 1;   //账户状态
            entity.WithdrawalPassword = rp.Parameters.OldWithdrawalPassword;
            var ds = bll.QueryByEntity(entity, null);
            if (ds.Length > 0)
            {
                entity = (CustomerBackEntity)ds[0];
                string strPwd = string.Empty; //密码
                if (ds.Length > 0 && ds != null)
                {
                    strPwd = ds.FirstOrDefault().WithdrawalPassword;
                    if (!string.IsNullOrWhiteSpace(strPwd) && strPwd == Oldpwd)  //当数据库中存的密码和输入的密码一样，更新密码
                    {
                        entity.WithdrawalPassword = NewPwd;
                        entity.MD5Pwd = NewPwd.Trim() + MD5Helper.Encryption(customerId.ToString()).Trim();
                        entity.CustomerBackId = Guid.Parse(ds.FirstOrDefault().CustomerBackId.ToString());
                        bll.Update(entity);//更新密码
                        rsp.ResultCode = 0;
                        rsp.Message = "OK";
                        return rsp.ToJSON();
                    }
                }
            }
            else
            {
                rsp.ResultCode = 301;
                rsp.Message = "提现密码输入错误！请重新输入";
                return rsp.ToJSON();
                throw new APIException("提现密码输入错误！") { ErrorCode = 301 };
            }
            return rsp.ToJSON();
        }
        /// <summary>
        /// 申请提现
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string ApplyForWithdrawal(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rd = new ApplyForWithdrawalRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<ApplyForWithdrawalRP>>();
            var customerId = loggingSessionInfo.ClientID; //客户ID
            var WithdrawalAmount = rp.Parameters.WithdrawalAmount; //提现金额
            var WithdrawalPassword = rp.Parameters.WithdrawalPassword;  //提现密码
            var rsp = new SuccessResponse<IAPIResponseData>();

            #region 1.根据CustomerID查询当前客户银行信息
            CustomerBackBLL BackBLL = new CustomerBackBLL(loggingSessionInfo);
            CustomerBackEntity BackEntity = new CustomerBackEntity();
            BackEntity.CustomerId = customerId;
            BackEntity.BackStatus = 1;
            var entityBack = BackBLL.QueryByEntity(BackEntity, null);
            if (entityBack != null && entityBack.Length > 0)
            {
                string Password = entityBack.First().WithdrawalPassword;  //提现密码
                if (!WithdrawalPassword.ToString().Equals(Password))
                {
                    rsp.ResultCode = 303;
                    rsp.Message = "输入密码错误";
                    return rsp.ToJSON();
                    throw new APIException(string.Format("输入密码错误!")) { ErrorCode = 303 };
                }
                else if (Password == "e10adc3949ba59abbe56e057f20f883e")  //1.未修改初始密码.跳转到输入密码页面
                {
                    rsp.ResultCode = 302;
                    return rsp.ToJSON();
                    throw new APIException(string.Format("您的初始密码没有修改。请修改初始密码!")) { ErrorCode = 302 };
                }

                else
                {
                    string MD5Pwd = WithdrawalPassword.ToString().Trim() + MD5Helper.Encryption(customerId.ToString().Trim()); //MD5密码
                    if ((Password.ToString().Trim().Equals(WithdrawalPassword.ToString().Trim())) && entityBack.FirstOrDefault()
.MD5Pwd.ToString().Trim().Equals(MD5Pwd))//2.当客户已经修改密码，并且输入的密码和数据库中客户密码一致.并且数据库中MD5的密码规则一样则提现
                    {
                        #region 根据金额，客户。状态 查询提现主标识
                        #endregion
                        var bll = new CustomerWithdrawalBLL(loggingSessionInfo);
                        string UserId = loggingSessionInfo.CurrentUser.User_Id;
                        // string strWithdrawalld = bll.GetWithdrawalID(customerId, 20);
                        var tran = bll.GetTran();
                        try
                        {
                            if (!bll.GetWithdrawalDayByMaxPeriod(customerId))
                            {
                                rsp.ResultCode = 305;
                                rsp.Message = "没到提现周期。不能提现";
                                return rsp.ToJSON();
                                throw new APIException("没到提现周期。不能提现") { ErrorCode = 305 };
                            }
                            else
                            {
                                #region 老版本提现。废弃
                                //#region 1.根据提现主标识更新对应的状态和更新人。
                                //bll.UpdateWithdrawalStatus(customerId, strWithdrawalld, 30, UserId);
                                //#endregion
                                //#region 2。根据提现主标识，更新订单支付明细的状态和时间
                                ////提现明细
                                //CustomerWithdrawalDetailBLL blldetail = new CustomerWithdrawalDetailBLL(loggingSessionInfo);
                                //CustomerOrderPayBLL orderPayBLL = new CustomerOrderPayBLL(loggingSessionInfo);

                                //orderPayBLL.UpdateOrderPayList(strWithdrawalld, customerId, 30, UserId);
                                //#endregion

                                //#region 根据CustomerId 3更新客户可提取的现金金额。已提取的金额累加，当前余额-提取金额
                                //CustomerAmountBLL amountBLL = new CustomerAmountBLL(loggingSessionInfo);
                                //CustomerAmountEntity amountentity = new CustomerAmountEntity();
                                //amountentity.CustomerId = customerId;//客户ID
                                //decimal strOutAmount, strWithdrawalAmount, strEndAmount;//已提取金额。可提现金额,当前余额
                                //var temp = amountBLL.QueryByEntity(amountentity, null);
                                //if (temp.Length > 0)
                                //{
                                //    strOutAmount = Convert.ToDecimal(temp.FirstOrDefault().OutAmount);
                                //    strWithdrawalAmount = Convert.ToDecimal(temp.FirstOrDefault().WithdrawalAmount);
                                //    strEndAmount = Convert.ToDecimal(temp.FirstOrDefault().EndAmount);
                                //    amountentity = temp[0];
                                //    amountentity.WithdrawalAmount = 0;//可提现金额变为0;
                                //    amountentity.OutAmount = strOutAmount + rp.Parameters.WithdrawalAmount;//已提取金额累加
                                //    amountentity.EndAmount = strEndAmount - WithdrawalAmount; //余额-提现金额
                                //    amountBLL.Update(amountentity, null);
                                //}
                                //#endregion
                                #endregion

                                int returnValue = bll.getApplyForWithdrawal(customerId, UserId, WithdrawalAmount);
                                if (returnValue == 0)
                                {
                                    rsp.ResultCode = 0;
                                    rsp.Message = "OK";
                                    return rsp.ToJSON();
                                }
                                if (returnValue == 1)
                                {
                                    rsp.ResultCode = 306;
                                    rsp.Message = "提现失败！";
                                    return rsp.ToJSON();
                                }
                                if (returnValue == 304)
                                {
                                    rsp.ResultCode = 304;
                                    rsp.Message = "没有可提取金额！不能提现";
                                    return rsp.ToJSON();
                                    throw new APIException("没有可提取金额！不能提现") { ErrorCode = 304 };
                                }

                            }

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw new APIException(ex.Message);
                        }
                    }
                }
            }
            #endregion
            rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        public string GetCustomerWithdrawalList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCustomerWithdrawalListRP>>();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var customerId = loggingSessionInfo.ClientID;

            var rd = new GetCustomerWithdrawalListRD();


            var beginDate = rp.Parameters.BeginDate;
            var endDate = rp.Parameters.EndDate;
            //if (rp.Parameters.BeginDate == "" || string.IsNullOrEmpty(rp.Parameters.BeginDate))
            //{
            //    beginDate = "1999-01-01";
            //}
            //if (rp.Parameters.EndDate == "" || string.IsNullOrEmpty(rp.Parameters.EndDate))
            //{
            //    endDate = DateTime.Now.ToString("yyyy-MM-dd");
            //}

            var serialNo = rp.Parameters.SerialNo;

            var status = rp.Parameters.Status;
            var pageIndex = rp.Parameters.PageIndex;
            var pageSize = rp.Parameters.PageSize;
            if (pageIndex==null)
            {
                if (HttpContext.Current.Request["page"] != null)
                {

                pageIndex=int.Parse(HttpContext.Current.Request["page"])-1;

                }
            }


            var bll = new CustomerWithdrawalBLL(loggingSessionInfo);

            var ds = bll.GetCustomerWithdrawalList(customerId, serialNo, beginDate, endDate, status, pageIndex ?? 0, pageSize ?? 15);

            var pageDs = bll.GetCustomerWithdrawalList(customerId, serialNo, beginDate, endDate, status, 0, Int32.MaxValue);
         
            if (ds.Tables[0].Rows.Count > 0)
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new CustomerWithdrawalInfo
                {
                    WithdrawalId = t["WithdrawalId"].ToString(),
                    SerialNo = t["SerialNo"].ToString(),
                    WithdrawalTime = t["WithdrawalTime"].ToString(),
                    CustomerName = t["CustomerName"].ToString(),
                    ReceivingBank = t["ReceivingBank"].ToString(),
                    BankAccount = t["BankAccount"].ToString(),
                    WithdrawalStatus = t["WithdrawalStatus"].ToString(),
                    FailureReason = t["FailureReason"].ToString(),
                    WithdrawalAmount = Convert.ToDecimal(t["WithdrawalAmount"])
                });
                rd.TotalPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(pageDs.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));
                rd.TotalCount = pageDs.Tables[0].Rows.Count;

                rd.CustomerWithdrawalList = tmp.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        public string GetCustomerOrderPayStatus(string pRequest)
        {
            // var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var rd = new GetCustomerOrderPayStatusRD();
            // var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new CustomerWithdrawalBLL(loggingSessionInfo);

            var ds = bll.GetCustomerOrderPayStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {
                var tmp = ds.Tables[0].AsEnumerable().Select(t => new GetCustomerOrderPayStatusInfo()
                {
                    StatusValue = t["OptionValue"].ToString(),
                    StatusName = t["OptionText"].ToString()
                });
                rd.GetCustomerOrderPayStatusList = tmp.ToArray();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        #region 入账查询
        public string GetCustomerOrderPayPage(string pRequest)
        {
            GetCustomerOrderPayInfoRD infoRd = new GetCustomerOrderPayInfoRD();
            try
            {
                ReqCustomer entity = pRequest.DeserializeJSONTo<ReqCustomer>();
                CustomerOrderPayEntity orer = new CustomerOrderPayEntity()
                {
                    OrderNo = entity.Parameters.OrderNo,
                    OrderSource = entity.Parameters.OrderSource,
                    PayTimeBegin = entity.Parameters.PayTimeBegin,
                    PayTimeEnd = entity.Parameters.PayTimeEnd,
                    OrderPayStatus = entity.Parameters.OrderPayStatus,
                    WithdrawalId = entity.Parameters.WithdrawalId

                };
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                CustomerOrderPayBLL server = new CustomerOrderPayBLL(loggingSessionInfo);
                DataSet ds = server.GetCustomerOrderPayPage(orer, (int)entity.Parameters.PageIndex, (int)entity.Parameters.PageSize);
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    infoRd.CustomerOrderList = DataTableToObject.ConvertToList<CustomerOrderPayInfo>(ds.Tables[1]);
                    infoRd.PageCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PageCount"]);
                    infoRd.RowsCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RowsCount"]);
                }
                else
                {
                    infoRd.CustomerOrderList = new List<CustomerOrderPayInfo>();
                    infoRd.PageCount = 0;
                    infoRd.RowsCount = 0;
                }
                var rsp = new SuccessResponse<IAPIResponseData>(infoRd);
                return rsp.ToJSON();
            }
            catch (Exception)
            {
                var rsp = new SuccessResponse<IAPIResponseData>(infoRd);
                return infoRd.ToJSON();
            }
        }
        #endregion
        #region 获取订单来源
        public string GetOrderSource()
        {
            List<OrderSource> listSource = new List<OrderSource>();
            listSource.Add(new OrderSource { VipSourceID = 3, VipSourceName = "微信" });
            listSource.Add(new OrderSource { VipSourceID = 2, VipSourceName = "门店Pad" });
            OrderSourceRD rd = new OrderSourceRD();
            rd.listSource = listSource;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 获取状态
        public string GetOptionsStatus(string pRequest)
        {
            OptionStatusRD rd = new OptionStatusRD();
            try
            {
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                ReqOptionStatus entity = pRequest.DeserializeJSONTo<ReqOptionStatus>();
                OptionsBLL server = new OptionsBLL(loggingSessionInfo);
                OrderBy[] orders = new OrderBy[1];
                orders[0] = new OrderBy() { FieldName = "Sequence", Direction = OrderByDirections.Asc };
                OptionsEntity[] options = server.QueryByEntity(new OptionsEntity { OptionName = entity.Parameters.statusName, IsDelete = 0 }, orders);
                rd.listOptionStatus = options;
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region 代付
        public string GetTradeCenterPay(string pRequest)
        {
            ReqTradeCenterPay entity = pRequest.DeserializeJSONTo<ReqTradeCenterPay>();
            var rsp = new SuccessResponse<IAPIResponseData>();
            string SeriaNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            string result = string.Empty;
            try
            {
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                string Key = "";
                if (!string.IsNullOrEmpty(entity.Parameters.SeriaNo))
                {
                    Key = entity.Parameters.SeriaNo;
                }
                string Parameters = "";
                var bll = new CustomerWithdrawalBLL(loggingSessionInfo);
                DataSet ds = bll.GetTradeCenterPay(Key);
                if (ds == null || ds.Tables.Count < 2)
                {
                    return "";
                }
                if (ds.Tables[1] == null || ds.Tables[1].Rows.Count < 1)
                {
                    rsp.ResultCode = 00001;
                    rsp.Message = "无可代付数据";
                    return rsp.ToJSON();
                }
                if (ds.Tables[0] == null || ds.Tables[0].Rows.Count < 1)
                {
                    rsp.ResultCode = 00001;
                    rsp.Message = "无客户信息";
                    return rsp.ToJSON();
                }
                Parameters = GetXml(ds.Tables[0], ds.Tables[1], SeriaNo, Key);
                string str = "request={\"ClientID\":\"" + loggingSessionInfo.ClientID + "\","
                     + "\"UserID\":\"" + loggingSessionInfo.UserID + "\","
                     + "\"Token\":null,\"AppID\":1,"
                     + "\"Parameters\":" + Parameters + "}";

                string url = ConfigurationManager.AppSettings["TradeCenterPayUrl"];

                if (string.IsNullOrEmpty(Key))
                {
                    result = HttpWebClient.DoHttpRequest(url + "?action=BatchPay", str);

                }
                else
                {
                    result = HttpWebClient.DoHttpRequest(url + "?action=Pay", str);
                }
                ResTradeCenterPay req = result.DeserializeJSONTo<ResTradeCenterPay>();
                if (req.ResultCode == "0")
                {
                    if (req.Datas.ResultCode == "0000")
                    {
                        SetCustomerWithdrawal(Key);
                        string pRes = CreateCustomerWithdrawalTransfer(SeriaNo, str, ds.Tables[1], 40);
                        return result;
                    }
                    rsp.ResultCode = 00001;
                    rsp.Message = req.Datas.Message;
                    return rsp.ToJSON();
                }
                else
                {
                    string pRes = CreateCustomerWithdrawalTransfer(SeriaNo, str, ds.Tables[1], 0);
                }

            }
            catch (Exception)
            {

                rsp.ResultCode = 00001;
                rsp.Message = "错误";
                return rsp.ToJSON();
            }
            return result;
        }
        public string GetXml(DataTable Customer, DataTable CustomerWithdrawal, string SeriaNo, string Key)
        {
            string PayChannelID = ConfigurationManager.AppSettings["PayChannelID"].ToString();
            string Parameters = "{\"ChannelId\":\"" + PayChannelID + "\",\"AppOrderID\":\"" + SeriaNo + "\""
                              + ",\"transTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\"";


            if (!string.IsNullOrEmpty(Key))
            {
              //  Parameters += ",\"account\":\"" + Customer.Rows[0]["BankAccount"].ToString() + "\",\"accBankCode\":\"" + Customer.Rows[0]["BackCode"].ToString() + "\",\"accName\":\"" + Customer.Rows[0]["CustomerName"].ToString() + "\",\"amount\":\"" + Convert.ToInt32(Convert.ToDecimal(CustomerWithdrawal.Rows[0]["WithdrawalAmount"].ToString()) * 100) + "\"}";
                  Parameters += ",\"account\":\"" + Customer.Rows[0]["BankAccount"].ToString() + "\",\"accBankCode\":\"" + Customer.Rows[0]["BackCode"].ToString() + "\",\"accName\":\"" + Customer.Rows[0]["CustomerName"].ToString() + "\",\"amount\":\"1\"}";
            }
            else
            {
                string pStr = string.Empty;
                foreach (DataRow item in CustomerWithdrawal.Rows)
                {
                 //   pStr += "|" + Convert.ToInt32(Convert.ToDecimal(item["WithdrawalAmount"].ToString()) * 100) + "," + Customer.Rows[0]["BankAccount"].ToString() + "," + Customer.Rows[0]["CustomerName"].ToString() + "," + Customer.Rows[0]["BackCode"] + ",0,0," + item["SerialNo"];
                    pStr += "|1," + Customer.Rows[0]["BankAccount"].ToString() + "," + Customer.Rows[0]["CustomerName"].ToString() + "," + Customer.Rows[0]["BackCode"] + ",0,0," + item["SerialNo"];
                }
                pStr = pStr.TrimStart('|');
                Parameters += ",\"transList\":\"" + pStr + "\"}";
            }
            return Parameters;
        }

        /// <summary>
        /// 创建提现数据
        /// </summary>
        public string CreateCustomerWithdrawalTransfer(string SeriaNo, string json, DataTable CustomerWithdrawal, int status)
        {

            try
            {
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                CustomerWithdrawalTransferBLL bll = new CustomerWithdrawalTransferBLL(loggingSessionInfo);

                CustomerWithdrawalTransferEntity entity = new CustomerWithdrawalTransferEntity
                {
                    SerialNo = SeriaNo,
                    TransferId = Guid.NewGuid(),
                    TransferTime = DateTime.Now,
                    TransferStatus = status,
                    TransferUserId = loggingSessionInfo.UserID,
                    TransferInfo = json,
                    IsDelete = 0, 
                    CustomerId = loggingSessionInfo.ClientID
                }; 

                bll.Create(entity, null);
                CustomerWithdrawalTransferMappingBLL server = new CustomerWithdrawalTransferMappingBLL(loggingSessionInfo);
                foreach (DataRow item in CustomerWithdrawal.Rows)
                {
                   
                    CustomerWithdrawalTransferMappingEntity mapping = new CustomerWithdrawalTransferMappingEntity
                    {
                        IsDelete = 0,
                        MappingId = Guid.NewGuid(),
                        WithdrawalId = (Guid)item["WithdrawalId"],
                        TransferId = entity.TransferId,
                        CreateBy = loggingSessionInfo.UserID
                    };
                    server.Create(mapping, null);

                }
                return entity.TransferId.ToString();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 修改提现后状态
        /// </summary>
        /// <param name="SeriaNo"></param>
        public void SetCustomerWithdrawal(string SeriaNo)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            CustomerWithdrawalBLL bll = new CustomerWithdrawalBLL(loggingSessionInfo);
            bll.SetTradeCenterPay(SeriaNo);

        }
        #endregion
    }


    #region 获取客户提现基本信息

    public class GetCustomerWithdrawalInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerID { get; set; }

        public void Validate()
        {

        }
    }
    public class GetCustomerWithdrawalInfoRD : IAPIResponseData
    {

        public GetCustomerWithdrawalInfo GetCustomerWithdrawal { get; set; }

        #region  结算信息
        #endregion
    }
    public class GetCustomerWithdrawalInfo
    {
        #region 申请提现
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 提现总额
        /// </summary>
        public decimal CountWithdrawalAmount { get; set; }

        /// <summary>
        /// 已到账总额
        /// </summary>
        public decimal BeenAmount { get; set; }

        /// <summary>
        /// 收款银行
        /// </summary>
        public string ReceivingBank { get; set; }


        /// <summary>
        /// 收款账号
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string OpenBank { get; set; }

        /// <summary>
        /// 可提取金额
        /// </summary>
        public decimal CanWithdrawalAmount { get; set; }


        /// <summary>
        /// 等待出账金额
        /// </summary>
        public decimal WaitForAmount { get; set; }


        /// <summary>
        /// 上次提现时间
        /// </summary>
        public string LastWithdrawalTime { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal CautionMoney { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 支付宝汇率
        /// </summary>
        public decimal PaypalRate { get; set; }

        /// <summary>
        /// 银联汇率
        /// </summary>
        public decimal CUPRate { get; set; }

        /// <summary>
        /// 结算周期
        /// </summary>
        public int OffPeriod { get; set; }

        /// <summary>
        /// 最低结算费用
        /// </summary>
        public int MinAmount { get; set; }

        /// <summary>
        /// 支付说明
        /// </summary>
        public string PayRemark { get; set; }

        #endregion
    }
    #endregion
    #region 修改密码
    public class SetWithdrawalPwdRP : IAPIRequestParameter
    {
        /// <summary>
        /// 提现密码(当前密码)
        /// </summary>
        public string OldWithdrawalPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewWithdrawalPassword { get; set; }

        ///// <summary>
        ///// 确认密码
        ///// </summary>
        //public string AffirmWithdrawalPassword { get; set; }




        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(OldWithdrawalPassword))
            {
                throw new APIException("当前密码不能为空！");
            }
        }
    }

    public class SetWithdrawalPwdRD : IAPIResponseData
    {
    }

    #endregion
    #region  申请提现
    public class ApplyForWithdrawalRP : IAPIRequestParameter
    {
        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal WithdrawalAmount { get; set; }

        /// <summary>
        /// 提现密码
        /// </summary>
        public string WithdrawalPassword { get; set; }


        public void Validate()
        {

        }
    }

    public class ApplyForWithdrawalRD : IAPIResponseData
    {

    }
    #endregion
    #region 提现记录查询
    public class GetCustomerWithdrawalListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string SerialNo { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        /// <summary>
        /// 1=成功；0=失败；-1=全部
        /// </summary>
        public int Status { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public void Validate()
        {
        }
    }

    public class GetCustomerWithdrawalListRD : IAPIResponseData
    {
        public CustomerWithdrawalInfo[] CustomerWithdrawalList { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }
    }

    public class CustomerWithdrawalInfo
    {
        public string WithdrawalId { get; set; }
        public string SerialNo { get; set; }
        public string WithdrawalTime { get; set; }
        public string CustomerName { get; set; }
        public string ReceivingBank { get; set; }
        public string BankAccount { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public string WithdrawalStatus { get; set; }
        public string FailureReason { get; set; }
    }
    #endregion
    #region 提现状态列表
    public class GetCustomerOrderPayStatusRD : IAPIResponseData
    {
        public GetCustomerOrderPayStatusInfo[] GetCustomerOrderPayStatusList { get; set; }
    }
    public class GetCustomerOrderPayStatusInfo
    {
        public string StatusValue { get; set; }
        public string StatusName { get; set; }
    }
    #endregion
    #region 订单来源
    public class OrderSource
    {
        public int VipSourceID { set; get; }
        public string VipSourceName { set; get; }
    }
    public class OrderSourceRD : IAPIResponseData
    {
        public List<OrderSource> listSource { set; get; }

    }
    #endregion
    #region 入账查询
    public class GetCustomerOrderPayInfoRD : IAPIResponseData
    {
        public List<CustomerOrderPayInfo> CustomerOrderList { set; get; }
        public int PageCount { set; get; }
        public int RowsCount { set; get; }

    }
    #region 请求参数
    public class ReqCustomer
    {
        public ACustomerOrderPayInfoRP Parameters { set; get; }
        public string CustomerID { set; get; }

    }

    public class ACustomerOrderPayInfoRP : IAPIRequestParameter
    {
        public string OrderNo { set; get; }
        public int? OrderSource { set; get; }
        public DateTime PayTimeBegin { set; get; }
        public DateTime PayTimeEnd { set; get; }
        public int? OrderPayStatus { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public Guid? WithdrawalId { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
    #region 返回参数
    public class CustomerOrderPayInfo
    {
        public Guid? OrderPayId { get; set; }
        public String OrderId { get; set; }
        public string OrderNo { set; get; }//订单号
        public string SerialPay { set; get; }//流水号
        public decimal PayAmount { set; get; }//订单支付金额
        public string VipSourceName { set; get; }//订单来源
        public string PaymentName { set; get; }//支付方式
        public string PayTime { set; get; }//付款时间
        public string OrderPayStatusName { set; get; }//状态描述
        public decimal AladingRate { set; get; }//费率
        public decimal WithdrawalAmount { set; get; }//可提现金额
    }
    #endregion
    #endregion

    #region 代付
    #region 请求参数
    public class ReqTradeCenterPay
    {
        public TradeCenterPay Parameters { set; get; }
    }

    public class TradeCenterPay
    {
        public string SeriaNo { set; get; }
    }

    #endregion
    #endregion

    #region 接收
    public class ResTradeCenterPay
    {
        public string ResultCode { set; get; }
        public ReqDatas Datas { set; get; }

    }
    public class ReqDatas
    {
        public string ResultCode { set; get; }
        public string Message { set; get; }
    }
    #endregion

    #region 获取单据状态
    public class OptionStatusRD : IAPIResponseData
    {
        public OptionsEntity[] listOptionStatus { set; get; }

    }
    //public class OptionStatus
    //{
    //    public int OptionValue { set; get; }
    //    public string OptionText { set; get; }
    //}
    /// <summary>
    /// 请求参数
    /// </summary>
    public class ReqOptionStatus
    {
        public OptionStatusInfoRP Parameters { set; get; }
        public string CustomerID { set; get; }

    }
    public class OptionStatusInfoRP : IAPIRequestParameter
    {

        public string statusName { set; get; }
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
    #endregion


}
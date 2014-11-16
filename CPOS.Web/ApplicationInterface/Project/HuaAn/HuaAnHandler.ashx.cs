using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// HuaAnHandler 的摘要说明
    /// </summary>
    public class HuaAnHandler : BaseGateway
    {
        // private const string m_logisticsinfo = "08881000000874111";  //交易号  
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            //2.根据action做不同的业务处理
            var rst = string.Empty;
            switch (pAction)
            {
                case "GetHouses":  //获取房产列表
                    rst = GetHousesList(pRequest);
                    break;
                case "GetMyHouses":  //获取我的房产
                    rst = GetMyHousesList(pRequest);
                    break;
                case "GetMyHouseDetail":  //获取我的楼盘详细
                    rst = GetMyHouseDetail(pRequest);
                    break;
                case "VerifIsRegister":   //验证用户是否注册
                    rst = VerifIsRegister(pRequest);
                    break;
                case "GetHouseProfitList":   //获取我的收益
                    rst = GetHouseProfitList(pRequest);
                    break;
                case "BuyFund":      //基金购买
                    rst = BuyFund(pRequest);
                    break;
                case "FundRansom":      //基金赎回
                    rst = FundRansom(pRequest);
                    break;
                case "PayHouse":      //用号
                    rst = PayHouse(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }
            return rst;
        }

        #region 接口处理逻辑

        #region 验证用户是否注册
        /// <summary>
        /// 判断用户是否注册。
        /// </summary>
        /// <returns></returns>
        private string VerifIsRegister(string reqContent)
        {
            var rd = new APIResponse<VipEntityRD>();
            try
            {
                var rp = reqContent.DeserializeJSONTo<APIRequest<VipEntityRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                ReserveBLL bll = new ReserveBLL(loggingSessionInfo);
                int count = bll.GetVipInfo(rp.UserID, rp.CustomerID);
                var rdData = new VipEntityRD();
                rdData.IsRegister = count.ToString();
                rd.Data = rdData;
                rd.Message = "成功";
            }
            catch (Exception)
            {
                throw;
            }
            return rd.ToJSON();
        }
        #endregion


        #region 获取房产列表数据
        /// <summary>
        /// 获取房产列表数据
        /// </summary>
        /// <returns></returns>
        public string GetHousesList(string pRequest)
        {
            var rd = new APIResponse<HouseDataRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HouseDataRP>>();
                //2.对请求参数进行验证
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                WXHouseBuildBLL bll = new WXHouseBuildBLL(loggingSessionInfo);
                int totalCount = bll.GetHousePageCount(rp.CustomerID, rp.Parameters.PageSize);
                int pageIndex = rp.Parameters.PageIndex;
                if (pageIndex < 1)
                {
                    rp.Parameters.PageIndex = 1;
                }
                if (pageIndex > totalCount)
                {
                    rp.Parameters.PageIndex = totalCount;
                }

                DataSet ds = bll.GetHouses(rp.CustomerID, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                List<HouseViewModel> list = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<HouseViewModel>(ds.Tables[0]);
                }

                List<HouseViewModel> returnList = new List<HouseViewModel>();
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var buildEntity = item;
                        if (item.HouseOpenDate != null)
                        {
                            buildEntity.HouseOpenDate2 = item.HouseOpenDate.ToString("yyyy年MM月dd日");
                        }

                        returnList.Add(buildEntity);
                    }
                }

                var rdData = new HouseDataRD();
                rdData.HouseList = returnList;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 300;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        private string GetHouseDetailUrl(LoggingSessionInfo loggingSessionInfo, string customerID, Guid? houseID)
        {
            if (string.IsNullOrEmpty(customerID)) new ArgumentNullException("customer 不能为空！");

            if (houseID.HasValue)
            {
                string objectId = houseID.Value.ToString();
                ObjectImagesBLL imgBll = new ObjectImagesBLL(loggingSessionInfo);
                ObjectImagesEntity imgEntity = imgBll.GetObjectImagesByCustomerId(customerID, objectId);
                if (imgEntity != null)
                {
                    return imgEntity.ImageURL;
                }
            }


            return string.Empty;
        }

        #region 获取我的房产
        /// <summary>
        /// 获取我的房产
        /// </summary>
        /// <returns></returns>
        public string GetMyHousesList(string pRequest)
        {
            var rd = new APIResponse<MyHouseDataRD>();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<MyHouseDataRP>>();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                WXHouseBuildBLL bll = new WXHouseBuildBLL(loggingSessionInfo);
                int totalCount = bll.GetMyHousePageCount(rp.CustomerID, rp.UserID, rp.Parameters.PageSize);
                int pageIndex = rp.Parameters.PageIndex;
                if (pageIndex < 1)
                {
                    rp.Parameters.PageIndex = 1;
                }
                if (pageIndex > totalCount)
                {
                    rp.Parameters.PageIndex = totalCount;
                }

                DataSet ds = bll.GetMyHouse(rp.CustomerID, rp.UserID, rp.Parameters.PageIndex, rp.Parameters.PageSize);
                List<MyHouseViewModel> list = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<MyHouseViewModel>(ds.Tables[0]);
                }

                List<MyHouseViewModel> returnList = new List<MyHouseViewModel>();
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        var buildEntity = item;
                        if (item.BuyHouseDate != null)
                        {
                            buildEntity.HouseOpenDate = item.BuyHouseDate.ToString("yyyy年MM月dd日");
                        }

                        returnList.Add(buildEntity);
                    }
                }

                var rdData = new MyHouseDataRD();
                rdData.HouseList = returnList;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 300;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region 获取我的楼盘详细信息
        /// <summary>
        /// 获取我的楼盘详细信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetMyHouseDetail(string pRequest)
        {
            var rd = new APIResponse<HouseDetailRD>();

            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<HouseDetailRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                string thirdOrderNo = rp.Parameters.ThirdOrderNo;
                WXHouseBuildBLL bll = new WXHouseBuildBLL(loggingSessionInfo);
                DataSet ds = bll.GetMyHouseDetail(rp.CustomerID, rp.UserID, rp.Parameters.PrePaymentID, thirdOrderNo);
                MyHouseDetailViewModel data = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    data = DataTableToObject.ConvertToList<MyHouseDetailViewModel>(ds.Tables[0]).FirstOrDefault();
                }

                if (data != null)
                {
                    // true:显示按钮，false 不显示按钮
                    //在售：显示赎回和支付
                    if (data.HouseState == (int)HouseStateEnum.Sold)
                    {
                        if (data.BuyFundState == 1)   //已购买基金成功
                        {
                            if (data.RedeemFundState == 1 || data.PayFundState == 1) //赎回或者过户成功
                            {
                                //禁用赎回和过户按钮
                                data.DisablePay = false;
                                data.DisabledRedeem = false;
                                if (data.RedeemFundState == 1) data.Msg = "您已退订基金";

                                if (data.PayFundState == 1) data.Msg = "您已转定基金";
                            }
                            else if (data.RedeemFundState == 3 || data.PayFundState == 3)
                            {
                                data.DisablePay = false;
                                data.DisabledRedeem = false;
                                if (data.RedeemFundState == 3) data.Msg = "委托受理中，将于2个工作日内完成转账";

                                if (data.PayFundState == 3) data.Msg = "委托受理中，将于2个工作日内完成转账";
                            }
                            else if (data.RedeemFundState == 4 || data.PayFundState == 4)  //赎回失败 过户失败
                            {
                                data.DisablePay = true;
                                data.DisabledRedeem = true;

                                if (data.RedeemFundState == 4) data.Msg = data.RedeemRetMsg;

                                if (data.PayFundState == 4) data.Msg = data.PayRetMsg;
                            }
                            else
                            {
                                data.DisablePay = true;
                                data.DisabledRedeem = true;
                            }
                        }
                        else if (data.BuyFundState == 3)
                        {
                            data.Msg = "委托受理中，将于1个工作日内完成转账";
                            data.DisablePay = false;
                            data.DisabledRedeem = false;
                        }
                        else if (data.BuyFundState == 4)   //委托失败
                        {
                            data.Msg = data.BuyRetMsg;
                            data.DisablePay = false;
                            data.DisabledRedeem = false;
                        }

                        data.IsShowRansom = true;
                        data.IsShowTransfer = true;
                    }
                    else if (data.HouseState == (int)HouseStateEnum.Unsold)   //房子在未售状态，是不能“赎回”和“过户”的
                    {
                        data.IsShowRansom = false;
                        data.IsShowTransfer = false;
                    }
                    if (data.BuyHouseDate != null)
                    {
                        data.HouseOpenDate = data.BuyHouseDate.ToString("yyyy年MM月dd日");
                    }
                }

                var rdData = new HouseDetailRD();
                rdData.HouseDetail = data;
                rd.Data = rdData;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 300;
                rd.Message = "业务处理失败";
            }
            return rd.ToJSON();
        }
        #endregion

        #region 获取我的收益
        /// <summary>
        /// 获取我的收益
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        private string GetHouseProfitList(string reqContent)
        {
            var rd = new APIResponse<HouseProfitListRD>();
            try
            {
                var rp = reqContent.DeserializeJSONTo<APIRequest<HouseProfitRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                var rdData = new HouseProfitListRD();

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                string customerID = loggingSessionInfo.ClientID;
                string userID = rp.UserID;

                // 判断用户是否购买楼盘，如果已购买，就算没有产生收益（收益页面显示为0：昨日收益，可用金额，总收益，万份收益），
                WXHouseBuildBLL buildBll = new WXHouseBuildBLL(loggingSessionInfo);
                int count = buildBll.GetMyHouseCount(rp.CustomerID, rp.UserID);
                if (count <= 0)
                {
                    rdData.IsBuyFund = false;
                    rd.ResultCode = 21;
                    rd.Message = "您还没有预定任何楼盘";
                    rd.Data = rdData;
                }
                else
                {
                    string yesterProfit = string.Empty;
                    string totalAssetsMoney = string.Empty;
                    string grandProfit = string.Empty;

                    WXHouseAssignbuyerBLL assBLL = new WXHouseAssignbuyerBLL(loggingSessionInfo);
                    WXHouseAssignbuyerEntity assEntity = assBLL.GetWXHouseAssignbuyer(loggingSessionInfo.ClientID, rp.UserID);
                    //WXHouseAssignbuyerEntity assEntity = assBLL.GetWXHouseAssignbuyer("2aa965e35e14485e9eb000be599f8355", "9502c38967574538a610b68923aa1a07");
                    if (assEntity == null)
                    {
                        throw new APIException("没有找到该用户信息");
                    }

                    WXHouseProfitListBLL profitBLL = new WXHouseProfitListBLL(loggingSessionInfo);
                    DataSet ds = profitBLL.GetWXHouseProfitList(assEntity.Assignbuyer, rp.CustomerID);
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        yesterProfit = ds.Tables[0].Rows[0]["YesterdayProfit"].ToString();   //昨日收益

                        //可用总金额
                        totalAssetsMoney = ds.Tables[0].Rows[0]["TotalAssetsMoney"].ToString();
                        //总收益
                        grandProfit = ds.Tables[0].Rows[0]["GrandProfit"].ToString();
                    }

                    rdData.YesterdayProfit = string.IsNullOrEmpty(yesterProfit) ? "0" : yesterProfit;
                    rdData.TotalAssetsMoney = string.IsNullOrEmpty(totalAssetsMoney) ? "0" : totalAssetsMoney;
                    rdData.GrandProfit = string.IsNullOrEmpty(grandProfit) ? "0" : grandProfit;

                    //获取万份收益
                    WXHouseNewRateBLL WXHnrBll = new WXHouseNewRateBLL(loggingSessionInfo);
                    DataSet dsNewRate = WXHnrBll.GetWXHouseNewRate(rp.CustomerID);
                    if (dsNewRate.Tables != null && dsNewRate.Tables.Count > 0 && dsNewRate.Tables[0] != null && dsNewRate.Tables[0].Rows.Count > 0)
                    {
                        //万份收益率-上一日期
                        if (rdData.TotalAssetsMoney == "0.00")
                            rdData.Bonuscuramt = "0.00";
                        else
                            rdData.Bonuscuramt = dsNewRate.Tables[0].Rows[0]["Bonusbefamt"].ToString();
                    }

                    //获取当前用户各类型状态下的总金额。
                    WXHouseBuyFundBLL fundBLL = new WXHouseBuyFundBLL(loggingSessionInfo);
                    string delegateMoney = fundBLL.GetTotalMoney(customerID, userID, 3);
                    string principal = fundBLL.GetTotalMoney(customerID, userID, 1);

                    //委托 返回的时间
                    // 1) 获取该用户所有委托中的单子，
                    List<WXHouseBuyFundEntity> fundList = fundBLL.Get(customerID, userID, 3);
                    if (fundList != null && fundList.Count > 0)
                    {
                        rdData.IsShowDeletateView = true;
                        //  2）如果购买时间在下午3点前，则收益将在下个工作日后，否则如果在3点之后，收益将在下下个工作日之后可查看
                        //  产生在委托日期查过2天
                        WXHouseBuildBLL buildBLL = new WXHouseBuildBLL(loggingSessionInfo);
                        foreach (var item in fundList)
                        {
                            DateTime turnTime = Convert.ToDateTime(item.PayDate);   //支付日期
                            DateTime thirdTime = Convert.ToDateTime(turnTime.ToString("yyyy-MM-dd") + " 15:00:00");  //是否大于3点
                            int peroid = turnTime < thirdTime ? 1 : 2;
                            DateTime? dt = buildBLL.GetWorkDays(turnTime, peroid);  //获取到账日
                            if (dt == null)
                            {
                                // Todo: Write log，工作日数据不足
                                throw new InvalidDataException("无效的到账日期");
                            }

                            TimeSpan ts = dt.Value - DateTime.Today;
                            if (ts.Days > 0)
                            {
                                int i = 0;
                                DateTime? day;
                                do
                                {
                                    ++i;

                                    //获取i工作日后的日期
                                    day = buildBLL.GetWorkDays(DateTime.Today, i);
                                    if (day == null)
                                    {
                                        // Todo: Write log，工作日数据不足
                                        throw new InvalidDataException("无效的到账日期");
                                    }
                                } while (day.Value.Date < dt.Value.Date);

                                rdData.RemainingWorkDay = i;
                            }
                            else
                            {
                                //就是今天
                                rdData.RemainingWorkDay = 0;
                            }
                        }
                    }
                    else
                    {
                        rdData.IsShowDeletateView = false;
                    }

                    rdData.DelegateMoney = delegateMoney;
                    rdData.Principal = principal;
                    rdData.IsBuyFund = true;
                    rd.Data = rdData;
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败" + ex.Message;
            }

            return rd.ToJSON();
        }
        #endregion

        #region 世联华安- 发送验证码
        ///// <summary>
        ///// 世联华安_发送短信验证码
        ///// </summary>
        ///// <returns></returns>
        //public string SendMobileCode(string pRequest)
        //{
        //    var rd = new APIResponse<SendCodeReqSpecialRD>();
        //    try
        //    {
        //        var rp = pRequest.DeserializeJSONTo<APIRequest<SendCodeRP>>();
        //        if (rp.Parameters != null)
        //        {
        //            rp.Parameters.Validate();
        //        }

        //        if (string.IsNullOrEmpty(rp.Parameters.Mobile))
        //        {
        //            throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = 311 };
        //        }

        //        #region  发送短信验证码
        //        string msg;
        //        var code = CharsFactory.Create6Char();
        //        var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
        //        var bll = new VipBLL(loggingSessionInfo);

        //        string sign = !string.IsNullOrEmpty(bll.GetSettingValue(loggingSessionInfo.ClientID)) ? bll.GetSettingValue(loggingSessionInfo.ClientID) : "华硕校园";
        //        if (!SMSHelper.Send(rp.CustomerID, rp.Parameters.Mobile, code, sign, out msg))
        //        {
        //            throw new APIException(msg) { ErrorCode = 310 };
        //        }
        //        #endregion   发送短信验证码

        //        #region 保存验证码
        //        int ValidPeriod;
        //        var tempstr = System.Configuration.ConfigurationManager.AppSettings["AuthCodeValidPeriod"];
        //        if (string.IsNullOrEmpty(tempstr))
        //        {
        //            ValidPeriod = 30;
        //        }
        //        else
        //        {
        //            if (!int.TryParse(tempstr, out ValidPeriod))
        //            {
        //                ValidPeriod = 30;
        //            }
        //        }
        //        var codebll = new RegisterValidationCodeBLL(loggingSessionInfo);
        //        codebll.DeleteByMobile(rp.Parameters.Mobile); //删除该手机号已有的验证码
        //        var codeEntity = new RegisterValidationCodeEntity()
        //        {
        //            Code = code,
        //            CodeID = Guid.NewGuid().ToString("N"),
        //            Mobile = rp.Parameters.Mobile,
        //            IsValidated = 0,
        //            Expires = DateTime.Now.AddMinutes(ValidPeriod)
        //        };
        //        codebll.Create(codeEntity); //以新的验证码为准
        //        #endregion

        //        var rdData = new SendCodeReqSpecialRD();
        //        rdData.AuthCode = code;
        //        rd.Data = rdData;
        //    }
        //    catch (Exception ex)
        //    {
        //        rd.ResultCode = 103;
        //        rd.Message = "数据库操作错误";

        //        Loggers.Exception(new ExceptionLogInfo()
        //        {
        //            ErrorMessage = string.Format("sendCode: {0}", ex.ToJSON())
        //        });
        //    }

        //    return rd.ToJSON();
        //}
        #endregion

        #region 基金购买
        /// <summary>
        /// 基金购买
        /// </summary>
        /// <param name="reqContent"></param>
        /// <returns></returns>
        public string BuyFund(string reqContent)
        {
            var rd = new APIResponse<PayEntityRD>();

            try
            {
                DateTime dt = DateTime.Now;
                var rp = reqContent.DeserializeJSONTo<APIRequest<PayEntityRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                string toPageUrl = rp.Parameters.ToPageUrl;
                Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "基金购买ToPageUrl:" + rp.Parameters.ToPageUrl });

                if (!string.IsNullOrEmpty(toPageUrl))
                {
                    toPageUrl = toPageUrl.Replace("&", "&amp;");
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                VipBLL vbll = new VipBLL(loggingSessionInfo);
                VipEntity vipEntity = vbll.GetVipDetailByVipID(rp.UserID);
                if (vipEntity == null)
                {
                    rd.ResultCode = 301;
                    rd.Message = "未找到该用户信息";
                    return rd.ToJSON();
                }

                //验证会员是否符合购买基金条件
                if (!CheckVipHasBuyFund(loggingSessionInfo, rp.UserID, rp.CustomerID, rp.Parameters.HouseDetailID))
                {
                    rd.ResultCode = 320;
                    rd.Message = "您购买次数达到上限或本楼盘已达购买上限，不可购买";
                    return rd.ToJSON();
                }

                WXHouseDetailBLL wxhdbll = new WXHouseDetailBLL(loggingSessionInfo);
                WXHouseDetailEntity wxhde = wxhdbll.GetDetailByID(rp.CustomerID, rp.Parameters.HouseDetailID);
                if (wxhde == null) throw new APIException("没有找到该楼盘信息。");
                decimal realPay = wxhde.RealPay.Value;

                string assignbuyer = GetClientAgreementNo(loggingSessionInfo, rp.UserID, rp.CustomerID);    //获取客户协议号
                string seqNO = GenerateSeqNO(dt);   //生成世联流水号

                #region  构造form表单
                Receive.ReceiveBuyMessage pEnity = new Receive.ReceiveBuyMessage();
                pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
                pEnity.Merchantdate = dt.ToString("yyyyMMdd");
                pEnity.Totalamt = realPay;
                pEnity.Assignbuyer = assignbuyer;
                pEnity.Assbuyername = vipEntity.VipName;
                pEnity.Assbuyermobile = vipEntity.Phone;
                pEnity.Fee = "0";
                //用于回调更新
                string strCommon = "CustomerID," + rp.CustomerID;
                strCommon += "|UserID," + rp.UserID;
                strCommon += "|HouseDetailID," + rp.Parameters.HouseDetailID;
                strCommon += "|SeqNO," + seqNO;
                strCommon += "|ToPageURL," + toPageUrl;
                strCommon += "|Merchantdate," + dt.ToString("yyyyMMdd");
                strCommon += "|HouseID," + wxhde.HouseID;
                pEnity.Commonreturn = strCommon;
                //回调url
                pEnity.RetURL = string.Format(HuaAnConfigurationAppSitting.RetCallBackPageUrl, "BuyCallBack");
                pEnity.PageURL = string.Format(HuaAnConfigurationAppSitting.CallBackPageUrl, "BuyCallBack");
                pEnity.Memo = "";
                //请求表单对象
                var fromList = new HuaAnFactory().FormRequestContent(dt, Utility.GetRequsetXml(pEnity), HuaAnConfigurationAppSitting.Buy, seqNO);

                var rdData = new PayEntityRD();
                rdData.FormData = fromList;
                //华安url
                rdData.Url = HuaAnConfigurationAppSitting.ReservationPurchaseUrl;
                #endregion

                rd.Data = rdData;

                //记录请求华安操作
                try
                {
                    WXHouseRequestLogEntity entity = new WXHouseRequestLogEntity
                    {
                        RequestId = Guid.NewGuid(),
                        AssignBuyer = assignbuyer,
                        SeqNO = seqNO,
                        RequestTargetId = wxhde.DetailID,
                        RequestType = HuaAnConfigurationAppSitting.Buy.ToString(),
                        RealPay = realPay,
                        CustomerID = rp.CustomerID
                    };
                    WXHouseRequestLogBLL logBll = new WXHouseRequestLogBLL(loggingSessionInfo);
                    logBll.Create(entity);
                }
                catch (Exception ex)
                {
                    Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败" + ex.Message;
            }
            return rd.ToJSON();
        }
        #endregion

        #region   基金赎回
        /// <summary>
        /// 基金赎回。
        /// </summary>
        /// <param name="rRequest"></param>
        /// <returns></returns>
        private string FundRansom(string pRequest)
        {
            var rd = new APIResponse<RansomEntityRD>();

            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<RansomEntityRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                string toPageUrl = rp.Parameters.ToPageUrl;
                if (!string.IsNullOrEmpty(toPageUrl))
                {
                    toPageUrl = toPageUrl.Replace("&", "&amp;");
                }

                //获取用户信息
                VipBLL vbll = new VipBLL(loggingSessionInfo);
                VipEntity vipEntity = vbll.GetVipDetailByVipID(rp.UserID);
                if (vipEntity == null)
                {
                    throw new APIException("未找到该用户信息！") { ErrorCode = 123 };
                }

                string userID = rp.UserID;
                string thirdOrderNo = rp.Parameters.ThirdOrderNo;
                //switch (userID)
                //{
                //    case "9502c38967574538a610b68923aa1a07":  //孔凡俊
                //        thirdOrderNo = m_logisticsinfo;
                //        break;
                //    default:
                //        thirdOrderNo = rp.Parameters.ThirdOrderNo;
                //        break;
                //}

                WXHouseBuildBLL bll = new WXHouseBuildBLL(loggingSessionInfo);
                DataSet ds = bll.GetMyHouseDetail(rp.CustomerID, userID, rp.Parameters.PrePaymentID, thirdOrderNo);
                MyHouseDetailViewModel myHouseDetailData = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    myHouseDetailData = DataTableToObject.ConvertToList<MyHouseDetailViewModel>(ds.Tables[0]).FirstOrDefault();
                }
                if (myHouseDetailData == null)
                {
                    throw new APIException("获取我的楼盘详细信息失败！") { ErrorCode = 123 };
                }

                //世联通讯流水号 ,订单号
                DateTime dt = DateTime.Now;
                string seqNO = GenerateSeqNO(dt);   //生成世联流水号
                string logisticsinfo = myHouseDetailData.ThirdOrderNo;
                string assignbuyer = GetClientAgreementNo(loggingSessionInfo, rp.UserID, rp.CustomerID);    //获取客户协议号

                #region 处理调用华安请求From表单对象
                //华安请求content对象
                ReceiveRansomMessage pEnity = new ReceiveRansomMessage();
                pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
                pEnity.Merchantdate = dt.ToString("yyyyMMdd");
                pEnity.Logisticsinfo = logisticsinfo;    // 交易号
                pEnity.Assignbuyer = assignbuyer;       //  客户协议号
                pEnity.Assbuyername = vipEntity.VipName;
                pEnity.Assbuyermobile = vipEntity.Phone;
                pEnity.Fee = "0";

                //用于回调更新
                string strCommon = "CustomerID," + rp.CustomerID;
                strCommon += "|UserID," + userID;
                strCommon += "|PrePaymentID," + rp.Parameters.PrePaymentID;
                strCommon += "|SeqNO," + seqNO;
                strCommon += "|Merchantdate," + dt.ToString("yyyyMMdd");
                strCommon += "|ToPageURL," + toPageUrl;
                pEnity.Commonreturn = strCommon;

                //回调url
                pEnity.PageURL = string.Format(HuaAnConfigurationAppSitting.CallBackPageUrl, "RansomCallBack");
                pEnity.Memo = "";
                pEnity.RetURL = string.Format(HuaAnConfigurationAppSitting.RetCallBackPageUrl, "RansomCallBack");

                //返回请求content xml
                string strContent = Utility.GetRequsetXml(pEnity);
                //华安请求Form表单对象
                HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConfigurationAppSitting.Redemption, seqNO);
                #endregion

                RansomEntityRD ransomRD = new RansomEntityRD()
                                            {
                                                FormData = rMessage,
                                                Url = HuaAnConfigurationAppSitting.ReservationRedeemUrl
                                            };
                rd.Data = ransomRD;

                //记录请求华安操作
                try
                {
                    WXHouseRequestLogEntity entity = new WXHouseRequestLogEntity
                    {
                        RequestId = Guid.NewGuid(),
                        AssignBuyer = assignbuyer,
                        SeqNO = seqNO,
                        RequestTargetId = Guid.Parse(rp.Parameters.PrePaymentID),
                        RequestType = HuaAnConfigurationAppSitting.Redemption.ToString(),
                        ThirdOrderNo = logisticsinfo,
                        RealPay = 0,
                        CustomerID = rp.CustomerID
                    };
                    WXHouseRequestLogBLL logBll = new WXHouseRequestLogBLL(loggingSessionInfo);
                    logBll.Create(entity);
                }
                catch (Exception ex)
                {
                    Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                }
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #region   支付（用号）
        /// <summary>
        ///基金过户（用号） 2001
        /// </summary>
        private string PayHouse(string pRequest)
        {
            var rd = new APIResponse<PayEntityRD>();

            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<RansomEntityRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();

                string toPageUrl = rp.Parameters.ToPageUrl;
                if (!string.IsNullOrEmpty(toPageUrl))
                {
                    toPageUrl = toPageUrl.Replace("&", "&amp;");
                }

                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //获取会员信息
                VipBLL vbll = new VipBLL(loggingSessionInfo);
                VipEntity vipEntity = vbll.GetVipDetailByVipID(rp.UserID);
                if (vipEntity == null)
                {
                    throw new APIException("没有找到该用户信息！") { ErrorCode = 122 };
                }

                WXHouseBuildBLL bll = new WXHouseBuildBLL(loggingSessionInfo);
                DataSet ds = bll.GetMyHouseDetail(rp.CustomerID, rp.UserID, rp.Parameters.PrePaymentID, rp.Parameters.ThirdOrderNo);
                MyHouseDetailViewModel detailModel = null;
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    detailModel = DataTableToObject.ConvertToList<MyHouseDetailViewModel>(ds.Tables[0]).FirstOrDefault();
                }
                if (detailModel == null)
                {
                    throw new APIException("获取我的楼盘详细信息失败！") { ErrorCode = 123 };
                }

                WXHouseDetailBLL detailBll = new WXHouseDetailBLL(loggingSessionInfo);

                string userID = rp.UserID;
                string thirdOrderNo = detailModel.ThirdOrderNo;
                //switch (userID)
                //{
                //    case "9502c38967574538a610b68923aa1a07":  //孔凡俊
                //        thirdOrderNo = m_logisticsinfo;
                //        break;
                //    default:
                //        thirdOrderNo = rp.Parameters.ThirdOrderNo;
                //        break;
                //}

                DateTime dt = DateTime.Now;
                string seqNO = GenerateSeqNO(dt);   //生成世联流水号
                string logisticsinfo = detailModel.ThirdOrderNo;
                string assignbuyer = GetClientAgreementNo(loggingSessionInfo, rp.UserID, rp.CustomerID);    //获取客户协议号

                //处理调用华安请求From表单对象
                ReceivePayMessage pEnity = new ReceivePayMessage();
                pEnity.MerchantID = HuaAnConfigurationAppSitting.MerchantID;
                pEnity.Merchantdate = dt.ToString("yyyyMMdd");
                pEnity.OrderNO = detailModel.OrderNO;  //世联订单号
                pEnity.Totalpay = detailModel.RealPay.ToString();
                pEnity.Assignbuyer = assignbuyer;  //客户协议号
                pEnity.Assbuyername = vipEntity.VipName;
                pEnity.Assbuyermobile = vipEntity.Phone;
                pEnity.Totaldiscount = "0";
                pEnity.Totaldeduction = "0";
                pEnity.Actualtotal = detailModel.RealPay.ToString();
                pEnity.Feetype = "0";
                pEnity.Fee = "0";
                pEnity.Logisticsinfo = detailModel.ThirdOrderNo;
                Goodsinfo info = new Goodsinfo
                {
                    id = detailModel.DetailID.ToString(),
                    Goodsname = detailModel.HouseName,
                    GoodsURL = "",
                    Goodspicture = "",
                    Goodsmodle = detailModel.HouseID.ToString(),
                    Goodsactualtotal = detailModel.RealPay.ToString(),
                    memo = ""
                };

                //用于回调更新
                string strCommon = "CustomerID," + rp.CustomerID;
                strCommon += "|UserID," + userID;
                strCommon += "|PrePaymentID," + rp.Parameters.PrePaymentID;
                strCommon += "|SeqNO," + seqNO;
                strCommon += "|Merchantdate," + dt.ToString("yyyyMMdd");
                strCommon += "|ToPageURL," + toPageUrl;
                pEnity.Commonreturn = strCommon;
                pEnity.ISDirectRedeem = "0";

                //回调url
                pEnity.PageURL = string.Format(HuaAnConfigurationAppSitting.CallBackPageUrl, "TransferCallBack");
                pEnity.Memo = "";
                pEnity.RetURL = string.Format(HuaAnConfigurationAppSitting.RetCallBackPageUrl, "TransferCallBack");

                string strContent = Utility.GetRequsetXml(pEnity);
                HanAnRequestMessage rMessage = new HuaAnFactory().FormRequestContent(dt, strContent, HuaAnConfigurationAppSitting.Pay, seqNO);
                PayEntityRD ransomRD = new PayEntityRD()
                {
                    FormData = rMessage,
                    Url = HuaAnConfigurationAppSitting.ReservationPayUrl
                };

                rd.Data = ransomRD;
                rd.ResultCode = 21;
                rd.Message = "成功";

                //记录请求华安操作
                try
                {
                    WXHouseRequestLogEntity entity = new WXHouseRequestLogEntity
                    {
                        RequestId = Guid.NewGuid(),
                        AssignBuyer = assignbuyer,
                        SeqNO = seqNO,
                        RequestTargetId = Guid.Parse(rp.Parameters.PrePaymentID),
                        RequestType = HuaAnConfigurationAppSitting.Pay.ToString(),
                        ThirdOrderNo = logisticsinfo,
                        RealPay = 0,
                        CustomerID = rp.CustomerID
                    };
                    WXHouseRequestLogBLL logBll = new WXHouseRequestLogBLL(loggingSessionInfo);
                    logBll.Create(entity);
                }
                catch (Exception ex)
                {
                    Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                }

            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rd.Message = "业务处理失败";
            }

            return rd.ToJSON();
        }
        #endregion

        #endregion

        #region  私有方法
        /// <summary>
        /// 获取客户协议号。
        /// </summary>
        /// <returns></returns>
        private string GetClientAgreementNo(LoggingSessionInfo pLoggingSessionInfo, string userID, string customerID)
        {
            try
            {
                WXHouseAssignbuyerBLL BLL = new WXHouseAssignbuyerBLL(pLoggingSessionInfo);
                WXHouseAssignbuyerEntity entity = BLL.GetWXHouseAssignbuyer(customerID, userID);
                if (entity != null)
                {
                    return entity.Assignbuyer;
                }
                else
                {
                    entity = new WXHouseAssignbuyerEntity()
                   {
                       AssignbuyerID = Guid.NewGuid(),
                       UserID = userID,
                       Assignbuyer = GenerateClientAgreementNo(pLoggingSessionInfo, customerID),
                       CustomerID = customerID,
                       CreateBy = userID,
                       CreateTime = DateTime.Now,
                       LastUpdateBy = userID,
                       LastUpdateTime = DateTime.Now,
                       IsDelete = 0
                   };

                    BLL.Create(entity);

                    return entity.Assignbuyer;
                }
            }
            catch (Exception ex)
            {

            }

            return string.Empty;
        }

        /// <summary>
        /// 生成客户协议号 
        /// </summary>
        /// <returns></returns>
        private string GenerateClientAgreementNo(LoggingSessionInfo pLoggingSessionInfo, string customerID)
        {
            WXHouseAssignbuyerBLL BLL = new WXHouseAssignbuyerBLL(pLoggingSessionInfo);
            string maxAssignbuyer = BLL.GetWXHouseMaxAssignbuyer(customerID);

            Int64 maxValue = 0;
            Int64.TryParse(maxAssignbuyer, out maxValue);
            maxValue = maxValue + 1;
            string str = maxValue.ToString().PadLeft(10, '0');

            return str;
        }

        #region GenerateSeqNO 生成世联通讯流水号:年月日时分秒毫秒+6位随机数。
        /// <summary>
        /// 生成世联通讯流水号:年月日时分秒毫秒+6位随机数。
        /// </summary>
        /// <returns></returns>
        private string GenerateSeqNO(DateTime dt)
        {
            string date = dt.ToString("yyyyMMddHHmmssfff");

            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rand = new Random();
            for (int i = 10; i > 1; i--)
            {
                int index = rand.Next(i);
                int tmp = array[index];
                array[index] = array[i - 1];
                array[i - 1] = tmp;
            }
            int result = 0;
            for (int i = 0; i < 6; i++)
                result = result * 10 + array[i];

            return string.Concat(date, result);
        }
        #endregion

        #region 验证会员是否符合购买基金条件
        /// <summary>
        /// 验证会员是否符合购买基金条件
        /// </summary>
        /// <param name="pLoggingSessionInfo"></param>
        /// <param name="pVipID"></param>
        /// <param name="pCustomerID"></param>
        /// <param name="pHouseDetailID"></param>
        /// <returns></returns>
        public bool CheckVipHasBuyFund(LoggingSessionInfo pLoggingSessionInfo, string pVipID, string pCustomerID, string pHouseDetailID)
        {
            bool f = false;
            //楼盘明细表
            //每人限购数量(PurchCaseNum) 可销售数量(TotalHoseNum)
            WXHouseDetailBLL wxhdb = new WXHouseDetailBLL(pLoggingSessionInfo);
            WXHouseDetailEntity wxhde = wxhdb.GetByID(pHouseDetailID);
            //楼盘主表
            //销售数量(SaleHoseNum) 已购买数量
            WXHouseBuildBLL wxhbb = new WXHouseBuildBLL(pLoggingSessionInfo);
            WXHouseBuildEntity wxhbe = wxhbb.GetByID(wxhde.HouseID);
            //会员楼盘已购买次数
            int vipBuyNum = wxhbb.GetVipBuyHouseNumber(pCustomerID, pVipID, pHouseDetailID);
            //购买条件：TotalHoseNum>SaleHoseNum && vipBuyNum<=PurchCaseNum
            if (wxhde.TotalHoseNum > wxhbe.SaleHoseNum && vipBuyNum <= wxhde.PurchCaseNum)
                f = true;
            return f;
        }
        #endregion

        #region 赎回或过户条件
        /// <summary>
        /// 赎回或过户条件
        /// </summary>
        /// <param name="pLoggingSessionInfo"></param>
        /// <param name="pHouseDetailID"></param>
        /// <returns></returns>
        public bool CheckIsRedeemTransfer(LoggingSessionInfo pLoggingSessionInfo, string pCustomerID, string pHouseDetailID)
        {
            //房子在“未售”状态中，只能申购，一旦申购就不能在“开售”前，进行“赎回”操作
            //房产在“在售”状态中，可以“赎回”和”支付“
            bool f = false;
            //楼盘主表
            WXHouseBuildBLL wxhbb = new WXHouseBuildBLL(pLoggingSessionInfo);
            DataSet ds = wxhbb.GetHouseInfo(pCustomerID, pHouseDetailID);
            if (CheckDsData(ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["HoseState"].ToString() == ((int)HouseStateEnum.Sold).ToString())
                        f = true;
                }
            }
            return f;
        }
        #endregion

        #region 验证DataSet
        /// <summary>
        /// 验证DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool CheckDsData(DataSet ds)
        {
            bool check = false;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                check = true;
            return check;
        }
        #endregion

        #endregion
    }

    #region 定义接口的请求参数及响应结果的数据结构

    #region 验证用户注册
    /// <summary>
    /// 验证用户注册
    /// </summary>
    public class VipEntityRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员
        /// <summary>
        /// 参数验证
        /// </summary>
        public void Validate()
        {

        }
        #endregion
    }

    public class VipEntityRD : IAPIResponseData
    {
        public string IsRegister { set; get; }
    }
    #endregion

    #region 我的收益
    /// <summary>
    /// 我的收益
    /// </summary>
    public class HouseProfitRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员
        public void Validate()
        {

        }
        #endregion
    }

    /// <summary>
    /// 我的收益模块
    /// </summary>
    public class HouseProfitListRD : IAPIResponseData
    {
        /// <summary>
        /// 昨日收益
        /// </summary>
        public string YesterdayProfit { set; get; }
        /// <summary>
        /// 总资产
        /// </summary>
        public string TotalAssetsMoney { set; get; }
        /// <summary>
        /// 累计收益
        /// </summary>
        public string GrandProfit { set; get; }
        /// <summary>
        /// 万份收益
        /// </summary>
        public string Bonuscuramt { set; get; }
        /// <summary>
        /// 本金
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 委托金额
        /// </summary>
        public string DelegateMoney { get; set; }

        /// <summary>
        /// 距离委托返回最终结果的时间
        /// </summary>
        public int RemainingWorkDay { get; set; }
        /// <summary>
        /// 是否买房
        /// </summary>
        public bool IsBuyFund { set; get; }

        /// <summary>
        /// 是否显示委托视图
        /// </summary>
        public bool IsShowDeletateView { get; set; }
    }

    /// <summary>
    /// 工作日
    /// </summary>
    public class WorkkerDay
    {
        public string WorkDay { get; set; }
    }

    public class WXHouseNewRate
    {
        public string Bonusbefdate { set; get; }
        public string Bonusbefamt { set; get; }
        public string Bonusbefratio { set; get; }
    }
    #endregion

    #region 立即预定
    /// <summary>
    /// 立即预定模块
    /// </summary>
    public class PayEntityRP : IAPIRequestParameter
    {
        /// <summary>
        /// 楼盘详细ID
        /// </summary>
        public string HouseDetailID { set; get; }

        /// <summary>
        /// 购买回调前端返回页面Url
        /// </summary>
        public string ToPageUrl { get; set; }

        #region IAPIRequestParameter 成员
        /// <summary>
        /// 参数验证
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(HouseDetailID))
            {
                throw new APIException("无效的楼盘详细ID【HouseDetailID】") { ErrorCode = 121 };
            }

            if (string.IsNullOrEmpty(ToPageUrl))
            {
                throw new APIException("无效的回调页面Url【ToPageUrl】") { ErrorCode = 121 };
            }
        }
        #endregion
    }
    public class PayEntityRD : IAPIResponseData
    {
        public HanAnRequestMessage FormData { set; get; }
        public string Url { set; get; }
    }
    #endregion


    #region 赎回
    /// <summary>
    /// 赎回和支付接口 适用。
    /// </summary>
    public class RansomEntityRP : IAPIRequestParameter
    {
        /// <summary>
        /// 楼盘详细ID
        /// </summary>
        public string HouseDetailID { set; get; }

        /// <summary>
        /// 预付款记录
        /// </summary>
        public string PrePaymentID { set; get; }

        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string ThirdOrderNo { get; set; }

        /// <summary>
        /// 回调前端返回页面Url
        /// </summary>
        public string ToPageUrl { get; set; }

        #region IAPIRequestParameter 成员
        /// <summary>
        /// 参数验证
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(HouseDetailID))
            {
                throw new APIException("无效的楼盘详细ID【HouseDetailID】") { ErrorCode = 121 };
            }
            if (string.IsNullOrEmpty(PrePaymentID))
            {
                throw new APIException("请提供预付款ID【PrePaymentID】") { ErrorCode = 122 };
            }
            if (string.IsNullOrEmpty(ThirdOrderNo))
            {
                throw new APIException("请提供交易号【ThirdOrderNo】") { ErrorCode = 122 };
            }
            if (string.IsNullOrEmpty(ToPageUrl))
            {
                throw new APIException("请提供回调成功后跳转Url【ToPageUrl】") { ErrorCode = 123 };
            }
        }
        #endregion
    }
    public class RansomEntityRD : IAPIResponseData
    {
        public HanAnRequestMessage FormData { set; get; }
        public string Url { set; get; }
    }
    #endregion

    #region  我的房产详细
    public class HouseDetailRP : IAPIRequestParameter
    {
        /// <summary>
        /// 楼盘详细ID
        /// </summary>
        public string DetailID { get; set; }

        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string ThirdOrderNo { get; set; }
        /// <summary>
        ///预付款标识
        /// </summary>
        public string PrePaymentID;

        public void Validate()
        {
            if (string.IsNullOrEmpty(DetailID))
            {
                throw new APIException("请提供楼盘详细ID【DetailID】");
            }

            if (string.IsNullOrEmpty(PrePaymentID))
            {
                throw new APIException("请提供预付款ID【PrePaymentID】");
            }

            if (string.IsNullOrEmpty(ThirdOrderNo))
            {
                throw new APIException("请提供第三方订单号【ThirdOrderNo】");
            }
        }
    }

    public class HouseDetailRD : IAPIResponseData
    {
        public MyHouseDetailViewModel HouseDetail { get; set; }
    }
    #endregion

    #region  房产列表
    /// <summary>
    /// 房产列表展示。
    /// </summary>
    public class HouseDataRP : HousePage, IAPIRequestParameter
    {
        //public string HouseID;

        public void Validate()
        {
        }
    }

    public class HouseDataRD : IAPIResponseData
    {
        public List<HouseViewModel> HouseList { get; set; }
    }

    #endregion

    #region  我的房产列表
    /// <summary>
    /// 我的房产列表展示。
    /// </summary>
    public class MyHouseDataRP : HousePage, IAPIRequestParameter
    {
        //public string HouseID;
        public void Validate()
        {

        }
    }

    public class MyHouseDataRD : IAPIResponseData
    {
        public List<MyHouseViewModel> HouseList { get; set; }
    }
    #endregion

    #region  发送验证码
    public class SendCodeRP : IAPIRequestParameter
    {
        /// <summary>
        /// //手机号码
        /// </summary>
        public string Mobile { get; set; }

        public void Validate()
        {

        }
    }
    public class SendCodeReqSpecialRD : IAPIResponseData
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string AuthCode { get; set; }
    }
    #endregion

    #region    注册
    /// <summary>
    /// 注册RP
    /// </summary>
    public class RegisterRP : IAPIRequestParameter
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string AuthCode { get; set; }

        public void Validate()
        {

        }
    }

    /// <summary>
    /// 注册RD
    /// </summary>
    public class RegisterRD : IAPIResponseData
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }


    #endregion

    #region 用号
    /// <summary>
    /// 楼盘明细视图模型定义。
    /// </summary>
    public class HouseDetailViewModel : WXHouseBuildEntity
    {
        /// <summary>
        /// 楼盘详细id
        /// </summary>
        public Guid DetailID { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal RealPay { get; set; }

        /// <summary>
        /// 抵扣金额
        /// </summary>
        public decimal DeductionPay { get; set; }
    }

    #endregion

    /// <summary>
    /// 分页
    /// </summary>
    public class HousePage
    {
        public int PageIndex;
        public int PageSize;
    }

    #endregion
}
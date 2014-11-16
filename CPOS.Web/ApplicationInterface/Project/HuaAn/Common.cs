using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    public static class Common
    {
        /// <summary>
        /// 购买回调。
        /// </summary>
        /// <returns></returns>
        public static void BuyCallBack(string content)
        {
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "BuyCallBack: " + DateTime.Now});

            //解析strXML为SendBuyMessage响应对象
            SendBuyMessage model = GetBuyModelResponse(content);
            Hashtable htComm = GetCommonreturn(model.Commonreturn);

            string customerId = htComm["CustomerID"].ToString();
            string userId = htComm["UserID"].ToString();
            string houseDetailId = htComm["HouseDetailID"].ToString();
            string seqNo = htComm["SeqNO"].ToString();
            string merchantdate = htComm["Merchantdate"].ToString();
            string houseId = htComm["HouseID"].ToString();
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "SeqNo: " + seqNo});

            string orgtotalamt = model.Orgtotalamt;   //实际交易额
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);

            //1判断会员楼盘明细映射是否存在记录
            var bll = new WXHouseVipMappingBLL(loggingSessionInfo);
            DataSet ds = bll.VerifWXHouseVipMapping(userId, houseDetailId, customerId);
            Guid mappingId = Guid.NewGuid();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //1.1存在
                mappingId = new Guid(ds.Tables[0].Rows[0]["MappingID"].ToString());
            }
            else
            {
                //1.2不存在
                //插入会员楼盘明细映射表
                var houserVipMappingEntity = new WXHouseVipMappingEntity
                {
                    MappingID = mappingId,
                    DetailID = new Guid(houseDetailId),
                    CustomerID = customerId,
                    VIPID = userId,
                    ReserveNo = seqNo
                };
                houserVipMappingEntity.CustomerID = customerId;
                bll.Create(houserVipMappingEntity);
            }

            DateTime dt = DateTime.Now;
            string orderNo = GenerateOrderNo(dt);

            #region  1.4插入订单信息
            var orderBll = new WXHouseOrderBLL(loggingSessionInfo);
            var orderEntity = new WXHouseOrderEntity();
            Guid prePaymentId = Guid.NewGuid();  //订单表ID
            orderEntity.PrePaymentID = prePaymentId;
            orderEntity.MappingID = mappingId;
            orderEntity.OrderNO = orderNo;
            orderEntity.OrderDate = Convert.ToDateTime(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            orderEntity.RealPay = Convert.ToDecimal(orgtotalamt);   //实付金额
            var assignbuyerBll = new WXHouseAssignbuyerBLL(loggingSessionInfo);
            WXHouseAssignbuyerEntity buyerEntity = assignbuyerBll.GetWXHouseAssignbuyer(customerId, userId);
            if (buyerEntity != null)
            {
                orderEntity.AssignbuyerID = buyerEntity.AssignbuyerID;
            }
            orderEntity.ThirdOrderNo = model.Logisticsinfo;
            orderEntity.Assbuyeridtp = model.Assbuyeridtp;
            orderEntity.Assbuyername = model.Assbuyername;
            orderEntity.Assbuyermobile = model.Assbuyermobile;

            orderEntity.CustomerID = customerId;
            orderBll.Create(orderEntity);
            #endregion


            #region  购买基金
            var fundBll = new WXHouseBuyFundBLL(loggingSessionInfo);
            var fundEntity = new WXHouseBuyFundEntity
            {
                BuyFundID = Guid.NewGuid(),
                PrePaymentID = prePaymentId,
                Fundtype = 2101,
                PayDate = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                Merchantdate = merchantdate,
                SeqNO = seqNo,
                Retmsg = model.Retmsg,
                CustomerID = customerId
            };

            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "Retcode" + model.Retcode});

            string retMsg;
            int fundState;
            //判断华安赎回返回Retcode状态
            if (model.Retcode == "0000")
            {
                //2 更改楼盘表销售数量 HouseDetailID
                var buildBll = new WXHouseBuildBLL(loggingSessionInfo);
                WXHouseBuildEntity buildEntity = buildBll.GetByID(houseId);
                if (buildEntity != null)
                {
                    buildEntity.SaleHoseNum += 1;
                    buildBll.Update(buildEntity);
                }

                retMsg = "支付成功";
                fundState = (int)FundStateEnum.Success;
            }
            else if (model.Retcode == "0999")
            {
                retMsg = "委托已受理";
                fundState = (int)FundStateEnum.Order;
            }
            else
            {
                retMsg = "支付失败";
                fundState = (int)FundStateEnum.Error;
            }

            fundEntity.Retcode = model.Retcode;
            fundEntity.FundState = fundState;
            fundBll.Create(fundEntity);
            #endregion

            //页面跳转
            string toPageUrl = htComm["ToPageURL"] + "&type=1&retStatus=" + fundState + "&retMsg=" + retMsg + "&Retcode=" + model.Retcode;
            RedirectUrl(toPageUrl);
        }

        /// <summary>
        /// 基金赎回。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static void RansomCallBack(string content)
        {
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "赎回  content" + content});

            SendRansomMessage model = GetRansomModelResponse(content);
            Hashtable htComm = GetCommonreturn(model.Commonreturn);

            string customerId = htComm["CustomerID"].ToString();
            string userId = htComm["UserID"].ToString();
            string prePaymentId = htComm["PrePaymentID"].ToString();
            string seqNo = htComm["SeqNO"].ToString();
            string merchantdate = htComm["Merchantdate"].ToString();
            string resultPageUrl = htComm["ToPageURL"].ToString();

            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "赎回  ToPageURL" + resultPageUrl});

            string retMsg;
            int fundState;

            //判断华安赎回返回Retcode状态
            if (model.Retcode == "0000")
            {
                fundState = (int)FundStateEnum.Order;
                retMsg = "委托已受理";
            }
            else
            {
                fundState = (int)FundStateEnum.Error;
                retMsg = "委托失败";
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);

            #region  添加赎回记录
            try
            {
                var redeemBll = new WXHouseReservationRedeemBLL(loggingSessionInfo);
                var entity = redeemBll.GetByPrePaymentID(customerId, prePaymentId);
                if (entity != null)
                {
                    entity.FundState = fundState;
                    redeemBll.Update(entity);
                }
                else
                {
                    entity = new WXHouseReservationRedeemEntity
                    {
                        RedeemID = Guid.NewGuid(),
                        PrePaymentID = new Guid(prePaymentId),
                        Fundtype = (int) FundtypeeEnum.ReservationRedeem,
                        FundState = fundState,
                        Merchantdate = merchantdate,
                        SeqNO = seqNo,
                        Retmsg = model.Retmsg,
                        Retcode = model.Retcode,
                        CustomerID = customerId
                    };

                    redeemBll.Create(entity);
                }
            }
            catch (Exception ex)
            {
                Loggers.DEFAULT.Exception(new ExceptionLogInfo(ex));
                // throw new APIException(string.Format("操作赎回记录失败,原因：{0}", ex.Message)) { ErrorCode = 201 };
            }
            #endregion

            string toPageUrl = resultPageUrl + "&type=2&retStatus=" + fundState + "&retMsg=" + retMsg + "&Retcode=" + model.Retcode;
            RedirectUrl(toPageUrl);
        }

        /// <summary>
        /// 支付（过户）。
        /// </summary>
        /// <returns></returns>
        public static void TransferCallBack(string content)
        {
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "支付（过户）"});
            // 解析strXML为SendBuyMessage响应对象
            SendPayMessage model = GetPayModelResponse(content);
            Hashtable htComm = GetCommonreturn(model.Commonreturn);

            string customerId = htComm["CustomerID"].ToString();
            string userId = htComm["UserID"].ToString();
            string prePaymentId = htComm["PrePaymentID"].ToString();
            string seqNo = htComm["SeqNO"].ToString();
            string merchantdate = htComm["Merchantdate"].ToString();
            string resultPageUrl = htComm["ToPageURL"].ToString();

            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "ToPageUrl" + resultPageUrl});
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);

            int fundState;  //交易状态
            string retMsg;

            //判断华安赎回返回Retcode状态
            if (model.Retcode == "0000")
            {
                fundState = (int)PayHouseStateEnum.Order;
                retMsg = "委托已受理";
            }
            else
            {
                fundState = (int)PayHouseStateEnum.Unsold;
                retMsg = "委托失败";
            }

            #region 更新订单表中的证件号码
            var orderBll = new WXHouseOrderBLL(loggingSessionInfo);
            WXHouseOrderEntity orderEntity = orderBll.GetByID(prePaymentId);
            if (orderEntity != null)
            {
                orderEntity.Assbuyeridno = model.Assbuyeridno;
            }
            orderBll.Update(orderEntity);
            #endregion

            #region   增加支付记录
            var redeemBll = new WXHouseReservationPayBLL(loggingSessionInfo);
            WXHouseReservationPayEntity entity = redeemBll.GetByPrePaymentID(customerId, prePaymentId);
            if (entity != null)
            {
                entity.FundState = fundState;
                entity.HaorderNO = model.HaorderNO;
                redeemBll.Update(entity);
            }
            else
            {
                entity = new WXHouseReservationPayEntity
                {
                    ReservationPayID = Guid.NewGuid(),
                    PrePaymentID = new Guid(prePaymentId),
                    SeqNO = seqNo,
                    Fundtype = (int) FundtypeeEnum.ReservationPay,
                    FundState = fundState,
                    Merchantdate = merchantdate,
                    HaorderNO = model.HaorderNO
                };
                int haDt;
                int.TryParse(model.Hatradedt, out haDt);

                entity.Hatradedt = haDt;
                entity.Retmsg = model.Retmsg;
                entity.Retcode = model.Retcode;
                entity.CustomerID = customerId;

                redeemBll.Create(entity);
            }
            #endregion

            string toPageUrl = resultPageUrl + "&type=3&retStatus=" + fundState + "&retMsg=" + retMsg + "&Retcode=" + model.Retcode;
            RedirectUrl(toPageUrl);
        }


        #region 工具方法

        #region 生成订单号:年月日时分秒毫秒+6位随机数
        /// <summary>
        /// 生成订单号:年月日时分秒毫秒+6位随机数。
        /// </summary>
        /// <returns></returns>
        private static string GenerateOrderNo(DateTime dt)
        {
            string date = dt.ToString("yyyyMMddHHmmssfff");

            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var rand = new Random();
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

        /// <summary>
        /// 解析Xml返回对象（购买回调）
        /// </summary>
        /// <returns></returns>
        public static SendBuyMessage GetBuyModelResponse(string pXmlString)
        {
            var send = new SendBuyMessage();
            //Xml解析
            var doc = new XmlDocument();
            doc.LoadXml(pXmlString);
            XmlNodeList xxList = doc.GetElementsByTagName("order"); //取得节点名为order的集合
            foreach (XmlNode xxNode in xxList)  //xxNode 是每一个<CL>...</CL>体
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得CL下的子节点集合
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "MerchantID":
                            send.MerchantID = node.InnerText;
                            break;
                        case "Merchantdate":
                            send.Merchantdate = node.InnerText;
                            break;
                        case "Orgtotalamt":
                            send.Orgtotalamt = node.InnerText;
                            break;
                        case "Assignbuyer":
                            send.Assignbuyer = node.InnerText;
                            break;
                        case "Assbuyername":
                            send.Assbuyername = node.InnerText;
                            break;
                        case "Assbuyermobile":
                            send.Assbuyermobile = node.InnerText;
                            break;
                        case "Assbuyeridtp":
                            send.Assbuyeridtp = node.InnerText;
                            break;
                        case "Logisticsinfo":
                            send.Logisticsinfo = node.InnerText;
                            break;
                        case "Fee":
                            send.Fee = node.InnerText;
                            break;
                        case "Fundtype":
                            send.Fundtype = node.InnerText;
                            break;
                        case "Retcode":
                            send.Retcode = node.InnerText;
                            break;
                        case "Retmsg":
                            send.Retmsg = node.InnerText;
                            break;
                        case "Commonreturn":
                            send.Commonreturn = node.InnerText;
                            break;
                    }
                }
            }
            return send;
        }

        /// <summary>
        /// 解析Xml返回对象（赎回回调）
        /// </summary>
        /// <returns></returns>
        public static SendRansomMessage GetRansomModelResponse(string pXmlString)
        {
            var send = new SendRansomMessage();
            //Xml解析
            var doc = new XmlDocument();
            doc.LoadXml(pXmlString);
            //取得节点名为order的集合
            XmlNodeList xxList = doc.GetElementsByTagName("order");
            //xxNode是每一个<CL>...</CL>体
            foreach (XmlNode xxNode in xxList)
            {
                //取得CL下的子节点集合
                XmlNodeList childList = xxNode.ChildNodes;
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "MerchantID":
                            send.MerchantID = node.InnerText;
                            break;
                        case "Merchantdate":
                            send.Merchantdate = node.InnerText;
                            break;
                        case "Orgtotalamt":
                            send.Orgtotalamt = node.InnerText;
                            break;
                        case "Assignbuyer":
                            send.Assignbuyer = node.InnerText;
                            break;
                        case "Assbuyername":
                            send.Assbuyername = node.InnerText;
                            break;
                        case "Assbuyermobile":
                            send.Assbuyermobile = node.InnerText;
                            break;
                        case "Assbuyeridtp":
                            send.Assbuyeridtp = node.InnerText;
                            break;
                        case "Logisticsinfo":
                            send.Logisticsinfo = node.InnerText;
                            break;
                        case "Fee":
                            send.Fee = node.InnerText;
                            break;
                        case "Fundtype":
                            send.Fundtype = node.InnerText;
                            break;
                        case "Retcode":
                            send.Retcode = node.InnerText;
                            break;
                        case "Retmsg":
                            send.Retmsg = node.InnerText;
                            break;
                        case "Commonreturn":
                            send.Commonreturn = node.InnerText;
                            break;
                    }
                }
            }
            return send;
        }

        /// <summary>
        /// 解析Xml返回对象（支付回调）
        /// </summary>
        /// <returns></returns>
        public static SendPayMessage GetPayModelResponse(string pXmlString)
        {
            var send = new SendPayMessage();
            //Xml解析
            var doc = new XmlDocument();
            doc.LoadXml(pXmlString);
            //取得节点名为order的集合
            XmlNodeList xxList = doc.GetElementsByTagName("order");
            //xxNode是每一个<CL>...</CL>体
            foreach (XmlNode xxNode in xxList)
            {
                //取得CL下的子节点集合
                XmlNodeList childList = xxNode.ChildNodes;
                foreach (XmlNode node in childList)
                {
                    String temp = node.Name;
                    switch (temp)
                    {
                        case "MerchantID":
                            send.MerchantID = node.InnerText;
                            break;
                        case "Merchantdate":
                            send.Merchantdate = node.InnerText;
                            break;

                        case "orderNO":
                            send.orderNO = node.InnerText;
                            break;

                        case "Assignbuyer":
                            send.Assignbuyer = node.InnerText;
                            break;

                        case "Assbuyername":
                            send.Assbuyername = node.InnerText;
                            break;

                        case "Assbuyermobile":
                            send.Assbuyermobile = node.InnerText;
                            break;

                        case "Assbuyeridtp":
                            send.Assbuyeridtp = node.InnerText;
                            break;

                        case "Assbuyeridno":
                            send.Assbuyeridno = node.InnerText;
                            break;

                        case "Logisticsinfo":
                            send.Logisticsinfo = node.InnerText;
                            break;
                        case "Totalpay":
                            send.Totalpay = node.InnerText;
                            break;
                        case "Totaldiscount":
                            send.Totaldiscount = node.InnerText;
                            break;
                        case "Totaldeduction":
                            send.Totaldeduction = node.InnerText;
                            break;
                        case "Actualtotal":
                            send.Actualtotal = node.InnerText;
                            break;
                        case "Feetype":
                            send.Feetype = node.InnerText;
                            break;
                        case "Fee":
                            send.Fee = node.InnerText;
                            break;
                        case "Retcode":
                            send.Retcode = node.InnerText;
                            break;
                        case "Retmsg":
                            send.Retmsg = node.InnerText;
                            break;
                        case "Hatradedt":
                            send.Hatradedt = node.InnerText;
                            break;
                        case "HaorderNO":
                            send.HaorderNO = node.InnerText;
                            break;
                        case "Commonreturn":
                            send.Commonreturn = node.InnerText;
                            break;
                    }
                }
            }
            return send;
        }

        /// <summary>
        /// 处理回调公共参数返回键值对
        /// </summary>
        /// <param name="pCommonreturn"></param>
        /// <returns></returns>
        public static Hashtable GetCommonreturn(string pCommonreturn)
        {
            //创建一个Hashtable实例
            var ht = new Hashtable();
            string[] arr = pCommonreturn.Split('|');
            foreach (string item in arr)
            {
                //|UserID:123|
                string key = item.Split(',')[0];
                string value = item.Split(',')[1];
                //添加key value键值对
                ht.Add(key, value);
            }
            return ht;
        }

        /// <summary>
        /// yyyyMMdd字符转日期
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public static DateTime ConvertDate(string pDate)
        {
            return Convert.ToDateTime(pDate.Substring(0, 4) + "-" + pDate.Substring(4, 2) + "-" + pDate.Substring(6, 2));
        }

        /// <summary>
        /// 指定Url跳转页面
        /// </summary>
        /// <param name="pToPageUrl"></param>
        private static void RedirectUrl(string pToPageUrl)
        {
            HttpContext context = HttpContext.Current;
            context.Response.ContentType = "text/html";
            context.Response.Redirect(pToPageUrl);
            context.Response.End();
        }
        #endregion

        #region 回调验证
        /// <summary>
        /// 验证回调是否执行过
        /// 记录已存在true 记录不存在false
        /// </summary>
        /// <param name="action"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool Verify(string action, string content)
        {
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "回调验证开始：" + action});
            bool pass = false;
            switch (action)
            {
                case "BuyCallBack":  //基金购买。
                    pass = VerifyExistBuyCallBack(content);
                    break;
                case "RansomCallBack":  //赎回。
                    pass = VerifyExistRansomCallBack(content);
                    break;
                case "TransferCallBack":  //用号
                    pass = VerifyExistTransferCallBack(content);
                    break;
            }
            Loggers.DEFAULT.Debug(new DebugLogInfo {Message = "回调验证结果：" + pass});
            return pass;
        }
        private static bool VerifyExistBuyCallBack(string content)
        {
            bool exist = false;
            //解析strXML为SendBuyMessage响应对象
            SendBuyMessage model = GetBuyModelResponse(content);
            Hashtable htComm = GetCommonreturn(model.Commonreturn);

            string customerId = htComm["CustomerID"].ToString();
            string userId = htComm["UserID"].ToString();
            string seqNo = htComm["SeqNO"].ToString();

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);
            var fundBll = new WXHouseBuyFundBLL(loggingSessionInfo);
            var fundList = fundBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =customerId },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "IsDelete", Value = "0"},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "SeqNO", Value =seqNo }
                    }, null);
            if (fundList != null && fundList.Length > 0)
                exist = true;
            return exist;
        }
        private static bool VerifyExistRansomCallBack(string content)
        {
            bool exist = false;

            SendRansomMessage model = GetRansomModelResponse(content);
            Hashtable htComm = GetCommonreturn(model.Commonreturn);

            string customerId = htComm["CustomerID"].ToString();
            string userId = htComm["UserID"].ToString();
            string seqNo = htComm["SeqNO"].ToString();

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);
            var redeemBll = new WXHouseReservationRedeemBLL(loggingSessionInfo);
            var reddemList = redeemBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =customerId },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "IsDelete", Value = "0"},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "SeqNO", Value =seqNo }
                    }, null);
            if (reddemList != null && reddemList.Length > 0)
                exist = true;
            return exist;
        }
        private static bool VerifyExistTransferCallBack(string content)
        {
            bool exist = false;
            // 解析strXML为SendBuyMessage响应对象
            SendPayMessage model = GetPayModelResponse(content);
            Hashtable htComm = GetCommonreturn(model.Commonreturn);

            string customerId = htComm["CustomerID"].ToString();
            string userId = htComm["UserID"].ToString();
            string seqNo = htComm["SeqNO"].ToString();

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, userId);
            var resPayBll = new WXHouseReservationPayBLL(loggingSessionInfo);
            var resPayList = resPayBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =customerId },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "IsDelete", Value = "0"},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "SeqNO", Value =seqNo }
                    }, null);
            if (resPayList != null && resPayList.Length > 0)
                exist = true;
            return exist;
        }
        #endregion
    }
}
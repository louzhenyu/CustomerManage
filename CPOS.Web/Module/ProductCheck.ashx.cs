using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.Module
{
    /// <summary>
    /// QRCodeScan 的摘要说明
    /// </summary>
    public class ProductCheck : IHttpHandler
    {

        string customerId = "";
        string reqContent = "";
        string requestIP = "";

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
            {
                requestIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
            }
            else // not using proxy or can't get the Client IP
            {
                requestIP = context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
            }

            reqContent = context.Request["ReqContent"];
            string action = context.Request["action"].ToString().Trim();
            string content = string.Empty;

            JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(context, context.Request, action);

            switch (action)
            {
                case "checkProduct":      //产品验真伪得积分
                    content = CheckProduct();
                    break;
                case "feedback":      //产品验真伪得积分
                    content = Feedback();
                    break;
                default:
                    throw new Exception("未定义的接口:" + action);
            }

            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(content);
            context.Response.End();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public string CheckProduct()
        {
            string content = string.Empty;
            var respData = new CheckProductRespData();

            #region 解析请求字符串
            var reqObj = reqContent.DeserializeJSONTo<CheckProductReqData>();

            if (reqObj.special == null)
            {
                respData.code = "101";
                respData.description = "没有特殊参数";
                return respData.ToJSON().ToString();
            }
            if (reqObj.special.traceCode == null || reqObj.special.traceCode.Equals(""))
            {
                respData.code = "102";
                respData.description = "识别码不能为空";
                return respData.ToJSON().ToString();
            }
            #endregion

            #region //判断客户ID是否传递
            if (!string.IsNullOrEmpty(reqObj.common.customerId))
            {
                customerId = reqObj.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            #endregion

            //产品二维码是否已被扫过
            ProductTraceLogBLL productTraceLogBLL = new ProductTraceLogBLL(loggingSessionInfo);
            ProductTraceLogEntity[] productTraceLogEntityArray = productTraceLogBLL.Query(new IWhereCondition[]{
                new EqualsCondition() { FieldName = "TraceCode", Value = reqObj.special.traceCode}
            }
                , new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } });

            Loggers.Debug(new DebugLogInfo() { Message = "产品二维码是否已被扫过:" + reqObj.special.traceCode + ", " + productTraceLogEntityArray.ToJSON() });

            ProductTraceLogEntity productTraceLogEntity = new ProductTraceLogEntity();
            productTraceLogEntity.TraceCode = reqObj.special.traceCode;
            productTraceLogEntity.VipId = reqObj.common.userId;
            productTraceLogEntity.RequestIP = requestIP;
            productTraceLogEntity.RequestDevice = reqObj.common.openId;

            //准备返回数据
            CheckProductRespData checkProductRespData = new CheckProductRespData();
            checkProductRespData.content = new CheckProductRespContentData();

            #region 第一次被扫描
            if (productTraceLogEntityArray == null || productTraceLogEntityArray.Length == 0)
            {
                SaturnReturn saturnReturn = new SaturnReturn();
                SaturnProduct saturnProduct = new SaturnProduct();

                #region 从赛腾获取数据
                try
                {
                    SaturnService.PlatformSystemSoapClient service = new SaturnService.PlatformSystemSoapClient();
                    string result = service.SaturnGetProductDetails(reqObj.special.traceCode, "clientCode", "deviceNum", "billID", "BE656D55-D7A8-43F1-A865-67B93FE7EB7A");

                    Loggers.Debug(new DebugLogInfo() { Message = "从赛腾获取数据, result = " + result });

                    saturnReturn = result.DeserializeJSONTo<SaturnReturn>();
                    saturnProduct = saturnReturn.product[0];
                }
                catch (Exception ex)
                {
                    respData.code = "201";
                    respData.description = "从赛腾获取数据失败";
                    respData.exception = ex.ToString();
                    Loggers.Exception(
                        new ExceptionLogInfo() { ErrorMessage = "从赛腾获取数据失败，错误原因" + ex.Message, UserID = reqObj.common.userId });
                }
                #endregion

                #region 处理返回数据
                switch (saturnReturn.result)
                {
                    case 1:
                        //设置验证日志中的产品有效性
                        productTraceLogEntity.IsValid = 1;
                        checkProductRespData.content.isValid = "1";

                        int point = 0;
                        //获取产品信息
                        ItemService itemService = new ItemService(loggingSessionInfo);
                        System.Data.DataRow itemDetail = itemService.GetvwAllItemDetailByItemCode(reqObj.special.traceCode.Substring(2,14), reqObj.common.customerId);

                        Loggers.Debug(new DebugLogInfo() { Message = "获取产品信息: itemDetail = " + itemDetail .ToJSON()});

                        if (itemDetail != null)
                        {
                            //获取本次产品积分
                            int.TryParse(itemDetail["ScanCodeIntegral"].ToString(), out point);
                            checkProductRespData.content.currentPoint = point.ToString();

                            //增加积分
                            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                            //扫描二维码积分的sourceid=19
                            vipIntegralBLL.ProcessPoint(19, reqObj.common.customerId, reqObj.common.userId, reqObj.special.traceCode, null, null, point, "验伪得积分");

                            //会员目前积分
                            VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                            VipEntity vipEntity = vipBLL.GetByID(reqObj.common.userId);
                            Loggers.Debug(new DebugLogInfo() { Message = "vipEntity.Integration: " + vipEntity.Integration.ToJSON() });

                            if (vipEntity.Integration == null)
                                checkProductRespData.content.totalPoint = "0";
                            else
                                checkProductRespData.content.totalPoint = vipEntity.Integration.Value.ToString("f0");

                            //产品详细信息
                            checkProductRespData.content.imageURL = itemDetail["imageUrl"].ToString();
                            checkProductRespData.content.productName = itemDetail["item_name"].ToString();
                            checkProductRespData.content.productFormat = itemDetail["Specification"].ToString();
                            checkProductRespData.content.wineDegree = itemDetail["SKUDegree"].ToString();
                            checkProductRespData.content.factoryName = itemDetail["FactoryName"].ToString();
                        }
                        break;
                    default:
                        //设置返回值中的产品有效性
                        checkProductRespData.content.isValid = "0";
                        //设置验证日志中的产品有效性
                        productTraceLogEntity.IsValid = 0;
                        break;
                }
                #endregion
            }
            #endregion
            #region 重复扫描
            else
            {
                //是真品
                if (productTraceLogEntityArray[0].IsValid.Value == 1)
                {
                    checkProductRespData.content.isValid = "1";
                    productTraceLogEntity.IsValid = 1;

                    ItemService itemService = new ItemService(loggingSessionInfo);
                    System.Data.DataRow itemDetail = itemService.GetvwAllItemDetailByItemCode(productTraceLogEntityArray[0].TraceCode.Substring(2, 14), reqObj.common.customerId);
                    int point = 0;

                    if (itemDetail != null)
                    {
                        Loggers.Debug(new DebugLogInfo() { Message = "重复扫描, 获取产品信息: itemDetail = " + itemDetail.ToJSON() });

                        try
                        {
                            int.TryParse((itemDetail["ScanCodeIntegral"] ?? "").ToString(), out point);
                            checkProductRespData.content.currentPoint = point.ToString(); ;

                            checkProductRespData.content.imageURL = (itemDetail["imageUrl"] ?? "").ToString();
                            checkProductRespData.content.productName = (itemDetail["item_name"] ?? "").ToString();
                            checkProductRespData.content.productFormat = (itemDetail["Specification"] ?? "").ToString();
                            checkProductRespData.content.wineDegree = (itemDetail["SKUDegree"] ?? "").ToString();
                            checkProductRespData.content.factoryName = (itemDetail["FactoryName"] ?? "").ToString();

                            checkProductRespData.content.traceCount = productTraceLogEntityArray.Length.ToString();
                            checkProductRespData.content.lastTraceTime = productTraceLogEntityArray[0].CreateTime.ToString();
                        }
                        catch (Exception ex)
                        {
                            respData.code = "202";
                            respData.description = "提取产品信息失败";
                            respData.exception = ex.ToString();
                            Loggers.Exception(
                                new ExceptionLogInfo() { ErrorMessage = "提取产品信息失败，错误原因" + ex.Message});
                        }
                    }
                }
                //非真品
                else
                {
                    productTraceLogEntity.IsValid = 0;

                    checkProductRespData.content.isValid = "0";
                }
            }
            #endregion

            if (respData.code != "201")
            {
                productTraceLogBLL.Create(productTraceLogEntity);
                checkProductRespData.code = "200";
            }

            respData = checkProductRespData;

            content = respData.ToJSON();
            Loggers.Debug(new DebugLogInfo() { Message = "返回数据，content=" + content });
            return content;
        }

        public string Feedback()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();

            #region //解析请求字符串 chech
            var reqObj = reqContent.DeserializeJSONTo<FeedbackReqData>();

            if (reqObj.special == null)
            {
                respData.code = "101";
                respData.description = "没有特殊参数";
                return respData.ToJSON().ToString();
            }
            if (reqObj.special.name == null || reqObj.special.name.Equals(""))
            {
                respData.code = "102";
                respData.description = "真实姓名不能为空";
                return respData.ToJSON().ToString();
            }
            if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
            {
                respData.code = "103";
                respData.description = "联系电话不能为空";
                return respData.ToJSON().ToString();
            }
            if (reqObj.special.state == null || reqObj.special.state.Equals(""))
            {
                respData.code = "104";
                respData.description = "反馈情况不能为空";
                return respData.ToJSON().ToString();
            }
            #endregion

            #region //判断客户ID是否传递
            if (!string.IsNullOrEmpty(reqObj.common.customerId))
            {
                customerId = reqObj.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            #endregion

            try
            {
                UserFeedbackEntity userFeedbackEntity = new UserFeedbackEntity();
                userFeedbackEntity.UserID = reqObj.common.userId;
                userFeedbackEntity.CustomerId = reqObj.common.customerId;
                userFeedbackEntity.CreateBy = reqObj.common.userId;
                userFeedbackEntity.Name = reqObj.special.name;
                userFeedbackEntity.Phone = reqObj.special.phone;
                userFeedbackEntity.Field1 = reqObj.special.area;
                userFeedbackEntity.Field2 = reqObj.special.seller;
                userFeedbackEntity.Description = reqObj.special.state;

                UserFeedbackBLL userFeedbackBLL = new UserFeedbackBLL(loggingSessionInfo);
                userFeedbackBLL.Create(userFeedbackEntity);

                respData.code = "200";
                respData.description = "提交成功！";
            }
            catch (Exception ex)
            {
                respData.code = "100";
                respData.description = ex.Message;
            }

            content = respData.ToJSON();
            return content;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region CheckProduct参数对象
        public class SaturnReturn
        {
            public int result { get; set; }
            public string boxencode { get; set; }
            public List<SaturnProduct> product { get; set; }
        }

        public class SaturnProduct
        {
            public string ID { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string EnCode { get; set; }
            public string ProductLife { get; set; }
            public string ProductFormat { get; set; }
            public string WineDegree { get; set; }
            public string ProductPrice { get; set; }
            public string ProductImage { get; set; }
            public string FactoryName { get; set; }
            public string ProduceLineName { get; set; }
            public string ProduceDate { get; set; }
            public string BatchCode { get; set; }
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        public class CheckProductRespData : Default.LowerRespData
        {
            public CheckProductRespContentData content { get; set; }
        }

        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class CheckProductRespContentData
        {
            public string isValid { get; set; }
            public string currentPoint { get; set; }
            public string totalPoint { get; set; }
            public string imageURL { get; set; }
            public string productName { get; set; }
            public string wineDegree { get; set; }
            public string productFormat { get; set; }
            public string factoryName { get; set; }
            public string traceCount { get; set; }
            public string lastTraceTime { get; set; }
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class CheckProductReqData : ReqData
        {
            public CheckProductReqSpecialData special;
        }
        
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class CheckProductReqSpecialData
        {
            public string traceCode { get; set; }
            public string openId { get; set; }
        }
        #endregion

        #region Feedback参数对象
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class FeedbackReqData : ReqData
        {
            public FeedbackReqSpecialData special;
        }

        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class FeedbackReqSpecialData
        {
            public string name { get; set; }
            public string phone { get; set; }
            public string area { get; set; }
            public string seller { get; set; }
            public string state { get; set; }
        }
        #endregion
    }
}
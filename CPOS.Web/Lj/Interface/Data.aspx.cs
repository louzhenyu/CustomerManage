using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using System.Data;
using System.Diagnostics;

namespace JIT.CPOS.Web.Lj.Interface
{
    public partial class Data : System.Web.UI.Page
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);
                switch (dataType)
                {
                    case "setVipCard":
                        content = setVipCard();
                        break;
                    case "IsVipCard":
                        content = IsVipCard();
                        break;
                    case "getVipInfo":
                        content = getVipInfo();
                        break;
                    case "getIntegrationExchange":
                        content = getIntegrationExchange();
                        break;
                    case "getOrders":
                        content = getOrders();
                        break;
                    case "getSkuProps":
                        content = getSkusProp();
                        break;
                    case "setExchanges":
                        content = setExchanges();
                        break;
                    case "getEventPrizes":  //获取活动奖项信息
                        content = getEventPrizes();
                        break;
                    case "getEventPrizes2":
                        content = getEventPrizes2();
                        break;
                    case "setEventPrizes":  //刮奖
                        content = setEventPrizes();
                        break;
                    case "getEventInfo":    //获取活动详情(活动介绍)
                        content = getEventInfo();
                        break;
                    case "setEventSignUp":    //提交报名（活动报名）
                        content = setEventSignUp();
                        break;
                    case "getSkus":     //获取定制酒介绍
                        content = getSkus();
                        break;
                    case "getSkuDetail":        //获取定制酒详情
                        content = getSkuDetail();
                        break;
                    case "setOrderInfo":        // 4.13 提交订单
                        content = setOrderInfo();
                        break;
                    case "getRecommend":        //获取推荐排行榜  qianzhi 2013-12-25
                        content = GetRecommend();
                        break;
                    case "getRecommendRecord":  //获取推荐战绩  qianzhi 2013-12-25
                        content = GetRecommendRecord();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region setVipCard
        /// <summary>
        /// 生成VIP卡
        /// </summary>
        public string setVipCard()
        {
            string content = string.Empty;
            var respData = new setVipCardRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<setVipCardReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                //string OpenID = "o8Y7Ejv3jR5fEkneCNu6N1_TIYIM";
                //string WeiXin = "gh_bf70d7900c28";


                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setVipCard: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "setVipCard", reqObj, respData, null);

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                var vipObj = vipService.QueryByEntity(vipQueryInfo, null);
                if (vipObj == null || vipObj.Length == 0 || vipObj[0] == null)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }

                //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                //qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //qrCodeEncoder.QRCodeScale = 5;
                //qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                //System.Drawing.Image qrImage = qrCodeEncoder.Encode(qrcode, Encoding.UTF8);

                string filePath = string.Format("../qrcode_images/{0}.jpg",
                    System.Guid.NewGuid().ToString().Replace("-", ""));
                string fileName = Server.MapPath(filePath);
                //qrImage.Save(fileName, ImageFormat.Jpeg);

                string newFilePath = string.Empty;
                string newFilename = string.Empty;
                string vipCode = vipObj[0].VipCode;
                bool bvipCode = false;
                if (vipCode == null || vipCode.Length < 4)
                {
                    //生成vipcode
                    vipCode = vipService.GetVipCode();
                    bvipCode = true;
                }
                string vipCodeInserg = vipCode;
                vipCode = vipCode.Substring(4).Insert(3, " ");

                if (true)
                {
                    string path = Server.MapPath("../vip.jpg");
                    System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);
                    //System.Drawing.Image imgWarter = qrImage;
                    using (Graphics g = Graphics.FromImage(imgSrc))
                    {
                        g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
                        string fontFamily = "微软雅黑";
                        Color fontColor = ColorTranslator.FromHtml("#735932"); //20px
                        using (Font f = new Font(fontFamily, 20, FontStyle.Regular))
                        {
                            using (Brush b = new SolidBrush(fontColor))
                            {
                                g.DrawString(vipCode, f, b, 346, 246);
                            }
                        }

                    }
                    newFilePath = string.Format("qrcode_images/{0}.jpg",
                        System.Guid.NewGuid().ToString().Replace("-", ""));
                    newFilename = Server.MapPath("../" + newFilePath);
                    imgSrc.Save(newFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                }



                #region 判断是新建还是修改
                VipEntity vipInfo = new VipEntity();
                if (bvipCode)
                {
                    vipInfo.VipCode = vipCodeInserg;
                }
                vipInfo.WeiXinUserId = OpenID;

                vipInfo.QRVipCode = ConfigurationManager.AppSettings["website_url"].Trim() + "Lj/" + newFilePath;

                vipInfo.WeiXin = WeiXin;

                vipInfo.VIPID = vipObj[0].VIPID;
                vipService.Update(vipInfo, false);
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("setVipCard-update: {0}", vipInfo.ToJSON())
                //});
                #endregion

                respData.content = new setVipCardRespContentData();
                respData.content.QRVipCode = vipInfo.QRVipCode;
                respData.content.VipCode = vipCode;
                //respData.Data = vipInfo.QRVipCode;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setVipCard-error: {0}", ex.ToString())
                });
                respData.description = "数据库操作错误" + ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setVipCardRespData : Default.LowerRespData
        {
            public setVipCardRespContentData content;
        }
        public class setVipCardRespContentData
        {
            public string QRVipCode;
            public string VipCode;
        }
        public class setVipCardReqData : Default.ReqData
        {
            public setVipCardReqSpecialData special;
        }
        public class setVipCardReqSpecialData
        {
            public string name;
        }
        #endregion

        #region IsVipCard
        /// <summary>
        /// 是否生成会员卡
        /// </summary>
        public string IsVipCard()
        {
            string content = string.Empty;
            var respData = new IsVipCardRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<IsVipCardReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("IsVipCard: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "IsVipCard", reqObj, respData, null);

                respData.content = new IsVipCardRespContentData();
                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                vipQueryInfo.QRVipCode = "/Lj/";
                var vipObj = vipService.GetLjVipInfo(vipQueryInfo);
                if (vipObj == null || vipObj.VIPID == null || vipObj.VIPID.Trim().Length == 0)
                {
                    respData.content.isGenerate = "0";
                }
                else
                {
                    respData.content.isGenerate = "1";
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class IsVipCardRespData : Default.LowerRespData
        {
            public IsVipCardRespContentData content;
        }
        public class IsVipCardRespContentData
        {
            public string isGenerate;
        }
        public class IsVipCardReqData : Default.ReqData
        {
            public IsVipCardReqSpecialData special;
        }
        public class IsVipCardReqSpecialData
        {
            public string name;
        }
        #endregion

        #region getVipInfo
        /// <summary>
        /// 获取会员信息
        /// </summary>
        public string getVipInfo()
        {
            string content = string.Empty;
            var respData = new getVipInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                var reqObj = reqContent.DeserializeJSONTo<getVipInfoReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipInfo: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getVipInfo", reqObj, respData, null);

                respData.content = new getVipInfoRespContentData();
                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                var vipObj = vipService.GetLjVipInfo(vipQueryInfo);
                if (vipObj == null || vipObj.VIPID == null || vipObj.VIPID.Trim().Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的会员信息";
                    return respData.ToJSON();
                }
                else
                {
                    respData.content.vipId = vipObj.VIPID;
                    respData.content.vipCode = vipObj.VipCode.Substring(4).Insert(3, " ");
                    respData.content.vipName = vipObj.VipName;
                    respData.content.qRVipCode = vipObj.QRVipCode;
                    respData.content.validIntegral = vipObj.Integration == null ? 0 :
                        decimal.Parse(vipObj.Integration.ToString());
                    respData.content.orderCount = vipObj.OrderCount;
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getVipInfoRespData : Default.LowerRespData
        {
            public getVipInfoRespContentData content;
        }
        public class getVipInfoRespContentData
        {
            public string vipId;
            public string vipCode;
            public string vipName;
            public string qRVipCode;
            public decimal validIntegral;
            public int orderCount;
        }
        public class getVipInfoReqData : Default.ReqData
        {
            public getVipInfoReqSpecialData special;
        }
        public class getVipInfoReqSpecialData
        {
            public string name;
        }
        #endregion

        #region getIntegrationExchange
        /// <summary>
        /// 获取积分兑汇
        /// </summary>
        public string getIntegrationExchange()
        {
            string content = string.Empty;
            var respData = new getIntegrationExchangeRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                var reqObj = reqContent.DeserializeJSONTo<getIntegrationExchangeReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegrationExchange: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getIntegrationExchange", reqObj, respData, null);

                respData.content = new getIntegrationExchangeRespContentData();
                respData.content.items = new List<getIntegrationExchangeRespContentItemData>();
                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                var itemList = vipService.GetVipItemList(vipQueryInfo);
                if (itemList != null || itemList.ItemInfoList != null)
                {
                    foreach (var item in itemList.ItemInfoList)
                    {
                        respData.content.items.Add(new getIntegrationExchangeRespContentItemData()
                        {
                            itemId = item.Item_Id,
                            itemName = item.Item_Name,
                            integration = item.integration == null || item.integration.Trim().Length == 0 ? 0 :
                                decimal.Parse(item.integration.ToString()),
                            displayIndex = item.display_index,
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getIntegrationExchangeRespData : Default.LowerRespData
        {
            public getIntegrationExchangeRespContentData content;
        }
        public class getIntegrationExchangeRespContentData
        {
            public IList<getIntegrationExchangeRespContentItemData> items;
        }
        public class getIntegrationExchangeRespContentItemData
        {
            public string itemId;
            public string itemName;
            public decimal integration;
            public int displayIndex;
        }
        public class getIntegrationExchangeReqData : Default.ReqData
        {
            public getIntegrationExchangeReqSpecialData special;
        }
        public class getIntegrationExchangeReqSpecialData
        {
            public string name;
        }
        #endregion

        #region getOrders
        /// <summary>
        /// 获取订单
        /// </summary>
        public string getOrders()
        {
            string content = string.Empty;
            var respData = new getOrdersRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getOrdersReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getOrders: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getOrders", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getOrdersRespContentData();
                respData.content.Orders = new List<getOrdersRespContentOrderData>();
                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                var vipObj = vipService.GetLjVipInfo(vipQueryInfo);
                if (vipObj == null || vipObj.VIPID == null || vipObj.VIPID.Trim().Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的会员信息";
                    return respData.ToJSON();
                }
                vipQueryInfo.VIPID = vipObj.VIPID;
                var orderList = vipService.GetVipOrderList(vipQueryInfo,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.totalCount = orderList.ICount;
                if (orderList != null || orderList.InoutInfoList != null)
                {
                    foreach (var orderItem in orderList.InoutInfoList)
                    {
                        var order = new getOrdersRespContentOrderData()
                        {
                            orderId = orderItem.order_id,
                            createTime = orderItem.create_time,
                            orderNo = orderItem.order_no,
                            orderStatus = orderItem.Field19,
                            totalAmount = orderItem.total_amount,
                            paidAmount = orderItem.Field14 == null || orderItem.Field14 == string.Empty ? 0 :
                                decimal.Parse(orderItem.Field14)
                        };
                        order.OrderDetails = new List<getOrdersRespContentOrderDetailData>();
                        if (orderItem.InoutDetailList != null)
                        {
                            foreach (var orderDetailItem in orderItem.InoutDetailList)
                            {
                                order.OrderDetails.Add(new getOrdersRespContentOrderDetailData()
                                {
                                    orderDetailId = orderDetailItem.order_detail_id,
                                    itemId = orderDetailItem.sku_id,
                                    itemName = orderDetailItem.item_name,
                                    stdPrice = orderDetailItem.std_price,
                                    enterQty = orderDetailItem.enter_qty,
                                    displayIndex = orderDetailItem.display_index,
                                });
                            }
                        }
                        respData.content.Orders.Add(order);
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getOrdersRespData : Default.LowerRespData
        {
            public getOrdersRespContentData content;
        }
        public class getOrdersRespContentData
        {
            public IList<getOrdersRespContentOrderData> Orders;
            public int totalCount;
        }
        public class getOrdersRespContentOrderData
        {
            public string orderId;
            public string createTime;
            public string orderNo;
            public string orderStatus;
            public decimal totalAmount;
            public decimal paidAmount;
            public IList<getOrdersRespContentOrderDetailData> OrderDetails;
        }
        public class getOrdersRespContentOrderDetailData
        {
            public string orderDetailId;
            public string itemId;
            public string itemName;
            public decimal stdPrice;
            public decimal enterQty;
            public int displayIndex;
        }
        public class getOrdersReqData : Default.ReqData
        {
            public getOrdersReqSpecialData special;
        }
        public class getOrdersReqSpecialData
        {
            public int page = 1;
            public int pageSize = 5;
        }
        #endregion

        #region getSkusProp
        /// <summary>
        /// 获取getSkusProp
        /// </summary>
        public string getSkusProp()
        {
            string content = string.Empty;
            var respData = new getSkusPropRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getSkusPropReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getSkusProp: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getSkusProp", reqObj, respData, null);

                respData.content = new getSkusPropRespContentData();
                respData.content.Skus = new List<getSkusPropRespContentOrderData>();
                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                //var vipObj = vipService.GetLjVipInfo(vipQueryInfo);
                //if (vipObj == null || vipObj.VIPID == null || vipObj.VIPID.Trim().Length == 0)
                //{
                //    respData.code = "103";
                //    respData.description = "未查找到匹配的会员信息";
                //    return respData.ToJSON();
                //}
                //vipQueryInfo.VIPID = vipObj.VIPID;
                var orderList = vipService.GetVipSkuPropList(vipQueryInfo, reqObj.special.itemId.Trim());
                //respData.content.totalCount = orderList.Count;
                if (orderList != null || orderList.SkuInfoList != null)
                {
                    foreach (var orderItem in orderList.SkuInfoList)
                    {
                        var order = new getSkusPropRespContentOrderData()
                        {
                            skuId = orderItem.sku_id,
                            gg = orderItem.prop_1_detail_id,
                            degree = orderItem.prop_2_detail_id,
                            baseWineYear = orderItem.prop_3_detail_id,
                            agePitPits = orderItem.prop_5_detail_id,
                            price = orderItem.prop_4_detail_id
                        };
                        respData.content.Skus.Add(order);
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getSkusPropRespData : Default.LowerRespData
        {
            public getSkusPropRespContentData content;
        }
        public class getSkusPropRespContentData
        {
            public IList<getSkusPropRespContentOrderData> Skus;
        }
        public class getSkusPropRespContentOrderData
        {
            public string skuId;
            public string gg;
            public string degree;
            public string baseWineYear;
            public string agePitPits;
            public string price;
        }
        public class getSkusPropReqData : Default.ReqData
        {
            public getSkusPropReqSpecialData special;
        }
        public class getSkusPropReqSpecialData
        {
            public string itemId;
        }
        #endregion

        #region setExchanges
        /// <summary>
        /// 兑换
        /// </summary>
        public string setExchanges()
        {
            string content = string.Empty;
            var respData = new setExchangesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<setExchangesReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setExchanges: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "setExchanges", reqObj, respData, null);

                respData.content = new setExchangesRespContentData();
                VipBLL vipService = new VipBLL(loggingSessionInfo);
                VipEntity vipQueryInfo = new VipEntity();
                vipQueryInfo.WeiXinUserId = OpenID;
                vipQueryInfo.WeiXin = WeiXin;
                var vipObj = vipService.GetLjVipInfo(vipQueryInfo);

                ItemInfo itemInfo = new ItemInfo();

                if (vipObj == null || vipObj.VIPID == null || vipObj.VIPID.Trim().Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的会员信息";
                    return respData.ToJSON();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setExchangesRespData : Default.LowerRespData
        {
            public setExchangesRespContentData content;
        }
        public class setExchangesRespContentData
        {

        }
        public class setExchangesReqData : Default.ReqData
        {
            public setExchangesReqSpecialData special;
        }
        public class setExchangesReqSpecialData
        {
            public IList<setExchangesReqSpecialItemData> Items;
        }
        public class setExchangesReqSpecialItemData
        {
            public string itemId;
            public int count;
        }
        #endregion

        #region getEventPrizes 获取活动奖项信息
        /// <summary>
        /// 获取活动奖项信息（缺中奖的存储过程，稍后实现）
        /// </summary>
        public string getEventPrizes()
        {
            string content = string.Empty;
            var respData = new getEventPrizesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventPrizesReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string eventID = reqObj.special.eventId;    //活动ID
                if (eventID == null || eventID.Equals(""))
                {
                    eventID = "8D41CDD7D5E4499195316E4645FCD7B9";
                }
                string longitude = reqObj.special.longitude;   //经度
                string latitude = reqObj.special.latitude;     //纬度
                string vipID = reqObj.common.userId;
                string vipName = string.Empty;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventPrizes: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventPrizes", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventPrizesRespContentData();
                respData.content.prizes = new List<PrizesEntity>();

                #region 是否在活动现场
                respData.content.isSite = "1";
                #endregion

                #region 获取VIPID

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    //WeiXinUserId = OpenID
                    VIPID = vipID
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                    vipName = vipList.FirstOrDefault().VipName;
                }

                #endregion

                #region 是否抽奖

                #region 获取是否开始抽奖
                LEventRoundBLL eventRoundServer = new LEventRoundBLL(loggingSessionInfo);
                var eventRoundList = eventRoundServer.QueryByEntity(new LEventRoundEntity
                {
                    EventId = eventID
                    ,RoundStatus = 0
                    ,IsDelete = 0
                }, null);
                var eventRoundList1 = eventRoundServer.QueryByEntity(new LEventRoundEntity
                {
                    EventId = eventID
                    ,IsDelete = 0
                }, null);
                if (eventRoundList != null || eventRoundList.Length > 0 || eventRoundList[0] != null)
                {
                    if (eventRoundList.Length == eventRoundList1.Length)
                    {
                        respData.content.remark = "不要着急，现在还没到抽奖时间噢…";
                    }
                }
                #endregion

                LLotteryLogBLL lotteryService = new LLotteryLogBLL(loggingSessionInfo);
                var lotterys = lotteryService.QueryByEntity(new LLotteryLogEntity()
                {
                    EventId = eventID,
                    VipId = vipID
                }, null);

                LPrizeWinnerBLL winnerService1 = new LPrizeWinnerBLL(loggingSessionInfo);
                var prizeName1 = winnerService1.GetWinnerInfoString(vipID, eventID);

                //add by Willie   如果抽奖次数设为0则表示一直可以抽
                LEventsBLL eventService = new LEventsBLL(loggingSessionInfo);
                var eventEntity = eventService.GetByID(eventID);

                Loggers.Debug(new DebugLogInfo() { Message = "lotterys.Length = " + lotterys.Length + " ,eventEntity.PersonCount.Value = " + eventEntity.PersonCount.Value + " ,prizeName1 = " + prizeName1});

                if (lotterys.Length == 0 || eventEntity.PersonCount.Value == 0)
                {
                    if (prizeName1 == null || prizeName1.Equals(""))
                    {//没中奖，就一直能刮奖，直到能中奖为止
                        //if (lotterys != null && lotterys.Length > 0)
                        //{
                        //    respData.content.isLottery = "1"; //Jermyn20130730 一直能抽奖，暂时改为0，本来为1
                        //}
                        //else
                        //{
                        //    respData.content.isLottery = "0";
                        //Jermyn20131108 抽奖
                        LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);
                        GetResponseParams<JIT.CPOS.BS.Entity.Interface.ShakeOffLotteryResult> returnDataObj = poolsServer.SetShakeOffLottery(
                        vipName,
                        vipID,
                        eventID,
                        ToFloat(longitude),
                        ToFloat(latitude));
                        //}

                        Loggers.Debug(new DebugLogInfo() { Message = "returnDataObj.Params.result_code = " + returnDataObj.Params.result_code });

                        if (string.IsNullOrEmpty(returnDataObj.Params.result_code) || returnDataObj.Params.result_code.Equals("1")) //中奖这次是隐藏，否则，就是显示刮奖
                        {
                            respData.content.isLottery = "0"; ////Jermyn20131108 没有中奖，一直能刮奖
                        }
                        else
                        {
                            respData.content.isLottery = "1";
                        }
                    }
                    else
                    {
                        respData.content.isLottery = "1";
                    }
                }
                else
                    respData.content.isLottery = "1";
                #endregion

                #region 是否中奖

                LPrizeWinnerBLL winnerService = new LPrizeWinnerBLL(loggingSessionInfo);
                var prizeName = winnerService.GetWinnerInfoString(vipID, eventID);

                if (prizeName!= null && !prizeName.Equals(""))
                {
                    respData.content.isWinning = "1";
                    respData.content.winningDesc = prizeName.ToString();
                    
                }
                else
                {
                    respData.content.isWinning = "0";
                    respData.content.winningDesc = "谢谢你";
                    //respData.content.isLottery = "0";     //edit by Willie 前面已经处理好isLottery属性，此处不应当再处理
                }

                #endregion

                #region 奖品信息

                LPrizesBLL prizesService = new LPrizesBLL(loggingSessionInfo);

                //var prizesList = prizesService.QueryByEntity(new LPrizesEntity()
                //{
                //    EventId = eventID
                //}, new Utility.DataAccess.Query.OrderBy[] { new Utility.DataAccess.Query.OrderBy { FieldName = " DisplayIndex ", Direction = Utility.DataAccess.Query.OrderByDirections.Asc } });

                var prizesList = prizesService.QueryByEntity(new LPrizesEntity()
                {
                    EventId = eventID
                }, null );


                if (prizesList != null && prizesList.Length > 0)
                {
                    foreach (var item in prizesList)
                    {
                        var entity = new PrizesEntity()
                        {
                            prizesID = item.PrizesID,
                            prizeName = item.PrizeName,
                            prizeDesc = item.PrizeDesc,
                            displayIndex = item.DisplayIndex.ToString(),
                            countTotal = item.CountTotal.ToString()
                            ,
                            imageUrl = item.ImageUrl
                            ,
                            sponsor = item.ContentText
                        };
                        if (prizeName.Equals(item.PrizeName))
                        {
                            respData.content.prizeIndex = item.DisplayIndex.ToString();
                        }
                        respData.content.prizes.Add(entity);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventPrizesRespData : Default.LowerRespData
        {
            public getEventPrizesRespContentData content;
        }
        public class getEventPrizesRespContentData
        {
            public string isSite;       //是否在现场		1=是，0=否
            public string isLottery;    //是否抽奖 1= 否，0=是
            public string isWinning;    //是否中奖  1= 是，0=否
            public string winningDesc;  //奖品名称
            public string remark;       //现场描述
            public string prizeIndex; //序号
            public IList<PrizesEntity> prizes;  //奖品集合
        }
        public class PrizesEntity
        {
            public string prizesID;     //奖品标识
            public string prizeName;    //奖品名称
            public string prizeDesc;    //奖品描述
            public string displayIndex; //排序
            public string countTotal;   //奖品数量
            public string imageUrl;     //图片
            public string sponsor;      //赞助对应ContentText
        }
        public class getEventPrizesReqData : Default.ReqData
        {
            public getEventPrizesReqSpecialData special;
        }
        public class getEventPrizesReqSpecialData
        {
            public string eventId;      //活动标识
            public string longitude;    //经度
            public string latitude;     //纬度
        }
        #endregion

        #region getEventPrizes2 获取活动奖项信息（）
        /// <summary>
        /// 获取活动奖项信息（缺中奖的存储过程，稍后实现）
        /// </summary>
        public string getEventPrizes2()
        {
            string content = string.Empty;
            var respData = new getEventPrizesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventPrizesReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string eventID = reqObj.special.eventId;    //活动ID
                if (eventID == null || eventID.Equals(""))
                {
                    eventID = "8D41CDD7D5E4499195316E4645FCD7B9";
                }
                string longitude = reqObj.special.longitude;   //经度
                string latitude = reqObj.special.latitude;     //纬度
                string vipID = reqObj.common.userId;
                string vipName = string.Empty;
                string prizeName = string.Empty;
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventPrizes: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventPrizes", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventPrizesRespContentData();
                respData.content.prizes = new List<PrizesEntity>();

                #region 是否在活动现场
                respData.content.isSite = "1";
                #endregion

                if (!string.IsNullOrEmpty(vipID))
                {
                    #region 获取VIPID

                    VipBLL vipService = new VipBLL(loggingSessionInfo);
                    var vipList = vipService.QueryByEntity(new VipEntity()
                    {
                        //WeiXinUserId = OpenID
                        VIPID = vipID
                    }, null);

                    if (vipList == null || vipList.Length == 0)
                    {
                        respData.code = "103";
                        respData.description = "未查找到匹配的VIP信息";
                        return respData.ToJSON();
                    }
                    else
                    {
                        vipID = vipList.FirstOrDefault().VIPID;
                        vipName = vipList.FirstOrDefault().VipName;
                    }

                    #endregion

                    #region 是否抽奖

                    #region 获取是否开始抽奖
                    LEventRoundBLL eventRoundServer = new LEventRoundBLL(loggingSessionInfo);
                    var eventRoundList = eventRoundServer.QueryByEntity(new LEventRoundEntity
                    {
                        EventId = eventID
                        ,
                        RoundStatus = 1
                        ,
                        IsDelete = 0
                    }, null);
                    if (eventRoundList == null || eventRoundList.Length == 0 || eventRoundList[0] == null)
                    {
                        respData.content.remark = "不要着急，现在还没到抽奖时间噢…";
                    }
                    #endregion

                    LLotteryLogBLL lotteryService = new LLotteryLogBLL(loggingSessionInfo);
                    var lotterys = lotteryService.QueryByEntity(new LLotteryLogEntity()
                    {
                        EventId = eventID,
                        VipId = vipID
                    }, null);

                    LPrizeWinnerBLL winnerService1 = new LPrizeWinnerBLL(loggingSessionInfo);
                    var prizeName1 = winnerService1.GetWinnerInfoString(vipID, eventID);

                    //add by Willie   如果抽奖次数设为0则表示一直可以抽
                    LEventsBLL eventService = new LEventsBLL(loggingSessionInfo);
                    var eventEntity = eventService.GetByID(eventID);

                    if (lotterys.Length == 0 || eventEntity.PersonCount.Value == 0)
                    {
                        if (prizeName1 == null || prizeName1.Equals(""))
                        {//没中奖，就一直能刮奖，直到能中奖为止
                            //if (lotterys != null && lotterys.Length > 0)
                            //{
                            //    respData.content.isLottery = "1"; //Jermyn20130730 一直能抽奖，暂时改为0，本来为1
                            //}
                            //else
                            //{
                            //    respData.content.isLottery = "0";
                            //Jermyn20131108 抽奖
                            LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);
                            GetResponseParams<JIT.CPOS.BS.Entity.Interface.ShakeOffLotteryResult> returnDataObj = poolsServer.SetShakeOffLottery(
                            vipName,
                            vipID,
                            eventID,
                            ToFloat(longitude),
                            ToFloat(latitude));
                            //}

                            if (string.IsNullOrEmpty(returnDataObj.Params.result_code) || returnDataObj.Params.result_code.Equals("1")) //中奖这次是隐藏，否则，就是显示刮奖
                            {
                                respData.content.isLottery = "0"; ////Jermyn20131108 没有中奖，一直能刮奖
                            }
                            else
                            {
                                respData.content.isLottery = "1";
                            }
                        }
                        else
                        {
                            respData.content.isLottery = "1";
                        }
                    }
                    else
                        respData.content.isLottery = "0";
                    #endregion

                    #region 是否中奖

                    LPrizeWinnerBLL winnerService = new LPrizeWinnerBLL(loggingSessionInfo);
                    prizeName = winnerService.GetWinnerInfoString(vipID, eventID);

                    if (prizeName != null && !prizeName.Equals(""))
                    {
                        respData.content.isWinning = "1";
                        respData.content.winningDesc = prizeName.ToString();
                    }
                    else
                    {
                        respData.content.isWinning = "0";
                        respData.content.winningDesc = "谢谢你";
                        //respData.content.isLottery = "0";     //edit by Willie 前面已经处理好isLottery属性，此处不应当再处理
                    }

                    #endregion
                }
                #region 奖品信息

                LPrizesBLL prizesService = new LPrizesBLL(loggingSessionInfo);

                var prizesList = prizesService.QueryByEntity(new LPrizesEntity()
                {
                    EventId = eventID
                }, new Utility.DataAccess.Query.OrderBy[] { new Utility.DataAccess.Query.OrderBy { FieldName = " DisplayIndex ", Direction = Utility.DataAccess.Query.OrderByDirections.Asc } });

                if (prizesList != null && prizesList.Length > 0)
                {
                    foreach (var item in prizesList)
                    {
                        var entity = new PrizesEntity()
                        {
                            prizesID = item.PrizesID,
                            prizeName = item.PrizeName,
                            prizeDesc = item.PrizeDesc,
                            displayIndex = item.DisplayIndex.ToString(),
                            countTotal = item.CountTotal.ToString()
                            ,
                            imageUrl = item.ImageUrl
                            ,
                            sponsor = item.ContentText
                        };
                        if (prizeName.Equals(item.PrizeName))
                        {
                            respData.content.prizeIndex = item.DisplayIndex.ToString();
                        }
                        respData.content.prizes.Add(entity);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region setEventPrizes 刮奖
        /// <summary>
        /// 刮奖
        /// </summary>
        public string setEventPrizes()
        {
            string content = string.Empty;
            var respData = new setEventPrizesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<setEventPrizesReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string eventId = reqObj.special.eventId;      //活动标识
                if (eventId == null || eventId.Equals(""))
                {
                    eventId = "8D41CDD7D5E4499195316E4645FCD7B9";
                }
                string vipID = string.Empty;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventPrizes: {0}", reqContent)
                });
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "setEventPrizes", reqObj, respData, reqObj.special.ToJSON());

                #region 获取VIPID

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = OpenID,
                    WeiXin = WeiXin
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                }

                #endregion

                LLotteryLogBLL lotteryService = new LLotteryLogBLL(loggingSessionInfo);
                var lotterys = lotteryService.QueryByEntity(new LLotteryLogEntity()
                {
                    EventId = eventId,
                    VipId = vipID
                }, null);

                if (lotterys != null && lotterys.Length > 0)
                {
                    //更新
                    var lotteryEntity = lotterys.FirstOrDefault();
                    lotteryEntity.LotteryCount += 1;
                    lotteryService.Update(lotteryEntity);
                }
                else
                {
                    //新增
                    var lotteryEntity = new LLotteryLogEntity()
                    {
                        LogId = CPOS.Common.Utils.NewGuid(),
                        VipId = vipID,
                        EventId = eventId,
                        LotteryCount = 1
                    };
                    lotteryService.Create(lotteryEntity);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setEventPrizesRespData : Default.LowerRespData
        {
            public setEventPrizesRespContentData content;
        }
        public class setEventPrizesRespContentData
        {

        }
        public class setEventPrizesReqData : Default.ReqData
        {
            public setEventPrizesReqSpecialData special;
        }
        public class setEventPrizesReqSpecialData
        {
            public string eventId;      //活动标识
        }
        #endregion

        #region getEventInfo 获取活动奖项信息
        /// <summary>
        /// 获取活动奖项信息
        /// </summary>
        public string getEventInfo()
        {
            string content = string.Empty;
            var respData = new getEventInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventInfoReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string eventID = reqObj.special.EventId;    //活动ID
                string longitude = reqObj.special.Longitude;//经度
                string latitude = reqObj.special.Latitude;  //纬度
                float distance = 30f;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventInfo: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventInfo", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventInfoRespContentData();

                LEventsBLL eventService = new LEventsBLL(loggingSessionInfo);
                var ds = eventService.GetEventInfo(eventID);

                LEventsEntity eventEntity = null;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    eventEntity = DataTableToObject.ConvertToObject<LEventsEntity>(ds.Tables[0].Rows[0]);
                }

                if (eventEntity != null)
                {
                    respData.content.eventId = eventEntity.EventID;
                    respData.content.imageURL = eventEntity.ImageURL;
                    respData.content.title = eventEntity.Title;
                    respData.content.eventTime = eventEntity.BeginTime;
                    respData.content.eventTime += (string.IsNullOrEmpty(eventEntity.EndTime)) ? "" : " 至 " + eventEntity.EndTime;
                    respData.content.address = eventEntity.Address;
                    respData.content.Description = eventEntity.Description;
                    respData.content.cityId = eventEntity.CityID;
                    respData.content.isHasApply = "0";
                    respData.content.isOverEvent = "0";
                    //是否在现场
                    respData.content.isSite = "1";//eventService.IsSite(eventID, latitude, longitude, distance) > 0 ? "1" : "0";

                    //是否可以报名
                    if (!string.IsNullOrEmpty(eventEntity.BeginTime))
                    {
                        if (DateTime.Now < Convert.ToDateTime(eventEntity.BeginTime))
                        {
                            respData.content.isHasApply = "1";
                        }
                    }

                    //活动是否结束
                    if (!string.IsNullOrEmpty(eventEntity.EndTime))
                    {
                        if (DateTime.Now < Convert.ToDateTime(eventEntity.EndTime))
                        {
                            respData.content.isHasApply = "1";
                        }
                    }
                    else
                    {
                        if (DateTime.Now < Convert.ToDateTime(eventEntity.BeginTime))
                        {
                            respData.content.isOverEvent = "1";
                        }
                    }

                    respData.content.myOrderCount = eventService.GetMyOrderCount(eventID, OpenID).ToString(); //我的认购数量

                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventInfoRespData : Default.LowerRespData
        {
            public getEventInfoRespContentData content;
        }
        public class getEventInfoRespContentData
        {
            public string eventId;      //活动标识
            public string imageURL;     //图片链接
            public string title;        //活动主题
            public string eventTime;    //活动时间
            public string address;      //活动地址
            public string Description;  //活动简介
            public string cityId;       //城市
            public string isHasApply;   //是否可以报名 1=是，0=否
            public string isOverEvent;  //活动是否结束 1=是，0=否
            public string isSite;       //是否在现场   1=是，0=否
            public string myOrderCount; //我的认购数量
        }
        public class getEventInfoReqData : Default.ReqData
        {
            public getEventInfoReqSpecialData special;
        }
        public class getEventInfoReqSpecialData
        {
            public string EventId;  //活动标识
            public string Longitude;//经度
            public string Latitude; //纬度
        }
        #endregion

        #region setEventSignUp 提交报名（活动报名）
        /// <summary>
        /// 提交报名（活动报名）
        /// </summary>
        public string setEventSignUp()
        {
            string content = string.Empty;
            var respData = new setEventSignUpRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<setEventSignUpReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string vipID = string.Empty;

                string eventID = reqObj.special.EventId;    //活动ID
                string userName = reqObj.special.UserName;  //用户名
                string phone = reqObj.special.Phone;        //手机号
                string city = reqObj.special.City;          //所在城市

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventSignUp: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "setEventSignUp", reqObj, respData, reqObj.special.ToJSON());

                #region 获取VIPID

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = OpenID,
                    WeiXin = WeiXin
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                }

                #endregion

                #region 新增或更新活动报名表

                LEventSignUpBLL signUpService = new LEventSignUpBLL(loggingSessionInfo);
                var signUpList = signUpService.QueryByEntity(new LEventSignUpEntity()
                {
                    EventID = eventID,
                    VipID = vipID
                }, null);

                var signUpEntity = new LEventSignUpEntity()
                {
                    EventID = eventID,
                    VipID = vipID,
                    UserName = userName,
                    Phone = phone,
                    City = city
                };

                if (signUpList != null && signUpList.Length > 0)
                {
                    //更新
                    signUpEntity.SignUpID = signUpList.FirstOrDefault().SignUpID;
                    signUpService.Update(signUpEntity, false);
                }
                else
                {
                    //新增
                    signUpEntity.SignUpID = CPOS.Common.Utils.NewGuid();
                    signUpService.Create(signUpEntity);
                }

                #region 推送消息 Jermyn20130726
                LEventsBLL eventServer = new LEventsBLL(loggingSessionInfo);
                var ds = eventServer.GetEventInfo(eventID);

                LEventsEntity eventEntity = null;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    eventEntity = DataTableToObject.ConvertToObject<LEventsEntity>(ds.Tables[0].Rows[0]);
                }
                if (eventEntity != null)
                {
                    string strMsg = "感谢您报名参加" + eventEntity.Title + "，请您于" + eventEntity.BeginTime.ToString() + "准时出席，谢谢.";
                    string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                    string msgText = string.Format("{0}", strMsg);
                    string msgData = "<xml><OpenID><![CDATA[" + OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                    var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMsgResult:{0}", msgResult)
                    });
                }
                #endregion
                #endregion

                #region 更新会员手机号

                if (!string.IsNullOrEmpty(phone))
                {
                    vipService.Update(new VipEntity()
                    {
                        VIPID = vipID,
                        Phone = phone
                    }, false);
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setEventSignUpRespData : Default.LowerRespData
        {
            public setEventSignUpRespContentData content;
        }
        public class setEventSignUpRespContentData
        {

        }
        public class setEventSignUpReqData : Default.ReqData
        {
            public setEventSignUpReqSpecialData special;
        }
        public class setEventSignUpReqSpecialData
        {
            public string EventId;  //活动标识
            public string UserName; //用户名
            public string Phone;    //手机号
            public string City;     //所在城市
        }
        #endregion

        #region getSkus 获取定制酒介绍
        /// <summary>
        /// 获取定制酒介绍
        /// </summary>
        public string getSkus()
        {
            string content = string.Empty;
            var respData = new getSkusRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getSkusReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string eventID = reqObj.special.EventId;    //活动标识

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getSkus: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getSkus", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getSkusRespContentData();
                respData.content.Skus = new List<SkusEntity>();

                LEventsBLL eventService = new LEventsBLL(loggingSessionInfo);
                var ds = eventService.GetSkus();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.Skus = DataTableToObject.ConvertToList<SkusEntity>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getSkusRespData : Default.LowerRespData
        {
            public getSkusRespContentData content;
        }
        public class getSkusRespContentData
        {
            public IList<SkusEntity> Skus;  //奖品集合
        }
        public class SkusEntity
        {
            public string skuId { get; set; }       //商品标识
            public string imageURL { get; set; }    //图片链接
            public string gg { get; set; }          //规格
            public string degree { get; set; }      //酒度
            public string baseWineYear { get; set; }//基酒年份
            public string agePitPits { get; set; }  //窖池窖龄
            public string itemName { get; set; }    //商品名称
            public string itemId { get; set; }      //商品ID
        }
        public class getSkusReqData : Default.ReqData
        {
            public getSkusReqSpecialData special;
        }
        public class getSkusReqSpecialData
        {
            public string EventId;  //活动标识
        }
        #endregion

        #region getSkuDetail 获取定制酒详情
        /// <summary>
        /// 获取定制酒详情
        /// </summary>
        public string getSkuDetail()
        {
            string content = string.Empty;
            var respData = new getSkuDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getSkuDetailReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string skuId = reqObj.special.skuId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getSkuDetail: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getSkuDetail", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getSkuDetailRespContentData();

                LEventsBLL eventService = new LEventsBLL(loggingSessionInfo);
                var ds = eventService.GetSkuDetail(skuId);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<getSkuDetailRespContentData>(ds.Tables[0].Rows[0]);

                    #region 获取图片集合
                    ObjectImagesBLL imageServer = new ObjectImagesBLL(loggingSessionInfo);
                    IList<getItemImagesData> list = new List<getItemImagesData>();
                    var imageList = imageServer.GetObjectImagesByObjectId(ds.Tables[0].Rows[0]["itemId"].ToString());
                    if (imageList != null && imageList.Count > 0)
                    {
                        foreach (var imageInfo in imageList)
                        {
                            getItemImagesData image1 = new getItemImagesData();
                            image1.imageId = imageInfo.ImageId;
                            image1.imageURL = imageInfo.ImageURL;
                            image1.displayIndex = imageInfo.DisplayIndex.ToString();
                            list.Add(image1);
                        }
                        respData.content.imageList = list;
                    }
                    #endregion
                }


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getSkuDetailRespData : Default.LowerRespData
        {
            public getSkuDetailRespContentData content;
        }
        public class getSkuDetailRespContentData
        {
            public string skuId { get; set; }       //商品标识
            public string imageURL { get; set; }    //图片链接
            public string gg { get; set; }          //规格
            public string degree { get; set; }      //酒度
            public string baseWineYear { get; set; }//基酒年份
            public string agePitPits { get; set; }  //窖池窖龄
            public string itemRemark { get; set; }    //商品介绍
            public string salesPrice { get; set; }    //零售价
            public string purchasePrice { get; set; } //认购价
            public string itemName { get; set; }      //商品名称
            public string unit { get; set; }          //单位(元/瓶)
            public string itemId { get; set; }
            public IList<getItemImagesData> imageList { get; set; } //图片集合
        }
        public class getSkuDetailReqData : Default.ReqData
        {
            public getSkuDetailReqSpecialData special;
        }
        public class getSkuDetailReqSpecialData
        {
            public string skuId;    //商品标识
        }
        public class getItemImagesData
        {
            public string imageId;
            public string imageURL;
            public string displayIndex;
        }

        #endregion

        #region 4.13 提交订单
        public string setOrderInfo()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });
                #region
                if (reqObj.special.skuId == null || reqObj.special.skuId.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "skuId不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.eventId == null || reqObj.special.eventId.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "活动标识不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.tableNumber == null || reqObj.special.tableNumber.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "座号不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.userName == null || reqObj.special.userName.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "用户不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.phone == null || reqObj.special.phone.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "手机号不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.individuationInfo == null || reqObj.special.individuationInfo.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "个性化信息不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.salesPrice == null || reqObj.special.salesPrice.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "认购价不能为空";
                    return respData.ToJSON();
                }
                #endregion
                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "setOrderInfo", reqObj, respData, reqObj.special.ToJSON());

                InoutService inoutService = new InoutService(loggingSessionInfo);
                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = inoutService.SetWapPosInoutInfo(reqObj.special.skuId.Trim()
                                                            , reqObj.special.eventId.Trim()
                                                            , OpenID.Trim()
                                                            , WeiXin.Trim()
                                                            , reqObj.special.userName.Trim()
                                                            , reqObj.special.phone.Trim()
                                                            , reqObj.special.individuationInfo.Trim()
                                                            , reqObj.special.salesPrice.Trim()
                                                            , reqObj.special.tableNumber.Trim()
                                                            , loggingSessionInfo
                                                            , out strError, out strMsg);
                if (bReturn)
                {
                    respData.code = "200";
                    // 推送消息
                    string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                    string msgText = string.Format("{0}", strMsg);
                    string msgData = "<xml><OpenID><![CDATA[" + OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                    var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMsgResult:{0}", msgResult)
                    });

                }
                else
                {
                    respData.code = "101";
                }
                respData.description = strError;
                return respData.ToJSON();
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setOrderInfoReqData : Default.ReqData
        {
            public setOrderInfoReqSpecialData special;
        }
        public class setOrderInfoReqSpecialData
        {
            public string eventId;		//活动标识
            public string skuId;	    //sku标识
            public string userName;	    //姓名
            public string phone;		//手机号
            public string individuationInfo;	//个性化信息
            public string salesPrice;	//价格
            public string tableNumber;  //座号

        }
        #endregion

        #region GetRecommend 获取推荐排行榜  qianzhi 2013-12-25
        /// <summary>
        /// 获取推荐排行榜
        /// </summary>
        public string GetRecommend()
        {
            string content = string.Empty;

            var respData = new GetRecommendRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetRecommendReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetRecommend: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new GetRecommendRespContentData();
                respData.content.recommendList = new List<RecommendEntity>();

                var server = new CouponBLL(loggingSessionInfo);
                var ds = server.GetRecommendList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.recommendList = DataTableToObject.ConvertToList<RecommendEntity>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetRecommendRespData : Default.LowerRespData
        {
            public GetRecommendRespContentData content;
        }
        public class GetRecommendRespContentData
        {
            public IList<RecommendEntity> recommendList;    //推荐排行榜列表
        }
        public class RecommendEntity
        {
            public string vipId { get; set; }            //会员ID
            public string vipName { get; set; }          //会员昵称
            public int recommendCount { get; set; }      //推荐成功人数
            public int parValue { get; set; }            //获得优惠券
            public Int64 displayIndex { get; set; }      //排名
            public int integral { get; set; }
        }
        public class GetRecommendReqData : Default.ReqData
        {

        }

        #endregion

        #region GetRecommendRecord 获取推荐战绩  qianzhi 2013-12-25
        /// <summary>
        /// 获取推荐战绩
        /// </summary>
        public string GetRecommendRecord()
        {
            string content = string.Empty;

            var respData = new GetRecommendRecordRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetRecommendRecordReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetRecommendRecord: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new GetRecommendRecordRespContentData();
                respData.content.recordList = new List<RecordEntity>();

                var server = new CouponBLL(loggingSessionInfo);
                var userId = ToStr(reqObj.common.userId);
                var ds = server.GetRecommendRecord(userId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<GetRecommendRecordRespContentData>(ds.Tables[0].Rows[0]);

                    ds = server.GetRecommendRecordList(userId);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        respData.content.recordList = DataTableToObject.ConvertToList<RecordEntity>(ds.Tables[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetRecommendRecordRespData : Default.LowerRespData
        {
            public GetRecommendRecordRespContentData content;
        }
        public class GetRecommendRecordRespContentData
        {
            public string vipId { get; set; }            //会员ID
            public string vipName { get; set; }          //会员昵称
            public int recommendCount { get; set; }      //推荐成功人数
            public int parValue { get; set; }            //获得优惠券
            public int integral { get; set; }            //获得积分
            public IList<RecordEntity> recordList { get; set; }    //推荐人员列表
        }
        public class RecordEntity
        {
            public string vipId { get; set; }    //会员ID
            public string vipName { get; set; }  //会员昵称
            public string recommendDate { get; set; }
        }
        public class GetRecommendRecordReqData : Default.ReqData
        {

        }

        #endregion

        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        #endregion
    }
}
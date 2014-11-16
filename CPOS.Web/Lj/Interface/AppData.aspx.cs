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
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.Web.Lj.Interface
{
    public partial class AppData : System.Web.UI.Page
    {
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
                    case "messageGet":
                        content = messageGet();
                        break;
                    case "signIn":
                        content = signIn();
                        break;
                    case "getVipMonthAddup":
                        content = getVipMonthAddup();
                        break;
                    case "getVipMonthCategoryAddup":
                        content = getVipMonthCategoryAddup();
                        break;
                    case "getEventMonthEventAddup":
                        content = getEventMonthEventAddup();
                        break;
                    case "getEventMonthCategoryAddup":
                        content = getEventMonthCategoryAddup();
                        break;
                    case "getEventAlbums":
                        content = getEventAlbums();
                        break;
                    case "getNewsList":
                        content = getNewsList();
                        break;
                    case "getEventOrders":
                        content = getEventOrders();
                        break;
                    case "getEventItemSales":
                        content = getEventItemSales();
                        break;
                    case "getEventList":
                        content = getEventList();
                        break;
                    case "getEventDetail":
                        content = getEventDetail();
                        break;
                    case "getEventSignUpUserInfo":
                        content = getEventSignUpUserInfo();
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

        #region messageGet
        /// <summary>
        /// messageGet
        /// </summary>
        public string messageGet()
        {
            string content = string.Empty;
            var respData = new messageGetRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<messageGetReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("messageGet: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "messageGet", reqObj, respData, null);

                IList<LEventsEntity> eventList = new List<LEventsEntity>();
                respData.content = new messageGetRespContentData();
                LEventsBLL lEventsBLL = new LEventsBLL(loggingSessionInfo);
                LEventsEntity eventQueryInfo = new LEventsEntity();
                var eventObj = lEventsBLL.GetMessageEventList(eventQueryInfo);
                eventList = eventObj.EventList;
                if (eventList == null && eventList.Count > 0)
                {
                    respData.content.vipCount = 1;
                }
                else
                {
                    respData.content.vipCount = eventList.Count;
                }
                //获取销售订单额
                InoutService inoutService = new InoutService(loggingSessionInfo);
                respData.content.eventSalesAmount = inoutService.GetHasTotalAmount();
                respData.content.toDoCount = 3;
                respData.content.imageURL = "1";
                respData.content.unitDesc = "2";
                respData.content.newsDesc = "3";

                respData.content.newVersion = new messageGetRespContentNewVersionData();
                respData.content.newVersion.isNewVersionAvailable = "0";
                respData.content.newVersion.message = "没有更新版本";
                respData.content.newVersion.canSkip = "1";
                respData.content.newVersion.updateUrl = "http://";

                respData.content.supportInfo = new messageGetRespContentSupportInfoData();
                respData.content.supportInfo.contactName = "技术支持人姓名.蔡蔡";
                respData.content.supportInfo.email = "caicai@jitmarketing.cn";
                respData.content.supportInfo.telephone = "400 000 800";
                respData.content.supportInfo.passwordResetHost = "11";

                respData.content.events = new List<messageGetRespContentEventData>();
                if (eventList != null || eventList.Count > 0)
                {
                    foreach (var item in eventList)
                    {
                        respData.content.events.Add(new messageGetRespContentEventData()
                        {
                            eventId = item.EventID,
                            imageURL = item.ImageURL,
                            eventTitle = item.Title,
                            eventAddress = item.Address,
                            eventDate = CPOS.Common.Utils.GetDateTime(item.CreateTime),
                            signUpCount = item.signUpCount,
                            salesAmount = item.salesAmount,
                            distanceDays = item.distanceDays
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

        public class messageGetRespData : Default.LowerRespData
        {
            public messageGetRespContentData content;
        }
        public class messageGetRespContentData
        {
            public int vipCount;
            public decimal eventSalesAmount;
            public int toDoCount;
            public string imageURL;
            public string unitDesc;
            public string newsDesc;
            public messageGetRespContentNewVersionData newVersion;
            public messageGetRespContentSupportInfoData supportInfo;
            public IList<messageGetRespContentEventData> events;
            public IList<messageGetRespContentNewsData> news;
        }
        public class messageGetRespContentNewVersionData
        {
            public string isNewVersionAvailable;
            public string message;
            public string canSkip;
            public string updateUrl;
        }
        public class messageGetRespContentSupportInfoData
        {
            public string contactName;
            public string email;
            public string telephone;
            public string passwordResetHost;
        }
        public class messageGetRespContentEventData
        {
            public string eventId;
            public string imageURL;
            public string eventTitle;
            public string eventAddress;
            public string eventDate;
            public int signUpCount;
            public decimal salesAmount;
            public int distanceDays;
        }
        public class messageGetRespContentNewsData
        {
            public string newsId;
            public string newsDesc;
        }
        public class messageGetReqData : Default.ReqData
        {
            public messageGetReqSpecialData special;
        }
        public class messageGetReqSpecialData
        {
            public string name;
        }
        #endregion

        #region signIn
        /// <summary>
        /// 用户登录接口
        /// </summary>
        public string signIn()
        {
            string content = string.Empty;
            var respData = new signInRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<signInReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("signIn: {0}", reqContent)
                });

                if (reqObj.special == null || reqObj.special.loginName == null || reqObj.special.password == null)
                {
                    respData.code = "103";
                    respData.description = "参数不能为空";
                    return respData.ToJSON();
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "signIn", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new signInRespContentData();
                cUserService service = new cUserService(loggingSessionInfo);
                var obj = service.GetSignInDashboard(new UserInfo() {
                    User_Name = reqObj.special.loginName,
                    User_Password = reqObj.special.password,
                    customer_id = "e703dbedadd943abacf864531decdac1",
                });

                if (obj == null || obj.User_Id == null)
                {
                    respData.code = "103";
                    respData.description = "未查询到用户信息";
                    return respData.ToJSON();
                }

                respData.content.userId = obj.User_Id;
                respData.content.userName = obj.User_Name;
                respData.content.imageUrl = obj.imageUrl;
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

        public class signInRespData : Default.LowerRespData
        {
            public signInRespContentData content;
        }
        public class signInRespContentData
        {
            public string userId;
            public string userName;
            public string imageUrl;
        }
        public class signInReqData : Default.ReqData
        {
            public signInReqSpecialData special;
        }
        public class signInReqSpecialData
        {
            public string loginName;
            public string password;
            public string isNewPushBind;
        }
        #endregion

        #region getVipMonthAddup
        /// <summary>
        /// 会员按月累计
        /// </summary>
        public string getVipMonthAddup()
        {
            string content = string.Empty;
            var respData = new getVipMonthAddupRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipMonthAddup: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getVipMonthAddupReqData>();
                reqObj = reqObj == null ? new getVipMonthAddupReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getVipMonthAddupReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getVipMonthAddup", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getVipMonthAddupRespContentData();
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                LVipAddupEntity queryInfo = new LVipAddupEntity();
                var obj = vipBLL.getVipMonthAddup(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.EntityList;
                respData.content.totalCount = obj.ICount;
                respData.content.vips = new List<getVipMonthAddupRespContentVipData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.vips.Add(new getVipMonthAddupRespContentVipData()
                        {
                            yearMonth = item.YearMonth,
                            vipAddupCount = item.VipAddupCount == null ? 0 : Convert.ToInt32(item.VipAddupCount),
                            vipMonthMoM = item.VipMonthMoM == null ? 0 : Convert.ToDecimal(item.VipMonthMoM),
                            displayIndex = item.displayIndex
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

        public class getVipMonthAddupRespData : Default.LowerRespData
        {
            public getVipMonthAddupRespContentData content;
        }
        public class getVipMonthAddupRespContentData
        {
            public IList<getVipMonthAddupRespContentVipData> vips;
            public int totalCount;
        }
        public class getVipMonthAddupRespContentVipData
        {
            public string yearMonth;
            public int vipAddupCount;
            public decimal vipMonthMoM;
            public Int64 displayIndex;
        }
        public class getVipMonthAddupReqData : Default.ReqData
        {
            public getVipMonthAddupReqSpecialData special;
        }
        public class getVipMonthAddupReqSpecialData
        {
            public int page;
            public int pageSize;
        }
        #endregion

        #region getVipMonthCategoryAddup
        /// <summary>
        /// 会员按月各项类别累计比较
        /// </summary>
        public string getVipMonthCategoryAddup()
        {
            string content = string.Empty;
            var respData = new getVipMonthCategoryAddupRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipMonthCategoryAddup: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getVipMonthCategoryAddupReqData>();
                reqObj = reqObj == null ? new getVipMonthCategoryAddupReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getVipMonthCategoryAddupReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getVipMonthCategoryAddup", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getVipMonthCategoryAddupRespContentData();
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                LVipAddupEntity queryInfo = new LVipAddupEntity();
                var obj = vipBLL.getVipMonthAddup(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.EntityList;
                respData.content.totalCount = obj.ICount;
                respData.content.vips = new List<getVipMonthCategoryAddupRespContentVipData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.vips.Add(new getVipMonthCategoryAddupRespContentVipData()
                        {
                            yearMonth = item.YearMonth,
                            vipAddupCount = item.VipAddupCount == null ? 0 : Convert.ToInt32(item.VipAddupCount),
                            vipMonthMoM = item.VipMonthMoM == null ? 0 : Convert.ToDecimal(item.VipMonthMoM),
                            vipVisitantCount = item.VipVisitantCount == null ? 0 : Convert.ToInt32(item.VipVisitantCount),
                            vipVisitantMonthMoM = item.VipVisitantMonthMoM == null ? 0 : Convert.ToDecimal(item.VipVisitantMonthMoM),
                            vipWeiXinAddupCount = item.VipWeiXinAddupCount == null ? 0 : Convert.ToInt32(item.VipWeiXinAddupCount),
                            vipWeiXinMonthMoM = item.VipWeiXinMonthMoM == null ? 0 : Convert.ToDecimal(item.VipWeiXinMonthMoM),
                            displayIndex = item.displayIndex
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

        public class getVipMonthCategoryAddupRespData : Default.LowerRespData
        {
            public getVipMonthCategoryAddupRespContentData content;
        }
        public class getVipMonthCategoryAddupRespContentData
        {
            public IList<getVipMonthCategoryAddupRespContentVipData> vips;
            public int totalCount;
        }
        public class getVipMonthCategoryAddupRespContentVipData
        {
            public string yearMonth;
            public int vipAddupCount;
            public decimal vipMonthMoM;
            public int vipVisitantCount;
            public decimal vipVisitantMonthMoM;
            public int vipWeiXinAddupCount;
            public decimal vipWeiXinMonthMoM;
            public Int64 displayIndex;
        }
        public class getVipMonthCategoryAddupReqData : Default.ReqData
        {
            public getVipMonthCategoryAddupReqSpecialData special;
        }
        public class getVipMonthCategoryAddupReqSpecialData
        {
            public int page;
            public int pageSize;
        }
        #endregion

        #region getEventMonthEventAddup
        /// <summary>
        /// 会员月活动销量统计
        /// </summary>
        public string getEventMonthEventAddup()
        {
            string content = string.Empty;
            var respData = new getEventMonthEventAddupRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventMonthEventAddup: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getEventMonthEventAddupReqData>();
                reqObj = reqObj == null ? new getEventMonthEventAddupReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventMonthEventAddupReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventMonthEventAddup", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventMonthEventAddupRespContentData();
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                LEventAddupEntity queryInfo = new LEventAddupEntity();
                var obj = vipBLL.getEventMonthEventAddup(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.EntityList;
                respData.content.totalCount = obj.ICount;
                respData.content.events = new List<getEventMonthEventAddupRespContentVipData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.events.Add(new getEventMonthEventAddupRespContentVipData()
                        {
                            yearMonth = item.YearMonth,
                            addupSalesTotal = item.AddupSalesTotal == null ? 0 : Convert.ToInt32(item.AddupSalesTotal),
                            monthSalesMoM = item.MonthSalesMoM == null ? 0 : Convert.ToDecimal(item.MonthSalesMoM),
                            displayIndex = item.displayIndex
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

        public class getEventMonthEventAddupRespData : Default.LowerRespData
        {
            public getEventMonthEventAddupRespContentData content;
        }
        public class getEventMonthEventAddupRespContentData
        {
            public IList<getEventMonthEventAddupRespContentVipData> events;
            public int totalCount;
        }
        public class getEventMonthEventAddupRespContentVipData
        {
            public string yearMonth;
            public decimal addupSalesTotal;
            public decimal monthSalesMoM;
            public Int64 displayIndex;
        }
        public class getEventMonthEventAddupReqData : Default.ReqData
        {
            public getEventMonthEventAddupReqSpecialData special;
        }
        public class getEventMonthEventAddupReqSpecialData
        {
            public int page;
            public int pageSize;
        }
        #endregion

        #region getEventMonthCategoryAddup
        /// <summary>
        /// 会员按月活动各项类别累计比较
        /// </summary>
        public string getEventMonthCategoryAddup()
        {
            string content = string.Empty;
            var respData = new getEventMonthCategoryAddupRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventMonthCategoryAddup: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getEventMonthCategoryAddupReqData>();
                reqObj = reqObj == null ? new getEventMonthCategoryAddupReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventMonthCategoryAddupReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventMonthCategoryAddup", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventMonthCategoryAddupRespContentData();
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                LEventAddupEntity queryInfo = new LEventAddupEntity();
                var obj = vipBLL.getEventMonthEventAddup(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.EntityList;
                respData.content.totalCount = obj.ICount;
                respData.content.events = new List<getEventMonthCategoryAddupRespContentVipData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.events.Add(new getEventMonthCategoryAddupRespContentVipData()
                        {
                            yearMonth = item.YearMonth,
                            addupSalesTotal = item.AddupSalesTotal == null ? 0 : Convert.ToInt32(item.AddupSalesTotal),
                            monthSalesMoM = item.MonthSalesMoM == null ? 0 : Convert.ToDecimal(item.MonthSalesMoM),
                            addupEventTotal = item.AddupEventTotal == null ? 0 : Convert.ToDecimal(item.AddupEventTotal),
                            monthEventMoM = item.MonthEventMoM == null ? 0 : Convert.ToDecimal(item.MonthEventMoM),
                            addupPutinTotal = item.AddupPutinTotal == null ? 0 : Convert.ToDecimal(item.AddupPutinTotal),
                            monthPutinMoM = item.MonthPutinMom == null ? 0 : Convert.ToDecimal(item.MonthPutinMom),
                            displayIndex = item.displayIndex
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

        public class getEventMonthCategoryAddupRespData : Default.LowerRespData
        {
            public getEventMonthCategoryAddupRespContentData content;
        }
        public class getEventMonthCategoryAddupRespContentData
        {
            public IList<getEventMonthCategoryAddupRespContentVipData> events;
            public int totalCount;
        }
        public class getEventMonthCategoryAddupRespContentVipData
        {
            public string yearMonth;
            public decimal addupSalesTotal;
            public decimal monthSalesMoM;
            public decimal addupEventTotal;
            public decimal monthEventMoM;
            public decimal addupPutinTotal;
            public decimal monthPutinMoM;
            public Int64 displayIndex;
        }
        public class getEventMonthCategoryAddupReqData : Default.ReqData
        {
            public getEventMonthCategoryAddupReqSpecialData special;
        }
        public class getEventMonthCategoryAddupReqSpecialData
        {
            public int page;
            public int pageSize;
        }
        #endregion

        #region getEventAlbums
        /// <summary>
        /// 活动相册集合
        /// </summary>
        public string getEventAlbums()
        {
            string content = string.Empty;
            var respData = new getEventAlbumsRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventAlbums: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getEventAlbumsReqData>();
                reqObj = reqObj == null ? new getEventAlbumsReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventAlbumsReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventAlbums", reqObj, respData, reqObj.special.ToJSON());

                string rootUrl = ConfigurationManager.AppSettings["website_url"].Trim();
                rootUrl = !rootUrl.EndsWith("/") ? rootUrl + "/" : rootUrl;
                rootUrl += "image_files/";

                respData.content = new getEventAlbumsRespContentData();
                LEventsBLL lEventsBLL = new LEventsBLL(loggingSessionInfo);
                ActivityMediaEntity queryInfo = new ActivityMediaEntity();
                queryInfo.ActivityID = reqObj.special.eventId;
                var obj = lEventsBLL.getEventAlbums(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.EntityList;
                respData.content.albumsCount = obj.ICount;
                respData.content.albums = new List<getEventAlbumsRespContentAlbumsData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.albums.Add(new getEventAlbumsRespContentAlbumsData()
                        {
                            albumsId = item.AttachmentID.ToString(),
                            albumsURL = rootUrl + "e703dbedadd943abacf864531decdac1/" + item.FileName,
                            displayIndex = item.displayIndex
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

        public class getEventAlbumsRespData : Default.LowerRespData
        {
            public getEventAlbumsRespContentData content;
        }
        public class getEventAlbumsRespContentData
        {
            public IList<getEventAlbumsRespContentAlbumsData> albums;
            public int albumsCount;
        }
        public class getEventAlbumsRespContentAlbumsData
        {
            public string albumsId;
            public string albumsURL;
            public Int64 displayIndex;
        }
        public class getEventAlbumsReqData : Default.ReqData
        {
            public getEventAlbumsReqSpecialData special;
        }
        public class getEventAlbumsReqSpecialData
        {
            public string eventId;
            public int page;
            public int pageSize;
        }
        #endregion

        #region getNewsList
        /// <summary>
        /// 消息列表
        /// </summary>
        public string getNewsList()
        {
            string content = string.Empty;
            var respData = new getNewsListRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getNewsList: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getNewsListReqData>();
                reqObj = reqObj == null ? new getNewsListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getNewsListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                    reqObj.special.parentNewsId = "";
                }

                if (reqObj.special.parentNewsId == null) { reqObj.special.parentNewsId = ""; }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getNewsList", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getNewsListRespContentData();
                LNewsBLL vipBLL = new LNewsBLL(loggingSessionInfo);
                LNewsEntity queryInfo = new LNewsEntity();
                queryInfo.ParentNewsId = reqObj.special.parentNewsId;
                var obj = vipBLL.getNewsList(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.EntityList;
                respData.content.newsCount = obj.ICount;
                respData.content.news = new List<getNewsListRespContentNewsData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.news.Add(new getNewsListRespContentNewsData()
                        {
                            newsId = item.NewsId,
                            content = item.Content,
                            pushUserName = item.CreateUserName,
                            pushImageUrl = item.ImageUrl,
                            pushTime = item.PublishTime == null ? null : item.PublishTime.ToString(),
                            displayIndex = item.displayIndex
                            ,newsLevel = item.NewsLevel
                            ,replyCount = item.ReplyCount
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

        public class getNewsListRespData : Default.LowerRespData
        {
            public getNewsListRespContentData content;
        }
        public class getNewsListRespContentData
        {
            public IList<getNewsListRespContentNewsData> news;
            public int newsCount;
        }
        public class getNewsListRespContentNewsData
        {
            public string newsId;
            public string content;
            public string pushUserName;
            public string pushImageUrl;
            public string pushTime;
            public Int64 displayIndex;
            public int replyCount;
            public int? newsLevel;
        }
        public class getNewsListReqData : Default.ReqData
        {
            public getNewsListReqSpecialData special;
        }
        public class getNewsListReqSpecialData
        {
            public int page;
            public int pageSize;
            public string parentNewsId;
        }
        #endregion

        #region getEventOrders
        /// <summary>
        /// 活动订单集合
        /// </summary>
        public string getEventOrders()
        {
            string content = string.Empty;
            var respData = new getEventOrdersRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventOrders: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getEventOrdersReqData>();
                reqObj = reqObj == null ? new getEventOrdersReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventOrdersReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventOrders", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventOrdersRespContentData();
                LEventsBLL vipBLL = new LEventsBLL(loggingSessionInfo);
                InoutInfo queryInfo = new InoutInfo();
                queryInfo.Field18 = reqObj.special.eventId;
                var obj = vipBLL.getEventOrders(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.InoutInfoList;
                respData.content.ordersCount = obj.ICount;
                respData.content.orders = new List<getEventOrdersRespContentOrderData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.orders.Add(new getEventOrdersRespContentOrderData()
                        {
                            orderId = item.order_id,
                            orderNo = item.order_no,
                            salesQty = item.total_qty,
                            salesTotal = item.total_retail,
                            displayIndex = item.displayIndex
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

        public class getEventOrdersRespData : Default.LowerRespData
        {
            public getEventOrdersRespContentData content;
        }
        public class getEventOrdersRespContentData
        {
            public IList<getEventOrdersRespContentOrderData> orders;
            public int ordersCount;
        }
        public class getEventOrdersRespContentOrderData
        {
            public string orderId;
            public string orderNo;
            public decimal salesQty;
            public decimal salesTotal;
            public Int64 displayIndex;
        }
        public class getEventOrdersReqData : Default.ReqData
        {
            public getEventOrdersReqSpecialData special;
        }
        public class getEventOrdersReqSpecialData
        {
            public int page;
            public int pageSize;
            public string eventId;
        }
        #endregion

        #region getEventItemSales
        /// <summary>
        /// 产品销量汇总
        /// </summary>
        public string getEventItemSales()
        {
            string content = string.Empty;
            var respData = new getEventItemSalesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventItemSales: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getEventItemSalesReqData>();
                reqObj = reqObj == null ? new getEventItemSalesReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventItemSalesReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventItemSales", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventItemSalesRespContentData();
                LEventsBLL vipBLL = new LEventsBLL(loggingSessionInfo);
                InoutInfo queryInfo = new InoutInfo();
                queryInfo.Field18 = reqObj.special.eventId;
                var obj = vipBLL.getEventItemSales(queryInfo, reqObj.special.page, reqObj.special.pageSize);
                var list = obj.InoutDetailList;
                respData.content.itemsCount = obj.ICount;
                respData.content.items = new List<getEventItemSalesRespContentItemData>();
                if (list != null || list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        respData.content.items.Add(new getEventItemSalesRespContentItemData()
                        {
                            itemId = item.item_id,
                            itemURL = item.imageUrl,
                            salesAmount = item.retail_amount,
                            salesQty = item.order_qty,
                            displayIndex = item.displayIndex
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

        public class getEventItemSalesRespData : Default.LowerRespData
        {
            public getEventItemSalesRespContentData content;
        }
        public class getEventItemSalesRespContentData
        {
            public IList<getEventItemSalesRespContentItemData> items;
            public int itemsCount;
        }
        public class getEventItemSalesRespContentItemData
        {
            public string itemId;
            public string itemURL;
            public decimal salesAmount;
            public decimal salesQty;
            public Int64 displayIndex;
        }
        public class getEventItemSalesReqData : Default.ReqData
        {
            public getEventItemSalesReqSpecialData special;
        }
        public class getEventItemSalesReqSpecialData
        {
            public int page;
            public int pageSize;
            public string eventId;
        }
        #endregion

        #region getEventList
        /// <summary>
        /// 按照年月与活动状态统计活动信息
        /// </summary>
        public string getEventList()
        {
            string content = string.Empty;
            var respData = new getEventListRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventListReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventList: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventList", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventListRespContentData();
                LEventsBLL vipBLL = new LEventsBLL(loggingSessionInfo);

                var ds = vipBLL.GetEventList(reqObj.special.yearMonth, reqObj.special.eventStatus, reqObj.special.page, reqObj.special.pageSize);
                respData.content.events = new List<getEventListRespContentEventData>();
                respData.content.eventCount = vipBLL.GetEventListCount(reqObj.special.yearMonth, reqObj.special.eventStatus).ToString();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string status = string.Empty;

                        if (Convert.ToDateTime(dr["BeginTime"]) > DateTime.Now)
                        {
                            status = "未开始";
                        }
                        else if (Convert.ToDateTime(dr["EndTime"]) > DateTime.Now)
                        {
                            status = "进行中";
                        }
                        else
                        {
                            status = "已结束";
                        }

                        string eventTime = dr["BeginTime"].ToString();
                        eventTime += (string.IsNullOrEmpty(dr["EndTime"].ToString())) ? "" : " -- " + dr["EndTime"].ToString();

                        respData.content.events.Add(new getEventListRespContentEventData()
                        {
                            displayIndex = dr["displayIndex"].ToString(),
                            eventTitle = dr["eventTitle"].ToString(),
                            eventDate = eventTime,
                            salesTotal = dr["salesTotal"].ToString(),
                            signUpCount = dr["signUpCount"].ToString(),
                            salesCount = dr["salesCount"].ToString(),
                            statusDesc = status,
                            eventId = dr["eventId"].ToString(),
                            eventStartTime = dr["eventStartTime"].ToString(),
                            imageURL = dr["imageURL"].ToString()
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

        public class getEventListRespData : Default.LowerRespData
        {
            public getEventListRespContentData content;
        }
        public class getEventListRespContentData
        {
            public string eventCount { get; set; }  //活动总数量
            public IList<getEventListRespContentEventData> events { get; set; }  //活动集合
        }
        public class getEventListRespContentEventData
        {
            public string displayIndex { get; set; }    //排序
            public string eventTitle { get; set; }      //活动名称
            public string eventDate { get; set; }       //活动时间(起始时间与结束时间拼接)
            public string salesTotal { get; set; }      //销售额
            public string signUpCount { get; set; }     //报名数量
            public string salesCount { get; set; }      //购买人数
            public string statusDesc { get; set; }      //状态(未开始，进行中，已结束)
            public string eventId { get; set; }         //活动标识
            public string eventStartTime { get; set; }  //活动举行时间
            public string imageURL { get; set; }        //图片链接地址
        }
        public class getEventListReqData : Default.ReqData
        {
            public getEventListReqSpecialData special;
        }
        public class getEventListReqSpecialData
        {
            public string yearMonth;    //活动年月(格式：2013-07)
            public string eventStatus;  //活动状态(1=已结束，0=未结束)
            public int page;            //页码
            public int pageSize;        //页面数量
        }
        #endregion

        #region getEventDetail
        /// <summary>
        /// 活动详情
        /// </summary>
        public string getEventDetail()
        {
            string content = string.Empty;
            var respData = new getEventDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventDetailReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventDetail: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventDetail", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventDetailRespContentData();
                LEventsBLL vipBLL = new LEventsBLL(loggingSessionInfo);

                var ds = vipBLL.GetEventDetail(reqObj.special.eventId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    string status = string.Empty;
                    if (Convert.ToDateTime(dr["BeginTime"]) > DateTime.Now)
                    {
                        status = "未开始";
                    }
                    else if (Convert.ToDateTime(dr["EndTime"]) > DateTime.Now)
                    {
                        status = "进行中";
                    }
                    else
                    {
                        status = "已结束";
                    }

                    string eventTime = dr["BeginTime"].ToString();
                    eventTime += (string.IsNullOrEmpty(dr["EndTime"].ToString())) ? "" : " -- " + dr["EndTime"].ToString();

                    respData.content.eventId = dr["eventId"].ToString();
                    respData.content.eventTitle = dr["eventTitle"].ToString();
                    respData.content.eventDate = eventTime;
                    respData.content.statusDesc = status;
                    respData.content.signUpCount = dr["signUpCount"].ToString();
                    respData.content.salesCount = dr["salesCount"].ToString();
                    respData.content.totalAmount = dr["totalAmount"].ToString();
                    respData.content.desc = dr["description"].ToString();
                    respData.content.imageURL = dr["imageURL"].ToString();
                    respData.content.address = dr["address"].ToString();
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

        public class getEventDetailRespData : Default.LowerRespData
        {
            public getEventDetailRespContentData content;
        }
        public class getEventDetailRespContentData
        {
            public string eventId { get; set; }         //活动标识
            public string eventTitle { get; set; }      //活动名称
            public string eventDate { get; set; }       //活动时间(起始时间与结束时间拼接)
            public string statusDesc { get; set; }      //状态(未开始，进行中，已结束)
            public string signUpCount { get; set; }     //报名人数
            public string salesCount { get; set; }      //购买人数
            public string totalAmount { get; set; }     //销售额
            public string desc { get; set; }            //活动简介
            public string imageURL { get; set; }
            public string address { get; set; }
        }
        public class getEventDetailReqData : Default.ReqData
        {
            public getEventDetailReqSpecialData special;
        }
        public class getEventDetailReqSpecialData
        {
            public string eventId;  //活动标识
        }
        #endregion

        #region getEventSignUpUserInfo
        /// <summary>
        /// 获取活动报名人员详细信息
        /// </summary>
        public string getEventSignUpUserInfo()
        {
            string content = string.Empty;
            var respData = new getEventSignUpUserInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventSignUpUserInfoReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventSignUpUserInfo: {0}", reqContent)
                });

                var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventSignUpUserInfo", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventSignUpUserInfoRespContentData();
                LEventsBLL vipBLL = new LEventsBLL(loggingSessionInfo);

                var ds = vipBLL.GetEventSignUpUserInfo(reqObj.special.eventId, reqObj.special.page, reqObj.special.pageSize);
                respData.content.signUpUsers = new List<getEventSignUpUserInfoRespContentEventData>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.signUpCount = vipBLL.GetEventSignUpUserInfoCount(reqObj.special.eventId).ToString();
                    respData.content.signUpUsers = DataTableToObject.ConvertToList<getEventSignUpUserInfoRespContentEventData>(ds.Tables[0]);
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

        public class getEventSignUpUserInfoRespData : Default.LowerRespData
        {
            public getEventSignUpUserInfoRespContentData content;
        }
        public class getEventSignUpUserInfoRespContentData
        {
            public string signUpCount { get; set; }  //报名总数量
            public IList<getEventSignUpUserInfoRespContentEventData> signUpUsers { get; set; }  //报名人员集合
        }
        public class getEventSignUpUserInfoRespContentEventData
        {
            public string vipId { get; set; }   //会员标识
            public string vipName { get; set; } //会员名称
            public string signUpTime { get; set; }    //报名时间
            public Int64 displayIndex { get; set; }       //排序
        }
        public class getEventSignUpUserInfoReqData : Default.ReqData
        {
            public getEventSignUpUserInfoReqSpecialData special;
        }
        public class getEventSignUpUserInfoReqSpecialData
        {
            public string eventId;      //活动标识
            public int page;            //页码
            public int pageSize;        //页面数量
        }
        #endregion
    }
}
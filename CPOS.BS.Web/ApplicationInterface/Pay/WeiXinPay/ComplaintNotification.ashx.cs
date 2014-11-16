using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Pay.WeiXinPay
{
    /// <summary>
    /// ComplaintNotification 的摘要说明
    /// </summary>
    public class ComplaintNotification : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string xmlStr = string.Empty;
                using (var stream = context.Request.InputStream)
                {
                    using (var rd = new StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        xmlStr = rd.ReadToEnd();
                        JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo()
                        {
                            Message = string.Format("用户维权:收到的通知信息:{0}", xmlStr)
                        });
                    }
                }
                var doc = new XmlDocument();
                doc.LoadXml(xmlStr);
                var dic = new Dictionary<string, string>();

                var ds = new DataSet();

                //遍历参数节点
                var list = doc.SelectNodes("xml/*");
                foreach (XmlNode item in list)
                {
                    if (item.Name != "PicInfo")
                    {
                        dic[item.Name] = item.InnerText;
                    }
                    else
                    {
                        //将图片读取到DS中
                        var stream = new StringReader("<PicInfo>" + item.InnerXml + "</PicInfo>");
                        var reader = new XmlTextReader(stream);
                        ds.ReadXml(reader);
                    }
                }

                #region 维权对象，不带图片

                #endregion

                #region 图片对象

                //遍历ds，向图片表插入数据

                #endregion

                var notify = dic.ToJSON().DeserializeJSONTo<WXRightOrdersEntity>();

                var customerWxMappingBll = new TCustomerWeiXinMappingBLL(GetBSLoggingSession("", ""));


                var customerId = customerWxMappingBll.GetCustomerIdByAppId(notify.AppId);
                if (customerId == "")
                {
                    throw new APIException("客户ID为空") { ErrorCode = 121 };
                }


                var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

                var bll = new WXRightOrdersBLL(currentUserInfo);
                //根据维权单号，查询是否有数据
                var entity = bll.QueryByEntity(new WXRightOrdersEntity()
                {
                    FeedBackId = notify.FeedBackId
                }, null);


                #region 验签

                ////支付中心URL
                //var url = System.Configuration.ConfigurationManager.AppSettings["paymentcenterUrl"];
                //url = "http://121.199.42.125:6002/DevPayTest.ashx";//测试,正式需删除此行
                //var customerID = "e703dbedadd943abacf864531decdac1";//先写死测试

                //var paraSign = new Dictionary<string, string>();
                //paraSign["openid"] = notify.OpenId;
                //paraSign["timestamp"] = notify.TimeStamp;

                //var request = new
                //{
                //    AppID = notify.AppId,
                //    ClientID = customerID,
                //    UserID = notify.OpenId,
                //    Parameters = paraSign
                //};
                //string content = string.Format("action=WXGetSign&request={0}", request.ToJSON());
                //string resStr = string.Empty;
                //try
                //{
                //    resStr = JIT.Utility.Web.HttpClient.PostQueryString(url, content);
                //}
                //catch (Exception ex)
                //{
                //    JIT.Utility.Log.Loggers.Exception(new Utility.Log.ExceptionLogInfo(ex));
                //    throw ex;
                //}
                //var dicGetSign = resStr.DeserializeJSONTo<Dictionary<string, object>>();
                //if (Convert.ToInt32(dicGetSign["ResultCode"]) < 100)
                //{
                //    var dicData = dicGetSign["Datas"].ToJSON().DeserializeJSONTo<Dictionary<string, string>>();
                //    var sign = dicData["Sign"].ToString();
                //    if (sign != notify.AppSignature)
                //        throw new Exception(string.Format("验签失败,原:{0}\t新:{1}", notify.AppSignature, sign));
                //}
                //else
                //{
                //    throw new Exception(string.Format("错误信息:{0}", dicGetSign["Message"]));
                //}

                #endregion


                var conEntity = bll.QueryByEntity(new WXRightOrdersEntity()
                {
                    FeedBackId = notify.FeedBackId
                }, null).FirstOrDefault();

                switch (notify.MsgType)
                {
                    case "request": //新的维权
                        //TODO:新增记录
                        JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo()
                        {
                            Message = string.Format("新增一条用户维权:{0}", notify.ToJSON())
                        });

                        if (entity != null)
                        {
                            //删除原有的维权单
                            bll.Delete(entity);
                        }
                        notify.Status = 10;
                        var rightOrderId = Guid.NewGuid();
                        notify.RightOrdersId = rightOrderId;
                        notify.CustomerId = customerId;
                        bll.Create(notify);
                        var objectImages = new ObjectImagesEntity();
                        var objectImageList = new List<ObjectImagesEntity>();
                        var imageBll = new ObjectImagesBLL(currentUserInfo);
                        int i = 0;
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            i++;
                            objectImages.ImageURL = row[0].ToString();
                            objectImages.ObjectId = Convert.ToString(rightOrderId);
                            objectImages.ImageId = Convert.ToString(Guid.NewGuid());
                            objectImages.DisplayIndex = i;
                            objectImages.CustomerId = customerId;
                            //将维权单的url插入到图片表中
                            imageBll.Create(objectImages);
                        }

                        //var feedBackPara = new
                        //{
                        //    PayChannelID = 9,
                        //    FeedBackID = notify.FeedBackId,
                        //    OpenID = notify.OpenId
                        //};
                        //var feedBackRequest = new
                        //{
                        //    AppID = notify.AppId,
                        //    ClientID = customerID,
                        //    UserID = notify.OpenId,
                        //    Parameters = paraSign
                        //};
                        //content = string.Format("action=WXGetUpdateFeedBackUrl&request={0}", feedBackRequest.ToJSON());
                        //resStr = string.Empty;
                        //try
                        //{
                        //    resStr = JIT.Utility.Web.HttpClient.PostQueryString(url, content);
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw ex;
                        //}

                        //var dicFeedBack = resStr.DeserializeJSONTo<Dictionary<string, object>>();
                        //if (Convert.ToInt32(dicFeedBack["ResultCode"]) < 100)
                        //{
                        //    var dicData = dicFeedBack["Datas"].ToJSON().DeserializeJSONTo<Dictionary<string, string>>();
                        //    var feedBackUrl = dicData["Url"].ToString();
                        //    string resStrFeedBack = string.Empty;
                        //    try
                        //    {
                        //        resStrFeedBack = JIT.Utility.Web.HttpClient.GetQueryString(feedBackUrl);
                        //        dicFeedBack = resStrFeedBack.DeserializeJSONTo<Dictionary<string, object>>();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //}
                        //else
                        //{
                        //    throw new Exception(string.Format("错误信息:{0}", dicGetSign["Message"]));
                        //}
                        break;
                    case "confirm": //客户同意解决
                        //TODO:对已有的记录Update
                        JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo()
                        {
                            Message = string.Format("客户同意维权解决:{0}", notify.ToJSON())
                        });


                        if (conEntity != null)
                        {
                            conEntity.Status = 50;
                            //conEntity.MsgType = "confirm";
                            //conEntity.Reason = notify.Reason;
                            conEntity.ConfirmReason = notify.Reason + "-----" + notify.MsgType;
                            conEntity.TimeStamp = notify.TimeStamp;

                            bll.Update(conEntity);
                        }
                        break;
                    case "reject": //客户拒绝解决
                        //TODO:对已有的记录Update,同时如何处理??
                        JIT.Utility.Log.Loggers.Debug(new Utility.Log.DebugLogInfo()
                        {
                            Message = string.Format("客户拒绝维权解决:{0}", notify.ToJSON())
                        });

                        if (conEntity != null)
                        {
                            conEntity.Status = 60;
                           // conEntity.MsgType = "reject";
                           // conEntity.Reason = notify.Reason;
                            conEntity.ConfirmReason = notify.Reason + "-----" + notify.MsgType;
                            conEntity.TimeStamp = notify.TimeStamp;

                            bll.Update(conEntity);
                        }
                        break;
                    default:
                        break;
                }

                #region 向表中记录调用的微信接口

                var wxInterfaceLogBll = new WXInterfaceLogBLL(currentUserInfo);
                var wxInterfaceLogEntity = new WXInterfaceLogEntity();
                wxInterfaceLogEntity.LogId = Guid.NewGuid();
                wxInterfaceLogEntity.InterfaceUrl = "微信公众号配置的维权URL";
                wxInterfaceLogEntity.AppId = notify.AppId;
                wxInterfaceLogEntity.OpenId = notify.OpenId;
                wxInterfaceLogEntity.RequestParam = notify.ToJSON();
                wxInterfaceLogEntity.IsSuccess = 1;
                wxInterfaceLogBll.Create(wxInterfaceLogEntity);

                #endregion
            }
            catch (Exception ex)
            {
                JIT.Utility.Log.Loggers.Exception(new Utility.Log.ExceptionLogInfo(ex));
                context.Response.Write(ex);
            }
        }

        public static LoggingSessionInfo GetBSLoggingSession(string customerId, string userId)
        {
            if (userId == null || userId == string.Empty) userId = "1";
            //string conn = GetCustomerConn(customerId);
            var conn = ConfigurationManager.AppSettings["Conn_ap"];
            
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new JIT.CPOS.BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = conn;

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }

        #region GetCustomerConn
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
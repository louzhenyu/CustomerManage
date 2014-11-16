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

namespace JIT.CPOS.Web.iAlumni.Interface
{
    public partial class WapData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                switch (dataType)
                {
                    case "setiAlumniOrderInfo":        // 5.1 提交订单
                        content = setiAlumniOrderInfo();
                        break;
                    case "setWeixinWallInfo":
                        content = setWeixinWallInfo();//5.2 提交微信墙聊天内容
                        break;
                    case "getWeixinWallInfo":
                        content = getWeixinWallInfo();//6.1
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

        #region 4.13 提交订单
        public string setiAlumniOrderInfo()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                
                //reqContent = "{\"common\":{\\\"weiXinId\\\":\\\"gh_bf70d7900c28\\\",\\\"openId\\\":\\\"o8Y7Ejm0kL4QB8-h_Z0Bncl619v4\\\"},\"special\":{\\\"salesPrice\\\":\\\"235\\\",\\\"stdPrice\\\":\\\"250\\\",\\\"orderCode\\\":\\\"DO2013080800001\\\",\\\"totalAmount\\\":\\\"470\\\",\\\"deliveryName\\\":\\\"�ŵ����\\\",\\\"qty\\\":\\\"2\\\",\\\"deliveryRemark\\\":\\\"\\\",\\\"orderId\\\":\\\"E370FC0B39A77078E044005056BE1CB0\\\"}}";
                //reqContent = Server.UrlDecode(reqContent);
                //reqContent = HttpUtility.UrlDecode(reqContent);
                //reqContent = reqContent.Replace("\\\"","\"");
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setiAlumniOrderInfo: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;

                
                #region
                if (OpenID == null || OpenID.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "OpenID不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.skuId == null || reqObj.special.skuId.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "skuId不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.orderId == null || reqObj.special.orderId.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "订单标识不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.orderCode == null || reqObj.special.orderCode.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "订单号码不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.qty == null || reqObj.special.qty.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "数量不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.salesPrice == null || reqObj.special.salesPrice.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "销售价格不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.stdPrice == null || reqObj.special.stdPrice.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "标准价格不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.totalAmount == null || reqObj.special.totalAmount.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "总价不能为空";
                    return respData.ToJSON();
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession("29E11BDC6DAC439896958CC6866FF64E", "1");
                //Default.WriteLog(loggingSessionInfo, "setiAlumniOrderInfo", null, respData, reqObj.special.ToJSON());

                InoutService inoutService = new InoutService(loggingSessionInfo);
                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = inoutService.SetiAlumniWapPosInoutInfo(reqObj.special.skuId.Trim()
                                                            , reqObj.special.orderId.Trim()
                                                            , OpenID.Trim()
                                                            , WeiXin.Trim()
                                                            , reqObj.special.orderCode.Trim()
                                                            , reqObj.special.qty.Trim()
                                                            , reqObj.special.stdPrice.Trim()
                                                            , reqObj.special.salesPrice.Trim()
                                                            , reqObj.special.totalAmount.Trim()
                                                            , reqObj.special.deliveryName.Trim()
                                                            , reqObj.special.deliveryRemark.Trim()
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

        public class setOrderInfoReqData : Default.ReqiAlumniData
        {
            public setOrderInfoReqSpecialData special;
        }
        public class setOrderInfoReqSpecialData
        {
            public string orderId;		//订单标识
            public string skuId;	    //sku标识
            public string orderCode;	    //订单号码
            public string qty;		    //数量
            public string salesPrice;	//销售价格
            public string stdPrice;	    //价格
            public string totalAmount;  //销售总价
            public string deliveryName; //配送方式
            public string deliveryRemark;//配送备注
        }
        #endregion

        #region 5.2 提交微信墙的聊天内容 （提供给钱志，为了20130815演示）
        public string setWeixinWallInfo()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                //reqContent = "{\"common\":{\\\"weiXinId\\\":\\\"gh_bf70d7900c28\\\",\\\"openId\\\":\\\"o8Y7Ejm0kL4QB8-h_Z0Bncl619v4\\\"},\"special\":{\\\"salesPrice\\\":\\\"235\\\",\\\"stdPrice\\\":\\\"250\\\",\\\"orderCode\\\":\\\"DO2013080800001\\\",\\\"totalAmount\\\":\\\"470\\\",\\\"deliveryName\\\":\\\"�ŵ����\\\",\\\"qty\\\":\\\"2\\\",\\\"deliveryRemark\\\":\\\"\\\",\\\"orderId\\\":\\\"E370FC0B39A77078E044005056BE1CB0\\\"}}";
                //reqContent = Server.UrlDecode(reqContent);
                //reqContent = HttpUtility.UrlDecode(reqContent);
                //reqContent = reqContent.Replace("\\\"","\"");
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setWeixinWallInfo: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<setWeiXinWallInfoReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string customerId = reqObj.common.customerId;


                #region
                if (OpenID == null || OpenID.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "OpenID不能为空";
                    return respData.ToJSON();
                }
                if (WeiXin == null || WeiXin.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "WeiXin不能为空";
                    return respData.ToJSON();
                }
                if (customerId == null || customerId.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.eventKeyword == null || reqObj.special.eventKeyword.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "eventKeyword不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.content == null || reqObj.special.content.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "content不能为空";
                    return respData.ToJSON();
                }
               
                
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                //Default.WriteLog(loggingSessionInfo, "setiAlumniOrderInfo", null, respData, reqObj.special.ToJSON());

                WeixinWallBLL server = new WeixinWallBLL(loggingSessionInfo);
                string strError = string.Empty;
                string strMsg = string.Empty;
                WeixinWallEntity wallInfo = new WeixinWallEntity();
                wallInfo.WallId = BaseService.NewGuidPub();
                wallInfo.EventKeyword = reqObj.special.eventKeyword.Trim();
                wallInfo.Content = reqObj.special.content.Trim();
                wallInfo.OpenId = OpenID.Trim();
                wallInfo.WeiXinId = WeiXin.Trim();
                wallInfo.CreateBy = OpenID.Trim();
                wallInfo.LastUpdateBy = OpenID.Trim();
                wallInfo.HasReader = 0;
                server.Create(wallInfo);
                respData.code = "200";

                respData.description = "成功";
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

        public class setWeiXinWallInfoReqData : Default.ReqiAlumniData
        {
            public setWeiXinWallInfoReqSpecialData special;
        }
        public class setWeiXinWallInfoReqSpecialData
        {
            public string eventKeyword;		//活动关键字
            public string content;	    //聊天内容
        }
        #endregion

        #region 6.1 获取微信墙的未读取的聊天内容 （提供给汤武强，为了20130815演示）
        public string getWeixinWallInfo()
        {
            string content = string.Empty;
            var respData = new getWeixinWallInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getWeixinWallInfo: {0}", reqContent)
                });
                var reqObj = reqContent.DeserializeJSONTo<getWeixinWallInfoReqData>();

                string customerId = reqObj.common.customerId;
                #region
              
                if (customerId == null || customerId.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special.eventKeyword == null || reqObj.special.eventKeyword.Trim().Equals(""))
                {
                    respData.code = "110";
                    respData.description = "eventKeyword不能为空";
                    return respData.ToJSON();
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                //Default.WriteLog(loggingSessionInfo, "setiAlumniOrderInfo", reqObj, respData, reqObj.special.ToJSON());

                WeixinWallBLL server = new WeixinWallBLL(loggingSessionInfo);
                string strError = string.Empty;
                string strMsg = string.Empty;
                WeixinWallEntity wallInfo = new WeixinWallEntity();

                wallInfo = server.GetWeiXinWall(reqObj.special.eventKeyword.Trim());
                if (wallInfo == null)
                {
                    respData.code = "200";
                    respData.description = "没有存在的记录";
                }
                else
                {
                    var contentObj = new getWeixinWallInfoRespContentData();
                    contentObj.wallsCount = wallInfo.WallsCount.ToString();
                    if ( wallInfo.WallsCount > 0)
                    {
                        contentObj.walls = new List<getWeixinWallEntity>();
                        IList<WeixinWallEntity> wallList = new List<WeixinWallEntity>();
                        foreach (WeixinWallEntity info in wallInfo.WallList)
                        {
                            getWeixinWallEntity reqInfo = new getWeixinWallEntity();
                            reqInfo.displayIndex = info.DisplayIndex.ToString();
                            reqInfo.content = info.Content;
                            reqInfo.createTime = info.CreateTime.ToString();
                            reqInfo.wallId = info.WallId;
                            reqInfo.userName = info.UserName;
                            reqInfo.imageUrl = info.ImageUrl;
                            contentObj.walls.Add(reqInfo);
                        }
                    }
                    respData.content = contentObj;
                    respData.code = "200";
                    respData.description = "成功";
                }

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

        public class getWeixinWallInfoReqData : Default.ReqiAlumniData
        {
            public getWeixinWallInfoReqSpecialData special;
        }
        public class getWeixinWallInfoReqSpecialData
        {
            public string eventKeyword;		//活动关键字
        }

        public class getWeixinWallInfoRespData : Default.LowerRespData
        {
            public getWeixinWallInfoRespContentData content;
        }
        public class getWeixinWallInfoRespContentData
        {
            public string wallsCount;   //微信墙聊天记录总数量
            public IList<getWeixinWallEntity> walls;
        }

        public class getWeixinWallEntity
        {
            public string wallId;
            public string createTime;
            public string content;
            public string userName;
            public string imageUrl;
            public string displayIndex;
        }
        #endregion
    }
}
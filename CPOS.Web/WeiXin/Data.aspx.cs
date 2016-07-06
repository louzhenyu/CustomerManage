using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using JIT.CPOS.Common;
using System.Text;
using System.Configuration;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using ItemService = JIT.CPOS.BS.BLL.ItemService;
using UnitService = JIT.CPOS.BS.BLL.UnitService;
using JIT.CPOS.BS.BLL.CS;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Contact;

namespace JIT.CPOS.Web.WeiXin
{
    public partial class Data : System.Web.UI.Page
    {
        public string webUrl = ConfigurationManager.AppSettings["website_url"];

        protected void Page_Load(object sender, EventArgs e)
        {
            //SetPushQRCode("oxbbcjnB7yQlWGb-fdWRKagGdWgo", "e703dbedadd943abacf864531decdac1", "3f9e2cb3c74a4c76b29196af5ee04c01", "1", "gh_e2b2da1e6edf");
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["dataType"].ToString().Trim();
                switch (dataType)
                {
                    case "SignIn":      //关注
                        content = SetSignIn();
                        break;
                    case "GetCouponImageText":
                        content = GetCouponImageText();
                        break;
                    case "GetVIPImageCommentShowList": //获取靓图欣赏列表
                        content = GetVIPImageCommentShowList();
                        break;
                    case "GetStoreInfo": // 获取门店信息
                        content = GetStoreInfo();
                        break;
                    case "ScanQrcode":  //扫描门店二维码
                        //content = ScanQrcode();
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

        #region 用户关注
        /// <summary>
        /// 用户关注
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public string SetSignIn()
        {

            Random rad = new Random();
            int iRad = rad.Next(1000, 100000);
            string content = string.Empty;
            var respData = new SetSignInRespData();
            string VipIdTmp = string.Empty;

            try
            {
                string OpenID = Request["openID"];
                string City = Request["city"] == null ? "" : HttpUtility.UrlDecode(Request["city"]);
                string Gender = Request["gender"];
                string VipName = Request["vipName"] == null ? "" : HttpUtility.UrlDecode(Request["vipName"]);
                string IsShow = Request["isShow"];
                string qrcode = Request["qrcode"]; // 二维码,这个是组成了一个带openid和weixinid的链接****
                string WeiXin = Request["weixin_id"];
                string qrcode_id = Request["qrcode_id"];    //门店转换id,二维码的key也传过来了 

                string headimgurl = Request["headimgurl"];
                string unionid = Request["unionid"]; //多个公众账号唯一标识
                //这里加密，只能取当前的了
                LoggingSessionInfo loggingSessionInfo = BaseService.GetWeixinLoggingSession(WeiXin);
                var application = new WApplicationInterfaceBLL(loggingSessionInfo);
                var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = WeiXin }, null);

                //下面的取开发平台的放在解密的代码里，因为明文用不到
                //如果公众帐号授权给第三方平台管理了
                var wAppEntity = appEntitys[0];
                if (!string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid))
                {
                    DataSet WXOpenOAuthDs = application.GetWXOpenOAuth(wAppEntity.OpenOAuthAppid);
                    if (WXOpenOAuthDs.Tables != null && WXOpenOAuthDs.Tables != null && WXOpenOAuthDs.Tables.Count != 0 && WXOpenOAuthDs.Tables[0].Rows.Count != 0)
                    {
                        DataRow WXOpenOAuthDr = WXOpenOAuthDs.Tables[0].Rows[0];
                        wAppEntity.OpenToken = WXOpenOAuthDr["Token"] == null ? "" : WXOpenOAuthDr["Token"].ToString();
                        wAppEntity.OpenPrevEncodingAESKey = WXOpenOAuthDr["PrevEncodingAESKey"] == null ? "" : WXOpenOAuthDr["PrevEncodingAESKey"].ToString();
                        wAppEntity.OpenCurrentEncodingAESKey = WXOpenOAuthDr["EncodingAESKey"] == null ? "" : WXOpenOAuthDr["EncodingAESKey"].ToString();
                        wAppEntity.OpenAppID = WXOpenOAuthDr["Appid"] == null ? "" : WXOpenOAuthDr["Appid"].ToString();
                    }
                }

                //生成时间戳
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                int timestamp = (int)(DateTime.Now - startTime).TotalSeconds;
                //生成随机数
                Random ran = new Random();
                int RandKey = ran.Next(10000, 99999);
                int RandKey2 = ran.Next(10000, 99999);
                string nonce = RandKey.ToString() + RandKey2.ToString();

                var requestParams = new RequestParams()
                {
                    OpenId = OpenID,
                    WeixinId = WeiXin,
                    //MsgType = msgType,
                    //XmlNode = xn,
                    LoggingSessionInfo = BaseService.GetWeixinLoggingSession(WeiXin),
                    TrueEncodingAESKey = string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid) ? wAppEntity.CurrentEncodingAESKey : wAppEntity.OpenCurrentEncodingAESKey, //appEntitys[0].CurrentEncodingAESKey,
                    Token = string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid) ? wAppEntity.Token : wAppEntity.OpenToken,//如果授权给公众平台了，就用公众平台的token
                    AppID = string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid) ? wAppEntity.AppID : wAppEntity.OpenAppID,
                    EncryptType = wAppEntity.EncryptType == null ? 0 : (int)wAppEntity.EncryptType,
                    Timestamp = timestamp.ToString(),
                    Nonce = nonce
                };

                #region 1.头像处理
                if (headimgurl == null || headimgurl.Equals(""))
                {

                }
                else
                {
                    CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                    string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];
                    headimgurl = downloadServer.DownloadFile(headimgurl, downloadImageUrl);
                }
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "OpenID:{0}, City:{1}, Gender:{2}, VipName:{3}, IsShow:{4}, qrcode:{5},weixin_id:{6}",
                        OpenID, City, Gender, VipName, IsShow, qrcode, WeiXin)
                });
                #endregion
                var apLoggingSessionInfo = Default.GetAPLoggingSession("");

                #region 2.管理平台处理日志
                VipShowLogEntity vipShowLogInfo = new VipShowLogEntity();
                vipShowLogInfo.VipLogID = System.Guid.NewGuid().ToString().Replace("-", "");
                vipShowLogInfo.OpenID = OpenID;
                vipShowLogInfo.City = qrcode_id;
                vipShowLogInfo.IsShow = Convert.ToInt32(IsShow);
                vipShowLogInfo.WeiXin = WeiXin;
                VipShowLogBLL vipShowLogBll = new VipShowLogBLL(apLoggingSessionInfo);
                vipShowLogInfo.VipName = VipName;
                vipShowLogInfo.Language = iRad.ToString();
                vipShowLogBll.Create(vipShowLogInfo);
                #endregion



                VipBLL vipService = new VipBLL(apLoggingSessionInfo);
                var vipCode = vipService.GetVipCode();//获取新的vipcode
                var vipCodeShort = vipCode.Substring(4).Insert(3, " ");


                VipEntity vipQueryInfo = new VipEntity();
                VipEntity vipInfo = new VipEntity();
              
                #region --判断是新建还是修改管理平台
                if (true)
                {
                    #region 插入或修改管理平台
                    vipQueryInfo.WeiXinUserId = OpenID;
                    vipQueryInfo.WeiXin = WeiXin;
                    try
                    {
                        if (IsShow.Equals("1")) //关注，取消关注不要修改vip表
                        {
                            vipInfo.WeiXinUserId = OpenID;
                            vipInfo.WeiXin = WeiXin;
                            vipInfo.UnionID = unionid;
                            
                            /**  //不做为查询条件
                            vipInfo.City = City;
                            if (Gender != string.Empty) vipInfo.Gender = Convert.ToInt32(Gender);
                            vipInfo.VipName = VipName;
                            //vipInfo.CouponInfo = qrcode;//CouponInfo已作为会籍店使用                         
                            vipInfo.HeadImgUrl = headimgurl;
                             **/
                            var vipObj = vipService.QueryByEntity(vipQueryInfo, null);

                            vipInfo.City = City;
                            if (Gender != string.Empty) vipInfo.Gender = Convert.ToInt32(Gender);
                            vipInfo.VipName = VipName;
                            //vipInfo.CouponInfo = qrcode;//CouponInfo已作为会籍店使用                         
                            vipInfo.HeadImgUrl = headimgurl;
                            if (vipObj == null || vipObj.Length == 0 || vipObj[0] == null)
                            {
                                //couponInfo字段保存总部门店ID
                                UnitService unitServer = new UnitService(loggingSessionInfo);
                                vipInfo.CouponInfo = unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识
                                vipInfo.VipSourceId = "3";//是否需要把这个去掉，因为VipSourceId=13也会走到这里
                                vipInfo.Status = 1;
                                vipInfo.VIPID = Guid.NewGuid().ToString().Replace("-", "");
                                vipInfo.VipCode = vipCode;
                                vipInfo.VipPasswrod = "e10adc3949ba59abbe56e057f20f883e";
                                //根据UnionID判断已关注其他绑定公众号
                                if (!string.IsNullOrEmpty(vipInfo.UnionID))
                                {
                                    var vipUnionInfo = vipService.QueryByEntity(new VipEntity() { UnionID = vipInfo.UnionID }, null).FirstOrDefault();
                                    if (vipUnionInfo == null)//首次关注
                                    {
                                        vipService.Create(vipInfo);                                        
                                    }
                                    //已关注了绑定公众号中的其他公众号
                                    WXUserInfoBLL wxUserInfoBLL = new WXUserInfoBLL(loggingSessionInfo);
                                    var wxUserInfoEntity = new WXUserInfoEntity()
                                    {
                                        VipID = vipUnionInfo == null ? vipInfo.VIPID : vipUnionInfo.VIPID,
                                        WeiXin = vipInfo.WeiXin,
                                        WeiXinUserID = vipInfo.WeiXinUserId,
                                        UnionID = vipInfo.UnionID,
                                        CustomerID = loggingSessionInfo.ClientID
                                    };
                                    wxUserInfoBLL.Create(wxUserInfoEntity);
                                }
                                else //未绑定多个公众号
                                {
                                    vipService.Create(vipInfo);
                                    
                                }

                            }
                            else
                            {
                                vipInfo.VIPID = vipObj[0].VIPID;
                                if (vipObj[0].VipCode == null || vipObj[0].VipCode.Equals(""))
                                {
                                    vipInfo.VipCode = vipCode;
                                }
                                //vipInfo.CreateTime = System.DateTime.Now;
                                if (headimgurl != null && !headimgurl.Equals(""))
                                {
                                    vipInfo.HeadImgUrl = headimgurl;
                                }
                                //vipInfo.VipCode = null;
                                vipService.Update(vipInfo, false);//修改
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "扫描插入管理平台出错：" + ex1.ToString()
                        });
                    }

                    #endregion
                }
                #endregion             

                string unitName = string.Empty;
                string customerIdUnoin = string.Empty;

                WMenuBLL menuServer = new WMenuBLL(loggingSessionInfo);
                customerIdUnoin = menuServer.GetCustomerIdByWx(WeiXin);
                #region --在商户系统中处理
                //插入商户业务系统
                customerIdUnoin = SetCustomerVipInfo(apLoggingSessionInfo, WeiXin, OpenID, City, IsShow, VipName, Gender, qrcode, null, null, headimgurl, qrcode_id, unionid, out VipIdTmp);
                #endregion

                //else //固定二维码
                //{

                //customerIdUnoin = menuServer.GetCustomerIdByWx(WeiXin);
                //SetPushQuestionnaire(OpenID, customerIdUnoin, VipIdTmp, unitName, qrcode_id, WeiXin, iRad);

                //}
                //张伟封装的固定二维码扫描
                customerIdUnoin = menuServer.GetCustomerIdByWx(WeiXin);

                //update by wzq 20140805 cancel old push qrcode info
                //   SetPushQRCode(OpenID, customerIdUnoin, VipIdTmp, qrcode_id, WeiXin);
                respData.QRVipCode = vipInfo.QRVipCode;
                respData.CouponURL = vipInfo.CouponURL;
                respData.Data = vipInfo.QRVipCode;

                //update by wzq 20140805 cancel old push qrcode info
                loggingSessionInfo = Default.GetBSLoggingSession(customerIdUnoin, "1");
                var eventsBll = new LEventsBLL(loggingSessionInfo);

           
                //处理扫描静态二维码事件****(这里的静态二维码事件返回的信息不会回到微信服务器，只是能返回到我们自己的服务器recivemsg.aspx上)
              //  eventsBll.SendQrCodeWxMessage(loggingSessionInfo, customerIdUnoin, WeiXin, qrcode_id, OpenID, HttpContext.Current, requestParams);

                #region 云店处理 Add Henry
                var wAppBll = new WApplicationInterfaceBLL(loggingSessionInfo);       //实例化微信公众号信息BLL
                var qrCodeBll = new WQRCodeManagerBLL(loggingSessionInfo);            //实例化门店二维码BLL
                string cloudCustomerId = ConfigurationManager.AppSettings["CloudCustomerId"].ToString();    //读取云店客户ID
                //获取云店微信号信息
                var appInterfaceEntity = wAppBll.QueryByEntity(new WApplicationInterfaceEntity() { CustomerId = cloudCustomerId }, null).FirstOrDefault();
                if (appInterfaceEntity != null)
                {
                    //根据QRCode和公众号的ApplicationID获取商户的门店ID
                    var qrCodeEntity = qrCodeBll.QueryByEntity(new WQRCodeManagerEntity() { ApplicationId = appInterfaceEntity.ApplicationId, QRCode = qrcode }, null).FirstOrDefault();
                    if (qrCodeEntity != null)
                    {
                        //根据获取的门店ID和客户ID商户的会员信息

                        //根据OpenID判断商户的此用户信息是否存在


                    }
                }


                #endregion

                
            }
            catch (Exception ex)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class SetSignInRespData : Default.RespData
        {
            public string QRVipCode;
            public string CouponURL;
        }
        #endregion

        #region 获取二维码图片字符串
        /// <summary>
        /// 获取二维码图片字符串
        /// </summary>
        public string GetCouponImageText()
        {
            var currentUser = Default.GetLoggingSession();
            string content = string.Empty;
            string openId = Request["openId"];
            string imgUrl = Request["imgUrl"];

            //openId = "o8Y7Ejv3jR5fEkneCNu6N1_TIYIM";
            //imgUrl = "http://mmsns.qpic.cn/mmsns/7258E1JgBM8zfgBfsoNXWMhHSNVsyhItPdibaKe1uYjNS1vEHP3ZtTw/0";

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("openId:{0}, imgUrl:{1}", openId, imgUrl)
            });

            var respData = new Default.RespData();
            if (openId == null || openId.Trim().Length == 0 ||
                imgUrl == null || imgUrl.Trim().Length == 0)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求参数不能为空";
                return respData.ToJSON();
            }

            System.Net.WebRequest req = System.Net.WebRequest.Create(imgUrl);
            System.Net.WebResponse response = req.GetResponse();
            System.Drawing.Image imgSrc = null;
            using (Stream stream = response.GetResponseStream())
            {
                imgSrc = System.Drawing.Image.FromStream(stream);
                stream.Close();
            }
            Bitmap bmp = new Bitmap(imgSrc);

            QRCodeDecoder decoder = new QRCodeDecoder();
            content = decoder.decode(new QRCodeBitmapImage(bmp), Encoding.UTF8);
            // "http://xxxx/Member.aspx?weixin_id=gh_bf70d7900c28&open_id=o8Y7Ejv3jR5fEkneCNu6N1_TIYIM"

            string qrcode = content;
            Uri qrcodeUri = new Uri(qrcode);
            string highOpenId = HttpUtility.ParseQueryString(qrcodeUri.Query).Get("open_id");
            if (highOpenId == null || highOpenId.Trim().Length == 0 || openId == highOpenId)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "无效的优惠券";
                return respData.ToJSON();
            }
            #region
            // vip
            VipBLL vipBLL = new VipBLL(currentUser);
            IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(currentUser);
            VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(currentUser);
            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(currentUser);

            string vipId = null, highVipId = null;
            if (true)
            {
                VipEntity vipIdData = null;
                var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = openId
                }, null);
                if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null ||
                    vipIdDataList[0].VIPID == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到Vip会员";
                    return respData.ToJSON();
                }
                else
                {
                    vipIdData = vipIdDataList[0];
                    vipId = vipIdData.VIPID;
                }
            }
            if (true)
            {
                VipEntity vipIdData = null;
                var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = highOpenId
                }, null);
                if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null ||
                    vipIdDataList[0].VIPID == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到上级Vip会员";
                    return respData.ToJSON();
                }
                else
                {
                    vipIdData = vipIdDataList[0];
                    highVipId = vipIdData.VIPID;
                }
            }

            // SysIntegralSource: 8
            string integralSourceId = "8";
            int integralValue = 0;
            if (true)
            {
                IntegralRuleEntity integralRuleData = null;
                var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                {
                    IntegralSourceID = integralSourceId
                }, null);
                if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到积分规则";
                    return respData.ToJSON();
                }
                else
                {
                    integralRuleData = integralRuleDataList[0];
                    integralValue = int.Parse(integralRuleData.Integral);
                }
            }

            // SysIntegralSource: 5
            string highIntegralSourceId = "5";
            int highIntegralValue = 0;
            if (true)
            {
                IntegralRuleEntity integralRuleData = null;
                var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                {
                    IntegralSourceID = highIntegralSourceId
                }, null);
                if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到积分规则";
                    return respData.ToJSON();
                }
                else
                {
                    integralRuleData = integralRuleDataList[0];
                    highIntegralValue = int.Parse(integralRuleData.Integral);
                }
            }

            // 保存积分
            if (true)
            {
                string tmpVipId = vipId;
                int tmpIntegralValue = integralValue;
                string tmpIntegralSourceId = integralSourceId;
                string tmpOpenId = openId;
                string msgModel = "您的优惠券已验证通过，新增积分{0}分。恭喜您！";

                // 插入积分明细
                VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                vipIntegralDetailEntity.VIPID = tmpVipId;
                vipIntegralDetailEntity.SalesAmount = 0;
                vipIntegralDetailEntity.Integral = tmpIntegralValue;
                vipIntegralDetailEntity.IntegralSourceID = tmpIntegralSourceId;
                vipIntegralDetailEntity.IsAdd = 1;
                //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                // 更新积分
                VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                    new VipIntegralEntity() { VipID = vipId }, null);
                if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.BeginIntegral = 0; // 期初积分
                    vipIntegralEntity.InIntegral = 0; // 增加积分
                    vipIntegralEntity.OutIntegral = tmpIntegralValue; //消费积分
                    vipIntegralEntity.EndIntegral = tmpIntegralValue; //积分余额
                    vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Create(vipIntegralEntity);
                }
                else
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.InIntegral = vipIntegralDataList[0].InIntegral + tmpIntegralValue; ; // 增加积分
                    //vipIntegralEntity.OutIntegral = 0; //消费积分
                    vipIntegralEntity.EndIntegral = vipIntegralDataList[0].EndIntegral + tmpIntegralValue; //积分余额
                    //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = vipIntegralDataList[0].ValidIntegral + tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Update(vipIntegralEntity, false);
                }

                // 更新VIP
                VipEntity vipEntity = new VipEntity();
                var vipEntityDataList = vipBLL.QueryByEntity(
                    new VipEntity() { VIPID = tmpVipId }, null);
                if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                {
                    vipEntity.VIPID = tmpVipId;
                    //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.HigherVipID = highOpenId; //----------------
                    vipEntity.Status = 1;
                    vipBLL.Create(vipEntity);
                }
                else
                {
                    vipEntity.VIPID = tmpVipId;
                    //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.HigherVipID = highOpenId; //-------------------
                    vipBLL.Update(vipEntity, false);
                }

                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format(msgModel, tmpIntegralValue);
                string msgData = "<xml><OpenID><![CDATA[" + tmpOpenId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult:{0}", msgResult)
                });
            }

            // 保存积分2
            if (true)
            {
                string tmpVipId = highVipId;
                int tmpIntegralValue = highIntegralValue;
                string tmpIntegralSourceId = highIntegralSourceId;
                string tmpOpenId = highOpenId;
                string msgModel = "刚刚有会员验证了您提供的优惠券，为了表示感谢，我们送您积分{0}分。该会员在本店的每次购买，我们都将奖励您积分。谢谢您！";

                // 插入积分明细
                VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                vipIntegralDetailEntity.VIPID = tmpVipId;
                vipIntegralDetailEntity.SalesAmount = 0;
                vipIntegralDetailEntity.Integral = tmpIntegralValue;
                vipIntegralDetailEntity.IntegralSourceID = tmpIntegralSourceId;
                vipIntegralDetailEntity.IsAdd = 1;
                //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                // 更新积分
                VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                    new VipIntegralEntity() { VipID = tmpVipId }, null);
                if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.BeginIntegral = 0; // 期初积分
                    vipIntegralEntity.InIntegral = 0; // 增加积分
                    vipIntegralEntity.OutIntegral = tmpIntegralValue; //消费积分
                    vipIntegralEntity.EndIntegral = tmpIntegralValue; //积分余额
                    vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Create(vipIntegralEntity);
                }
                else
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.InIntegral = vipIntegralDataList[0].InIntegral + tmpIntegralValue; // 增加积分
                    //vipIntegralEntity.OutIntegral = 0; //消费积分
                    vipIntegralEntity.EndIntegral = vipIntegralDataList[0].EndIntegral + tmpIntegralValue; //积分余额
                    //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = vipIntegralDataList[0].ValidIntegral + tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Update(vipIntegralEntity, false);
                }

                // 更新VIP
                VipEntity vipEntity = new VipEntity();
                var vipEntityDataList = vipBLL.QueryByEntity(
                    new VipEntity() { VIPID = tmpVipId }, null);
                if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                {
                    vipEntity.VIPID = tmpVipId;
                    //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                    vipEntity.Status = 1;
                    vipBLL.Create(vipEntity);
                }
                else
                {
                    vipEntity.VIPID = tmpVipId;
                    //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                    vipBLL.Update(vipEntity, false);
                }

                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format(msgModel, tmpIntegralValue);
                string msgData = "<xml><OpenID><![CDATA[" + tmpOpenId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult2:{0}", msgResult)
                });
            }
            #endregion
            respData.Data = qrcode;
            return respData.ToJSON();
        }
        #endregion

        #region 获取优惠券图片信息 Jermyn20130518
        public string GetCouponImageInfo()
        {
            var currentUser = Default.GetLoggingSession();
            string content = string.Empty;
            string openId = Request["openId"];
            string imgUrl = Request["imgUrl"];
            var respData = new Default.RespData();
            #region 处理字符窜
            if (openId == null || openId.Trim().Length == 0 ||
                imgUrl == null || imgUrl.Trim().Length == 0)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求参数不能为空";
                return respData.ToJSON();
            }
            #endregion

            #region 解析图片二维码
            string uri = "?imgUrl=" + imgUrl + "";//ialumniHost + "publicMark?action=getForgetPassword&fromUserName=" + fromUserName + "&toUserName=" + toUserName;
            string method = "GET";
            string data = Common.Utils.GetRemoteData(uri, method, string.Empty);
            Uri qrcodeUri = new Uri(data);
            string highOpenId = HttpUtility.ParseQueryString(qrcodeUri.Query).Get("open_id");
            if (highOpenId == null || highOpenId.Trim().Length == 0 || openId == highOpenId)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "无效的优惠券";
                return respData.ToJSON();
            }

            #endregion

            content = SetCouponInfo(openId, highOpenId, data);
            return content;
        }
        #endregion

        #region 设置优惠券图片业务逻辑
        public string SetCouponInfo(string openId, string highOpenId, string data)
        {
            var currentUser = Default.GetLoggingSession();
            string content = string.Empty;
            var respData = new Default.RespData();
            #region 判断值不为空
            if (openId == null || openId.Trim().Length == 0)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求参数不能为空";
                return respData.ToJSON();
            }
            if (highOpenId == null || highOpenId.Trim().Length == 0 || openId == highOpenId)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "无效的优惠券";
                return respData.ToJSON();
            }
            #endregion
            #region vip
            VipBLL vipBLL = new VipBLL(currentUser);
            IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(currentUser);
            VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(currentUser);
            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(currentUser);

            string vipId = null, highVipId = null;
            if (true)
            {
                VipEntity vipIdData = null;
                var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = openId
                }, null);
                if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null ||
                    vipIdDataList[0].VIPID == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到Vip会员";
                    return respData.ToJSON();
                }
                else
                {
                    vipIdData = vipIdDataList[0];
                    vipId = vipIdData.VIPID;
                }
            }
            if (true)
            {
                VipEntity vipIdData = null;
                var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = highOpenId
                }, null);
                if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null ||
                    vipIdDataList[0].VIPID == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到上级Vip会员";
                    return respData.ToJSON();
                }
                else
                {
                    vipIdData = vipIdDataList[0];
                    highVipId = vipIdData.VIPID;
                }
            }

            // SysIntegralSource: 8
            string integralSourceId = "8";
            int integralValue = 0;
            if (true)
            {
                IntegralRuleEntity integralRuleData = null;
                var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                {
                    IntegralSourceID = integralSourceId
                }, null);
                if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到积分规则";
                    return respData.ToJSON();
                }
                else
                {
                    integralRuleData = integralRuleDataList[0];
                    integralValue = int.Parse(integralRuleData.Integral);
                }
            }

            // SysIntegralSource: 5
            string highIntegralSourceId = "5";
            int highIntegralValue = 0;
            if (true)
            {
                IntegralRuleEntity integralRuleData = null;
                var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                {
                    IntegralSourceID = highIntegralSourceId
                }, null);
                if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "未查询到积分规则";
                    return respData.ToJSON();
                }
                else
                {
                    integralRuleData = integralRuleDataList[0];
                    highIntegralValue = int.Parse(integralRuleData.Integral);
                }
            }
            #endregion
            #region 保存积分
            if (true)
            {
                string tmpVipId = vipId;
                int tmpIntegralValue = integralValue;
                string tmpIntegralSourceId = integralSourceId;
                string tmpOpenId = openId;
                string msgModel = "您的优惠券已验证通过，新增积分{0}分。恭喜您！";

                // 插入积分明细
                VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                vipIntegralDetailEntity.VIPID = tmpVipId;
                vipIntegralDetailEntity.FromVipID = tmpVipId;
                vipIntegralDetailEntity.SalesAmount = 0;
                vipIntegralDetailEntity.Integral = tmpIntegralValue;
                vipIntegralDetailEntity.IntegralSourceID = tmpIntegralSourceId;
                vipIntegralDetailEntity.IsAdd = 1;
                //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                // 更新积分
                VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                    new VipIntegralEntity() { VipID = vipId }, null);
                if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.BeginIntegral = 0; // 期初积分
                    vipIntegralEntity.InIntegral = 0; // 增加积分
                    vipIntegralEntity.OutIntegral = tmpIntegralValue; //消费积分
                    vipIntegralEntity.EndIntegral = tmpIntegralValue; //积分余额
                    vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Create(vipIntegralEntity);
                }
                else
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.InIntegral = vipIntegralDataList[0].InIntegral + tmpIntegralValue; ; // 增加积分
                    //vipIntegralEntity.OutIntegral = 0; //消费积分
                    vipIntegralEntity.EndIntegral = vipIntegralDataList[0].EndIntegral + tmpIntegralValue; //积分余额
                    //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = vipIntegralDataList[0].ValidIntegral + tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Update(vipIntegralEntity, false);
                }

                // 更新VIP
                VipEntity vipEntity = new VipEntity();
                var vipEntityDataList = vipBLL.QueryByEntity(
                    new VipEntity() { VIPID = tmpVipId }, null);
                if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                {
                    vipEntity.VIPID = tmpVipId;
                    // vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.HigherVipID = highOpenId; //-----------
                    vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                    vipEntity.Status = 1;
                    vipBLL.Create(vipEntity);
                }
                else
                {
                    vipEntity.VIPID = tmpVipId;
                    //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.HigherVipID = highOpenId; //---------------
                    vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                    vipBLL.Update(vipEntity, false);
                }

                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format(msgModel, tmpIntegralValue);
                string msgData = "<xml><OpenID><![CDATA[" + tmpOpenId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult:{0}", msgResult)
                });
            }
            #endregion
            #region 保存积分2
            if (true)
            {
                string tmpVipId = highVipId;
                int tmpIntegralValue = highIntegralValue;
                string tmpIntegralSourceId = highIntegralSourceId;
                string tmpOpenId = highOpenId;
                string msgModel = "刚刚有会员验证了您提供的优惠券，为了表示感谢，我们送您积分{0}分。该会员在本店的每次购买，我们都将奖励您积分。谢谢您！";

                // 插入积分明细
                VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                vipIntegralDetailEntity.VIPID = tmpVipId;
                vipIntegralDetailEntity.FromVipID = vipId;
                vipIntegralDetailEntity.SalesAmount = 0;
                vipIntegralDetailEntity.Integral = tmpIntegralValue;
                vipIntegralDetailEntity.IntegralSourceID = tmpIntegralSourceId;
                vipIntegralDetailEntity.IsAdd = 1;
                //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                // 更新积分
                VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                    new VipIntegralEntity() { VipID = tmpVipId }, null);
                if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.BeginIntegral = 0; // 期初积分
                    vipIntegralEntity.InIntegral = 0; // 增加积分
                    vipIntegralEntity.OutIntegral = tmpIntegralValue; //消费积分
                    vipIntegralEntity.EndIntegral = tmpIntegralValue; //积分余额
                    vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Create(vipIntegralEntity);
                }
                else
                {
                    vipIntegralEntity.VipID = tmpVipId;
                    vipIntegralEntity.InIntegral = vipIntegralDataList[0].InIntegral + tmpIntegralValue; // 增加积分
                    //vipIntegralEntity.OutIntegral = 0; //消费积分
                    vipIntegralEntity.EndIntegral = vipIntegralDataList[0].EndIntegral + tmpIntegralValue; //积分余额
                    //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                    vipIntegralEntity.ValidIntegral = vipIntegralDataList[0].ValidIntegral + tmpIntegralValue; // 当前有效积分
                    //vipIntegralBLL.Update(vipIntegralEntity, false);
                }

                // 更新VIP
                VipEntity vipEntity = new VipEntity();
                var vipEntityDataList = vipBLL.QueryByEntity(
                    new VipEntity() { VIPID = tmpVipId }, null);
                if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                {
                    vipEntity.VIPID = tmpVipId;
                    // vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                    vipEntity.Status = 1;
                    vipBLL.Create(vipEntity);
                }
                else
                {
                    vipEntity.VIPID = tmpVipId;
                    //  vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                    vipBLL.Update(vipEntity, false);
                }

                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format(msgModel, tmpIntegralValue);
                string msgData = "<xml><OpenID><![CDATA[" + tmpOpenId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult2:{0}", msgResult)
                });
            }
            #endregion
            respData.Data = data;
            return respData.ToJSON();
        }
        #endregion

        #region 获取靓图欣赏列表
        public string GetVIPImageCommentShowList()
        {
            string content = string.Empty;
            try
            {
                string ToUserName = Request["ToUserName"];
                string FromUserName = Request["FromUserName"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "ToUserName:{0}, FromUserName:{1}",
                        ToUserName, FromUserName)
                });

                var loggingSessionInfo = Default.GetLoggingSession();
                VIPImageCommentBLL bll = new VIPImageCommentBLL(loggingSessionInfo);

                if (ToUserName.Length > 0 && FromUserName.Length > 0)
                {
                    content = bll.GetVIPImageCommentShowList(ToUserName, FromUserName);
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "GetVIPImageCommentShowList-Exception:{0}",
                        ex.ToString())
                });
            }
            return content;
        }
        #endregion

        #region 获取门店列表
        public string GetStoreInfo()
        {
            string content = string.Empty;
            try
            {
                string ToUserName = Request["ToUserName"];
                string FromUserName = Request["FromUserName"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "GetStoreInfo-ToUserName:{0}, FromUserName:{1}",
                        ToUserName, FromUserName)
                });
                content = GetStoreXML(ToUserName, FromUserName, "", "");

            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "GetStoreInfo-Exception:{0}",
                        ex.ToString())
                });
            }
            return content;
        }
        private string GetStoreXML(string ToUserName, string FromUserName, string Longitude, string Latitude)
        {
            string host = System.Configuration.ConfigurationManager.AppSettings["website_url"];
            string xml = string.Empty;
            xml += "<xml>";
            xml += "<ToUserName>" + ToUserName + "</ToUserName>";
            xml += "<FromUserName>" + FromUserName + "</FromUserName>";
            xml += "<CreateTime>" + System.DateTime.Now.ToString() + "</CreateTime>";
            xml += "<MsgType>" + "news" + "</MsgType>";
            xml += "<ArticleCount>" + "4" + "</ArticleCount>";
            xml += "<Articles>";
            //string hostx = host + "/wap/base.html?ID=" + eventInfo.VipImageCommentID + "#Event/Detail";
            xml += "<item>";
            xml += "<Title>" + "【上海 闵行】 莲花国际广场店 \r\n 库存数量：28 \r\n 地址: 沪闵路7866弄莲花国际广场二楼F212-214号" + "</Title>";//
            xml += "<Description>" + "北京朝阳区北四东环路73号 远洋未来广场3层F3-10-A 电话：010-8444 5644" + "</Description>";//
            xml += "<PicUrl>" + host + "weixin/img/lhd.jpg" + "</PicUrl>";
            xml += "<Url>" + "" + "</Url>";
            xml += "</item>";

            xml += "<item>";
            xml += "<Title>" + "【上海 闵行】 闵行龙之梦店 \r\n 库存数量：58 \r\n 地址: 沪闵路6088号凯德龙之梦广场02-13号" + "</Title>";//
            xml += "<Description>" + "" + "</Description>";//
            xml += "<PicUrl>" + host + "weixin/img/dl_lsf.jpg" + "</PicUrl>";
            xml += "<Url>" + "" + "</Url>";
            xml += "</item>";

            xml += "<item>";
            xml += "<Title>" + "【上海 闵行】 漕宝星星店 \r\n 库存数量：38 \r\n 地址: 漕宝路1574号漕宝购物中心2楼2005号" + "</Title>";//
            xml += "<Description>" + "" + "</Description>";//
            xml += "<PicUrl>" + host + "weixin/img/xxd.jpg" + "</PicUrl>";
            xml += "<Url>" + "" + "</Url>";
            xml += "</item>";

            xml += "<item>";
            xml += "<Title>" + "【上海 闵行】 七宝店 \r\n 库存数量：35 \r\n 地址: 七莘路3655凯德龙城购物广场01-11室" + "</Title>";//
            xml += "<Description>" + "" + "</Description>";//
            xml += "<PicUrl>" + host + "weixin/img/qbd.jpg" + "</PicUrl>";
            xml += "<Url>" + "" + "</Url>";
            xml += "</item>";

            xml += "</Articles>";
            xml += "<FuncFlag>1</FuncFlag>";
            xml += "</xml>";
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = "门店列表:" + xml.ToString() });
            return xml;
        }
        #endregion



        #region 发送关注时，给予问券
        /// <summary>
        /// 发送关注时，给予问券
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="unitName"></param>
        /// <param name="qrcode_id"></param>
        /// <param name="WeiXin"></param>
        public void SetPushQuestionnaire(string openId
            , string customerId
            , string userId
            , string unitName
            , string qrcode_id
            , string WeiXin
            , int iRad
            )
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format(
                    "进入SetPushQuestionnaire: OpenID:{0}, customerId:{1}, userId:{2}, unitName:{3}, qrcode_id:{4}, WeiXin:{5}",
                    openId, customerId, userId, unitName, qrcode_id, WeiXin)
            });
            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"];
            string AuthUrl = ConfigurationManager.AppSettings["website_url"];
            if (qrcode_id == null || qrcode_id.Equals(""))
            { }
            else
            {
                if (!WeiXin.Equals("gh_3dce00b8133c"))   //o2omarketing
                {
                    #region
                    Random rad = new Random();
                    string Url1 = webUrl + "WEvent/base.html?eventId=8D41CDD7D5E4499195316E4645FCD7B9&rid=" + rad.Next(1000, 100000) + "&customerId=" + customerId + "&userId=" + userId + "&openId=" + openId + "#Event/Detail";
                    Url1 = webUrl + "wap/Event/20131109/e.htm?customerId=" + customerId + "&eventId=8D41CDD7D5E4499195316E4645FCD7B9&userId=" + userId + "&openId=" + openId + "&rid=" + rad.Next(1000, 100000) + "";
                    string picUrl1 = webUrl + "/weixin/o2omarketing.jpg";
                    string msgData = string.Empty;

                    if (qrcode_id.Equals("4"))
                    {
                        #region 特殊的曾那大师
                        picUrl1 = webUrl + "weixin/lj004.jpg";
                        Url1 = "http://www.lzlj.com/BigPic/63.html";
                        msgData = "<xml>"
                                + "<OpenID><![CDATA[" + openId + "]]></OpenID>"
                                + "<MsgType><![CDATA[news]]></MsgType>"
                                + "<Articles>"
                                + "<item>"
                                + "<Title><![CDATA[和曾娜一起品味不一样的精彩！]]></Title> "
                                + "<Description><![CDATA[谁说白酒是男人的战场？她，告诉世界另一种可能。26岁，当选中国最年轻的国家级白酒评委。甲午新春，中国调酒大师曾娜纪念版国窖1573，邀您一起探索白酒的精彩世界！]]></Description> "
                                + "<Url><![CDATA[" + Url1 + "]]></Url> "
                                + "<PicUrl><![CDATA[" + picUrl1 + "]]></PicUrl> "
                                + "</item>"
                                + "</Articles>"
                                + "</xml>";

                        var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #endregion
                    }
                    else
                    {
                        if (Convert.ToInt32(qrcode_id) < 100)
                        {
                            #region 门店
                            Url1 = "http://o2oapi.aladingyidong.com/WXOAuth/AuthUniversal.aspx?eventId=BFC41A8BF8564B6DB76AE8A8E43557BA&rid=" + rad.Next(1000, 100000) + "&customerId=f6a7da3d28f74f2abedfc3ea0cf65c01&applicationId=24F084EDA94648E4BEFBDB11597EC42A&ver=111&goUrl=o2oapi.aladingyidong.com/OnlineClothing20131217/guaguaorderLj1.html";
                            msgData = "<xml>"
                                    + "<OpenID><![CDATA[" + openId + "]]></OpenID>"
                                    + "<MsgType><![CDATA[news]]></MsgType>"
                                    + "<Articles>"
                                    + "<item>"
                                    + "<Title><![CDATA[" + unitName + "]]></Title> "
                                    + "<Description><![CDATA[" + unitName + "欢迎你，请点击参与天天有礼！]]></Description> "
                                    + "<Url><![CDATA[" + Url1 + "]]></Url> "
                                    + "<PicUrl><![CDATA[" + picUrl1 + "]]></PicUrl> "
                                    + "</item>"
                                    + "</Articles>"
                                    + "</xml>";
                            var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                            #endregion
                        }
                        else
                        {
                            #region 活动
                            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                            LEventsBLL eventServer = new LEventsBLL(loggingSessionInfo);
                            LEventsEntity eventInfo = new LEventsEntity();
                            eventInfo = eventServer.GetEventInfoByWX(qrcode_id);
                            if (eventInfo != null && eventInfo.EventID != null)
                            {
                                #region 获取门店需要退出的特殊信息
                                //Jermyn20131209 添加用户与活动关系
                                WEventUserMappingBLL eventUserMappingServer = new WEventUserMappingBLL(loggingSessionInfo);
                                WEventUserMappingEntity eventUserMappingInfo = new WEventUserMappingEntity();
                                eventUserMappingInfo.Mapping = Common.Utils.NewGuid();
                                eventUserMappingInfo.EventID = eventInfo.EventID;
                                eventUserMappingInfo.UserID = userId;
                                eventUserMappingServer.Create(eventUserMappingInfo);
                                //////////////////////////////////////////////////////////////////////////////////////////////////
                                //提交活动关注回复
                                string strError = "";
                                bool bReturn = eventServer.SetEventWXPush(eventInfo, WeiXin, openId, userId, msgUrl, out strError, AuthUrl, iRad);
                                if (!bReturn)
                                {
                                    Loggers.Debug(new DebugLogInfo()
                                    {
                                        Message = string.Format("关注固定二维码时，处理活动的回复信息SetPushQuestionnaire：{0},eventInfo:{1};WeiXin:{2};openId:{3}", strError, eventInfo.ToJSON().ToString(), WeiXin, openId)
                                    });
                                }
                                #endregion

                                #region 问卷调查
                                /////////////////////////////////////////////////////////////////////////////
                                //if (eventInfo.ApplyQuesID != null && !eventInfo.ApplyQuesID.Equals(""))
                                //{
                                //    //Jermyn20131209 关闭
                                //    string strUrl = ConfigurationManager.AppSettings["website_WWW"];
                                //    picUrl1 = eventInfo.ImageURL;
                                //    Url1 = strUrl + "wap/Event/20131109/e.htm?customerId=" + customerId + "&eventId=" + eventInfo.EventID + "&userId=" + userId + "&openId=" + openId + "&rid=" + rad.Next(1000, 100000) + "";
                                //    string desc = eventInfo.Description;
                                //    if (desc != null && !desc.Equals(""))
                                //    {
                                //        desc = DataTableToObject.NoHTML(HttpUtility.HtmlDecode(desc));
                                //    }
                                //    else
                                //    {
                                //        desc = "欢迎参与调查.";
                                //    }
                                //    msgData = "<xml>"
                                //            + "<OpenID><![CDATA[" + openId + "]]></OpenID>"
                                //            + "<MsgType><![CDATA[news]]></MsgType>"
                                //            + "<Articles>"
                                //            + "<item>"
                                //            + "<Title><![CDATA[欢迎参与调查]]></Title> "
                                //            + "<Description><![CDATA[" + HttpUtility.HtmlDecode(eventInfo.Description) + "]]></Description> "
                                //            + "<Url><![CDATA[" + Url1 + "]]></Url> "
                                //            + "<PicUrl><![CDATA[" + picUrl1 + "]]></PicUrl> "
                                //            + "</item>"
                                //            + "</Articles>"
                                //            + "</xml>";
                                //    var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                                //}
                                #endregion
                            }
                            #endregion
                        }
                    }


                    #endregion
                }
                else
                { //中欧校友
                    var loggingSessionInfo = Default.GetBSLoggingSession("29E11BDC6DAC439896958CC6866FF64E", "1");
                    JIT.CPOS.BS.BLL.zhongou.ZOSignPush signServer = new BS.BLL.zhongou.ZOSignPush(loggingSessionInfo);
                    signServer.SetPushSignIn(qrcode_id, WeiXin, openId);
                }
            }
        }
        #endregion

        #region 用户与门店关系
        private void SetUserUnitMapping(string VipId, string UnitId, LoggingSessionInfo loggingSessionInfo)
        {
            VipUnitMappingBLL vipUnitMappingServer = new VipUnitMappingBLL(loggingSessionInfo);
            var vipUnitMappingObj = vipUnitMappingServer.QueryByEntity(new VipUnitMappingEntity
            {
                IsDelete = 0
                ,
                VIPID = VipId
            }, null);
            if (vipUnitMappingObj == null || vipUnitMappingObj.Length == 0 || vipUnitMappingObj[0] == null)
            {
                //新建
            }
            else
            {
                foreach (VipUnitMappingEntity vipUnitMappingInfo in vipUnitMappingObj)
                {
                    vipUnitMappingServer.Delete(vipUnitMappingInfo);
                }
            }
        }
        #endregion


        #region 设置插入用户客户信息
        private string SetCustomerVipInfo(LoggingSessionInfo AploggingSessionInfo
                                        , string WeiXin
                                        , string OpenID
                                        , string City
                                        , string IsShow
                                        , string VipName
                                        , string Gender
                                        , string qrcode
                                        , string newFilePath
                                        , string couponNewFilePath
                                        , string headimgurl
                                        , string qrcode_id
                                        , string unionid
                                        , out string vipId
                                        )
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = "进入关注日志：" + qrcode_id.ToString()
            });
            Random rad = new Random();
            int iRad = rad.Next(1000, 100000);
            #region 判断是新建还是修改 Jermyn20131107 处理来自不同客户信息
            VipEntity vipQueryInfo = new VipEntity();
            VipEntity vipInfo = new VipEntity();

            string customerIdUnoin = string.Empty;
            string unitName = string.Empty;

            WMenuBLL menuServer = new WMenuBLL(AploggingSessionInfo);
            customerIdUnoin = menuServer.GetCustomerIdByWx(WeiXin);//根据微信标识获取商户信息

            vipQueryInfo.WeiXinUserId = OpenID;
            vipQueryInfo.WeiXin = WeiXin;
            vipQueryInfo.ClientID = customerIdUnoin;

            var tmpUser = Default.GetBSLoggingSession(customerIdUnoin, "system");//商户系统的数据库链接**

            #region 处理日志
            try
            {
                VipShowLogEntity vipShowLogInfoTmp = new VipShowLogEntity();
                vipShowLogInfoTmp.VipLogID = System.Guid.NewGuid().ToString().Replace("-", "");
                vipShowLogInfoTmp.OpenID = OpenID;
                if (qrcode_id == null || qrcode_id.Equals(""))
                {
                    vipShowLogInfoTmp.City = City;
                }
                else { vipShowLogInfoTmp.City = qrcode_id; }
                vipShowLogInfoTmp.IsShow = Convert.ToInt32(IsShow);
                vipShowLogInfoTmp.WeiXin = WeiXin;
                VipShowLogBLL vipShowLogBllTmp = new VipShowLogBLL(tmpUser);
                vipShowLogInfoTmp.VipName = VipName;
                vipShowLogInfoTmp.Language = iRad.ToString();
                vipShowLogBllTmp.Create(vipShowLogInfoTmp);

            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "插入关注日志：" + ex.ToString()
                });
            }
            #endregion

            if (IsShow.Equals("1")) //取消关注不要修改vip表
            {
                #region 处理会员
                VipBLL vipServiceUnion = new VipBLL(tmpUser);
                var bllPrize = new LPrizesBLL(tmpUser);//触点奖励实例
                vipInfo.WeiXinUserId = OpenID;
                vipInfo.City = City;
                if (Gender != string.Empty) vipInfo.Gender = Convert.ToInt32(Gender);
                vipInfo.VipName = VipName;
                // vipInfo.QRVipCode = ConfigurationManager.AppSettings["website_url"].Trim() + "WeiXin/" + newFilePath;
                // vipInfo.CouponURL = ConfigurationManager.AppSettings["website_url"].Trim() + "WeiXin/" + couponNewFilePath;
                vipInfo.Col49 = qrcode;//这个应该放在这里，因为这样会作为一个查询条件，这样之前不是集客方式注册的会员就不会被查到
                vipInfo.WeiXin = WeiXin;
                vipInfo.VipSourceId = "3";
                vipInfo.HeadImgUrl = headimgurl;

                var vipObj = vipServiceUnion.QueryByEntity(vipQueryInfo, null);

                //取消关注做一个标识，以和未关注的oauth认证做区别***
                vipInfo.Col25 = "";//表示为取消关注状态，会员取消关注时，需要将该字段置‘1’

                if (vipObj == null || vipObj.Length == 0 || vipObj[0] == null)//新建
                {
                    if (vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                    {
                        vipInfo.VIPID = Guid.NewGuid().ToString().Replace("-", "");
                    }
                    if (vipInfo.VipCode == null || vipInfo.VipCode.Equals(""))
                    {
                        vipInfo.VipCode = vipServiceUnion.GetVipCode();
                    }
                    // vipInfo.Col49 = qrcode;//应该放在这里****
                    vipInfo.VipSourceId = "3";
                    vipInfo.ClientID = tmpUser.CurrentUser.customer_id;
                    vipInfo.Status = 1;//关注状态
                    vipInfo.VipPasswrod = "e10adc3949ba59abbe56e057f20f883e";
                    vipInfo.UnionID = unionid;
                    //if(qrcode_id != null && !qrcode_id.Equals(""))
                    vipInfo.Col50 = iRad.ToString();
                    vipInfo.RegistrationTime = DateTime.Now;
                    UnitService unitServer = new UnitService(tmpUser);
                    vipInfo.CouponInfo = unitServer.GetUnitByUnitTypeForWX("总部", null).Id; //获取总部门店标识
                    //根据UnionID判断已关注其他绑定公众号
                    if (!string.IsNullOrEmpty(vipInfo.UnionID))
                    {
                        var vipUnionInfo = vipServiceUnion.QueryByEntity(new VipEntity() { ClientID = tmpUser.CurrentUser.customer_id, UnionID = vipInfo.UnionID }, null).FirstOrDefault();
                        if (vipUnionInfo == null)//首次关注
                        {

                            vipServiceUnion.Create(vipInfo);
                            #region 关注触点活动奖励

                           // bllPrize.CheckIsWinnerForShare(vipInfo.VIPID, "", "Focus");
                            RedisContactBLL redisContactBll = new RedisContactBLL();
                            if (redisContactBll.GetContactLength(new RedisOpenAPIClient.Models.CC.CC_Contact()
                                                                    {
                                                                        CustomerId = tmpUser.CurrentUser.customer_id,
                                                                        ContactType = "Focus",
                                                                        VipId = vipInfo.VIPID
                                                                    }) == 0)
                            {
                                redisContactBll.SetRedisContact(new RedisOpenAPIClient.Models.CC.CC_Contact()
                                {
                                    CustomerId = tmpUser.CurrentUser.customer_id,
                                    ContactType = "Focus",
                                    VipId = vipInfo.VIPID
                                });
                            }
                            #endregion
                            #region 创意仓库关注log
                            BaseService.WriteLogWeixin(" 二维码code：" + qrcode_id);
                            WQRCodeManagerBLL bllWQRCodeManage = new WQRCodeManagerBLL(tmpUser);
                            var entityWQRCodeManage = bllWQRCodeManage.QueryByEntity(new WQRCodeManagerEntity() { QRCode = qrcode_id, Remark = "CTW", CustomerId = tmpUser.CurrentUser.customer_id }, null).SingleOrDefault();
                            if(entityWQRCodeManage!=null)
                            {
                                T_LEventsRegVipLogBLL lEventRegVipLogBll = new T_LEventsRegVipLogBLL(tmpUser);
                                if (!string.IsNullOrEmpty(entityWQRCodeManage.ObjectId))
                                {
                                    lEventRegVipLogBll.CTWRegOrFocusLog(entityWQRCodeManage.ObjectId, "", vipInfo.VIPID, tmpUser, "Focus");
                                }
                            }
                            #endregion


                        }
                        //已关注了绑定公众号中的其他公众号
                        WXUserInfoBLL wxUserInfoBLL = new WXUserInfoBLL(tmpUser);
                        var wxUserInfoEntity = new WXUserInfoEntity()
                        {
                            VipID = vipUnionInfo == null ? vipInfo.VIPID : vipUnionInfo.VIPID,
                            WeiXin = vipInfo.WeiXin,
                            WeiXinUserID = vipInfo.WeiXinUserId,
                            UnionID = vipInfo.UnionID,
                            CustomerID = tmpUser.CurrentUser.customer_id
                        };
                        wxUserInfoBLL.Create(wxUserInfoEntity);
                    }
                    else //未绑定多个公众号
                    {
                        vipServiceUnion.Create(vipInfo);
                        #region 关注触点活动奖励
                        
                           // bllPrize.CheckIsWinnerForShare(vipInfo.VIPID, "", "Focus");
                            RedisContactBLL redisContactBll = new RedisContactBLL();
                            if (redisContactBll.GetContactLength(new RedisOpenAPIClient.Models.CC.CC_Contact()
                            {
                                CustomerId = tmpUser.CurrentUser.customer_id,
                                ContactType = "Focus",
                                VipId = vipInfo.VIPID
                            }) == 0)
                            {
                                redisContactBll.SetRedisContact(new RedisOpenAPIClient.Models.CC.CC_Contact()
                                {
                                    CustomerId = tmpUser.CurrentUser.customer_id,
                                    ContactType = "Focus",
                                    VipId = vipInfo.VIPID
                                });
                            }
                        #endregion
                            #region 创意仓库关注log
                            BaseService.WriteLogWeixin(" 二维码code：" + qrcode_id);
                            WQRCodeManagerBLL bllWQRCodeManage = new WQRCodeManagerBLL(tmpUser);
                            var entityWQRCodeManage = bllWQRCodeManage.QueryByEntity(new WQRCodeManagerEntity() { QRCode = qrcode_id, Remark = "CTW", CustomerId = tmpUser.CurrentUser.customer_id }, null).SingleOrDefault();
                            if (entityWQRCodeManage != null)
                            {
                                
                                T_LEventsRegVipLogBLL lEventRegVipLogBll = new T_LEventsRegVipLogBLL(tmpUser);
                                if (!string.IsNullOrEmpty(entityWQRCodeManage.ObjectId))
                                {
                                    BaseService.WriteLogWeixin(" 创意仓库日志：" + entityWQRCodeManage.ObjectId + "_vipid:" + vipInfo.VIPID);
                                    lEventRegVipLogBll.CTWRegOrFocusLog(entityWQRCodeManage.ObjectId, "", vipInfo.VIPID, tmpUser, "Focus");
                                }
                            }
                        
                        #endregion
                    }
                    vipId = vipInfo.VIPID;
                }
                else
                {
                    #region 创意仓库关注log
                    if (vipObj[0].Status == 0)
                    {
                        try
                        {


                            BaseService.WriteLogWeixin(" 二维码code：" + qrcode_id);
                            WQRCodeManagerBLL bllWQRCodeManage = new WQRCodeManagerBLL(tmpUser);
                            var entityWQRCodeManage = bllWQRCodeManage.QueryByEntity(new WQRCodeManagerEntity() { QRCode = qrcode_id, Remark = "CTW", CustomerId = tmpUser.CurrentUser.customer_id }, null).SingleOrDefault();
                            if (entityWQRCodeManage != null)
                            {

                                T_LEventsRegVipLogBLL lEventRegVipLogBll = new T_LEventsRegVipLogBLL(tmpUser);
                                if (!string.IsNullOrEmpty(entityWQRCodeManage.ObjectId))
                                {
                                    BaseService.WriteLogWeixin(" 创意仓库日志：" + entityWQRCodeManage.ObjectId + "_vipid:" + vipObj[0].VIPID);
                                    lEventRegVipLogBll.CTWRegOrFocusLog(entityWQRCodeManage.ObjectId, "", vipObj[0].VIPID, tmpUser, "Focus");
                                    
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            BaseService.WriteLogWeixin(" 创意仓库日志：" + ex.Message.ToString());
                        }
                    }
                    #endregion

                    vipInfo.VIPID = vipObj[0].VIPID;
                    vipId = vipObj[0].VIPID;
                    #region Jermyn20140819 处理会员状态，根据手机号码判断
                    if (vipObj[0].Phone == null || vipObj[0].Phone.ToString().Equals(""))
                    {
                        BaseService.WriteLogWeixin(" 会员状态：" + vipInfo.Status + "_vipid:" + vipObj[0].VIPID);
                        vipInfo.Status = 1;
                    }
                    else
                    {
                        vipInfo.Status = 2;
                    }
                    #endregion
                    if (vipObj[0].VipCode == null || vipObj[0].VipCode.Equals(""))
                    {
                        vipInfo.VipCode = vipServiceUnion.GetVipCode();
                    }
                    if (headimgurl != null && !headimgurl.Equals(""))
                    {
                        vipInfo.HeadImgUrl = headimgurl;
                    }
                    vipInfo.ClientID = tmpUser.CurrentUser.customer_id;
                    //vipInfo.VipCode = null;
                    vipInfo.VipPasswrod = "e10adc3949ba59abbe56e057f20f883e";
                    vipInfo.Col49 = iRad.ToString();
                    vipInfo.IsDelete = 0;//设为没有被删除***

                    vipServiceUnion.Update(vipInfo, false);
                }
                #endregion

                #region 获取门店标识
                VipUnitMappingBLL vipUnitMappingServer = new VipUnitMappingBLL(tmpUser);
                string unitId = string.Empty;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("关注--qrcode_id：{0}", qrcode_id)
                });
                //1.根据门店标识获取门店信息
                VwUnitPropertyBLL vwUnitPServer = new VwUnitPropertyBLL(tmpUser);
                var unitObj = vwUnitPServer.QueryByEntity(new VwUnitPropertyEntity
                {
                    WeiXinUnitCode = qrcode_id
                    ,
                    IsDelete = 0
                    ,
                    CustomerId = tmpUser.CurrentUser.customer_id
                }, null);
                if (unitObj == null || unitObj.Length == 0 || unitObj[0] == null)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("关注：{0}", "不存在对应的门店标识")
                    });

                }
                else
                {
                    unitId = unitObj[0].UnitId.ToString();
                    unitName = unitObj[0].UnitName.ToString();
                    SetUserUnitMapping(vipInfo.VIPID, unitId, tmpUser);
                }
                //Jermyn20131112判断是否临时二维码码
                VipDCodeBLL vipDCodeServer = new VipDCodeBLL(tmpUser);
                VipDCodeEntity vipDCodeInfo = vipDCodeServer.GetByID(qrcode_id);
                if (vipDCodeInfo == null || vipDCodeInfo.Equals(""))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("关注获取dcode：{0}", "没找到匹配的")
                    });
                }
                else
                {
                    //这里记录临时二维码的扫描记录***                               
                    QRCodeScanLogEntity qrCodeScanLogEntity = new QRCodeScanLogEntity();
                    qrCodeScanLogEntity.QRCodeScanLogID = Guid.NewGuid();
                    qrCodeScanLogEntity.VipID = vipInfo.VIPID;
                    qrCodeScanLogEntity.OpenID = OpenID;
                    qrCodeScanLogEntity.WeiXin = WeiXin;
                    qrCodeScanLogEntity.QRCodeID = vipDCodeInfo.DCodeId.ToString();//记录主键值，而不是二维码code
                    qrCodeScanLogEntity.CustomerId = tmpUser.ClientID;
                    qrCodeScanLogEntity.QRCodeType = 2;//  二维码类型：1.永久二维码  2.临时二维码
                    qrCodeScanLogEntity.BusTypeCode = vipDCodeInfo.DCodeType.ToString();//业务类型编码(记录临时二维码类型表里的场景值)
                    qrCodeScanLogEntity.ObjectId = vipDCodeInfo.ObjectId ?? vipDCodeInfo.CreateBy;//二维码对应的对象（如员工集客，就记录员工的标识）,为空就记录create
                    qrCodeScanLogEntity.IsDelete = 0;
                    qrCodeScanLogEntity.CreateBy = "临时二维码扫描记录";
                    qrCodeScanLogEntity.CreateTime = DateTime.Now;
                    qrCodeScanLogEntity.LastUpdateBy = "临时二维码扫描记录";
                    qrCodeScanLogEntity.LastUpdateTime = DateTime.Now;

                    QRCodeScanLogBLL qrCodeScanLogBll = new QRCodeScanLogBLL(tmpUser);
                    qrCodeScanLogBll.Create(qrCodeScanLogEntity);



                    vipDCodeInfo.Status = "1";
                    vipDCodeInfo.OpenId = OpenID;
                    vipDCodeInfo.DCodeId = qrcode_id;//类似50007094的文字
                    unitId = vipDCodeInfo.UnitId;
                    vipDCodeInfo.VipId = vipId;
                    vipDCodeServer.Update(vipDCodeInfo);
                    SetUserUnitMapping(vipInfo.VIPID, unitId, tmpUser);
                    #region 邹坤 2014-10-11 处理动态二维码分类业务****
                    switch (vipDCodeInfo.DCodeType)
                    {
                        //case 1: //返利集客员工二维码，已经做好
                        case 2: //人人销售 集客员工二维码

                            var subBll = new VipOrderSubRunObjectMappingBLL(tmpUser);

                            //设置集客员工与会员关系
                            var o = subBll.SetVipOrderSubRun(customerIdUnoin, vipId, 15, vipDCodeInfo.CreateBy);//这里把集客员工从vipDCodeInfo.CreateBy里取****
                            Type t = o.GetType();
                            var Desc = t.GetProperty("Desc").GetValue(o, null).ToString();
                            var IsSuccess = t.GetProperty("IsSuccess").GetValue(o, null).ToString();

                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("设置集客员工与会员关系IsSuccess:{0},Desc:{1}", IsSuccess, Desc)
                            });

                            //如果销售员没有会籍店，直接不做任何操作
                            if (!string.IsNullOrWhiteSpace(vipDCodeInfo.UnitId))
                            {

                                //给集客员工奖励
                                //subBll.setJiKeGift(vipDCodeInfo.CreateBy, vipId);

                                //设置集客员工与会集店关系****
                                dynamic ob = subBll.SetVipOrderSubRun(customerIdUnoin, vipId, 3, vipDCodeInfo.UnitId);
                                Type ty = ob.GetType();
                                var Desc_ = ty.GetProperty("Desc").GetValue(o, null).ToString();
                                var IsSuccess_ = ty.GetProperty("IsSuccess").GetValue(o, null).ToString();
                                Loggers.Debug(new DebugLogInfo()
                                {
                                    Message = string.Format("设置集客员工与会集店关系IsSuccess:{0},Desc:{1}", IsSuccess_, Desc_)
                                });
                            }

                            break;
                        default:
                            break;
                    }
                    #endregion
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("关注获取dcode：{0}", "更新")
                    });
                }

                VipUnitMappingEntity vipUnitInfo = new VipUnitMappingEntity();
                vipUnitInfo.UnitId = unitId;
                vipUnitInfo.VipUnitID = Guid.NewGuid().ToString().Replace("-", string.Empty);
                vipUnitInfo.VIPID = vipInfo.VIPID;
                vipUnitInfo.CreateBy = vipInfo.VIPID;
                vipUnitMappingServer.Create(vipUnitInfo);
                #endregion

                #region Jermyn20140213关注获取积分
                VipIntegralBLL vipIntegralServer = new VipIntegralBLL(tmpUser);
                vipIntegralServer.ProcessPoint(3, customerIdUnoin, vipId, vipId);

                #endregion
                #region 关注获取返现
                string strErrormessage = "";
                VipAmountBLL Amountbll = new VipAmountBLL(tmpUser);
                Amountbll.SetVipAmountChange(customerIdUnoin, 9, vipId, 0, vipId, "关注奖励", "IN", out strErrormessage);//关注奖励对应的

                #endregion
            }
            else
            {
                #region 取消关注，会员状态置为取消status=0
                VipBLL vipServiceUnion = new VipBLL(tmpUser);
                var vipObj = vipServiceUnion.QueryByEntity(new VipEntity
                {
                    WeiXinUserId = OpenID
                }, null);
                if (vipObj == null || vipObj.Length == 0 || vipObj[0] == null)
                {
                    vipId = "";
                }
                else
                {
                    VipEntity vipInfoUnion = vipObj[0];
                    vipInfoUnion.Status = 0;
                    vipInfoUnion.VipCode = null;

                    //取消关注做一个标识，以和未关注的oauth认证做区别***
                    vipInfoUnion.Col25 = "1";//表示为取消关注状态，会员取消关注时，需要将该字段置‘1’

                    vipServiceUnion.Update(vipInfoUnion, false);
                    vipId = vipInfoUnion.VIPID;
                   

                }

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetCustomerVipInfo-Cancel: {0}", IsShow + Convert.ToString(System.DateTime.Now))
                });

                var vipEntity = vipObj[0];

                try
                {
                    CSPublicInterface csServer = new CSPublicInterface(tmpUser);
                    csServer.UnSubscribe(vipEntity.WeiXinUserId);
                }
                catch (Exception ex)
                {
                    new MarketSendLogBLL(tmpUser).Create(new MarketSendLogEntity()
                    {
                        LogId = Common.Utils.NewGuid(),
                        VipId = vipEntity.VIPID,
                        WeiXinUserId = vipEntity.WeiXinUserId,
                        MarketEventId = "取消关注",
                        TemplateContent = ex.ToString(),
                        SendTypeId = "1",
                        IsSuccess = 0
                    });
                }
                #endregion
            }

            return customerIdUnoin;
            #endregion
        }
        #endregion

        #region 推广二维码扫描时处理 added by zhangwei 2014-2-13
        //已经不在使用
        /// <summary>
        ///推广二维码扫描时处理
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="qrcode_id"></param>
        /// <param name="WeiXin"></param>
        public void SetPushQRCode(string openId
            , string customerId
            , string userId
            , string qrcode_id
            , string WeiXin

            )
        {
            CommonBLL commonService = new CommonBLL();
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format(
                    "进入SetPushQRCode: OpenID:{0}, customerId:{1}, userId:{2},  qrcode_id:{3}, WeiXin:{4}",
                    openId, customerId, userId, qrcode_id, WeiXin)
            });
            if (!string.IsNullOrEmpty(qrcode_id))
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                WQRCodeManagerEntity qWqrCodeManagerEntity =
                    new WQRCodeManagerBLL(loggingSessionInfo).QueryByEntity(new WQRCodeManagerEntity
                        {
                            QRCode = qrcode_id
                        }, null)[0];
                //记录扫描记录
                QRCodeScanLogEntity qrCodeScanLogEntity = new QRCodeScanLogEntity();
                qrCodeScanLogEntity.QRCodeScanLogID = Guid.NewGuid();
                qrCodeScanLogEntity.VipID = userId;
                qrCodeScanLogEntity.OpenID = openId;
                qrCodeScanLogEntity.WeiXin = WeiXin;
                qrCodeScanLogEntity.QRCodeID = qWqrCodeManagerEntity.QRCodeId.ToString();
                qrCodeScanLogEntity.CustomerId = customerId;
                QRCodeScanLogBLL qrCodeScanLogBll = new QRCodeScanLogBLL(loggingSessionInfo);
                qrCodeScanLogBll.Create(qrCodeScanLogEntity);
                //触发扫描动作
                QRSacnPreAction(loggingSessionInfo, WeiXin, userId, openId, qrcode_id);

                WModelBLL modelBll = new WModelBLL(loggingSessionInfo);
                WQRCodeManagerBLL qCodeManagerBll = new WQRCodeManagerBLL(loggingSessionInfo);
                WQRCodeTypeBLL qCodeTypeBll = new WQRCodeTypeBLL(loggingSessionInfo);
                WQRCodeTypeModelMappingBLL qCodeTypeModelMappingBll = new WQRCodeTypeModelMappingBLL(loggingSessionInfo);
                WApplicationInterfaceBLL applicationInterfaceBll = new WApplicationInterfaceBLL(loggingSessionInfo);
                var applicationInterfaceEntity = applicationInterfaceBll.QueryByEntity(
                    new WApplicationInterfaceEntity { WeiXinID = WeiXin }, null);
                if (applicationInterfaceEntity.Length == 0)
                {
                    throw new Exception("微信ID：" + WeiXin + "不存在");
                }
                string appID = applicationInterfaceEntity[0].AppID;
                string appSecret = applicationInterfaceEntity[0].AppSecret;
                WQRCodeManagerEntity qCodeManagerEntity = qCodeManagerBll.QueryByEntity(new WQRCodeManagerEntity
                {
                    QRCode = qrcode_id
                }, null)[0];

                if (qCodeManagerEntity == null)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format(
                            "二维码不存在: OpenID:{0}, customerId:{1}, userId:{2},  qrcode_id:{3}, WeiXin:{4}",
                            openId, customerId, userId, qrcode_id, WeiXin)
                    });
                    throw new Exception("二维码不存在:" + qrcode_id);
                }
                #region Jermyn20140530 插入活动签到
                else
                {
                    SetEventSign(customerId, userId, qCodeManagerEntity.ObjectId);
                }
                #endregion

                var modelMappings = qCodeTypeModelMappingBll.QueryByEntity(new WQRCodeTypeModelMappingEntity
                {
                    QRCodeTypeId = qCodeManagerEntity.QRCodeTypeId
                }, null);
                if (modelMappings.Length > 0)
                {
                    foreach (var modelMapping in modelMappings)
                    {
                        WQRCodeTypeEntity qCodeTypeEntity = qCodeTypeBll.GetByID(modelMapping.QRCodeTypeId);
                        WModelEntity modelEntity = modelBll.GetByID(modelMapping.ModelId);
                        switch (modelEntity.MaterialTypeId)
                        {
                            //文本消息
                            case 1:
                                var dsMaterialWriting = new WMaterialWritingDAO(loggingSessionInfo).GetWMaterialWritingByID(modelEntity.MaterialId);

                                if (dsMaterialWriting != null && dsMaterialWriting.Tables.Count > 0 && dsMaterialWriting.Tables[0].Rows.Count > 0)
                                {
                                    var content = dsMaterialWriting.Tables[0].Rows[0]["Content"].ToString();
                                    //替换模板
                                    ReplaceTemplate(loggingSessionInfo, ref content, qCodeTypeEntity, qCodeManagerEntity);
                                    //推送消息
                                    SendMessageEntity sendInfo = new SendMessageEntity();
                                    sendInfo.msgtype = "text";
                                    sendInfo.touser = openId;
                                    //sendInfo.articles = newsList;
                                    sendInfo.content = content;

                                    ResultEntity msgResultObj = commonService.SendMessage(sendInfo, appID, appSecret, loggingSessionInfo);
                                    // commonService.ResponseTextMessage(WeiXin, openId, content, HttpContext.Current);
                                }
                                break;
                            //图片消息
                            case 2:

                                break;
                            //图文消息
                            case 3:
                                var dsMaterialText = new WMaterialTextDAO(loggingSessionInfo).GetMaterialTextByID(modelEntity.MaterialId);
                                if (dsMaterialText != null && dsMaterialText.Tables.Count > 0 && dsMaterialText.Tables[0].Rows.Count > 0)
                                {

                                    var newsList = new List<NewsEntity>();
                                    foreach (DataRow dr in dsMaterialText.Tables[0].Rows)
                                    {
                                        var url = dr["OriginalUrl"].ToString();

                                        #region 分享业务 链接后面加上openId和userId

                                        if (url.IndexOf("ParAll=") != -1)
                                        {
                                            var vipId = userId;
                                            if (string.IsNullOrEmpty(vipId))
                                            {
                                                VipBLL vipService = new VipBLL(loggingSessionInfo);
                                                var vipEntity =
                                                    vipService.QueryByEntity(
                                                        new VipEntity { WeiXinUserId = openId, WeiXin = WeiXin }, null);
                                                if (vipEntity != null && vipEntity.Length > 0)
                                                {
                                                    var firstOrDefault = vipEntity.FirstOrDefault();
                                                    if (firstOrDefault != null)
                                                        vipId = firstOrDefault.VIPID;
                                                }
                                            }
                                            url += "&openId=" + openId + "&userId=" + vipId;
                                        }

                                        #endregion

                                        newsList.Add(new NewsEntity()
                                        {
                                            title = dr["Title"].ToString(),
                                            description = "",
                                            picurl = dr["CoverImageUrl"].ToString(),
                                            url = url
                                        });
                                    }

                                    //替换模板
                                    ReplaceTemplate(loggingSessionInfo, newsList, qCodeTypeEntity, qCodeManagerEntity);
                                    //推送消息
                                    SendMessageEntity sendInfo = new SendMessageEntity();
                                    sendInfo.msgtype = "news";
                                    sendInfo.touser = openId;
                                    sendInfo.articles = newsList;

                                    ResultEntity msgResultObj = commonService.SendMessage(sendInfo, appID, appSecret, loggingSessionInfo);
                                    // commonService.ResponseNewsMessage(WeiXin, openId, newsList, HttpContext.Current);
                                }
                                break;
                        }
                    }
                }
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format(
                    "完成SetPushQRCode: OpenID:{0}, customerId:{1}, userId:{2},  qrcode_id:{3}, WeiXin:{4}",
                    openId, customerId, userId, qrcode_id, WeiXin)
            });
        }

        /// <summary>
        /// 二维码扫描后预处理
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="weiXin"></param>
        /// <param name="userId"></param>
        /// <param name="openID"></param>
        /// <param name="qrcodeID"></param>
        private void QRSacnPreAction(LoggingSessionInfo loggingSessionInfo, string weiXin, string userId, string openID, string qrcodeID)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format(
                    "二维码扫描预处理QRSacnPreAction: OpenID:{0}, customerId:{1}, userId:{2},  qrcode_id:{3}, WeiXin:{4}",
                    openID, loggingSessionInfo.ClientID, userId, qrcodeID, weiXin)
            });
            WQRCodeManagerBLL qCodeManagerBll = new WQRCodeManagerBLL(loggingSessionInfo);
            WQRCodeManagerEntity qCodeManagerEntity = qCodeManagerBll.QueryByEntity(new WQRCodeManagerEntity
            {
                QRCode = qrcodeID
            }, null)[0];
            if (qCodeManagerEntity == null)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "二维码扫描预处理QRSacnPreAction，二维码ID不存在: OpenID:{0}, customerId:{1}, userId:{2},  qrcode_id:{3}, WeiXin:{4}",
                        openID, loggingSessionInfo.ClientID, userId, qrcodeID, weiXin)
                });
                return;
            }
            WQRCodeTypeOperatingMappingBLL wqrCodeTypeOperatingMappingBll = new WQRCodeTypeOperatingMappingBLL(loggingSessionInfo);
            WQRCodeTypeOperatingMappingEntity[] wqrCodeTypeOperatingMappingEntities =
                wqrCodeTypeOperatingMappingBll.QueryByEntity(new WQRCodeTypeOperatingMappingEntity
                {
                    WeiXinID = weiXin,
                    QRCodeTypeId = qCodeManagerEntity.QRCodeTypeId
                }, new[]
                    {
                        new OrderBy
                            {
                                FieldName = "DisplayIndex",
                                Direction = OrderByDirections.Asc
                            }
                    });
            if (wqrCodeTypeOperatingMappingEntities.Length > 0)
            {
                foreach (var wqrCodeTypeOperatingMappingEntity in wqrCodeTypeOperatingMappingEntities)
                {
                    //1: 添加签到记录；2：推送消息
                    switch (wqrCodeTypeOperatingMappingEntity.OperatingId)
                    {
                        //如果有多个case，扩展此项
                        case 1:
                            WEventUserMappingBLL eventUserMappingServer = new WEventUserMappingBLL(loggingSessionInfo);
                            WEventUserMappingEntity eventUserMappingInfo = new WEventUserMappingEntity();
                            LEventsBLL eventsBll = new LEventsBLL(loggingSessionInfo);

                            if (!string.IsNullOrEmpty(qCodeManagerEntity.ObjectId))
                            {
                                //避免重复签到
                                var eventUserEntity = eventUserMappingServer.Query(new IWhereCondition[] {
                                    new EqualsCondition() { FieldName="EventID", Value=qCodeManagerEntity.ObjectId},
                                    new EqualsCondition(){ FieldName="UserID", Value = userId }
                                }, null);

                                if (eventUserEntity.Length == 0)
                                {
                                    var eventInfo = eventsBll.GetByID(qCodeManagerEntity.ObjectId);
                                    if (eventInfo != null)
                                    {
                                        eventUserMappingInfo.Mapping = Common.Utils.NewGuid();
                                        eventUserMappingInfo.EventID = eventInfo.EventID;
                                        eventUserMappingInfo.UserID = userId;
                                        eventUserMappingServer.Create(eventUserMappingInfo);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 根据二维码类型替换模板中的变量
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="newsList">图文列表</param>
        /// <param name="qCodeTypeEntity">二维码类型实体</param>
        /// <param name="qCodeManagerEntity">二维码实体</param>
        private void ReplaceTemplate(LoggingSessionInfo loggingSessionInfo, List<NewsEntity> newsList, WQRCodeTypeEntity qCodeTypeEntity, WQRCodeManagerEntity qCodeManagerEntity)
        {
            switch (qCodeTypeEntity.TypeCode.ToLower())
            {
                //门店
                case "unit":
                    UnitService unitService = new UnitService(loggingSessionInfo);
                    var unit = unitService.GetUnitById(qCodeManagerEntity.ObjectId);
                    foreach (var materialTextEntity in newsList)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "UnitNAMe" + unit.Name + "UnitID:" + unit.Id + "objID:" + qCodeManagerEntity.ObjectId
                        });
                        materialTextEntity.description = materialTextEntity.description.Replace("$UNITNAME$", unit.Name);
                        materialTextEntity.title = materialTextEntity.title.Replace("$UNITNAME$", unit.Name);
                        ObjectImagesBLL imagesBll = new ObjectImagesBLL(loggingSessionInfo);
                        unit.ItemImageList = imagesBll.QueryByEntity(new ObjectImagesEntity { ObjectId = unit.Id }, new[] { new OrderBy { FieldName = "DisplayIndex", Direction = OrderByDirections.Asc } });
                        if (unit.ItemImageList.Count > 0)
                        {
                            materialTextEntity.picurl = unit.ItemImageList[0].ImageURL;
                        }
                        else if (!string.IsNullOrEmpty(unit.imageURL))
                        {
                            materialTextEntity.picurl = unit.imageURL;
                        }
                        materialTextEntity.url = materialTextEntity.url.Replace("$UNITID$", unit.Id);
                    }
                    break;
                //商品
                case "item":
                    ItemService itemService = new ItemService(loggingSessionInfo);
                    var item = itemService.GetItemInfoById(loggingSessionInfo, qCodeManagerEntity.ObjectId);
                    foreach (var materialTextEntity in newsList)
                    {
                        materialTextEntity.description = materialTextEntity.description.Replace("$ITEMNAME$", item.Item_Name);
                        materialTextEntity.title = materialTextEntity.title.Replace("$ITEMNAME$", item.Item_Name);
                        ObjectImagesBLL imagesBll = new ObjectImagesBLL(loggingSessionInfo);
                        item.ItemImageList = imagesBll.QueryByEntity(new ObjectImagesEntity { ObjectId = item.Item_Id }, new[] { new OrderBy { FieldName = "DisplayIndex", Direction = OrderByDirections.Asc } });
                        if (item.ItemImageList.Count > 0)
                        {
                            materialTextEntity.picurl = item.ItemImageList[0].ImageURL;
                        }
                        materialTextEntity.url = materialTextEntity.url.Replace("$ITEMID$", item.Item_Id);
                    }
                    break;
            }

        }

        /// <summary>
        /// 根据二维码类型替换模板中的变量
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="content">文本消息</param>
        /// <param name="qCodeTypeEntity">二维码类型实体</param>
        /// <param name="qCodeManagerEntity">二维码实体</param>
        private void ReplaceTemplate(LoggingSessionInfo loggingSessionInfo, ref string content, WQRCodeTypeEntity qCodeTypeEntity, WQRCodeManagerEntity qCodeManagerEntity)
        {
            switch (qCodeTypeEntity.TypeCode.ToLower())
            {
                //门店
                case "unit":
                    UnitService unitService = new UnitService(loggingSessionInfo);
                    var unit = unitService.GetUnitById(qCodeManagerEntity.ObjectId);
                    content = content.Replace("$UNITNAME$", unit.Name);
                    break;
                //商品
                case "item":
                    ItemService itemService = new ItemService(loggingSessionInfo);
                    var item = itemService.GetItemInfoById(loggingSessionInfo, qCodeManagerEntity.ObjectId);
                    content = content.Replace("$ITEMNAME$", item.Item_Name);
                    break;
            }
        }

        #endregion

        #region Jermyn插入活动签到 Jermyn20140530
        /// <summary>
        /// 插入活动签到
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="qrcode_id"></param>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        public void SetEventSign(string customerId, string userId, string eventId)
        {
            #region 活动
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            LEventsBLL eventServer = new LEventsBLL(loggingSessionInfo);
            LEventsEntity eventInfo = new LEventsEntity();
            DataSet ds = eventServer.GetEventInfo(eventId);
            if (ds != null && ds.Tables.Count > 0)
            {
                #region 获取门店需要退出的特殊信息
                //Jermyn20131209 添加用户与活动关系
                WEventUserMappingBLL eventUserMappingServer = new WEventUserMappingBLL(loggingSessionInfo);
                WEventUserMappingEntity eventUserMappingInfo = new WEventUserMappingEntity();
                eventUserMappingInfo.Mapping = Common.Utils.NewGuid();
                eventUserMappingInfo.EventID = eventId;
                eventUserMappingInfo.UserID = userId;
                eventUserMappingServer.Create(eventUserMappingInfo);

                #endregion

            }
            #endregion
        }
        #endregion
    }
}
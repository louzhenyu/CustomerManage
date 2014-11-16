using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity.WX;

namespace JIT.CPOS.Web.Pad
{
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["dataType"].ToString().Trim();
                switch (dataType)
                {
                    case "GetRecentfollowers"://近期关注的用户
                        content = GetRecentfollowers();
                        break;
                    case "GetVipInfoBySerialNumber": //根据流水号，获取信息
                        content = GetVipInfoBySerialNumber();
                        break;
                    case "SetVipInfoList":  //获取终端的用户
                        content = SetVipInfoList();
                        break;
                    case "SetVipIntegral":
                        content = SetVipIntegral();
                        break;
                    case "GetVipDetail":    //根据条件获取会员详细信息
                        content = GetVipDetail();
                        break;
                    case "SetVipIntegral111":
                        content = SetVipIntegral111();
                        break;
                    case "GetCouponInfo":   //获取优惠券信息
                        content = GetCouponInfo();
                        break;
                    case "SubmitCouponInfo":    //提交优惠券使用信息
                        content = SubmitCouponInfo();
                        break;
                    case "CouponPushMessage":    //优惠券消息推送
                        content = CouponPushMessage();
                        break;
                    case "SetOrderPush":    //主动给用户推送订单消息
                        content = SetOrderPush();
                        break;
                    case "GetOrderNo":      //获取订单号码(每次返回10个订单号)
                        content = GetOrderNo();
                        break;
                    case "GetOrderInfo":
                        content = GetOrderInfo();
                        break;
                    case "getEventTotalInfo":
                        content = getEventTotalInfo(); //3.2 活动实时信息
                        break;
                    #region 餐饮 20131010
                    case "GetTableNumberList":
                        content = GetTableNumberList(); //获取餐桌集合
                        break;
                    case "GetOrderInfoById":
                        content = GetOrderInfoById();   //获取订单根据订单标识
                        break;
                    case "setOrderStatus":              //修改订单状态
                        content = setOrderStatus();
                        break;
                    #endregion

                    case "GetImageList":
                        content = GetImageList();
                        break;
                    case "SetImageData":
                        content = SetImageData();
                        break;
                    case "GetUserMessageList":
                        content = GetUserMessageList();
                        break;
                    case "SetUserMessageData":
                        content = SetUserMessageData();
                        break;
                    case "getOrderList":     //获取各种状态的订单信息 
                        content = getOrderList();
                        break;
                    case "SetUserMessageDataByWap":
                        content = SetUserMessageDataByWap();
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

        #region 获取近期关注的用户信息
        /// <summary>
        /// 返回信息 获取近期关注的用户信息
        /// </summary>
        /// <returns></returns>
        public string GetRecentfollowers()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            GetResponseParams<IList<JIT.CPOS.BS.Entity.VipShowLogEntity>> response = new GetResponseParams<IList<VipShowLogEntity>>();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                string Weixin = Request["Weixin"].ToString().Trim();
                string TimeLength = Request["TimeLength"].ToString().Trim();
                VipShowLogBLL vipShowLogServer = new VipShowLogBLL(loggingSessionInfo);
                response.Params = vipShowLogServer.GetRecentfollowers(Weixin, TimeLength);
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"Vips\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            return content;

        }
        #endregion

        #region 根据流水号获取客户信息
        /// <summary>
        /// 根据流水号获取客户信息
        /// </summary>
        /// <returns></returns>
        public string GetVipInfoBySerialNumber()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            GetResponseParams<IList<JIT.CPOS.BS.Entity.VipShowLogEntity>> response = new GetResponseParams<IList<VipShowLogEntity>>();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                string SerialNumber = string.Empty; ;
                if (!string.IsNullOrEmpty(Request["SerialNumber"]))
                {
                    SerialNumber = Request["SerialNumber"].ToString().Trim();
                }
                VipShowLogBLL vipShowLogServer = new VipShowLogBLL(loggingSessionInfo);
                response.Params = vipShowLogServer.GetVipInfoBySerialNumber();
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"Vips\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            return content;

        }
        #endregion


        #region 接收pad端提交的会员补充信息后保存到数据库
        /// <summary>
        /// 接收pad端提交的会员补充信息后保存到数据库
        /// </summary>
        /// <returns></returns>
        public string SetVipInfoList()
        {
            string content = string.Empty;
            var respData = new RespData();
            try
            {
                var dataStream = Request.InputStream;
                var sr = new StreamReader(dataStream);
                var dataStr = sr.ReadToEnd();
                //string dataStr = Request["data"];
                //dataStr = "{\"UnitId\" : \"b35450d1510242c9811f1d242b864ab2\",\"customerId\" : \"090efab18fcc4e5e9bd41a31bfa084d5\",\"Vips\" : [{\"VipName\" : \"Test\",\"Qq\" : null,\"WeiXinUserId\" : \"o8Y7EjpXP-Dep8qeYnum7pX6vUP8\",\"VIPID\" : \"c789c251722d40ee889971d135e13f1f\",\"Phone\" : \"13681975556\",\"Gender\" : 1,\"Email\" : null,\"VipCode\" : \"VIPnestle25200001159851755\",\"Birthday\" : \"2010-09-23\"}],\"UserID\" : \"fc3be57c16ac47319a8a901cc381822e\"}";

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetVipInfoList:{0}", dataStr)
                });

                var data = dataStr.DeserializeJSONTo<SetVipInfoListReqData>();
                if (data == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "请求的数据不能为空";
                    return respData.ToJSON();
                }

                var loggingSessionInfo = Default.GetLoggingSession();
                //根据客户标识获取连接字符串  qianzhi  2013-07-30
                if (!string.IsNullOrEmpty(data.customerId))
                {
                    loggingSessionInfo = Default.GetBSLoggingSession(data.customerId.Trim(), "");
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetVipInfoList--loggingSessionInfo:{0}", loggingSessionInfo.ToJSON().ToString())
                    });
                }

                VipBLL vipService = new VipBLL(loggingSessionInfo);

                if (data != null)
                {
                    if (data.Vips != null && data.Vips.Count > 0)
                    {
                        foreach (var vip in data.Vips)
                        {
                            VipEntity vipEntity = vip;
                            //Jermyn20130911 从总部导入vip信息
                            bool bReturn = vipService.GetVipInfoFromApByOpenId(vip.WeiXinUserId, vip.VIPID);
                            ///////////////////////////////////////////////////////////////////////////////
                            VipEntity[] vipObj = { };
                            if (!string.IsNullOrEmpty(vip.WeiXinUserId)
                                || !string.IsNullOrEmpty(vip.VIPID)
                                || !string.IsNullOrEmpty(vip.Phone)
                                || !string.IsNullOrEmpty(vip.Email))
                            {
                                vipObj = vipService.QueryByEntity(new VipEntity()
                                {
                                    //WeiXinUserId = vip.WeiXinUserId
                                    //,
                                    VIPID = vip.VIPID
                                    //,Phone = vip.Phone
                                    //,Email = vip.Email
                                }, null);
                            }
                            if (vipObj == null || vipObj.Length == 0 || vipObj[0] == null || vipObj.Length > 1)
                            {
                                vipEntity.VIPID = vipEntity.VIPID == null || vipEntity.VIPID.Trim().Length == 0 ?
                                    Guid.NewGuid().ToString().Replace("-", string.Empty) : vipEntity.VIPID;
                                vipEntity.VipCode = vipService.GetVipCode();
                                vipEntity.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                                vipEntity.VipSourceId = "2";
                                vipEntity.Status = 1;
                                vipService.Create(vipEntity);

                                //Jermyn20130828 建立VIP与门店关系
                                #region
                                if (!string.IsNullOrEmpty(data.UnitId))
                                {
                                    string UnitId = data.UnitId.Trim();
                                    VipUnitMappingEntity vipUnitMappingInfo = new VipUnitMappingEntity();
                                    vipUnitMappingInfo.UnitId = UnitId;
                                    vipUnitMappingInfo.VipUnitID = Guid.NewGuid().ToString().Replace("-", string.Empty);
                                    vipUnitMappingInfo.VIPID = vipEntity.VIPID;
                                    vipUnitMappingInfo.CreateBy = data.UserID;
                                    vipUnitMappingInfo.UserId = data.UserID;
                                    VipUnitMappingBLL vipUnitMappingServer = new VipUnitMappingBLL(loggingSessionInfo);
                                    vipUnitMappingServer.Create(vipUnitMappingInfo);
                                }
                                #endregion
                            }
                            else
                            {
                                vipEntity.VipSourceId = "2";
                                vipService.Update(vipEntity, false);
                            }
                        }
                    }
                }
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

        public class SetVipInfoListReqData
        {
            public string UserID;
            public string customerId;
            public string UnitId;
            public IList<VipEntity> Vips;
        }
        public class RespData
        {
            public string Code = "200";
            public string Description = "操作成功";
            public string Exception = null;
            public string Data;
        }
        #endregion

        #region SetVipIntegral
        /// <summary>
        /// 计算积分增长数据，并更新积分值
        /// </summary>
        /// <returns></returns>
        public string SetVipIntegral()
        {
            string content = string.Empty;
            var respData = new RespData();
            try
            {
                var dataStream = Request.InputStream;
                var sr = new StreamReader(dataStream);
                var dataStr = sr.ReadToEnd();
                //string dataStr = Request["data"];
                //dataStr = "{ \"OpenID\":\"o8Y7EjpXP-Dep8qeYnum7pX6vUP8\", \"SalesAmount\":2877 }";
                //SetVipIntegral:{"OpenID" : "o8Y7Ejv3jR5fEkneCNu6N1_TIYIM","SalesAmount" : "0.01"}

                //dataStr = "{\"orderId\" : \"A7C43F9C90E7483584024E94073D737B\",\"customerId\" : \"090efab18fcc4e5e9bd41a31bfa084d5\",\"OpenID\" : \"o8Y7EjpXP-Dep8qeYnum7pX6vUP8\",\"SalesAmount\" : 768}";

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetVipIntegral:{0}", dataStr)
                });

                var currentUser = Default.GetLoggingSession();
                //根据客户标识获取连接字符串  qianzhi  2013-07-30
                if (!string.IsNullOrEmpty(Request["customerId"]))
                {
                    currentUser = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
                }
                VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(currentUser);
                VipBLL vipBLL = new VipBLL(currentUser);
                IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(currentUser);
                VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(currentUser);
                var data = dataStr.DeserializeJSONTo<SetVipIntegralReqData>();

                if (!string.IsNullOrEmpty(Request["orderId"]))
                {
                    string strError = string.Empty;
                    bool bReturnt = vipIntegralBLL.SetPushIntegral(Request["orderId"].ToString().Trim()
                                                        , ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim()
                                                        , out strError);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetVipIntegral-错误信息:{0}", strError)
                    });
                    if (!bReturnt)
                    {
                        respData.Code = "105";
                        respData.Description = strError;
                        return respData.ToJSON();
                    }
                }
                else
                {
                    #region
                    if (data == null)
                    {
                        respData.Code = "103";
                        respData.Description = "数据库操作错误";
                        respData.Exception = "请求的数据不能为空";
                        return respData.ToJSON();
                    }
                    if (data != null)
                    {
                        string integralSourceId = "1";
                        int integralValue = 0;
                        if (data.OpenID == null || data.OpenID.Trim().Length == 0)
                        {
                            respData.Code = "103";
                            respData.Description = "数据库操作错误";
                            respData.Exception = "OpenID不能为空";
                            return respData.ToJSON();
                        }

                        // 查询会员ID
                        VipEntity vipIdData = null;
                        var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                        {
                            WeiXinUserId = data.OpenID
                        }, null);
                        if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null)
                        {
                            if (data.OpenID == null || data.OpenID.Trim().Length == 0)
                            {
                                respData.Code = "103";
                                respData.Description = "数据库操作错误";
                                respData.Exception = "未查询到Vip会员";
                                return respData.ToJSON();
                            }
                        }
                        else
                        {
                            vipIdData = vipIdDataList[0];
                            data.VipID = vipIdData.VIPID;
                        }

                        // 计算积分
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
                            integralValue = System.Math.Abs(CPOS.Common.Utils.GetParseInt(integralRuleData.Integral) *
                                CPOS.Common.Utils.GetParseInt(data.SalesAmount));
                        }

                        // 插入积分明细
                        VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                        vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                        vipIntegralDetailEntity.VIPID = data.VipID;
                        vipIntegralDetailEntity.FromVipID = data.VipID;
                        vipIntegralDetailEntity.SalesAmount = data.SalesAmount;
                        vipIntegralDetailEntity.Integral = integralValue;
                        vipIntegralDetailEntity.IntegralSourceID = integralSourceId;
                        vipIntegralDetailEntity.IsAdd = 1;
                        //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                        // 更新积分
                        VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                        var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                            new VipIntegralEntity() { VipID = data.VipID }, null);
                        if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                        {
                            vipIntegralEntity.VipID = data.VipID;
                            vipIntegralEntity.BeginIntegral = 0; // 期初积分
                            vipIntegralEntity.InIntegral = 0; // 增加积分
                            vipIntegralEntity.OutIntegral = integralValue; //消费积分
                            vipIntegralEntity.EndIntegral = integralValue; //积分余额
                            vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                            vipIntegralEntity.ValidIntegral = integralValue; // 当前有效积分
                            //vipIntegralBLL.Create(vipIntegralEntity);
                        }
                        else
                        {
                            vipIntegralEntity.VipID = data.VipID;
                            //vipIntegralEntity.InIntegral = 0; // 增加积分
                            vipIntegralEntity.OutIntegral = Common.Utils.GetDecimalVal(
                                vipIntegralDataList[0].OutIntegral) + integralValue; //消费积分
                            vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
                                vipIntegralDataList[0].EndIntegral) + integralValue; //积分余额
                            //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                            vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
                                vipIntegralDataList[0].ValidIntegral) + integralValue; // 当前有效积分
                            //vipIntegralBLL.Update(vipIntegralEntity, false);
                        }

                        // 更新VIP
                        VipEntity vipEntity = new VipEntity();
                        var vipEntityDataList = vipBLL.QueryByEntity(
                            new VipEntity() { VIPID = data.VipID }, null);
                        if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                        {
                            vipEntity.VIPID = data.VipID;
                            //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                            vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                            vipEntity.Status = 1;
                            vipBLL.Create(vipEntity);
                        }
                        else
                        {
                            vipEntity.VIPID = data.VipID;
                            //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                            vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                            vipBLL.Update(vipEntity, false);
                        }

                        // 推送消息
                        var strValidIntegral = string.Empty;
                        if (vipIntegralEntity.ValidIntegral == null)
                        {
                            strValidIntegral = "0";
                        }
                        else
                        {
                            decimal vd = (decimal)vipIntegralEntity.ValidIntegral;
                            strValidIntegral = Convert.ToString(decimal.Truncate(vd));
                        }

                        string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                        string msgText = string.Format("感谢您来店消费。您刚刚消费共计{1}元，新增积分{0}，积分累计为{2}。欢迎您下次光临。",
                            Convert.ToString(integralValue), data.SalesAmount.ToString("0.0"), System.Math.Abs(CPOS.Common.Utils.GetParseInt(vipIntegralEntity.ValidIntegral)));
                        string msgData = "<xml><OpenID><![CDATA[" + data.OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                        //推送刮奖消息
                        //msgText = "您赢得了一次在线抽奖机会，<a href='http://xxxx:9004/wap/weixin/luckyDraw.html'>点击参与刮奖</a>";
                        //msgData = "<xml><OpenID><![CDATA[" + data.OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        //msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("PushMsgResult:{0}", msgResult)
                        });
                    #endregion
                    }
                    GetHightOpenInfo(data); //Jermyn20130517给上家添加积分
                    //respData.Data = vipIntegralEntity.ValidIntegral.ToString();

                }
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


        public string SetVipIntegral111()
        {
            string content = string.Empty;
            var respData = new RespData();
            try
            {
                var currentUser = Default.GetLoggingSession();
                //根据客户标识获取连接字符串  qianzhi  2013-07-30
                if (!string.IsNullOrEmpty(Request["customerId"]))
                {
                    currentUser = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
                }

                VipBLL vipBLL = new VipBLL(currentUser);
                IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(currentUser);
                VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(currentUser);
                VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(currentUser);
                var data = new SetVipIntegralReqData();
                data.OpenID = "o8Y7Ejv3jR5fEkneCNu6N1_TIYIM";
                data.SalesAmount = Convert.ToDecimal(0.01);
                if (data == null)
                {
                    respData.Code = "103";
                    respData.Description = "数据库操作错误";
                    respData.Exception = "请求的数据不能为空";
                    return respData.ToJSON();
                }
                if (data != null)
                {
                    string integralSourceId = "1";
                    decimal integralValue = 0;
                    if (data.OpenID == null || data.OpenID.Trim().Length == 0)
                    {
                        respData.Code = "103";
                        respData.Description = "数据库操作错误";
                        respData.Exception = "OpenID不能为空";
                        return respData.ToJSON();
                    }

                    // 查询会员ID
                    VipEntity vipIdData = null;
                    var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId = data.OpenID
                    }, null);
                    if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null)
                    {
                        if (data.OpenID == null || data.OpenID.Trim().Length == 0)
                        {
                            respData.Code = "103";
                            respData.Description = "数据库操作错误";
                            respData.Exception = "未查询到Vip会员";
                            return respData.ToJSON();
                        }
                    }
                    else
                    {
                        vipIdData = vipIdDataList[0];
                        data.VipID = vipIdData.VIPID;
                    }

                    // 计算积分
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
                        integralValue = CPOS.Common.Utils.GetDecimalVal(integralRuleData.Integral) *
                            CPOS.Common.Utils.GetDecimalVal(data.SalesAmount);
                    }

                    // 插入积分明细
                    VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                    vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                    vipIntegralDetailEntity.VIPID = data.VipID;
                    vipIntegralDetailEntity.FromVipID = data.VipID;
                    vipIntegralDetailEntity.SalesAmount = data.SalesAmount;
                    vipIntegralDetailEntity.Integral = integralValue;
                    vipIntegralDetailEntity.IntegralSourceID = integralSourceId;
                    vipIntegralDetailEntity.IsAdd = 1;
                    //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                    // 更新积分
                    VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                    var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                        new VipIntegralEntity() { VipID = data.VipID }, null);
                    if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                    {
                        vipIntegralEntity.VipID = data.VipID;
                        vipIntegralEntity.BeginIntegral = 0; // 期初积分
                        vipIntegralEntity.InIntegral = 0; // 增加积分
                        vipIntegralEntity.OutIntegral = integralValue; //消费积分
                        vipIntegralEntity.EndIntegral = integralValue; //积分余额
                        vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = integralValue; // 当前有效积分
                        //vipIntegralBLL.Create(vipIntegralEntity);
                    }
                    else
                    {
                        vipIntegralEntity.VipID = data.VipID;
                        //vipIntegralEntity.InIntegral = 0; // 增加积分
                        vipIntegralEntity.InIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].InIntegral) + integralValue; //消费积分
                        vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].EndIntegral) + integralValue; //积分余额
                        //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                        vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].ValidIntegral) + integralValue; // 当前有效积分
                        //vipIntegralBLL.Update(vipIntegralEntity, false);
                    }

                    // 更新VIP
                    VipEntity vipEntity = new VipEntity();
                    var vipEntityDataList = vipBLL.QueryByEntity(
                        new VipEntity() { VIPID = data.VipID }, null);
                    if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                    {
                        vipEntity.VIPID = data.VipID;
                        //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                        vipEntity.VipSourceId = "2";
                        vipEntity.Status = 1;
                        vipBLL.Create(vipEntity);
                    }
                    else
                    {
                        vipEntity.VIPID = data.VipID;
                        //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                        vipBLL.Update(vipEntity, false);
                    }

                    // 推送消息
                    string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                    string msgText = string.Format("感谢您来店消费。您刚刚消费共计{1}元，新增积分{0}，积分累计为{2}。欢迎您下次光临。",
                        integralValue, data.SalesAmount, vipIntegralEntity.ValidIntegral);
                    string msgData = "<xml><OpenID><![CDATA[" + data.OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                    var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMsgResult:{0}", msgResult)
                    });
                    GetHightOpenInfo(data); //Jermyn20130517给上家添加积分
                    respData.Data = vipIntegralEntity.ValidIntegral.ToString();
                }
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

        public class SetVipIntegralReqData
        {
            public string OpenID;
            public string VipID;
            public decimal SalesAmount;
            public string IntegralSourceId;
        }
        #endregion

        #region 获取会员详细信息
        /// <summary>
        /// 会员详细信息
        /// </summary>
        /// <returns></returns>
        public string GetVipDetail()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<VipEntity> response = new GetResponseParams<VipEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                string Weixin = Request["Weixin"].ToString().Trim();
                VipBLL vipBll = new VipBLL(loggingSessionInfo);
                response.Params = vipBll.GetVipDetail(Weixin);
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"Vip\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            return content;
        }
        #endregion

        #region 获取登录用户信息
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private LoggingSessionInfo GetLoggingSession(string customerId, string token)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, token);
            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = loggingSessionInfo.CurrentUser.customer_id;
            return loggingSessionInfo;
        }
        private LoggingSessionInfo GetLoggingSession()
        {
            return GetLoggingSession(
                ConfigurationManager.AppSettings["customer_id"].Trim(),
                ConfigurationManager.AppSettings["token"].Trim());
        }
        #endregion

        #region Jermyn20130517处理消费时，下家消费成功，上家获取积分
        /// <summary>
        /// 处理上家消费
        /// </summary>
        /// <param name="reqInfo"></param>
        private void GetHightOpenInfo(SetVipIntegralReqData reqInfo)
        {
            //获取登录用户信息
            var currentUser = Default.GetLoggingSession();
            //获取vip类
            VipBLL vipBLL = new VipBLL(currentUser);
            //获取积分规则类
            IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(currentUser);
            //获取vip积分明细类
            VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(currentUser);
            //获取vip积分类
            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(currentUser);

            if (reqInfo != null)
            {
                string integralSourceId = "9";
                decimal integralValue = 0;

                // 查询会员ID
                VipEntity vipIdData = null;
                var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqInfo.OpenID
                }, null);

                if (vipIdDataList != null && vipIdDataList.Length > 0 && vipIdDataList[0].HigherVipID != null && !vipIdDataList[0].HigherVipID.Equals(""))
                {
                    var vipIdDataList1 = vipBLL.QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId = vipIdDataList[0].HigherVipID
                    }, null);

                    if (vipIdDataList1 == null && vipIdDataList1.Length == 0 && vipIdDataList1[0] == null)
                    {
                        return;
                    }
                    else
                    {
                        vipIdData = vipIdDataList1[0];
                        reqInfo.VipID = vipIdData.VIPID;
                        reqInfo.OpenID = vipIdData.WeiXinUserId;

                        #region 计算积分
                        IntegralRuleEntity integralRuleData = null;
                        var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                        {
                            IntegralSourceID = integralSourceId
                        }, null);
                        if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                        {
                            return;
                        }
                        else
                        {
                            integralRuleData = integralRuleDataList[0];
                            integralValue = CPOS.Common.Utils.GetDecimalVal(integralRuleData.Integral) * Convert.ToInt32(reqInfo.SalesAmount);
                        }
                        #endregion
                        #region 插入积分明细
                        VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                        vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                        vipIntegralDetailEntity.VIPID = reqInfo.VipID;
                        vipIntegralDetailEntity.SalesAmount = reqInfo.SalesAmount;
                        vipIntegralDetailEntity.Integral = integralValue;
                        vipIntegralDetailEntity.IntegralSourceID = integralSourceId;
                        vipIntegralDetailEntity.IsAdd = 1;
                        vipIntegralDetailEntity.FromVipID = reqInfo.VipID;
                        //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);
                        #endregion

                        #region 更新积分
                        VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                        var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                            new VipIntegralEntity() { VipID = reqInfo.VipID }, null);
                        if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                        {
                            vipIntegralEntity.VipID = reqInfo.VipID;
                            vipIntegralEntity.BeginIntegral = 0; // 期初积分
                            vipIntegralEntity.InIntegral = integralValue; // 增加积分
                            //vipIntegralEntity.OutIntegral = integralValue; //消费积分
                            vipIntegralEntity.EndIntegral = integralValue; //积分余额
                            vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                            vipIntegralEntity.ValidIntegral = integralValue; // 当前有效积分
                            //vipIntegralBLL.Create(vipIntegralEntity);
                        }
                        else
                        {
                            vipIntegralEntity.VipID = reqInfo.VipID;

                            vipIntegralEntity.InIntegral = Common.Utils.GetDecimalVal(
                                vipIntegralDataList[0].InIntegral) + integralValue; //增加积分
                            vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
                                vipIntegralDataList[0].EndIntegral) + integralValue; //积分余额
                            //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
                            vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
                                vipIntegralDataList[0].ValidIntegral) + integralValue; // 当前有效积分
                            //vipIntegralBLL.Update(vipIntegralEntity, false);
                        }
                        #endregion

                        #region 更新VIP
                        VipEntity vipEntity = new VipEntity();
                        var vipEntityDataList = vipBLL.QueryByEntity(
                            new VipEntity() { VIPID = reqInfo.VipID }, null);
                        if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                        {
                            vipEntity.VIPID = reqInfo.VipID;
                            //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                            vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                            vipEntity.VipSourceId = "2";
                            vipEntity.Status = 1;
                            vipBLL.Create(vipEntity);
                        }
                        else
                        {
                            vipEntity.VIPID = reqInfo.VipID;
                            //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                            vipEntity.ClientID = currentUser.CurrentUser.customer_id;
                            vipBLL.Update(vipEntity, false);
                        }
                        #endregion

                        #region 推送消息
                        string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                        string msgText = string.Format("您推荐的会员刚刚购买了我店商品，为了表示感谢。我们送您积分{0}分。",
                            Convert.ToInt32(integralValue));
                        string msgData = "<xml><OpenID><![CDATA[" + reqInfo.OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #endregion

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("PushMsgResult:{0}", msgResult)
                        });

                    }
                }

            }
        }
        #endregion

        #region 获取优惠券信息

        /// <summary>
        /// 获取优惠券信息
        /// </summary>
        /// <returns></returns>
        public string GetCouponInfo()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }

            string content = string.Empty;
            string status = "2";    //1：可正常使用  2：不可使用
            string title = "";      //优惠券描述
            string discount = "0.95";   //折扣

            GetResponseParams<bool> response = new GetResponseParams<bool>();
            response.Code = "200";
            response.Description = "操作成功";

            try
            {
                string couponInfo = Request["CouponInfo"].ToString().Trim();
                BaseService.WriteLog("获取优惠券信息GetCouponInfo()");
                BaseService.WriteLog("couponInfo：" + couponInfo);

                VipBLL vipServer = new VipBLL(loggingSessionInfo);

                IWhereCondition[] whereCondition = new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "CouponInfo", Value = couponInfo }
                };

                var vips = vipServer.Query(whereCondition, null);

                status = (vips.Length > 0) ? "1" : "2";
                title = (vips.Length > 0) ? "优惠券可抵用" : "优惠券不可使用";
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }

            content = string.Format("{{\"Description\":\"{0}\",\"Code\":\"{1}\",\"Status\":\"{2}\",\"Discount\":\"{3}\",\"Title\":\"{4}\"}}",
                response.Description, response.Code, status, discount, title);
            return content;
        }

        #endregion

        #region 提交优惠券使用信息

        /// <summary>
        /// 提交优惠券使用信息
        /// </summary>
        /// <returns></returns>
        public string SubmitCouponInfo()
        {
            var respData = new Default.RespData();
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }

            string openId = Request["OpenId"].Trim();
            string couponInfo = System.Web.HttpUtility.UrlDecode(Request["CouponInfo"].Trim());
            string imgUrl = string.Empty;

            if (openId == null || openId.Length == 0 ||
                couponInfo == null || couponInfo.Length == 0)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求参数不能为空";
                return respData.ToJSON();
            }

            BaseService.WriteLog("提交优惠券使用信息SubmitCouponInfo()");
            BaseService.WriteLog("OpenId：" + openId);
            BaseService.WriteLog("CouponInfo：" + couponInfo);

            var coupons = couponInfo.Split(',');

            //通过二维码字符串获取优惠券图片URL
            VipBLL vipBLL = new VipBLL(loggingSessionInfo);
            var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
            {
                CouponInfo = coupons[0]
            }, null);
            if (vipIdDataList.Length > 0)
            {
                imgUrl = vipIdDataList.FirstOrDefault().CouponURL;
            }

            BaseService.WriteLog("imgUrl：" + imgUrl);

            //上传优惠券
            string webUrl = ConfigurationManager.AppSettings["website_url"];
            string uri = webUrl + "WeiXin/data.aspx?dataType=GetCouponImageText&openId=" + openId + "&imgUrl=" + imgUrl;
            string method = "GET";
            string data = Common.Utils.GetRemoteData(uri, method, string.Empty);

            BaseService.WriteLog("返回值：" + data);

            return data.ToJSON();
        }

        #endregion

        #region 优惠券消息推送

        /// <summary>
        /// 优惠券消息推送
        /// </summary>
        /// <returns></returns>
        public string CouponPushMessage()
        {
            var respData = new Default.RespData();
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }

            string weiXinId = Request["WeiXinId"].Trim();
            string openId = Request["OpenId"].Trim();
            string brandName = Request["BrandName"].Trim();

            if (string.IsNullOrEmpty(Request["WeiXinId"]) ||
                string.IsNullOrEmpty(Request["OpenId"]) ||
                string.IsNullOrEmpty(Request["BrandName"]))
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求参数不能为空";
                return respData.ToJSON();
            }

            BaseService.WriteLog("优惠券消息推送CouponPushMessage()");
            BaseService.WriteLog("WeiXinId：" + weiXinId);
            BaseService.WriteLog("OpenId：" + openId);
            BaseService.WriteLog("BrandName：" + brandName);

            //推送消息

            string content = string.Empty;
            try
            {
                string vipID = string.Empty;
                string vipName = string.Empty;

                #region 获取VIP信息

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = Request["OpenId"],
                    WeiXin = Request["WeiXinId"]
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.Code = "103";
                    respData.Description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                    vipName = vipList.FirstOrDefault().VipName;
                }

                #endregion
                string webUrl = ConfigurationManager.AppSettings["website_url"];
                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format("亲爱的{0}，欢迎您加入我们{1}的大家庭，我们为您准备了全场通用的20元现金券，请点击链接领取。" + webUrl + "OnlineShopping/", vipName, brandName);
                string msgData = "<xml><OpenID><![CDATA[" + openId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult:{0}", msgResult)
                });
            }
            catch (Exception ex)
            {
                respData.Code = "201";
                respData.Description = "操作失败";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region SetOrderPush 主动给用户推送订单消息

        /// <summary>
        /// 主动给用户推送订单消息
        /// </summary>
        /// <returns></returns>
        public string SetOrderPush()
        {
            var respData = new RespData();
            if (string.IsNullOrEmpty(Request["WeiXinId"]) ||
                string.IsNullOrEmpty(Request["OpenId"]) ||
                string.IsNullOrEmpty(Request["OrdeNo"]))
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求的数据不能为空";
                return respData.ToJSON();
            }

            string content = string.Empty;
            try
            {
                string vipID = string.Empty;
                string vipName = string.Empty;
                var loggingSessionInfo = Default.GetLjLoggingSession();
                //根据客户标识获取连接字符串  qianzhi  2013-07-30
                if (!string.IsNullOrEmpty(Request["customerId"]))
                {
                    loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
                }

                #region 获取VIP信息

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = Request["OpenId"],
                    WeiXin = Request["WeiXinId"]
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.Code = "103";
                    respData.Description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                    vipName = vipList.FirstOrDefault().VipName;
                }

                #endregion

                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format("亲爱的会员{1}，您单号为{0}的购买请求我们已经收到，请您到指定渠道下交纳钱款。谢谢再次惠顾！", Request["OrdeNo"], vipName);
                string msgData = "<xml><OpenID><![CDATA[" + Request["OpenId"] + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

                #region 发送日志
                MarketSendLogBLL logServer = new MarketSendLogBLL(loggingSessionInfo);
                MarketSendLogEntity logInfo = new MarketSendLogEntity();
                logInfo.LogId = BaseService.NewGuidPub();
                logInfo.IsSuccess = 1;
                logInfo.MarketEventId = Request["OrdeNo"];
                logInfo.SendTypeId = "2";
                logInfo.TemplateContent = msgData;
                logInfo.VipId = vipID;
                logInfo.WeiXinUserId = Request["OpenId"];
                logInfo.CreateTime = System.DateTime.Now;
                logServer.Create(logInfo);
                #endregion

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult:{0}", msgResult)
                });
            }
            catch (Exception ex)
            {
                respData.Code = "201";
                respData.Description = "操作失败";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #endregion

        #region GetOrderNo 获取订单号码(每次返回10个订单号)
        /// <summary>
        /// 获取订单号码(每次返回10个订单号)
        /// </summary>
        /// <returns></returns>
        public string GetOrderNo()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<GetOrderNoEntity> response = new GetResponseParams<GetOrderNoEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            response.Params = new GetOrderNoEntity();
            response.Params.orderNos = new List<OrderNoEntity>();

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"]))
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"orderNos\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识

                TUnitExpandBLL service = new TUnitExpandBLL(loggingSessionInfo);
                int orderNo = service.GetOrderNo(loggingSessionInfo, unitId, 10);

                OrderNoEntity orderEntity = null;
                for (int i = 0; i < 10; i++)
                {
                    orderEntity = new OrderNoEntity();
                    var len = orderNo.ToString().Length;
                    switch (len)
                    {
                        case 1: orderEntity.orderNo = "000" + orderNo; break;
                        case 2: orderEntity.orderNo = "00" + orderNo; break;
                        case 3: orderEntity.orderNo = "0" + orderNo; break;
                        case 4: orderEntity.orderNo = orderNo.ToString(); break;
                    }

                    response.Params.orderNos.Add(orderEntity);

                    orderNo++;
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"orderNos\":{0}}}",
                response.Params.orderNos.ToJSON(), response.Code, response.Description);
            return content;
        }

        public class GetOrderNoEntity
        {
            public IList<OrderNoEntity> orderNos { get; set; }
        }
        public class OrderNoEntity
        {
            public string orderNo { get; set; }
        }
        #endregion

        #region GetOrderInfo
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns></returns>
        public string GetOrderInfo()
        {
            string content = string.Empty;
            GetResponseParams<GetOrderInfoEntity> response = new GetResponseParams<GetOrderInfoEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            response.Params = new GetOrderInfoEntity();

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"]) ||
                string.IsNullOrEmpty(Request["OrderNo"]))
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\"}}",
                    "", response.Code, response.Description);
            }

            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识
                string orderNo = Request["OrderNo"].ToString().Trim();    //订单号

                InoutService inoutService = new InoutService(loggingSessionInfo);
                //TUnitExpandBLL service = new TUnitExpandBLL(loggingSessionInfo);
                //var unitExpandEntity = new TUnitExpandEntity() { UnitId = unitId };
                //var unitExpandEntitys = service.QueryByEntity(unitExpandEntity, null).ToList();
                //if (unitExpandEntitys != null && unitExpandEntitys.Count > 0)
                //{
                var orderId = inoutService.GetInoutId(new InoutInfo()
                {
                    sales_unit_id = unitId,
                    //customer_id = customerId,
                    order_type_id = "1F0A100C42484454BAEA211D4C14B80F",
                    order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5",
                    red_flag = "1",
                    Field16 = orderNo
                });
                if (orderId != null && orderId.Trim().Length > 0)
                {
                    response.Params.order = inoutService.GetInoutInfoById(orderId);
                }
                //}
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"content\":{0}}}",
                response.Params.order.ToJSON(), response.Code, response.Description);
            return content;
        }
        public class GetOrderInfoEntity
        {
            public InoutInfo order { get; set; }
        }
        #endregion

        #region getEventTotalInfo 3.2 活动实时信息
        public string getEventTotalInfo()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<EventTotalInfo> response = new GetResponseParams<EventTotalInfo>();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                string WeiXinId = Request["WeiXinId"].ToString().Trim();
                string EventId = Request["EventId"].ToString().Trim();
                LEventsEntity eventInfo = new LEventsEntity();
                LEventsBLL inoutServer = new LEventsBLL(loggingSessionInfo);
                string strError = string.Empty;
                eventInfo = inoutServer.GetEventTotalInfo(WeiXinId, EventId, loggingSessionInfo, out strError);
                if (eventInfo == null)
                {
                    response.Code = "201";
                    response.Description = strError;
                }
                else
                {
                    EventTotalInfo eventTotalInfo = new EventTotalInfo();
                    eventTotalInfo.hasOrderCount = eventInfo.hasOrderCount;
                    eventTotalInfo.hasPayCount = eventInfo.hasPayCount;
                    eventTotalInfo.hasSalesAmount = eventInfo.hasSalesAmount;
                    eventTotalInfo.hasVipCount = eventInfo.hasVipCount;
                    eventTotalInfo.newVipCount = eventInfo.newVipCount;
                    response.Params = eventTotalInfo;
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"Vip\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            return content;
        }

        public class EventTotalInfo
        {
            /// <summary>
            /// 已关注会员
            /// </summary>
            public int hasVipCount { get; set; }
            /// <summary>
            /// 新采集会员
            /// </summary>
            public int newVipCount { get; set; }
            /// <summary>
            /// 已下订单数
            /// </summary>
            public int hasOrderCount { get; set; }
            /// <summary>
            /// 已付款订单数
            /// </summary>
            public int hasPayCount { get; set; }
            /// <summary>
            /// 已销售订单额
            /// </summary>
            public decimal hasSalesAmount { get; set; }
        }
        #endregion

        #region  5 酒店接口

        #region GetTableNumberList 5.1 获取餐桌集合
        /// <summary>
        /// 获取餐桌集合
        /// </summary>
        /// <returns></returns>
        public string GetTableNumberList()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<GetTableNumberEntityContent> response = new GetResponseParams<GetTableNumberEntityContent>();
            response.Code = "200";
            response.Description = "操作成功";
            response.Params = new GetTableNumberEntityContent();
            response.Params.tableNumberList = new List<GetTableNumberEntity>();

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"]))
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"tableNumberList\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识

                InoutService service = new InoutService(loggingSessionInfo);
                DataSet ds = service.GetTableNumberList(customerId, unitId);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    response.Params.tableNumberList = DataTableToObject.ConvertToList<GetTableNumberEntity>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"tableNumberList\":{0}}}",
                response.Params.tableNumberList.ToJSON(), response.Code, response.Description);
            return content;
        }

        public class GetTableNumberEntityContent
        {
            public IList<GetTableNumberEntity> tableNumberList { get; set; }
        }
        public class GetTableNumberEntity
        {
            public string tableNumber { get; set; } // 桌号
            public string status { get; set; } // 状态（1：点餐中，2：空闲，3：已下单）
            public string statusDesc { get; set; } // 状态描述
            public string orderId { get; set; } // 订单标识
            public string orderCode { get; set; } // 订单号码

        }
        #endregion

        #region setOrderStatus 5.2 下单确认
        /// <summary>
        /// 获取餐桌集合
        /// </summary>
        /// <returns></returns>
        public string setOrderStatus()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<setOrderStatusEntity> response = new GetResponseParams<setOrderStatusEntity>();
            response.Code = "200";
            response.Description = "操作成功";

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"])
                || string.IsNullOrEmpty(Request["OrderId"])
                || string.IsNullOrEmpty(Request["Status"])
                )
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"content\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识
                string orderId = Request["OrderId"].ToString().Trim();
                string status = Request["Status"].ToString().Trim();
                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                JIT.CPOS.BS.Entity.Interface.SetOrderEntity orderInfo = new JIT.CPOS.BS.Entity.Interface.SetOrderEntity();
                orderInfo.OrderId = orderId;
                orderInfo.Status = status;
                orderInfo.StatusDesc = "已下单";
                #endregion
                string strError = string.Empty;
                bool bReturn = service.SetOrderPayment(orderInfo, out strError);
                if (!bReturn)
                {
                    response.Code = "201";
                    response.Description = strError;
                }

            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"content\":{0}}}",
                "11", response.Code, response.Description);
            return content;
        }

        public class setOrderStatusEntity
        {
            // public IList<GetTableNumberEntity> tableNumberList { get; set; }
        }

        #endregion

        #region GetOrderInfoById 5.3 根据订单标识获取订单
        /// <summary>
        /// 根据订单标识获取订单
        /// </summary>
        /// <returns></returns>
        public string GetOrderInfoById()
        {
            string content = string.Empty;
            GetResponseParams<GetOrderInfoByIdEntity> response = new GetResponseParams<GetOrderInfoByIdEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            response.Params = new GetOrderInfoByIdEntity();

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"]) ||
                string.IsNullOrEmpty(Request["OrderId"]))
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\"}}",
                    "", response.Code, response.Description);
            }

            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识
                string OrderId = Request["OrderId"].ToString().Trim();    //订单号

                InoutService inoutService = new InoutService(loggingSessionInfo);

                //var orderId = inoutService.GetInoutId(new InoutInfo()
                //{
                //    order_id = OrderId
                //});
                if (OrderId != null && OrderId.Trim().Length > 0)
                {
                    response.Params.order = inoutService.GetInoutInfoById(OrderId);
                }
                //}
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"content\":{0}}}",
                response.Params.order.ToJSON(), response.Code, response.Description);
            return content;
        }
        public class GetOrderInfoByIdEntity
        {
            public InoutInfo order { get; set; }
        }
        #endregion

        #endregion

        #region 获取图片集合
        /// <summary>
        /// 获取图片集合
        /// </summary>
        /// <returns></returns>
        public string GetImageList()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            GetImageListEntity response = new GetImageListEntity();
            response.content = new GetImageListContentEntity();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                ObjectImagesEntity queryObj = new ObjectImagesEntity();
                queryObj.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                queryObj.PageSize = Request["PageSize"] == null ? 20 :
                    Convert.ToInt32(Request["PageSize"].ToString().Trim());
                queryObj.DisplayIndexLast = Request["displayIndexLast"] == null || Request["displayIndexLast"].Trim().Length == 0 ?
                    0 : Convert.ToInt64(Request["displayIndexLast"].ToString().Trim());
                queryObj.timestamp = Request["timestamp"] == null ? "" : Request["timestamp"].ToString().Trim();

                ObjectImagesBLL objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
                response.content.imageList = objectImagesBLL.GetClientImageList(queryObj);
                response.content.totalCount = objectImagesBLL.GetClientImageListCount(queryObj);

                content = response.ToJSON();
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
                content = response.ToJSON();
            }
            return content;

        }
        public class GetImageListEntity : RespData
        {
            public GetImageListContentEntity content { get; set; }
        }
        public class GetImageListContentEntity : RespData
        {
            public int totalCount { get; set; }
            public IList<ObjectImagesEntity> imageList { get; set; }
        }
        #endregion

        #region 更新图片数据接口
        /// <summary>
        /// 更新图片数据接口
        /// </summary>
        /// <returns></returns>
        public string SetImageData()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            SetImageDataEntity response = new SetImageDataEntity();
            response.content = new SetImageDataContentEntity();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                ObjectImagesEntity queryObj = new ObjectImagesEntity();
                queryObj.ImageId = Request["ImageId"];
                queryObj.ObjectId = Request["ObjectId"];
                queryObj.ImageURL = Request["ImageURL"];
                queryObj.DisplayIndex = Request["DisplayIndex"] == null || Request["DisplayIndex"].Trim().Length == 0 ?
                    0 : Convert.ToInt32(Request["DisplayIndex"]);
                queryObj.CreateBy = Request["UserId"];
                queryObj.IsDelete = Request["IsDelete"] == null || Request["IsDelete"].Trim().Length == 0 ?
                    0 : Convert.ToInt32(Request["IsDelete"]);

                ObjectImagesBLL objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);

                if (queryObj.ImageId == null || queryObj.ImageId.Length == 0)
                {
                    queryObj.ImageId = Utils.NewGuid();
                    queryObj.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                    queryObj.CreateBy = Request["UserId"];
                    queryObj.CreateTime = DateTime.Now;
                    queryObj.IsDelete = 0;
                    objectImagesBLL.Create(queryObj);
                }
                else if (queryObj.IsDelete == 0)
                {
                    var tmpObj = objectImagesBLL.GetByID(queryObj.ImageId);
                    if (tmpObj == null)
                    {
                        queryObj.ImageId = Utils.NewGuid();
                        queryObj.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                        queryObj.CreateBy = Request["UserId"];
                        queryObj.CreateTime = DateTime.Now;
                        queryObj.IsDelete = 0;
                        objectImagesBLL.Create(queryObj);
                    }
                    else
                    {
                        queryObj.LastUpdateBy = Request["UserId"];
                        queryObj.LastUpdateTime = DateTime.Now;
                        objectImagesBLL.Update(queryObj, false);
                    }
                }
                else if (queryObj.IsDelete == 1)
                {
                    queryObj.LastUpdateBy = Request["UserId"];
                    queryObj.LastUpdateTime = DateTime.Now;
                    objectImagesBLL.Delete(queryObj);
                }

                response.content.ImageId = queryObj.ImageId;

                content = response.ToJSON();
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
                content = response.ToJSON();
            }
            return content;

        }
        public class SetImageDataEntity : RespData
        {
            public SetImageDataContentEntity content { get; set; }
        }
        public class SetImageDataContentEntity : RespData
        {
            public string ImageId { get; set; }
        }
        #endregion

        #region 获取微信平台用户留言集合
        /// <summary>
        /// 获取微信平台用户留言集合
        /// </summary>
        /// <returns></returns>
        public string GetUserMessageList()
        {
            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            GetUserMessageListEntity response = new GetUserMessageListEntity();
            response.content = new GetUserMessageListContentEntity();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                WUserMessageEntity queryObj = new WUserMessageEntity();
                queryObj.DataFrom = 1;
                if (!string.IsNullOrEmpty(Request["unitId"]))
                {
                    queryObj.UnitId = Request["unitId"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(Request["userId"]))
                {
                    queryObj.CreateBy = Request["userId"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(Request["vipType"]))
                {
                    queryObj.ToVipType = int.Parse(Request["vipType"].ToString().Trim());
                }
                queryObj.PageSize = Request["PageSize"] == null ? 20 :
                    Convert.ToInt32(Request["PageSize"].ToString().Trim());
                queryObj.DisplayIndexLast = Request["displayIndexLast"] == null || Request["displayIndexLast"].Trim().Length == 0 ?
                    -1 : Convert.ToInt64(Request["displayIndexLast"].ToString().Trim());
                queryObj.timestamp = Request["timestamp"] == null ? "" : Request["timestamp"].ToString().Trim();
                var AppendType = Request["AppendType"] == null ? 1 :
                    Convert.ToInt32(Request["AppendType"].ToString().Trim());

                WUserMessageBLL objectImagesBLL = new WUserMessageBLL(loggingSessionInfo);
                response.content.msgList = objectImagesBLL.GetClientUserMessageList(queryObj, AppendType);
                response.content.totalCount = objectImagesBLL.GetClientUserMessageListCount(queryObj, AppendType);

                content = response.ToJSON();
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
                content = response.ToJSON();
            }
            return content;

        }
        public class GetUserMessageListEntity : RespData
        {
            public GetUserMessageListContentEntity content { get; set; }
        }
        public class GetUserMessageListContentEntity : RespData
        {
            public int totalCount { get; set; }
            public IList<WUserMessageEntity> msgList { get; set; }
        }
        #endregion

        #region 提交微信平台用户留言接口
        /// <summary>
        /// 提交微信平台用户留言接口
        /// </summary>
        /// <returns></returns>
        public string SetUserMessageData()
        {
            var dataStream = Request.InputStream;
            var sr = new StreamReader(dataStream);
            var dataStr = sr.ReadToEnd();
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("SetUserMessageData: ", dataStr)
            });

            SetUserMessageDataEntity response = new SetUserMessageDataEntity();
            response.content = new SetUserMessageDataContentEntity();
            var data = dataStr.DeserializeJSONTo<SetUserMessageDataReqData>();
            if (data == null)
            {
                response.Code = "103";
                response.Description = "数据库操作错误";
                response.Exception = "请求的数据不能为空";
                return response.ToJSON();
            }

            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                JIT.CPOS.BS.BLL.WX.CommonBLL commonService = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                WUserMessageBLL wUserMessageBLL = new WUserMessageBLL(loggingSessionInfo);
                VipUnitMappingBLL vipUnitMappingBLL = new VipUnitMappingBLL(loggingSessionInfo);
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                WUserMessageEntity queryObj = new WUserMessageEntity();
                queryObj.MessageId = data.msg.MessageId;
                queryObj.UnitId = data.msg.UnitId;
                queryObj.VipId = data.msg.VipId;
                queryObj.Text = data.msg.Text;
                queryObj.ImageUrl = data.msg.ImageUrl;
                queryObj.VoiceUrl = data.msg.VoiceUrl;
                queryObj.VideoUrl = data.msg.VideoUrl;
                queryObj.DataFrom = 2;
                queryObj.Title = data.msg.Title;
                queryObj.OriUrl = data.msg.OriUrl;
                queryObj.CreateTime = data.msg.CreateTime;
                queryObj.CreateBy = data.msg.CreateBy;
                queryObj.LastUpdateBy = data.msg.LastUpdateBy;
                queryObj.LastUpdateTime = data.msg.LastUpdateTime;

                queryObj.MaterialTypeId = "1";
                if (queryObj.Text != null && queryObj.Text.Length > 0 &&
                    (queryObj.ImageUrl == null || queryObj.ImageUrl.Length == 0))
                {
                    queryObj.MaterialTypeId = "1";
                }
                else if ((queryObj.Text == null || queryObj.Text.Length == 0) &&
                    queryObj.ImageUrl != null && queryObj.ImageUrl.Length > 0)
                {
                    queryObj.MaterialTypeId = "2";
                }
                else if (queryObj.VoiceUrl != null && queryObj.VoiceUrl.Length > 0)
                {
                    queryObj.MaterialTypeId = "4";
                }
                else if (queryObj.VideoUrl != null && queryObj.VideoUrl.Length > 0)
                {
                    queryObj.MaterialTypeId = "5";
                }
                else
                {
                    queryObj.MaterialTypeId = "3";
                }

                queryObj.ParentMessageId = data.msg.ParentMessageId;
                var parentMsgObj = wUserMessageBLL.GetByID(queryObj.ParentMessageId);
                if (parentMsgObj != null)
                {
                    queryObj.OpenId = parentMsgObj.OpenId;
                    queryObj.WeiXinId = parentMsgObj.WeiXinId;
                }

                var msgObj = wUserMessageBLL.GetByID(queryObj.MessageId);
                if (msgObj != null)
                {
                    response.Code = "103";
                    response.Description = "数据库操作错误";
                    response.Exception = "消息已存在";
                    return response.ToJSON();
                }

                if (true)
                {
                    //queryObj.MessageId = Utils.NewGuid();
                    //queryObj.CreateBy = Request["UserId"];
                    //queryObj.CreateTime = DateTime.Now;
                    //queryObj.LastUpdateBy = Request["UserId"];
                    //queryObj.LastUpdateTime = DateTime.Now;
                    queryObj.IsDelete = 0;
                    wUserMessageBLL.Create(queryObj);

                    vipUnitMappingBLL.Create(new VipUnitMappingEntity()
                    {
                        VipUnitID = Utils.NewGuid(),
                        UnitId = queryObj.UnitId,
                        VIPID = queryObj.VipId
                    });

                    //// 推送消息
                    //var tmpVipList = vipBLL.QueryByEntity(new VipEntity()
                    //{
                    //    VIPID = queryObj.OpenId
                    //}, null);
                    //var vipObj = new VipEntity();
                    //if (tmpVipList != null && tmpVipList.Length > 0)
                    //{
                    //    vipObj = tmpVipList[0];
                    //}
                    //else
                    //{
                    //    response.Code = "103";
                    //    response.Description = "数据库操作错误";
                    //    response.Exception = "未查询到匹配的会员信息";
                    //    return response.ToJSON();
                    //}

                    var WeiXin = queryObj.WeiXinId;

                    string appID = "";
                    string appSecret = "";
                    WApplicationInterfaceBLL waServer = new WApplicationInterfaceBLL(loggingSessionInfo);
                    var waObj = waServer.QueryByEntity(new WApplicationInterfaceEntity
                    {
                        WeiXinID = WeiXin
                    }, null);
                    if (waObj == null || waObj.Length == 0 || waObj[0] == null)
                    {
                        response.Code = "103";
                        response.Description = "数据库操作错误";
                        response.Exception = "未查询到匹配的公众账号信息";
                        return response.ToJSON();
                    }
                    else
                    {
                        appID = waObj[0].AppID;
                        appSecret = waObj[0].AppSecret;
                    }

                    var newsList = new List<NewsEntity>();
                    newsList.Add(new NewsEntity()
                    {
                        title = queryObj.Title,
                        description = queryObj.Text,
                        picurl = queryObj.ImageUrl,
                        url = ""
                    });
                    SendMessageEntity sendInfo = new SendMessageEntity();
                    sendInfo.msgtype = queryObj.MaterialTypeId == "2" ? "news" : "text";
                    sendInfo.touser = queryObj.OpenId;
                    sendInfo.articles = newsList;
                    sendInfo.content = queryObj.Text;

                    JIT.CPOS.BS.Entity.WX.ResultEntity msgResultObj = new JIT.CPOS.BS.Entity.WX.ResultEntity();
                    msgResultObj = commonService.SendMessage(sendInfo, appID, appSecret, loggingSessionInfo);

                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("PushMsgResult:{0}", msgResultObj)
                    });
                    if (msgResultObj != null)
                    {
                        queryObj.IsPushWX = 1;
                        queryObj.PushWXTime = DateTime.Now;
                        if (msgResultObj.errcode == "0")
                        {
                            queryObj.IsPushSuccess = 1;
                        }
                        else
                        {
                            queryObj.IsPushSuccess = 0;
                            queryObj.FailureReason = msgResultObj.ToJSON();
                        }
                        wUserMessageBLL.Update(queryObj, false);
                    }
                }

                //response.content.MessageId = queryObj.MessageId;

                content = response.ToJSON();
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
                content = response.ToJSON();
            }
            return content;

        }
        public class SetUserMessageDataEntity : RespData
        {
            public SetUserMessageDataContentEntity content { get; set; }
        }
        public class SetUserMessageDataReqData
        {
            public string UserId;
            public string CustomerId;
            public string UnitId;
            public WUserMessageEntity msg;
        }
        public class SetUserMessageDataContentEntity : RespData
        {
            public string MessageId { get; set; }
        }
        public class SendWXMsgRespEntity
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
        }
        #endregion

        #region 公众平台提交用户留言接口
        /// <summary>
        /// 公众平台提交用户留言接口
        /// </summary>
        /// <returns></returns>
        public string SetUserMessageDataByWap()
        {

            var dataStr = Request["ReqContent"];


            SetUserMessageDataEntity response = new SetUserMessageDataEntity();
            response.content = new SetUserMessageDataContentEntity();
            var data = dataStr.DeserializeJSONTo<SetUserMessageDataReqData>();
            if (data == null)
            {
                response.Code = "103";
                response.Description = "数据库操作错误";
                response.Exception = "请求的数据不能为空";
                return response.ToJSON();
            }

            var loggingSessionInfo = Default.GetLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                WUserMessageBLL wUserMessageBLL = new WUserMessageBLL(loggingSessionInfo);
                WUserMessageEntity queryObj = new WUserMessageEntity();
                queryObj.MessageId = Guid.NewGuid().ToString();
                queryObj.UnitId = data.msg.UnitId;
                queryObj.ToVipType = data.msg.ToVipType;
                queryObj.VipId = data.msg.VipId;
                queryObj.OpenId = data.msg.OpenId;
                queryObj.WeiXinId = data.msg.WeiXinId;
                queryObj.Text = data.msg.Text;
                queryObj.ImageUrl = data.msg.ImageUrl;
                queryObj.VoiceUrl = data.msg.VoiceUrl;
                queryObj.VideoUrl = data.msg.VideoUrl;
                queryObj.DataFrom = 2;
                queryObj.Title = data.msg.Title;
                queryObj.OriUrl = data.msg.OriUrl;
                queryObj.CreateTime = data.msg.CreateTime;
                queryObj.CreateBy = data.msg.CreateBy;
                queryObj.LastUpdateBy = data.msg.LastUpdateBy;
                queryObj.LastUpdateTime = data.msg.LastUpdateTime;

                queryObj.MaterialTypeId = "1";
                if (queryObj.Text != null && queryObj.Text.Length > 0 &&
                    (queryObj.ImageUrl == null || queryObj.ImageUrl.Length == 0))
                {
                    queryObj.MaterialTypeId = "1";
                }
                else if ((queryObj.Text == null || queryObj.Text.Length == 0) &&
                    queryObj.ImageUrl != null && queryObj.ImageUrl.Length > 0)
                {
                    queryObj.MaterialTypeId = "2";
                }
                else if (queryObj.VoiceUrl != null && queryObj.VoiceUrl.Length > 0)
                {
                    queryObj.MaterialTypeId = "4";
                }
                else if (queryObj.VideoUrl != null && queryObj.VideoUrl.Length > 0)
                {
                    queryObj.MaterialTypeId = "5";
                }
                else
                {
                    queryObj.MaterialTypeId = "3";
                }
                wUserMessageBLL.Create(queryObj);


                content = response.ToJSON();
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
                content = response.ToJSON();
            }
            return content;

        }
        #endregion

        #region 获取各种状态的订单信息
        public string getOrderList()
        {
            string content = string.Empty;
            var respData = new getOrderListRespData();
            try
            {
                var loggingSessionInfo = Default.GetLoggingSession();
                //根据客户标识获取连接字符串  qianzhi  2013-07-30
                if (!string.IsNullOrEmpty(Request["customerId"]))
                {
                    loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
                }

                var page = 1;
                var pageSize = 15;
                var status = Request["status"];
                string orderId = null;
                string vipId = null;
                string timestamp = "0";
                if (Request["page"] != null && Request["page"].Length > 0)
                {
                    page = Convert.ToInt32(Request["page"]);
                }
                if (Request["pageSize"] != null && Request["pageSize"].Length > 0)
                {
                    pageSize = Convert.ToInt32(Request["pageSize"]);
                }
                if (Request["timestamp"] != null && Request["timestamp"].Length > 0)
                {
                    timestamp = Request["timestamp"];
                }
                if (Request["orderId"] != null && Request["orderId"].Length > 0)
                {
                    orderId = Request["orderId"];
                }

                //if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                //{
                //    respData.code = "2202";
                //    respData.description = "openId不能为空";
                //    return respData.ToJSON().ToString();
                //}

                //var vipId = "";
                //var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                //{
                //    WeiXinUserId = reqObj.common.openId
                //}, null);
                //if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                //if (vipId == null || vipId.Length == 0)
                //{
                //    //respData.code = "2200";
                //    //respData.description = "未查询到匹配的VIP信息";
                //    //return respData.ToJSON().ToString();
                //    vipId = ToStr(reqObj.common.userId);
                //}

                respData.content = new getOrderListRespContentData();
                var inoutService = new InoutService(loggingSessionInfo);
                var skuService = new SkuService(loggingSessionInfo);
                var objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
                ItemKeepEntity queryEntity = new ItemKeepEntity();
                queryEntity.VipId = vipId;

                var order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                var order_reason_type_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                var red_flag = "1";
                int maxRowCount = pageSize;
                int startRowIndex = (page - 1) * pageSize;

                var data = inoutService.SearchInoutInfo(
                    "", // order_no
                    order_reason_type_id,
                    "", //sales_unit_id,
                    "", //warehouse_id,
                    "", //purchase_unit_id,
                    "", //status,
                    "", //order_date_begin,
                    "", //order_date_end,
                    "", //complete_date_begin,
                    "", //complete_date_end,
                    "", //data_from_id,
                    "", //ref_order_no,
                    order_type_id,
                    red_flag,
                    maxRowCount,
                    startRowIndex,
                    "", //purchase_warehouse_id
                    "", //sales_warehouse_id
                    status, //Field7, 
                    "", //DeliveryId, 
                    "", //DefrayTypeId, 
                    "", //Field9_begin, 
                    "", //Field9_end, 
                    "", //ModifyTime_begin, 
                    "", //ModifyTime_end
                    orderId,
                    vipId, "", timestamp,false);

                int totalCount = data.ICount;
                var list = data.InoutInfoList;
                respData.content.isNext = "0";
                if (totalCount > 0 && page > 0 &&
                    totalCount > (page * pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.orderList = new List<getOrderListRespCourseData>();
                    foreach (var item in list)
                    {
                        var orderItem = new getOrderListRespCourseData();
                        orderItem.orderId = item.order_id;
                        orderItem.orderCode = item.order_no;
                        orderItem.orderDate = item.order_date;
                        orderItem.totalQty = item.total_qty;
                        orderItem.totalAmount = item.total_amount;
                        orderItem.mobile = item.linkTel;
                        orderItem.postcode = item.Field5;
                        orderItem.deliveryAddress = item.address;
                        orderItem.deliveryTime = item.Field9;
                        orderItem.remark = item.remark;
                        orderItem.deliveryName = item.DeliveryName;
                        orderItem.username = item.vip_name;
                        orderItem.status = item.status;
                        orderItem.statusDesc = item.status_desc;
                        orderItem.clinchTime = item.create_time;
                        orderItem.carrierName = item.carrier_name;
                        orderItem.receiptTime = item.accpect_time;
                        orderItem.storeId = item.purchase_unit_id;
                        orderItem.timestamp = ToStr(item.timestamp);
                        orderItem.vipLevel = item.vipLevel;
                        orderItem.vipLevelDesc = item.vipLevelDesc;
                        orderItem.orderIntegral = Convert.ToInt32(Math.Floor(item.total_amount));
                        orderItem.vipId = item.vipId;
                        orderItem.openId = item.openId;
                        orderItem.phone = item.phone;
                        orderItem.vipRealName = item.vipRealName;
                        orderItem.consignee = item.linkMan;
                        orderItem.isPayment = ToStr(item.Field1);
                        var vwVipCenterInfoBLL = new VwVipCenterInfoBLL(loggingSessionInfo);
                        var vwVipCenterInfoList = vwVipCenterInfoBLL.QueryByEntity(new VwVipCenterInfoEntity() { VIPID = item.vip_no }, null);

                        if (vwVipCenterInfoList != null && vwVipCenterInfoList.Length > 0)
                        {
                            orderItem.vipIntegral = Convert.ToInt32(vwVipCenterInfoList.FirstOrDefault().ValidIntegral);
                        }

                        if (item.create_time != null && item.create_time.Length > 5)
                        {
                            orderItem.createTime = Convert.ToDateTime(item.create_time).ToString("yyyy-MM-dd HH:mm");
                        }

                        orderItem.orderDetailList = new List<getOrderListRespDetailData>();
                        item.InoutDetailList = inoutService.GetInoutDetailInfoByOrderId(orderItem.orderId);
                        if (item.InoutDetailList != null)
                        {
                            foreach (InoutDetailInfo tmpOrderDetail in item.InoutDetailList)
                            {
                                var orderDetail = new getOrderListRespDetailData();
                                orderDetail.skuId = tmpOrderDetail.sku_id;
                                orderDetail.itemId = tmpOrderDetail.item_id;
                                orderDetail.itemName = tmpOrderDetail.item_name;
                                orderDetail.GG = tmpOrderDetail.prop_1_detail_name;
                                orderDetail.salesPrice = tmpOrderDetail.enter_price;
                                orderDetail.stdPrice = tmpOrderDetail.std_price;
                                orderDetail.discountRate = tmpOrderDetail.discount_rate;
                                orderDetail.qty = tmpOrderDetail.enter_qty;
                                orderDetail.itemCategoryName = tmpOrderDetail.itemCategoryName;
                                orderDetail.beginDate = ToStr(tmpOrderDetail.Field1);
                                orderDetail.endDate = ToStr(tmpOrderDetail.Field2);
                                orderDetail.dayCount = ToInt(tmpOrderDetail.DayCount);
                                orderDetail.imageList = new List<getOrderListRespDetailImageData>();
                                if (tmpOrderDetail.item_id != null && tmpOrderDetail.item_id.Length > 0)
                                {
                                    var tmpImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                                    {
                                        ObjectId = tmpOrderDetail.item_id
                                    }, null);
                                    if (tmpImageList != null)
                                    {
                                        foreach (var tmpImageItem in tmpImageList)
                                        {
                                            var orderDetailImage = new getOrderListRespDetailImageData();
                                            orderDetailImage.imageId = tmpImageItem.ImageId;
                                            orderDetailImage.imageUrl = tmpImageItem.ImageURL;
                                            orderDetail.imageList.Add(orderDetailImage);
                                        }
                                    }
                                }
                                orderItem.orderDetailList.Add(orderDetail);
                            }
                        }
                        respData.content.orderList.Add(orderItem);
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

        public class getOrderListRespData : Default.LowerRespData
        {
            public getOrderListRespContentData content { get; set; }
        }
        public class getOrderListRespContentData
        {
            public string isNext { get; set; }
            public IList<getOrderListRespCourseData> orderList { get; set; }
        }
        public class getOrderListRespCourseData
        {
            public string orderId { get; set; }
            public string orderCode { get; set; }
            public string orderDate { get; set; }
            public decimal totalQty { get; set; }
            public decimal totalAmount { get; set; }
            public string mobile { get; set; }
            public string postcode { get; set; }
            public string deliveryAddress { get; set; }
            public string deliveryTime { get; set; }
            public string remark { get; set; }
            public string deliveryName { get; set; }
            public string username { get; set; }
            public string status { get; set; }
            public string statusDesc { get; set; }
            public string clinchTime { get; set; }
            public string carrierName { get; set; }
            public string receiptTime { get; set; }
            public string createTime { get; set; }
            public IList<getOrderListRespDetailData> orderDetailList { get; set; }
            public string storeId { get; set; }
            public string timestamp { get; set; }
            public int vipLevel { get; set; }
            public string vipLevelDesc { get; set; }
            public int vipIntegral { get; set; }
            public int orderIntegral { get; set; }
            public string vipId { get; set; }
            public string openId { get; set; }
            public string phone { get; set; }
            public string vipRealName { get; set; }
            public string consignee { get; set; }
            public string isPayment { get; set; }
        }
        public class getOrderListRespDetailData
        {
            public string skuId { get; set; }
            public string itemId { get; set; }
            public string itemName { get; set; }
            public string GG { get; set; }
            public decimal salesPrice { get; set; }
            public decimal stdPrice { get; set; }
            public decimal discountRate { get; set; }
            public decimal qty { get; set; }
            public IList<getOrderListRespDetailImageData> imageList { get; set; }
            public string itemCategoryName { get; set; }
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public int dayCount { get; set; }
        }
        public class getOrderListRespDetailImageData
        {
            public string imageId { get; set; }
            public string imageUrl { get; set; }
        }
        public class getOrderListReqData // : ReqData
        {
            public getOrderListReqSpecialData special;
        }
        public class getOrderListReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
            public string status { get; set; }
            public string orderId { get; set; }
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
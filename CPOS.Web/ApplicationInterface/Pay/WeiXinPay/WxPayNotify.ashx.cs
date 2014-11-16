using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Pay.WeiXinPay
{
    /// <summary>
    /// WxPayNotify 的摘要说明
    /// </summary>
    public class WxPayNotify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            #region


            //const string url = "http://localhost:23130/WX/ReceiveMsg.aspx";
            //var paras =
            //    @"<xml><ToUserName><![CDATA[gh_3dce00b8133c]]></ToUserName><FromUserName><![CDATA[oKOmbjgPAF5nDOrbh8wmWmcN006M]]></FromUserName><CreateTime>1406516103</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[LOCATION]]></Event><Latitude>31.292549</Latitude><Longitude>121.515648</Longitude><Precision>120.000000</Precision></xml>";
            //CommonBLL.GetRemoteData(url, "POST", paras);

            //var orderId = context.Request.QueryString["orderId"];
            //var customerId = "24af2889e6054496b1903e0ba5dd01cf";
            //var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");
            //var bll = new T_InoutBLL(currentUserInfo);
            //bll.GetDeliverInfoByOrderId(orderId, currentUserInfo);

            var content = context.Request.QueryString["content"];
            var vipId = "60a00bd463b747cbb4235537f449b5e6";
            StoreRebate(content, vipId);

            #endregion
        }

        public void StoreRebate(string content, string vipID)
        {
            Loggers.Debug(new DebugLogInfo()
            {
                Message = "返利信息：" + content
            });

            var loggingSessionInfo = Default.GetBSLoggingSession("e703dbedadd943abacf864531decdac1", "1");

            VipDCodeBLL bll = new VipDCodeBLL(loggingSessionInfo);
            WXSalesPolicyRateBLL SalesPolicybll = new WXSalesPolicyRateBLL(loggingSessionInfo);
            //var tran = bll.GetTran();
            try
            {
                //判断当前发送二维码的微信号是否是 二维码表中当前二维码Code的会员 VipId=vipID;
                //var temp = bll.QueryByEntity(new VipDCodeEntity { IsDelete = 0, DCodeId = content}, null);
                var temp = bll.GetByID(content);
                decimal? ReturnAmount = 0;
                string PushInfo = string.Empty;
                //using (tran.Connection)
                //{
                if (temp != null)   //如果可以匹配，则更新二维码表中的会员ID，OpenId
                {

                    #region 1.更新返现金额。更新返现状态
                    VipDCodeEntity entity = new VipDCodeEntity();
                    entity = temp;
                    DataSet ds = SalesPolicybll.getReturnAmount(Convert.ToDecimal(entity.SalesAmount), entity.CustomerId);
                    if (ds != null && ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "该客户没有配置策略信息"
                        });

                        throw new Exception("该客户没有配置策略信息");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //返现金额
                        ReturnAmount = entity.ReturnAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReturnAmount"].ToString());
                        //返现消息内容
                        PushInfo = ds.Tables[0].Rows[0]["PushInfo"].ToString();
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "PushInfo1：" + PushInfo
                        });

                    }
                    else
                    {
                        //返现金额
                        ReturnAmount = entity.ReturnAmount = Convert.ToDecimal(ds.Tables[1].Rows[0]["ReturnAmount"].ToString());
                        //返现消息内容
                        PushInfo = ds.Tables[1].Rows[0]["PushInfo"].ToString();

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "PushInfo2：" + PushInfo
                        });

                    }

                    entity.OpenId = "oxbbcjg5NBbdpK1T9mDkIzTn434U";
                    entity.VipId = vipID;
                    entity.ReturnAmount = ReturnAmount;
                    VipAmountBLL Amountbll = new VipAmountBLL(loggingSessionInfo);

                    var vipBll = new VipBLL(loggingSessionInfo);

                    var vipEntity = vipBll.GetByID(vipID);

                    if (temp.IsReturn == 1)
                    {
                        //发送消息

                        JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage("对不起，该返利码已经被领取", "1", loggingSessionInfo, vipEntity);
                        return;
                    }

                    if (DateTime.Now > (temp.CreateTime ?? DateTime.Now).AddDays(1))
                    {
                        //发送消息
                        JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage("对不起，您的返利码已经过期，请在收到返利码后的24小时内使用", "1", loggingSessionInfo, vipEntity);
                        return;
                    }



                    string strErrormessage = "";
                    if (entity.IsReturn != 1)  //当未返现的时候金额变更
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = "begin update VipDcode："
                        });

                        if (Amountbll.SetVipAmountChange(entity.CustomerId, 2, vipID, ReturnAmount ?? 0, entity.ObjectId, "门店返现", "IN", out strErrormessage))
                        {
                            entity.IsReturn = 1;
                            entity.DCodeId = content;
                            bll.Update(entity); //更新返现金额

                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = "update VipDcode success"
                            });
                        }
                    }

                    #endregion



                    var vipamountBll = new VipAmountBLL(loggingSessionInfo);
                    var vipAmountEntity = vipamountBll.GetByID(vipID);
                    decimal endAmount = 0;
                    if (vipAmountEntity != null)
                    {
                        endAmount = vipAmountEntity.EndAmount ?? 0;
                    }
                    var message = PushInfo.Replace("#SalesAmount#", entity.SalesAmount.ToString()).Replace("#ReturnAmount#", Convert.ToDecimal(ReturnAmount).ToString("0.00")).Replace("#EndAmount#", endAmount.ToString("0.00")).Replace("#VipName#", vipEntity.VipName);

                    #region 插入门店返现推送消息日志表
                    WXSalesPushLogBLL PushLogbll = new WXSalesPushLogBLL(loggingSessionInfo);
                    WXSalesPushLogEntity pushLog = new WXSalesPushLogEntity();
                    pushLog.LogId = Guid.NewGuid();
                    pushLog.WinXin = "gh_e2b2da1e6edf";
                    pushLog.OpenId = "oxbbcjg5NBbdpK1T9mDkIzTn434U";
                    pushLog.VipId = vipID;
                    pushLog.PushInfo = message;
                    pushLog.DCodeId = content;
                    pushLog.RateId = Guid.NewGuid();
                    PushLogbll.Create(pushLog);
                    #endregion
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = message
                    });
                    string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, "1", loggingSessionInfo, vipEntity);

                    Loggers.Debug(new DebugLogInfo() { Message = "消息推送完成，code=" + code + ", message=" + message });

                    #region 增加抽奖信息
                    var rateList = SalesPolicybll.QueryByEntity(new WXSalesPolicyRateEntity { CustomerId = temp.CustomerId }, null);
                    if (rateList == null || rateList.Length == 0)
                    {
                        //rateList = SalesPolicybll.QueryByEntity(new WXSalesPolicyRateEntity{CustomerId =null},null);
                        rateList = SalesPolicybll.GetWxSalesPolicyRateList().ToArray();
                    }

                    if (rateList != null && rateList.Length > 0)
                    {

                        var wxSalespolicyRateMapBll = new WXSalesPolicyRateObjectMappingBLL(loggingSessionInfo);

                        var rateMappingEntity =
                            wxSalespolicyRateMapBll.QueryByEntity(new WXSalesPolicyRateObjectMappingEntity { RateId = rateList[0].RateId }, null);
                        if (rateMappingEntity != null && rateMappingEntity.Length > 0)
                        {
                            if (Convert.ToDecimal(temp.SalesAmount) >= rateMappingEntity[0].CoefficientAmount)
                            {
                                if (rateMappingEntity[0].PushInfo != null)
                                {
                                    var eventMessage = rateMappingEntity[0].PushInfo.Replace("#CustomerId#", temp.CustomerId).Replace("#EventId#", rateMappingEntity[0].ObjectId).Replace("#VipId#", vipID).Replace("#OpenId#", vipEntity.WeiXinUserId);
                                    JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(eventMessage, "1", loggingSessionInfo, vipEntity);
                                }
                            }
                        }


                    }
                    #endregion
                }
                // }
            }
            catch (Exception)
            {
                // tran.Rollback();
                throw;
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
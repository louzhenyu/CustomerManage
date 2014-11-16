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
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    public partial class OnlinePayAfter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseService.WriteLog("支付成功页面----------------OnlinePayAfter.aspx");

            if (!IsPostBack)
            {
                if (Request["order_id"] != null)
                {
                    BaseService.WriteLog("order_id:  " + Request["order_id"]);
                }
                if (Request["result"] != null)
                {
                    BaseService.WriteLog("result:  " + Request["result"]);
                }
                if (Request["out_trade_no"] != null)
                {
                    BaseService.WriteLog("out_trade_no:  " + Request["out_trade_no"]);
                    SetPayOrderInfo(Request["order_id"].Trim().ToString()
                               , Request["result"].Trim().ToString()
                               , Request["out_trade_no"].Trim().ToString()
                               );
                }
                
            }
        }

        /// <summary>
        /// 处理订单支付之后业务标识
        /// </summary>
        /// <param name="OrderCustomerInfo"></param>
        /// <param name="strResult"></param>
        /// <param name="strOutTradeNo"></param>
        private void SetPayOrderInfo(string OrderCustomerInfo,string strResult,string strOutTradeNo)
        {
            //order_id：  订单号
            //result：    success（交易成功）  或者   fail（交易失败）
            //out_trade_no：  与订单号对应的外部交易号（方便日后查询交易中的详细信息）
            try
            {
                #region check
                if (OrderCustomerInfo == null || OrderCustomerInfo.Equals(""))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetPayOrderInfo: {0}", "返回订单号为空")
                    });
                    return;
                }
                if (strResult == null || strResult.Equals("fail"))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetPayOrderInfo: {0}", "支付失败")
                    });
                    return;
                }
                #endregion

                #region 处理业务
                var infos = OrderCustomerInfo.Split(',');
                if (infos.Length != 2) {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetPayOrderInfo-OrderCustomerInfo: {0}", "长度错误")
                    });
                    return;
                }
                string customerId = infos[0].ToString().Trim();
                string orderCode = infos[1].ToString().Trim();
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.OrderCode = orderCode;
                orderInfo.PaymentTypeId = "BB04817882B149838B19DE2BDDA5E91B";
                //orderInfo.PaymentAmount = Convert.ToDecimal(reqObj.special.paymentAmount);
                //orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.PaymentTime = System.DateTime.Now;
                orderInfo.Status = "2";
                orderInfo.StatusDesc = "已支付";
                orderInfo.OutTradeNo = strOutTradeNo;
                string strError = string.Empty;
                bool bReturn = service.SetOrderPayment(orderInfo, out strError);
                if (!bReturn)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetPayOrderInfo: {0}", "提交失败")
                    });
                } else { 
                    string openId = string.Empty;
                    string amount = string.Empty;
                    string orderId = string.Empty;
                    //支付成功，发送邮件与短信
                    cUserService userServer = new cUserService(loggingSessionInfo);
                    userServer.SendOrderMessage(orderCode);

                    bool bReturnx = service.GetOrderOpenId(orderCode, out openId, out amount, out orderId);
                    //推送消息
                    if (bReturnx)
                    {
                        string dataStr = "{ \"OpenID\":\"" + openId + "\", \"" + amount + "\":2877 }";
                     
                        //bool bReturnt = SetVipIntegral(loggingSessionInfo, dataStr);
                        VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                        bool bReturnt = vipIntegralBLL.SetPushIntegral(orderId
                                                        , ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim()
                                                        ,out strError);
                        if (!bReturnt) {
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("SetPayOrderInfo-失败: {0}", "发送失败")
                            });
                        }
                    }
                    else {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetPayOrderInfo-失败: {0}", "获取信息失败")
                        });
                    }
                }
                #endregion
            }
            catch (Exception ex) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPayOrderInfo-失败: {0}", ex.ToString())
                });
            }
        }

        //#region 积分推送
        //public bool SetVipIntegral(LoggingSessionInfo loggingSessionInfo, string dataStr)
        //{
        //    //string content = string.Empty;
        //    //var respData = new RespData();
        //    try
        //    {
        //        //var dataStream = Request.InputStream;
        //        //var sr = new StreamReader(dataStream);
        //        //var dataStr = sr.ReadToEnd();
        //        //string dataStr = Request["data"];
        //        //dataStr = "{ \"OpenID\":\"o8Y7EjpXP-Dep8qeYnum7pX6vUP8\", \"SalesAmount\":2877 }";
        //        //SetVipIntegral:{"OpenID" : "o8Y7Ejv3jR5fEkneCNu6N1_TIYIM","SalesAmount" : "0.01"}

        //        Loggers.Debug(new DebugLogInfo()
        //        {
        //            Message = string.Format("SetVipIntegral:{0}", dataStr)
        //        });

        //        VipBLL vipBLL = new VipBLL(loggingSessionInfo);
        //        IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(loggingSessionInfo);
        //        VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(loggingSessionInfo);
        //        VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
        //        var data = dataStr.DeserializeJSONTo<SetVipIntegralReqData>();

        //        if (data != null)
        //        {
        //            string integralSourceId = "1";
        //            decimal integralValue = 0;
        //            if (data.OpenID == null || data.OpenID.Trim().Length == 0)
        //            {
        //                Loggers.Debug(new DebugLogInfo()
        //                {
        //                    Message = string.Format("SetVipIntegral:{0}", "OpenID为空")
        //                });
        //                return false;
        //            }

        //            // 查询会员ID
        //            VipEntity vipIdData = null;
        //            var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
        //            {
        //                WeiXinUserId = data.OpenID
        //            }, null);
        //            if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null)
        //            {
        //                if (data.OpenID == null || data.OpenID.Trim().Length == 0)
        //                {
        //                    Loggers.Debug(new DebugLogInfo()
        //                    {
        //                        Message = string.Format("SetVipIntegral:{0}", "OpenID为空1")
        //                    });
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                vipIdData = vipIdDataList[0];
        //                data.VipID = vipIdData.VIPID;
        //            }

        //            // 计算积分
        //            IntegralRuleEntity integralRuleData = null;
        //            var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
        //            {
        //                IntegralSourceID = integralSourceId
        //            }, null);
        //            if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
        //            {
        //                Loggers.Debug(new DebugLogInfo()
        //                {
        //                    Message = string.Format("SetVipIntegral:{0}", "未查询到积分规则")
        //                });

        //            }
        //            else
        //            {
        //                integralRuleData = integralRuleDataList[0];
        //                integralValue = CPOS.Common.Utils.GetDecimalVal(integralRuleData.Integral) *
        //                    CPOS.Common.Utils.GetDecimalVal(data.SalesAmount);
        //            }

        //            // 插入积分明细
        //            VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
        //            vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
        //            vipIntegralDetailEntity.VIPID = data.VipID;
        //            vipIntegralDetailEntity.FromVipID = data.VipID;
        //            vipIntegralDetailEntity.SalesAmount = data.SalesAmount;
        //            vipIntegralDetailEntity.Integral = integralValue;
        //            vipIntegralDetailEntity.IntegralSourceID = integralSourceId;
        //            vipIntegralDetailEntity.IsAdd = 1;
        //            vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

        //            // 更新积分
        //            VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
        //            var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
        //                new VipIntegralEntity() { VipID = data.VipID }, null);
        //            if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
        //            {
        //                vipIntegralEntity.VipID = data.VipID;
        //                vipIntegralEntity.BeginIntegral = 0; // 期初积分
        //                vipIntegralEntity.InIntegral = 0; // 增加积分
        //                vipIntegralEntity.OutIntegral = integralValue; //消费积分
        //                vipIntegralEntity.EndIntegral = integralValue; //积分余额
        //                vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
        //                vipIntegralEntity.ValidIntegral = integralValue; // 当前有效积分
        //                vipIntegralBLL.Create(vipIntegralEntity);
        //            }
        //            else
        //            {
        //                vipIntegralEntity.VipID = data.VipID;
        //                //vipIntegralEntity.InIntegral = 0; // 增加积分
        //                vipIntegralEntity.OutIntegral = Common.Utils.GetDecimalVal(
        //                    vipIntegralDataList[0].OutIntegral) + integralValue; //消费积分
        //                vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
        //                    vipIntegralDataList[0].EndIntegral) + integralValue; //积分余额
        //                //vipIntegralEntity.InvalidIntegral = 0; // 累计失效积分
        //                vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
        //                    vipIntegralDataList[0].ValidIntegral) + integralValue; // 当前有效积分
        //                vipIntegralBLL.Update(vipIntegralEntity, false);
        //            }

        //            // 更新VIP
        //            VipEntity vipEntity = new VipEntity();
        //            var vipEntityDataList = vipBLL.QueryByEntity(
        //                new VipEntity() { VIPID = data.VipID }, null);
        //            if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
        //            {
        //                vipEntity.VIPID = data.VipID;
        //                vipEntity.Integration = vipIntegralEntity.ValidIntegral;
        //                vipBLL.Create(vipEntity);
        //            }
        //            else
        //            {
        //                vipEntity.VIPID = data.VipID;
        //                vipEntity.Integration = vipIntegralEntity.ValidIntegral;
        //                vipBLL.Update(vipEntity, false);
        //            }

        //            // 推送消息
        //            var strValidIntegral = string.Empty;
        //            if (vipIntegralEntity.ValidIntegral == null)
        //            {
        //                strValidIntegral = "0";
        //            }
        //            else
        //            {
        //                decimal vd = (decimal)vipIntegralEntity.ValidIntegral;
        //                strValidIntegral = Convert.ToString(decimal.Truncate(vd));
        //            }

        //            string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
        //            string msgText = string.Format("感谢您来店消费。您刚刚消费共计{1}元，新增积分{0}，积分累计为{2}。欢迎您下次光临。",
        //                integralValue, data.SalesAmount, vipIntegralEntity.ValidIntegral);
        //            string msgData = "<xml><OpenID><![CDATA[" + data.OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

        //            var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

        //            //推送刮奖消息
        //            msgText = "您赢得了一次在线抽奖机会，<a href='http://xxx:9004/wap/weixin/luckyDraw.html'>点击参与刮奖</a>";
        //            msgData = "<xml><OpenID><![CDATA[" + data.OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

        //            msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);

        //            Loggers.Debug(new DebugLogInfo()
        //            {
        //                Message = string.Format("PushMsgResult:{0}", msgResult)
        //            });
        //            //GetHightOpenInfo(data); //Jermyn20130517给上家添加积分
        //            //respData.Data = vipIntegralEntity.ValidIntegral.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Loggers.Debug(new DebugLogInfo()
        //        {
        //            Message = string.Format("PushMsgResult:{0}", ex.ToString())
        //        });
        //    }

        //    return true;
        //}

        //public class SetVipIntegralReqData
        //{
        //    public string OpenID;
        //    public string VipID;
        //    public decimal SalesAmount;
        //    public string IntegralSourceId;
        //}
        //#endregion
    }
}
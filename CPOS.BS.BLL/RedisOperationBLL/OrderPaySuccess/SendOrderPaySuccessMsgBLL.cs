using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using RedisOpenAPIClient.Models;
using JIT.CPOS.BS.BLL.Utility;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderPaySuccess
{
    /// <summary>
    /// 订单支付完成 队列 消息发送
    /// </summary>
    public class SendOrderPaySuccessMsgBLL
    {
        /// <summary>
        /// 入 订单支付完成 队列
        /// </summary>
        public void SentPaymentMessage(T_InoutEntity Inout, string OpenID, string VipID, LoggingSessionInfo loggingSessionInfo)
        {
            //
            var commonBLL = new CommonBLL();

            //
            var wxTMConfigData = new WXTMConfigBLL(loggingSessionInfo).QueryByEntity(new WXTMConfigEntity
            {
                TemplateIdShort = "TM00398",
                CustomerId = loggingSessionInfo.ClientID,
                IsDelete=0
            }, null).FirstOrDefault();
            if (wxTMConfigData == null)
            {
                return;
            }

            //
            string ItemName = commonBLL.GetItemName(Inout.order_no, loggingSessionInfo);
            PaySuccess PaySuccessData = new PaySuccess();
            PaySuccessData.first = new DataInfo() { value = wxTMConfigData.FirstText, color = wxTMConfigData.FirstColour };
            PaySuccessData.orderProductPrice = new DataInfo() { value = Math.Round(Inout.actual_amount ?? 0, 2).ToString(), color = wxTMConfigData.Colour1 };
            PaySuccessData.orderProductName = new DataInfo() { value = ItemName, color = wxTMConfigData.Colour2 };
            PaySuccessData.orderAddress = new DataInfo() { value = Inout.Field4, color = wxTMConfigData.Colour3 };
            PaySuccessData.orderName = new DataInfo() { value = Inout.order_no, color = wxTMConfigData.Colour3 };
            PaySuccessData.remark = new DataInfo() { value = wxTMConfigData.RemarkText, color = wxTMConfigData.RemarkColour };

            //
            var response = RedisOpenAPI.Instance.CCOrderPaySuccess().SetPaySuccess(new CC_PaySuccess
            {
                CustomerID = loggingSessionInfo.ClientID,
                ConfigData = new CC_ConfigData
                {
                    LogSession = loggingSessionInfo.JsonSerialize(),
                    OpenID = OpenID,
                    TemplateID = wxTMConfigData.TemplateID,
                    VipID = VipID
                },
                PaySuccessData = new CC_PaySuccessData
                {
                    first = new CC_DataInfo { value = PaySuccessData.first.value, color = PaySuccessData.first.color },
                    orderAddress = new CC_DataInfo { value = PaySuccessData.orderAddress.value, color = PaySuccessData.orderAddress.color },
                    orderName = new CC_DataInfo { value = PaySuccessData.orderName.value, color = PaySuccessData.orderName.color },
                    orderProductName = new CC_DataInfo { value = PaySuccessData.orderProductName.value, color = PaySuccessData.orderProductName.color },
                    orderProductPrice = new CC_DataInfo { value = PaySuccessData.orderProductPrice.value, color = PaySuccessData.orderProductPrice.color },
                    remark = new CC_DataInfo { value = PaySuccessData.remark.value, color = PaySuccessData.remark.color }
                }
            });
            if (response.Code != ResponseCode.Success)
            {
                commonBLL.SendMatchWXTemplateMessage(wxTMConfigData.TemplateID, null, null, null, PaySuccessData, null, null, null, null, "15", OpenID, VipID, loggingSessionInfo);
                new RedisXML().RedisReadDBCount("OrderPaySuccess", "微信端支付成功通知", 2);
            }
            else {
                new RedisXML().RedisReadDBCount("OrderPaySuccess", "微信端支付成功通知", 1);
            }
            //return commonBLL.SendMatchWXTemplateMessage(wxTMConfigData.TemplateID, null, null, null, PaySuccessData, null, "15", OpenID, VipID, loggingSessionInfo);
        }
    }
}

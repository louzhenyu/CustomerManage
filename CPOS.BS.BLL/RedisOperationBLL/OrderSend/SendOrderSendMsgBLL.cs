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
using RedisOpenAPIClient.Models.CC.OrderSend;
using JIT.CPOS.BS.BLL.Utility;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderSend
{
    /// <summary>
    /// 发货提醒通知
    /// </summary>
    public class SendOrderSendMsgBLL
    {

        public void SentShipMessage(InoutInfo Inout, string OpenID, string VipID, LoggingSessionInfo loggingSessionInfo)
        {
            //这里保持了源代码
            if (string.IsNullOrWhiteSpace(OpenID))
            {
                var VipInfo = new VipBLL(loggingSessionInfo).GetByID(Inout.vipId);
                OpenID = VipInfo == null ? "" : VipInfo.WeiXinUserId;
            }
            var CommonBLL = new JIT.CPOS.BS.BLL.WX.CommonBLL();
            //根据模板信息的简称和商户的标识获取模板消息的模板内容
            var WXTMConfigData = new WXTMConfigBLL(loggingSessionInfo).QueryByEntity(new WXTMConfigEntity() { TemplateIdShort = "OPENTM200565259", CustomerId = loggingSessionInfo.ClientID,IsDelete=0 }, null).FirstOrDefault();
            if (WXTMConfigData == null)
                return;
            CommonData CommonData = new CommonData();
            CommonData.first = new DataInfo() { value = WXTMConfigData.FirstText, color = WXTMConfigData.FirstColour };
            CommonData.keyword1 = new DataInfo() { value = Inout.order_no, color = WXTMConfigData.Colour1 };
            CommonData.keyword2 = new DataInfo() { value = Inout.carrier_name, color = WXTMConfigData.Colour2 };
            CommonData.keyword3 = new DataInfo() { value = Inout.Field2, color = WXTMConfigData.Colour3 };
            CommonData.remark = new DataInfo() { value = WXTMConfigData.RemarkText, color = WXTMConfigData.RemarkColour };

            //下面往redis里存入数据
            var response = RedisOpenAPI.Instance.CCOrderSend().SeOrderSend(new CC_OrderSend
            {
                CustomerID = loggingSessionInfo.ClientID,
                ConfigData = new CC_ConfigData
                {
                    LogSession = loggingSessionInfo.JsonSerialize(),
                    OpenID = OpenID,
                    TemplateID = WXTMConfigData.TemplateID,
                    VipID = VipID
                },
                OrderSendData = new CC_OrderSendData
                {
                    first = new CC_DataInfo { value = CommonData.first.value, color = CommonData.first.color },
                    keyword1 = new CC_DataInfo { value = CommonData.keyword1.value, color = CommonData.keyword1.color },
                    keyword2 = new CC_DataInfo { value = CommonData.keyword1.value, color = CommonData.keyword1.color },
                    keyword3 = new CC_DataInfo { value = CommonData.keyword1.value, color = CommonData.keyword1.color },

                    remark = new CC_DataInfo { value = CommonData.remark.value, color = CommonData.remark.color }
                }
            });
            //如果往缓存redis里写入不成功，还是按照原来的老方法往数据库里写
            if (response.Code != ResponseCode.Success)
            {
                CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, CommonData, null, null, null, null, null, null, null, "8", OpenID, VipID, loggingSessionInfo);
                new RedisXML().RedisReadDBCount("OrderSend", "确认收货/完成订单 - 处理奖励", 2);
            }
            else {
                new RedisXML().RedisReadDBCount("OrderSend", "确认收货/完成订单 - 处理奖励", 1);
            
            }
            //return  CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, CommonData, null, null, null, null, "8", OpenID, VipID, loggingSessionInfo);
        }
    }
}

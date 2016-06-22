using JIT.CPOS.BS.BLL.Utility;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.CouponToBeExpired;
using RedisOpenAPIClient.Models.CC.OrderNotPay;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.Models.CC.OrderSend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace JIT.CPOS.BS.BLL.RedisOperationBLL.CouponsUpcomingExpired
{
    //往缓存里写数据
    public class CouponToBeExpiredMsgBLL
    {
        public void CouponsUpcomingExpiredMessage(string CouponType, string CouponCode, string EffectiveData, string FailData, string OpenID, LoggingSessionInfo loggingSessionInfo)
        {
            var CommonBLL = new JIT.CPOS.BS.BLL.WX.CommonBLL();
            var WXTMConfigData = new WXTMConfigBLL(loggingSessionInfo).QueryByEntity(new WXTMConfigEntity() { TemplateIdShort = "OPENTM206623166", CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).FirstOrDefault();

            var Data = new JIT.CPOS.BS.Entity.WX.CouponsUpcomingExpired();
            Data.first = new DataInfo() { value = WXTMConfigData.FirstText, color = WXTMConfigData.FirstColour };
            Data.keyword1 = new DataInfo() { value = CouponType, color = WXTMConfigData.Colour1 };//券类型
            Data.keyword2 = new DataInfo() { value = CouponCode, color = WXTMConfigData.Colour1 };//券码
            Data.keyword3 = new DataInfo() { value = EffectiveData, color = WXTMConfigData.Colour1 };//生效日期
            Data.keyword4 = new DataInfo() { value = FailData, color = WXTMConfigData.Colour1 };//失效日期
            Data.remark = new DataInfo() { value = WXTMConfigData.RemarkText, color = WXTMConfigData.RemarkColour };

          //  return SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, null, null, null, null, null, null, Data, null, "1", OpenID, null, loggingSessionInfo);
      
            //下面往redis里存入数据
            var response = RedisOpenAPI.Instance.CCCouponToBeExpired().SetCouponToBeExpired(new CC_CouponToBeExpired
            {
                CustomerID = loggingSessionInfo.ClientID,
                ConfigData = new CC_ConfigData
                {
                    LogSession = loggingSessionInfo.JsonSerialize(),
                    OpenID = OpenID,
                    TemplateID = WXTMConfigData.TemplateID,
                    //   VipID = VipID
                },
                CouponToBeExpiredData = new CC_CouponToBeExpiredData
                {
                    first = new CC_DataInfo { value = Data.first.value, color = Data.first.color },
                    keyword1 = new CC_DataInfo { value = Data.keyword1.value, color = Data.keyword1.color },
                    keyword2 = new CC_DataInfo { value = Data.keyword2.value, color = Data.keyword2.color },
                    keyword3 = new CC_DataInfo { value = Data.keyword3.value, color = Data.keyword3.color },
                    keyword4 = new CC_DataInfo { value = Data.keyword4.value, color = Data.keyword4.color },
                    remark = new CC_DataInfo { value = Data.remark.value, color = Data.remark.color }
                }
            });
            //如果往缓存redis里写入不成功，还是按照原来的老方法直接发送**
            if (response.Code != ResponseCode.Success)
            {
                CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, null, null, null, null, null, null, Data, null, "1", OpenID, null, loggingSessionInfo);

                new RedisXML().RedisReadDBCount("CouponsUpcomingExpired", "优惠券即将过期通知", 2);
            }
            else
            {
                new RedisXML().RedisReadDBCount("CouponsUpcomingExpired", "优惠券即将过期通知", 1);

            }

            //return  CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, CommonData, null, null, null, null, "8", OpenID, VipID, loggingSessionInfo);
        }
 

    }
}

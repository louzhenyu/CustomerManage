using JIT.CPOS.BS.BLL.Utility;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.OrderNotPay;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.Models.CC.OrderSend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderNotPay
{
   public class SendOrderNotPayMsgBLL
    {

       public void NotPayMessage(string orderProductPrice, string orderProductName, string orderAddress, string orderName, string OpenID, LoggingSessionInfo loggingSessionInfo)
       {
           var CommonBLL = new JIT.CPOS.BS.BLL.WX.CommonBLL();
           var WXTMConfigData = new WXTMConfigBLL(loggingSessionInfo).QueryByEntity(new WXTMConfigEntity() { TemplateIdShort = "TM00701", CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).FirstOrDefault();

           var Data = new NotPay();
           Data.first = new DataInfo() { value = WXTMConfigData.FirstText, color = WXTMConfigData.FirstColour };
           Data.orderProductPrice = new DataInfo() { value = orderProductPrice, color = WXTMConfigData.Colour1 };//订单金额
           Data.orderProductName = new DataInfo() { value = orderProductName, color = WXTMConfigData.Colour1 };//商品详情
           Data.orderAddress = new DataInfo() { value = orderAddress, color = WXTMConfigData.Colour1 };//收货信息
           Data.orderName = new DataInfo() { value = orderName, color = WXTMConfigData.Colour1 };//订单编号
           Data.remark = new DataInfo() { value = WXTMConfigData.RemarkText, color = WXTMConfigData.RemarkColour };   //remark是从模板里取出来的
 
           //下面往redis里存入数据
           var response = RedisOpenAPI.Instance.CCOrderNotPay().SetOrderNotPay(new CC_OrderNotPay
           {
               CustomerID = loggingSessionInfo.ClientID,
               ConfigData = new CC_ConfigData
               {
                   LogSession = loggingSessionInfo.JsonSerialize(),
                   OpenID = OpenID,
                   TemplateID = WXTMConfigData.TemplateID,
                //   VipID = VipID
               },
               OrderNotPayData = new CC_OrderNotPayData
               {
                   first = new CC_DataInfo { value = Data.first.value, color = Data.first.color },
                   orderProductPrice = new CC_DataInfo { value = Data.orderProductPrice.value, color = Data.orderProductPrice.color },
                   orderProductName = new CC_DataInfo { value = Data.orderProductName.value, color = Data.orderProductName.color },
                   orderAddress = new CC_DataInfo { value = Data.orderAddress.value, color = Data.orderAddress.color },
                   orderName = new CC_DataInfo { value = Data.orderName.value, color = Data.orderName.color },
                   remark = new CC_DataInfo { value = Data.remark.value, color = Data.remark.color }
               }
           });
           //如果往缓存redis里写入不成功，还是按照原来的老方法直接发送**
           if (response.Code != ResponseCode.Success)
           {
               CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, null, null, null, null, null, null, null, Data, "2", OpenID, null, loggingSessionInfo);

               new RedisXML().RedisReadDBCount("OrderNotPay", "订单未付款通知", 2);
           }
           else
           {
               new RedisXML().RedisReadDBCount("OrderNotPay", "订单未付款通知", 1);

           }
   
           //return  CommonBLL.SendMatchWXTemplateMessage(WXTMConfigData.TemplateID, CommonData, null, null, null, null, "8", OpenID, VipID, loggingSessionInfo);
       }
 
    }
}

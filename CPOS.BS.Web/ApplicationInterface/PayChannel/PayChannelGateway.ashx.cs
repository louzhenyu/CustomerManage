using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.IO;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.ApplicationInterface.PayChannel
{
    /// <summary>
    /// PayChannelGateway 的摘要说明
    /// </summary>
    public class PayChannelGateway : BaseGateway
    {
        public string SetPayChannel(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetPayChannel>>();

            if (rp.Parameters.AddPayChannelData.Length == 0)
            {
                throw new APIException("请求参数中缺少AddPayChannelData或值为空.") { ErrorCode = 121 };
            }

            var paymentTypeId = rp.Parameters.AddPayChannelData[0].PaymentTypeId;
            


            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var tPaymentTypeCustomerMappingBll = new TPaymentTypeCustomerMappingBLL(loggingSessionInfo);
            var customerId = loggingSessionInfo.ClientID;

            
            //获取该支付通道的信息
            var tPaymeentTypeCusMapEntity = tPaymentTypeCustomerMappingBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity()
            {
                ChannelId = Convert.ToString(rp.Parameters.AddPayChannelData[0].ChannelId),
                CustomerId = customerId
            },null);

            if (tPaymeentTypeCusMapEntity != null && tPaymeentTypeCusMapEntity.Length > 0)
            {
                var payDeloyType = tPaymeentTypeCusMapEntity[0].PayDeplyType;

                //如果该支付通道之前为默认配置，将channelId设置为0，这样支付中心会生成一条新的数据
                if (payDeloyType == 0)
                {
                    rp.Parameters.AddPayChannelData[0].ChannelId = 0;
                }
            }

            var tPaymentTypeCustomerMappingEntityList =
                tPaymentTypeCustomerMappingBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity()
                {
                    PaymentTypeID = rp.Parameters.AddPayChannelData[0].PaymentTypeId,
                    CustomerId = customerId
                }, null);

            var paychannelUrl = ConfigurationManager.AppSettings["payChannelUrl"];
          
            string reqs = "request" + pRequest;

            string str = "{\"ClientID\":\"" + loggingSessionInfo.ClientID + "\","
                         + "\"UserID\":\"" + loggingSessionInfo.UserID + "\","
                         +"\"Token\":null,\"AppID\":1,"
                         + "\"Parameters\":" + rp.Parameters.ToJSON() + "}";
            var result = HttpWebClient.DoHttpRequest(paychannelUrl + "Gateway.ashx?action=SetPayChannel", "request="+
                HttpUtility.UrlEncode(str));

            string loggerStr = paychannelUrl+ "Gateway.ashx?action=SetPayChannel&" + 
                    str;
            Loggers.Debug(new DebugLogInfo()
            {
                Message = loggerStr
            });

            var channelList = result.DeserializeJSONTo<PayChannelReturnResult>();

            if (channelList.Datas == null)
            {
                throw new APIException("创建失败：" + result) { ErrorCode = 122 };
            }           

            if (channelList.Datas.PayChannelIdList[0].ChannelId == 0)
            {
                throw new APIException("创建失败：" + result) { ErrorCode = 122 };
            }
            else
            {
                if (tPaymentTypeCustomerMappingEntityList != null && tPaymentTypeCustomerMappingEntityList.Length > 0)
                {
                    tPaymentTypeCustomerMappingBll.Delete(tPaymentTypeCustomerMappingEntityList);
                }


                var tPaymentTypeCustomerMappingEntity = new TPaymentTypeCustomerMappingEntity();

                tPaymentTypeCustomerMappingEntity.MappingId = Guid.NewGuid();
                tPaymentTypeCustomerMappingEntity.CustomerId = loggingSessionInfo.ClientID;
                tPaymentTypeCustomerMappingEntity.PaymentTypeID = rp.Parameters.AddPayChannelData[0].PaymentTypeId;
                tPaymentTypeCustomerMappingEntity.APPId = "1";
                tPaymentTypeCustomerMappingEntity.Currency = 1;
                tPaymentTypeCustomerMappingEntity.PayDeplyType = 1;

                tPaymentTypeCustomerMappingBll.Create(tPaymentTypeCustomerMappingEntity);            
         
                
                var payTypeId = rp.Parameters.AddPayChannelData[0].PayType;

                var updateSql = "";
                if (payTypeId == 1 || payTypeId == 2)
                {
                    var unionPayData = rp.Parameters.AddPayChannelData[0].UnionPayData;

                    var merchantId = unionPayData.MerchantID;
                    var certificateFilePath = unionPayData.CertificateFilePath;
                    var certificateFilePassword = unionPayData.CertificateFilePassword;
                    var decryptCertificateFilePath = unionPayData.DecryptCertificateFilePath;
                    var packetEncryptKey = unionPayData.PacketEncryptKey;

                    updateSql = "PayAccountNumber = '" + merchantId + "',"
                    + "EncryptionCertificate = '" + certificateFilePath + "',"
                    + "EncryptionPwd ='" + certificateFilePassword + "',"
                    + "DecryptionCertificate ='" + decryptCertificateFilePath + "',"
                    + "DecryptionPwd ='" + packetEncryptKey + "',"
                    + "PayDeplyType=1";
  
                    
                }
                if (payTypeId == 3 || payTypeId == 4)
                {
                    var wapData = rp.Parameters.AddPayChannelData[0].WapData;

                    var partner = wapData.Partner;
                    var sellerAccountName = wapData.SellerAccountName;
                    var RSA_PublicKey = wapData.RSA_PublicKey;
                    var RSA_PrivateKey = wapData.RSA_PrivateKey;
                    var MD5Key = wapData.MD5Key;

                    updateSql = "PayAccountNumber ='" + partner + "',"
                            + "PayAccounPublic='" + RSA_PublicKey + "',"
                            + "PayPrivate='" + RSA_PrivateKey + "',"
                            + "SalesTBAccess='" + sellerAccountName + "',"
                            + "ApplyMD5Key='" + MD5Key + "',"
                            + "PayDeplyType=1";
                }
                if (payTypeId == 5 || payTypeId == 6)
                {
                    var wxData = rp.Parameters.AddPayChannelData[0].WxPayData;
                    var appID = wxData.AppID;
                    var appSecret = wxData.AppSecret;
                    var parnterID = wxData.ParnterID;
                    var parnterKey = wxData.ParnterKey;
                    var PaySignKey = wxData.PaySignKey;

                    updateSql = "AccountIdentity='" + appID + "',"
                        + "PublicKey='" + appSecret + "',"
                        + "TenPayIdentity='" + parnterID + "',"
                        + "TenPayKey='" + parnterKey + "',"
                        + "PayEncryptedPwd='" + PaySignKey + "',"
                        + "PayDeplyType=1";
                }

                tPaymentTypeCustomerMappingBll.UpdatePaymentMap(updateSql, channelList.Datas.PayChannelIdList[0].ChannelId
                    , rp.Parameters.AddPayChannelData[0].PaymentTypeId, customerId);
            }

            var rd = new EmptyResponseData();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();

        }
        public string SetDefaultPayChannel(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetPayChannel>>();
            if (rp.Parameters.AddPayChannelData.Length == 0)
            {
                throw new APIException("请求参数中缺少AddPayChannelData或值为空.") { ErrorCode = 121 };
            }

            var paymentTypeId = rp.Parameters.AddPayChannelData[0].PaymentTypeId;
           

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var tPaymentTypeCustomerMappingBll = new TPaymentTypeCustomerMappingBLL(loggingSessionInfo);
            var customerId = loggingSessionInfo.ClientID;

            var tPaymentTypeCustomerMappingEntityList =
                tPaymentTypeCustomerMappingBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity()
                {
                    PaymentTypeID = rp.Parameters.AddPayChannelData[0].PaymentTypeId,
                    CustomerId = customerId
                }, null);
          
            //从ap库里面获取channelID

            var channelId = tPaymentTypeCustomerMappingBll.GetChannelIdByPaymentTypeAndCustomer(paymentTypeId, customerId);


            if (channelId == "-1")
            {
                throw new APIException("未找到默认的支付通道.") { ErrorCode = 122 };
            }
            else
            {
                if (tPaymentTypeCustomerMappingEntityList != null && tPaymentTypeCustomerMappingEntityList.Length > 0)
                {
                    tPaymentTypeCustomerMappingBll.Delete(tPaymentTypeCustomerMappingEntityList);
                }
                var tPaymentTypeCustomerMappingEntity = new TPaymentTypeCustomerMappingEntity();
                tPaymentTypeCustomerMappingEntity.MappingId = Guid.NewGuid();
                tPaymentTypeCustomerMappingEntity.PaymentTypeID = paymentTypeId;
                tPaymentTypeCustomerMappingEntity.CustomerId = customerId;
                tPaymentTypeCustomerMappingEntity.ChannelId = channelId;
                tPaymentTypeCustomerMappingEntity.APPId = "1";
                tPaymentTypeCustomerMappingEntity.Currency = 1;
                tPaymentTypeCustomerMappingEntity.PayDeplyType = 0;
                tPaymentTypeCustomerMappingBll.Create(tPaymentTypeCustomerMappingEntity);

                string updateSql =  " PayDeplyType=0 ";
                tPaymentTypeCustomerMappingBll.UpdatePaymentMap(updateSql, Convert.ToInt32(channelId)
                   , paymentTypeId, customerId);
            }

           
            var rd = new EmptyResponseData();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "SetPayChannel":
                    rst = this.SetPayChannel(pRequest);
                    break;
                case "SetDefaultPayChannel":
                    rst = this.SetDefaultPayChannel(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }   

    #region 创建支付通道参数类

    public class SetPayChannel : IAPIRequestParameter
    {
        public SetPayChannelList[] AddPayChannelData { get; set; }


        public void Validate()
        {            
        }
    }



    public class SetPayChannelList
    {
        public int ChannelId { get; set; }

        public string PaymentTypeId { get; set; }        

        public int PayType { get; set; }

        public string NotifyUrl { get; set; }

        public WapInfo WapData { get; set; }

        public WxPayInfo WxPayData { get; set; }

        public UnionPayInfo UnionPayData { get; set; }


    }

    public class WapInfo
    {
        /// <summary>
        /// 帐号（线上，线下）
        /// </summary>
        public string Partner { get; set; }
        /// <summary>
        /// 卖家淘宝帐号（线上）
        /// </summary>
        public string SellerAccountName { get; set; }
        /// <summary>
        /// 公钥（线上）
        /// </summary>
        public string RSA_PublicKey { get; set; }
        /// <summary>
        /// 私钥（线上）
        /// </summary>
        public string RSA_PrivateKey { get; set; }
        /// <summary>
        /// 密钥（线下）
        /// </summary>
        public string MD5Key { get; set; }

        public string AgentID { get; set; }
        
    }

    public class WxPayInfo
    {
        /// <summary>
        /// 微信公众号
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 权限获取所需密钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 财付通商户身份标识
        /// </summary>
        public string ParnterID { get; set; }
        /// <summary>
        /// 财付通商户权限密钥
        /// </summary>
        public string ParnterKey { get; set; }
        /// <summary>
        /// 加密的密钥
        /// </summary>
        public string PaySignKey { get; set; }
        public string NotifyToTradeCenterURL { get; set; }
        public string NotifyToBussinessSystemURL { get; set; }

    }

    public class UnionPayInfo
    {
        /// <summary>
        ///  账户ID
        /// </summary>
        public string MerchantID { get; set; }
        /// <summary>
        /// 加密证书路径  
        /// </summary>
        public string CertificateFilePath { get; set; }
        /// <summary>
        /// 加密证书密码
        /// </summary>
        public string CertificateFilePassword { get; set; }
        /// <summary>
        /// 解密证书路径
        /// </summary>
        public string DecryptCertificateFilePath { get; set; }
        /// <summary>
        /// 解密证书密码
        /// </summary>
        public string PacketEncryptKey { get; set; }


    }  


    public class PayChannelResponse
    {
        public int ChannelId { get; set; }
    }
    public class PayChannelReturnResult
    {
        public PayChannelResponsList Datas { get; set; }
    }
    public class PayChannelResponsList
    {
        
        public List<PayChannelResponse> PayChannelIdList { get; set; }

    }
    #endregion
}
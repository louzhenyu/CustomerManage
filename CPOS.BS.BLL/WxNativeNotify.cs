using CPOS.Common;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    public class WxNativeNotify
    {
        public T_InoutEntity inoutEntity { get; set; }
        public TPaymentTypeCustomerMappingEntity paymentTypeEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ProcessNotify(string dataXml)
        {
            var rest = GetNotifyData(dataXml);
            if (!rest.Item1)
            {
                return rest.Item2.ToXml();
            }
            WxPayData result = new WxPayData();
            var notifyData = rest.Item2;
            if (!notifyData.IsSet("openid") || !notifyData.IsSet("product_id"))
            {

                result.SetValue("return_code", "FAIL");
                result.SetValue("return_msg", "回调数据异常");
                return result.ToXml();
            }

            string openid = notifyData.GetValue("openid").ToString();
            string product_id = notifyData.GetValue("product_id").ToString();
            // string 

            try
            {
                var loggingSessionInfo = BaseService.GetLoggingSession();
                var inoutBll = new T_InoutBLL(loggingSessionInfo);
                inoutEntity = inoutBll.GetByID(product_id);

                if (inoutEntity != null && inoutEntity.Field1 == "1")
                {
                    WxPayData res = new WxPayData();
                    res.SetValue("return_code", "FAIL");
                    res.SetValue("return_msg", "无效二维码");
                    return res.ToXml();
                }

                var TPaymentType = new TPaymentTypeCustomerMappingBLL(loggingSessionInfo);

                paymentTypeEntity = TPaymentType.QueryByEntity(new TPaymentTypeCustomerMappingEntity()
                {
                    CustomerId = inoutEntity.customer_id,
                    IsDelete = 0,
                    // AccountIdentity = "wxa0db700325ddd846"   // tonys
                    AccountIdentity = "wxb4f8f3d799d22f03"   // zmind
                }, null).FirstOrDefault();

                result = UnifiedOrder(openid, product_id, paymentTypeEntity, inoutEntity);
            }
            catch (Exception ex)
            {
                result.SetValue("return_code", "FAIL");
                result.SetValue("return_msg", "统一下单失败");
                return result.ToXml();
            }

            if (!result.IsSet("appid") || !result.IsSet("mch_id") || !result.IsSet("prepay_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");
                return res.ToXml();
            }

            WxPayData data = new WxPayData();
            data.SetValue("return_code", "SUCCESS");
            data.SetValue("return_msg", "OK");
            data.SetValue("appid", paymentTypeEntity.APPId);
            data.SetValue("mch_id", paymentTypeEntity.TenPayIdentity);
            data.SetValue("nonce_str", Utils.GenerateNonceStr());
            data.SetValue("prepay_id", result.GetValue("prepay_id"));
            data.SetValue("result_code", "SUCCESS");
            data.SetValue("err_code_des", "OK");
            data.SetValue("sign", data.MakeSign(paymentTypeEntity.TenPayKey));
            return data.ToXml();

        }

        private WxPayData UnifiedOrder(string openid, string product_id, TPaymentTypeCustomerMappingEntity paymentTypeEntity, T_InoutEntity inoutEntity)
        {
            //统一下单
            WxPayData req = new WxPayData();
            req.SetValue("body", string.Format("{0}元储值卡", (int)inoutEntity.total_amount));
            req.SetValue("attach", "多利农庄储值卡+");
            req.SetValue("out_trade_no", product_id);
            // req.SetValue("total_fee", Convert.ToInt32(inoutEntity.total_amount * 100)); // 单位分
            req.SetValue("total_fee", 1);
            req.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            req.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            req.SetValue("goods_tag", "储值卡");
            req.SetValue("trade_type", "NATIVE");
            req.SetValue("openid", openid);
            req.SetValue("product_id", product_id);
            WxPayData result = UnifiedOrder(req, paymentTypeEntity);
            return result;
        }

        private WxPayData UnifiedOrder(WxPayData inputObj, TPaymentTypeCustomerMappingEntity paymentTypeEntity)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                inputObj.SetValue("notify_url", "http://14w97e7933.iask.in/Notify/WxScanQrCodePayNotify.ashx");//异步通知url
            }

            // inputObj.SetValue("appid", "wxb4f8f3d799d22f03");//公众账号ID--paymentTypeEntity.APPId
            inputObj.SetValue("appid", paymentTypeEntity.AccountIdentity);//公众账号ID--
            inputObj.SetValue("mch_id", paymentTypeEntity.TenPayIdentity);//商户号
            inputObj.SetValue("spbill_create_ip", Utils.GetLocalIp());//终端ip	  	    
            inputObj.SetValue("nonce_str", Utils.GenerateNonceStr());//随机字符串

            //签名
            inputObj.SetValue("sign", inputObj.MakeSign(paymentTypeEntity.TenPayKey));
            string xml = inputObj.ToXml();

            var start = DateTime.Now;
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("UnfiedOrder request : {0}", xml)
            });
            string response = CommonBLL.HttpPost(url, xml); //HttpService.Post(xml, url, false, timeOut);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("UnfiedOrder response12345678555 : {0}", response)
            });

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            WxPayData result = new WxPayData();
            result.FromXml(response);
            return result;
        }

        #region 接收从微信支付后台发送过来的数据并验证签名
        /// <summary>
        /// 接收从微信支付后台发送过来的数据并验证签名
        /// </summary>
        /// <returns>微信支付后台返回的数据</returns>
        public Tuple<bool, WxPayData> GetNotifyData(string dataXml)
        {
            //接收从微信后台POST过来的数据


            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            bool isOk = true;
            try
            {
                data.FromXml(dataXml.ToString());
            }
            catch (Exception ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                data.SetValue("return_code", "FAIL");
                data.SetValue("return_msg", ex.Message);
                isOk = false;
                JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = "请求:" + this.GetType().ToString() + "Sign check error : " + data.ToXml() });
            }


            JIT.Utility.Log.Loggers.Debug(new DebugLogInfo() { Message = "请求:" + this.GetType().ToString() + "::Check sign success" });
            return new Tuple<bool, WxPayData>(isOk, data);
        }
        #endregion
    }
}

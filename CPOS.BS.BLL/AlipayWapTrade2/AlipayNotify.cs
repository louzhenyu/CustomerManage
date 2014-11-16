using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL.AlipayWapTrade2
{
    /// <summary>
    /// 类名：Notify
    /// 功能：支付宝通知处理类
    /// 详细：处理支付宝各接口通知返回
    /// 版本：3.3
    /// 修改日期：2011-07-05
    /// '说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// //////////////////////注意/////////////////////////////
    /// 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    /// </summary>
    public class AlipayNotify
    {
        #region 字段
        private string _partner = "";               //合作身份者ID
        private string _key = "";                   //支付宝MD5私钥
        private string _private_key = "";           //商户的私钥
        private string _public_key = "";            //支付宝的公钥
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";             //签名方式

        //支付宝消息验证地址
        private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";
        #endregion


        /// <summary>
        /// 构造函数
        /// 从配置文件中初始化变量
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notify_id">通知验证ID</param>
        public AlipayNotify()
        {
            //初始化基础配置信息
            _partner = AlipayConfig.Partner.Trim();
            _key = AlipayConfig.Key.Trim();
            _private_key = AlipayConfig.Private_key.Trim();
            _public_key = AlipayConfig.Public_key.Trim();
            _input_charset = AlipayConfig.Input_charset.Trim().ToLower();
            _sign_type = AlipayConfig.Sign_type.Trim().ToUpper();
        }

        /// <summary>
        ///  验证消息是否是支付宝发出的合法消息，验证callback
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">支付宝生成的签名结果</param>
        /// <returns>验证结果</returns>
        public bool VerifyReturn(Dictionary<string, string> inputPara, string sign)
        {
            BaseService.WriteLog("验证消息是否是支付宝发出的合法消息，验证callback AlipayNotify.VerifyReturn()");

            //获取返回时的签名验证结果
            bool isSign = GetSignVeryfy(inputPara, sign, true);

            //写日志记录（若要调试，请取消下面两行注释）
            string sWord = "isSign=" + isSign.ToString() + "\n 返回回来的参数：" + GetPreSignStr(inputPara) + "\n ";
            BaseService.WriteLog("同步通知结果：" + sWord);

            //判断isSign是否为true
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (isSign)//验证成功
            {
                return true;
            }
            else//验证失败
            {
                return false;
            }
        }

        /// <summary>
        ///  验证消息是否是支付宝发出的合法消息，验证服务器异步通知
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">支付宝生成的签名结果</param>
        /// <returns>验证结果</returns>
        public bool VerifyNotify(Dictionary<string, string> inputPara, string sign)
        {
            BaseService.WriteLog("验证消息是否是支付宝发出的合法消息，验证服务器异步通知 AlipayNotify.VerifyNotify()");
            BaseService.WriteLog("inputPara：" + inputPara.ToString());
            BaseService.WriteLog("sign：" + sign);
            BaseService.WriteLog("_sign_type：" + _sign_type);
            BaseService.WriteLog("notify_data解密前：" + inputPara["notify_data"]);

            var tradeStatus = string.Empty;

            //解密
            if (_sign_type == "0001")
            {
                inputPara = Decrypt(inputPara);
                BaseService.WriteLog("notify_data解密后：" + inputPara["notify_data"]);
            }

            //获取是否是支付宝服务器发来的请求的验证结果
            string responseTxt = "true";
            try
            {
                //XML解析notify_data数据，获取notify_id
                string notify_id = "";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(inputPara["notify_data"]);
                notify_id = xmlDoc.SelectSingleNode("/notify/notify_id").InnerText;

                BaseService.WriteLog("notify_id： " + notify_id);
                BaseService.WriteLog("开始更新支付宝交易信息");
                //更新支付宝交易信息
                UpdateAlipayWapTrade(xmlDoc, "1");

                if (notify_id != "") { responseTxt = GetResponseTxt(notify_id); }

                BaseService.WriteLog("responseTxt：" + responseTxt);

                tradeStatus = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText;
                BaseService.WriteLog("tradeStatus：" + tradeStatus);
            }
            catch (Exception e)
            {
                BaseService.WriteLog("异常信息：" + e.ToString());
                responseTxt = e.ToString();
            }

            //获取返回时的签名验证结果
            bool isSign = false;
            if (tradeStatus == "TRADE_SUCCESS" || tradeStatus == "TRADE_FINISHED")
            {
                isSign = true;
            }

            //写日志记录（若要调试，请取消下面两行注释）
            string sWord = "responseTxt=" + responseTxt + "\n isSign=" + isSign.ToString() + "\n 返回回来的参数：" + GetPreSignStr(inputPara) + "\n ";
            BaseService.WriteLog("异步通知结果：" + sWord);
            //Core.LogResult(sWord);

            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (responseTxt == "true" && isSign)//验证成功
            {
                return true;
            }
            else//验证失败
            {
                return false;
            }
        }

        /// <summary>
        /// 获取待签名字符串（调试用）
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <returns>待签名字符串</returns>
        public string GetPreSignStr(Dictionary<string, string> inputPara)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = AlipayCore.FilterPara(inputPara);

            //根据字母a到z的顺序把参数排序
            sPara = AlipayCore.SortPara(sPara);

            //获取待签名字符串
            string preSignStr = AlipayCore.CreateLinkString(sPara);

            return preSignStr;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inputPara">要解密数据</param>
        /// <returns>解密后结果</returns>
        public Dictionary<string, string> Decrypt(Dictionary<string, string> inputPara)
        {
            try
            {
                inputPara["notify_data"] = RSAFromPkcs8.decryptData(inputPara["notify_data"], _private_key, _input_charset);
            }
            catch (Exception e) { }

            return inputPara;
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <param name="isSort">是否对待签名数组排序</param>
        /// <returns>签名验证结果</returns>
        private bool GetSignVeryfy(Dictionary<string, string> inputPara, string sign, bool isSort)
        {
            BaseService.WriteLog("RSA获取返回时的签名验证结果： AlipayNotify.GetSignVeryfy()");
            BaseService.WriteLog("sign: " + sign);
            BaseService.WriteLog("isSort: " + isSort);

            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = AlipayCore.FilterPara(inputPara);

            if (isSort)
            {
                //根据字母a到z的顺序把参数排序
                sPara = AlipayCore.SortPara(sPara);
                BaseService.WriteLog("排序后 sPara: " + sPara.ToString());
            }

            //获取待签名字符串
            string preSignStr = AlipayCore.CreateLinkString(sPara);
            BaseService.WriteLog("preSignStr: " + preSignStr);

            //获得签名验证结果
            bool isSgin = false;
            if (sign != null && sign != "")
            {
                BaseService.WriteLog("_sign_type: " + _sign_type);
                switch (_sign_type)
                {
                    case "MD5":
                        isSgin = AlipayMD5.Verify(preSignStr, sign, _key, _input_charset);
                        break;
                    case "RSA":
                        isSgin = RSAFromPkcs8.verify(preSignStr, sign, _public_key, _input_charset);
                        break;
                    case "0001":
                        isSgin = RSAFromPkcs8.verify(preSignStr, sign, _public_key, _input_charset);
                        break;
                    default:
                        break;
                }
            }

            BaseService.WriteLog("isSgin: " + isSgin);
            return isSgin;
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求的验证结果
        /// </summary>
        /// <param name="notify_id">通知验证ID</param>
        /// <returns>验证结果</returns>
        private string GetResponseTxt(string notify_id)
        {
            string veryfy_url = Https_veryfy_url + "partner=" + _partner + "&notify_id=" + notify_id;

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt = Get_Http(veryfy_url, 120000);

            return responseTxt;
        }

        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="strUrl">指定URL路径地址</param>
        /// <param name="timeout">超时时间设置</param>
        /// <returns>服务器ATN结果</returns>
        private string Get_Http(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }

        /// <summary>
        /// 交易成功，更新支付宝交易状态
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void UpdateAlipayWapTrade(XmlDocument xmlDoc, string status)
        {
            BaseService.WriteLog("OutTradeNo： " + xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText);
            BaseService.WriteLog("Subject： " + xmlDoc.SelectSingleNode("/notify/subject").InnerText);
            BaseService.WriteLog("TotalFee： " + xmlDoc.SelectSingleNode("/notify/total_fee").InnerText);
            BaseService.WriteLog("PaymentType： " + xmlDoc.SelectSingleNode("/notify/payment_type").InnerText);
            BaseService.WriteLog("TradeNo： " + xmlDoc.SelectSingleNode("/notify/trade_no").InnerText);
            BaseService.WriteLog("BuyerEmail： " + xmlDoc.SelectSingleNode("/notify/buyer_email").InnerText);
            BaseService.WriteLog("GmtCreate： " + xmlDoc.SelectSingleNode("/notify/gmt_create").InnerText);
            BaseService.WriteLog("NotifyType： " + xmlDoc.SelectSingleNode("/notify/notify_type").InnerText);
            BaseService.WriteLog("Quantity： " + xmlDoc.SelectSingleNode("/notify/quantity").InnerText);
            BaseService.WriteLog("NotifyTime： " + xmlDoc.SelectSingleNode("/notify/notify_time").InnerText);
            BaseService.WriteLog("SellerID： " + xmlDoc.SelectSingleNode("/notify/seller_id").InnerText);
            BaseService.WriteLog("TradeStatus： " + xmlDoc.SelectSingleNode("/notify/trade_status").InnerText);
            BaseService.WriteLog("IsTotalFeeAdjust： " + xmlDoc.SelectSingleNode("/notify/is_total_fee_adjust").InnerText);
            BaseService.WriteLog("GmtPayment： " + xmlDoc.SelectSingleNode("/notify/gmt_payment").InnerText);
            BaseService.WriteLog("SellerEmail： " + xmlDoc.SelectSingleNode("/notify/seller_email").InnerText);
            //BaseService.WriteLog("GmtClose： " + xmlDoc.SelectSingleNode("/notify/gmt_close").InnerText);
            BaseService.WriteLog("Price： " + xmlDoc.SelectSingleNode("/notify/price").InnerText);
            BaseService.WriteLog("BuyerID： " + xmlDoc.SelectSingleNode("/notify/buyer_id").InnerText);
            BaseService.WriteLog("NotifyID： " + xmlDoc.SelectSingleNode("/notify/notify_id").InnerText);
            BaseService.WriteLog("UseCoupon： " + xmlDoc.SelectSingleNode("/notify/use_coupon").InnerText);
            BaseService.WriteLog("Status： " + status);

            var alipayEntity = new AlipayWapTradeResponseEntity()
            {
                OutTradeNo = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText,
                Subject = xmlDoc.SelectSingleNode("/notify/subject").InnerText,
                TotalFee = xmlDoc.SelectSingleNode("/notify/total_fee").InnerText,
                PaymentType = xmlDoc.SelectSingleNode("/notify/payment_type").InnerText,
                TradeNo = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText,
                BuyerEmail = xmlDoc.SelectSingleNode("/notify/buyer_email").InnerText,
                GmtCreate = xmlDoc.SelectSingleNode("/notify/gmt_create").InnerText,
                NotifyType = xmlDoc.SelectSingleNode("/notify/notify_type").InnerText,
                Quantity = xmlDoc.SelectSingleNode("/notify/quantity").InnerText,
                NotifyTime = xmlDoc.SelectSingleNode("/notify/notify_time").InnerText,
                SellerID = xmlDoc.SelectSingleNode("/notify/seller_id").InnerText,
                TradeStatus = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText,
                IsTotalFeeAdjust = xmlDoc.SelectSingleNode("/notify/is_total_fee_adjust").InnerText,
                GmtPayment = xmlDoc.SelectSingleNode("/notify/gmt_payment").InnerText,
                SellerEmail = xmlDoc.SelectSingleNode("/notify/seller_email").InnerText,
                //GmtClose = xmlDoc.SelectSingleNode("/notify/gmt_close").InnerText,
                Price = xmlDoc.SelectSingleNode("/notify/price").InnerText,
                BuyerID = xmlDoc.SelectSingleNode("/notify/buyer_id").InnerText,
                NotifyID = xmlDoc.SelectSingleNode("/notify/notify_id").InnerText,
                UseCoupon = xmlDoc.SelectSingleNode("/notify/use_coupon").InnerText,
                //Status = status
            };

            BaseService.WriteLog("更新支付宝交易信息");

            AlipayWapTradeResponseBLL alipayServer = new AlipayWapTradeResponseBLL(new JIT.Utility.BasicUserInfo());
            alipayServer.UpdateAlipayWapTrade(alipayEntity);
        }
    }
}

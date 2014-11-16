
using System.Configuration;

namespace JIT.CPOS.BS.BLL.AlipayWapTrade
{
    /// <summary>
    /// 类名：Config
    /// 功能：支付宝配置公共类
    /// 详细：该类是配置所有请求参数，支付宝网关、接口，商户的基本参数等
    /// </summary>
    public class Config
    {
        static Config()
        {
            #region 支付宝参数，必须按照以下值传递

            Req_url = "http://wappaygw.alipay.com/service/rest.htm";

            V = "2.0";
            Service_Create = "alipay.wap.trade.create.direct";
            Service_Auth = "alipay.wap.auth.authAndExecute";
            Sec_id = "0001";
            Format = "xml";
            Input_charset_UTF8 = "UTF-8";

            #endregion

            #region 商户需要手动配置

            //商户ID
            Partner = "2088701598244705";
            //卖价账户名称
            Seller_account_name = "account@jitmarketing.cn";
            //支付宝公钥
            Alipaypublick = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCidRWLiJfzFuKYNYyoW27s70e9yStuiE1Et4aQ qUGL4Z4/vG6p8sJ3JALUTQwZmHwY7GeTqA+n2nUSqpFQLfqUeTBS5IDnxR+5DqL5lOaCDDDw3Uxn CnBdBcfuiwjsEXaDv4sqi/So6tmQlVgi9wFlRc87uM6/kL+WxkhkenIgMwIDAQAB";
            //商户私钥
            PrivateKey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALEtRLU0wUHcmn8seP8Du8P6jBe57/igqtqwKoPU8t6Zxr/U8pNB+ORj4uTLblvT+KdYCR1TIbkvaZWkHaD/dlcVw+xM8dWqvM86Hf+NB7y7st1mzN9xVhkVwr3sc/2B4J3jOQMpEkN19yejf3KCo779zYdldWi6Kk9hpQeYYNqdAgMBAAECgYEAhsoLlVe3FqX/m3R38HokpKm9XmeEWr/Qe2K+VWDyC8stWs9kZAcylH4xJSJmqNGQP69H79lItJuPVdpu+AahPccs+DBt/moUCdsP5IZphk4gNVTJDPxzqO2mPiGJH+gjK0u/DhiOQUKUcOMjTgEkE+U5d0ftjVxm+WT5WiLS3AECQQDXky9sJmgQ2fU+iYnuGTQDs8NXghUgdwd6RjyTf1lOjlBMnZEUhdtDJ4tQe9X9CjgHwrZXF7iOlRV0mIt7FZQdAkEA0mbBwlCYJVluDFSJORzdZmpllZcfRncjQ3vepNvdYgppXJhLNqaLAbSTHXBbIsTTsiVUOi4ipA8crf4FJ9CYgQJAQlVR9E9lGjpXEmU0AgXTUYhRBW5Lne/CZ0eRgDlhe6Ci6NBbQhtmOqXCYoOYdwJb91dc0DPGYGlTbss5sCgVqQJALGWceylQgYkWbKml7xRFL6hB2UfzRIY9Pa80surmExsJUo2cSWLpMCnvZSXhRTvtQ8kWtdQoYSADOD/CzLz6gQJAKixONE4XyXercbnLKUXcMZ6NKpEaoS5eaTAkyzpHMxWmNV1S5k1pViKq8wMhY/GarOEIOEcTbEtYv32zeOvHpw==";

            Req_id = System.DateTime.Now.ToString();
            Out_trade_no = System.DateTime.Now.ToString();
            Subject = "百搭花朵珍珠胸花";
            Total_fee = "0.01";
            Out_user = "jitmarketing";

            //三个返回URL
            var host = ConfigurationManager.AppSettings["website_url"];
            Call_back_url = host + "AlipayWapTrade/Call_Back.aspx";
            Notify_url = host + "AlipayWapTrade/Notify.aspx";
            Merchant_url = host + "AlipayWapTrade/PaySuccess.aspx";

            #endregion
        }

        #region 属性

        /// <summary>
        /// 请求ID 请随机生成
        /// </summary>
        public static string Req_id { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public static string Req_url { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public static string V { get; set; }

        /// <summary>
        /// 创建交易网关
        /// </summary>
        public static string Service_Create { get; set; }

        /// <summary>
        /// 执行授权网关
        /// </summary>
        public static string Service_Auth { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public static string Partner { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public static string Sec_id { get; set; }

        /// <summary>
        /// 支付宝公钥
        /// </summary>
        public static string Alipaypublick { get; set; }

        /// <summary>
        /// 商户私钥
        /// </summary>
        public static string PrivateKey { get; set; }

        /// <summary>
        /// 请求参数格式
        /// </summary>
        public static string Format { get; set; }

        /// <summary>
        /// 用户付款成功同步返回URL
        /// </summary>
        public static string Call_back_url { get; set; }

        /// <summary>
        /// 外部交易号(由商户创建，请不要重复)
        /// </summary>
        public static string Out_trade_no { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        public static string Subject { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>
        public static string Total_fee { get; set; }

        /// <summary>
        /// 卖家账户名称
        /// </summary>
        public static string Seller_account_name { get; set; }

        /// <summary>
        /// 外部用户唯一标识
        /// </summary>
        public static string Out_user { get; set; }

        /// <summary>
        /// 服务端接收通知URL
        /// </summary>
        public static string Notify_url { get; set; }

        /// <summary>
        /// 用户付款中途退出返回URL
        /// </summary>
        public static string Merchant_url { get; set; }

        /// <summary>
        /// 编码格式UTF-8
        /// </summary>
        public static string Input_charset_UTF8 { get; set; }

        #endregion
    }
}

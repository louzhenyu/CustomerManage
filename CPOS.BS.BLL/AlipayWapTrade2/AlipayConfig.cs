
using System;
using System.Configuration;
namespace JIT.CPOS.BS.BLL.AlipayWapTrade2
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class AlipayConfig
    {
        static AlipayConfig()
        {
            #region 支付宝参数，必须按照以下值传递

            //字符编码格式 目前支持 utf-8
            Input_charset = "utf-8";
            //签名方式，选择项：0001(RSA)、MD5
            //无线的产品中，签名方式为rsa时，sign_type需赋值为0001而不是RSA
            Sign_type = "0001";

            Service_Create = "alipay.wap.trade.create.direct";
            Service_Execute = "alipay.wap.auth.authAndExecute";
            Format = "xml";
            V = "2.0";
            Req_url = "http://wappaygw.alipay.com/service/rest.htm?";

            #endregion

            #region 商户需要手动配置

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            Partner = "2088701598244705";
            //Partner = "2088011289712913";

            //卖价账户名称
            Seller_account_name = "account@jitmarketing.cn";
            //Seller_account_name = "zhifubao@weixun.co";

            //支付宝的公钥
            //如果签名方式设置为“0001”时，请设置该参数
            Public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCidRWLiJfzFuKYNYyoW27s70e9yStuiE1Et4aQ qUGL4Z4/vG6p8sJ3JALUTQwZmHwY7GeTqA+n2nUSqpFQLfqUeTBS5IDnxR+5DqL5lOaCDDDw3Uxn CnBdBcfuiwjsEXaDv4sqi/So6tmQlVgi9wFlRc87uM6/kL+WxkhkenIgMwIDAQAB";
            //Public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCPnN+aKYGCd9Eoy2uzfs6hf9F09+9bIUcdJnn/O9Pu2wpdPz+VcVr2gyesMwK+nAjkpdvjjiXRwYfl6mxd3SNhqUaJYEUsWKQwNhLPYWUEtrraVKRPy41Vbm64LJ1CRUXC7ORx2/1BK0ip/KQyaIzzb5m5Q0ry3k0ockcBp6PawwIDAQAB";

            //商户的私钥
            //如果签名方式设置为“0001”时，请设置该参数
            Private_key = @"MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALEtRLU0wUHcmn8seP8Du8P6jBe57/igqtqwKoPU8t6Zxr/U8pNB+ORj4uTLblvT+KdYCR1TIbkvaZWkHaD/dlcVw+xM8dWqvM86Hf+NB7y7st1mzN9xVhkVwr3sc/2B4J3jOQMpEkN19yejf3KCo779zYdldWi6Kk9hpQeYYNqdAgMBAAECgYEAhsoLlVe3FqX/m3R38HokpKm9XmeEWr/Qe2K+VWDyC8stWs9kZAcylH4xJSJmqNGQP69H79lItJuPVdpu+AahPccs+DBt/moUCdsP5IZphk4gNVTJDPxzqO2mPiGJH+gjK0u/DhiOQUKUcOMjTgEkE+U5d0ftjVxm+WT5WiLS3AECQQDXky9sJmgQ2fU+iYnuGTQDs8NXghUgdwd6RjyTf1lOjlBMnZEUhdtDJ4tQe9X9CjgHwrZXF7iOlRV0mIt7FZQdAkEA0mbBwlCYJVluDFSJORzdZmpllZcfRncjQ3vepNvdYgppXJhLNqaLAbSTHXBbIsTTsiVUOi4ipA8crf4FJ9CYgQJAQlVR9E9lGjpXEmU0AgXTUYhRBW5Lne/CZ0eRgDlhe6Ci6NBbQhtmOqXCYoOYdwJb91dc0DPGYGlTbss5sCgVqQJALGWceylQgYkWbKml7xRFL6hB2UfzRIY9Pa80surmExsJUo2cSWLpMCnvZSXhRTvtQ8kWtdQoYSADOD/CzLz6gQJAKixONE4XyXercbnLKUXcMZ6NKpEaoS5eaTAkyzpHMxWmNV1S5k1pViKq8wMhY/GarOEIOEcTbEtYv32zeOvHpw==";
            //Private_key = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAO/cxqSMduqJ2mWTwbFRN4HMcvgnDW/Oc+UDPeO4rCNLc5jkYdlKHetuviohoaSMGOrQJYnqpsx75Fy2lEvkJjcGBz1kL77EybsEv1L79p2jef6j5sDsH2ChaJ3Awh30p1TYdfo3i1GGAdykvItbMU9itZUiNs0m6rqS5K7cKZPXAgMBAAECgYEAjp/vef6P4ywvMcEnJkGNyN+B6W6HPdk77ov77AFuUdpWlS4PxL2ehtSlvLWcwRQQ6Ob1u0lM/0AX7M0f5vR1h5GXpvHe7JnxJ76iif6SvW7G3MceSOkTnIinYNf2S1xkRnIW67WNTVDVlJL9wldsI6TxaQrOMjtuGa8Y0nMqZJECQQD6VgCMGyIcWXJ9v1r8ploNyPm8mU9QcRsvVEe1uGcTqMHNslVNATR3aS5lxp56yBTn7BQpkLJ7KSqcm+n+k0OPAkEA9Uob4DHKLyukesQUIgwbcwegH6ddUCgHmaqxCFf3Tmpda7tLwRLoTEG7oxTfBvujdqXpSm3FoHzOmxeWYz9nOQJBANPi3V25TZrvPtAemnXEm+6VEITIwvBUe+0IihXOujhSm49uhXLDNVRpC6OLhPJpzgAruzkfR2KlinK6KUmX/hMCQAX8E+gJbvRtrSqtpAwcnYLV+csr6zPsdhsCtiUM+GS6ZaMeQ7/nNTG/HNPiy3pBI4DelW2SdhLvWJ8iGTI8tskCQDM5u00IIDIJfjaQ+JA9Nvcv4Y6TSJ9bOa+b/vLHLW/H77T4z7iz+vax8RzIoNKtEnIb+ZYfsaMLJd6NqTPzoLc=";
            
            //交易安全检验码，由数字和字母组成的32位字符串
            //如果签名方式设置为“MD5”时，请设置该参数
            Key = "";

            Req_id = DateTime.Now.ToString("yyyyMMddHHmmss");
            Out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmss");
            Subject = "百搭花朵珍珠胸花";
            Total_fee = "0.01";

            //返回URL
            var host = ConfigurationManager.AppSettings["website_url"];
            Call_back_url = host + "AlipayWapTrade2/Call_Back.aspx";
            Notify_url = host + "AlipayWapTrade2/Notify.aspx";
            Merchant_url = host + "AlipayWapTrade/PaySuccess.aspx";

            #endregion
        }

        #region 属性

        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner { get; set; }

        /// <summary>
        /// 获取或设交易安全校验码
        /// </summary>
        public static string Key { get; set; }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key { get; set; }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key { get; set; }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset { get; set; }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        public static string Subject { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>
        public static string Total_fee { get; set; }

        /// <summary>
        /// 外部交易号(由商户创建，请不要重复)
        /// </summary>
        public static string Out_trade_no { get; set; }

        /// <summary>
        /// 卖家账户名称
        /// </summary>
        public static string Seller_account_name { get; set; }

        /// <summary>
        /// 用户付款成功同步返回URL
        /// </summary>
        public static string Call_back_url { get; set; }

        /// <summary>
        /// 服务端接收通知URL
        /// </summary>
        public static string Notify_url { get; set; }

        /// <summary>
        /// 用户付款中途退出返回URL
        /// </summary>
        public static string Merchant_url { get; set; }

        /// <summary>
        /// 创建交易网关
        /// </summary>
        public static string Service_Create { get; set; }

        /// <summary>
        /// 执行授权网关
        /// </summary>
        public static string Service_Execute { get; set; }

        /// <summary>
        /// 请求参数格式
        /// </summary>
        public static string Format { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public static string V { get; set; }

        /// <summary>
        /// 请求ID 请随机生成
        /// </summary>
        public static string Req_id { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public static string Req_url { get; set; }

        #endregion
    }
}

/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/6 13:21:27
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    /// <summary>
    /// 微信支付API入口 
    /// </summary>
    public static class WeiXinPayGateway
    {
        /// <summary>
        /// 获得微信预订单请求JSON内容
        /// </summary>
        /// <param name="pOrderPackage"></param>
        /// <returns></returns>
        public static string GeneratePreOrderRequest(OrderPackage pOrderPackage)
        {
            PreOrderRequest por = new PreOrderRequest();
            por.Package = pOrderPackage;
            return por.GenerateRequestJSON();
        }

    }

    /*
     * appId wxf8b4f85f3a794e77
     * appSecret 4333d426b8d01a3fe64d53f36892df
     * paySignKey 2Wozy2aksie1puXUBpWD8oZxiD1DfQuEaiC7KcRATv1Ino3mdop
     * KaPGQQ7TtkNySuAmCaDCrw4xhPY5qKTBl7Fzm0RgR3c0WaVY
     * IXZARsxzHV2x7iwPPzOz94dnwPWSn
     * partnerId 1900000109
     * partnerKey 8934e7d15453e97507ef794cf7b0519d
     */

    public class PreOrderRequest
    {
        private Dictionary<string, string> _innerParams = new Dictionary<string, string>();
        public PreOrderRequest()
        {
            this._innerParams.Add("appId", "wxf8b4f85f3a794e77");
            this._innerParams.Add("appKey", "2Wozy2aksie1puXUBpWD8oZxiD1DfQuEaiC7KcRATv1Ino3mdopKaPGQQ7TtkNySuAmCaDCrw4xhPY5qKTBl7Fzm0RgR3c0WaVYIXZARsxzHV2x7iwPPzOz94dnwPWSn");
            this._innerParams.Add("timeStamp", (DateTime.Now - new DateTime(1970,1,1)).Seconds.ToString());
            //this._innerParams.Add("timeStamp", "189026618");
            this._innerParams.Add("nonceStr", Guid.NewGuid().ToString("N"));
            //this._innerParams.Add("nonceStr", "adssdasssd13d");
            //this._innerParams.Add("signType", "SHA1");
        }

        public OrderPackage Package { get; set; }

        public string PaySign
        {
            get { return this._innerParams["paySign"]; }
            set { this._innerParams["paySign"] = value; }
        }

        public void GeneratePaySign()
        {
            if (this._innerParams.ContainsKey("paySign"))
            {
                this._innerParams.Remove("paySign");
            }
            //
            var packageContent = this.Package.GeneratePackageContent();
            if (this._innerParams.ContainsKey("package"))
            {
                this._innerParams["package"] = packageContent;
            }
            else
            {
                this._innerParams.Add("package", packageContent);
            }
            var items = this._innerParams.OrderBy(item => item.Key).ToArray();
            //
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendFormat("&{0}={1}",item.Key.ToLower(),item.Value);
            }
            sb.Remove(0, 1);
            var sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "SHA1");
            sign = sign.ToLower();
            this._innerParams.Add("paySign", sign);
        }

        public string GenerateRequestJSON()
        {
            this.GeneratePaySign();
            InnerRequest req = new InnerRequest();
            req.appId = this._innerParams["appId"];
            req.timeStamp = this._innerParams["timeStamp"];
            req.nonceStr = this._innerParams["nonceStr"];
            req.package = this.Package.GeneratePackageContent();
            req.signType = "SHA1";
            req.paySign = this._innerParams["paySign"];

            return req.ToJSON();
        }

        class InnerRequest
        {
            public string appId { get; set; }
            public string timeStamp { get; set; }
            public string nonceStr { get; set; }
            public string package { get; set; }
            public string signType { get; set; }
            public string paySign { get; set; }
        }
    }


    public class OrderPackage
    {
        private Dictionary<string, string> _innerParams = new Dictionary<string, string>();
        public OrderPackage()
        {
            this._innerParams.Add("bank_type","WX");    //银行通道类型，由于这里是使用的微信公众号支付，因此这个字段固定为WX，注意大写。参数取值："WX"
            this._innerParams.Add("body", "XXX");   //商品描述。参数长度：128 字节以下。
            this._innerParams.Add("partner", "1900000109");       //商户号,即注册时分配的partnerId。
            this._innerParams.Add("fee_type", "1");       //现金支付币种,取值：1（人民币）,默认值是1，暂只支持1。
            this._innerParams.Add("input_charset", "UTF-8");       //传入参数字符编码。取值范围："GBK"、"UTF-8"。默认："GBK"
            this._innerParams.Add("notify_url", "http://www.qq.com");       //通知URL,在支付完成后,接收微信通知支付结果的URL,需给绝对路径,255 字符内, 格式如:http://wap.tenpay.com/tenpay.asp。取值范围：255 字节以内。
        }

        public string OrderNO
        {
            get { return this._innerParams["out_trade_no"]; }
            set { this._innerParams["out_trade_no"] = value; }
        }

        public string TotalAmount
        {
            get { return this._innerParams["total_fee"]; }
            set { this._innerParams["total_fee"] = value; }
        }

        public string ClientIP
        {
            get { return this._innerParams["spbill_create_ip"]; }
            set { this._innerParams["spbill_create_ip"] = value; }
        }

        public string GeneratePackageContent()
        {
            var items =this._innerParams.OrderBy(item => item.Key).ToArray();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendFormat("&{0}={1}",item.Key,item.Value);
                sb2.AppendFormat("&{0}={1}",item.Key,HttpUtility.UrlEncode(item.Value));
            }
            sb.Remove(0, 1);
            sb2.Remove(0, 1);
            sb.AppendFormat("&key={0}", "8934e7d15453e97507ef794cf7b0519d");
            var s1 = sb.ToString();
            var signTemp = JIT.Utility.MD5Helper.Encryption(s1);
            signTemp = signTemp.ToUpper();

            return sb2.AppendFormat("&sign={0}", signTemp).ToString(); ;
        }
    }
}

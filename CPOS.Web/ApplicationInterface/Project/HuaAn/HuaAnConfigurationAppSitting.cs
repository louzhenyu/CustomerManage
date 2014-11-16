namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    public class HuaAnConfigurationAppSitting
    {
        #region 华安交易代码
        /// <summary>
        /// 5.1.1	交换密钥（1000）（短期可暂不实现）
        /// </summary>
        public const int ExchKey = 1000;

        /// <summary>
        /// 5.1.2	货币基金世联支付（2001）
        /// </summary>
        public const int Pay = 2001;

        /// <summary>
        /// 5.1.3	货币基金购买（2101）
        /// </summary>
        public const int Buy = 2101;

        /// <summary>
        /// 5.1.4	货币基金赎回（2201）
        /// </summary>
        public const int Redemption = 2201;

        /// <summary>
        /// 5.1.5	货币基金支付单笔查询（3001）
        /// </summary>
        public const int PaySingleQuery = 3001;

        /// <summary>
        /// 5.1.6	客户资产及收益查询（5000）
        /// </summary>
        public const int CusAssetEarnings = 5000;

        /// <summary>
        /// 5.1.7	当日交易对账单下载（5001）
        /// </summary>
        public const int TradingStatements = 5001;

        /// <summary>
        /// 5.1.8	最新每万份收益、年化收益率（5002）
        /// </summary>
        public const int Earnings = 5002;

        #endregion

        /// <summary>
        /// 版本号 
        /// </summary>
        //public static string Version = "20140401";
        public static readonly string Version = System.Configuration.ConfigurationManager.AppSettings["Version"];

        /// <summary>
        /// 商户ID
        /// </summary>
        //public const string MerchantID = "10000008";
        public static readonly string MerchantID = System.Configuration.ConfigurationManager.AppSettings["MerchantID"];


        #region   正式环境
        /// <summary>
        /// AES密钥
        /// </summary>
        //public const string AesKey = "PgYh9jXj5fbkFBkNYYHN/A==";
        public static readonly string AesKey = System.Configuration.ConfigurationManager.AppSettings["AesKey"];

        /// <summary>
        /// MacCode检验串
        /// </summary>
        //public const string MacCodeKey = "NgtPaOIKtz83lCt7Wm3FkQ==";
        public static readonly string MacCodeKey = System.Configuration.ConfigurationManager.AppSettings["MacCodeKey"];

        /// <summary>
        /// 买号请求Url
        /// </summary>
        //public const string ReservationPurchaseUrl = "https://mobile.huaan.com.cn/worldunion/t/ReservationPurchase.action";
        public static readonly string ReservationPurchaseUrl = System.Configuration.ConfigurationManager.AppSettings["ReservationPurchaseUrl"];

        /// <summary>
        /// 基金赎回URL。
        /// </summary>
        //public const string ReservationRedeemUrl = "https://mobile.huaan.com.cn/worldunion/t/ReservationRedeem.action";
        public static readonly string ReservationRedeemUrl = System.Configuration.ConfigurationManager.AppSettings["ReservationRedeemUrl"];

        /// <summary>
        /// 支付Url。
        /// </summary>
        //public const string ReservationPayUrl = "https://mobile.huaan.com.cn/worldunion/t/ReservationPay.action";
        public static readonly string ReservationPayUrl = System.Configuration.ConfigurationManager.AppSettings["ReservationPayUrl"];

        /// <summary>
        /// 查询接口Url
        /// </summary>
        //public const string ReservationServletUrl = "https://mobile.huaan.com.cn/worldunion/ReservationServlet.servlet‍‍";
        public static readonly string ReservationServletUrl = System.Configuration.ConfigurationManager.AppSettings["ReservationServletUrl"];

        /// <summary>
        /// 世联回调Url 正式环境
        /// </summary>
        //public const string CallBackPageUrl = "http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnCallbackHandler.ashx?action={0}";
        public static readonly string CallBackPageUrl = System.Configuration.ConfigurationManager.AppSettings["CallBackPageUrl"];

        /// <summary>
        /// 世联回调Url 正式环境
        /// </summary>
        //public const string CallBackPageUrl = "http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnRetCallbackHandler.ashx?action={0}";
        public static readonly string RetCallBackPageUrl = System.Configuration.ConfigurationManager.AppSettings["RetCallBackPageUrl"];
        #endregion

        #region  测试环境
        ///// <summary>
        ///// AES密钥
        ///// </summary>
        //public const string AesKey = "5dOf9FHI1Y5hW2TNvVFY4w==";

        ///// <summary>
        ///// MacCode检验串
        ///// </summary>
        //public const string MacCodeKey = "123456";

        ///// <summary>
        ///// 买号请求Url
        ///// </summary>
        //public const string ReservationPurchaseUrl = "http://222.66.40.26/huaan-worldunion/t/ReservationPurchase.action";

        ///// <summary>
        ///// 基金赎回URL。
        ///// </summary>
        //public const string ReservationRedeemUrl = "http://222.66.40.26/huaan-worldunion/t/ReservationRedeem.action";

        //#region  测试环境
        ///// <summary>
        ///// AES密钥
        ///// </summary>
        //public const string AesKey = "5dOf9FHI1Y5hW2TNvVFY4w==";

        ///// <summary>
        ///// MacCode检验串
        ///// </summary>
        //public const string MacCodeKey = "123456";

        ///// <summary>
        ///// 买号请求Url
        ///// </summary>
        //public const string ReservationPurchaseUrl = "http://222.66.40.26/huaan-worldunion/t/ReservationPurchase.action";

        ///// <summary>
        ///// 基金赎回URL。
        ///// </summary>
        //public const string ReservationRedeemUrl = "http://222.66.40.26/huaan-worldunion/t/ReservationRedeem.action";

        ///// <summary>
        ///// 用号Url。
        ///// </summary>
        //public const string ReservationPayUrl = "http://222.66.40.26/huaan-worldunion/t/ReservationPay.action";

        ///// <summary>
        ///// 查询接口Url
        ///// </summary>
        //public const string ReservationServletUrl = "http://222.66.40.26/huaan-worldunion/ReservationServlet.servlet";

        //#region 世联回调Url
        ///// <summary>
        ///// 世联回调Url
        ///// </summary>
        //public static string CallBackPageUrl = "http://o2oapi.aladingyidong.com/ApplicationInterface/Project/HuaAn/HuaAnCallbackHandler.ashx?action={0}";
        //#endregion
        #endregion
    }
}
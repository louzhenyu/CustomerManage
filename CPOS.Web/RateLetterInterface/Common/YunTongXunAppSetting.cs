using System.Configuration;

namespace JIT.CPOS.Web.RateLetterInterface.Common
{
    /// <summary>
    /// 云通讯相关配置。
    /// </summary>
    public class YunTongXunAppSetting
    {
        private static string appID;
        /// <summary>
        /// 云通讯应用ID
        /// </summary>
        public static string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(appID))
                {
                    return ConfigurationManager.AppSettings["YTXAppID"];
                }
                return appID;
            }
        }

        private static string mainAccount;
        /// <summary>
        /// 云通讯主账号
        /// </summary>
        public static string MainAccount
        {
            get
            {
                if (string.IsNullOrEmpty(mainAccount))
                {
                    return ConfigurationManager.AppSettings["YTXMainAccount"];
                }
                return mainAccount;
            }
        }

        private static string mainToken;
        /// <summary>
        /// 主账号Token
        /// </summary>
        public static string MainToken
        {
            get
            {
                if (string.IsNullOrEmpty(mainToken))
                {
                    return ConfigurationManager.AppSettings["YTXMainToken"];
                }
                return mainToken;
            }
        }

        private static string restUrl;
        /// <summary>
        /// 云通讯服务器地址
        /// </summary>
        public static string RestAddress
        {
            get
            {
                if (string.IsNullOrEmpty(restUrl))
                {
                    return ConfigurationManager.AppSettings["YTXRestUrl"];
                }
                return restUrl;
            }

        }

        private static string restPort;
        /// <summary>
        /// 端口号
        /// </summary>
        public static string RestPort
        {
            get
            {
                if (string.IsNullOrEmpty(restUrl))
                {
                    return ConfigurationManager.AppSettings["YTXRestPort"];
                }
                return restUrl;
            }

        }
    }
}
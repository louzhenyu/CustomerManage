using System.Configuration;
using System.Linq;

namespace CPOS.Common
{
    /// <summary>
    /// web.config 配置信息操作
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// 获取AppSetting节点
        /// </summary>
        /// <param name="strkey">App字符串</param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string GetAppSetting(string strkey, string defaultVal = "")
        {
            if (ConfigurationManager.AppSettings.Cast<string>().Any(key => strkey == key))
            {
                return ConfigurationManager.AppSettings[strkey];
            }
            return defaultVal;
        }


        /// <summary>
        /// 获取ConnectionString节点
        /// </summary>
        /// <param name="strName">Connection 字符串</param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string GetConnectionString(string strName, string defaultVal = "")
        {
            if (ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().Any(key => strName == key.Name))
            {
                return ConfigurationManager.ConnectionStrings[strName].ToString();
            }
            return defaultVal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace JIT.CPOS.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        //public static string JsonSerializer<T>(T t)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    MemoryStream ms = new MemoryStream();
        //    ser.WriteObject(ms, t);
        //    string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        //    ms.Close();
        //    return jsonString;
        //}

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        /// <summary>
        /// 序列化  针对时间做了处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, t);
                    string strJson = Encoding.UTF8.GetString(ms.ToArray());
                    //替换Json的date字符串
                    string p = @"\\/Date\((\d+)\+\d+\)\\/";
                    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDataToDateString);

                    Regex reg = new Regex(p);
                    strJson = reg.Replace(strJson, matchEvaluator);
                    return strJson;
                }

            }
            catch (IOException)
            {
                
                return null;
            }
        }
        /// <summary>
        /// Converts the json data to date string.
        /// </summary>
        ///<param name="m">
        /// <returns>System.String.</returns>
        /// <remarks></remarks>
        private static string ConvertJsonDataToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();//转换为本地时间
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;

        }
        /// <summary>
        /// Converts the date string to json date.
        /// </summary>
        ///<param name="m">
        /// <returns></returns>
        /// <remarks></remarks>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;

            DateTime dt = DateTime.Parse(m.Groups[0].Value);

            dt = dt.ToUniversalTime();

            TimeSpan ts = dt - DateTime.Parse("1970-01-01");

            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);

            return result;

        }
    }
}
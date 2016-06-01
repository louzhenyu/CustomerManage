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

        public static string JsonSerializerForRedis<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
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


        public static string Dictionary2Json<TKey, TValue>(Dictionary<TKey, TValue> d)
        {
            string jsonString = "{";
            bool blFirst = true;
            foreach (var item in d)
            {
                if (!blFirst)
                {
                    jsonString += ",";
                }
                jsonString += "\"" + item.Key + "\":\"" + String2Json(item.Value.ToString()) + "\"";
                blFirst = false;
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }

        //
        /// <summary> 
        /// 过滤特殊字符 
        /// </summary> 
        /// <param name="s"></param> 
        /// <returns></returns> 
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary> 
        /// 格式化字符型、日期型、布尔型 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="type"></param> 
        /// <returns></returns> 
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            return str;
        }
    }
}
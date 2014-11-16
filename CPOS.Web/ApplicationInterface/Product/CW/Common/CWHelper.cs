using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace JIT.CPOS.Web.ApplicationInterface.Product.CW
{
    public class CWHelper
    {
        /// <summary>
        /// 序列化Json。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            string data = json.Serialize(obj);
            return data;
        }

        /// <summary>
        /// 反序列化。 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string input)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            T obj = json.Deserialize<T>(input);

            return obj;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CPOS.Common
{
    public static class HttpHelper
    {
        #region 以POST 形式请求数据
        /// <summary>
        /// 以POST 形式请求数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="reqType">默认的请求类型</param>
        /// <returns></returns>
        public static string PostData(string requestPara, string url,
            string reqType = "application/x-www-form-urlencoded")
        {
            return PostData(requestPara, url, 60, reqType);
        } 
        #endregion

        #region 以POST 形式请求数据
        /// <summary>
        /// 以POST 形式请求数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="reqType">默认的请求类型</param>
        /// <returns></returns>
        public static string PostData(string requestPara, string url, int timeout,
            string reqType = "application/x-www-form-urlencoded")
        {
            string responseString = string.Empty;
            try
            {
                WebRequest hr = WebRequest.Create(url);

                byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(requestPara);
                hr.ContentType = reqType;
                hr.ContentLength = buf.Length;
                hr.Timeout = timeout * 1000;
                hr.Method = "POST";

                using (Stream requestStream = hr.GetRequestStream())
                {
                    requestStream.Write(buf, 0, buf.Length);
                }

                using (WebResponse response = hr.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                        {
                            responseString = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseString = ex.ToString();
            }
            return responseString;
        } 
        #endregion
        
        #region 以GET 形式获取数据 默认utf-8
        /// <summary>
        /// 以GET 形式获取数据 默认utf-8
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(string requestPara, string url)
        {
            return GetData(requestPara, url, 60, Encoding.GetEncoding("utf-8"));
        } 
        #endregion

        #region 以GET 形式获取数据
        /// <summary>
        /// 以GET 形式获取数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static string GetData(string requestPara, string url, Encoding code)
        {
            return GetData(requestPara, url, 60, code);
        } 
        #endregion

        #region 以GET 形式获取数据
        /// <summary>
        /// 以GET 形式获取数据
        /// </summary>
        /// <param name="requestPara"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static string GetData(string requestPara, string url, int timeout, Encoding code, Hashtable ht = null)
        {
            string responseString = string.Empty;
            requestPara = requestPara.IndexOf('?') > -1 ? (requestPara) : ("?" + requestPara);
            try
            {
                WebRequest hr = WebRequest.Create(url + requestPara);
                hr.Method = "GET";
                hr.Timeout = timeout * 1000;
                if (ht != null)
                {
                    foreach (DictionaryEntry de in ht)
                    {
                        hr.Headers.Add(de.Key.ToString(), de.Value.ToString());
                    }
                }

                using (WebResponse response = hr.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream, code))
                        {
                            responseString = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseString = ex.ToString();
            }
            return responseString;
        } 
        #endregion

        #region Http Soap 请求 默认超时时间1分钟\
        /// <summary>
        /// Http Soap 请求 默认超时时间1分钟
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="url">请求地址</param>
        /// <param name="reqType">请求参数类型</param>
        /// <param name="resType">返回参数类型</param>
        /// <returns></returns>
        public static string SendSoapRequest(string request, string url,
            string reqType = "application/x-www-form-urlencoded",
            string resType = "application/json")
        {
            return PostSoapRequest(request, url, 60, new Hashtable(), reqType, resType);
        } 
        #endregion
        
        #region Http Soap post 请求
        /// <summary>
        /// Http Soap post 请求
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="url">请求地址</param>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="reqType">请求参数类型</param>
        /// <param name="resType">返回参数类型</param>
        /// <returns></returns>
        public static string PostSoapRequest(string request, string url, int timeout, Hashtable ht, string reqType = "application/json", string resType = "application/json")//application/x-www-form-urlencoded
        {
            string responseString = string.Empty;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = reqType;// + ";charset=\"utf-8\""
            webRequest.Accept = resType;
            webRequest.Method = "POST";
            webRequest.Timeout = timeout * 1000;
            foreach (DictionaryEntry de in ht)
            {
                webRequest.Headers.Add(de.Key.ToString(), de.Value.ToString());
            }
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(request);
            webRequest.ContentLength = bytes.Length;
            using (Stream oStreamOut = webRequest.GetRequestStream())
            {
                oStreamOut.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse myWebResponse = webRequest.GetResponse())
            {
                using (Stream streamResponse = myWebResponse.GetResponseStream())
                {
                    if (streamResponse != null)
                    {
                        using (var streamRead = new StreamReader(streamResponse, Encoding.GetEncoding("utf-8")))
                        {
                            responseString = streamRead.ReadToEnd();
                        }
                    }
                }
            }
            return responseString;
        } 
        #endregion

        #region Http Soap get请求
        /// <summary>
        /// Http Soap get请求
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="url">请求地址</param>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="reqType">请求参数类型</param>
        /// <param name="resType">返回参数类型</param>
        /// <returns></returns>
        public static string GetSoapRequest(string request, string url, int timeout, Hashtable ht, string reqType = "application/json", string resType = "application/json")//application/x-www-form-urlencoded
        {
            string responseString = string.Empty;
            if (!string.IsNullOrEmpty(request))
            {
                url = string.Format("{0}?{1}", url, request);
            }
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = reqType;// + ";charset=\"utf-8\""
            webRequest.Accept = resType;
            webRequest.Method = "GET";
            webRequest.Timeout = timeout * 1000;
            foreach (DictionaryEntry de in ht)
            {
                webRequest.Headers.Add(de.Key.ToString(), de.Value.ToString());
            }
            // byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(request);

            using (WebResponse response = webRequest.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
            }
            return responseString;
        }
        #endregion
    }


}
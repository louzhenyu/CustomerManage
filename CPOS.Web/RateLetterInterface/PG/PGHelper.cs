using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.RateLetterInterface.PG
{
    public class PGHelper
    {
        public static readonly string PG_SERVER_URL = System.Configuration.ConfigurationManager.AppSettings["PGServerUrl"].ToString() + "/app/";
        //public static readonly string PG_SERVER_URL = "http://120.132.145.149:9080/app/common!";
        //public static readonly string PG_SERVER_IREPORT_URL = "http://120.132.145.149:9080/app/";
        public static readonly string PG_COOKIE_KEY = "JSESSIONID";

        public static string HttpRequestSsoVerifyPost(string requestMethod, string json, string pToken)
        {
            //json格式请求数据
            string requestData = json;
            if (!string.IsNullOrEmpty(json))
                json = "?" + json;

            //拼接URL
            string serviceUrl = string.Format("{0}{1}", PG_SERVER_URL, requestMethod + json);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //POST请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.Accept = "application/json";
            myRequest.ContentType = "application/json";

            //设置cookie
            CookieContainer objCookie = new CookieContainer();
            objCookie.Add(new Uri(serviceUrl), new Cookie(PG_COOKIE_KEY, pToken));
            myRequest.CookieContainer = objCookie;

            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }

        public static string HttpRequestIReportPost(string requestMethod, string json, string pToken)
        {
            //json格式请求数据
            string requestData = json;
            if (!string.IsNullOrEmpty(json))
                json = "?" + json;

            //拼接URL
            string serviceUrl = string.Format("{0}{1}", PG_SERVER_URL, requestMethod + json);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //POST请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.Accept = "application/json";
            myRequest.ContentType = "application/json";

            //设置cookie
            CookieContainer objCookie = new CookieContainer();
            objCookie.Add(new Uri(serviceUrl), new Cookie(PG_COOKIE_KEY, pToken));
            myRequest.CookieContainer = objCookie;

            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }

        //public static string SendHttpRequestGet(string requestMethod, string json, string pToken)
        //{
        //    //json格式请求数据
        //    string requestData = json;
        //    if (!string.IsNullOrEmpty(json))
        //        json = "?" + json;

        //    //拼接URL
        //    string serviceUrl = string.Format("{0}{1}", PG_SERVER_URL, requestMethod + json);
        //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
        //    //utf-8编码
        //    byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

        //    //POST请求 
        //    myRequest.Method = "POST";
        //    myRequest.ContentLength = buf.Length;
        //    //指定为json否则会出错
        //    myRequest.Accept = "application/json";
        //    myRequest.ContentType = "application/json";

        //    //Content-type: application/json; charset=utf-8

        //    //myRequest.ContentType = "text/json";
        //    myRequest.MaximumAutomaticRedirections = 1;
        //    myRequest.AllowAutoRedirect = true;

        //    Stream newStream = myRequest.GetRequestStream();
        //    newStream.Write(buf, 0, buf.Length);
        //    newStream.Close();

        //    //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
        //    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        //    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
        //    string ReqResult = reader.ReadToEnd();
        //    reader.Close();
        //    myResponse.Close();
        //    return ReqResult;
        //}

        #region  token验证
        /// <summary>
        /// token验证
        /// </summary>
        /// <returns></returns>
        public SsoVerify SsoVerify(string pToken)
        {
            string data = HttpRequestSsoVerifyPost("common!ssoVerify.action", string.Empty, pToken);
            return CWHelper.Deserialize<SsoVerify>(data);
        }
        #endregion

        #region 修改宝洁用户信息

        public PromptMsg ChangeUserMessage(string pKeyName, string pKeyValue, string pToken)
        {
            try
            {
                string str = "keyName=" + pKeyName + "&keyValue=" + pKeyValue;
                string data = HttpRequestSsoVerifyPost("common!updateProfile.action", str, pToken);
                return CWHelper.Deserialize<PromptMsg>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 请求宝洁报表
        /// <summary>
        /// 请求宝洁报表
        /// </summary>
        /// <param name="pDynamicParam">动态url参数</param>
        /// <param name="pAction">action</param>
        /// <param name="pToken">token</param>
        /// <returns></returns>
        public string RequestReport(string pToken, string pAction, string pDynamicParam)
        {
            try
            {
                string data = HttpRequestIReportPost(pAction, pDynamicParam, pToken);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

    #region SsoVerify 验证返回对象
    public class SsoVerify
    {
        public SsoVerifyError error { set; get; }
        public SsoVerifyObject Object { set; get; }
        public SsoVerifyProperty property { set; get; }
    }
    public class SsoVerifyError
    {
        public string err_code { set; get; }
        public string err_msg { set; get; }
        public string request_args { set; get; }
    }
    public class SsoVerifyObject
    {
        public string email { set; get; }
        public string first_name { set; get; }
        public string name { set; get; }
        public string head_photo_path { set; get; }
    }
    public class SsoVerifyProperty
    {
        public string is_list { set; get; }
        public string obj_name { set; get; }
    }
    #endregion


    #region 修改用户信息
    public class ChangeMessage
    {
        public string KeyName { set; get; }
        public string KeyValue { set; get; }
    }

    public class PromptMsg
    {
        public PromptError error { set; get; }
    }

    public class PromptError
    {
        public string err_code { set; get; }
        public string err_msg { set; get; }
        public string request_args { set; get; }
    }
    #endregion
}
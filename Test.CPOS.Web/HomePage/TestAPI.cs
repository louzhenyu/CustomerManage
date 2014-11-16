using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;
using System.IO;

namespace Test.CPOS.Web.HomePage
{
    [TestFixture]
    public class TestAPI
    {
        string url = "";
        [Test]
        public void GetLevel1ItemCategory()
        {
            string json = string.Format("action={0}", "GetLevel1ItemCategory");
            var res = SendHttpRequest(url, json);
        }


        public static string SendHttpRequest(string requestURI, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = requestURI;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            //myRequest.Accept = "application/json";
            myRequest.ContentType = "application/x-www-form-urlencoded";

            //Content-type: application/json; charset=utf-8

            //myRequest.ContentType = "text/json";
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
    }


}

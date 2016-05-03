using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;
using System.Configuration;
using JIT.CPOS.Common;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class GetLingKinSignAH : BaseActionHandler<LingkinRP, LingkinRD>
    {

        protected override LingkinRD ProcessRequest(APIRequest<LingkinRP> pRequest)
        {
            var rd = new LingkinRD();
            var para = pRequest.Parameters;


            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            string strPageKey = string.Empty;
            var pageBll = new SysPageBLL(loggingSessionInfo);

            var wapentity = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
            {
                CustomerId = loggingSessionInfo.ClientID,
                IsDelete = 0
            }, null).FirstOrDefault();//取默认的第一个微信

            if (wapentity == null)
            {
                throw new APIException("微信公众号未授权");
            }

            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            var Domain1 = ConfigurationManager.AppSettings["interfacehost1"].Replace("http://", "");

            string strEventType = string.Empty;
            string strParamJson = "";
            switch (para.DrawMethodCode)
            {
                case "TG":
                    strEventType = "1";
                    strParamJson = "[{\"key\":\"CTWEventId\",\"value\":\"" + para.CTWEventId + "\"},{\"key\":\"eventTypeId\",\"value\":\"" + strEventType + "\"}]";
                    break;
                case "QG":
                    strEventType = "2";
                    strParamJson = "[{\"key\":\"CTWEventId\",\"value\":\"" + para.CTWEventId + "\"},{\"key\":\"eventTypeId\",\"value\":\"" + strEventType + "\"}]";
                    break;
                case "RX":
                    strEventType = "3";
                    strParamJson = "[{\"key\":\"CTWEventId\",\"value\":\"" + para.CTWEventId + "\"},{\"key\":\"eventTypeId\",\"value\":\"" + strEventType + "\"}]";
                    break;
                default:
                    strParamJson = "[{\"key\":\"eventId\",\"value\":\"" + para.CTWEventId + "\"}]";
                    break;
            }
            switch (para.DrawMethodCode)
            {
                case "HB":
                    strPageKey = "RedPacket";
                    break;
                case "DZP":
                    strPageKey = "BigDial";
                    break;
                case "QN":
                    strPageKey = "Questionnaire";
                    break;
                default:
                    strPageKey = "NewsActivityList";
                    break;
            }

            string URL = string.Empty;

            var page = pageBll.GetPageByCustomerIdAndPageKey(loggingSessionInfo.ClientID, strPageKey).SingleOrDefault();
            if (page == null)
                throw new APIException("未找到Page信息") { ErrorCode = 341 };
            string path = string.Empty;//要替换的路径
            string urlTemplate = page.URLTemplate;//模板URL
            string json = page.JsonValue;// JSON体
            var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//转换后的字典
            var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//htmls是一个数组，里面有他的很多属性
            Dictionary<string, object> html = null;//选择的html信息
            var defaultHtmlId = jsonDic["defaultHtml"].ToString();
            html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);//默认的htmlid*****
            if (html != null)
                path = html["path"].ToString();
            //判断高级oauth认证
            var scope = "snsapi_base";
            //if (jsonDic.ContainsKey("scope"))//必须要判断key里是否包含scope
            //{
            //    scope = (jsonDic["scope"] == null || jsonDic["scope"].ToString() == "") ? "snsapi_base" : "snsapi_userinfo";
            //}

            //判断是否有定制,没有则取JSON体中的默认
            //找出订制内容
            if (page != null)
            {
                //看是否有htmls的定制(Node值=2)
                if (page.Node == "2")
                {
                    var nodeValue = page.NodeValue;
                    //在Json解析后的集合中找到path
                    html = htmls.Find(t => t["id"].ToString() == nodeValue);
                    if (html != null)
                    {
                        path = html["path"].ToString();
                    }
                }

            }
            //替换URL模板
           
            urlTemplate = urlTemplate.Replace("_pageName_", path);//用path替换掉_pageName_***（可以查看红包或者客服的path信息即可以知道）
            var paraDic = strParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
            foreach (var item in paraDic)   //这里key和value对于活动来说，其实就是活动的eventId，和eventId的值
            {
                if (item.ContainsKey("key") && item.ContainsKey("value"))
                    urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
            }
           
            string strUrl = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&applicationId={2}&goUrl={3}&scope={4}", Domain.Trim('/'), loggingSessionInfo.ClientID, wapentity.ApplicationId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)), scope);
            UrlInfo urlinfo = new UrlInfo();
            urlinfo.Name = "1";
            urlinfo.Url = strUrl;

            rd.UrlInfo = urlinfo;

            string strTokenUrl = ConfigurationManager.AppSettings["TokenUrl"];
            string strAppId = ConfigurationManager.AppSettings["appid"];
            string strSecret = ConfigurationManager.AppSettings["secret"];
            string strTicketUrl = ConfigurationManager.AppSettings["TicketUrl"];

            string strTokenAddress = strTokenUrl + "?grant_type=client_credential&appid=" + strAppId + "&secret=" + strSecret + "";
            var Token = JsonHelper.JsonDeserialize<TokenInfo>(HttpGetData(strTokenAddress));
            if (!string.IsNullOrEmpty(Token.errMsg))
            {
                rd.errCode = Token.errCode;
                rd.errMsg = Token.errMsg;
                return rd;
            }
            string strTicketAddress = strTicketUrl + "?type=jsapi&access_token=" + Token.access_token + "";
            var Ticket = JsonHelper.JsonDeserialize<TicketInfo>(HttpGetData(strTicketAddress));
            if (Ticket.errcode != 0)
            {
                rd.errCode = Ticket.errcode;
                rd.errMsg = Ticket.errmsg;
                return rd;
            }
            String nonceStr = StringUtil.GetRandomStr(6);
            String timestamp = ConvertDateTimeInt(DateTime.Now).ToString();


            String signatureStr = String.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", Ticket.ticket, nonceStr, timestamp, para.Url);

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(signatureStr);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out).Replace("-", "").ToLower();

            rd.appid = strAppId;
            rd.noncestr = nonceStr;
            rd.secret = strSecret;
            rd.timestamp = timestamp;
            rd.sign = str_sha1_out;


            return rd;
        }
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public class TokenInfo
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public int errCode { get; set; }
            public string errMsg { get; set; }
        }
        public class TicketInfo
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strAddress"></param>
        /// <returns></returns>
        public string HttpGetData(string strAddress)
        {

            string serviceAddress = strAddress;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
    }


}
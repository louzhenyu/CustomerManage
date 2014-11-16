/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/1 11:46:47
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Net;
using System.Web;
using System.IO;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class UrlBLL
    {
        private static readonly string sinaAppKey = ConfigurationManager.AppSettings["sinaWeiboKey"];// "504136043";// 测试的，上线时需要公司申请
        private static readonly string sinaUrl = "http://api.t.sina.com.cn/short_url/shorten.json";
        //private static readonly string baiduUrl = "http://dwz.cn/create.php";
        /// <summary>
        /// 根据长链接 获取 实例
        /// </summary>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        public UrlEntity GetByLongUrl(string longUrl,string clientId)
        {
            if (string.IsNullOrEmpty(longUrl)) return null;
            var  e =  this._currentDAO.GetByLongUrl(longUrl);
            if (null != e) return e;
            longUrl = HttpUtility.UrlEncode(longUrl);
            var url = string.Format(sinaUrl + "?source={0}&url_long={1}", sinaAppKey, longUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader sr = new StreamReader(streamReceive, encoding);
            string strResult = sr.ReadToEnd();
            var urls = strResult.DeserializeJSONTo<JsonSinaUrl[]>();
            if (urls.Length == 0) return null;
            var entity = MapJsonSinaUrl2Entity(urls[0], clientId);
            this._currentDAO.Create(entity);
            return entity;
        }
        public UrlEntity[] GetEntitysByLongUrl(string[] longUrls,string clientId)
        {
            if (longUrls.Length == 0) return null;
            var entities = new List<UrlEntity>();
            var existedEntities = this._currentDAO.GetUrlsByLongUrl(longUrls);
            var newUrls = longUrls.Where( s => existedEntities.Where(e=>e.LongUrl==s).Count() ==0);
            if (newUrls.Count() == 0) return existedEntities;
            var urlPara = new List<string>();
            foreach (var e in newUrls)
            {
                urlPara.Add("url_long=" + HttpUtility.UrlEncode(e));
            }
            var url = string.Format(sinaUrl + "?source={0}&{1}", sinaAppKey, urlPara.ToArray().ToJoinString("&"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader sr = new StreamReader(streamReceive, encoding);
            string strResult = sr.ReadToEnd();
            var urls = strResult.DeserializeJSONTo<JsonSinaUrl[]>();
            var urlEntities = MapJsonSinaUrl2Entities(urls, clientId);
            foreach (var m in urlEntities)
            {
                this._currentDAO.Create(m);
            }
            sr.Close();
            response.Close();
            entities.AddRange(existedEntities);
            entities.AddRange(urlEntities);
            return entities.ToArray();
        }
        private UrlEntity[] MapJsonSinaUrl2Entities(JsonSinaUrl[] u, string clientId)
        {
            var lt = new List<UrlEntity>();
            foreach(var e in u)
            {
                lt.Add(MapJsonSinaUrl2Entity(e,clientId));
            }
            return lt.ToArray();
        }
        private UrlEntity MapJsonSinaUrl2Entity(JsonSinaUrl u,string clientId)
        {
            return new UrlEntity
            {
                UrlId = Guid.NewGuid().ToString("N"),
                LongUrl = u.url_long,
                ShortUrl = u.url_short,
                ClientId = clientId
            };
        }
        public class JsonSinaUrl
        {
            public string url_short { get; set; }
            public string url_long { get; set; }
            public int type { get; set; }
            public bool result { get; set; }
        }
    }
}
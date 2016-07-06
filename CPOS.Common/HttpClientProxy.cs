using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace CPOS.Common
{
    /// <summary>
    /// HTTP请求帮助方法
    /// </summary>
    public class HttpClientProxy
    {
        #region 预定义方法或者变更
        #region 字段
        //默认的编码
        private Encoding _encoding = Encoding.Default;
        //Post数据编码
        private Encoding _postencoding = Encoding.Default;
        //HttpWebRequest对象用来发起请求
        private HttpWebRequest _request = null;
        //获取影响流的数据对象
        private HttpWebResponse _response = null;
        #endregion

        #region 根据相传入的数据，得到相应页面数据
        /// <summary>
        /// 根据相传入的数据，得到相应页面数据
        /// </summary>
        /// <param name="item">参数类对象</param>
        /// <returns>返回HttpResult类型</returns>
        public HttpResult HttpRequest(HttpItem item)
        {
            //返回参数
            HttpResult result = new HttpResult();
            try
            {
                //准备参数
                SetRequest(item);
            }
            catch (Exception ex)
            {
                return new HttpResult() { Cookie = string.Empty, Header = null, Html = ex.Message, StatusDescription = "配置参数时出错：" + ex.Message };
            }
            try
            {
                #region 得到请求的response
                using (_response = (HttpWebResponse)_request.GetResponse())
                {
                    result.StatusCode = _response.StatusCode;
                    result.StatusDescription = _response.StatusDescription;
                    result.Header = _response.Headers;
                    if (_response.Cookies != null) result.CookieCollection = _response.Cookies;
                    if (_response.Headers["set-cookie"] != null) result.Cookie = _response.Headers["set-cookie"];
                    byte[] ResponseByte = null;
                    using (MemoryStream _stream = new MemoryStream())
                    {
                        //GZIIP处理
                        if (_response.ContentEncoding != null && _response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //开始读取流并设置编码方式
                            new GZipStream(_response.GetResponseStream(), CompressionMode.Decompress).CopyTo(_stream, 10240);
                        }
                        else
                        {
                            //开始读取流并设置编码方式
                            _response.GetResponseStream().CopyTo(_stream, 10240);
                        }
                        //获取Byte
                        ResponseByte = _stream.ToArray();
                    }
                    if (ResponseByte != null & ResponseByte.Length > 0)
                    {
                        //是否返回Byte类型数据
                        if (item.ResultType == ResultType.Byte)
                        {
                            result.ResultByte = ResponseByte;
                        }
                        //从这里开始我们要无视编码了
                        if (_encoding == null)
                        {
                            Match meta = Regex.Match(Encoding.Default.GetString(ResponseByte), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
                            string c = (meta.Groups.Count > 1) ? meta.Groups[2].Value.ToLower().Trim() : string.Empty;
                            if (c.Length > 2)
                            {
                                try
                                {
                                    if (c.IndexOf(" ") > 0) c = c.Substring(0, c.IndexOf(" "));
                                    _encoding = Encoding.GetEncoding(c.Replace("\"", "").Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim());
                                }
                                catch
                                {
                                    if (string.IsNullOrEmpty(_response.CharacterSet)) _encoding = Encoding.UTF8;
                                    else _encoding = Encoding.GetEncoding(_response.CharacterSet);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(_response.CharacterSet)) _encoding = Encoding.UTF8;
                                else _encoding = Encoding.GetEncoding(_response.CharacterSet);
                            }
                        }
                        //得到返回的HTML
                        result.Html = _encoding.GetString(ResponseByte);
                    }
                    else
                    {
                        //得到返回的HTML
                        result.Html = "本次请求并未返回任何数据";
                    }
                }
                #endregion
            }
            catch (WebException ex)
            {
                //这里是在发生异常时返回的错误信息
                _response = (HttpWebResponse)ex.Response;
                result.Html = ex.Message;
                if (_response != null)
                {
                    result.StatusCode = _response.StatusCode;
                    result.StatusDescription = _response.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                result.Html = ex.Message;
            }
            if (item.IsToLower) result.Html = result.Html.ToLower();
            return result;
        }
        #endregion

        #region 为请求准备参数
        /// <summary>
        /// 为请求准备参数
        /// </summary>
        ///<param name="item">参数列表</param>
        private void SetRequest(HttpItem item)
        {
            // 验证证书
            SetCer(item);
            //设置Header参数
            if (item.Header != null && item.Header.Count > 0) foreach (string key in item.Header.AllKeys)
                {
                    _request.Headers.Add(key, item.Header[key]);
                }
            // 设置代理
            SetProxy(item);
            if (item.ProtocolVersion != null) _request.ProtocolVersion = item.ProtocolVersion;
            _request.ServicePoint.Expect100Continue = item.Expect100Continue;
            //请求方式Get或者Post
            _request.Method = item.Method;
            _request.Timeout = item.Timeout;
            _request.KeepAlive = item.KeepAlive;
            _request.ReadWriteTimeout = item.ReadWriteTimeout;
            if (!string.IsNullOrWhiteSpace(item.Host))
            {
                _request.Host = item.Host;
            }
            //Accept
            _request.Accept = item.Accept;
            //ContentType返回类型
            _request.ContentType = item.ContentType;
            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            _request.UserAgent = item.UserAgent;
            // 编码
            _encoding = item.Encoding;
            //设置Cookie
            SetCookie(item);
            //来源地址
            _request.Referer = item.Referer;
            //是否执行跳转功能
            _request.AllowAutoRedirect = item.Allowautoredirect;
            //设置Post数据
            SetPostData(item);
            //设置最大连接
            if (item.Connectionlimit > 0) _request.ServicePoint.ConnectionLimit = item.Connectionlimit;
        }
        #endregion

        #region 设置证书
        /// <summary>
        /// 设置证书
        /// </summary>
        /// <param name="item"></param>
        private void SetCer(HttpItem item)
        {
            if (!string.IsNullOrWhiteSpace(item.CerPath))
            {
                //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                //初始化对像，并设置请求的URL地址
                _request = (HttpWebRequest)WebRequest.Create(item.Url);
                SetCerList(item);
                //将证书添加到请求里
                _request.ClientCertificates.Add(new X509Certificate(item.CerPath));
            }
            else
            {
                //初始化对像，并设置请求的URL地址
                _request = (HttpWebRequest)WebRequest.Create(item.Url);
                SetCerList(item);
            }
        }
        #endregion

        #region 设置多个证书
        /// <summary>
        /// 设置多个证书
        /// </summary>
        /// <param name="item"></param>
        private void SetCerList(HttpItem item)
        {
            if (item.ClentCertificates != null && item.ClentCertificates.Count > 0)
            {
                foreach (X509Certificate c in item.ClentCertificates)
                {
                    _request.ClientCertificates.Add(c);
                }
            }
        }

        #endregion

        #region 设置Cookie
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="item">Http参数</param>
        private void SetCookie(HttpItem item)
        {
            //Cookie
            if (!string.IsNullOrEmpty(item.Cookie))
            {
                _request.Headers[HttpRequestHeader.Cookie] = item.Cookie;
            }
            if (item.ResultCookieType == ResultCookieType.CookieCollection)
            {
                _request.CookieContainer = new CookieContainer();
            }
            //设置CookieCollection
            if (item.CookieCollection != null && item.CookieCollection.Count > 0)
            {
                _request.CookieContainer = new CookieContainer();
                _request.CookieContainer.Add(item.CookieCollection);
            }
        }

        #endregion

        #region 设置Post数据
        /// <summary>
        /// 设置Post数据
        /// </summary>
        /// <param name="item">Http参数</param>
        private void SetPostData(HttpItem item)
        {
            //验证在得到结果时是否有传入数据
            if (_request.Method.Trim().ToLower().Contains("post"))
            {
                if (item.PostEncoding != null)
                {
                    _postencoding = item.PostEncoding;
                }
                byte[] buffer = null;
                //写入Byte类型
                if (item.PostDataType == PostDataType.Byte && item.PostDataByte != null && item.PostDataByte.Length > 0)
                {
                    //验证在得到结果时是否有传入数据
                    buffer = item.PostDataByte;
                }//写入文件
                else if (item.PostDataType == PostDataType.FilePath && !string.IsNullOrWhiteSpace(item.PostDataString))
                {
                    StreamReader r = new StreamReader(item.PostDataString, _postencoding);
                    buffer = _postencoding.GetBytes(r.ReadToEnd());
                    r.Close();
                } //写入字符串
                else if (!string.IsNullOrWhiteSpace(item.PostDataString))
                {
                    buffer = _postencoding.GetBytes(item.PostDataString);
                }
                if (buffer != null)
                {
                    _request.ContentLength = buffer.Length;
                    _request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
            }
        }
        #endregion

        #region 设置代理
        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="item">参数对象</param>
        private void SetProxy(HttpItem item)
        {
            if (!string.IsNullOrWhiteSpace(item.ProxyIp))
            {
                //设置代理服务器
                if (item.ProxyIp.Contains(":"))
                {
                    string[] plist = item.ProxyIp.Split(':');
                    WebProxy myProxy = new WebProxy(plist[0].Trim(), Convert.ToInt32(plist[1].Trim()));
                    //建议连接
                    myProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    //给当前请求对象
                    _request.Proxy = myProxy;
                }
                else
                {
                    WebProxy myProxy = new WebProxy(item.ProxyIp, false);
                    //建议连接
                    myProxy.Credentials = new NetworkCredential(item.ProxyUserName, item.ProxyPwd);
                    //给当前请求对象
                    _request.Proxy = myProxy;
                }
                _request.Credentials = CredentialCache.DefaultCredentials;
            }
            else if (item.WebProxy != null)
            {
                _request.Proxy = item.WebProxy;
            }
        }
        #endregion

        #region 回调验证证书问题
        /// <summary>
        /// 回调验证证书问题
        /// </summary>
        /// <param name="sender">流对象</param>
        /// <param name="certificate">证书</param>
        /// <param name="chain">X509Chain</param>
        /// <param name="errors">SslPolicyErrors</param>
        /// <returns>bool</returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; }

        #endregion
        #endregion

        #region POST请求，参数以XML形式传递
        public static string GetRequest(string requestXml, string m_QuestURL)
        {
            string error = string.Empty;
            return GetRequest(m_QuestURL, requestXml, "UTF-8", 10000, out error);
        }
        /// <summary>
        /// 发送HTTP请求（METHOD：POST）
        /// </summary>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="requestData">请求参数</param>
        /// <param name="codingType">编码方式</param>
        /// <param name="timeout">请求超时时间，单位毫秒</param>
        /// <param name="error">当请求出现异常时，记录异常消息</param>
        /// <returns>以字符串格式返回响应结果</returns>
        public static string GetRequest(string requestUrl, string requestData, string codingType, int timeout, out string error)
        {
            string result = "";
            error = "";
            long elapsed = 0;
            string responseData = string.Empty;
            //Post请求地址
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                HttpWebRequest m_Request = (HttpWebRequest)WebRequest.Create(requestUrl);
                //相应请求的参数
                byte[] data = Encoding.GetEncoding(codingType).GetBytes(requestData);
                m_Request.Method = "Post";
                m_Request.ContentType = "application/x-www-form-urlencoded";
                m_Request.ContentLength = data.Length;
                m_Request.Timeout = timeout;
                m_Request.ServicePoint.Expect100Continue = false;
                //请求流
                Stream requestStream = m_Request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                //响应流
                HttpWebResponse m_Response = (HttpWebResponse)m_Request.GetResponse();
                Stream responseStream = m_Response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(codingType));
                //获取返回的信息
                result = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                stopWatch.Stop();
                elapsed = stopWatch.ElapsedMilliseconds;
            }
            catch (WebException ex)
            {
                result = "";
                stopWatch.Stop();
                elapsed = stopWatch.ElapsedMilliseconds;
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    error = string.Format("请求超时[{0}],请求地址：{1}", elapsed, requestUrl);
                }
                else
                {
                    error = string.Format("{0},请求地址：{1}", ex.Message, requestUrl);
                }
                responseData = ex.ToString();
                // RecordException(requestUrl, ex);
            }
            catch (Exception ex)
            {
                result = "";
                error = string.Format("请求接口异常,请求地址：{0}", requestUrl);
                stopWatch.Stop();
                elapsed = stopWatch.ElapsedMilliseconds;
                responseData = ex.ToString();
            }
            if (string.IsNullOrWhiteSpace(responseData))
            {
                responseData = result;
            }
            return result;
        }
        #endregion
    }


    #region Http请求参考类
    /// <summary>
    /// Http请求参考类
    /// </summary>
    public class HttpItem
    {
        /// <summary>
        /// 请求URL必须填写
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求方式默认为GET方式,当为POST方式时必须设置Postdata的值
        /// </summary>
        public string Method { get; set; }

        int _timeout = 100000;
        /// <summary>
        /// 默认请求超时时间
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        int _readWriteTimeout = 30000;
        /// <summary>
        /// 默认写入Post数据超时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _readWriteTimeout; }
            set { _readWriteTimeout = value; }
        }
        /// <summary>
        /// 设置Host的标头信息
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///  获取或设置一个值，该值指示是否与 Internet 资源建立持久性连接默认为true。
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 请求标头值 默认为text/html, application/xhtml+xml, */*
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// 请求返回类型默认 text/html
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 返回数据编码默认为NUll,可以自动识别,一般为utf-8,gbk,gb2312
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Post的数据类型 默认为String
        /// </summary>
        public PostDataType PostDataType { get; set; }

        /// <summary>
        /// Post请求时要发送的字符串Post数据
        /// </summary>
        public string PostDataString { get; set; }

        /// <summary>
        /// Post请求时要发送的Byte类型的Post数据
        /// </summary>
        public byte[] PostDataByte { get; set; }

        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection { get; set; }

        /// <summary>
        /// 请求时的Cookie
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// 来源地址，上次访问地址
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// 证书绝对路径
        /// </summary>
        public string CerPath { get; set; }

        /// <summary>
        /// 设置代理对象
        /// </summary>
        public WebProxy WebProxy { get; set; }

        /// <summary>
        /// 是否设置为全文小写，默认为不转化
        /// </summary>
        public bool IsToLower { get; set; }

        /// <summary>
        /// 支持跳转页面，查询结果将是跳转后的页面，默认是不跳转
        /// </summary>
        public bool Allowautoredirect { get; set; }

        /// <summary>
        /// 最大连接数
        /// </summary>
        public int Connectionlimit { get; set; }

        /// <summary>
        /// 代理Proxy 服务器用户名
        /// </summary>
        public string ProxyUserName { get; set; }

        /// <summary>
        /// 代理 服务器密码
        /// </summary>
        public string ProxyPwd { get; set; }

        /// <summary>
        /// 代理 服务IP
        /// </summary>
        public string ProxyIp { get; set; }

        /// <summary>
        /// 设置返回类型String和Byte 默认为String
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        /// header对象
        /// </summary>
        public WebHeaderCollection Header { get; set; }

        /// <summary>
        /// 获取或设置用于请求的 HTTP 版本。返回结果:用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11。
        /// </summary>
        public Version ProtocolVersion { get; set; }

        /// <summary>
        ///  获取或设置一个 System.Boolean 值，该值确定是否使用 100-Continue 行为。如果 POST 请求需要 100-Continue 响应，则为 true；否则为 false。默认值为 true。
        /// </summary>
        public bool Expect100Continue { get; set; }

        /// <summary>
        /// 设置509证书集合
        /// </summary>
        public X509CertificateCollection ClentCertificates { get; set; }

        /// <summary>
        /// 设置或获取Post参数编码,默认的为Default编码
        /// </summary>
        public Encoding PostEncoding { get; set; }

        /// <summary>
        /// Cookie返回类型,默认的是只返回字符串类型
        /// </summary>
        public ResultCookieType ResultCookieType { get; set; }

        public HttpItem()
        {
            KeepAlive = true;
            Connectionlimit = 1024;
            ResultCookieType = ResultCookieType.String;
            Expect100Continue = true;
            Header = new WebHeaderCollection();
            ResultType = ResultType.String;
            Allowautoredirect = false;
            IsToLower = false;
            PostDataType = PostDataType.String;
            UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            Accept = "text/html, application/xhtml+xml, */*";
            ContentType = "text/html";
            Method = "GET";
        }
    }

    #endregion

    #region Http返回参数类
    /// <summary>
    /// Http返回参数类
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// Http请求返回的Cookie
        /// </summary>
        public string Cookie { get; set; }
        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        /// <summary>
        /// 返回的String类型数据 只有ResultType.String时才返回数据，其它情况为空
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// 返回的Byte数组 只有ResultType.Byte时才返回数据，其它情况为空
        /// </summary>
        public byte[] ResultByte { get; set; }
        /// <summary>
        /// header对象
        /// </summary>
        public WebHeaderCollection Header { get; set; }
        /// <summary>
        /// 返回状态说明
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// 返回状态码,默认为OK
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
    #endregion

    #region 返回类型
    /// <summary>
    /// 返回类型
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 表示只返回字符串 只有Html有数据
        /// </summary>
        String,
        /// <summary>
        /// 表示返回字符串和字节流 ResultByte和Html都有数据返回
        /// </summary>
        Byte
    }
    #endregion

    #region Post的数据格式默认为string
    /// <summary>
    /// Post的数据格式默认为string
    /// </summary>
    public enum PostDataType
    {
        /// <summary>
        /// 字符串类型，这时编码Encoding可不设置
        /// </summary>
        String,
        /// <summary>
        /// Byte类型，需要设置PostdataByte参数的值编码Encoding可设置为空
        /// </summary>
        Byte,
        /// <summary>
        /// 传文件，Postdata必须设置为文件的绝对路径，必须设置Encoding的值
        /// </summary>
        FilePath
    }
    #endregion

    #region Cookie返回类型
    /// <summary>
    /// Cookie返回类型
    /// </summary>
    public enum ResultCookieType
    {
        /// <summary>
        /// 只返回字符串类型的Cookie
        /// </summary>
        String,
        /// <summary>
        /// CookieCollection格式的Cookie集合同时也返回String类型的cookie
        /// </summary>
        CookieCollection
    }
    #endregion
}

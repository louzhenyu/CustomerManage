using System;
using System.Linq;
using System.Web;
using System.Xml;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.BLL.WX.Const;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using JIT.CPOS.BS.BLL.WX.Interface;
using JIT.CPOS.BS.BLL.WX.Factory;
using JIT.Utility.ExtensionMethod;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.WX
{
    public partial class ReceiveMsg : System.Web.UI.Page
    {
        public string guid { get; set; }
        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region HTTP请求入口

        public override void ProcessRequest(HttpContext param_context)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            guid = Guid.NewGuid().ToString().Replace("-", "");
            BaseService.WriteLogWeixin("---------------------------开始接收微信平台推送消息---------------------------|" + guid);
            string redUrl = "";
            string host = "";
            string redisKey = "";

            try
            {
                host = ConfigurationManager.AppSettings["RedisApiUrl"];

                var httpContext = param_context;
                string url = HttpContext.Current.Request.Url.ToString();
                BaseService.WriteLogWeixin("  请求的url:" + url);
                BaseService.WriteLogWeixin("  请求方式:" + httpContext.Request.HttpMethod.ToLower());
                //把HTTP请求转换为字符串
                string postStr = new BaseService().ConvertHttpContextToString(httpContext);   //获取post传过来的信息
                BaseService.WriteLogWeixin("  解密前postStr：" + postStr);
                //延迟几秒执行，看看微信会不会重复推送三次信息
                //System.Threading.Thread.Sleep(10000);
                // //避免微信服务器发起重试
                // httpContext.Response.Write("");
                // httpContext.Response.Write("success");
                //// 延迟几秒执行，看看微信会不会重复推送三次信息（放在后面查看还会不会重试）
                //       System.Threading.Thread.Sleep(10000);

                //增加redis验证防止多次回调处理脏数据
                redisKey = Utils.GetEncryptKey(postStr);
                var data = string.Empty;
                var redisEntity = new RedisEntity();

                try
                {
                    //缓存模式
                    redUrl = host + "keyvalue/get/" + redisKey; //判断缓存中是否存在标示
                    data = CommonBLL.GetRemoteData(redUrl, "Get", string.Empty);
                    string msg1 = string.Format("url:{0},data:{1}", redUrl, data);
                    BaseService.WriteLogWeixin(msg1 + "|" + guid);
                    redisEntity = data.DeserializeJSONTo<RedisEntity>();
                }
                catch (Exception e)
                {
                    BaseService.WriteLogWeixin("获取redis参数异常;" + e.Message + "|" + guid); //防止redis服务停止,终止程序
                }

                if (redisEntity.Message == "success")
                {
                    BaseService.WriteLogWeixin("-------------FSR WEI XIN redis 处理中" + redUrl + ":-------|" + guid);
                    return;
                }


                redUrl = host + "keyvalue/set/" + redisKey + "/ScanCode/2";
                RedisMedthodAsy(redUrl);



                if (httpContext.Request.HttpMethod.ToLower() == "post")
                {
                    if (!string.IsNullOrEmpty(postStr))
                    {
                        //获取微信公众号信息
                        LoggingSessionInfo loggingSessionInfo = null;//这里取过之后，后面不用重复取
                        WApplicationInterfaceEntity wAppEntity = new CommonBLL().GetWAppEntity(postStr, out loggingSessionInfo);
                        string token = string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid) ? wAppEntity.Token : wAppEntity.OpenToken;//如果开放授权给开放平台了就用开放平台的token
                        if (wAppEntity == null || string.IsNullOrEmpty(token))
                        {
                            return;
                        }
                        if (!string.IsNullOrEmpty(httpContext.Request["echoStr"]))
                        {
                            //用于进行微信平台token验证
                            new CommonBLL().ValidToken(httpContext, token);//Config.TOKEN配置的token，其实应该是每个客户都有自己的token，配置在数据库里，然后取出来
                        }
                        #region
                        //验证这个之后看看是否可以去重
                        //避免微信服务器发起重试
                        //httpContext.Response.Write("");
                        //httpContext.Response.Write("success");
                        // 延迟几秒执行，看看微信会不会重复推送三次信息（放在后面查看还会不会重试）
                        //  System.Threading.Thread.Sleep(10000);

                        //在这里要进行加解密，用接收过来信息的ToUserName（公众号的为weixinid，例如： gh_9cbe4cd7941a）
                        int ret = 0;//解密情况
                        string TrueEncodingAESKey = "";//如果是安全模式，后面回复信息时，加密的key
                        // int EncryptType = 0;
                        //string appid = "";
                        //Config.TOKEN 替换为wAppEntity.Token
                        postStr = new CommonBLL().WXDecryptMsg(httpContext, postStr, wAppEntity, loggingSessionInfo, out  ret, out  TrueEncodingAESKey);
                      //  BaseService.WriteLogWeixin("  解密后post string:" + postStr);
                        if (ret != 0)
                        {
                            BaseService.WriteLogWeixin("解密出现错误 ret: " + ret + "|" + guid);
                            return;
                        }

                        //设置请求参数
                        var requestParams = SetRequestParams(postStr, TrueEncodingAESKey, httpContext, loggingSessionInfo, wAppEntity);
                        BaseService.WriteLogWeixin("请求参数:" + requestParams.ToJSON() + "|" + guid);

                        //响应微信平台推送消息
                        ResponseMsg(httpContext, requestParams);
                        #endregion
                    }
                }
                else//专门用于微信公众号里“填写服务器配置”，这时候向这个url传递的方式是get，没有weixinid，无法从数据库里取数据
                {
                    if (!string.IsNullOrEmpty(httpContext.Request["echoStr"]))
                    {
                        //用于进行微信平台token验证
                        new CommonBLL().ValidToken(httpContext, JIT.CPOS.BS.BLL.WX.Const.Config.TOKEN);//Config.TOKEN配置的token，其实应该是。每个客户都有自己的token，配置在数据库里，然后取出来
                    }
                }
                redUrl = host + "keyvalue/del/" + redisKey; //数据处理成功,删除缓存
                RedisMedthodAsy(redUrl);
            }
            catch (Exception ex)
            {
                redUrl = host + "keyvalue/del/" + redisKey; //数据处理成功,删除缓存
                string msg = string.Format("异常信息：【{0}】{1}", redUrl, ex.Message + "|" + guid);
                BaseService.WriteLogWeixin(msg);
                try
                {
                    CommonBLL.GetRemoteData(redUrl, "Get", string.Empty);
                }
                catch (Exception ex1)
                {
                    msg = string.Format("异常信息2：【{0}】{1}", redUrl, ex1.Message + "|" + guid);
                    BaseService.WriteLogWeixin(msg);
                }
            }
            watch.Stop();
            BaseService.WriteLogWeixin("-------------FSR WEI XIN ex1接收微信平台推送消息处理结束:" + watch.ElapsedMilliseconds + "|" + guid);
        }

        #endregion

        #region 设置请求参数
        //设置请求参数
        private RequestParams SetRequestParams(string postStr, string _TrueEncodingAESKey, HttpContext httpContext, LoggingSessionInfo loggingSessionInfo, WApplicationInterfaceEntity wAppEntity)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);

            XmlNodeList list = doc.GetElementsByTagName("xml");
            XmlNode xn = list[0];
            string openID = xn.SelectSingleNode("//FromUserName").InnerText;    //发送方帐号（一个OpenID）
            string weixinID = xn.SelectSingleNode("//ToUserName").InnerText;    //开发者微信号
            string msgType = xn.SelectSingleNode("//MsgType").InnerText.ToLower();  //消息类型(text, image, location, link, event)

            BaseService.WriteLogWeixin("微信平台推送的消息:  " + postStr);
            BaseService.WriteLogWeixin("FromUserName(发送方帐号):  " + openID);
            BaseService.WriteLogWeixin("ToUserName(开发者微信号):  " + weixinID);
            BaseService.WriteLogWeixin("MsgType(消息类型):  " + msgType);

            var timestamp = httpContext.Request["timestamp"] == null ? "" : httpContext.Request["timestamp"].ToString();
            var nonce = httpContext.Request["nonce"] == null ? "" : httpContext.Request["nonce"].ToString();

            var requestParams = new RequestParams()
            {
                OpenId = openID,
                WeixinId = weixinID,
                MsgType = msgType,
                XmlNode = xn,
                LoggingSessionInfo = loggingSessionInfo,//BaseService.GetWeixinLoggingSession(weixinID),
                TrueEncodingAESKey = _TrueEncodingAESKey,
                Token = string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid) ? wAppEntity.Token : wAppEntity.OpenToken,//如果授权给公众平台了，就用公众平台的token
                AppID = string.IsNullOrEmpty(wAppEntity.OpenOAuthAppid) ? wAppEntity.AppID : wAppEntity.OpenAppID,
                EncryptType = (int)wAppEntity.EncryptType,
                Timestamp = timestamp,
                Nonce = nonce
            };

            return requestParams;
        }

        #endregion

        #region 响应微信平台推送消息

        //响应微信平台推送消息
        private void ResponseMsg(HttpContext httpContext, RequestParams requestParams)
        {
            BaseBLL weixin = null;
            IFactory factory = null;

            #region 通过微信类型生成对应的业务处理类

            var application = new WApplicationInterfaceBLL(requestParams.LoggingSessionInfo);
            var appEntitys = application.QueryByEntity(new WApplicationInterfaceEntity() { WeiXinID = requestParams.WeixinId }, null);

            if (appEntitys != null && appEntitys.Length > 0)
            {
                var entity = appEntitys.FirstOrDefault();
                BaseService.WriteLogWeixin("通过微信类型生成对应的业务处理类");
                BaseService.WriteLogWeixin("WeiXinTypeId(微信类型):  " + entity.WeiXinTypeId);

                switch (entity.WeiXinTypeId)
                {
                    case WeiXinType.SUBSCRIPTION:
                        factory = new SubscriptionFactory();
                        weixin = factory.CreateWeiXin(httpContext, requestParams);
                        BaseService.WriteLogWeixin("订阅号");
                        break;
                    case WeiXinType.SERVICE:
                        factory = new ServiceFactory();
                        weixin = factory.CreateWeiXin(httpContext, requestParams);
                        BaseService.WriteLogWeixin("服务号");
                        break;
                    case WeiXinType.CERTIFICATION:    //目前我们的客户一般是认证服务号，所以关注事件从这里查看
                        factory = new CertificationFactory();
                        weixin = factory.CreateWeiXin(httpContext, requestParams);
                        BaseService.WriteLogWeixin("认证服务号");
                        break;
                    case WeiXinType.SUBSCRIPTION_EXTEND:
                        BaseService.WriteLogWeixin("可扩展订阅号");
                        break;
                    case WeiXinType.SERVICE_EXTEND:
                        BaseService.WriteLogWeixin("可扩展服务号");
                        break;
                    case WeiXinType.CERTIFICATION_EXTEND:
                        BaseService.WriteLogWeixin("可扩展认证服务号");
                        break;
                    default:
                        factory = new SubscriptionFactory();
                        weixin = factory.CreateWeiXin(httpContext, requestParams);
                        BaseService.WriteLogWeixin("默认订阅号");
                        break;
                }
            }

            #endregion

            weixin.ResponseMsg();//根据消息类型，回应事件。有文本消息、图片消息、多客服、地理位置、事件
        }

        #endregion

        public void RedisMedthodAsy(string url)
        {
            ThreadPool.QueueUserWorkItem(RedisMedthod, url);
        }

        public void RedisMedthod(object obj)
        {
            try
            {
                CommonBLL.GetRemoteData((obj as string), "Get", string.Empty);
            }
            catch (Exception e)
            {
                BaseService.WriteLogWeixin(obj + "删除redis参数异常;" + e.Message + "|" + guid);
            }
        }

        #region redis 实体
        /// <summary>
        /// redis 实体
        /// </summary>
        public class RedisEntity
        {
            public string Code = string.Empty;
            public string Message = string.Empty;
            public string Result = string.Empty;
        }
        #endregion

    }
}
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

namespace JIT.CPOS.Web.WX
{
    public partial class ReceiveMsg : System.Web.UI.Page
    {
        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region HTTP请求入口

        public override void ProcessRequest(HttpContext param_context)
        {
            BaseService.WriteLogWeixin("---------------------------开始接收微信平台推送消息---------------------------");

            try
            {
                var httpContext = param_context;

                if (!string.IsNullOrEmpty(httpContext.Request["echoStr"]))
                {
                    //用于进行微信平台token验证
                    new CommonBLL().ValidToken(httpContext, Config.TOKEN);
                }

                if (httpContext.Request.HttpMethod.ToLower() == "post")
                {
                    //把HTTP请求转换为字符串
                    string postStr = new BaseService().ConvertHttpContextToString(httpContext);

                    BaseService.WriteLogWeixin("post string:" + postStr);

                    if (!string.IsNullOrEmpty(postStr))
                    {
                        //设置请求参数
                        var requestParams = SetRequestParams(postStr);
                        BaseService.WriteLogWeixin("请求参数:" + requestParams.ToJSON());

                        //响应微信平台推送消息
                        ResponseMsg(httpContext, requestParams);
                    }
                }
            }
            catch (Exception ex)
            {
                BaseService.WriteLogWeixin("异常信息:  " + ex.ToString());
            }
        }

        #endregion

        #region 设置请求参数

        //设置请求参数
        private RequestParams SetRequestParams(string postStr)
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

            var requestParams = new RequestParams()
            {
                OpenId = openID,
                WeixinId = weixinID,
                MsgType = msgType,
                XmlNode = xn,
                LoggingSessionInfo = BaseService.GetWeixinLoggingSession(weixinID)
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

            weixin.ResponseMsg();
        }

        #endregion
    }
}
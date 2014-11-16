using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Pay.WeiXinPay
{
    /// <summary>
    /// WxPayNotifyTest 的摘要说明
    /// </summary>
    public class WxPayNotifyTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            #region

            var customerId = "4bbc5931e8d94de98a858775ebb1a93e";
            var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");

            var common = new CommonBLL();

            var accessToken =
                common.GetAccessTokenByCache("wxa2f899fbaf225904", "d3e2cb21fdb44fd2a945a593c1ba9856", currentUserInfo).access_token;


            var interfaceType = context.Request["Type"];
            var result = "";
            if (interfaceType == "UpdateFeedback")
            {
                var feedbackId = context.Request["feedbackId"];
                var openId = context.Request["openId"];
                result = common.UpdatePayFeedBack(accessToken, currentUserInfo, openId, feedbackId);
            }

            context.Response.Write(result);


            //const string url = "http://localhost:11291/ApplicationInterface/Pay/WeiXinPay/ComplaintNotification.ashx";
            //var paras =
            //    @"<xml><OpenId><![CDATA[oDF3iY9P32sK_5GgYiRkjsCo45bk]]></OpenId><AppId><![CDATA[wx04659ffd5ee802b6]]></AppId><TimeStamp>1393400471</TimeStamp><MsgType><![CDATA[request]]></MsgType><FeedBackId>7197417460812502768</FeedBackId><TransId><![CDATA[1900000109201402143240185685]]></TransId><Reason><![CDATA[ 质量问题]]></Reason><Solution><![CDATA[ 换货]]></Solution><ExtInfo><![CDATA[ 备注12435321321]]></ExtInfo><AppSignature><![CDATA[d60293982cc7c97a5a9d3383af761db763c07c86]]></AppSignature><SignMethod><![CDATA[sha1]]></SignMethod><PicInfo><item><PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz/49ogibiahRNtOk37iaztwmdgFbyFS9FUrqfodiaUAmxr4hOP34C6R4nGgebMalKuY3H35riaZ5vtzJh25tp7vBUwWxw/0]]></PicUrl></item><item><PicUrl><![CDATA[http://mmbiz.qpic.cn/mmbiz/49ogibiahRNtOk37iaztwmdgFbyFS9FUrqfn3y72eHKRSAwVz1PyIcUSjBrDzXAibTiaAdrTGb4eBFbib9ibFaSeic3OIg/0]]></PicUrl></item></PicInfo></xml>";
            //CommonBLL.GetRemoteData(url, "POST", paras);
            //const string url = "http://www.o2omarketing.cn/ApplicationInterface/Pay/WeiXinPay/Warning.ashx";
            ////var paras =
            //   // @"<xml><AppId><![CDATA[wxf8b4f85f3a794e77]]></AppId><TimeStamp>1393400471</TimeStamp><ErrorType>1001</ErrorType><Description>![CDATA[错误描述]]</Description><AlarmContent>![CDATA[transaction_id=10001]]</AlarmContent><AppSignature><![CDATA[d60293982cc7c97a5a9d3383af761db763c07c86]]></AppSignature><SignMethod><![CDATA[sha1]]></SignMethod></xml>";
            //var paras =
            //    @"<xml><AppId><![CDATA[wxa2f899fbaf225904]]></AppId><TimeStamp>1404897678</TimeStamp><ErrorType>1</ErrorType><Description><![CDATA[test]]></Description><AlarmContent><![CDATA[test]]></AlarmContent><AppSignature><![CDATA[6f7c023a39441b69fd42b4c6d606f4c3dab41203]]></AppSignature><SignMethod><![CDATA[sha1]]></SignMethod></xml>";
            //CommonBLL.GetRemoteData(url, "POST", paras);

            //var orderId = context.Request.QueryString["orderId"];
            //var customerId = "24af2889e6054496b1903e0ba5dd01cf";
            //var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");
            //var bll = new T_InoutBLL(currentUserInfo);
            //bll.GetDeliverInfoByOrderId(orderId, currentUserInfo);

            #endregion
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
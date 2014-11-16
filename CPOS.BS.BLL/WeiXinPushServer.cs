using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.Common;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 微信推送接口
    /// </summary>
    public class WeiXinPushServer
    {
        #region 推送消息
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="msgUrl">微信调用接口地址譬如：http://IP/ialumni/sendmessage.aspx </param>
        /// <param name="msgText">推送内容</param>
        /// <param name="OpenID">客户在微信平台的唯一公众码</param>
        /// <returns></returns>
        public bool SetPushServer(string msgUrl,string msgText,string OpenID)
        {
            try
            {
                #region 推送消息
                //string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                //string msgText = string.Format("您推荐的会员刚刚购买了我店商品，为了表示感谢。我们送您积分{0}分。",Convert.ToInt32(integralValue));
                string msgData = "<xml><OpenID><![CDATA[" + OpenID + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";
                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                #endregion

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushServer:{0}", msgResult)
                });
                return true;
            }
            catch (Exception ex) {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushServerError:{0}", ex.ToString())
                });
                return false;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Util.SMS
{
    public static class SMSHelper
    {
        public static bool Send(string customerId,string pPhone, string pContent, string pSign, out string msg)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["SMSURL"];
                var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");
                if (string.IsNullOrEmpty(url))
                    throw new Exception("未配置短信服务URL");
                //var para = new { MobileNO = pPhone, SMSContent = string.Format(@"您的验证码是：【{0}】", pContent), Sign = pSign };
              var para = new { MobileNO = pPhone, SMSContent = string.Format(@"您的验证码是：{1}，请不要把验证码泄露给其他人。", currentUserInfo.ClientName , pContent), Sign = pSign };
                var request = new { Action = "SendMessage", Parameters = para };
                string str = string.Format("request={0}", request.ToJSON());//请求参数
                Loggers.Debug(new DebugLogInfo() { Message = "发送短信:" + str });
                var res = HttpClient.PostQueryString(url, str);//发送请求，开始发送短信
                var response = res.DeserializeJSONTo<Response>();//解析发送短信后，返回的内容
                Loggers.Debug(new DebugLogInfo() { Message = "收到返回信息:" + response.ToJSON() });

                
                //var currentUserInfo = Default.GetBSLoggingSession(customerId, "1");//放到方法头使用
                var vipBll = new VipBLL(currentUserInfo);

                var vipEntity = vipBll.QueryByEntity(new VipEntity()
                {
                    Phone = pPhone
                }, null);
                string vipId = Common.Utils.NewGuid();
                if (vipEntity != null && vipEntity.Length > 0)
                {
                    vipId = vipEntity[0].VIPID;
                }

                (new MarketSendLogBLL(currentUserInfo)).Create(new MarketSendLogEntity()
                {
                    LogId = Common.Utils.NewGuid(),
                    VipId = vipId,
                    MarketEventId = vipId,
                    TemplateContent = response.ToJSON(),
                    SendTypeId = "1",
                    Phone = pPhone,
                    IsSuccess = response.ResultCode < 100 ? 1 : 0
                });

                if (response.ResultCode < 100)
                {
                    msg = "短信发送成功";
                    return true;
                }
                else
                {
                    msg = "短信发送失败:" + response.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                msg = ex.Message;
                return false;
            }
        }
    }

    public class Response
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public object ResData { get; set; }
    }

    public static class CharsFactory
    {
        public static string Create6Char()
        {
            Random r = new Random();
            var i = r.Next(100000, 1000000);
            return i.ToString();
        }
    }
}
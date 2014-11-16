using System;
using System.Configuration;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Log;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.BLL.CS
{
    public class PushSMSMessage : IPushMessage
    {
        private LoggingSessionInfo loggingSessionInfo;
        public PushSMSMessage(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        public void PushMessage(string memberID, string messageContent)
        {
            string[] content;
            if (messageContent.IndexOf("[|") == 0)
            {
                throw new Exception("发送短信失败:没有指定签名" + messageContent);
            }
            content = messageContent.Split("[|".ToCharArray());
            string signContent = content[2];

            string[] arr = signContent.Split(',');
            string mobileNo = "";
            if (arr.Length == 2)
            {
                mobileNo = arr[1];
                signContent = arr[0];
            }

            VipEntity vipEntity = new VipBLL(loggingSessionInfo).GetByID(memberID);
            if (string.IsNullOrEmpty(mobileNo))
            {

                if (!string.IsNullOrEmpty(vipEntity.Phone))
                {
                    mobileNo = vipEntity.Phone;
                }
            }

            if (!string.IsNullOrEmpty(mobileNo) && content.Length == 3)
            {
                string Url = ConfigurationManager.AppSettings["customer_service_url"].TrimEnd('/') + "/CustomerService/Data.aspx";
                string param = "action=sendSMS&ReqContent={\"common\":{\"userId\":\"" + vipEntity.VIPID + "}\"},\"special\":{\"mobileNo\":\"" + mobileNo + "\",\"content\":\"" + content[0] + "\",\"sign\":\"" + signContent + "\"}}";

                var res = HttpClient.PostQueryString(Url, param);
                Loggers.DEFAULT.Debug(new DebugLogInfo
                {
                    Message = "外发短信" + param + "结果：" + res
                });
            }


        }
    }
}

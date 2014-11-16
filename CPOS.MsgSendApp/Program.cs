using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Web;

namespace JIT.CPOS.MsgSendApp
{
    class Program
    {
        static int SendDelayTime = 10; // ms

        static void Main(string[] args)
        {
            try
            {
                int cycleTime = int.Parse(ConfigurationManager.AppSettings["cycleTime"]);
                string batId = string.Empty;

                while (true)
                {
                    if (ConfigurationManager.AppSettings["EnableTaskSend"] == "1")
                    {
                        Console.WriteLine(string.Format("[{0}]任务开始...", Utils.GetNow()));
                        GOrderBLL gOrderBLL = new GOrderBLL(GetLoginUser("bs"));
                        var msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"];
                        var error = "";
                        var flag = gOrderBLL.SetGOrderPushAll(msgUrl, out error);
                        if (flag)
                        {
                            Console.WriteLine(string.Format("[{0}] 执行成功", Utils.GetNow()));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("[{0}] 执行失败：{1}", Utils.GetNow(), error));
                        }
                        Thread.Sleep(SendDelayTime);
                        Console.WriteLine(string.Format("[{0}]任务结束", Utils.GetNow()));
                    }
                    if (ConfigurationManager.AppSettings["EnableMsgSend_lj"] == "1")
                    {
                        Console.WriteLine(string.Format("[{0}]lj消息发送任务开始...", Utils.GetNow()));
                        LVipOldBLL lVipOldBLL = new LVipOldBLL(GetLoginUser("lj"));

                        // 获取lj消息列表
                        var msgList = lVipOldBLL.GetVipListForMsgSend(new LVipOldEntity() { });

                        // 发送lj消息
                        Console.WriteLine(string.Format("[{0}]消息数量：{1}", Utils.GetNow(),
                            (msgList == null ? 0 : msgList.Count)));
                        if (msgList != null)
                        {
                            foreach (var msgItem in msgList)
                            {
                                if (msgItem == null ||
                                    msgItem.Mobile == null || msgItem.Mobile.Length < 10)
                                    continue;

                                string weixin = "泸州老窖VIP尊享";
                                string msg = string.Format(
                                    //"亲爱的{0}，泸州老窖最新推出的官方微信公众号“{1}”上线了，我们等待您的关注，请关注后在平台的菜单中选择积分券兑换，并输入积分券号“{2}”，赢取积分，兑换精美礼品。",
                                    "亲爱的{0}，泸州老窖最新推出的官方微信“{1}”上线了，我们等待您的关注，请关注后在平台的菜单中选择“贵宾卡领取”，并输入贵宾卡号“{2}”验证，您将成为我们泸州老窖的VIP贵宾，独享更多的尊崇服务。",
                                    msgItem.VipName, weixin, msgItem.CardCode);
                                MsgSend(lVipOldBLL, msgItem, msg);

                                Thread.Sleep(SendDelayTime);
                            }
                        }
                        Console.WriteLine(string.Format("[{0}]lj消息发送任务结束", Utils.GetNow()));
                    }
                    Console.WriteLine(string.Format("".PadLeft(50, '=')));
                    Thread.Sleep(cycleTime);
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("Exception:", ex.ToString())
                });
                Console.Write(ex.ToString());
                Console.Read();
            }
        }

        public static bool SMSSend(string mobileNO, string SMSContent)
        {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            string smsContentEncode = HttpUtility.UrlEncode(SMSContent, gb2312);
            string uri = string.Format("http://www.jitmarketing.cn/SendSMS.asp?code=jit2010sms&mobilelist={0}&content={1}",
                mobileNO, smsContentEncode);
            string method = "GET";
            string cotent = "";
            string data = CPOS.Common.Utils.GetRemoteData(uri, method, cotent);
            return true;
        }

        static void MsgSend(LVipOldBLL lVipOldBLL, LVipOldEntity vipEntity, string msg)
        {
            string mailto = vipEntity.Mobile;
            string mailBody = msg;
            try
            {
                var success = SMSSend(mailto, mailBody);
                if (success)
                {
                    vipEntity.IsPush = 1;
                    vipEntity.PushCount = vipEntity.PushCount == null ? 1 : vipEntity.PushCount + 1;
                    vipEntity.LastPushTime = DateTime.Now;
                    lVipOldBLL.Update(vipEntity);
                    Console.WriteLine("短信发送成功: {0}--{1}", mailto, mailBody);
                }
                else
                {
                    vipEntity.PushCount = vipEntity.PushCount == null ? 1 : vipEntity.PushCount + 1;
                    vipEntity.LastPushTime = DateTime.Now;
                    vipEntity.ErrReason = "短信发送失败";
                    lVipOldBLL.Update(vipEntity);
                    Console.WriteLine("短信发送失败: {0}--{1}", mailto, mailBody);
                }
            }
            catch (Exception ex)
            {
                vipEntity.PushCount = vipEntity.PushCount == null ? 1 : vipEntity.PushCount + 1;
                vipEntity.LastPushTime = DateTime.Now;
                vipEntity.ErrReason = "短信发送失败:" + ex.ToString();
                lVipOldBLL.Update(vipEntity);
                Console.WriteLine("短信发送失败: {0}--{1}--{2}", mailto, mailBody, vipEntity.ErrReason);
            }
        }

        static LoggingSessionInfo GetLoginUser(string name)
        {
            var obj = new LoggingSessionInfo();
            obj.CurrentLoggingManager = new LoggingManager();
            obj.CurrentLoggingManager.Connection_String = GetConn(name);
            obj.CurrentUser = new BS.Entity.User.UserInfo();
            obj.CurrentUser.customer_id = ConfigurationManager.AppSettings["customer_id"];
            obj.CurrentUser.User_Id = "1";
            return obj;
        }

        static string GetConn(string name)
        {
            switch (name)
            { 
                case "lj":
                    return ConfigurationManager.AppSettings["Conn_lj"].Trim();
                case "bs":
                    return ConfigurationManager.AppSettings["Conn_bs"].Trim();
            }
            return string.Empty;
        }
    }
}

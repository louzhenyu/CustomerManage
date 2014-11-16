using System;
using System.Xml;
using System.Web;
using System.Linq;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Base.XML;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;

using JIT.Utility.Web;
using JIT.Utility.DataAccess;
using JIT.Utility.Reflection;
using JIT.Utility.Notification;
using JIT.Utility.ExtensionMethod;



namespace JIT.CPOS.BS.Web.Project.SendMessage.Handler
{
    /// <summary>
    /// SendMessageHandler 的摘要说明
    /// </summary>
    public class SendMessageHandler : JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "sendTestMsg":
                    res = sendTestMsg(pContext.Request.Form);
                    break;
                case "sendMsg":
                    res = sendMsg(pContext.Request.Form);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region sendTestMsg
        /// <summary>
        /// 发送测试邮件
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string sendTestMsg(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";

            string emailAddress = "";
            if (!string.IsNullOrEmpty(rParams["emailAddress"]))
            {
                emailAddress = rParams["emailAddress"];
            }
            else
            {
                return res = "{success:false,msg:'测试邮件地址不可为空.'}";
            }

            try
            {
                XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                FromSetting fs = new FromSetting();
                fs.SMTPServer = xml.SelectNodeText("//Root/TestMail//SMTPServer", 0);
                fs.SendFrom = xml.SelectNodeText("//Root/TestMail//MailSendFrom", 0);
                fs.UserName = xml.SelectNodeText("//Root/TestMail//MailUserName", 0);
                fs.Password = xml.SelectNodeText("//Root/TestMail//MailUserPassword", 0);
                Mail.SendMail(fs, emailAddress, xml.SelectNodeText("//Root/TestMail//MailTitle", 0), xml.SelectNodeText("//Root/TestMail//MailContent", 0), null);
            }
            catch
            {
                return res;
            }
            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion

        #region sendMsg
        /// <summary>
        /// 发送测试邮件
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string sendMsg(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";

            string emailAddress = null, emailTitle = null, emailContent = null, userName = null;

            #region 参数验证
            if (!string.IsNullOrEmpty(rParams["emailAddress"]))
            {
                emailAddress = rParams["emailAddress"];
            }
            else
            {
                return res = "{success:false,msg:'邮箱地址不可为空.'}";
            }
            if (!string.IsNullOrEmpty(rParams["tempEmailTitle"]))
            {
                emailTitle = rParams["tempEmailTitle"];
            }
            else
            {
                return res = "{success:false,msg:'邮箱标题不可为空.'}";
            }
            if (!string.IsNullOrEmpty(rParams["emailContent"]))
            {
                emailContent = rParams["emailContent"];
            }
            else
            {
                return res = "{success:false,msg:'邮箱内容不可为空.'}";
            }
            if (!string.IsNullOrEmpty(rParams["userName"]))
            {
                userName = rParams["userName"];
            }
            else
            {
                return res = "{success:false,msg:'收件人姓名不可为空.'}";
            }
            #endregion

            try
            {
                XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                FromSetting fs = new FromSetting();
                fs.SMTPServer = xml.SelectNodeText("//Root/TestMail//SMTPServer", 0);
                fs.SendFrom = xml.SelectNodeText("//Root/TestMail//MailSendFrom", 0);
                fs.UserName = xml.SelectNodeText("//Root/TestMail//MailUserName", 0);
                fs.Password = xml.SelectNodeText("//Root/TestMail//MailUserPassword", 0);
                if (emailAddress.Contains(',') && userName.Contains(','))
                {
                    string[] addrList = emailAddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string[] nameList = userName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < addrList.Length; i++)
                    {
                        Mail.SendMail(fs, addrList[i], emailTitle.Replace("{$username$}", nameList[i].ToString()), emailContent.Replace("#会员#", nameList[i].ToString()), null);
                    }
                }
                else
                {
                    Mail.SendMail(fs, emailAddress, emailTitle.Replace("{$username$}", userName), emailContent.Replace("#会员#", userName), null);
                }
            }
            catch
            {
                return res;
            }
            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion
    }
}
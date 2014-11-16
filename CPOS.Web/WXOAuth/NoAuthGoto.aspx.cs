using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.WXOAuth
{
    /// <summary>
    /// 无认证跳转
    /// </summary>
    public partial class NoAuthGoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //参数&参数校验
                var pageName = this.Request["pageName"];
                var customerID = this.Request["customerId"];
                var goUrl = this.Request["goUrl"];
                if (string.IsNullOrWhiteSpace(pageName))
                {
                    this.WriteError("QueryString参数中不存在pageName节");
                    return;
                }
                if (string.IsNullOrWhiteSpace(customerID))
                {
                    this.WriteError("QueryString参数中不存在customerId节");
                    return;
                }
                if (string.IsNullOrWhiteSpace(goUrl))
                {
                    this.WriteError("QueryString参数中不存在goUrl节");
                    return;
                }
                //
                var loggingSessionInfo = Default.GetBSLoggingSession(customerID, "1");
                goUrl = "http://" + HttpUtility.UrlDecode(goUrl);
                var configBll = new WeiXinH5ConfigBLL(loggingSessionInfo);
                var pagePath = configBll.GetPagePathByPageName(customerID, pageName);
                Random rad = new Random();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("NoAuthGoto: pagePath：{0};goUrl:{1}", pagePath, goUrl)
                });
                goUrl = goUrl.Replace("_pageName_", pagePath);
                if (goUrl.IndexOf("?") > 0)
                {
                    goUrl = goUrl + "&customerId=" + customerID + "&rid=" + rad.Next(1000, 100000) + "";
                }
                else
                {
                    goUrl = goUrl + "?customerId=" + customerID + "&rid=" + rad.Next(1000, 100000) + "";
                }
                Response.Write(goUrl);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("跳转URL：{0}", goUrl)
                });
                Response.Redirect(goUrl);
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                this.WriteError(string.Format("发生异常,异常信息：{0}",ex.Message));
            }
        }

        protected void WriteError(string pErrorMsg)
        {
            this.Response.Clear();
            this.Response.StatusCode = 500;
            this.Response.Write(pErrorMsg);
            this.Response.End();
        }
    }
}
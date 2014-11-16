using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Collections;

namespace JIT.CPOS.BS.Web.Module.Reports
{
    public partial class ReportVitality : System.Web.UI.Page
    {
        /// <summary>
        /// 页面初始载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }

        #region 私有方法
        private void LoginSSOReport()
        {
            //传入值
            ArrayList arr = new ArrayList();
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                    arr.Add(CheckBoxList1.Items[i].Value);
            }
            if (arr.Count > 0)
            {
                #region 登录SSO

                ReportService.MSTRIntegrationServiceClient service = new ReportService.MSTRIntegrationServiceClient();
                string user = "test";
                int lcid = 2052;//LCID区域编码
                //string localIP = GetHttpClientIP();//本机外网IP
                string localIP = GetClientIP();
                HiddenField1.Value = localIP;
                string pclient = "10001";//项目ID号
                int rst = service.Login(lcid, localIP, pclient, user, Session.SessionID);
                #endregion

                #region 读取报表

                ReportService.MstrPromptAnswerItem[] plist = new ReportService.MstrPromptAnswerItem[1];
                ReportService.MstrPromptAnswerItem pl = new ReportService.MstrPromptAnswerItem();
                pl.PromptCode = "SelectMonth";
                pl.QueryCondition = (string[])arr.ToArray(typeof(string));
                plist[0] = pl;
                ReportService.MstrDataRigthPromptAnswerItem[] dlist = new ReportService.MstrDataRigthPromptAnswerItem[0];


                string reportGUID = "C5988CDA4B4DCD4433AF9D862EA68F5B";//会员活跃度分析
                string ReportUrl = service.GetMstrReportUrl(rst, reportGUID, pclient, user, plist, dlist);
                lblReport.Text = "<iframe src=\"" + ReportUrl + "\" frameborder=\"0\" id=\"frm\" scrolling=\"auto\" width=\"100%\" height=\"600\" />";
                #endregion

            }
        }

        /// <summary>
        /// 获取客户端IP地址，必须外网IP有效
        /// </summary>
        /// <returns></returns>
        private string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>
        /// 临时获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        private string GetHttpClientIP()
        {
            string html = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://iframe.ip138.com/ic.asp");
                request.Method = "Get";
                request.ContentType = "application/x-www-form-urlencoded ";
                WebResponse response = request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, System.Text.Encoding.GetEncoding("GB2312"));
                html = sr.ReadToEnd();
                s.Close();
                sr.Close();
            }
            catch (Exception err)
            {
                throw new Exception("访问地址出错~~~ ");
            }
            int count = html.Length;
            int starIndex = html.IndexOf("[", 0, count);
            int endIndex = html.IndexOf("]", starIndex, count - starIndex);
            html = html.Substring(starIndex + 1, endIndex - (starIndex + 1));
            return html;
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoginSSOReport();
        }
    }
}
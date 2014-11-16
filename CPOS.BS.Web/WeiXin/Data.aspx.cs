using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Text;

namespace JIT.CPOS.BS.Web.WeiXin
{
    public partial class Data : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            if (!IsPostBack)
            {
                string customerId = "29E11BDC6DAC439896958CC6866FF64E";
                string token = "7d4cda48970b4ed0aa697d8c2c2e4af3";
                LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
                loggingSessionInfo = GetLoggingSession(customerId, token);
                //-----------------------------------------------------------------

                string content = string.Empty;
                try
                {
                    string dataType = Request["dataType"].ToString().Trim().ToLower();
                    switch (dataType) { 
                        case "SignIn":
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    content = ex.Message;
                }
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write(content);
                Response.End();
            }
        }

        #region 获取登录用户信息
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private LoggingSessionInfo GetLoggingSession(string customerId, string token)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, token);
            return loggingSessionInfo;
        }
        #endregion

        #region 用户关注
        /// <summary>
        /// 用户关注
        /// </summary>
        /// <param name="loggingSesssionInfo"></param>
        /// <returns></returns>
        public string SetSignIn(LoggingSessionInfo loggingSesssionInfo)
        {
            //关注：openID，城市，性别，昵称
            string content = string.Empty;
            #region 参数
            string OpenID = Request["openID"].ToString().Trim();
            string City = Request["city"].ToString().Trim();
            string Gender = Request["gender"].ToString().Trim();
            string VipName = Request["vipName"].ToString().Trim();
            string IsShow = Request["isShow"].ToString().Trim();
            #endregion
            #region 1.处理日志
            VipShowLogEntity vipShowLogInfo = new VipShowLogEntity();
            vipShowLogInfo.VipLogID = System.Guid.NewGuid().ToString().Replace("-", "");
            vipShowLogInfo.OpenID = OpenID;
            vipShowLogInfo.City = City;
            vipShowLogInfo.IsShow = Convert.ToInt32(IsShow);
            VipShowLogBLL vipShowLogBll = new VipShowLogBLL(loggingSesssionInfo);
            vipShowLogBll.Create(vipShowLogInfo);
            #endregion
            #region 2.判断是新建还是修改
            VipEntity vipInfo = new VipEntity();
            #endregion
            return content;
        }
        #endregion
    }
}
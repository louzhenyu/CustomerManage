using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility;
using JIT.CPOS.BS.Entity;
using System.Configuration;
using JIT.Utility.DataAccess;

namespace JIT.EMBAUnion.Web
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        public static string imageUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.newsTitle.Text = "Apache VirtualHost配置成功/Wamp虚拟主机配置 分享";
                imageUrl = "http://www.vxinqing.com/productions/20130425004309477.jpgs_800.jpg";
                this.newsPublishTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

                string content = "<p style=\"text-indent: 2em;\">";
                content += "微软在过去的很多年中为IE增加了很多特性，有些对web带来了一些很深远的影响（XMLHTTPRequest";
                content += "，Dom，XML工具，字体嵌入，浏览器插件等），还有些根本没有流行起来，当然也有一些是非常糟糕的。";
                content += "IE团队打算在IE10版本中移除一些价值不大的历史特性（或许他们阅读了可能让我们喜欢上IE的10种方式？";
                content += "中的第7点）。或许你可能从没有在你的代码中使用过XML数据到和元素行为，但你很可能是用过的条件注释。这些特性将永远从IE中消失。条件注释101</p>";
                content += "<p style=\"text-indent: 2em;\">";
                content += "要让你的网站或web应用在所有浏览器中都很好的工作是一件相当困难的事。特别是当你不得不支持旧版的IE浏览器时就变得尤为困难。";
                content += "IE6发布于2001年，IE7是2006年，IE8发布于2009年。不管你是如何看待微软的，但期望一个十年前的浏览器能和firefox5、";
                content += "Chrome12同样的来渲染web页面都是不合适的。";
                content += "Web开发人员尤其痛恨IE6。很多花费数个月构建的漂亮的网站或应用，经常在最后会发现IE6下的展示彻底崩溃。不过幸运的是：</p>";
                this.newsContent.Text = content;

                if (!string.IsNullOrEmpty(Request["news_id"]) && !string.IsNullOrEmpty(Request["customerId"]))
                {
                    var newsID = Request.QueryString["news_id"].Replace("'","");
                    //Response.Write(newsID);
                    var condition = new List<IWhereCondition> { 
                        new EqualsCondition() { FieldName = "NewsID", Value = newsID }
                    };
                    string customerId = Request["customerId"];
                    var loggingSessionInfo = GetBSLoggingSession(customerId, "1");
                    var data = new LNewsBLL(loggingSessionInfo).Query(condition.ToArray(), null).ToList();

                    if (data != null && data.Count > 0)
                    {
                        var newsEntity = data.FirstOrDefault();
                        this.newsTitle.Text = newsEntity.NewsTitle;
                        imageUrl = newsEntity.ImageUrl;
                        this.newsPublishTime.Text = newsEntity.PublishTime.Value.ToString("yyyy-MM-dd");
                        this.newsContent.Text = newsEntity.Content;
                    }
                }
            }
        }

        public static LoggingSessionInfo GetBSLoggingSession(string customerId, string userId)
        {
            if (userId == null || userId == string.Empty) userId = "1";
            string conn = GetCustomerConn(customerId);

            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            //loggingSessionInfo = new CLoggingSessionService().GetLoggingSessionInfo(customerId, "7d4cda48970b4ed0aa697d8c2c2e4af3");
            loggingSessionInfo.CurrentUser = new JIT.CPOS.BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = conn;

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }

        #region GetCustomerConn
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion
    }
}
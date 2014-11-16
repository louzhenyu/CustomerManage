using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility;

namespace JIT.CPOS.Web.WeiXin
{
    public partial class TextImageWapMobile : System.Web.UI.Page
    {
        string customerId = "29E11BDC6DAC439896958CC6866FF64E";
        public static string imageUrl = "";
        public static string title = "";
        public static string description = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    var id = Request.QueryString["id"].Replace("'", "");
                    if (!string.IsNullOrEmpty(Request.QueryString["customerId"]))
                    {
                        customerId = Request.QueryString["customerId"].Replace("'", "");
                    }
                    //Response.Write(newsID);
                    var condition = new List<IWhereCondition> { 
                        new EqualsCondition() { FieldName = "TextId", Value = id }
                    };
                    LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                    WMaterialTextBLL server = new WMaterialTextBLL(loggingSessionInfo);
                    var data = server.Query(condition.ToArray(), null).ToList();

                    if (data != null && data.Count > 0)
                    {
                        var newsEntity = data.FirstOrDefault();
                        this.newsTitle.Text = newsEntity.Title;
                        title = newsEntity.Title;
                        imageUrl = newsEntity.CoverImageUrl;
                        
                        this.newsPublishTime.Text = newsEntity.CreateTime.Value.ToString("yyyy-MM-dd");
                        this.newsContent.Text = newsEntity.Text;
                        newsPublishTime.Visible = false;
                        string result = System.Text.RegularExpressions.Regex.Replace(newsEntity.Text, @"<[^>]*>", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace(" ", "");

                        if (result.Length > 100)
                        {
                            description = HttpUtility.UrlEncode(result.Substring(0, 100));
                        }
                        else
                        {
                            description = HttpUtility.UrlEncode(result.Substring(0, result.Length));
                        }
                    }
                }
            }
        }
    }
}
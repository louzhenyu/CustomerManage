/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/29 11:05:54
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class VIPImageCommentBLL
    {
        #region 获取图文共享列表
        public string GetVIPImageCommentShowList(string ToUserName, string FromUserName)
        {
            string content = string.Empty;
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVIPImageCommentShowList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                IList<VIPImageCommentEntity> eventListInfo = new List<VIPImageCommentEntity>();
                eventListInfo = DataTableToObject.ConvertToList<VIPImageCommentEntity>(ds.Tables[0]);
                content = SetEventXML(ToUserName, FromUserName, eventListInfo);
            }
            return content;
        }

        private string SetEventXML(string ToUserName, string FromUserName, IList<VIPImageCommentEntity> EventList)
        {
            string host = System.Configuration.ConfigurationManager.AppSettings["website_url"];
            string xml = string.Empty;
            xml += "<xml>";
            xml += "<ToUserName>" + ToUserName + "</ToUserName>";
            xml += "<FromUserName>" + FromUserName + "</FromUserName>";
            xml += "<CreateTime>" + "1234567" +"</CreateTime>"; //EventList[0].CreateTime.ToString()
            xml += "<MsgType>" + "news" + "</MsgType>";
            xml += "<ArticleCount>" + EventList.Count + "</ArticleCount>";
            xml += "<Articles>";
            foreach (VIPImageCommentEntity eventInfo in EventList)
            {
                string hostx = host + "wap/wap2.html?id=" + eventInfo.VipImageCommentID + "&openID=" + ToUserName + "";
                xml += "<item>";
                xml += "<Title>" + eventInfo.Comment + " " + Convert.ToDateTime(eventInfo.CreateTime).ToString("yyyy-MM-dd") + "</Title>";//【上海】中欧校友高尔夫俱乐部5-6月份活动通知  2013-05-23 
                xml += "<Description>" + "" + "</Description>";//
                xml += "<PicUrl><![CDATA[" + eventInfo.ImageURL + "]]></PicUrl>";
                xml += "<Url><![CDATA[" + hostx + "]]></Url>";
                xml += "</item>";
            }
            xml += "</Articles>";
            xml += "<FuncFlag>1</FuncFlag>";
            xml += "</xml>";
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = "靓图欣赏:" + xml.ToString() });
            return xml;
        }
        #endregion
    }
}
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseService
    {

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        protected LoggingSessionInfo loggingSessionInfo { get; set; }

        /// <summary>
        /// 分离角色ID(角色Id,单位Id)
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        protected string GetBasicRoleId(string roleID)
        {
            string[] arr_role = roleID.Split(new char[] { ',' });
            return arr_role[0];
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDateTime()
        {
            return GetDateTime(DateTime.Now);
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 生成guid
        /// </summary>
        /// <returns></returns>
        protected string NewGuid()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string NewGuidPub()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "");
        }

        public decimal ToDecimal(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDecimal(obj);
        }

        #region 根据Customer_Id拼接成登录类
        /// <summary>
        /// 根据Customer_Id获取登录Model
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public LoggingSessionInfo GetLoggingSessionInfoByCustomerId(string Customer_Id)
        {
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            LoggingManager loggingManagerInfo = new LoggingManager();
            //获取数据库连接字符串
            //cPos.WebServices.AuthManagerWebServices.AuthService AuthWebService = new cPos.WebServices.AuthManagerWebServices.AuthService();
            //AuthWebService.Url = System.Configuration.ConfigurationManager.AppSettings["sso_url"] + "/authservice.asmx";
            //this.Log(LogLevel.DEBUG, "BS", "", "", "url", AuthWebService.Url);
            //this.Log(LogLevel.DEBUG, "BS", "", "", "customer_id", Customer_Id);
            //string str = AuthWebService.GetCustomerDBConnectionString(Customer_Id);//"0b3b4d8b8caa4c71a7c201f53699afcc"

            //loggingManagerInfo = (cPos.Model.LoggingManager)cXMLService.Deserialize(str, typeof(cPos.Model.LoggingManager));


            //loggingManagerInfo.Customer_Id = Customer_Id;
            //loggingSessionInfo.CurrentLoggingManager = loggingManagerInfo;
            return loggingSessionInfo;
        }

        #endregion

        /// <summary>
        /// 分离当前角色ID(角色Id,单位Id)
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <returns></returns>
        protected string GetBasicRoleId(LoggingSessionInfo loggingSession)
        {
            string[] arr_role = loggingSession.CurrentUserRole.RoleId.Split(new char[] { ',' });
            return arr_role[0];
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        //protected string GenInsertUnitTemporaryTableSQL(LoggingSessionInfo loggingSession,
        //    cPos.Model.Unit.UnitQueryCondition condition)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("declare @tmp_unit table(unit_id varchar(32));");
        //    sb.AppendLine("insert into @tmp_unit");
        //    //表
        //    //sb.AppendLine("select a2.unit_id from t_unit_level a1, t_unit_level a2 ");
        //    //视图
        //    sb.AppendLine("select a2.unit_id from vw_unit_level a1, vw_unit_level a2 ");
        //    if (condition.SuperUnitIDs.Count == 0)
        //    {
        //        sb.Append(",(select distinct unit_id from t_user_role where ");
        //        sb.Append(string.Format(" user_id='{0}' ", loggingSession.CurrentUser.User_Id));
        //        sb.AppendLine(string.Format(" and role_id='{0}') a3 ", this.GetBasicRoleId(loggingSession.CurrentUserRole.RoleId)));
        //        sb.AppendLine("where a3.unit_id=a1.unit_id ");
        //    }
        //    else
        //    {
        //        sb.Append(string.Format("where (a1.unit_id='{0}' ", condition.SuperUnitIDs[0]));
        //        for (int i = 1; i < condition.SuperUnitIDs.Count; i++)
        //        {
        //            sb.Append(string.Format("or a1.unit_id='{0}' ", condition.SuperUnitIDs[i]));
        //        }
        //        sb.AppendLine(")");

        //    }
        //    //表
        //    //sb.AppendLine(" and a2.path_unit_no like a1.path_unit_no + '%' ");
        //    //视图
        //    sb.AppendLine(" and a2.path_unit_id like a1.path_unit_id + '%' ");
        //    sb.AppendLine("group by a2.unit_id; ");

        //    return sb.ToString();
        //}

        /// <summary>
        /// 根据单位的查询条件，将符合条件的单位的ID插入到临时表中
        /// </summary>
        /// <param name="sqlMap">数据库连接</param>
        /// <param name="loggingSession">当前登录的信息</param>
        /// <param name="condition">单位的查询条件</param>
        /// <returns></returns>
        //protected void InsertUnitTemporaryTable(ISqlMapper sqlMap, LoggingSessionInfo loggingSession, 
        //    cPos.Model.Unit.UnitQueryCondition condition)
        //{
        //    sqlMap.QueryForObject("Pos.Operate.InsertUnitTemporaryTable", this.GenInsertUnitTemporaryTableSQL(loggingSession, condition));
        //}

        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="systemName">系统名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="functionName">方法名称</param>
        /// <param name="messageType">信息类型</param>
        /// <param name="message">信息</param>
        //protected void Log(LogLevel level, string systemName, String moduleName, String functionName, String messageType, string message)
        //{
        //    FileLogService log_service = new FileLogService();
        //    log_service.Log(level, systemName, moduleName, functionName, messageType, message);
        //}
        /// <summary>
        /// 根据域名获取IP地址（Hostname2ip("www.baidu.com")）
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static string Hostname2ip(string hostname)
        {
            try
            {
                IPAddress ip;
                if (IPAddress.TryParse(hostname, out ip))
                    return ip.ToString();
                else
                    return Dns.GetHostEntry(hostname).AddressList[0].ToString();
            }
            catch (Exception)
            {
                throw new Exception("IP Address Error");
            }
        }

        /// <summary>
        /// 将字符串中的特殊字符转换为html符号
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToHtml(string str)
        {
            if ((str == null) || str.Equals(""))
            {
                return str;
            }
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br/>");
            sb.Replace("\n", "<br/>");
            sb.Replace("\t", "\t");
            sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串中的html符号转换为特殊字符
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string ToTxt(string str)
        {
            if ((str == null) || str.Equals(""))
            {
                return str;
            }
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("&quot;", "'");
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br/>", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        /// <summary>
        /// 字符窜转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String unHtml(String s)
        {
            s = s.Replace("&amp;", "&");
            s = s.Replace("&nbsp;", " ");
            s = s.Replace("&#39;", "'");
            s = s.Replace("&lt;", "<");
            s = s.Replace("&gt;", ">");
            s = s.Replace("<br>", "\n");
            s = s.Replace("<br />", "\n");
            s = s.Replace("&quot;", "\"");
            s = s.Replace("&ldquo;", "“");
            s = s.Replace("&rdquo;", "”");
            s = s.Replace("?D", "—");
            return s;
        }

        public static string NoHTML(string Htmlstring) //去除HTML标记   
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("/r/n", "");
            //Htmlstring = System.Web. HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();   

            return Htmlstring;
        }

        /// <summary>
        /// 转换日期为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 将HTTP请求参数转换为字符串
        /// </summary>
        /// <param name="httpContext">HTTP上下文</param>
        /// <returns></returns>
        public string ConvertHttpContextToString(HttpContext httpContext)
        {
            BaseService.WriteLogWeixin("将HTTP请求参数转换为字符串");
            System.IO.Stream stream = httpContext.Request.InputStream;
            byte[] bt = new byte[stream.Length];
            stream.Read(bt, 0, (int)stream.Length);
            string postStr = System.Text.Encoding.UTF8.GetString(bt);

            BaseService.WriteLogWeixin("postStr:  " + postStr);

            return postStr;
        }

        /// <summary>
        /// 获取微信用户登录信息
        /// </summary>
        /// <param name="weixinID"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoggingSessionInfo GetWeixinLoggingSession(string weixinID)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSession();

            var server = new WMenuDAO(loggingSessionInfo);
            string conn = server.GetCustomerConn(weixinID);
            string customerId = server.GetCustomerID(weixinID);

            loggingSessionInfo.Conn = conn;
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;

            loggingSessionInfo.CurrentUser.customer_id = customerId;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;

            return loggingSessionInfo;
        }

        /// <summary>
        /// 获取默认登录信息
        /// </summary>
        /// <returns></returns>
        public static LoggingSessionInfo GetLoggingSession()
        {
            var userId = ConfigurationManager.AppSettings["user_id"].Trim();
            var customerId = ConfigurationManager.AppSettings["customer_id"].Trim();
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();

            loggingSessionInfo.CurrentUser = new BS.Entity.User.UserInfo();
            loggingSessionInfo.CurrentUser.User_Id = userId;
            loggingSessionInfo.CurrentUser.customer_id = customerId;

            loggingSessionInfo.UserID = loggingSessionInfo.CurrentUser.User_Id;
            loggingSessionInfo.ClientID = customerId;
            loggingSessionInfo.Conn = ConfigurationManager.AppSettings["Conn"].Trim();

            loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
            loggingSessionInfo.CurrentLoggingManager.Connection_String = loggingSessionInfo.Conn;
            loggingSessionInfo.CurrentLoggingManager.User_Id = userId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Id = customerId;
            loggingSessionInfo.CurrentLoggingManager.Customer_Name = "";
            loggingSessionInfo.CurrentLoggingManager.User_Name = "";
            return loggingSessionInfo;
        }

        #region 写日志

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="strMemo"></param>
        public static void WriteLog(string strMemo)
        {
            string date = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
            string filename = "C:/WEBHOME/logs/cpos/" + date + ".txt";

            if (!System.IO.Directory.Exists("C:/WEBHOME/logs/cpos/"))
                System.IO.Directory.CreateDirectory("C:/WEBHOME/logs/cpos/");

            System.IO.StreamWriter sr = null;

            try
            {
                if (!System.IO.File.Exists(filename))
                {
                    sr = System.IO.File.CreateText(filename);
                }
                else
                {
                    sr = System.IO.File.AppendText(filename);
                }

                sr.WriteLine("\n" + DateTime.Now.ToLocalTime().ToString() + " :  " + strMemo);
            }
            catch
            {

            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="strMemo"></param>
        public static void WriteLogWeixin(string strMemo)
        {
            var platform = "weixin";

            string date = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
            string filename = "C:/WEBHOME/logs/" + platform + "/" + date + ".txt";

            if (!System.IO.Directory.Exists("C:/WEBHOME/logs/" + platform + "/"))
                System.IO.Directory.CreateDirectory("C:/WEBHOME/logs/" + platform + "/");

            System.IO.StreamWriter sr = null;

            try
            {
                if (!System.IO.File.Exists(filename))
                {
                    sr = System.IO.File.CreateText(filename);
                }
                else
                {
                    sr = System.IO.File.AppendText(filename);
                }

                sr.WriteLine("\n" + DateTime.Now.ToLocalTime().ToString() + " :  " + strMemo);
            }
            catch
            {

            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        #endregion
    }
}

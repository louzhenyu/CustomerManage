using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace EMBAUnion.MsgSendApp
{
    public class Utils
    {
        #region GetDate/GetDateTime
        //public static string GetDate(object date)
        //{
        //    return GetDate(GetStrVal(date));
        //}

        public static string GetDate(string date)
        {
            if (date == null || date.Trim().Length < 8)
                return string.Empty;
            else
                return Convert.ToDateTime(date.Trim()).ToString("yyyy-MM-dd");
        }

        public static string GetDateTime(string time)
        {
            if (time == null || time.Trim().Length < 8)
                return string.Empty;
            else
                return Convert.ToDateTime(time.Trim()).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTime(DateTime time)
        {
            if (time == null)
                return string.Empty;
            else
                return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTime(object time)
        {
            if (time == null || time.ToString().Trim().Length < 8)
                return string.Empty;
            else
                return Convert.ToDateTime(time.ToString().Trim()).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetTodayString()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyyMMdd");
        }

        public static string GetNowString()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyyMMddHHmmssfff");
        }
        #endregion

        #region NewGuid
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
        }
        #endregion

        #region GetNow
        public static string GetNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetNowWithMillisecond()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        #endregion

    }
}

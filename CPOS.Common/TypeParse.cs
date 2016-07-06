using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPOS.Common
{
    /// <summary>
    /// 类型转换方法
    /// </summary>
    public class TypeParse
    {
        #region 转换为int类型转换
        /// <summary>
        /// to int helper
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        #endregion

        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public static double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public static decimal ToDecimal(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDecimal(obj);
        }

        public static float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }

        #region ToStr
        public static string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public static string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public static string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public static string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public static string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public static string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
    }
}

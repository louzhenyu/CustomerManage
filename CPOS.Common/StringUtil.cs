using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Drawing;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections;

//using XINLG.Labs.RegExp;
using System.Net;
using System.IO;

namespace JIT.CPOS.Common
{
    public class StringUtil
    {

        /// <summary>
        /// 判断字符串起始部分是否包含某个字符
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool StartsWith(string target, string lookfor)
        {
            if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(lookfor))
                return false;

            if (lookfor.Length > target.Length)
                return false;
            return (0 == string.Compare(target, 0, lookfor, 0, lookfor.Length, StringComparison.Ordinal));
        }


        /// <summary>
        /// 判断字符串起始部分是否包含某个字符 忽略大小写
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(string target, string lookfor)
        {
            if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(lookfor))
                return false;

            if (lookfor.Length > target.Length)
                return false;
            return (0 == string.Compare(target, 0, lookfor, 0, lookfor.Length, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 判断字符串结束部分是否包含某个字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lookfor"></param>
        /// <returns></returns>
        public static bool EndsWith(string text, char lookfor)
        {
            return (text.Length > 0 && text[text.Length - 1] == lookfor);
        }

        /// <summary>
        /// 判断字符串结束部分是否包含某个字符
        /// </summary>
        /// <param name="target"></param>
        /// <param name="lookfor"></param>
        /// <returns></returns>
        public static bool EndsWith(string target, string lookfor)
        {
            int indexA = target.Length - lookfor.Length;
            if (indexA < 0)
                return false;
            return (0 == string.Compare(target, indexA, lookfor, 0, lookfor.Length, StringComparison.Ordinal));
        }

        /// <summary>
        /// 判断字符串结束部分是否包含某个字符 忽略大小写
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(string target, string lookfor)
        {
            int indexA = target.Length - lookfor.Length;
            if (indexA < 0)
                return false;
            return (0 == string.Compare(target, indexA, lookfor, 0, lookfor.Length, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 判断某个字符在字符串中否存在
        /// </summary>
        /// <param name="target"></param>
        /// <param name="lookfor"></param>
        /// <returns></returns>
        public static bool Contains(string target, string lookfor)
        {
            if (target.Length < lookfor.Length)
                return false;

            return (0 <= target.IndexOf(lookfor));
        }

        /// <summary>
        /// 忽略大小写判断字符串是否包含某个字符
        /// </summary>
        /// <param name="target"></param>
        /// <param name="lookfor"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(string target, string lookfor)
        {
            if (target.Length < lookfor.Length)
                return false;

            return (0 <= target.IndexOf(lookfor, StringComparison.OrdinalIgnoreCase));
        }

        private static Encoding s_EncodingCache = null;
        /// <summary>
        /// 尝试获取GB2312编码并缓存起来，如果运行环境不支持GB2312编码，将缓存系统默认编码
        /// </summary>
        private static Encoding EncodingCache
        {
            get
            {
                if (s_EncodingCache == null)
                {

                    try
                    {
                        s_EncodingCache = Encoding.GetEncoding(936);

                    }
                    catch { }

                    if (s_EncodingCache == null)
                        s_EncodingCache = Encoding.UTF8;

                }

                return s_EncodingCache;
            }
        }

        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (length < 1)
                return text;

            byte[] buf = EncodingCache.GetBytes(text);

            if (buf.Length <= length)
            {
                return text;
            }

            int newLength = length;
            int[] numArray1 = new int[length];
            byte[] newBuf = null;
            int counter = 0;
            for (int i = 0; i < length; i++)
            {
                if (buf[i] > 0x7f)
                {
                    counter++;
                    if (counter == 3)
                    {
                        counter = 1;
                    }
                }
                else
                {
                    counter = 0;
                }
                numArray1[i] = counter;
            }

            if ((buf[length - 1] > 0x7f) && (numArray1[length - 1] == 1))
            {
                newLength = length + 1;
            }
            newBuf = new byte[newLength];
            Array.Copy(buf, newBuf, newLength);
            return EncodingCache.GetString(newBuf) + "...";

        }


        /// <summary>
        /// 字符串截取函数
        /// </summary>
        /// <param name="str">所要截取的字符串</param>
        /// <param name="num">截取字符串的长度</param>
        /// <param name="flg">true:加...,flase:不加</param>
        /// <returns></returns>
        public static string CutString(string str, int num, bool flg)
        {
            #region
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += str.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > num)
                    break;
            }
            //如果截过则加上半个省略号
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(str);
            if (mybyte.Length > num)
                if (flg)
                {
                    tempString += "...";
                }
            return tempString;
            #endregion
        }


        /// <summary>
        /// 如果内容只有换行和空格  返回空字符串   否则返回原内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Trim(string content)
        {
            if (string.IsNullOrEmpty(content) == false)
            {
                string tempContent = Regex.Replace(content, "(<br />)|(<br>)|(<br/>)|(&nbsp;)", string.Empty, RegexOptions.IgnoreCase);
                if (tempContent == string.Empty)
                    return string.Empty;
                else
                    return content;
            }

            return content;
        }



        /// <summary>
        /// 过滤非法字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatSafeText(string text)
        {
            string ffchar = "fuck|bitch|他妈的|法轮|falundafa|falun|操你妈|日本av|日B|三级片|Fa轮功|fa轮功|falun|日你|我日|suck|shit|法轮|我操|李宏治|李洪志|阴茎|傻B|妈的|操你|干你|日您|屁眼|国民党|台独|卖淫|流氓|999fuck|傻逼|阴道|阳痿|法輪|毛泽东|邓小平|江泽民|胡锦涛|周恩来|朱镕基|温家宝";

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            StringBuilder result = new StringBuilder(text);
            result.Replace("\"", "");
            result.Replace("<", "");
            result.Replace(">", "");
            result.Replace("-", "");
            result.Replace("'", "");
            result.Replace("%", "");

            foreach (string s in SplitString(ffchar, "|"))
                result.Replace(s, "");

            return result.ToString();
        }

        /// <summary>
        /// 将第一个字符转为大写，其它转小写
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatFirstCharToUpper(string text)
        {
            if (text.Length > 0)
            {
                text = text.ToLower();
                text = text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1);
                return text;
            }
            return text;
        }



        /// <summary>
        /// 过滤HTML内容里的脚本
        /// </summary>
        /// <param name="sourceHtml">HTML内容</param>
        /// <returns>返回过滤后的</returns>
        //public static string FilterScript(string sourceHtml)
        //{
        //    Regex scriptReg = new JavascriptRegex();
        //    sourceHtml = scriptReg.Replace(sourceHtml, string.Empty);
        //    return sourceHtml;
        //}


        /// <summary>
        /// 获取友好数据大小方式
        /// </summary>
        public static string FriendlyCapacitySize(long value)
        {
            if (value < 1024 * 5 && value % 1024 != 0)
            {
                return value + " B";
            }
            else if (value < 1024 * 5 && value % 1024 == 0)
            {
                return (value / 1024) + " KB";
            }
            else if (value >= 1024 * 5 && value < 1024 * 1024)
            {
                return (value / 1024) + " KB";
            }
            else if (value < 1024 * 1024 * 5 && value % (1024 * 1024) != 0)
            {
                return (value / 1024) + " KB";
            }
            else if (value < 1024 * 1024 * 5 && value % (1024 * 1024) == 0)
            {
                return (value / (1024 * 1024)) + " MB";
            }
            else if (value >= 1024 * 1024 * 5 && value < 1024 * 1024 * 1024)
            {
                return (value / (1024 * 1024)) + " MB";
            }
            else
            {
                return (value / (1024 * 1024 * 1024)) + " GB";
            }
        }

        /// <summary>
        /// 取得友好的时间显示
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string FormatSecond(DateTime dt)
        {
            string str;
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 0)
                str = span.Days.ToString() + "天";
            else
                str = string.Empty;

            if (span.Hours > 0)
                str += span.Hours.ToString() + "时";
            if (span.Minutes > 0)
                str += span.Minutes.ToString() + "分";
            if (span.Seconds > 0)
                str += span.Seconds.ToString() + "秒";

            return str;
        }


        /// <summary>
        /// 对字符串进行Html解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HtmlDecode(string content)
        {
            if(string.IsNullOrEmpty(content)) return null;
            return HttpUtility.HtmlDecode(content);
        }

        /// <summary>
        /// 对字符串进行Html编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HtmlEncode(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }


        /// <summary>
        /// 字符串转换成INT
        /// </summary>
        /// <param name="str">要转换成int的字符传</param>
        /// <returns>错误时返回0</returns>
        public static int GetStrToInt(string str)
        {
            #region
            try
            {
                if (str.Length == 0)
                    return 0;
                else
                {
                    if (Int32.Parse(str) == 0)
                        return 0;
                    else
                        return Int32.Parse(str);
                }
            }
            catch
            {
                return 0;
            }
            #endregion
        }


        /// <summary>
        /// 对时间进行格式化，如：2007-1-15,2007/5/2
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format">如：y-m-d；y/m/d；y-m-d h:mm:ss；m-d-y；m/d/y</param>
        /// <param name="spstr">分隔符号，如：-，/</param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dt, string format, string spstr)
        {
            #region
            string str = "";
            string y, m, d, h, mm, ss;
            y = dt.Year.ToString();
            m = dt.Month.ToString();
            if (m.Length < 2) m = "0" + m;
            d = dt.Day.ToString();
            if (d.Length < 2) d = "0" + d;
            h = dt.Hour.ToString();
            if (h.Length < 2) h = "0" + h;
            mm = dt.Minute.ToString();
            if (mm.Length < 2) mm = "0" + mm;
            ss = dt.Second.ToString();
            if (ss.Length < 2) ss = "0" + ss;

            if (format == "y-m-d")
            {
                str = y + spstr + m + spstr + d;
            }
            else if (format == "y-m-d h:mm:ss")
            {
                str = y + spstr + m + spstr + d + " " + h + ":" + mm + ":" + ss;
            }
            else if (format == "m-d-y")
            {
                str = m + spstr + d + spstr + y;
            }
            else if (format == "d-m-y")
            {
                str = d + spstr + m + spstr + y;
            }
            else
            {
                str = DateTime.Now.ToString();
            }
            return str;
            #endregion
        }



        // <summary>
        /// 将日期转换成人性化的显示
        /// </summary>
        /// <param name="date">当前时间</param>
        /// <returns></returns>
        public static string ToPrettyDate(string date)
        {
            try
            {
                DateTime dates = Convert.ToDateTime(date);
                var timeSince = DateTime.Now.Subtract(dates);
                if (timeSince.TotalMilliseconds < 1) return "还未到";
                if (timeSince.TotalMinutes < 1) return "刚才";
                if (timeSince.TotalMinutes < 2) return "1 分钟前";
                if (timeSince.TotalMinutes < 60) return string.Format("{0} 分钟前", timeSince.Minutes);
                if (timeSince.TotalMinutes < 120) return "1 小时前";
                if (timeSince.TotalHours < 24) return string.Format("{0} 小时前", timeSince.Hours);
                if (timeSince.TotalDays == 1) return "昨天";
                if (timeSince.TotalDays < 7) return string.Format("{0} 天前", timeSince.Days);
                if (timeSince.TotalDays < 14) return "上周";
                if (timeSince.TotalDays < 21) return "2 周前";
                if (timeSince.TotalDays < 28) return "3 周前";
                if (timeSince.TotalDays < 60) return "上个月前";
                if (timeSince.TotalDays < 365) return string.Format("{0} 个月前", Math.Round(timeSince.TotalDays / 30));
                if (timeSince.TotalDays < 730) return "去年";
                return string.Format("{0} 年前", Math.Round(timeSince.TotalDays / 365));
            }
            catch
            {
                return date;
            }
        }



        /// <summary>
        /// 根据自定义长度生成随机字符串 字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomStr(int length)
        {
            string baseString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(baseString.Length);
                char c = baseString[number];
                key.Append(c);
            }
            return key.ToString();
        }
        /// <summary>
        /// 随机大写字幕
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomUperStr(int length)
        {
            string baseString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random(System.Guid.NewGuid().GetHashCode());
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(baseString.Length);
                char c = baseString[number];
                key.Append(c);
            }
            return key.ToString();
        }
        /// <summary>
        /// 根据自定义长度生成随机字符串 字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomStrInt(int length)
        {
            string baseString = "0123456789";
            Random random = new Random(System.Guid.NewGuid().GetHashCode());
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(baseString.Length);
                char c = baseString[number];
                key.Append(c);
            }
            return key.ToString();
        }



        /// <summary>
        /// 返回一个随机整数
        /// </summary>
        /// <param name="Min"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        public static int GetRandomInt(int Min, int Max)
        {
            int rndNum;
            Random rnd2 = new Random();
            rndNum = rnd2.Next(Min, Max);
            return rndNum;
        }

        /// <summary>
        /// 返回随机整数列表 count必须小于等于maxNumber
        /// </summary>
        /// <param name="maxNumber"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<int> GetRandomArray(int maxNumber, int count)
        {
            List<int> list = new List<int>();//保存取出的随机数    
            int[] array = new int[maxNumber];//定义初始数组    
            for (int i = 0; i < maxNumber; i++)//给数组元素赋值    
                array[i] = i + 1;
            Random rnd = new Random();
            for (int j = 0; j < count; j++)
            {
                int index = rnd.Next(j, maxNumber);//生成一个随机数，作为数组下标    
                int temp = array[index];//从数组中取出index为下标的数    
                list.Add(temp);//将取出的数添加到list中    
                array[index] = array[j];//将下标为j的数交换到index位置    
                array[j] = temp;//将取出的数交换到j的位置    
            }
            return list;
        }

        /// <summary>
        /// 获取日期形式随机数
        /// </summary>
        /// <returns></returns>
        public static string GetRandomDatetimeStr()
        {
            DateTime date = DateTime.Now;
            int y = Convert.ToInt32(date.Year) + 1900;
            int m = Convert.ToInt32(date.Month) + 1;
            int d = Convert.ToInt32(date.Day);
            string dateStr = "" + date.Year + date.Month + date.Day + date.Hour + date.Minute + date.Second + date.Millisecond;
            dateStr += GetRandomStrInt(5);
            return dateStr;
        }



        /// <summary>
        /// 格式化money数据 保留两位小数
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string FormatMoney(decimal money)
        {
            return money.ToString("0.00");
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// 获取某一个字符在字符串中最后一次出现的末尾字符
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="LookFor"></param>
        /// <returns></returns>
        public static string GetStringEndString(string strContent, string LookFor)
        {
            int i = strContent.LastIndexOf(LookFor);
            if (i != -1)
            {
                return strContent.Substring(i, strContent.Length - i);
            }
            return null;
        }

        /// <summary>
        /// 将数据转换成字符串
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="SplitStr"></param>
        /// <returns></returns>
        public static string FormatArrayToString(string[] Array, string SplitStr)
        {
            string str = "";
            foreach (string s in Array)
            {
                str += s + SplitStr;
            }
            return str.Substring(0, str.Length - SplitStr.Length);
        }



        /// <summary>
        /// 清除HTML标记
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string NoHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring))
                return null;

            Htmlstring = HttpUtility.UrlDecode(Htmlstring);

            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            //Htmlstring.Replace("<", "");
            //Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");


            return Htmlstring;
        }

        /// <summary>
        /// 获取某个字符在字串中出现在的次数
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static int ContainCount(string input, char value, bool ignoreCase)
        {
            if (ignoreCase)
            {
                input = input.ToLower();
                if (Char.IsUpper(value))
                {
                    value = Char.ToLower(value);
                }
            }
            int count = 0;
            for (int i = 0; (i = input.IndexOf(value, i)) >= 0; i++)
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// 获取bytes
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static byte[] GetBytes(WebResponse response)
        {
            var length = (int)response.ContentLength;
            byte[] data;

            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[0x100];

                using (var rs = response.GetResponseStream())
                {
                    for (var i = rs.Read(buffer, 0, buffer.Length); i > 0; i = rs.Read(buffer, 0, buffer.Length))
                    {
                        memoryStream.Write(buffer, 0, i);
                    }
                }

                data = memoryStream.ToArray();
            }

            return data;
        }

        /// <summary>
        /// byte 转sbyte
        /// </summary>
        /// <param name="myByte"></param>
        /// <returns></returns>
        public static sbyte[] byteToSbyte(byte[] myByte)
        {
            sbyte[] mySByte = new sbyte[myByte.Length];

            for (int i = 0; i < myByte.Length; i++)
            {
                if (myByte[i] > 127)
                    mySByte[i] = (sbyte)(myByte[i] - 256);
                else
                    mySByte[i] = (sbyte)myByte[i];
            }
            return mySByte;
        }


        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns>
        public static string gethtmlContent(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }


        public static string CovertIntToChineseNum(int Num)
        {
            string str = Num.ToString();
            str = str.Replace("10", "十");
            str = str.Replace("11", "十一");
            str = str.Replace("12", "十二");
            str = str.Replace("1", "一");
            str = str.Replace("2", "二");
            str = str.Replace("3", "三");
            str = str.Replace("4", "四");
            str = str.Replace("5", "五");
            str = str.Replace("6", "六");
            str = str.Replace("7", "七");
            str = str.Replace("8", "八");
            str = str.Replace("9", "九");

            return str;

        }

        /// <summary>
        /// 隐藏手机中间的数字
        /// </summary>
        /// <param name="MobileNumber"></param>
        /// <returns></returns>
        public static string HiddenMobileMiddleNumber(string mobileNumber)
        {
            if (!string.IsNullOrEmpty(mobileNumber) && mobileNumber.Length == 11)
            {
                string newModbileNumber = mobileNumber.Substring(0, 3) + "****";
                newModbileNumber += mobileNumber.Substring(mobileNumber.Length - 3, 3);
                return newModbileNumber;
            }
            return mobileNumber;
        }


        /// <summary>
        /// 隐藏邮箱中间的字符
        /// </summary>
        /// <param name="MobileNumber"></param>
        /// <returns></returns>
        //public static string HiddenEmailMiddleStr(string email)
        //{
        //    if (!string.IsNullOrEmpty(email) && Utils.ValidateUtil.IsEmail(email))
        //    {
        //        string newEmail = email.Substring(0, 2) + "****";
        //        newEmail += email.Substring(email.LastIndexOf("@") - 1, email.Length - email.LastIndexOf("@") + 1);
        //        return newEmail;
        //    }
        //    return email;
        //}


        private const double EARTH_RADIUS = 6378.137; //地球半径
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }
    }
}
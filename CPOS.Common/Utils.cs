using Aspose.Cells;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using ThoughtWorks.QRCode.Codec;
using System.Globalization;
using System.Drawing.Drawing2D;

namespace JIT.CPOS.Common
{
    public class Utils
    {
        #region GetDate/GetDateTime
        public static string GetDate(object date)
        {
            return GetDate(GetStrVal(date));
        }

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

        #region AppendPropNode
        public static XmlNode AppendPropNode(XmlDocument doc, XmlNode parent, string name, object value)
        {
            if (value == null)
                value = string.Empty;
            else if (value.GetType() == typeof(DateTime))
                value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");

            XmlNode propNode = doc.CreateElement(name);
            propNode.InnerText = value.ToString();
            parent.AppendChild(propNode);
            return propNode;
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

        #region GetFieldVal
        public static string GetFieldVal(object val)
        {
            if (val == null) return "null";
            return string.Format("'{0}'", val.ToString());
        }

        public static string GetFieldVal(object val, int maxLength)
        {
            if (val == null) return "null";
            if (val.ToString().Length > maxLength)
                return string.Format("'{0}'", val.ToString().Substring(0, maxLength));
            return string.Format("'{0}'", val.ToString());
        }
        #endregion

        #region SaveFile
        public static void SaveFile(string folderPath, string fileName, string content)
        {
            if (folderPath == null || folderPath.Length <= 0) return;
            if (fileName == null || fileName.Length <= 0) return;

            folderPath = folderPath.Trim();
            fileName = fileName.Trim().Replace("-", "_").Replace(" ", "_").Replace(":", "_");

            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            if (!folderPath.EndsWith(@"\"))
                folderPath = folderPath + @"\";

            string filePath = folderPath + fileName;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine(content);
            }
        }
        #endregion

        #region GetFilePath
        public static string GetFilePath(string folder, string name)
        {
            if (!folder.EndsWith(@"\")) folder = folder + @"\";
            return folder + name;
        }

        public static string GetFilePath(string folder, string name, bool appendDate)
        {
            if (!folder.EndsWith(@"\")) folder = folder + @"\";
            return folder + GetTodayString() + @"\" + name;
        }
        #endregion

        #region CheckTimeScope
        public static bool CheckTimeScope(string time, double scope)
        {
            time = time.Trim();
            DateTime now = DateTime.Now;
            if (time.Length == 5)
            {
                int th = int.Parse(time.Substring(0, 2));
                int tm = int.Parse(time.Substring(3, 2));
                DateTime dtTime = new DateTime(now.Year, now.Month, now.Day, th, tm, 0);
                DateTime dtEnd = dtTime.AddSeconds(scope);
                if (dtTime <= now && now <= dtEnd)
                    return true;
            }
            return false;
        }
        #endregion

        #region GetChineseCaps
        /// <summary>
        /// 拼音助记码
        /// </summary>
        public static string GetChineseCaps(string chineseStr)
        {
            if (chineseStr.Trim().Length == 0) return string.Empty;
            byte[] ZW = new byte[2];
            long ChineseStr_int;
            string Capstr = "", CharStr, ChinaStr = "";
            for (int i = 0; i <= chineseStr.Length - 1; i++)
            {
                CharStr = chineseStr.Substring(i, 1).ToString();
                ZW = System.Text.Encoding.Default.GetBytes(CharStr);
                // 得到汉字符的字节数组   
                if (ZW.Length == 2)
                {
                    int i1 = (short)(ZW[0]);
                    int i2 = (short)(ZW[1]);
                    ChineseStr_int = i1 * 256 + i2;
                    //table   of   the   constant   list   
                    //   'A';   //45217..45252   
                    //   'B';   //45253..45760   
                    //   'C';   //45761..46317   
                    //   'D';   //46318..46825   
                    //   'E';   //46826..47009   
                    //   'F';   //47010..47296   
                    //   'G';   //47297..47613   

                    //   'H';   //47614..48118   
                    //   'J';   //48119..49061   
                    //   'K';   //49062..49323   
                    //   'L';   //49324..49895   
                    //   'M';   //49896..50370   
                    //   'N';   //50371..50613   
                    //   'O';   //50614..50621   
                    //   'P';   //50622..50905   
                    //   'Q';   //50906..51386   

                    //   'R';   //51387..51445   
                    //   'S';   //51446..52217   
                    //   'T';   //52218..52697   
                    //没有U,V   
                    //   'W';   //52698..52979   
                    //   'X';   //52980..53640   
                    //   'Y';   //53689..54480   
                    //   'Z';   //54481..55289   

                    if ((ChineseStr_int >= 45217) && (ChineseStr_int <= 45252))
                    {
                        ChinaStr = "A";
                    }
                    else if ((ChineseStr_int >= 45253) && (ChineseStr_int <= 45760))
                    {
                        ChinaStr = "B";
                    }
                    else if ((ChineseStr_int >= 45761) && (ChineseStr_int <= 46317))
                    {
                        ChinaStr = "C";
                    }
                    else if ((ChineseStr_int >= 46318) && (ChineseStr_int <= 46825))
                    {
                        ChinaStr = "D";
                    }
                    else if ((ChineseStr_int >= 46826) && (ChineseStr_int <= 47009))
                    {
                        ChinaStr = "E";
                    }
                    else if ((ChineseStr_int >= 47010) && (ChineseStr_int <= 47296))
                    {
                        ChinaStr = "F";
                    }
                    else if ((ChineseStr_int >= 47297) && (ChineseStr_int <= 47613))
                    {
                        ChinaStr = "G";
                    }
                    else if ((ChineseStr_int >= 47614) && (ChineseStr_int <= 48118))
                    {
                        ChinaStr = "H";
                    }
                    else if ((ChineseStr_int >= 48119) && (ChineseStr_int <= 49061))
                    {
                        ChinaStr = "J";
                    }
                    else if ((ChineseStr_int >= 49062) && (ChineseStr_int <= 49323))
                    {
                        ChinaStr = "K";
                    }
                    else if ((ChineseStr_int >= 49324) && (ChineseStr_int <= 49895))
                    {
                        ChinaStr = "L";
                    }
                    else if ((ChineseStr_int >= 49896) && (ChineseStr_int <= 50370))
                    {
                        ChinaStr = "M";
                    }
                    else if ((ChineseStr_int >= 50371) && (ChineseStr_int <= 50613))
                    {
                        ChinaStr = "N";
                    }
                    else if ((ChineseStr_int >= 50614) && (ChineseStr_int <= 50621))
                    {
                        ChinaStr = "O";
                    }
                    else if ((ChineseStr_int >= 50622) && (ChineseStr_int <= 50905))
                    {
                        ChinaStr = "P";
                    }
                    else if ((ChineseStr_int >= 50906) && (ChineseStr_int <= 51386))
                    {
                        ChinaStr = "Q";
                    }
                    else if ((ChineseStr_int >= 51387) && (ChineseStr_int <= 51445))
                    {
                        ChinaStr = "R";
                    }
                    else if ((ChineseStr_int >= 51446) && (ChineseStr_int <= 52217))
                    {
                        ChinaStr = "S";
                    }
                    else if ((ChineseStr_int >= 52218) && (ChineseStr_int <= 52697))
                    {
                        ChinaStr = "T";
                    }
                    else if ((ChineseStr_int >= 52698) && (ChineseStr_int <= 52979))
                    {
                        ChinaStr = "W";
                    }
                    else if ((ChineseStr_int >= 52980) && (ChineseStr_int <= 53640))
                    {
                        ChinaStr = "X";
                    }
                    else if ((ChineseStr_int >= 53689) && (ChineseStr_int <= 54480))
                    {
                        ChinaStr = "Y";
                    }
                    else if ((ChineseStr_int >= 54481) && (ChineseStr_int <= 55289))
                    {
                        ChinaStr = "Z";
                    }
                }
                else if (CharStr == "1")
                {
                    ChinaStr = "Y";
                }
                else if (CharStr == "2")
                {
                    ChinaStr = "E";
                }
                else if (CharStr == "3")
                {
                    ChinaStr = "S";
                }
                else if (CharStr == "4")
                {
                    ChinaStr = "S";
                }
                else if (CharStr == "5")
                {
                    ChinaStr = "W";
                }
                else if (CharStr == "6")
                {
                    ChinaStr = "L";
                }
                else if (CharStr == "7")
                {
                    ChinaStr = "Q";
                }
                else if (CharStr == "8")
                {
                    ChinaStr = "B";
                }
                else if (CharStr == "9")
                {
                    ChinaStr = "J";
                }
                else if (CharStr == "0")
                {
                    ChinaStr = "L";
                }
                else
                {
                    ChinaStr = CharStr;
                }
                //else
                //{   
                //    Capstr = ChineseStr;
                //    break;
                //}
                Capstr = Capstr + ChinaStr;
            }
            return Capstr.Trim();
        }
        #endregion

        #region IsEmpty
        public static bool IsEmpty(object obj)
        {
            if (obj == null) return true;
            if (obj.GetType() == typeof(string))
            {
                if (obj.ToString().Trim().Length == 0) return true;
            }
            else if (obj.GetType() == typeof(int))
            {
                if (obj.ToString().Trim().Length == 0) return true;
            }
            else
            {
                if (obj.ToString().Trim().Length == 0) return true;
            }
            return false;
        }
        #endregion

        #region CheckUrl
        public static bool CheckUrl(string url)
        {
            if (url == null) return false;
            if (url.Trim().Length <= 4) return false;
            if (!url.StartsWith("http")) return false;
            return true;
        }
        #endregion

        #region CheckIsNumber
        public static bool CheckIsNumber(string num)
        {
            try
            {
                int n = int.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ToAbs
        public static decimal ToAbs(object value)
        {
            return Math.Abs(decimal.Parse(value.ToString()));
        }
        #endregion

        #region GetIP
        public static string GetIP(string url)
        {
            string url2 = url;
            int index = url2.LastIndexOf(":");
            if (index > 6) url2 = url2.Substring(0, index);
            return url2.Replace("https://", string.Empty)
                .Replace("http://", string.Empty)
                .Replace("/", string.Empty)
                .Replace(":", string.Empty);
        }
        #endregion

        #region GetStrVal
        public static string GetStrVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
        }
        #endregion

        #region GetDecimalVal
        public static decimal GetDecimalVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? 0 : decimal.Parse(obj.ToString());
        }
        #endregion

        #region GetIntVal
        public static int GetIntVal(object obj)
        {
            return obj == DBNull.Value || obj == null || obj.ToString() == string.Empty ? 
                0 : int.Parse(obj.ToString());
        }
        #endregion

        #region GetDateTimeVal
        public static DateTime GetDateTimeVal(object obj)
        {
            return obj == DBNull.Value || obj == null ? new DateTime(1900, 1, 1) : DateTime.Parse(obj.ToString());
        }
        #endregion

        #region GetStatus
        /// <summary>
        /// 获取状态
        /// </summary>
        public static string GetStatus(bool status)
        {
            return status.ToString().ToLower();
        }
        #endregion

        #region GetLimitedStr
        /// <summary>
        /// 获取受限制长度字符串
        /// </summary>
        public static string GetLimitedStr(string src, int maxLength)
        {
            if (src != null && src.Length > maxLength) return src.Substring(0, maxLength);
            return src;
        }
        #endregion

        #region GetRemoteData
        public static string GetRemoteData(string uri, string method, string content)
        {
            string respData = "";
            method = method.ToUpper();
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = method.ToUpper();
            req.Credentials = System.Net.CredentialCache.DefaultCredentials;
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            if (method == "POST")
            {
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                req.ContentLength = buffer.Length;
                //req.ContentType = "text/json";
                Stream postStream = req.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
                postStream.Close();
            }
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            respData = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            resp.Close();
            return respData;
        }

        internal class AcceptAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public AcceptAllCertificatePolicy()
            { }

            public bool CheckValidationResult(ServicePoint sPoint,
                System.Security.Cryptography.X509Certificates.X509Certificate cert,
                WebRequest wRequest, int certProb)
            {
                return true;
            }
        }
        #endregion

        #region SMSSend Obsoleted
        public static bool SMSSend(string mobileNO, string SMSContent)
        {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            string smsContentEncode = HttpUtility.UrlEncode(SMSContent, gb2312);
            string uri = string.Format("http://www.jitmarketing.cn/SendSMS.asp?code=jit2010sms&mobilelist={0}&content={1}",
                mobileNO, smsContentEncode);
            string method = "GET";
            string cotent = "";
            string data = CPOS.Common.Utils.GetRemoteData(uri, method, cotent);
            return true;
        }
        #endregion


        #region SMSSend Obsoleted
        public static string SMSSendOrder(string mobileNO, string SMSContent)
        {
            var para = new { MobileNO = mobileNO, SMSContent};
            var request = new { Action = "SendMessage", Parameters = para };
            string str = string.Format("request={0}", request.ToJSON());

           string url = ConfigurationManager.AppSettings["SMSURL"];
            if(string.IsNullOrEmpty(url))//用来测试
            {
                url = @"http://www.jitmarketing.cn:10001/Geteway.ashx";
            }
            var res = HttpClient.PostQueryString(url, str);

            return res;
        }
        #endregion


        #region Jermyn 取整数部分
        /// <summary>
        /// 取整数部分
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetParseInt(object obj)
        {
            if (obj == DBNull.Value || obj == null || obj.ToString().Equals(""))
            {
                return 0;
            }
            else { 
                string returnvalue = "";
                string s = obj.ToString();
                int i = s.IndexOf( "." );
                if ( i > 0 )
                {
                    returnvalue = s.Substring( 0,i );
                }
                else
                {
                    returnvalue = s;
                }
                return int.Parse( returnvalue );
            }
        }
        #endregion

        #region OutputExcel
        public static void OutputExcel(HttpContext p, string filePath)
        {
            p.Response.Clear();
            p.Response.Buffer = true;
            p.Response.Charset = "GB2312";
            p.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
            p.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
            p.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。          
            p.Response.WriteFile(filePath);
            p.ApplicationInstance.CompleteRequest();
        }
        #endregion

        #region DataTableToExcel
        public static string DataTableToExcel(DataTable dataTable, string folderName, string fileNamePre, string postOrGet = "Get")
        {
            //数据获取
            Workbook wb = DataTableExporter.WriteXLS(dataTable, 0);
            string savePath = HttpContext.Current.Server.MapPath(@"~/File/Excel/" + folderName);
            if (!System.IO.Directory.Exists(savePath))
                System.IO.Directory.CreateDirectory(savePath);

            string fileName = fileNamePre + DateTime.Now.ToFileTime() + ".xls";
            wb.Save(savePath + "\\" + fileName);//保存Excel文件

            if (postOrGet.ToLower() == "get")
            {
                OutputExcel(HttpContext.Current, savePath);
                HttpContext.Current.Response.End();
            }

            return @"/File/Excel/" + folderName + "/" + fileName;
        }
        #endregion

        #region 生成二维码
        public static string GenerateQRCode(string info, string domain, string sourcePath, string targetPath)
        {
            #region 处理图片
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            System.Drawing.Image qrImage = qrCodeEncoder.Encode(info, Encoding.UTF8);
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(256, 256);
            System.Drawing.Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
            g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g2.Clear(System.Drawing.Color.Transparent);
            g2.DrawImage(qrImage, new System.Drawing.Rectangle(0, 0, 256, 256),
                new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);
            string fileName = System.Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            string host = domain;
            if (!host.EndsWith("/")) host += "/";
            string fileUrl = host + "QRCodeImage/" + fileName;
            string newFilePath = string.Empty;
            string newFilename = string.Empty;
            System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(sourcePath);
            System.Drawing.Image imgWarter = bitmap;
            using (Graphics g = Graphics.FromImage(imgSrc))
            {
                g.DrawImage(imgWarter, new Rectangle(0, 0, imgWarter.Width, imgWarter.Height),
                    0, 0, imgWarter.Width, imgWarter.Height, GraphicsUnit.Pixel);
            }
            imgSrc.Save(targetPath + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            imgWarter.Dispose();
            imgSrc.Dispose();
            qrImage.Dispose();
            bitmap.Dispose();
            g2.Dispose();

            return fileUrl;
            #endregion
        }


        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage(Image imgBack, string destImg)
        {
            Image img = Image.FromFile(destImg);        //照片图片      
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }


        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }  
        #endregion

        #region SendSMSCode,发送验证码
        public static bool SendSMSCode(string customerId, string mobile, string code, string sign, out string msg)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["SMSURL"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("未配置短信服务URL");
                var para = new { MobileNO = mobile, SMSContent = string.Format(@"您的验证码是：{0}", code), Sign = sign };
                var request = new { Action = "SendMessage", Parameters = para };
                string str = string.Format("request={0}", request.ToJSON());

                Loggers.Debug(new DebugLogInfo() { Message = "发送短信:" + str });
                var res = HttpClient.PostQueryString(url, str);
                var response = res.DeserializeJSONTo<Response>();
                Loggers.Debug(new DebugLogInfo() { Message = "收到返回信息:" + response.ToJSON() });

                if (response.ResultCode < 100)
                {
                    msg = "短信发送成功";
                    return true;
                }
                else
                {
                    msg = "短信发送失败:" + response.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                msg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 加密解密
        /// <summary>
        /// 获取加密密钥
        /// </summary>
        /// <param name="pOriginalKey">原始密钥</param>
        /// <returns></returns>
        public static string GetEncryptKey(string pOriginalKey)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] val, hash;
            val = Encoding.UTF8.GetBytes(pOriginalKey);
            hash = md5.ComputeHash(val);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToLower().Substring(0, 24);
        }

        #region 3DES加密解密
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="pKey">加密密钥</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(string pKey, string pEncryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);
            byte[] contentBytes = Encoding.UTF8.GetBytes(pEncryptContent);
            return TripleDESEncrypt(keyBytes, contentBytes);
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="pKey">加密密钥</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(string pKey, byte[] pEncryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);
            return TripleDESEncrypt(keyBytes, pEncryptContent);
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="pKey">加密密钥</param>
        /// <param name="pEncryptContent">需要加密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(byte[] pKey, byte[] pEncryptContent)
        {
            byte[] ivBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            using (var mStream = new MemoryStream())
            {
                using (var cStream = new CryptoStream(mStream, tdsp.CreateEncryptor(pKey, ivBytes), CryptoStreamMode.Write))
                {
                    cStream.Write(pEncryptContent, 0, pEncryptContent.Length);
                    cStream.FlushFinalBlock();

                    byte[] encryptedBytes = mStream.ToArray();
                    return encryptedBytes;
                }
            }
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="pKey">解密密钥</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(string pKey, string pDecryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);
            byte[] contentBytes = Encoding.UTF8.GetBytes(pDecryptContent);

            return TripleDESDecrypt(keyBytes, contentBytes);
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="pKey">解密密钥</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(string pKey, byte[] pDecryptContent)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(pKey);

            return TripleDESDecrypt(keyBytes, pDecryptContent);
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="pKey">解密密钥</param>
        /// <param name="pDecryptContent">需要解密的内容</param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(byte[] pKey, byte[] pDecryptContent)
        {
            byte[] ivBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            using (var dStream = new MemoryStream(pDecryptContent))
            {
                using (var cStream = new CryptoStream(dStream, tdsp.CreateDecryptor(pKey, ivBytes), CryptoStreamMode.Read))
                {
                    byte[] result = new byte[pDecryptContent.Length];
                    cStream.Read(result, 0, result.Length);
                    return result;
                }
            }
        }
        #endregion

        #endregion

        #region 判断DataSet是否有效
        public static bool IsDataSetValid(DataSet dataSet)
        {
            return dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0;
        }
        #endregion

        #region List转DataTable
        /// <summary>  
        /// 将集合类转换成DataTable  
        /// </summary>  
        /// <param name="list">集合</param>  
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            DataTable result = new DataTable();
            PropertyInfo[] propertys = typeof(T).GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                Type colType = pi.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                result.Columns.Add(new DataColumn(pi.Name, colType));
            }
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        } 

        #endregion

        #region 保存上传附件
        public static string SaveUploadAttachment(HttpFileCollection files, string customerID, string userID, out string errorMessage)
        {
            errorMessage = "";
            //string filetype = "";
            string fileName = "";
            string fullFileName = "";

            if (files.Count > 0)
            {
                HttpPostedFile postedFile = files[0];
                if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                {
                    fileName = postedFile.FileName;
                    string suffixname = "";
                    if (fileName != null)
                    {
                        suffixname = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
                    }

                    if (suffixname != ".xls" && suffixname != ".xlsx")
                    {
                        errorMessage = "只能上传后缀名为 xls 或 xlsx 的文件!";
                        return "";
                    }

                    string tempPath = "/File/Excel/" + customerID + "/" + userID + "/";
                    fullFileName = DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss") + suffixname;
                    string savepath = HttpContext.Current.Server.MapPath(tempPath);
                    if (!Directory.Exists(savepath))
                    {
                        Directory.CreateDirectory(savepath);
                    }
                    postedFile.SaveAs(savepath + @"/" + fullFileName);//保存

                    fullFileName = savepath + fullFileName;
                }
                else
                {
                    errorMessage = "请选择文件!";
                }
            }
            else
                errorMessage = "请选择文件!";

            return fullFileName;
        }
        #endregion

        #region 通用表参数
        public static DataTable TableParameterCommon
        {
            get
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Column3", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Column4", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Column5", typeof(string)));

                return dataTable;
            }
        }
        #endregion
    }

    public class Response
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public object ResData { get; set; }
    }
}

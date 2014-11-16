using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;

namespace JIT.CPOS.Common
{
    public class DownloadImage
    {
        #region 图片转换
        public string DownloadFile(string address,string DownloadUrl)
        {

            try
            {
                if (DownloadUrl == null || DownloadUrl.Equals(""))
                {
                    DownloadUrl = "http://o2oapi.aladingyidong.com";
                }
                string host = DownloadUrl + "/HeadImage/";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                WebClient webClient = new WebClient();

                //创建下载根文件夹
                //var dirPath = @"C:\DownloadFile\";
                var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "HeadImage\\";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //根据年月日创建下载子文件夹
                var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + @"\";
                host += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //下载到本地文件
                var fileExt = Path.GetExtension(address).ToLower();
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt+".jpg";
                var filePath = dirPath + newFileName;
                host += newFileName;
                webClient.DownloadFile(address, filePath);


                return host;
            }
            catch (Exception ex)
            {
              //  BaseService.WriteLogWeixin("图片下载异常信息：  " + ex.Message);
                return string.Empty;
            }
        }
        #endregion
    }
}

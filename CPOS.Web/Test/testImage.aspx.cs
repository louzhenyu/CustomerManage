using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Linq;
using JIT.CPOS.BS.BLL.WX.Enum;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.WX;
using JIT.Utility.ExtensionMethod;
using System.Web;
using System.Configuration;
using System.Collections;

namespace JIT.CPOS.Web.Test
{
    public partial class testImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string str11 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //string str12 = System.Environment.CurrentDirectory;
            //string str13 = System.IO.Directory.GetCurrentDirectory();
            //string str14 = System.AppDomain.CurrentDomain.BaseDirectory;
            //string str15 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //string str2 = Environment.CurrentDirectory;
            //string str3 = Directory.GetCurrentDirectory();
            //string str4 = AppDomain.CurrentDomain.BaseDirectory;
            //string str7 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //Response.Write("System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName:" + str11);
            //Response.Write("<br><br>");
            //Response.Write("System.Environment.CurrentDirectory:" + str12);
            //Response.Write("<br><br>");
            //Response.Write("System.IO.Directory.GetCurrentDirectory():" + str13);
            //Response.Write("<br/><br>");
            //Response.Write("System.AppDomain.CurrentDomain.BaseDirectory:" + str14);
            //Response.Write("<br/><br>");
            //Response.Write("System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase:" + str15);
            //Response.Write("<br/><br>");
            //Response.Write("Environment.CurrentDirectory:" + str2);
            //Response.Write("<br/><br>");
            //Response.Write("Directory.GetCurrentDirectory():" + str3);
            //Response.Write("<br/><br>");
            //Response.Write("AppDomain.CurrentDomain.BaseDirectory:" + str4);
            //Response.Write("<br/><br>");
            //Response.Write("AppDomain.CurrentDomain.SetupInformation.ApplicationBase:" + str7);
            //Response.Write("<br/><br>");

            //DownloadFile("http://wx.qlogo.cn/mmopen/ZtTia7gTBL1X8qUWjzslA79x1Z63zHIeaBst51lazljriaxzwa4cUib0kDEYWv3cMvb6TGEKObZKhb9VzXU1UwsNZDhjUvCeibP4/0");
            testGetImageText();
        }

        public string DownloadFile(string address)
        {

            try
            {
                string webUrl = ConfigurationManager.AppSettings["website_url"];
                string host = webUrl + "HeadImage/";
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
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                var filePath = dirPath + newFileName;
                host += newFileName;
                webClient.DownloadFile(address, filePath);

                
                return host;
            }
            catch (Exception ex)
            {
               // BaseService.WriteLogWeixin("图片下载异常信息：  " + ex.Message);
                return string.Empty;
            }
        }

        private void testGetImageText()
        {
            string materialId = "E79E0381658F4D7CA5E2D8920361F20A";
            string WeiXin = "gh_3dce00b8133c";
            string OpenId = "oKOmbjvLiao3NKKfbaZi8NxEk0Rc";
            JIT.CPOS.BS.BLL.zhongou.ZOSignPush server = new BS.BLL.zhongou.ZOSignPush(Default.GetLoggingSession());
            server.GetImageText(materialId, WeiXin, OpenId);
        }
    }
}
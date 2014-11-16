<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using System.Configuration;
using JIT.Utility.Log;
using LitJson;

public class Upload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        if (!new JIT.CPOS.BS.Web.PageBase.JITPage().CheckUserLogin())
        {
            showError("未登录，请先登录");
        }

        var loginInfo = new JIT.CPOS.BS.Web.PageBase.JITPage().CurrentUserInfo;


        String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

        //网站域名
        string host = ConfigurationManager.AppSettings["host"];

        Loggers.Debug(new DebugLogInfo() { Message = "ProcessRequest" + " host: " + ConfigurationManager.AppSettings["host"] });

        //文件保存目录路径
        String savePath = "../attached/";

        //文件保存目录URL
        // String saveUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority  + aspxUrl + "../attached/";
        String saveUrl = aspxUrl + "../attached/";
        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", "gif,jpg,jpeg,png,bmp");
        extTable.Add("flash", "swf,flv");
        extTable.Add("media", "swf,flv,mp3,mp4,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
        extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

        //最大文件大小
        int maxSize = 52428800;
        this.context = context;

        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }

        String dirPath = context.Server.MapPath(savePath);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        String dirName = context.Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = "image";
        }
        if (!extTable.ContainsKey(dirName))
        {
            showError("目录名不正确。");
        }

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();

        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }

        //创建文件夹
        dirPath += dirName + "/" + loginInfo.ClientID + "/";
        saveUrl += dirName + "/" + loginInfo.ClientID + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
        dirPath += ymd + "/";
        saveUrl += ymd + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        String filePath = dirPath + newFileName;

        imgFile.SaveAs(filePath);

        String fileUrl = saveUrl + newFileName;

        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = host + fileUrl;

        //这里上传ftp，把前面传到本地的图片和缩略图传到ftp上去。
        #region
        string ftpHostname = ConfigurationManager.AppSettings["FtpUrl"].ToString();
        string ftpUser = ConfigurationManager.AppSettings["FtpUser"].ToString();
        string ftpPass = ConfigurationManager.AppSettings["FtpPassword"].ToString();
        FileInfo fileInfo = new FileInfo(filePath);   //原图片
        if (!string.IsNullOrEmpty(ftpHostname))
        {
            try
            {
                //按日期建文件夹
                string ftpMappingimg = JIT.CPOS.BS.Web.Framework.Upload.FtpMapping.UploadFile(fileInfo, dirName + "/" + ymd, ftpHostname, ftpUser, ftpPass);
                hash["url"] = ftpMappingimg;               
            }
            catch (Exception ex)
            {
                hash["url"] = host + fileUrl;             
                Loggers.Debug(new DebugLogInfo() { Message = ex.Message });
            }
        }
        #endregion

        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}

<%@ WebHandler Language="C#" Class="UploadHomePage" %>

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using System.Configuration;
using JIT.Utility.Log;
using LitJson;
using JIT.CPOS.BS.Web.Framework.Upload;
/// <summary>
/// 上传图片并生成缩略图
/// </summary>
public class UploadHomePage : IHttpHandler
{
    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        //获得文件名
        String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);
        //网站域名
        //string host = ConfigurationManager.AppSettings["host"];
        string host = "";
        Loggers.Debug(new DebugLogInfo() { Message = "ProcessRequest" + " host: " + ConfigurationManager.AppSettings["host"] });

        //文件保存目录路径
        String savePath = "../attached/";
        //文件保存目录URL
        String saveUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + "" + aspxUrl + "../attached/";


        String dirPath = context.Server.MapPath(savePath);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        //获取文件类型
        String dirName = context.Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = "image";
        }

        //创建文件夹
        dirPath += dirName + "/";
        saveUrl += dirName + "/";
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

        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", "gif,jpg,jpeg,png,bmp");
        extTable.Add("flash", "swf,flv");
        extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
        extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

        //最大文件大小
        int maxSize = 52428800;
        this.context = context;
        //获取文件
        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
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
            showError("上传文件扩展名是不允许的扩展名。n只允许" + ((String)extTable[dirName]) + "格式。");
        }


        //生成文件名
        String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        //文件保存的物理里地址
        String filePath = dirPath + newFileName;
        //保存文件
        imgFile.SaveAs(filePath);
        //文件相对地址
        String fileUrl = saveUrl + newFileName;
        //设置返回信息，返回值是以JSON数组形式返回的，即前面是key,后面是value
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        //返回原图的地址
        hash["url"] = host + fileUrl;
        Loggers.Debug(new DebugLogInfo() { Message = "ProcessRequest" + " hash[\"url\"]: " + host + fileUrl });

        /*
         还可设置多个返回结果,比如图片的宽，高等
         如：hash["width"]=100;
         */
        //判断上传的文件类型
        string thumbfilePath = "";
        string smillUrl = "";
        if (context.Request.QueryString["dir"] == "image")
        {
            //请求中有width这个参数表示图片需要进行处理
            if (context.Request.QueryString["width"] != null)
            {   //缩略图相对地址
                thumbfilePath = dirPath + "Thumb" + newFileName;
                int width = ToInt(context.Request.QueryString["width"]);
                int height = ToInt(context.Request.QueryString["height"]);
                //获得原始图片的宽和高
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(filePath);
                int ow = originalImage.Width;
                int oh = originalImage.Height;
                //如果原图的尺寸比缩略图要求的尺寸小,则不进行任何处理
                if (ow <= width)
                {
                     smillUrl = fileUrl;
                    //返回缩略图的地址
                    hash["thumUrl"] = host + smillUrl;
                    Loggers.Debug(new DebugLogInfo() { Message = "ProcessRequest" + " hash[\"thumUrl\"]: " + host + smillUrl });
                }
                else
                {
                    //生成缩略图
                    if (MakeThumbnail(filePath, thumbfilePath, width, height, "W"))
                    {
                       smillUrl = saveUrl + "Thumb" + newFileName;
                        hash["thumUrl"] = host + smillUrl;
                    }
                    else
                    {
                        showError("生成缩略图失败");
                    }
                }
            }
        }
        //这里上传ftp，把前面传到本地的图片和缩略图传到ftp上去。
        #region
        string ftpHostname = ConfigurationManager.AppSettings["FtpUrl"].ToString();
        string ftpUser = ConfigurationManager.AppSettings["FtpUser"].ToString();
        string ftpPass = ConfigurationManager.AppSettings["FtpPassword"].ToString();
     
        FileInfo fileInfo = new FileInfo(filePath);   //原图片
        FileInfo fileInfothumb = new FileInfo(thumbfilePath);//缩略图
        if (!string.IsNullOrEmpty(ftpHostname))
        {
            try
            {
                //按日期建文件夹
                string ftpMappingimg = FtpMapping.UploadFile(fileInfo, dirName + "/" + ymd, ftpHostname, ftpUser, ftpPass);
                hash["url"] = ftpMappingimg;
                //上传缩略图
                string ftpMappingthumb = FtpMapping.UploadFile(fileInfothumb, dirName + "/" + ymd, ftpHostname, ftpUser, ftpPass);
                hash["thumUrl"] = ftpMappingthumb;
            }
            catch(Exception ex)
            {
                hash["url"] = host + fileUrl;
                hash["thumUrl"] = host + smillUrl;
                Loggers.Debug(new DebugLogInfo() { Message = ex.Message });
            }
        }
        #endregion


        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    //返回错误消息
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

    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式</param>
    public bool MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
        int towidth = width;
        int toheight = height;
        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;
        switch (mode)
        {
            case "HW"://指定高宽缩放（可能变形）                
                break;
            case "W"://指定宽，高按比例                    
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://指定高，宽按比例
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://指定高宽裁减（不变形）                
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }

        //新建一个bmp图片
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

        //新建一个画板
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充
        g.Clear(System.Drawing.Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分
        g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

        try
        {
            //以jpg格式保存缩略图
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            return true;
        }
        catch (System.Exception e)
        {
            return false;
            throw e;

        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }

    public int ToInt(object obj)
    {
        if (obj == null) return 0;
        else if (obj.ToString() == "") return 0;
        return Convert.ToInt32(obj);
    }



}

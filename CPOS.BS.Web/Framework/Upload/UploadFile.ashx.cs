using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using System.Configuration;
using JIT.CPOS.Common;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.Framework.Upload
{
    public class UploadFile : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext context)
        {
            //context.Response.ContentType = "multipart/form-data";
            //context.Response.Expires = -1;
            try
            {
                context.Response.Clear();
                //string content = "";
                switch (context.Request.QueryString["method"])
                {
                    case "image":
                        UploadImageData(context);
                        break;
                    case "file":
                    default:
                        var respObj = new UploadFileResp();
                        HttpPostedFile postedFile = context.Request.Files["file"];
                        if (postedFile == null) postedFile = context.Request.Files[0];
                        if (postedFile == null || postedFile.ContentLength == 0)
                        {
                            respObj.success = false;
                            respObj.msg = "文件不能为空";
                            respObj.file = new FileData();
                            context.Response.Write(respObj.ToJSON());
                            return;
                        }
                        string folderPath = "Framework/Upload/File/" + Utils.GetTodayString() + "/";
                        string savepath = HttpContext.Current.Server.MapPath("~/" + folderPath);
                        var extension = Path.GetExtension(postedFile.FileName).ToLower();
                        if (!Directory.Exists(savepath)) Directory.CreateDirectory(savepath);

                        var host = ConfigurationManager.AppSettings["host"];
                        if (!host.EndsWith("/")) host += "/";

                        var fileName = Common.Utils.NewGuid() + extension;
                        var fileLocation = string.Format("{0}/{1}", savepath, fileName);
                        postedFile.SaveAs(fileLocation);

                        respObj.o = true;
                        respObj.success = true;
                        respObj.msg = "";
                        respObj.file = new FileData();
                        #region
                        string targetDir = "file/" + Utils.GetTodayString();
                        string tempFileUrl = host + folderPath + fileName;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        tempFileUrl = getFtpUrl(fileInfo, tempFileUrl, targetDir);//上传到ftp
                        #endregion
                        respObj.file.url = tempFileUrl;
                        respObj.file.name = fileName;
                        respObj.file.extension = extension;
                        respObj.file.size = postedFile.ContentLength;

                        context.Response.Write(respObj.ToJSON());
                        context.Response.End();
                        break;
                }

            }
            catch (System.Threading.ThreadAbortException exf)
            {

            }
            catch (Exception ex)
            {
                context.Response.Clear();
                var respObj = new UploadFileResp();
                respObj.success = false;
                respObj.msg = ex.Message;
                context.Response.Write(respObj.ToJSON());
                context.Response.End();
            }
        }

        #region UploadFileResp
        public class UploadFileResp
        {
            public bool o { get; set; }
            public bool success { get; set; }
            public string msg { get; set; }
            public FileData file { get; set; }
            public IList<FileData> thumbs { get; set; }
        }
        public class FileData
        {
            public string url { get; set; }
            public string name { get; set; }
            public string extension { get; set; }
            public long size { get; set; }
            public string type { get; set; }
        }
        #endregion

        #region UploadImageData
        public void UploadImageData(HttpContext context)
        {
            var respObj = new UploadFileResp();
            HttpPostedFile postedFile = context.Request.Files["file"];
            if (postedFile == null) postedFile = context.Request.Files[0];
            if (postedFile == null || postedFile.ContentLength == 0)
            {
                respObj.success = false;
                respObj.msg = "文件不能为空";
                respObj.file = new FileData();
                context.Response.Write(respObj.ToJSON());
                return;
            }
            string folderPath = "Framework/Upload/Image/" + Utils.GetTodayString() + "/";
            string savepath = HttpContext.Current.Server.MapPath("~/" + folderPath);
            var extension = Path.GetExtension(postedFile.FileName).ToLower();
            if (!Directory.Exists(savepath)) Directory.CreateDirectory(savepath);

            var host = ConfigurationManager.AppSettings["host"];
            if (!host.EndsWith("/")) host += "/";

            var fileName = Common.Utils.NewGuid();
            var fileFullName = fileName + extension;
            var fileLocation = string.Format("{0}/{1}", savepath, fileFullName);
            postedFile.SaveAs(fileLocation);//保存原图

            string targetDir = "image/" + Utils.GetTodayString();
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(fileLocation);
            //生成缩略图
            respObj.thumbs = new List<FileData>();
            if (true)
            {
                int thumbWidth = 120;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    string thumbUrl = host + folderPath + thumbFullName;
                    thumbUrl = getFtpUrl(thumbImage, thumbUrl, targetDir);//上传到ftp
                    respObj.thumbs.Add(new FileData()
                    {
                        url = thumbUrl,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 240;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    string thumbUrl = host + folderPath + thumbFullName;
                    thumbUrl = getFtpUrl(thumbImage, thumbUrl, targetDir);//上传到ftp
                    respObj.thumbs.Add(new FileData()
                    {
                        url = thumbUrl,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 480;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    string thumbUrl = host + folderPath + thumbFullName;
                    thumbUrl = getFtpUrl(thumbImage, thumbUrl, targetDir);//上传到ftp
                    respObj.thumbs.Add(new FileData()
                    {
                        url = thumbUrl,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)//商品详情里用的图片
            {
                int thumbWidth = 640;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    string thumbUrl = host + folderPath + thumbFullName;
                    thumbUrl = getFtpUrl(thumbImage, thumbUrl, targetDir);//上传到ftp
                    respObj.thumbs.Add(new FileData()
                    {
                        url = thumbUrl,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 960;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    string thumbUrl = host + folderPath + thumbFullName;
                    thumbUrl = getFtpUrl(thumbImage, thumbUrl, targetDir);//上传到ftp
                    respObj.thumbs.Add(new FileData()
                    {
                        url = thumbUrl,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }

            respObj.o = true;
            respObj.success = true;
            respObj.msg = "";
            respObj.file = new FileData();
            //传ftp
            #region
            string tempFileUrl = host + folderPath + fileFullName;
            FileInfo fileInfo = new FileInfo(fileLocation);
            tempFileUrl = getFtpUrl(fileInfo, tempFileUrl, targetDir);//上传到ftp
            #endregion
            respObj.file.url = tempFileUrl;
            respObj.file.name = fileFullName;
            respObj.file.extension = extension;
            respObj.file.size = postedFile.ContentLength;
            if (originalImage != null) originalImage.Dispose();


            context.Response.Write(respObj.ToJSON());
            context.Response.End();
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public bool MakeThumbnail(System.Drawing.Image originalImage, string thumbnailPath, int width, int height, string mode)
        {
            //System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            //如果原图的尺寸比缩略图要求的尺寸小,则不进行任何处理
            if (originalImage.Width <= width && originalImage.Height <= height)
            {
                return false;
            }

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
                //originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 上传ftp，把前面传到本地的图片和缩略图传到ftp上去。
        /// </summary>
        /// <param name="thumbImage"></param>
        /// <param name="thumbUrl"></param>
        /// <returns></returns>
        private string getFtpUrl(FileInfo thumbImage, string thumbUrl, string targetDir)
        {
            string ftpHostname = ConfigurationManager.AppSettings["FtpUrl"].ToString();
            string ftpUser = ConfigurationManager.AppSettings["FtpUser"].ToString();
            string ftpPass = ConfigurationManager.AppSettings["FtpPassword"].ToString();
            string tempUrl = thumbUrl;
            if (!string.IsNullOrEmpty(ftpHostname))
            {
                try
                {
                    tempUrl = FtpMapping.UploadFile(thumbImage, targetDir, ftpHostname, ftpUser, ftpPass);
                }
                catch (Exception ex)
                {
                    tempUrl = thumbUrl;//还等于原来的路径
                    Loggers.Debug(new DebugLogInfo() { Message = ex.Message });
                }
            }
            return tempUrl;
        }
    }
}
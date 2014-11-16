using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using System.Collections.Specialized;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.Utility;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using JIT.Utility.Log;
using System.Threading;
//using JIT.TenantPlatform.Web.Base.Xml;

namespace JIT.TenantPlatform.Web.Framework.Javascript.Other.editor
{
    /// <summary>
    /// EditorFileHandler 的摘要说明
    /// </summary>
    public class EditorFileHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";

            if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
            {
                switch (pContext.Request.QueryString["method"])
                {
                    case "EditorFile":
                        EditorFile(pContext);
                        break;
                }
            }

            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region 编辑器上传处理
        #region EditorFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public void EditorFile(HttpContext pContext)
        {
            HttpPostedFile imgFile = pContext.Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError(pContext, "请选择要上传文件！");
                return;
            }
            string remsg = UpLoadEditor(imgFile, pContext.Request.QueryString["FileUrl"]);
            string pattern = @"^{\s*msg:\s*(.*)\s*,\s*msgbox:\s*\""(.*)\""\s*}$"; //键名前和键值前后都允许出现空白字符
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
            Match m = r.Match(remsg); //搜索匹配项
            string msg = m.Groups[1].Value; //msg的值，正则表达式中第1个圆括号捕获的值
            string msgbox = m.Groups[2].Value; //msgbox的值，正则表达式中第2个圆括号捕获的值
            if (msg == "0")
            {
                showError(pContext, msgbox);
                return;
            }
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = msgbox;
            pContext.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            pContext.Response.Write(hash.ToJSON());
            pContext.Response.End();
        }

        //显示错误
        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(hash.ToJSON());
            context.Response.End();
        }
        #endregion

        #region UpLoadEditor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string UpLoadEditor(HttpPostedFile files, string path)
        {
            try
            {
                string fileExt = files.FileName; //文件扩展名，含“.”
                string suffixname = "";
                if (files != null)
                {
                    suffixname = fileExt.Substring(fileExt.LastIndexOf(".")).ToLower();
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + suffixname; //随机文件名
                string dirPath = "/File/Other/" + path + "/"; //上传目录路径
                if (!System.IO.Directory.Exists(dirPath))
                {
                    System.IO.Directory.CreateDirectory(dirPath);
                }
                //检查是否必须上传图片
                if (!IsImage(suffixname))
                {
                    return "{msg: 0, msgbox: \"对不起，仅允许上传图片文件！\"}";
                }
                string savepath = HttpContext.Current.Server.MapPath(dirPath);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                files.SaveAs(savepath + @"/" + fileName);//保存
                return "{msg: 1, msgbox: \"" + ConfigurationManager.AppSettings["AttachFilePath"].ToString() + dirPath + fileName + "\"}";
            }
            catch
            {
                return "{msg: 0, msgbox: \"上传过程中发生意外错误！\"}";
            }
        }

        #endregion

        #region IsImage
        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="_fileExt">文件扩展名，含“.”</param>
        private bool IsImage(string _fileExt)
        {
            string fileType = ".bmp|.jpeg|.jpg|.gif|.png";
            if (fileType.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion
    }
}
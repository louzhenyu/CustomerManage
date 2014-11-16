using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace JIT.CPOS.BS.Web.Module.College.CommRes
{
    public partial class UploadImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection hf = Request.Files;
            string ret = string.Empty;
            //有图片上传
            if (hf.Count > 0)
            {
                //网站域名
                string host = ConfigurationManager.AppSettings["host"];
                //文件名
                string filename = Guid.NewGuid().ToString().ToLower();
                //扩展名
                string ext = Path.GetExtension(hf[0].FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".giff" && ext != ".bmp" && ext != ".png")
                {
                    ret = "-1";
                }
                else
                {
                    hf[0].SaveAs(Server.MapPath("~/File/college/"+filename+ext));
                    ret = host + "/file/college/" + filename + ext;
                }
                Response.Write(ret);
            }
        }
    }
}
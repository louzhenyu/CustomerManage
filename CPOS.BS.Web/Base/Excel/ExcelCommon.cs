using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace JIT.CPOS.BS.Web.Base.Excel
{
    public class ExcelCommon
    {
        #region OutPutExcel
        /// <summary>
        /// 输出excel文件
        /// </summary>
        /// <param name="p"></param>
        /// <param name="filePath"></param>
        public void OutPutExcel(HttpContext p, string filePath)
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
    }
}
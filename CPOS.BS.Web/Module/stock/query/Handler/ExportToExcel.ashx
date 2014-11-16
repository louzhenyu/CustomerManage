<%@ WebHandler Language="C#" CodeBehind="ExportToExcel.ashx.cs" Class="JIT.CPOS.BS.Web.Module.stock.query.Handler.ExportToExcel" %>


using System;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
//using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;

public class ExportToExcel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public string ExportDir { get { return HttpContext.Current.Server.MapPath("~/ExportTemp/"); } } 
    private Random _random = new Random();
    LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
    public void ProcessRequest (HttpContext context)
    {
        try
        {
            LoadLoggingSessionInfo();
            var paras = context.Request.Params;
            context.Response.ContentType = "text/plain";
            string fileName = doDownLoadMethod(context, paras);
            var str = System.IO.Path.Combine(context.Request.ApplicationPath, "ExportTemp/" + fileName);
            context.Response.Write(str);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            throw ex;
        } 
    }

    private void LoadLoggingSessionInfo()
    {
        this.loggingSessionInfo = HttpContext.Current.Session["loggingSessionInfo"] as LoggingSessionInfo;
    }
    
    private string doDownLoadMethod(HttpContext context, 
        System.Collections.Specialized.NameValueCollection paras)
    {
        var type = paras["type"].ToString();
        ClearTempFilesBeforeToday();
        switch (type)
        {
            case "stock": LoadSkuProp(); return ExportStock();
            default: return null;
        }
    }
    #region 加载Sku属性信息
    private System.Collections.Generic.IList<JIT.CPOS.BS.Entity.SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            SkuPropInfos = new JIT.CPOS.BS.BLL.SkuPropServer(loggingSessionInfo).GetSkuPropList();
        }
        catch(Exception ex)
        {  
            PageLog.Current.Write(ex);
        }
    }
    #endregion
    private string ExportStock()
    {
        try
        {
            var para = HttpContext.Current.Request.Params;
            var source = new JIT.CPOS.BS.BLL.StockBalanceService(loggingSessionInfo).SearchStockBalance(
                para["unit_id"] ?? "",
                para["warehouse_id"] ?? "",
                para["item_name"] ?? "",
                36500,
                0);
            if (source == null || source.StockBalanceInfoList == null || source.StockBalanceInfoList.Count == 0)
            {
                return string.Empty;
            }
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+_random.Next(1,100).ToString() + ".xls";
            var full_path = ExportDir + fileName;
            var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
            System.Collections.Generic.List<string> headers = new List<string>();
            string[] header_pre = new string[] { "单位", "仓库", "品质", "商品编码", "商品名称" };
            string[] header_nex = new string[] { "期初数", "入库数", "出库数", "调整入数", "调整出数", "期末数" };
            headers.AddRange(header_pre);
            for (int i = 1; i <= 5; i++)
            {
                var item = this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == i);
                if (item != null)
                {
                    headers.Add(item.prop_name);
                }
            }
            headers.AddRange(header_nex);
            var data = source.StockBalanceInfoList;
            ExportHtmlExcel<JIT.CPOS.BS.Entity.StockBalanceInfo>(Writer, headers.ToArray(), data, obj =>
            {
                var cells = new object[] 
                { 
                    obj.unit_name??"", 
                    obj.warehouse_name??"", 
                    obj.item_label_type_name??"", 
                    obj.item_code??"", 
                    obj.item_name??"", 
                    propValue(obj, 1),  
                    propValue(obj, 2) , 
                    propValue(obj, 3) ,  
                    propValue(obj, 4) , 
                    propValue(obj, 5) ,
                    obj.begin_qty, 
                    obj.in_qty, 
                    obj.out_qty, 
                    obj.adjust_in_qty, 
                    obj.adjust_out_qty,
                    obj.end_qty 
                };
                return cells;
            }, null);
            Writer.Close();
            sw.Close();
             return fileName;
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    private string propValue(JIT.CPOS.BS.Entity.StockBalanceInfo info, int index)
    {
        switch (index)
        {
            case 1: return info.prop_1_detail_name;
            case 2: return info.prop_2_detail_name;
            case 3: return info.prop_3_detail_name;
            case 4: return info.prop_4_detail_name;
            case 5: return info.prop_5_detail_name;
            default: return null;
        }
    }
   
    public bool IsReusable {
        get {
            return false;
        }
    }

    private void ClearTempFilesBeforeToday()
    {
        try
        {
            if (!Directory.Exists(ExportDir))
            {
                Directory.CreateDirectory(ExportDir);
            }
            var _dic = new DirectoryInfo(ExportDir);
            var _files = _dic.GetFiles();
            foreach (var file in _files)
            {
                if (file.CreationTime <= DateTime.Now.Date)
                {
                    if (!file.IsReadOnly) file.Delete();
                }
            }
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            throw ex;
        }
    }

    string ExportHtmlExcel<T>(IEnumerable<string> tabHeader,
        IEnumerable<T> dataRows,
        Func<T,IEnumerable<string>> func_row,
        Func<IEnumerable<T>,
        IEnumerable<string>> func_total_row)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
        sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

        //写出列名 
        sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
        
        foreach (string head in tabHeader)
        {  
            sb.AppendLine("<td>" + System.Web.HttpUtility.HtmlEncode(head) + "</td>"); 
        }
        
        sb.AppendLine("</tr>");

        //写出数据 
        foreach (var item in dataRows)
        {
            sb.Append("<tr>");
            foreach (var cel in func_row(item))
            {
                if (cel != null)
                    sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + 
                        System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
            }
            sb.AppendLine("</tr>"); 
        }
        if (func_total_row != null)
        {
            sb.Append("<tr>");
            foreach (var cel in func_total_row(dataRows))
            {
                sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + 
                    System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
            }
            sb.Append("</tr>");
        }
        sb.AppendLine("</table>");

        return sb.ToString();
    }
    void ExportHtmlExcel<T>(StreamWriter sb, IEnumerable<string> tabHeader, 
        IEnumerable<T> dataRows, Func<T, 
        IEnumerable<object>> func_row, 
        Func<IEnumerable<T>, 
        IEnumerable<string>> func_total_row)
    {
        sb.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");
        sb.WriteLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

        //写出列名 
        sb.WriteLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");

        foreach (string head in tabHeader)
        {
            sb.WriteLine("<td>" + System.Web.HttpUtility.HtmlEncode(head) + "</td>");
        }

        sb.WriteLine("</tr>");

        //写出数据 
        foreach (var item in dataRows)
        {
            sb.Write("<tr>");
            foreach (var cel in func_row(item))
            {
                if (cel != null)
                {
                    if (cel is decimal|| cel is Int32)
                    {
                        sb.Write("<td>" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
                    }
                    else
                    {
                        sb.Write("<td style=\"vnd.ms-excel.numberformat:@\" >" + 
                            System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
                    }
                }
            }
            sb.WriteLine("</tr>");
        }
        if (func_total_row != null)
        {
            sb.Write("<tr>");
            foreach (var cel in func_total_row(dataRows))
            {
                if (cel != null)
                {
                    sb.Write("<td style=\"vnd.ms-excel.numberformat:@\"></td>");
                }
            }
            sb.Write("</tr>");
            sb.Write("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            foreach (var cel in func_total_row(dataRows))
            {
                if (cel != null)
                    sb.Write("<td>" + System.Web.HttpUtility.HtmlEncode(cel) + "</td>");
            }
            sb.Write("</tr>");
        }
        sb.WriteLine("</table>");
    }
}

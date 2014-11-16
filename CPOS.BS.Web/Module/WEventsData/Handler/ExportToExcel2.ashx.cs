
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
using System.Data;
using System.Collections;

namespace JIT.CPOS.BS.Web.Module.WEventsData.Handler
{

    public class ExportToExcel2 : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        public string ExportDir { get { return HttpContext.Current.Server.MapPath("~/ExportTemp/"); } }
        private Random _random = new Random();
        LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        protected override void AjaxRequest(HttpContext context)
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
            this.loggingSessionInfo = CurrentUserInfo;
        }

        private string doDownLoadMethod(HttpContext context,
            System.Collections.Specialized.NameValueCollection paras)
        {
            var type = paras["type"].ToString();
            ClearTempFilesBeforeToday();
            switch (type)
            {
                case "stock": return ExportStock();
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
            catch (Exception ex)
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

                string EventId = FormatParamValue(para["eventId"]);
                string searchSql = FormatParamValue(para["SearchOptionValue"]).Trim();

                DataTable dataExport = new DataTable();
                WEventUserMappingBLL wEventUserMappingBLL = new WEventUserMappingBLL(this.CurrentUserInfo);
                QuesQuestionsBLL quesQuestionsBLL = new QuesQuestionsBLL(this.CurrentUserInfo);
                dataExport = wEventUserMappingBLL.SearchEventUserList(EventId, searchSql).Tables[0];


                //var service = new LEventSignUpBLL(loggingSessionInfo);
                //GetResponseParams<LEventSignUpEntity> dataList = service.GetEventApplies(para["eventId"] ?? "", 0, 10000);

                //IList<LEventSignUpEntity> data2 = new List<LEventSignUpEntity>();
                int dataTotalCount = 0;

                var data2 = dataExport.Rows;
                dataTotalCount = dataExport.Rows.Count;


                IList<List<string>> source = new List<List<string>>();
                for (var i = 0; i < dataExport.Rows.Count; i++)
                {
                    var tmpHt = new List<string>();
                    var dr = dataExport.Rows[i];
                    for (var c = 1; c < dataExport.Columns.Count; c++)
                    {
                        tmpHt.Add(dr[c] == null ? "" : dr[c].ToString());
                    }
                    source.Add(tmpHt);
                }

                if (source == null || source.Count == 0)
                {
                    return string.Empty;
                }
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _random.Next(1, 100).ToString() + ".xls";
                var full_path = ExportDir + fileName;
                var sw = new FileStream(full_path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                var Writer = new StreamWriter(sw, System.Text.Encoding.UTF8);
                System.Collections.Generic.List<string> headers = new List<string>();
                string[] header_nex = new string[] { };
                for (var i = 1; i < dataExport.Columns.Count; i++)
                {
                    var col = dataExport.Columns[i];
                    string colName = wEventUserMappingBLL.GetQuestionsDesc(EventId, col.ColumnName);
                    headers.Add(colName);
                }
                var data = source;
                ExportHtmlExcel<List<string>>(Writer, headers.ToArray(), data, obj =>
                {
                    return obj;
                }, null);
                Writer.Close();
                sw.Close();
                return fileName;
            }
            catch (Exception ex)
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

        public bool IsReusable
        {
            get
            {
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
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                throw ex;
            }
        }

        string ExportHtmlExcel<T>(IEnumerable<string> tabHeader,
            IEnumerable<T> dataRows,
            Func<T, IEnumerable<string>> func_row,
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
                        if (cel is decimal || cel is Int32)
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

}
using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    /// <summary>
    /// DownLoadSales 的摘要说明
    /// 销售订单处理
    /// </summary>
    public class DownLoadSales : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 导出已完成(已退款)订单
        /// </summary>
        /// <param name="pContext"></param>
        private void ExportDownLoadRefunded(HttpContext pContext)
        {
            try
            {
                var T_RefundOrderBLL = new T_RefundOrderBLL(CurrentUserInfo);
                var ExcelHelper = new ExcelHelper();
                #region 获取信息
                //获取退款单数据
                DataSet ds = T_RefundOrderBLL.GetWhereRefundOrder(2, CurrentUserInfo.ClientID);
                string filename = "";
                #endregion

                if (filename == null || filename == "")
                {
                    filename = "交易订单";
                }
                string filesrc = @"~/Framework/Upload/" + filename + "/";

                if (!System.IO.Directory.Exists(pContext.Server.MapPath(filesrc)))
                {
                    System.IO.Directory.CreateDirectory(pContext.Server.MapPath(filesrc));
                }
                //导出操作
                string ExcelflieName = "已完成退款订单" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string MapUrl = pContext.Server.MapPath(filesrc+ExcelflieName + ".xls");
                ExcelHelper.RenderToExcel(ds.Tables[0], MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 导出待退款订单
        /// </summary>
        private void ExportDownLoadPendingRefund(HttpContext pContext)
        {
            try
            {
                var T_RefundOrderBLL = new T_RefundOrderBLL(CurrentUserInfo);
                var ExcelHelper = new ExcelHelper();
                #region 获取信息
                //获取退款单数据
                DataSet ds = T_RefundOrderBLL.GetWhereRefundOrder(1, CurrentUserInfo.ClientID);
                string filename = "";
                #endregion

                if (filename == null || filename == "")
                {
                    filename = "交易订单";
                }
                string filesrc = @"~/Framework/Upload/" + filename + "/";

                
                //导出操作
                string ExcelflieName = "待退款订单" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string MapUrl = pContext.Server.MapPath(filesrc + ExcelflieName + ".xls");
                ExcelHelper.RenderToExcel(ds.Tables[0], MapUrl);
                
                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 导出所有退款单
        /// </summary>
        /// <param name="pContext"></param>
        private void ExportDownLoadALLRefund(HttpContext pContext)
        {
            try
            {
                var T_RefundOrderBLL = new T_RefundOrderBLL(CurrentUserInfo);
                var ExcelHelper = new ExcelHelper();
                #region 获取信息
                //获取退款单数据
                DataSet ds = T_RefundOrderBLL.GetWhereRefundOrder(0, CurrentUserInfo.ClientID);
                string filename = "";
                #endregion

                if (filename == null || filename == "")
                {
                    filename = "交易订单";
                }
                string filesrc = @"~/Framework/Upload/" + filename + "/";

                if (!System.IO.Directory.Exists(pContext.Server.MapPath(filesrc)))
                {
                    System.IO.Directory.CreateDirectory(pContext.Server.MapPath(filesrc));
                }
                //导出操作
                string ExcelflieName = "退款订单" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string MapUrl = pContext.Server.MapPath(filesrc + ExcelflieName + ".xls");
                ExcelHelper.RenderToExcel(ds.Tables[0], MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "ExportDownLoadRefunded":
                    ExportDownLoadRefunded(pContext);
                    break;
                case "ExportDownLoadPendingRefund":
                    ExportDownLoadPendingRefund(pContext);
                    break;
                case "ExportDownLoadALLRefund":
                    ExportDownLoadALLRefund(pContext);
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
using CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    /// <summary>
    /// DownLoadSales 的摘要说明
    /// 销售订单处理
    /// </summary>
    public class DownLoadSales : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        
        /// <summary>
        /// 导出退款单
        /// </summary>
        /// <param name="pContext"></param>
        private void ExportDownLoadRefund(HttpContext pContext)
        {
            try
            {
                var reqContent = pContext.Request["para"];
                var commonRequest = JsonHelper.JsonDeserialize<ExportRefundRP>(reqContent);
                var T_RefundOrderBLL = new T_RefundOrderBLL(CurrentUserInfo);
                var ExcelHelper = new ExcelHelper();
                #region 获取信息
                //获取退款单数据
                DataSet ds = T_RefundOrderBLL.GetWhereRefundOrder(commonRequest.RefundNo, commonRequest.paymentcenterId, commonRequest.payId, commonRequest.Status, CurrentUserInfo.ClientID);
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
        /// <summary>
        /// 导出退货单
        /// </summary>
        /// <param name="pContext"></param>
        private void ExportDownLoadSalesReturnOrderList(HttpContext pContext)
        {
            try
            {
                var reqContent = pContext.Request["para"];
                var commonRequest = JsonHelper.JsonDeserialize<SalesReturnRP>(reqContent);
                var T_SalesReturnBLL = new T_SalesReturnBLL(CurrentUserInfo);
                var ExcelHelper = new ExcelHelper();
                #region 获取信息
                DataSet ds = T_SalesReturnBLL.GetWhereSalesReturnOrder(commonRequest.SalesReturnNo, commonRequest.DeliveryType, commonRequest.Status, commonRequest.paymentcenterId, commonRequest.payId);
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
                string ExcelflieName = "退货订单" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string MapUrl = pContext.Server.MapPath(filesrc + ExcelflieName + ".xls");
                ExcelHelper.RenderToExcel(ds.Tables[0], MapUrl);

                Utils.OutputExcel(pContext, MapUrl);//输出Excel文件
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
                  
            
        }
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "ExportDownLoadRefund":
                    ExportDownLoadRefund(pContext);
                    break;
                case "ExportDownLoadSalesReturnOrderList":
                    ExportDownLoadSalesReturnOrderList(pContext);
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

    public class ExportRefundRP {
        public string RefundNo { get; set; }
        public string paymentcenterId { get; set; }
        public string payId { get; set; }
        public int Status { get; set; }
    }

    public class SalesReturnRP {
        public string SalesReturnNo { get; set; }
        public int DeliveryType { get; set; }
        public int Status { get; set; }
        public string paymentcenterId { get; set; }
        public string payId { get; set; }
    }
}
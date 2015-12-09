using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JIT.CPOS.BS.BLL;
using CPOS.Common;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity.WX;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    /// <summary>
    /// 导出退款单
    /// </summary>
    public class ExportSalesReturnExcelAH : BaseActionHandler<ExportSalesReturnExcelRD, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<ExportSalesReturnExcelRD> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            T_RefundOrderBLL T_RefundOrderBLL = new T_RefundOrderBLL(loggingSessionInfo);
            ExcelHelper ExcelHelper = new ExcelHelper();
            //获取退款单数据
            DataSet ds = T_RefundOrderBLL.GetWhereRefundOrder(para.Status, loggingSessionInfo.ClientID);
            //导出操作
            string flieName = "D://导出退款单" + DateTime.Now.ToString("yyyymmddhhmmss") + ".xls";
            ExcelHelper.RenderToExcel(ds.Tables[0], flieName);
            
            return rd;
        }
    }
}
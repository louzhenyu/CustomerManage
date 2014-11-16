using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.Utility;
using Aspose.Cells;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.ApplicationInterface.WXSalesRebate
{
    /// <summary>
    /// Gateway 的摘要说明
    /// </summary>
    public class Gateway : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction.ToLower())
            {
                case "export":
                    ExportList(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                case "query":
                    rst = QueryRebate(pRequest);
                    break;
                case "loadunit":
                    rst = GetUnitList(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        /// <summary>
        /// 获取用户可以查看的会籍店列表
        /// </summary>
        /// <returns></returns>
        private string GetUnitList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var controller = new WXSalesPolicyRateBLL(loggingSessionInfo);
            var ds = controller.GetUnitList(loggingSessionInfo.UserID, loggingSessionInfo.ClientID);
            if (ds == null && ds.Tables.Count == 0)
                throw new Exception("没有可用的门店列表.");
            var result = ds.Tables[0].AsEnumerable().Select(e => new
            {
                UnitID = e["UnitID"].ToString(),
                UnitName = e["UnitName"].ToString(),
                ParentUnitID = e["ParrentUnitID"].ToString()
            });
            return result.ToJSON();
        }
        /// <summary>
        /// 查询返利列表
        /// </summary>
        /// <returns></returns>
        private string QueryRebate(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<QueryRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            try
            {
                var controller = new WXSalesPolicyRateBLL(loggingSessionInfo);
                var ds = controller.GetRebateList(loggingSessionInfo.UserID, loggingSessionInfo.ClientID, rp.Parameters.PageIndex, rp.Parameters.PageSize, rp.Parameters.SearchColumns);
                if (ds == null || ds.Tables.Count == 0)
                    throw new Exception("查询出错");
                var result = ds.Tables[0].AsEnumerable().Select(e => new
                {
                    Id = e["DCodeId"].ToString(),
                    TotalCount = Convert.ToInt32(e["totalCount"]),
                    HandleTime = e["HandleTime"].ToString(),
                    Operator = e["Operator"].ToString(),
                    UnitName = e["UnitName"].ToString(),
                    SalesAmount = e["SalesAmount"].ToString(),
                    ReturnAmount = e["ReturnAmount"].ToString(),
                    VipName = e["VipName"].ToString(),
                    OrderNo = e["OrderNo"].ToString()
                });
                return result.ToJSON();
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(@"查询返利列表出错:{0}", ex.Message)
                });
                throw new Exception("查询出错.");
            }
        }
        /// <summary>
        private void ExportList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<QueryRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var pageIndex = 1;
            var pageSize = int.MaxValue;
            var searchColumns = rp.Parameters.SearchColumns;
            if (searchColumns == null)
                searchColumns = new List<SearchColumn>().ToArray();
            var controller = new WXSalesPolicyRateBLL(loggingSessionInfo);
            var ds = controller.GetRebateList(loggingSessionInfo.UserID, loggingSessionInfo.ClientID, pageIndex, pageSize, searchColumns);
            if (ds == null || ds.Tables.Count == 0)
                throw new Exception("查询出错");
            var columns = new Dictionary<string, string>();
            columns.Add("lastupdatetime", "时间");
            columns.Add("UnitName", "门店");
            columns.Add("Operator", "操作员");
            columns.Add("SalesAmount", "交易金额");
            columns.Add("ReturnAmount", "返利");
            columns.Add("VipName", "会员昵称");
            columns.Add("OrderNo", "订单编号");
            ExportTable(columns, ds.Tables[0], "返利列表");
        }
        private void ExportTable(Dictionary<string, string> columns, DataTable dt, string fileName)
        {
            DataColumn dc = new DataColumn();
            dt = dt.DefaultView.ToTable(false, columns.Keys.ToArray());

            foreach (DataColumn c in dt.Columns)
            {
                string title;
                columns.TryGetValue(c.ColumnName, out title);
                c.ColumnName = title;
            }
            Workbook wb = DataTableExporter.WriteXLS(dt, 0);
            string savePath = HttpContext.Current.Server.MapPath(@"~/File/Vip");
            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }
            savePath = savePath + "\\" + fileName + DateTime.Now.ToFileTime() + ".xls";
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(HttpContext.Current, savePath);
            HttpContext.Current.Response.End();
        }
    }
    public class QueryRP : IAPIRequestParameter
    {
        public SearchColumn[] SearchColumns { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        {

        }
    }
}
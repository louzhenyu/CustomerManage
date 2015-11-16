using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using System.Data;
using Aspose.Cells;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.Utility;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Card
{
    /// <summary>
    /// VipCardHandle 的摘要说明
    /// </summary>
    public class VipCardHandle : BaseGateway
    {
        #region 导出卡号
        /// <summary>
        /// 导出卡号
        /// </summary>
        /// <param name="pRequest"></param>
        public void ExportVipCardCode(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<ExportVipCardCodeRP>>();
            if (!string.IsNullOrWhiteSpace(rp.Parameters.BatchNo))
            {
                var si = new SessionManager().CurrentUserLoginInfo;
                if (null == si)
                    si = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                var VipCardBLL = new VipCardBLL(si);
                var ds = VipCardBLL.ExportVipCardCode(rp.Parameters.BatchNo);
                var columns = new Dictionary<string, string>();
                columns.Add("VipCardCode", "卡号");
                ExportVipInfo(columns, ds.Tables[0], "导出卡号");
            }
        }
        #endregion
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "ExportVipCardCode":
                    this.ExportVipCardCode(pRequest);
                    rst = "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true}";
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            //HttpContext.Current.Response.ContentType = "text/html;charset=UTF-8";  
            return rst;
        }

        #region 导出方法
        private void ExportVipInfo(Dictionary<string, string> columns, DataTable dt, string fileName)
        {
            Workbook wb = DataTableExporter.WriteXLS(dt, 0);
            string savePath = HttpContext.Current.Server.MapPath(@"~/File/VipCard");
            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }
            savePath = savePath + "\\" + fileName + DateTime.Now.ToFileTime() + ".xls";
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(HttpContext.Current, savePath);
            HttpContext.Current.Response.End();
        }

        #endregion
    }
}
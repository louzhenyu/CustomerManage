using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;
using System.Data;
using JIT.Utility;
using Aspose.Cells;
using JIT.CPOS.BS.Web.Base.Excel;

namespace JIT.CPOS.BS.Web.Project.EMBAUnion.Events.Handler
{
    /// <summary>
    /// EventVipHandler 的摘要说明
    /// </summary>
    public class EventVipHandler : JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "GetList":
                    res = GetList(pContext.Request.Form);
                    break;
                case "ExportUserList":
                    ExportUserList(pContext);
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["id"]);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetList
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        private string GetList(NameValueCollection rParams)
        {
            #region 条件拼接
            Dictionary<string, string> pParems = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(rParams["pVipName"]))
            {
                pParems.Add("pVipName", rParams["pVipName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pEventID"]))
            {
                pParems.Add("pEventID", rParams["pEventID"]);
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();

            #endregion

            PagedQueryResult<VipViewEntity> pagedQueryEntity = new EMBAUnionBLL(CurrentUserInfo, "vip").GetUserList(pParems, pageIndex, pageSize);

            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}", pagedQueryEntity.Entities.ToJSON(), pagedQueryEntity.PageCount);
        }
        #endregion

        #region ExportUserList
        public void ExportUserList(HttpContext pContext)
        {
            #region 条件拼接
            Dictionary<string, string> pParems = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["pVipName"]))
            {
                pParems.Add("pVipName", pContext.Request.QueryString["pVipName"]);
            }
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["pEventID"]))
            {
                pParems.Add("pEventID", pContext.Request.QueryString["pEventID"]);
            }
            #endregion

            //数据获取
            DataTable result = new EMBAUnionBLL(CurrentUserInfo, "vip").ExportUserList(pParems).Tables[0];
            result.TableName = "参加活动人员信息";
            Workbook wb = DataTableExporter.WriteXLS(result, 0);
            string savePath = pContext.Server.MapPath(@"~/File/Excel/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(pContext, savePath);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除品牌信息
        /// </summary>
        /// <param name="ids">要删除的id</param>
        /// <returns>返回json</returns>
        private string Delete(string ids)
        {
            string res = "{success:false}";
            new EMBAUnionBLL(CurrentUserInfo, "vip").DeleteEventVipMapping(ids);
            res = "{success:true}";
            return res;
        }
        #endregion
    }
}
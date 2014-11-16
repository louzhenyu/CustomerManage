using System.Collections.Generic;
using System.Web;
using System.Linq;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using System.Configuration;
using JIT.CPOS.Common;
using System.Threading;

using JIT.Utility.DataAccess;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.BS.Web.Base.Excel;
using System.Collections.Specialized;
using Aspose.Cells;
using JIT.Utility;
using System.IO;

namespace JIT.CPOS.BS.Web.Module.XieHuiBao.Events.Handler
{
    /// <summary>
    /// EventsHandler
    /// </summary>
    public class EventsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        public class GridInitEntity
        {
            #region 属性集
            /// <summary>
            /// 表格数据定义
            /// </summary>
            public List<GridColumnModelEntity> GridDataDefinds { get; set; }

            /// <summary>
            /// 表格表头定义
            /// </summary>
            public List<GridColumnEntity> GridColumnDefinds { get; set; }

            /// <summary>
            /// 表格数据
            /// </summary>
            public PageResultEntity GridDatas { get; set; }
            #endregion
        }

        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "InitGridDataByEventID": //根据活动ID获取定义信息
                    res = GetInitGridDataByEventID(pContext).ToJSON();
                    break;
                case "PageGridDataByEventID":
                    res = GetPageDataByEventID(pContext).ToJSON();
                    break;
                case "ExportUserListByEventID":
                    ExportUserListByEventID(pContext);
                    break;
                case "PageGridData":
                    res = GetPageData(pContext).ToJSON();
                    break;
                case "InitGridData":
                    res = GetInitGridData(pContext).ToJSON();
                    break;
                case "GetModuleColumn":
                    res = GetModuleColumn().ToJSON();
                    break;
                case "GetList":
                    res = GetList(pContext.Request.Form);
                    break;
                case "ExportUserList"://导出数据
                    ExportUserList(pContext);
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["id"]);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetPageData
        private PageResultEntity GetPageData(HttpContext pContext)
        {
            XieHuiBaoBLL b = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();
            }

            int pageSize = pContext.Request["limit"].ToInt();
            int pageIndex = pContext.Request["page"].ToInt();

            return b.GetPageData(l, pageSize, pageIndex - 1, 2);
        }
        #endregion

        #region GetInitGridData
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private GridInitEntity GetInitGridData(HttpContext pContext)
        {
            string pEventID = "";
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["pEventID"]))
            {
                pEventID = pContext.Request.QueryString["pEventID"];
            }
            GridInitEntity g = new GridInitEntity();
            g.GridDataDefinds = GetGridDataModels(pContext);
            g.GridColumnDefinds = GetGridColumns(pContext);

            return g;
        }


        #region GetModuleColumn
        public string GetModuleColumn()
        {
            XieHuiBaoBLL b = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            return b.GetModuleColumn().ToJSON();
        }
        #endregion

        #endregion

        #region  2014-04-21 修改者：tiansheng.zhu
        #region GetPageDataByEventID
        private PageResultEntity GetPageDataByEventID(HttpContext pContext)
        {
            XieHuiBaoBLL b = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            string pSearch = pContext.Request["pSearch"];
            string pEventID = "";
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["pEventID"]))
            {
                pEventID = pContext.Request.QueryString["pEventID"];
            }
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();
            }
            int pageSize = pContext.Request["limit"].ToInt();
            int pageIndex = pContext.Request["page"].ToInt();
            return b.GetPageDataByEventID(l, pageSize, pageIndex - 1, pEventID);
        }
        #endregion

        #region GetInitGridDataByEventID 获取列的模型和列
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private GridInitEntity GetInitGridDataByEventID(HttpContext pContext)
        {
            string pEventID = "";
            GridInitEntity g = new GridInitEntity();
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["pEventID"]))
            {
                pEventID = pContext.Request.QueryString["pEventID"];
                g.GridDataDefinds = GetGridDataModelsByEventID(pEventID);
                g.GridColumnDefinds = GetGridColumnsByEventID(pEventID);
            }
            return g;
        }
        #endregion

        #region  GetGridColumnsByEventID 获取配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="pEventID"></param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumnsByEventID(string pEventID)
        {
            XieHuiBaoBLL bll = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            return bll.GetGridColumnsByEventID(pEventID);
        }
        #endregion

        #region GetGridDataModelsByEventID 获取列的模型
        /// <summary>
        /// 获取列的模型
        /// </summary>
        /// <param name="pEventID"></param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModelsByEventID(string pEventID)
        {
            XieHuiBaoBLL bll = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            return bll.GetGridDataModelsByEventID(pEventID);
        }
        #endregion      

        #region ExportUserListByEventID 导出数据
        public void ExportUserListByEventID(HttpContext pContext)
        {
            XieHuiBaoBLL b = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            string pSearch = pContext.Request["pSearch"];
            string pEventID = "";
            if (!string.IsNullOrEmpty(pContext.Request.QueryString["pEventID"]))
            {
                pEventID = pContext.Request.QueryString["pEventID"];
            }
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();
            }
            PageResultEntity pageEntity = b.GetPageDataByEventID(l, 100000000, 0, pEventID);

            GridInitEntity g = GetInitGridDataByEventID(pContext);

            if (pageEntity != null && pageEntity.GridData != null)
            {
                #region 替换标题信息
                if (g != null && g.GridColumnDefinds != null)
                {
                    if (pageEntity.GridData.Columns.Contains("ROW_NUMBER"))
                    {
                        pageEntity.GridData.Columns.Remove("ROW_NUMBER");
                    }
                    if (pageEntity.GridData.Columns.Contains("SignUpID"))
                    {
                        pageEntity.GridData.Columns.Remove("SignUpID");
                    }                  
                    for (int i = 0; i < pageEntity.GridData.Columns.Count; i++)
                    {
                        for (int j = 0; j < g.GridColumnDefinds.Count; j++)
                        {
                            if (pageEntity.GridData.Columns[i].ColumnName.ToLower() == g.GridColumnDefinds[j].DataIndex.ToLower())
                            {
                                pageEntity.GridData.Columns[i].ColumnName = g.GridColumnDefinds[j].ColumnText;
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            //数据获取        
            Workbook wb = DataTableExporter.WriteXLS(pageEntity.GridData, 0);
            wb.Worksheets[0].Name = "参加活动人员信息";
            string savePath = pContext.Server.MapPath(@"~/File/Excel");
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            savePath = savePath+"/参加活动人员信息" + DateTime.Now.ToString("mmssms") + ".xls";
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(pContext, savePath);
        }
        #endregion
     
        #endregion

        #region GetGridColumns
        /// <summary>
        /// 获取终端列表表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumns(HttpContext pContext)
        {
            XieHuiBaoBLL bll = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            return bll.GetGridColumns(2);
        }
        #endregion

        #region GetGridDataModels        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModels(HttpContext pContext)
        {
            XieHuiBaoBLL bll = new XieHuiBaoBLL(CurrentUserInfo, "vip");
            return bll.GetGridDataModels(2);
        }
        #endregion

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

            PagedQueryResult<VipViewEntity> pagedQueryEntity = new XieHuiBaoBLL(CurrentUserInfo, "vip").GetUserList(pParems, pageIndex, pageSize);

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
            DataTable result = new XieHuiBaoBLL(CurrentUserInfo, "vip").ExportUserList(pParems).Tables[0];
            result.TableName = "参加活动人员信息";
            Workbook wb = DataTableExporter.WriteXLS(result, 0);
            string savePath = pContext.Server.MapPath(@"~/File/Excel/" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ms") + ".xls");
            wb.Save(savePath);//保存Excel文件
            new ExcelCommon().OutPutExcel(pContext, savePath);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ids">要删除的id</param>
        /// <returns>返回json</returns>
        private string Delete(string ids)
        {
            string res = "{success:false}";
            new XieHuiBaoBLL(CurrentUserInfo, "vip").DeleteEventVipMapping(ids);
            res = "{success:true}";
            return res;
        }
        #endregion
    }
}
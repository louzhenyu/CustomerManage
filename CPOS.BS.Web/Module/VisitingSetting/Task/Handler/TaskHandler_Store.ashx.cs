using System;
using System.Collections.Generic;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.PageBase;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.Module.VisitingSetting.Task.Handler
{
    /// <summary>
    /// TaskHandler_Store 的摘要说明
    /// </summary>
    public class TaskHandler_Store : JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.BTNCode)
            {
                case "search":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "InitGridData": //初使化终端编辑列表的数据
                                res = GetInitGridData(pContext).ToJSON();
                                break;
                            case "PageGridData": //查询数据分页查询
                                res = GetStorePageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res = GetStoreQueryConditionControls(pContext).ToJSON();
                                break;
                        }
                    }
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "InitGridData": //初使化终端编辑列表的数据
                                res = GetInitGridData(pContext).ToJSON();
                                break;
                            case "PageGridData": //查询数据分页查询
                                res = GetStorePageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res =GetStoreQueryConditionControls(pContext).ToJSON();
                                break;
                            case "EditTaskPOP_Store":
                                res = EditTaskPOP_Store(pContext);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "InitGridData": //初使化终端编辑列表的数据
                                res = GetInitGridData(pContext).ToJSON();
                                break;
                            case "PageGridData": //查询数据分页查询
                                res = GetStorePageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res = GetStoreQueryConditionControls(pContext).ToJSON();
                                break;
                            case "EditTaskPOP_Store":
                                res = EditTaskPOP_Store(pContext);
                                break;
                        }
                    }
                    break;

            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetInitGridData
        private GridInitEntity GetInitGridData(HttpContext pContext)
        {
            GridInitEntity g = new GridInitEntity();

            g.GridDataDefinds = GetStoreGridDataModels(pContext);
            g.GridColumnDefinds = GetStoreGridColumns(pContext);
            //g.GridDatas = GetStorePageData(pContext);
            return g;

        }
        #endregion
        #region GetStorePageData
        public PageResultEntity GetStorePageData(HttpContext pContext)
        {
            VisitingPOPMappingBLL_Store b = new VisitingPOPMappingBLL_Store(CurrentUserInfo, "Store");
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }

            int? pPageIndex = null;
            int? pPageSize = null;
            if (!string.IsNullOrEmpty(pContext.Request["pPageIndex"]))
            {
                pPageIndex = Convert.ToInt32(pContext.Request["pPageIndex"]);
            }
            if (!string.IsNullOrEmpty(pContext.Request["pPageSize"]))
            {
                pPageSize = Convert.ToInt32(pContext.Request["pPageSize"]);
            }
            return b.GetTaskStoreList(l, pPageSize, pPageIndex, pContext.Request["CorrelationValue"]);


        }
        #endregion
        #region EditTaskPOP_Store
        private string EditTaskPOP_Store(HttpContext pContext)
        {
            string pSearch = pContext.Request["sCondition"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            int isAutoFill = pContext.Request["isAutoFill"] == "true" ? 1 : 0;

            Guid taskid = Guid.Parse(pContext.Request["id"]);
            int allSelectorStatus = pContext.Request["allSelectorStatus"].ToInt();
            string defaultList = pContext.Request["defaultList"];//关联表有的数据
            string includeList = pContext.Request["includeList"];//新加的数据
            string excludeList = pContext.Request["excludeList"];//排除的数据

            new VisitingPOPMappingBLL_Store(CurrentUserInfo, "Store").EditTaskPOP_Store(isAutoFill, l, pSearch, taskid, allSelectorStatus, defaultList, includeList, excludeList);
            return "{success:true,msg:'编辑成功'}";
        }
        #endregion


        /// <summary>
        /// 获取终端列表数据定义模型
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetStoreGridDataModels(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetGridDataModels();

        }
        /// <summary>
        /// 获取终端列表表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<GridColumnEntity> GetStoreGridColumns(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetGridColumns();

        }
        /// <summary>
        /// 获取终端查询控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public List<DefindControlEntity> GetStoreQueryConditionControls(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(CurrentUserInfo, "Store");
            return b.GetQueryConditionControls();

        }
    }
    public class GridInitEntity
    {
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

    }
}
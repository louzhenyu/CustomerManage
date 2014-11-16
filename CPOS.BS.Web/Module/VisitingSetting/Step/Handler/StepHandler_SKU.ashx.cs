using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Module.VisitingSetting.Task.Handler;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Extension;

namespace JIT.CPOS.BS.Web.Module.VisitingSetting.Step.Handler
{
    /// <summary>
    /// StepHandler_SKU 的摘要说明
    /// </summary>
    public class StepHandler_SKU : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
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
                                res = GetPageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res =  new SKUModuleCRUDBLL(CurrentUserInfo, "T_Sku").GetQueryConditionControls().ToJSON();
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
                                res = GetPageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res =  new SKUModuleCRUDBLL(CurrentUserInfo, "T_Sku").GetQueryConditionControls().ToJSON();
                                break;
                            case "EditStepObject_SKU": //得到查询控件
                                res = EditStepObject_SKU(pContext);
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
                                res = GetPageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res = new SKUModuleCRUDBLL(CurrentUserInfo, "T_Sku").GetQueryConditionControls().ToJSON();
                                break;
                            case "EditStepObject_SKU": //得到查询控件
                                res = EditStepObject_SKU(pContext);
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
            GridInitEntity g= new GridInitEntity();
            g.GridDataDefinds = new StoreDefindModuleBLL(CurrentUserInfo, "T_Sku").GetGridDataModels();
            g.GridColumnDefinds = new StoreDefindModuleBLL(CurrentUserInfo, "T_Sku").GetGridColumns();
            //g.GridDatas = GetPageData(pContext);
            return g;
        }
        #endregion
        #region GetPageData
        private PageResultEntity GetPageData(HttpContext pContext)
        {
            VisitingTaskStepObjectBLL_SKU b = new VisitingTaskStepObjectBLL_SKU(CurrentUserInfo, "T_Sku");
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
            return b.GetStepSKUList(l, pPageSize, pPageIndex, pContext.Request["CorrelationValue"]);
        }
        #endregion
        #region EditStepObject_SKU
        private string EditStepObject_SKU(HttpContext pContext)
        {
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }

            Guid stepid = Guid.Parse(pContext.Request["id"]);
            int allSelectorStatus = pContext.Request["allSelectorStatus"].ToInt();
            string defaultList = pContext.Request["defaultList"];//关联表有的数据
            string includeList = pContext.Request["includeList"];//新加的数据
            string excludeList = pContext.Request["excludeList"];//排除的数据

            new VisitingTaskStepObjectBLL_SKU(CurrentUserInfo, "T_Sku").EditStepObject_SKU(l, stepid, allSelectorStatus, defaultList, includeList, excludeList);
            return "{success:true,msg:'编辑成功'}";
        }
        #endregion

        
    }
}
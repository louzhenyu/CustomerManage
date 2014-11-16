using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Extension;
using System.Collections.Specialized;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using System.Web.Script.Serialization;

namespace JIT.CPOS.BS.Web.Module.VisitingSetting.Task.Handler
{
    /// <summary>
    /// TaskHandler_DIS 的摘要说明
    /// </summary>
    public class TaskHandler_Distributor : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request["btncode"])
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
                                res = new JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler().GetQueryConditionControls(pContext).ToJSON();
                                break;
                        }
                    }
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(pContext.Request["method"]))
                    {
                        switch (pContext.Request["method"])
                        {
                            case "InitGridData": //初使化终端编辑列表的数据
                                res = GetInitGridData(pContext).ToJSON();
                                break;
                            case "PageGridData": //查询数据分页查询
                                res = GetStorePageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res = new JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler().GetQueryConditionControls(pContext).ToJSON();
                                break;
                            case "EditTaskPOP_Distributor":
                                res = EditTaskPOP_Distributor(pContext);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request["method"]))
                    {
                        switch (pContext.Request["method"])
                        {
                            case "InitGridData": //初使化终端编辑列表的数据
                                res = GetInitGridData(pContext).ToJSON();
                                break;
                            case "PageGridData": //查询数据分页查询
                                res = GetStorePageData(pContext).ToJSON();
                                break;
                            case "QueryView": //得到查询控件
                                res = new JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler().GetQueryConditionControls(pContext).ToJSON();
                                break;
                            case "EditTaskPOP_Distributor":
                                res = EditTaskPOP_Distributor(pContext);
                                break;
                        }
                    }
                    break;

            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetInitGridData
        private JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler.GridInitEntity GetInitGridData(HttpContext pContext)
        {
            JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler.GridInitEntity g
                = new JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler.GridInitEntity();

            g.GridDataDefinds = new JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler().GetGridDataModels(pContext);
            g.GridColumnDefinds =new JIT.CPOS.BS.Web.Module.BasicData.Distributor.Handler.DistributorHandler().GetGridColumns(pContext);
            //g.GridDatas = GetStorePageData(pContext);
            return g;

        }
        #endregion
        #region GetStorePageData
        public PageResultEntity GetStorePageData(HttpContext pContext)
        {
            VisitingPOPMappingBLL_Distributor b = new VisitingPOPMappingBLL_Distributor(CurrentUserInfo, "Distributor");
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
            return b.GetTaskDistributorList(l, pPageSize, pPageIndex, pContext.Request["CorrelationValue"]);


        }
        #endregion
        #region EditTaskPOP_Distributor
        private string EditTaskPOP_Distributor(HttpContext pContext)
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

            new VisitingPOPMappingBLL_Distributor(CurrentUserInfo, "Distributor").EditTaskPOP_Distributor(isAutoFill, l, pSearch, taskid, allSelectorStatus, defaultList, includeList, excludeList);
            return "{success:true,msg:'编辑成功'}";
        }
        #endregion
    }
}
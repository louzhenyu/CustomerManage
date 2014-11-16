using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.Web.Module.VisitingPlan.CallDayPlanning.Handler
{
    /// <summary>
    /// CallDayPlanning_StoreHandler 的摘要说明
    /// </summary>
    public class CallDayPlanning_StoreHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                                res = new JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler().GetStoreQueryConditionControls(pContext).ToJSON();
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
                                res = new JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler().GetStoreQueryConditionControls(pContext).ToJSON();
                                break;
                            case "EditRoutePOPList_Store":
                                res = EditRoutePOPList_Store(pContext);
                                break;
                            case "EditRoutePOPMap_Store":
                                res = EditRoutePOPMap_Store(pContext);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetInitGridData
        private JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler.GridInitEntity GetInitGridData(HttpContext pContext)
        {
            JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler.GridInitEntity g
                = new JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler.GridInitEntity();

            g.GridDataDefinds = new JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler().GetStoreGridDataModels(pContext);
            //new
            GridColumnModelEntity entity = new GridColumnModelEntity();
            entity.DataType = 1;
            entity.DataIndex = "MappingID";
            g.GridDataDefinds.Add(entity);

            entity = new GridColumnModelEntity();
            entity.DataType = 3;
            entity.DataIndex = "Sequence";
            g.GridDataDefinds.Add(entity);

            g.GridColumnDefinds = new JIT.CPOS.BS.Web.Module.BasicData.Store.Handler.StoreHandler().GetStoreGridColumns(pContext);
            //new
            //GridColumnEntity entity1 = new GridColumnEntity();
            //entity1.ColumnText = "MappingID";
            //entity1.DataIndex = "MappingID";
            //g.GridColumnDefinds.Add(entity1);

            //entity1 = new GridColumnEntity();
            //entity1.ColumnText = "Sequence";
            //entity1.DataIndex = "Sequence";
            //g.GridColumnDefinds.Add(entity1);

            //g.GridDatas = GetStorePageData(pContext);
            return g;
        }
        #endregion
        #region GetStorePageData
        public PageResultEntity GetStorePageData(HttpContext pContext)
        {
            CallDayPlanningBLL_Store b = new CallDayPlanningBLL_Store(CurrentUserInfo, "Store");
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
            CallDayPlanningViewEntity_POP entity = new CallDayPlanningViewEntity_POP();
            entity.ClientUserID = pContext.Request["CorrelationValue"].Split('|')[0].ToInt();
            entity.CallDate = pContext.Request["CorrelationValue"].Split('|')[1].ToDateTime();
            return b.GetUserCDPStoreList(l, pPageSize, pPageIndex, entity);
        }
        #endregion
        #region EditRoutePOPList_Store
        private string EditRoutePOPList_Store(HttpContext pContext)
        {
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            CallDayPlanningViewEntity_POP infoEntity = new CallDayPlanningViewEntity_POP();
            infoEntity.ClientUserID = pContext.Request["id"].Split('|')[0].ToInt();
            infoEntity.CallDate = pContext.Request["id"].Split('|')[1].ToDateTime();

            CallDayPlanningViewEntity_POP[] entity = pContext.Request["form"].DeserializeJSONTo<CallDayPlanningViewEntity_POP[]>();

            new CallDayPlanningBLL_Store(CurrentUserInfo, "Store").EditUserCDPPOPList_Store(infoEntity, l, entity);
            return "{success:true}";
        }
        #endregion
        #region EditRoutePOPMap_Store
        private string EditRoutePOPMap_Store(HttpContext pContext)
        {
            string pSearch = pContext.Request["pSearch"];
            List<DefindControlEntity> l = new List<DefindControlEntity>();
            if (!string.IsNullOrEmpty(pSearch))
            {
                l = pSearch.DeserializeJSONTo<List<DefindControlEntity>>();

            }
            CallDayPlanningViewEntity_POP infoEntity = new CallDayPlanningViewEntity_POP();
            infoEntity.ClientUserID = pContext.Request["id"].Split('|')[0].ToInt();
            infoEntity.CallDate = pContext.Request["id"].Split('|')[1].ToDateTime();

            CallDayPlanningViewEntity_POP[] entity = pContext.Request["form"].DeserializeJSONTo<CallDayPlanningViewEntity_POP[]>();
            string deleteList = pContext.Request["deleteList"];
            new CallDayPlanningBLL_Store(CurrentUserInfo, "Store").EditUserCDPPOPMap_Store(infoEntity, l, entity, deleteList);
            return "{success:true}";
        }
        #endregion
    }
}
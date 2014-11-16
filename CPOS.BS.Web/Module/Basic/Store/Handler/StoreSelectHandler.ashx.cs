
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.Reflection;
using System.Web.SessionState;

namespace JIT.CPOS.BS.Web.Module.BasicData.Store.Handler
{
    /// <summary>
    /// StoreSelectHandler 的摘要说明
    /// </summary>
    public class StoreSelectHandler : IHttpHandler, IRequiresSessionState
    {
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

      
        /// <summary>
        /// 初始化终端点选控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private GridInitEntity GetInitSelectGridData(HttpContext pContext)
        {
            GridInitEntity g = new GridInitEntity();
            g.GridDataDefinds = GetStoreGridDataModels(pContext);
            g.GridColumnDefinds = GetStoreGridColumns(pContext);
           // g.GridDatas = GetStoreSelectPageData(pContext);
            return g;

        }
       
        /// <summary>
        /// 获取终端查询控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<DefindControlEntity> GetStoreQueryConditionControls(HttpContext pContext)
        {
            StoreDefindModuleBLL b = new StoreDefindModuleBLL(new SessionManager().CurrentUserLoginInfo, "Store");
            return b.GetQueryConditionControls();

        }

        /// <summary>
        /// 获取终端列表数据定义模型
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<GridColumnModelEntity> GetStoreGridDataModels(HttpContext pContext)
        {
            StoreContorlSelectBLL b = new StoreContorlSelectBLL(new SessionManager().CurrentUserLoginInfo, "Store");
            return b.GetGridDataModels();

        }
        /// <summary>
        /// 获取终端列表表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<GridColumnEntity> GetStoreGridColumns(HttpContext pContext)
        {
            StoreContorlSelectBLL b = new StoreContorlSelectBLL(new SessionManager().CurrentUserLoginInfo, "Store");
            return b.GetGridColumns();

        }
        /// <summary>
        /// 用于点选控件编辑显示的值
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private DataTable GetSelectData(HttpContext pContext)
        {
            StoreContorlSelectBLL b = new StoreContorlSelectBLL(new SessionManager().CurrentUserLoginInfo, "Store");
            string pKeyValue = pContext.Request["pKeyValue"];
            return b.GetSelectData(pKeyValue);

        }
        /// <summary>
        /// 点选终端分页查询控件
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private PageResultEntity GetStoreSelectPageData(HttpContext pContext)
        {
            StoreContorlSelectBLL b = new StoreContorlSelectBLL(new SessionManager().CurrentUserLoginInfo, "Store");
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
            if (!string.IsNullOrEmpty(pContext.Request["pKeyValue"]))
            {
                string pKeyValue = pContext.Request["pKeyValue"];
                return b.GetSelectPageData(l, pPageSize, pPageIndex, pKeyValue);

            }
            else
            {
                return b.GetStorePageData(l, pPageSize, pPageIndex);
            }



        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region IHttpHandler 成员


        public void ProcessRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request["method"])
            {
                case "InitGridData"://点选控件初始化
                    res = GetInitSelectGridData(pContext).ToJSON();
                    break;
                case "PageGridData": //分面查询数据
                    res = GetStoreSelectPageData(pContext).ToJSON();
                    break;
                case "QueryView": //得到查询控件
                    res = GetStoreQueryConditionControls(pContext).ToJSON();
                    break;
                case "selectSetValue": //根KEY值得到显示的值
                    res = GetSelectData(pContext).ToJSON();
                    break;
            }


            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #endregion
    }
}
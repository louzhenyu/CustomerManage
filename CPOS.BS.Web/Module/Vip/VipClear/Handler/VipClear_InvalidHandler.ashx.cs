using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.Pos;
using System.Collections;
using JIT.CPOS.BS.BLL.Module.BasicData;

namespace JIT.CPOS.BS.Web.Module.Vip.VipClear.Handler
{
    /// <summary>
    /// VipClear_InvalidHandler 的摘要说明
    /// </summary>
    public class VipClear_InvalidHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "InitGridData": //初使化终端编辑列表的数据
                    res = GetInitGridData(pContext).ToJSON();
                    break;
                case "PageGridData": //查询数据分页查询
                    res = GetPageData(pContext).ToJSON();
                    break;
                case "QueryView": //得到查询控件
                    res = new StoreDefindModuleBLL(CurrentUserInfo, "VIP").GetEditControls().ToJSON();
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetInitGridData
        /// <summary>
        /// 初使化终端编辑列表的数据
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private JIT.CPOS.BS.Web.Module.VisitingSetting.Task.Handler.GridInitEntity GetInitGridData(HttpContext pContext)
        {
            JIT.CPOS.BS.Web.Module.VisitingSetting.Task.Handler.GridInitEntity g = new JIT.CPOS.BS.Web.Module.VisitingSetting.Task.Handler.GridInitEntity();
            g.GridDataDefinds = GetGridDataModels(pContext);
            g.GridColumnDefinds = GeGridColumns(pContext);
            return g;
        }
        #endregion

        #region GetGridDataModels
        /// <summary>
        /// 获取终端列表数据定义模型
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<GridColumnModelEntity> GetGridDataModels(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, "VIP");
            return b.GetGridDataModels();
        }
        #endregion

        #region GeGridColumns
        /// <summary>
        /// 获取表头定义
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private List<GridColumnEntity> GeGridColumns(HttpContext pContext)
        {
            VipBLLByNew b = new VipBLLByNew(CurrentUserInfo, "VIP");
            return b.GetGridColumns();
        }
        #endregion

        #region GetPageData
        /// <summary>
        /// 查询数据分页查询
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private PageResultEntity GetPageData(HttpContext pContext)
        {
            VipClear_InvalidBLL b = new VipClear_InvalidBLL(CurrentUserInfo, "VIP");
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
            return b.GetVIPClearList(l, pPageSize, pPageIndex, pContext.Request["CorrelationValue"]);
        }
        #endregion
        
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
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
using System.Data;
using JIT.CPOS.BS.BLL.Vip;

namespace JIT.CPOS.BS.Web.Module.Vip.VipClear.Handler
{
    public class GridGroupEntity
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
        /// 会员数据
        /// </summary>
        public DataTable VipDatas { get; set; }
        /// <summary>
        /// 分组数据
        /// </summary>
        public DataTable VipCroupDatas { get; set; }
        /// <summary>
        /// 分组记录数
        /// </summary>
        public DataTable VipCroupRows { get; set; }
    }

    /// <summary>
    /// VipClearHandler 的摘要说明
    /// </summary>
    public class VipClearHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "SearchAllList":
                    res = GetVipCliearList(pContext.Request.Form);
                    break;
                case "SearchGroup":
                    res = GetVipGroup(pContext);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }
        private string GetVipGroup(HttpContext pContext)
        {
            VIPClearBLLByMB vnm = new VIPClearBLLByMB(this.CurrentUserInfo, "VIP");
            int pVIPClearID = Convert.ToInt32(pContext.Request["pVIPClearID"]);
            int pPageSize = Convert.ToInt32(pContext.Request["pPageSize"]);
            int pPageInex = Convert.ToInt32(pContext.Request["pPageInex"]);
            DataSet dt = vnm.GetGroupData(pVIPClearID, pPageSize, pPageInex);
            GridGroupEntity gg = new GridGroupEntity();
            gg.GridDataDefinds = vnm.GetGridDataModels();
            gg.GridColumnDefinds = vnm.GetGridColumns();
            gg.VipCroupRows = dt.Tables[0];
            gg.VipCroupDatas = dt.Tables[1];
            gg.VipDatas = dt.Tables[2];
            return gg.ToJSON();

        }
        #region GetVipCliearList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        private string GetVipCliearList(NameValueCollection rParams)
        {
            //组装参数
            #region 组装参数
            Dictionary<string, string> pParems = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(rParams["pStartDate"]))
            {
                pParems.Add("pStartDate", rParams["pStartDate"]);
            }
            if (!string.IsNullOrEmpty(rParams["pEndDate"]))
            {
                pParems.Add("pEndDate", rParams["pEndDate"]);
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            #endregion

            //获取数据
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VipClearBLL(CurrentUserInfo).GetVipCliearList(pParems, pageIndex, pageSize, out rowCount).Entities.ToJSON(), rowCount);
        }
        #endregion

    }
}
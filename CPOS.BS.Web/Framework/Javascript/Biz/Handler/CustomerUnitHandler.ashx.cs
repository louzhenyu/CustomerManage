using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Web.SessionState;
using JIT.Utility.Web.ComponentModel;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// CustomerUnitHandler 的摘要说明
    /// </summary>
    public class CustomerUnitHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler,
        IHttpHandler, IRequiresSessionState
    {

        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="context"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request["method"])
            {
                case "get_cusomer_unit":
                    content = GetSupplierUnit();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

       

        #region GetSupplierUnit
        /// <summary>
        /// 判断品牌是否有树
        /// </summary>
        /// <param name="pParentID">传入</param>
        /// <returns>success是代表存在树</returns>
        private string GetSupplierUnit()
        {
            string typeCode = "客户";
            UnitService unitService = new UnitService(new SessionManager().CurrentUserLoginInfo);
            IList<UnitInfo> units;

            string key = string.Empty;
            string content = string.Empty;
            if (Request("id") != null && Request("id") != string.Empty)
            {
                key = Request("id").ToString().Trim();
            }

            if (key == "root" || key.Length == 0)
            {
                units = unitService.GetUnitInfoListByTypeCode(typeCode);
            }
            else
            {
                units = unitService.GetSubUnitsByDefaultRelationMode(key);
            }

            var jsonData = new JsonData();
            jsonData.totalCount = units.Count.ToString();
            jsonData.data = units;

            content = jsonData.ToJSON();
            return content;
        }


        
        /// <summary>
        /// 循环读取节点数据
        /// </summary>
        /// <param name="pModel">节点对象</param>
        /// <param name="pBrand">渠道对象集合</param>
        /// <param name="pBrandID">上一级的编号</param>
        /// <returns>返回节点对象信息</returns>
        private TreeNodes GetBrandTreeNodes(TreeNodes pModel, ControlBrandEntity[] pBrand, string pBrandID)
        {
            ControlBrandEntity[] Brand = pBrand.Where(i => i.ParentID.ToString() == pBrandID).ToArray();
            for (int i = 0; i < Brand.Length; i++)
            {
                if (Brand[i].ParentID.ToString() == pBrandID)
                {
                    pModel.Add(Brand[i].BrandID.ToString(), Brand[i].BrandName, Brand[i].ParentID.ToString(), true);
                    GetBrandTreeNodes(pModel, pBrand, Brand[i].BrandID.ToString());
                }
            }
            return pModel;
        }
        #endregion

        #region 初始化树控件的参数
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="pContext"></param>
        private void InitParams(HttpContext pContext, out string pParentNodeID,
            out bool pIsMultiSelect, out bool pIsSelectLeafOnly, out  TreeNode[] pModel)
        {
            pParentNodeID = "";
            pIsMultiSelect = false;
            pIsSelectLeafOnly = false;
            pModel = null;
            //父节点ID
            pParentNodeID = pContext.Request.QueryString["node"];
            //是否为多选
            string strMultiSelect = pContext.Request.QueryString["multiSelect"];
            if (!string.IsNullOrEmpty(strMultiSelect))
            {
                bool temp = false;
                if (bool.TryParse(strMultiSelect, out temp))
                {
                    pIsMultiSelect = temp;
                }
            }
            //是否只能选择叶子节点
            string strIsSelectLeafOnly = pContext.Request.QueryString["isSelectLeafOnly"];
            if (!string.IsNullOrEmpty(strIsSelectLeafOnly))
            {
                bool temp = false;
                if (bool.TryParse(strIsSelectLeafOnly, out temp))
                {
                    pIsSelectLeafOnly = temp;
                }
            }
            //初始值
            string strInitValues = pContext.Request.QueryString["initValues"];
            if (!string.IsNullOrEmpty(strInitValues))
            {
                pModel = strInitValues.DeserializeJSONTo<TreeNode[]>();
            }
        }
        #endregion

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
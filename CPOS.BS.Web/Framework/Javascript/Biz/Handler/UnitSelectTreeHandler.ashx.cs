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
    /// UnitSelectTreeHandler 的摘要说明
    /// </summary>
    public class UnitSelectTreeHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSTreeHandler, 
        IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 获取子节点数据
        /// </summary>
        /// <param name="pParentNodeID">父节点ID</param>
        /// <returns></returns>
        protected override TreeNodes GetNodes(string pParentNodeID)
        {
            TreeNodes nodes = new TreeNodes();
            //nodes.Add("1", "上海市", null, false);
            //nodes.Add("2", "普陀区", "1", true);
            //nodes.Add("3", "长宁区", "1", true);
            //nodes.Add("4", "静安区", "1", true);
            //nodes.Add("5", "浦东新区", "1", true);
            //nodes.Add("6", "浙江省", null, false);
            //nodes.Add("7", "杭州市", "6", true);
            //nodes.Add("8", "1浙江省", null, true);
            //nodes.Add("9", "2杭州市", "8", true);
            //nodes.Add("10", "3杭州市", "9", true);
            //nodes.Add("11", "1杭州市", null, false);

            string typeCode = "总部";
            UnitService unitService = new UnitService(new SessionManager().CurrentUserLoginInfo);
            LoggingSessionInfo loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            IList<UnitInfo> units;

            string key = string.Empty;
            string content = string.Empty;
            key = pParentNodeID;

            var userUnitId = new SessionManager().CurrentUserLoginInfo.CurrentUserRole.UnitId;

            if (key == null || key == "-1" || key == "root" || key.Length == 0) //如果父节点为空，就取总部的数据
            {
               // units = unitService.GetUnitInfoListByTypeCode(typeCode);//这是原来的方法，就是extjs里只取根节点的方法，
                //现在需要根据用户的userID和customerID来取他权限下面的角色
                //一次吧下面所有的门店信息都取出来了
                units = unitService.GetUnitByUser(loggingSessionInfo.ClientID, loggingSessionInfo.UserID);//获取当前登录人的门店
            }
            else
            {
                units = unitService.GetSubUnitsByDefaultRelationMode(key);
            }

            foreach (var item in units)
            {
                nodes.Add(item.Id, item.Name, item.Parent_Unit_Id, false);
              //  AddChildNodes(nodes, item.Id);  //把子节点也添加进来
            }
            return nodes;
        }


        public void AddChildNodes(TreeNodes nodes,string ParentID)
        {
            UnitService unitService = new UnitService(new SessionManager().CurrentUserLoginInfo);
            IList<UnitInfo> units;
            units = unitService.GetSubUnitsByDefaultRelationMode(ParentID);
            foreach (var item in units)
            {
                nodes.Add(item.Id, item.Name, item.Parent_Unit_Id, false);
                AddChildNodes(nodes, item.Id);
            }

        }

        ///// <summary>
        ///// 页面入口
        ///// </summary>
        ///// <param name="context"></param>
        //protected override void AjaxRequest(HttpContext pContext)
        //{
        //    string content = "";
        //    switch (pContext.Request["method"])
        //    {
        //        case "get_unit_tree":
        //            content = GetUnitTreeData();
        //            break;
        //    }
        //    pContext.Response.Write(content);
        //    pContext.Response.End();
        //}

        //#region GetUnitTreeData
        ///// <summary>
        ///// GetUnitTreeData
        ///// </summary>
        //private string GetUnitTreeData()
        //{
        //    string typeCode = "总部";
        //    UnitService unitService = new UnitService(new SessionManager().CurrentUserLoginInfo);
        //    IList<UnitInfo> units;

        //    string key = string.Empty;
        //    string content = string.Empty;
        //    if (Request("id") != null && Request("id") != string.Empty)
        //    {
        //        key = Request("id").ToString().Trim();
        //    }

        //    var userUnitId = new SessionManager().CurrentUserLoginInfo.CurrentUserRole.UnitId;

        //    if (key == "root" || key.Length == 0)
        //    {
        //        units = unitService.GetUnitInfoListByTypeCode(typeCode);
        //    }
        //    else
        //    {
        //        units = unitService.GetSubUnitsByDefaultRelationMode(key);
        //    }

        //    string pParentNodeID = "";
        //    bool pIsMultiSelect = false;
        //    bool pIsSelectLeafOnly = false;
        //    TreeNode[] pModel = null;
        //    InitParams(HttpContext.Current, out pParentNodeID, out pIsMultiSelect,
        //        out pIsSelectLeafOnly, out pModel);
        //    TreeNodes nodes = new TreeNodes();
        //    foreach (var item in units)
        //    {
        //        nodes.Add(item.Id, item.Name, item.Parent_Unit_Id, false);
        //    }

        //    content = nodes.ToTreeStoreJSON(pIsMultiSelect, pIsSelectLeafOnly, pModel);
        //    return content;


        //    //ControlUnitSelectTreeEntity[] pUnitSelectTree = new ControlBLL(new SessionManager().CurrentUserLoginInfo).
        //    //    GetUnitSelectTreeByClientID(pParentID);
        //    //if (pTree == "success")
        //    //{
        //    //    string pParentNodeID = "";
        //    //    bool pIsMultiSelect = false;
        //    //    bool pIsSelectLeafOnly = false;
        //    //    TreeNode[] pModel = null;
        //    //    InitParams(HttpContext.Current, out pParentNodeID, out pIsMultiSelect, 
        //    //        out pIsSelectLeafOnly, out pModel);
        //    //    TreeNodes nodes = new TreeNodes();
        //    //    if (pUnitSelectTree != null && pUnitSelectTree.Length > 0)
        //    //    {
        //    //        nodes = GetUnitSelectTreeTreeNodes(nodes, pUnitSelectTree, "");
        //    //        return nodes.ToTreeStoreJSON(pIsMultiSelect, pIsSelectLeafOnly, pModel);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return pUnitSelectTree.ToJSON();
        //    //}
        //    //return "";
        //}

        /////// <summary>
        /////// 获取结果集,Json 格式
        /////// </summary>
        /////// <param name="pParentID">传入ParentID</param>
        /////// <param name="pTree">传入</param>
        /////// <returns>Json格式的数据</returns>
        ////private string GetUnitSelectTreeByParentID(string pParentID, string pTree, HttpContext pContext)
        ////{
        ////    ControlUnitSelectTreeEntity[] pUnitSelectTree = new ControlBLL(new SessionManager().CurrentUserLoginInfo).
        ////        GetUnitSelectTreeByClientID(pParentID);
        ////    if (pTree == "success")
        ////    {
        ////        string pParentNodeID = "";
        ////        bool pIsMultiSelect = false;
        ////        bool pIsSelectLeafOnly = false;
        ////        TreeNode[] pModel = null;
        ////        InitParams(pContext, out pParentNodeID, out pIsMultiSelect, out pIsSelectLeafOnly, out pModel);
        ////        TreeNodes nodes = new TreeNodes();
        ////        if (pUnitSelectTree != null && pUnitSelectTree.Length > 0)
        ////        {
        ////            nodes = GetUnitSelectTreeTreeNodes(nodes, pUnitSelectTree, "");
        ////            return nodes.ToTreeStoreJSON(pIsMultiSelect, pIsSelectLeafOnly, pModel);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        return pUnitSelectTree.ToJSON();
        ////    }
        ////    return "";
        ////}

        /////// <summary>
        /////// 循环读取节点数据
        /////// </summary>
        /////// <param name="pModel">节点对象</param>
        /////// <param name="pUnitSelectTree">渠道对象集合</param>
        /////// <param name="pUnitSelectTreeID">上一级的编号</param>
        /////// <returns>返回节点对象信息</returns>
        ////private TreeNodes GetUnitSelectTreeTreeNodes(TreeNodes pModel, ControlUnitSelectTreeEntity[] pUnitSelectTree, string pUnitSelectTreeID)
        ////{
        ////    ControlUnitSelectTreeEntity[] UnitSelectTree = pUnitSelectTree.Where(i => i.ParentID.ToString() == pUnitSelectTreeID).ToArray();
        ////    for (int i = 0; i < UnitSelectTree.Length; i++)
        ////    {
        ////        if (UnitSelectTree[i].ParentID.ToString() == pUnitSelectTreeID)
        ////        {
        ////            pModel.Add(UnitSelectTree[i].UnitSelectTreeID.ToString(), UnitSelectTree[i].UnitSelectTreeName, UnitSelectTree[i].ParentID.ToString(), true);
        ////            GetUnitSelectTreeTreeNodes(pModel, pUnitSelectTree, UnitSelectTree[i].UnitSelectTreeID.ToString());
        ////        }
        ////    }
        ////    return pModel;
        ////}
        //#endregion        

        //#region 初始化树控件的参数
        ///// <summary>
        ///// 初始化参数
        ///// </summary>
        ///// <param name="pContext"></param>
        //private void InitParams(HttpContext pContext, out string pParentNodeID, 
        //    out bool pIsMultiSelect, out bool pIsSelectLeafOnly, out TreeNode[] pModel)
        //{
        //    pParentNodeID = "";
        //    pIsMultiSelect = false;
        //    pIsSelectLeafOnly = false;
        //    pModel = null;
        //    //父节点ID
        //    pParentNodeID = pContext.Request.QueryString["node"];
        //    //是否为多选
        //    string strMultiSelect = pContext.Request.QueryString["multiSelect"];
        //    if (!string.IsNullOrEmpty(strMultiSelect))
        //    {
        //        bool temp = false;
        //        if (bool.TryParse(strMultiSelect, out temp))
        //        {
        //            pIsMultiSelect = temp;
        //        }
        //    }
        //    //是否只能选择叶子节点
        //    string strIsSelectLeafOnly = pContext.Request.QueryString["isSelectLeafOnly"];
        //    if (!string.IsNullOrEmpty(strIsSelectLeafOnly))
        //    {
        //        bool temp = false;
        //        if (bool.TryParse(strIsSelectLeafOnly, out temp))
        //        {
        //            pIsSelectLeafOnly = temp;
        //        }
        //    }
        //    //初始值
        //    string strInitValues = pContext.Request.QueryString["initValues"];
        //    if (!string.IsNullOrEmpty(strInitValues))
        //    {
        //        pModel = strInitValues.DeserializeJSONTo<TreeNode[]>();
        //    }
        //}
        //#endregion

        new public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
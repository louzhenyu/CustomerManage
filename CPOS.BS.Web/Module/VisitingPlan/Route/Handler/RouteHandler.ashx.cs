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
using System.Text;

namespace JIT.CPOS.BS.Web.Module.VisitingPlan.Route.Handler
{
    /// <summary>
    /// RouteHandler 的摘要说明
    /// </summary>
    public class RouteHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                            case "GetRouteList":
                                res = GetRouteList(pContext.Request.Form);
                                break;
                            case "GetRouteByID":
                                res = GetRouteByID(pContext.Request.Form["id"]);
                                break;
                            case "GetCycleList":
                                res = GetCycleList();
                                break;
                            case "GetCycleDetailByCID":
                                res = GetCycleDetailByCID(pContext.Request["cycleid"]);
                                break;
                        }
                    }
                    break;
                case "delete":
                    res = DeleteRoute(pContext.Request.Form["ids"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetRouteByID":
                                res = GetRouteByID(pContext.Request.Form["id"]);
                                break;
                            case "GetCycleList":
                                res = GetCycleList();
                                break;
                            case "GetCycleDetailByCID":
                                res = GetCycleDetailByCID(pContext.Request["cycleid"]);
                                break;
                            case "EditRoute":
                                res = EditRoute(pContext.Request.Form);
                                break;
                            
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetRouteByID":
                                res = GetRouteByID(pContext.Request.Form["id"]);
                                break;
                            case "GetCycleList":
                                res = GetCycleList();
                                break;
                            case "GetCycleDetailByCID":
                                res = GetCycleDetailByCID(pContext.Request["cycleid"]);
                                break;
                            case "EditRoute":
                                res = EditRoute(pContext.Request.Form);
                                break;
                            
                        }
                    }
                    break;
            }
            this.ResponseContent(res);
        }

        #region GetRouteList
        public string GetRouteList(NameValueCollection rParams)
        {
            RouteViewEntity entity = rParams["form"].DeserializeJSONTo<RouteViewEntity>();

            if (!string.IsNullOrEmpty(rParams["ClientStructureID"]))
            {
                entity.ClientStructureID = Guid.Parse(rParams["ClientStructureID"]);
            }
            entity.ClientUserID = rParams["ClientUserID"].ToInt();
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new RouteBLL(CurrentUserInfo).GetRouteList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region DeleteRoute
        public string DeleteRoute(string id)
        {
            string res = "{success:false,msg:\"删除失败\"}";

            new RouteBLL(CurrentUserInfo).DeleteRoute(Guid.Parse(id));

            res = "{success:true}";
            return res;

        }
        #endregion

        #region GetRouteByID
        public string GetRouteByID(string id)
        {
            string res=new RouteBLL(CurrentUserInfo).GetByID(id).ToJSON();
            //获取路线周期 路线人员信息
            RouteCycleMappingEntity[] cyclemapEntity = new RouteBLL(CurrentUserInfo).GetRouteCycle(Guid.Parse(id));
            CycleDetailEntity cycleEntity = new CycleDetailBLL(CurrentUserInfo).GetByID(cyclemapEntity[0].CycleDetailID);
            RouteUserMappingEntity usermapEntity = new RouteBLL(CurrentUserInfo).GetRouteUser(Guid.Parse(id));
            res = res.Replace("}", 
                ",CycleID:\"" + cycleEntity.CycleID 
                + "\",CycleDetailID:\"" + string.Join(",", cyclemapEntity.Select(m => m.CycleDetailID)) 
                + "\",ClientUserID:\"" + usermapEntity.ClientUserID + "\"}");

            return "[" + res + "]";
        }
        #endregion
        #region GetCycleList
        public string GetCycleList()
        {
            return new CycleBLL(CurrentUserInfo).GetCycleList().ToJSON();
        }
        #endregion
        #region GetCycleDetailByCID
        public string GetCycleDetailByCID(string cycleid)
        {
            //这里周期类型如果是周，需要进行一下特殊处理
            CycleEntity cycleEntity = new CycleBLL(CurrentUserInfo).GetByID(Guid.Parse(cycleid));
            CycleDetailEntity[] cycleDetailEntity= new CycleDetailBLL(CurrentUserInfo).GetCycleDetailByCID(Guid.Parse(cycleid));
            StringBuilder res=new StringBuilder();
            if (cycleEntity.CycleType == 1)
            {
                res.Append("[");
                foreach (CycleDetailEntity entity in cycleDetailEntity)
                {
                    res.Append("{CycleDetailID:\"" + entity.CycleDetailID + "\",DayOfCycle:\"星期" + GetWeekDayByNum(entity.DayOfCycle.Value) + "\"},");
                }
                res.Remove(res.Length - 1, 1);
                res.Append("]");
            }
            else
            {
                res.Append(cycleDetailEntity.ToJSON());
            }
            return res.ToString();
        }

        private string GetWeekDayByNum(int num)
        {
            string res = "";
            switch (num)
            {
                case 1:
                    res = "一";
                    break;
                case 2:
                    res = "二";
                    break;
                case 3:
                    res = "三";
                    break;
                case 4:
                    res = "四";
                    break;
                case 5:
                    res = "五";
                    break;
                case 6:
                    res = "六";
                    break;
                case 7:
                    res = "日";
                    break;
            }
            return res;
        }
        #endregion

        #region EditRoute
        public string EditRoute(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";
            RouteEntity entity = new RouteEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new RouteBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<RouteEntity>(rParams, entity);

            entity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);

            new RouteBLL(CurrentUserInfo).EditRoute(entity, rParams["CycleDetailID"], rParams["ClientUserID"].ToInt());

            res = "{success:true,msg:'保存成功',id:'" + entity.RouteID + "'}";
            return res;
        }
        #endregion

    }
}
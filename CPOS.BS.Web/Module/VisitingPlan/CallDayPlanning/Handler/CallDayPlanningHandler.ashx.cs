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
    /// CallDayPlanningHandler 的摘要说明
    /// </summary>
    public class CallDayPlanningHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request.QueryString["btncode"])
            {
                case "search":
                   if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetUserCDPList":
                                res = GetUserCDPList(pContext.Request.Form);
                                break;
                            case "GetUserCDPPOPType":
                                res = GetUserCDPPOPType(pContext.Request.Form);
                                break;
                        }
                    }
                    break;

                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetUserCDPPOPType":
                                res = GetUserCDPPOPType(pContext.Request.Form);
                                break;
                            case "EditCDP":
                                res = EditCDP(pContext.Request.Form);
                                break;
                        }
                    }
                    break;

            }
            this.ResponseContent(res);
        }

        #region GetUserCDPList
        public string GetUserCDPList(NameValueCollection rParams)
        {
            CallDayPlanningViewEntity_User entity = new CallDayPlanningViewEntity_User();

            if (!string.IsNullOrEmpty(rParams["ClientStructureID"]) 
                && rParams["ClientStructureID"]!="null")
            {
                entity.ClientStructureID = Guid.Parse(rParams["ClientStructureID"]);
            }
            if (!string.IsNullOrEmpty(rParams["ClientPositionID"])
                && rParams["ClientPositionID"] != "null")
            {
                entity.ClientPositionID = rParams["ClientPositionID"].ToInt();
            }
            if (!string.IsNullOrEmpty(rParams["ClientUserID"])
                && rParams["ClientUserID"] != "null")
            {
                entity.ClientUserID = rParams["ClientUserID"].ToInt();
            }
            if (!string.IsNullOrEmpty(rParams["CallDate"])
                && rParams["CallDate"] != "null")
            {
                entity.CallDate = rParams["CallDate"].ToDateTime();
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new CallDayPlanningBLL(CurrentUserInfo).GetUserCDPList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion
        #region GetUserCDPPOPType
        public string GetUserCDPPOPType(NameValueCollection rParams)
        {
            CallDayPlanningEntity entity = new CallDayPlanningEntity();
            entity.ClientUserID = rParams["ClientUserID"].ToInt();
            entity.CallDate = rParams["CallDate"].ToDateTime();
            return new CallDayPlanningBLL(CurrentUserInfo).GetUserCDPPOPType(entity).Tables[0].ToJSON();
        }
        #endregion
        #region EditCDP
        public string EditCDP(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'保存失败'}";

            CallDayPlanningEntity entity = new CallDayPlanningEntity();
            entity.ClientUserID = rParams["ClientUserID"].ToInt();
            entity.CallDate = rParams["CallDate"].ToDateTime();
            entity.POPType = rParams["POPType"].ToInt();
            string poplist="";
            if(entity.POPType==1)
            {
                poplist = rParams["StoreList"];
            }
            else if(entity.POPType==2)
            {
                poplist = rParams["DistributorList"];
            }
            entity.Remark = rParams["Remark"];
            entity.PlanningType = 2;
            
            entity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);

            new CallDayPlanningBLL(CurrentUserInfo).EditCDP(entity, poplist);
            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion
    }
}
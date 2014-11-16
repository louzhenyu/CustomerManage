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
    /// TaskHandler 的摘要说明
    /// </summary>
    public class TaskHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                    switch (this.Method)
                    {
                        case "GetTaskList":
                            res = GetTaskList(pContext.Request.Form);
                            break;
                        case "GetTaskByID":
                            res = GetTaskByID(pContext.Request.Form["id"]);
                            break;
                        case "GetStepByTID":
                            res = GetStepByTID(pContext.Request.Form);
                            break;
                        case "GetTaskPOP_SearchConditions":
                            res = GetTaskPOP_SearchConditions(pContext.Request.Form);
                            break;
                    }
                    break;
                case "delete":
                    res = DeleteTask(pContext.Request.Form["id"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetTaskByID":
                                res = GetTaskByID(pContext.Request.Form["id"]);
                                break;
                            case "EditTask":
                                res = EditTask(pContext.Request.Form);
                                break;

                            case "GetStepByTID":
                                res = GetStepByTID(pContext.Request.Form);
                                break;

                            case "GetTaskPOP_SearchConditions":
                                res = GetTaskPOP_SearchConditions(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetTaskByID":
                                res = GetTaskByID(pContext.Request.Form["id"]);
                                break;
                            case "EditTask":
                                res = EditTask(pContext.Request.Form);
                                break;
                            
                            case "GetStepByTID":
                                res = GetStepByTID(pContext.Request.Form);
                                break;
                            case "DeleteStep":
                                res = DeleteStep(pContext.Request.Form["ids"]);
                                break;

                            case "GetTaskPOP_SearchConditions":
                                res = GetTaskPOP_SearchConditions(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetTaskList
        private string GetTaskList(NameValueCollection rParams)
        {
            VisitingTaskViewEntity entity = new VisitingTaskViewEntity();
            if (!string.IsNullOrEmpty(rParams["ClientPositionID"]))
            {
                entity.ClientPositionID = rParams["ClientPositionID"];
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                new VisitingTaskBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion
        #region GetTaskByID
        private string GetTaskByID(string id)
        {
            return "[" + new VisitingTaskBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion
        #region EditTask
        private string EditTask(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            //组装参数
            VisitingTaskEntity entity = new VisitingTaskEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new VisitingTaskBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<VisitingTaskEntity>(rParams, entity);
            if (string.IsNullOrEmpty(rParams["StartPic"]))
            {
                entity.StartPic = 0;
            }
            if (string.IsNullOrEmpty(rParams["EndPic"]))
            {
                entity.EndPic = 0;
            }
            entity.ClientID =CurrentUserInfo.ClientID;
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new VisitingTaskBLL(CurrentUserInfo).EditTask(entity);
            }
            else
            {
                new VisitingTaskBLL(CurrentUserInfo).Create(entity);
            }
            res = "{success:true,msg:'编辑成功',id:'" + entity.VisitingTaskID + "'}";
            return res;
        }
        #endregion
        #region DeleteTask
        private string DeleteTask(string id)
        {
            string res = "{success:false}";
            new VisitingTaskBLL(CurrentUserInfo).DeleteTask(Guid.Parse(id));
            res = "{success:true}";
            return res;
        }
        #endregion

        #region GetStepByTID
        private string GetStepByTID(NameValueCollection rParams)
        {
            VisitingTaskStepViewEntity entity = new VisitingTaskStepViewEntity();
            entity.VisitingTaskID = Guid.Parse(rParams["id"]);
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                new VisitingTaskStepBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion
        #region DeleteStep
        private string DeleteStep(string ids)
        {
            string res = "{success:false}";
            new VisitingTaskStepBLL(CurrentUserInfo).DeleteStep(ids);
            res = "{success:true}";
            return res;
        }
        #endregion

        #region GetTaskPOP_SearchConditions
        private string GetTaskPOP_SearchConditions(NameValueCollection rParams)
        {
            string res = "{{IsAutoFill:\"{0}\",GroupCondition:\"{1}\"}}";
            Guid taskid = Guid.Parse(rParams["id"]);
            POPGroupEntity entity = new VisitingTaskBLL(CurrentUserInfo).GetTaskPOP_SearchConditions(taskid);
            if (entity != null)
            {
                return string.Format(res, entity.IsAutoFill, entity.GroupCondition.Replace("\"","'"));
            }
            else
            {
                return "";
            }
        }
        #endregion

        
    }
}
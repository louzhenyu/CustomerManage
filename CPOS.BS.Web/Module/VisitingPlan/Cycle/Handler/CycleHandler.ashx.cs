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

namespace JIT.CPOS.BS.Web.Module.VisitingPlan.Cycle.Handler
{
    /// <summary>
    /// CycleHandler 的摘要说明
    /// </summary>
    public class CycleHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                            case "GetList":
                                res = GetList(pContext.Request.Form);
                                break;
                            case "GetByID":
                                res = GetByID(pContext.Request.Form["id"]);
                                break;
                        }
                    }
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["ids"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "Edit":
                                res = Edit(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetByID":
                                res = GetByID(pContext.Request.Form["id"]);
                                break;
                            case "Edit":
                                res = Edit(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
            }
            this.ResponseContent(res);
        }

        #region GetList
        private string GetList(NameValueCollection rParams)
        {
            CycleEntity entity = rParams["form"].DeserializeJSONTo<CycleEntity>();
            
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new CycleBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);

        }
        #endregion

        #region GetByID
        private string GetByID(string id)
        {
            string res = new CycleBLL(CurrentUserInfo).GetByID(Guid.Parse(id)).ToJSON();
            CycleDetailEntity[] detailEntity = new CycleDetailBLL(CurrentUserInfo).GetCycleDetailByCID(Guid.Parse(id));
            if (detailEntity != null)
            {
                var detail = from m in detailEntity 
                             select new { CycleDetailID = m.CycleDetailID, DayOfCycle = m.DayOfCycle };
                res = res.Replace("}", ",detailEntity:" + detail.ToJSON() + "}");
            }
            return res;
        }
        #endregion

        #region Delete
        private string Delete(string id)
        {
            string res = "";
            string checkRes = "";
            new CycleBLL(CurrentUserInfo).Delete(Guid.Parse(id), out checkRes);
            if (!string.IsNullOrEmpty(checkRes))
            {
                res = "{success:false,msg:\"" + checkRes + "\"}";
            }
            else
            {
                res = "{success:true}";
            }
            return res;
        }
        #endregion

        #region Edit
        private string Edit(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";
            CycleEntity entity = new CycleEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new CycleBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<CycleEntity>(rParams, entity);

            string selectedList = rParams["cycleDetail"];
            entity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);

            new CycleBLL(CurrentUserInfo).Edit(entity, selectedList);

            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion
    }
}
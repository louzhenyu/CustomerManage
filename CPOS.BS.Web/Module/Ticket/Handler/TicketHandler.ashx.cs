using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Linq;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Extension;
using System;
using System.Text;
using System.Data;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.Module.Ticket.Handler
{
    /// <summary>
    /// TicketHandler 的摘要说明
    /// </summary>
    public class TicketHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "ticket_query":      //票务查询
                    content = GetTicketData(pContext.Request.Form);
                    break;
                case "ticket_delete":     //票务删除
                    content = TicketDeleteData();
                    break;
                case "get_ticket_by_id":  //根据ID获取票务信息
                    content = GetTicketById();
                    break;
                case "ticket_save":       //保存票务信息
                    content = SaveTicket();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region TicketDeleteData 票务删除

        /// <summary>
        /// 票务删除
        /// </summary>
        public string TicketDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "票务ID不能为空";
                return responseData.ToJSON();
            }

            //string[] ids = key.Split(',');
            new TicketBLL(this.CurrentUserInfo).DeleteTicket(key);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetTicketData 查询票务列表

        /// <summary>
        /// 查询票务列表
        /// </summary>
        public string GetTicketData(NameValueCollection rParams)
        {
            TicketEntity entity = rParams["form"].DeserializeJSONTo<TicketEntity>();
            if (entity == null)
            {
                entity = new TicketEntity();
            }

            if (!string.IsNullOrEmpty(rParams["EventID"]))
            {
                entity.EventID = rParams["EventID"];
            }
            if (!string.IsNullOrEmpty(rParams["TicketName"]))
            {
                entity.TicketName = rParams["TicketName"];
            }

            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();

            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new TicketBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }

        #endregion

        #region GetTicketById 根据ID获取票务信息
        /// <summary>
        /// 根据ID获取票务信息
        /// </summary>
        public string GetTicketById()
        {
            var ticketService = new TicketBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("TicketID")) != null && FormatParamValue(Request("TicketID")) != string.Empty)
            {
                key = FormatParamValue(Request("TicketID")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "TicketID", Value = key });
            }

            var data = ticketService.Query(condition.ToArray(), null).ToList().FirstOrDefault();

            if (data != null)
            {
                var condition2 = new List<IWhereCondition>();
                condition2.Add(new EqualsCondition() { FieldName = "EventID", Value = data.EventID });

                var data2 = new WLEventsBLL(this.CurrentUserInfo).Query(condition2.ToArray(), null).ToList().FirstOrDefault();
                data.Title = data2.Title;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveTicket 保存票务信息

        /// <summary>
        /// 保存票务信息
        /// </summary>
        public string SaveTicket()
        {
            var ticketService = new TicketBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string TicketID = string.Empty;
            var ticket = Request("ticket");

            if (FormatParamValue(ticket) != null && FormatParamValue(ticket) != string.Empty)
            {
                key = FormatParamValue(ticket).ToString().Trim();
            }
            if (FormatParamValue(Request("TicketID")) != null && FormatParamValue(Request("TicketID")) != string.Empty)
            {
                TicketID = FormatParamValue(Request("TicketID")).ToString().Trim();
            }

            var ticketEntity = key.DeserializeJSONTo<TicketEntity>();

            if (ticketEntity.EventID == null || ticketEntity.EventID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动不能为空";
                return responseData.ToJSON();
            }
 
            ticketEntity.CustomerID = this.CurrentUserInfo.CurrentLoggingManager.Customer_Id.ToString().Trim();

            var tmpObj = ticketService.GetByID(ticketEntity.TicketID);
            if (tmpObj == null)
            {
                ticketService.Create(ticketEntity);
            }
            else
            {
                ticketEntity.TicketID = Guid.Parse(TicketID);

                ticketService.Update(ticketEntity);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        

    }
}
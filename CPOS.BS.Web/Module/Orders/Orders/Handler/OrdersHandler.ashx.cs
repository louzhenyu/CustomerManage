using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;

using JIT.Utility.Web;
using JIT.Utility.Reflection;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.Orders.Orders.Handler
{
    /// <summary>
    /// OrdersHandler 的摘要说明
    /// </summary>
    public class OrdersHandler : JITCPOSAjaxHandler
    {
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.Method)
            {
                case "GetList":
                    res = GetList(pContext.Request.Form);
                    break;
                case "GetOrdersStatusCount":
                    res = GetOrdersStatusCount(pContext.Request.Form);
                    break;
                case "EditOrderRemark":
                    res = EditOrderRemark(pContext.Request.Form);
                    break;
                case "GetOrdersDetail":
                    res = GetOrdersDetail(pContext.Request.Form);
                    break;
                case "OrderApprove":
                    res = OrderApprove(pContext.Request.Form);
                    break;
                case "Complete":
                    res = Complete(pContext.Request.Form);
                    break;
                case "sendMsg":
                    res = SendMsg(pContext.Request.Form);
                    break;
                case "Export":
                    res = Export(pContext.Request.Form);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetList
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        private string GetList(NameValueCollection rParams)
        {
            PagedQueryResult<TInoutViewEntity>  pagedQueryEntity = SearchOrder(rParams);

            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}", pagedQueryEntity.Entities.ToJSON(), pagedQueryEntity.PageCount);
        }
        #endregion

        #region EditOrderRemark
        /// <summary>
        /// 编辑备注
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string EditOrderRemark(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";
            TInoutEntity entity = new TInoutEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new TInoutBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<TInoutEntity>(rParams, entity);
            new TInoutBLL(CurrentUserInfo).Update(entity);
            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion

        #region GetOrdersDetail
        /// <summary>
        /// 根据订单ID获取订单明细和订单操作流水数据
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string GetOrdersDetail(NameValueCollection rParams)
        {
            TInoutDetailEntity entity = new TInoutDetailEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity.OrderID = rParams["id"].ToString();
                DataSet ds = new DataSet();
                ds = new TInoutDetailBLL(CurrentUserInfo).GetOrdersDetail(entity, rParams["isHotel"] == null ? "" : rParams["isHotel"]);
                return string.Format("{{\"orderDetail\":{0},\"orderStatus\":{1}}}", ds.Tables[0].ToJSON(), ds.Tables[1].ToJSON());
            }
            else
            {
                return "{{\"orderDetail\":\"\",\"orderStatus\":\"\"}}";
            }
        }
        #endregion

        #region OrderApprove
        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string OrderApprove(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";
            #region 接收参数
            Dictionary<string, string> pParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(rParams["pOrdersID"]))
            {
                pParams.Add("pOrdersID", rParams["pOrdersID"]);
            }
            if (!string.IsNullOrEmpty(rParams["pOrdersType"]))
            {
                pParams.Add("pOrdersType", rParams["pOrdersType"]);
            }
            if (!string.IsNullOrEmpty(rParams["pOrdersStatus"]))
            {
                pParams.Add("pOrdersStatus", rParams["pOrdersStatus"]);
            }
            if (!string.IsNullOrEmpty(rParams["pOrderDesc"]))
            {
                pParams.Add("pOrdersDesc", rParams["pOrderDesc"]);
            }
            pParams.Add("pCheckResult", rParams["pCheckResult"]);
            pParams.Add("pRemark", rParams["pRemark"]);
            #endregion

            new TInoutBLL(CurrentUserInfo).OrdersApprove(pParams);
            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion

        #region Complete
        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string Complete(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";
            string pOrdersID = "", pStatus = "";
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                pOrdersID = rParams["id"].ToString();
            }
            if (!string.IsNullOrEmpty(rParams["status"]))
            {
                pStatus = rParams["status"];
            }

            new TInoutBLL(CurrentUserInfo).Complete(pOrdersID, pStatus);

            //订单完成，调用方法给积分  Updated by Willie Yan on 2014-05-29
            T_InoutBLL inoutBLL = new T_InoutBLL(CurrentUserInfo);
            string userId = inoutBLL.GetByID(pOrdersID).vip_no;
            new VipIntegralBLL(CurrentUserInfo).OrderReturnMoneyAndIntegral(pOrdersID, userId, null);

            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion

        #region GetOrdersStatusCount
        /// <summary>
        /// 获取订单各个状态的数量
        /// </summary>
        /// <param name="rParam"></param>
        /// <returns></returns>
        public string GetOrdersStatusCount(NameValueCollection rParams)
        {
            #region 条件拼接
            Dictionary<string, string> pParems = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(rParams["pOrdersNo"]))
            {
                pParems.Add("pOrdersNo", rParams["pOrdersNo"]);
            }
            if (!string.IsNullOrEmpty(rParams["pStartDate"]))
            {
                pParems.Add("pStartDate", rParams["pStartDate"]);
            }
            if (!string.IsNullOrEmpty(rParams["pEndDate"]))
            {
                pParems.Add("pEndDate", rParams["pEndDate"]);
            }
            if (!string.IsNullOrEmpty(rParams["pStoreName"]))
            {
                pParems.Add("pStoreName", rParams["pStoreName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pItemName"]))
            {
                pParems.Add("pItemName", rParams["pItemName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pUserName"]))
            {
                pParems.Add("pUserName", rParams["pUserName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pStatus"]))
            {
                pParems.Add("pStatus", rParams["pStatus"]);
            }

            #endregion

            DataSet ds = new TInoutBLL(CurrentUserInfo).GetOrdersListCountFHotels(pParems);

            return string.Format("{{\"approveCount\":{0},\"checkCount\":{1},\"completeCount\":{2},\"cancelCount\":{3},\"notAuditCount\":{4},\"allCount\":{5}}}"
                , ds.Tables[0].Rows[0][0]
                , ds.Tables[1].Rows[0][0]
                , ds.Tables[2].Rows[0][0]
                , ds.Tables[3].Rows[0][0]
                , ds.Tables[4].Rows[0][0]
                , ds.Tables[5].Rows[0][0]);
        }
        #endregion

        #region SendMsg
        public string SendMsg(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'操作失败'}";
            string sign = "";
            int isCS = 1;
            string mobile = "";
            string userid = "";
            int csPipelineId = 1;
            string messageContent = "";
            string conn = GetCustomerConn(CurrentUserInfo.ClientID);
            CurrentUserInfo.Conn = conn;
            CSInvokeMessageBLL bll = new CSInvokeMessageBLL(CurrentUserInfo);
            if (!string.IsNullOrEmpty(rParams["csPipelineId"]))
            {
                csPipelineId = int.Parse(rParams["csPipelineId"]);
            }
            if (!string.IsNullOrEmpty(rParams["messageContent"]))
            {
                messageContent = rParams["messageContent"];
            }
            if (!string.IsNullOrEmpty(rParams["userid"]))
            {
                userid = rParams["userid"];
            }
            if (!string.IsNullOrEmpty(rParams["mobile"]))
            {
                mobile = rParams["mobile"];
            }
            if (!string.IsNullOrEmpty(rParams["sign"]))
            {
                sign = rParams["sign"];
            }
            if (!string.IsNullOrEmpty(rParams["isCS"]))
            {
                isCS = int.Parse(rParams["isCS"]);
            }
            bll.SendMessage(csPipelineId, userid, isCS, "", messageContent, null, null, null, null, sign, mobile);
            res = "{success:true,msg:'保存成功'}";
            return res;
        }
        #endregion

        #region GetCustomerConn
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GetCustomerConn(string customerId)
        {
            string sql = string.Format(
                "select 'user id='+a.db_user+';password='+a.db_pwd+';data source='+a.db_server+';database='+a.db_name+';' conn " +
                " from t_customer_connect a where a.customer_id='{0}' ",
                customerId);
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            var result = sqlHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }
        #endregion

        #region Export
        /// <summary>
        /// 导出订单
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        private string Export(NameValueCollection rParams)
        {
            string fileName = "";

            PagedQueryResult<TInoutViewEntity> pagedQueryEntity = SearchOrder(rParams);
            if (pagedQueryEntity.Entities.Length > 0)
            {
                //花间堂默认为到店付款
                foreach (var item in pagedQueryEntity.Entities)
                {
                    item.Payment = "到店付款";
                }

                DataTable dataTable = Utils.ToDataTable<TInoutViewEntity>(pagedQueryEntity.Entities);

                if (CurrentContext.Request.Form != null && CurrentContext.Request.Form[0].ToString() != "")
                {
                    dynamic column = rParams["Columns"].ToString().DeserializeJSONTo<dynamic>();

                    //Rename column name
                    foreach (var c in column)
                    {
                        if (c["text"] != "操作")
                        {
                            DataColumn dataColumn = new DataColumn();
                            dataColumn = dataTable.Columns[c["dataIndex"].ToString()];

                            if (dataColumn != null)
                            {
                                dataColumn.ColumnName = c["text"].ToString();
                                dataColumn.Caption = "Remain";
                            }
                        }
                    }

                    //remove unwanted columns
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (dataTable.Columns[i] != null && dataTable.Columns[i].Caption != "Remain")
                        {
                            dataTable.Columns.Remove(dataTable.Columns[i].ColumnName);
                            i--;
                        }
                    }

                    fileName = Utils.DataTableToExcel(dataTable, "Order", "订单数据", "post");
                }
            }
            return fileName;
        }
        #endregion

        private PagedQueryResult<TInoutViewEntity> SearchOrder(NameValueCollection rParams)
        {
            #region 条件拼接
            Dictionary<string, object> pParems = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rParams["pOrdersNo"]))
            {
                pParems.Add("pOrdersNo", rParams["pOrdersNo"]);
            }
            if (!string.IsNullOrEmpty(rParams["pStartDate"]))
            {
                pParems.Add("pStartDate", rParams["pStartDate"]);
            }
            if (!string.IsNullOrEmpty(rParams["pEndDate"]))
            {
                pParems.Add("pEndDate", rParams["pEndDate"]);
            }
            if (!string.IsNullOrEmpty(rParams["pStoreName"]))
            {
                pParems.Add("pStoreName", rParams["pStoreName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pItemName"]))
            {
                pParems.Add("pItemName", rParams["pItemName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pUserName"]))
            {
                pParems.Add("pUserName", rParams["pUserName"]);
            }
            if (!string.IsNullOrEmpty(rParams["pStatus"]))
            {
                pParems.Add("pStatus", rParams["pStatus"]);
            }
            int pageSize = 1000000;
            if (rParams["limit"] != null)
                pageSize = rParams["limit"].ToInt();

            int pageIndex = 1;
            if (rParams["page"] != null)
                pageIndex = rParams["page"].ToInt();

            #endregion

            PagedQueryResult<TInoutViewEntity> pagedQueryEntity = new TInoutBLL(CurrentUserInfo).GetOrdersList(pParems, pageIndex, pageSize);

            return pagedQueryEntity;
        }
    }
}
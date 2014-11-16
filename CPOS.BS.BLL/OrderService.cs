using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using JIT.CPOS.BS.Entity.Eliya;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 订单方法类
    /// </summary>
    public class OrderService : BaseService
    {
        JIT.CPOS.BS.DataAccess.OrderService orderService = null;
        #region 构造函数
        public OrderService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            orderService = new DataAccess.OrderService(loggingSessionInfo);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 订单查询（订单类）
        /// </summary>
        /// <param name="order_no">订单号码</param>
        /// <param name="order_type_id">订单类型1</param>
        /// <param name="order_reason_type_id">订单类型2</param>
        /// <param name="red_flag">红单</param>
        /// <param name="order_status">订单状态值</param>
        /// <param name="purchase_unit_id">采购单位标识（总部，门店，渠道......）</param>
        /// <param name="sales_unit_id">销售单位标识（供应商，客户，渠道，总部......）</param>
        /// <param name="order_date_begin">单据日期起</param>
        /// <param name="order_date_end">单据日期止</param>
        /// <param name="request_date_begin">预计到达日期起</param>
        /// <param name="request_date_end">预计到达日期止</param>
        /// <param name="maxRowCount">当前页显示数量</param>
        /// <param name="startRowIndex">当前开始行号</param>
        /// <returns></returns>
        public OrderInfo SearchOrderInfo(string order_no
                                        , string order_type_id
                                        , string order_reason_type_id
                                        , string red_flag
                                        , string order_status
                                        , string purchase_unit_id
                                        , string sales_unit_id
                                        , string order_date_begin
                                        , string order_date_end
                                        , string request_date_begin
                                        , string request_date_end
                                        , int maxRowCount
                                        , int startRowIndex)
        {
            OrderInfo orderInfo = new OrderInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.sales_unit_id = sales_unit_id;
            orderSearchInfo.red_flag = red_flag;
            orderSearchInfo.purchase_unit_id = purchase_unit_id;
            orderSearchInfo.status = order_status;
            orderSearchInfo.order_date_begin = order_date_begin;
            orderSearchInfo.order_date_end = order_date_end;
            orderSearchInfo.request_date_begin = request_date_begin;
            orderSearchInfo.request_date_end = request_date_end;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            orderInfo = SearchOrderInfo(orderSearchInfo);
            return orderInfo;
        }

        /// <summary>
        /// 订单查询（订单类）
        /// </summary>
        /// <param name="orderSearchInfo">参数集合</param>
        /// <returns></returns>
        public OrderInfo SearchOrderInfo(OrderSearchInfo orderSearchInfo)
        {

            try
            {
                OrderInfo orderInfo = new OrderInfo();
                if (orderSearchInfo.customer_id == null || orderSearchInfo.customer_id.Equals(""))
                {
                    orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                }
                int iCount = orderService.SearchOrderCount(orderSearchInfo);

                IList<OrderInfo> orderInfoList = new List<OrderInfo>();
                DataSet ds = new DataSet();
                ds = orderService.SearchOrderInfo(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    orderInfoList = DataTableToObject.ConvertToList<OrderInfo>(ds.Tables[0]);
                }
                orderInfo.ICount = iCount;
                orderInfo.orderInfoList = orderInfoList;
                return orderInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region order commit
        /// <summary>
        /// 订单提交（新建，修改）
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="IsTrans"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetOrderInfo(OrderInfo orderInfo, bool IsTrans, out string strError)
        {
            try
            {
                if (orderInfo.order_id == null || orderInfo.order_id.Equals(""))
                {
                    orderInfo.order_id = NewGuid();
                    orderInfo.operate = "Create";
                }

                OrderInfo orderInfo1 = new OrderInfo();
                orderInfo1 = GetOrderInfoById(orderInfo.order_id);
                if (orderInfo1 == null || orderInfo1.order_id == null || orderInfo1.order_id.Equals("")) { orderInfo.operate = "Create"; } else { orderInfo.operate = "Modify"; }
                if (orderInfo.create_user_id == null || orderInfo.create_user_id.Equals(""))
                {
                    orderInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    orderInfo.create_time = GetCurrentDateTime();
                }
                if (orderInfo.modify_user_id == null || orderInfo.modify_user_id.Equals(""))
                {
                    orderInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    orderInfo.modify_time = GetCurrentDateTime();
                }
                orderInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

                if (orderInfo.orderDetailList != null)
                {
                    foreach (OrderDetailInfo orderDetailInfo in orderInfo.orderDetailList)
                    {
                        if (orderDetailInfo.order_detail_id == null || orderDetailInfo.order_detail_id.Equals(""))
                        {
                            orderDetailInfo.order_detail_id = NewGuid();
                            orderDetailInfo.create_time = GetCurrentDateTime();
                            orderDetailInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        }
                        orderDetailInfo.order_id = orderInfo.order_id;
                    }
                }

                string strCount = string.Empty;
                if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null || loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
                {
                    orderInfo.order_status = "1";
                    orderInfo.order_status_desc = "未审批";
                }
                else
                {
                    if (orderInfo1 == null
                        && (!new BillService(loggingSessionInfo).CanHaveBill(orderInfo.order_id)))
                    {
                        //2.提交表单
                        if (!SetOrderInfoInsertBill(orderInfo))
                        {
                            strError = "inout表单提交失败。";
                            throw (new System.Exception(strError));
                        }
                        //3.更改状态
                        BillModel billInfo = new cBillService(loggingSessionInfo).GetBillById(orderInfo.order_id);
                        if (billInfo != null)
                        {
                            orderInfo.order_status = billInfo.Status;
                            orderInfo.order_status_desc = billInfo.BillStatusDescription;
                            strError = billInfo.Status + "--" + orderInfo.order_id;
                        }
                        else
                        {
                            strError = "没找到对应的bill";
                        }

                    }
                    else
                    {
                        strError = "存在相同的表单:" + strCount + ":-- " + loggingSessionInfo.CurrentLoggingManager.Connection_String + "--";
                    }
                }


                bool bReturn = orderService.SetOrderInfo(orderInfo, IsTrans, out strError);

                return bReturn;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // <summary>
        /// 单子提交到表单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        private bool SetOrderInfoInsertBill(OrderInfo orderInfo)
        {
            try
            {
                BillModel bill = new BillModel();
                cBillService bs = new cBillService(loggingSessionInfo);

                bill.Id = orderInfo.order_id;
                DataSet ds = new DataSet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string order_type_id = bs.GetBillKindByCode(orderInfo.BillKindCode).Id;

                    bill.Code = new AppSysService(loggingSessionInfo).GetNo(orderInfo.BillKindCode);

                    bill.KindId = order_type_id;
                    bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                    bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                    BillOperateStateService state = bs.InsertBill(bill);

                    if (state == BillOperateStateService.CreateSuccessful)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 订单状态修改
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetOrderStatusUpdate(string order_id, BillActionType billActionType, out string strError)
        {
            string strResult = string.Empty;
            try
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.order_id = order_id;
                orderInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                orderInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null || loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
                {

                    switch (billActionType)
                    {
                        case BillActionType.Cancel:
                            orderInfo.order_status = "-1";
                            orderInfo.order_status_desc = "删除";
                            break;
                        case BillActionType.Approve:
                            orderInfo.order_status = "10";
                            orderInfo.order_status_desc = "已审批";
                            orderInfo.approve_time = GetCurrentDateTime();
                            orderInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                            break;
                        case BillActionType.Reject:
                            orderInfo.order_status = "1";
                            orderInfo.order_status_desc = "未审批";
                            break;
                        case BillActionType.Create:
                            orderInfo.order_status = "1";
                            orderInfo.order_status_desc = "未审批";
                            break;
                        default:
                            orderInfo.order_status = "1";
                            orderInfo.order_status_desc = "未审批";
                            break;
                    }
                }
                else
                {
                    cBillService bs = new cBillService(loggingSessionInfo);

                    BillOperateStateService state = bs.ApproveBill(order_id, "", billActionType, out strResult);
                    if (state == BillOperateStateService.ApproveSuccessful)
                    {
                        //获取要改变的表单信息
                        BillModel billInfo = bs.GetBillById(order_id);
                        //设置要改变的用户信息

                        orderInfo.order_status = billInfo.Status;
                        orderInfo.order_status_desc = billInfo.BillStatusDescription;

                        if (billActionType == BillActionType.Approve)
                        {
                            orderInfo.approve_time = GetCurrentDateTime();
                            orderInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        }
                    }
                    else
                    {
                        strError = "获取状态失败--" + strResult;
                        return false;
                    }
                }
                bool bReturn = orderService.SetOrderStatusUpdate(orderInfo);
                strError = "操作成功";
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                throw (ex);
            }
        }
        #endregion

        #region 获取订单主信息
        /// <summary>
        /// 获取订单主信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoById(string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                OrderInfo orderInfo = new OrderInfo();
                DataSet ds = orderService.GetOrderInfoById(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    orderInfo = DataTableToObject.ConvertToObject<OrderInfo>(ds.Tables[0].Rows[0]);
                }
                orderInfo.orderDetailList = GetOrderDetailInfoByOrderId(orderId);
                return orderInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取明细集合
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<OrderDetailInfo> GetOrderDetailInfoByOrderId(string orderId)
        {
            try
            {
                IList<OrderDetailInfo> inoutDetailList = new List<OrderDetailInfo>();
                DataSet ds = orderService.GetOrderDetailInfoByOrderId(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutDetailList = DataTableToObject.ConvertToList<OrderDetailInfo>(ds.Tables[0]);
                }
                return inoutDetailList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 根据用户ID返回订单统计
        public OrderStatisticsInfo[] GetCountByUserAndStatus(string customerid, string userid, string status)
        {
            List<OrderStatisticsInfo> list = new List<OrderStatisticsInfo> { };
            var ds = this.orderService.GetCountByUserAndStatus(customerid, userid, status);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(new OrderStatisticsInfo()
                 {
                     TypeCode = item["TypeCode"].ToString(),
                     Count = Convert.ToInt32(item["Count"]),
                     Description = item["Description"].ToString()
                 });
            }
            return list.ToArray();
        }
        #endregion

        #region 更新订单的支付方式
        public bool SetOrderPaymentType(string orderid, string paymentid)
        {
            return this.orderService.SetOrderPaymentType(orderid, paymentid);
        }
        #endregion
    }
}

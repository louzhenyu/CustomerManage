using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 进出库单据服务
    /// </summary>
    public class Inout3Service : BaseService
    {
        JIT.CPOS.BS.DataAccess.Inout3Service inoutService = null;
        #region 构造函数
        public Inout3Service(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            inoutService = new DataAccess.Inout3Service(loggingSessionInfo);
        }
        #endregion

        #region 出入库单据查询
        /// <summary>
        /// 出入库单据查询
        /// </summary>
        /// <param name="order_no">单据号码</param>
        /// <param name="order_reason_type_id">类型标识</param>
        /// <param name="sales_unit_id">销售单位标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="purchase_unit_id">采购单位标识</param>
        /// <param name="status">状态code</param>
        /// <param name="order_date_begin">单据日期起(yyyy-MM-dd)</param>
        /// <param name="order_date_end">单据日期止(yyyy-MM-dd)</param>
        /// <param name="complete_date_begin">完成日期起(yyyy-MM-dd)</param>
        /// <param name="complete_date_end">完成日期止(yyyy-MM-dd)</param>
        /// <param name="data_from_id">数据来源标识</param>
        /// <param name="ref_order_no">原单据号码</param>
        /// <param name="order_type_id">出入库单据标签：（出库单=1F0A100C42484454BAEA211D4C14B80F，入库单=C1D407738E1143648BC7980468A399B8）</param>
        /// <param name="red_falg">红单标志</param>
        /// <param name="maxRowCount">当前页显示数量</param>
        /// <param name="startRowIndex">当前页开始数量</param>
        /// <param name="DeliveryStatus">状态</param>
        /// <param name="DeliveryId">配送方式</param>
        /// <param name="DefrayTypeId">支付方式</param>
        /// <returns></returns>
        public InoutInfo SearchInoutInfo(string order_no
                                       , string order_reason_type_id
                                       , string sales_unit_id
                                       , string warehouse_id
                                       , string purchase_unit_id
                                       , string status
                                       , string order_date_begin
                                       , string order_date_end
                                       , string complete_date_begin
                                       , string complete_date_end
                                       , string data_from_id
                                       , string ref_order_no
                                       , string order_type_id
                                       , string red_falg
                                       , int maxRowCount
                                       , int startRowIndex
                                       , string purchase_warehouse_id
                                       , string sales_warehouse_id
                                       , string DeliveryStatus
                                       , string DeliveryId
                                       , string DefrayTypeId
                                       , string DeliveryDateBegin
                                       , string DeliveryDateEnd
                                       , string CancelDateBegin
                                       , string CancelDateEnd, string order_id, string vipId,
            string path_unit_id, string timestamp, string InoutSort)
        {
            InoutInfo inoutInfo = new InoutInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.InoutSort = InoutSort; //排序
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.sales_unit_id = sales_unit_id;
            orderSearchInfo.warehouse_id = warehouse_id;
            orderSearchInfo.purchase_unit_id = purchase_unit_id;
            orderSearchInfo.status = status;
            orderSearchInfo.order_date_begin = order_date_begin;
            orderSearchInfo.order_date_end = order_date_end;
            orderSearchInfo.complete_date_begin = complete_date_begin;
            orderSearchInfo.complete_date_end = complete_date_end;
            orderSearchInfo.data_from_id = data_from_id;
            orderSearchInfo.ref_order_no = ref_order_no;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            orderSearchInfo.red_flag = red_falg;
            orderSearchInfo.purchase_warehouse_id = purchase_warehouse_id;
            orderSearchInfo.sales_warehouse_id = sales_warehouse_id;
            orderSearchInfo.DeliveryStatus = DeliveryStatus;
            orderSearchInfo.DeliveryId = DeliveryId;
            orderSearchInfo.DefrayTypeId = DefrayTypeId;
            orderSearchInfo.DeliveryDateBegin = DeliveryDateBegin;
            orderSearchInfo.DeliveryDateEnd = DeliveryDateEnd;
            orderSearchInfo.CancelDateBegin = CancelDateBegin;
            orderSearchInfo.CancelDateEnd = CancelDateEnd;
            orderSearchInfo.order_id = order_id;
            orderSearchInfo.vip_no = vipId;
            orderSearchInfo.path_unit_id = path_unit_id;
            orderSearchInfo.timestamp = timestamp;

            inoutInfo = SearchInoutInfo(orderSearchInfo);
            return inoutInfo;
        }
        #endregion


        #region 出入库单据查询 jifeng.cao 20140319
        /// <summary>
        /// 出入库单据查询
        /// </summary>
        /// <param name="PayStatus">付款状态</param>
        /// <param name="order_no">单据号码</param>
        /// <param name="order_reason_type_id">类型标识</param>
        /// <param name="sales_unit_id">销售单位标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="purchase_unit_id">采购单位标识</param>
        /// <param name="status">状态code</param>
        /// <param name="order_date_begin">单据日期起(yyyy-MM-dd)</param>
        /// <param name="order_date_end">单据日期止(yyyy-MM-dd)</param>
        /// <param name="complete_date_begin">完成日期起(yyyy-MM-dd)</param>
        /// <param name="complete_date_end">完成日期止(yyyy-MM-dd)</param>
        /// <param name="data_from_id">数据来源标识</param>
        /// <param name="ref_order_no">原单据号码</param>
        /// <param name="order_type_id">出入库单据标签：（出库单=1F0A100C42484454BAEA211D4C14B80F，入库单=C1D407738E1143648BC7980468A399B8）</param>
        /// <param name="red_falg">红单标志</param>
        /// <param name="maxRowCount">当前页显示数量</param>
        /// <param name="startRowIndex">当前页开始数量</param>
        /// <param name="DeliveryStatus">状态</param>
        /// <param name="DeliveryId">配送方式</param>
        /// <param name="DefrayTypeId">支付方式</param>
        /// <returns></returns>
        public InoutInfo SearchInoutInfo_lj(string PayStatus
                                       , string order_no
                                       , string order_reason_type_id
                                       , string sales_unit_id
                                       , string warehouse_id
                                       , string purchase_unit_id
                                       , string status
                                       , string order_date_begin  //成交日期
                                       , string order_date_end    //成交日期
                                       , string complete_date_begin   //结束日期
                                       , string complete_date_end
                                       , string data_from_id
                                       , string ref_order_no
                                       , string order_type_id
                                       , string red_falg
                                       , int maxRowCount
                                       , int startRowIndex
                                       , string purchase_warehouse_id
                                       , string sales_warehouse_id
                                       , string DeliveryStatus
                                       , string DeliveryId
                                       , string DefrayTypeId
                                       , string DeliveryDateBegin   //配送日期
                                       , string DeliveryDateEnd    //
                                       , string CancelDateBegin
                                       , string CancelDateEnd, string order_id, string vipId,
            string path_unit_id, string timestamp, string InoutSort, bool getDetail = false)//getDetail默认是false，不是取详细信息。
        {
            InoutInfo inoutInfo = new InoutInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.InoutSort = InoutSort; //排序
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.sales_unit_id = sales_unit_id;
            orderSearchInfo.warehouse_id = warehouse_id;
            orderSearchInfo.purchase_unit_id = purchase_unit_id;
            orderSearchInfo.status = status;
            orderSearchInfo.order_date_begin = order_date_begin;//成交日期
            orderSearchInfo.order_date_end = order_date_end;//成交日期
            orderSearchInfo.complete_date_begin = complete_date_begin;//结束日期
            orderSearchInfo.complete_date_end = complete_date_end;
            orderSearchInfo.data_from_id = data_from_id;
            orderSearchInfo.ref_order_no = ref_order_no;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            orderSearchInfo.red_flag = red_falg;
            orderSearchInfo.purchase_warehouse_id = purchase_warehouse_id;
            orderSearchInfo.sales_warehouse_id = sales_warehouse_id;
            orderSearchInfo.DeliveryStatus = DeliveryStatus;
            orderSearchInfo.DeliveryId = DeliveryId;
            orderSearchInfo.DefrayTypeId = DefrayTypeId;
            orderSearchInfo.DeliveryDateBegin = DeliveryDateBegin;   //配送日期？结束日期
            orderSearchInfo.DeliveryDateEnd = DeliveryDateEnd;
            orderSearchInfo.CancelDateBegin = CancelDateBegin;
            orderSearchInfo.CancelDateEnd = CancelDateEnd;
            orderSearchInfo.order_id = order_id;
            orderSearchInfo.vip_no = vipId;
            orderSearchInfo.path_unit_id = path_unit_id;
            orderSearchInfo.timestamp = timestamp;

            orderSearchInfo.PayStatus = PayStatus;

            inoutInfo = SearchInoutInfo_lj(orderSearchInfo, getDetail);
            return inoutInfo;
        }

        //返回订单数据的优化接口，不返回各种状态订单数量
        public InoutInfo SearchInoutInfo_lj2(string PayStatus
                                    , string order_no
                                    , string order_reason_type_id
                                    , string sales_unit_id
                                    , string warehouse_id
                                    , string purchase_unit_id
                                    , string status
                                    , string order_date_begin  //成交日期
                                    , string order_date_end    //成交日期
                                    , string complete_date_begin   //结束日期
                                    , string complete_date_end
                                    , string data_from_id
                                    , string ref_order_no
                                    , string order_type_id
                                    , string red_falg
                                    , int maxRowCount
                                    , int startRowIndex
                                    , string purchase_warehouse_id
                                    , string sales_warehouse_id
                                    , string DeliveryStatus
                                    , string DeliveryId
                                    , string DefrayTypeId
                                    , string DeliveryDateBegin   //配送日期
                                    , string DeliveryDateEnd    //
                                    , string CancelDateBegin
                                    , string CancelDateEnd, string order_id, string vipId,
         string path_unit_id, string timestamp, string InoutSort, bool getDetail = false)//getDetail默认是false，不是取详细信息。
        {
            InoutInfo inoutInfo = new InoutInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.InoutSort = InoutSort; //排序
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.sales_unit_id = sales_unit_id;
            orderSearchInfo.warehouse_id = warehouse_id;
            orderSearchInfo.purchase_unit_id = purchase_unit_id;
            orderSearchInfo.status = status;
            orderSearchInfo.order_date_begin = order_date_begin;//成交日期
            orderSearchInfo.order_date_end = order_date_end;//成交日期
            orderSearchInfo.complete_date_begin = complete_date_begin;//结束日期
            orderSearchInfo.complete_date_end = complete_date_end;
            orderSearchInfo.data_from_id = data_from_id;
            orderSearchInfo.ref_order_no = ref_order_no;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            orderSearchInfo.red_flag = red_falg;
            orderSearchInfo.purchase_warehouse_id = purchase_warehouse_id;
            orderSearchInfo.sales_warehouse_id = sales_warehouse_id;
            orderSearchInfo.DeliveryStatus = DeliveryStatus;
            orderSearchInfo.DeliveryId = DeliveryId;
            orderSearchInfo.DefrayTypeId = DefrayTypeId;
            orderSearchInfo.DeliveryDateBegin = DeliveryDateBegin;   //配送日期？结束日期
            orderSearchInfo.DeliveryDateEnd = DeliveryDateEnd;
            orderSearchInfo.CancelDateBegin = CancelDateBegin;
            orderSearchInfo.CancelDateEnd = CancelDateEnd;
            orderSearchInfo.order_id = order_id;
            orderSearchInfo.vip_no = vipId;
            orderSearchInfo.path_unit_id = path_unit_id;
            orderSearchInfo.timestamp = timestamp;

            orderSearchInfo.PayStatus = PayStatus;

            inoutInfo = SearchInoutInfo_lj2(orderSearchInfo, getDetail);
            return inoutInfo;
        }

        //优化，仅返回各种订单的数量
             public InoutInfo SearchInoutInfo_lj3(string PayStatus
                                       , string order_no
                                       , string order_reason_type_id
                                       , string sales_unit_id
                                       , string warehouse_id
                                       , string purchase_unit_id
                                       , string status
                                       , string order_date_begin  //成交日期
                                       , string order_date_end    //成交日期
                                       , string complete_date_begin   //结束日期
                                       , string complete_date_end
                                       , string data_from_id
                                       , string ref_order_no
                                       , string order_type_id
                                       , string red_falg
                                       , int maxRowCount
                                       , int startRowIndex
                                       , string purchase_warehouse_id
                                       , string sales_warehouse_id
                                       , string DeliveryStatus
                                       , string DeliveryId
                                       , string DefrayTypeId
                                       , string DeliveryDateBegin   //配送日期
                                       , string DeliveryDateEnd    //
                                       , string CancelDateBegin
                                       , string CancelDateEnd, string order_id, string vipId,
            string path_unit_id, string timestamp, string InoutSort, bool getDetail = false)//getDetail默认是false，不是取详细信息。
        {
            InoutInfo inoutInfo = new InoutInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.InoutSort = InoutSort; //排序
            orderSearchInfo.order_no = order_no;
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.sales_unit_id = sales_unit_id;
            orderSearchInfo.warehouse_id = warehouse_id;
            orderSearchInfo.purchase_unit_id = purchase_unit_id;
            orderSearchInfo.status = status;
            orderSearchInfo.order_date_begin = order_date_begin;//成交日期
            orderSearchInfo.order_date_end = order_date_end;//成交日期
            orderSearchInfo.complete_date_begin = complete_date_begin;//结束日期
            orderSearchInfo.complete_date_end = complete_date_end;
            orderSearchInfo.data_from_id = data_from_id;
            orderSearchInfo.ref_order_no = ref_order_no;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.StartRow = startRowIndex;
            orderSearchInfo.EndRow = startRowIndex + maxRowCount;
            orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            orderSearchInfo.red_flag = red_falg;
            orderSearchInfo.purchase_warehouse_id = purchase_warehouse_id;
            orderSearchInfo.sales_warehouse_id = sales_warehouse_id;
            orderSearchInfo.DeliveryStatus = DeliveryStatus;
            orderSearchInfo.DeliveryId = DeliveryId;
            orderSearchInfo.DefrayTypeId = DefrayTypeId;
            orderSearchInfo.DeliveryDateBegin = DeliveryDateBegin;   //配送日期？结束日期
            orderSearchInfo.DeliveryDateEnd = DeliveryDateEnd;
            orderSearchInfo.CancelDateBegin = CancelDateBegin;
            orderSearchInfo.CancelDateEnd = CancelDateEnd;
            orderSearchInfo.order_id = order_id;
            orderSearchInfo.vip_no = vipId;
            orderSearchInfo.path_unit_id = path_unit_id;
            orderSearchInfo.timestamp = timestamp;

            orderSearchInfo.PayStatus = PayStatus;

            inoutInfo = SearchInoutInfo_lj3(orderSearchInfo, getDetail);
            return inoutInfo;
        }


        /// <summary>
        /// 查询未审核订单数
        /// </summary>
        /// <param name="order_reason_type_id"></param>
        /// <param name="order_type_id"></param>
        /// <param name="path_unit_id"></param>
        /// <returns></returns>
        public int GetPosOrderUnAuditTotalCount(string order_reason_type_id, string order_type_id, string path_unit_id)
        {
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
            orderSearchInfo.order_reason_id = order_reason_type_id;
            orderSearchInfo.order_type_id = order_type_id;
            orderSearchInfo.path_unit_id = path_unit_id;

            return SearchUnAuditTypeCount(orderSearchInfo);
        }
        #endregion


        #region inout类型单据保存
        /// <summary>
        /// inout 单据保存
        /// </summary>
        /// <param name="inoutInfo">inout model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns></returns>
        public bool SetInoutInfo(InoutInfo inoutInfo, bool IsTrans, out string strError)
        {

            if (inoutInfo.order_id == null || inoutInfo.order_id.Equals(""))
            {
                inoutInfo.order_id = NewGuid();
                inoutInfo.operate = "Create";
            }

            InoutInfo inoutInfo1 = new InoutInfo();
            inoutInfo1 = GetInoutInfoById(inoutInfo.order_id);
            if (inoutInfo1 == null || inoutInfo1.order_id == null || inoutInfo1.order_id.Equals("")) { inoutInfo.operate = "Create"; } else { inoutInfo.operate = "Modify"; }

            if (inoutInfo.create_user_id == null || inoutInfo.create_user_id.Equals(""))
            {
                inoutInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                inoutInfo.create_time = GetCurrentDateTime();
            }
            if (inoutInfo.modify_user_id == null || inoutInfo.modify_user_id.Equals(""))
            {
                inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                inoutInfo.modify_time = GetCurrentDateTime();
            }
            inoutInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

            if (inoutInfo.InoutDetailList != null)
            {
                foreach (InoutDetailInfo inoutDetailInfo in inoutInfo.InoutDetailList)
                {
                    if (inoutDetailInfo.order_detail_id == null || inoutDetailInfo.order_detail_id.Equals(""))
                    {
                        inoutDetailInfo.order_detail_id = NewGuid();
                        inoutDetailInfo.create_time = GetCurrentDateTime();
                        inoutDetailInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    }
                    else
                    {
                        inoutDetailInfo.modify_time = GetCurrentDateTime();
                        inoutDetailInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    }
                    inoutDetailInfo.order_id = inoutInfo.order_id;
                }
            }

            string strCount = string.Empty;
            if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null || loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
            {
                inoutInfo.status = "1";
                inoutInfo.status_desc = "未审批";
            }
            else
            {
                if (inoutInfo.operate.Equals("Create")
                    && (!new BillService(loggingSessionInfo).CanHaveBill(inoutInfo.order_id)))
                {
                    //2.提交表单
                    if (!SetInoutOrderInsertBill(inoutInfo))
                    {
                        strError = "inout表单提交失败。";
                        throw (new System.Exception(strError));
                    }
                    //3.更改状态
                    BillModel billInfo = new cBillService(loggingSessionInfo).GetBillById(inoutInfo.order_id);
                    if (billInfo != null)
                    {
                        inoutInfo.status = billInfo.Status;
                        inoutInfo.status_desc = billInfo.BillStatusDescription;
                        strError = billInfo.Status + "--" + inoutInfo.order_id;
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


            bool bReturn = inoutService.SetInoutInfo(inoutInfo, IsTrans, out strError);

            return bReturn;
        }


        // <summary>
        /// 单子提交到表单
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <returns></returns>
        private bool SetInoutOrderInsertBill(InoutInfo inoutInfo)
        {
            try
            {
                BillModel bill = new BillModel();
                cBillService bs = new cBillService(loggingSessionInfo);

                bill.Id = inoutInfo.order_id;
                DataSet ds = new DataSet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string order_type_id = bs.GetBillKindByCode(inoutInfo.BillKindCode).Id;

                    bill.Code = new AppSysService(loggingSessionInfo).GetNo(inoutInfo.BillKindCode);

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

        #region inout单据，状态修改

        /// <summary>
        /// Inout状态修改（审核，删除。。。。）
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <returns></returns>
        public bool SetInoutOrderStatus(string order_id, BillActionType billActionType)
        {
            string err;
            return SetInoutOrderStatus(order_id, billActionType, out err);
        }

        /// <summary>
        /// Inout状态修改（审核，删除。。。。）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <param name="strError">输出信息</param>
        /// <returns></returns>
        public bool SetInoutOrderStatus(string order_id, BillActionType billActionType, out string strError)
        {
            string strResult = string.Empty;
            try
            {
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.order_id = order_id;
                inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                inoutInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null || loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
                {

                    switch (billActionType)
                    {
                        case BillActionType.Cancel:
                            inoutInfo.status = "-1";
                            inoutInfo.status_desc = "删除";
                            break;
                        case BillActionType.Approve:
                            inoutInfo.status = "10";
                            inoutInfo.status_desc = "已审批";
                            inoutInfo.approve_time = GetCurrentDateTime();
                            inoutInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                            break;
                        case BillActionType.Reject:
                            inoutInfo.status = "1";
                            inoutInfo.status_desc = "未审批";
                            break;
                        case BillActionType.Create:
                            inoutInfo.status = "1";
                            inoutInfo.status_desc = "未审批";
                            break;
                        default:
                            inoutInfo.status = "1";
                            inoutInfo.status_desc = "未审批";
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

                        inoutInfo.status = billInfo.Status;
                        inoutInfo.status_desc = billInfo.BillStatusDescription;

                        if (billActionType == BillActionType.Approve)
                        {
                            inoutInfo.approve_time = GetCurrentDateTime();
                            inoutInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        }
                    }
                    else
                    {
                        strError = "获取状态失败--" + strResult;
                        return false;
                    }
                }
                bool bReturn = inoutService.SetInoutStatus(inoutInfo);
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

        #region inout单据查询
        /// <summary>
        /// inout 单据查询
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public InoutInfo SearchInoutInfo(OrderSearchInfo orderSearchInfo)
        {
            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutInfo inoutInfo = new InoutInfo();
                #region 获取POS小票的不同单据数量 Jermyn20130906
                if (orderSearchInfo != null
                    && orderSearchInfo.order_reason_id.Equals("2F6891A2194A4BBAB6F17B4C99A6C6F5")
                    && orderSearchInfo.order_type_id.Equals("1F0A100C42484454BAEA211D4C14B80F"))
                {
                    DataSet ds1 = null;
                    ds1 = inoutService.SearchStatusTypeCount(orderSearchInfo);
                    if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds1.Tables[0].Rows)
                        {
                            switch (dr["StatusType"].ToString())
                            {
                                case "100":
                                    inoutInfo.StatusCount1 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "300":
                                    inoutInfo.StatusCount2 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "500":
                                    inoutInfo.StatusCount3 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "600":
                                    inoutInfo.StatusCount4 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "700":
                                    inoutInfo.StatusCount5 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "800":
                                    inoutInfo.StatusCount0 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "900":
                                    inoutInfo.StatusCount99 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                            }
                        }
                        //
                    }
                }
                #endregion
                int iCount = inoutService.SearchInoutCount(orderSearchInfo);//cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Inout.SearchCount", orderSearchInfo);
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                DataSet ds = new DataSet();
                ds = inoutService.SearchInoutInfo(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfoList = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }
                inoutInfo.ICount = iCount;
                inoutInfo.InoutInfoList = inoutInfoList;
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// inout 单据Detail查询
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public InoutDetailInfo SearchInoutDetailInfo(OrderSearchInfo orderSearchInfo)
        {
            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutDetailInfo inoutDetailInfo = new InoutDetailInfo();
                int iCount = inoutService.SearchInoutDetailCount(orderSearchInfo);
                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                DataSet ds = new DataSet();
                ds = inoutService.SearchInoutDetailInfo(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutDetailInfoList = DataTableToObject.ConvertToList<InoutDetailInfo>(ds.Tables[0]);
                }
                inoutDetailInfo.ICount = iCount;
                inoutDetailInfo.InoutDetailList = inoutDetailInfoList;
                return inoutDetailInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #region Jermyn20130524+ 处理门店消费记录
        /// <summary>
        /// 获取vip的消费记录
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public InoutDetailInfo SearchInoutDetailInfoByVip(OrderSearchInfo orderSearchInfo)
        {
            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutDetailInfo inoutDetailInfo = new InoutDetailInfo();
                int iCount = inoutService.SearchInoutDetailInfoByVipCount(orderSearchInfo);
                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                DataSet ds = new DataSet();
                ds = inoutService.SearchInoutDetailInfoByVipList(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutDetailInfoList = DataTableToObject.ConvertToList<InoutDetailInfo>(ds.Tables[0]);
                }
                inoutDetailInfo.ICount = iCount;
                inoutDetailInfo.InoutDetailList = inoutDetailInfoList;
                return inoutDetailInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #endregion


        #region inout单据查询 jifeng.cao 20140319
        /// <summary>
        /// inout 单据查询
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public InoutInfo SearchInoutInfo_lj(OrderSearchInfo orderSearchInfo, bool getDetail = false)
        {
            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.StatusManagerList = new List<StatusManager>();

                //获取不同状态订单数量
                #region 获取POS小票的不同单据数量 Jermyn20130906
                //
                if (orderSearchInfo != null
                    && orderSearchInfo.order_reason_id.Equals("2F6891A2194A4BBAB6F17B4C99A6C6F5")
                    && orderSearchInfo.order_type_id.Equals("1F0A100C42484454BAEA211D4C14B80F"))
                {
                    DataSet ds1 = null;
                   // ds1 = inoutService.SearchStatusTypeCount_lj(orderSearchInfo);
                    ds1 = inoutService.SearchStatusTypeCount_lj2(orderSearchInfo);//按照优化后的
                    if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        //foreach (DataRow dr in ds1.Tables[0].Rows)
                        //{
                        //    switch (dr["StatusType"].ToString())
                        //    {
                        //        case "100":
                        //            inoutInfo.StatusCount1 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "200":
                        //            inoutInfo.StatusCount2 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "300":
                        //            inoutInfo.StatusCount3 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "400":
                        //            inoutInfo.StatusCount4 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "500":
                        //            inoutInfo.StatusCount5 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "600":
                        //            inoutInfo.StatusCount6 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "700":
                        //            inoutInfo.StatusCount7 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "800":
                        //            inoutInfo.StatusCount8 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "900":
                        //            inoutInfo.StatusCount9 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //    }
                        //}

                        //动态获取对应客户的所有订单状态值及每个状态值的订单数量(jifeng.cao 20140418)
                        inoutInfo.StatusManagerList = DataTableToObject.ConvertToList<StatusManager>(ds1.Tables[0]);
                    }
                }

                #endregion
                //获取总数量
                // int iCount = inoutService.SearchInoutCount(orderSearchInfo);//cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Inout.SearchCount", orderSearchInfo);
                int iCount = inoutService.SearchInoutCount2(orderSearchInfo);
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                DataSet ds = new DataSet();
                //获取订单列表
                //  ds = inoutService.SearchInoutInfo_lj(orderSearchInfo);  //这里真正查数据
                ds = inoutService.SearchInoutInfo_lj2(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfoList = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }
                inoutInfo.ICount = iCount;

                if (getDetail)
                {
                    foreach (var item in inoutInfoList)
                    {
                        item.InoutDetailList = GetInoutDetailInfoByOrderId(item.order_id); //根据订单的ID取数据，查找详细信息
                    }
                }

                inoutInfo.InoutInfoList = inoutInfoList;
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       //优化后获取订单数据的，去出了获取各种状态订单数量的接口
        public InoutInfo SearchInoutInfo_lj2(OrderSearchInfo orderSearchInfo, bool getDetail = false)
        {

            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.StatusManagerList = new List<StatusManager>();
                //大问题*****PosOrder_lj，GetPosOrderTotalCount_lj两个方法都调用了这个方法，而且是在一个接口里使用。
                //获取不同状态订单数量
               
                #region 获取POS小票的不同单据数量 Jermyn20130906
                /**
                //
                if (orderSearchInfo != null
                    && orderSearchInfo.order_reason_id.Equals("2F6891A2194A4BBAB6F17B4C99A6C6F5")
                    && orderSearchInfo.order_type_id.Equals("1F0A100C42484454BAEA211D4C14B80F"))
                {
                    DataSet ds1 = null;
                    // ds1 = inoutService.SearchStatusTypeCount_lj(orderSearchInfo);
                    ds1 = inoutService.SearchStatusTypeCount_lj2(orderSearchInfo);//按照优化后的
                    if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        //foreach (DataRow dr in ds1.Tables[0].Rows)
                        //{
                        //    switch (dr["StatusType"].ToString())
                        //    {
                        //        case "100":
                        //            inoutInfo.StatusCount1 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "200":
                        //            inoutInfo.StatusCount2 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "300":
                        //            inoutInfo.StatusCount3 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "400":
                        //            inoutInfo.StatusCount4 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "500":
                        //            inoutInfo.StatusCount5 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "600":
                        //            inoutInfo.StatusCount6 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "700":
                        //            inoutInfo.StatusCount7 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "800":
                        //            inoutInfo.StatusCount8 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "900":
                        //            inoutInfo.StatusCount9 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //    }
                        //}

                        //动态获取对应客户的所有订单状态值及每个状态值的订单数量(jifeng.cao 20140418)
                        inoutInfo.StatusManagerList = DataTableToObject.ConvertToList<StatusManager>(ds1.Tables[0]);
                    }
                }
                 * **/
                
                #endregion
                //获取总数量
                // int iCount = inoutService.SearchInoutCount(orderSearchInfo);//cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Inout.SearchCount", orderSearchInfo);
                int iCount = inoutService.SearchInoutCount2(orderSearchInfo);
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                DataSet ds = new DataSet();
                //获取订单列表
                //  ds = inoutService.SearchInoutInfo_lj(orderSearchInfo);  //这里真正查数据
                ds = inoutService.SearchInoutInfo_lj2(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfoList = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }
                inoutInfo.ICount = iCount;

                if (getDetail)
                {
                    foreach (var item in inoutInfoList)
                    {
                        item.InoutDetailList = GetInoutDetailInfoByOrderId(item.order_id); //根据订单的ID取数据，查找详细信息
                    }
                }

                inoutInfo.InoutInfoList = inoutInfoList;
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public InoutInfo SearchInoutInfo_lj3(OrderSearchInfo orderSearchInfo, bool getDetail = false)
        {

            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.StatusManagerList = new List<StatusManager>();
                //大问题*****PosOrder_lj，GetPosOrderTotalCount_lj两个方法都调用了这个方法，而且是在一个接口里使用。
                //获取不同状态订单数量
             
                #region 获取POS小票的不同单据数量 Jermyn20130906
                //
                if (orderSearchInfo != null
                    && orderSearchInfo.order_reason_id.Equals("2F6891A2194A4BBAB6F17B4C99A6C6F5")
                    && orderSearchInfo.order_type_id.Equals("1F0A100C42484454BAEA211D4C14B80F"))
                {
                    DataSet ds1 = null;
                    // ds1 = inoutService.SearchStatusTypeCount_lj(orderSearchInfo);
                    ds1 = inoutService.SearchStatusTypeCount_lj2(orderSearchInfo);//按照优化后的
                    if (ds1 != null && ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        //foreach (DataRow dr in ds1.Tables[0].Rows)
                        //{
                        //    switch (dr["StatusType"].ToString())
                        //    {
                        //        case "100":
                        //            inoutInfo.StatusCount1 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "200":
                        //            inoutInfo.StatusCount2 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "300":
                        //            inoutInfo.StatusCount3 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "400":
                        //            inoutInfo.StatusCount4 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "500":
                        //            inoutInfo.StatusCount5 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "600":
                        //            inoutInfo.StatusCount6 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "700":
                        //            inoutInfo.StatusCount7 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "800":
                        //            inoutInfo.StatusCount8 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //        case "900":
                        //            inoutInfo.StatusCount9 = Convert.ToInt32(dr["StatusCount"].ToString());
                        //            break;
                        //    }
                        //}

                        //动态获取对应客户的所有订单状态值及每个状态值的订单数量(jifeng.cao 20140418)
                        inoutInfo.StatusManagerList = DataTableToObject.ConvertToList<StatusManager>(ds1.Tables[0]);
                    }
                }

               
                #endregion
                /**
                //获取总数量
                // int iCount = inoutService.SearchInoutCount(orderSearchInfo);//cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Inout.SearchCount", orderSearchInfo);
                int iCount = inoutService.SearchInoutCount2(orderSearchInfo);
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                DataSet ds = new DataSet();
                //获取订单列表
                //  ds = inoutService.SearchInoutInfo_lj(orderSearchInfo);  //这里真正查数据
                ds = inoutService.SearchInoutInfo_lj2(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfoList = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }
                inoutInfo.ICount = iCount;

                if (getDetail)
                {
                    foreach (var item in inoutInfoList)
                    {
                        item.InoutDetailList = GetInoutDetailInfoByOrderId(item.order_id); //根据订单的ID取数据，查找详细信息
                    }
                }
               
                inoutInfo.InoutInfoList = inoutInfoList; 
                 * **/
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




        /// <summary>
        /// 修改订单门店
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public int SetOrderUnit(string orderList, string unitID)
        {
            try
            {
                return inoutService.SetOrderUnit(orderList, unitID);
            }
            catch (Exception)
            {
                return -1;
            }

        }

        /// <summary>
        /// 查询未审核订单数
        /// </summary>
        /// <returns></returns>
        /// add by donal 2014-10-10 17:21:19
        public int SearchUnAuditTypeCount(OrderSearchInfo orderSearchInfo)
        {
            try
            {
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                return inoutService.SearchUnAuditTypeCount(orderSearchInfo);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        /// <summary>
        /// 获取订单运费
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal? GetDeliveryAmountByOrderId(string orderId)
        {
            return inoutService.GetDeliveryAmountByOrderId(orderId);
        }
        #region 单个单据详细信息

        /// <summary>
        /// 获取单个进出库单据的详细信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InoutInfo GetInoutInfoByIdDelivery(string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                InoutInfo inoutInfo = new InoutInfo();
                DataSet ds = inoutService.GetInoutInfoByIdDelivery(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfo = DataTableToObject.ConvertToObject<InoutInfo>(ds.Tables[0].Rows[0]);
                }
                //inoutInfo.InoutDetailList = GetInoutDetailInfoByOrderId(orderId);
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取单个进出库单据的详细信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InoutInfo GetInoutInfoById(string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                InoutInfo inoutInfo = new InoutInfo();
                DataSet ds = inoutService.GetInoutInfoById(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfo = DataTableToObject.ConvertToObject<InoutInfo>(ds.Tables[0].Rows[0]);
                }
                inoutInfo.InoutDetailList = GetInoutDetailInfoByOrderId(orderId);
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取配送单 对应的订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetDeliveryDetail(string orderId)
        {
            return inoutService.GetDeliveryDetail(orderId);
        }
        /// <summary>
        /// 获取单个进出库单据的详细信息 jifeng.cao 20140320
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InoutInfo GetInoutInfoById_lj(string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                InoutInfo inoutInfo = new InoutInfo();
                DataSet ds = inoutService.GetInoutInfoById_lj(orderId);//基本信息
                VipDAO vipDao = new VipDAO(loggingSessionInfo);
                T_UserDAO userDao = new T_UserDAO(loggingSessionInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfo = DataTableToObject.ConvertToObject<InoutInfo>(ds.Tables[0].Rows[0]);//转换成对象
                    //读取销售人员名称(会员/员工)
                    if (inoutInfo != null)
                    {
                        string tempName = string.Empty;
                        if (inoutInfo.data_from_id == "3") //订单来自微信
                        {
                            var vipInfo = vipDao.GetByID(inoutInfo.sales_user);
                            if (vipInfo != null)
                                tempName = vipInfo.VipName;
                        }
                        else
                        {
                            var userInfo = userDao.GetByID(inoutInfo.sales_user);
                            if (userInfo != null)
                                tempName = userInfo.user_name;
                        }
                        inoutInfo.sales_user_name = tempName;
                    }

                    decimal vipDiscount = 0;//会员折扣金额
                    if (inoutInfo.discount_rate > 0)
                    {
                        var tempAmount = inoutInfo.actual_amount - inoutInfo.DeliveryAmount; //应付-运费后的应付金额
                        vipDiscount = tempAmount / (inoutInfo.discount_rate / 100) - tempAmount;// (应付-运费)/折扣率=去除折扣后实付Y；Y-包含折扣的实付=会员折扣
                    }
                    inoutInfo.VipDiscount = Math.Round(vipDiscount,2);

                }
                inoutInfo.InoutDetailList = GetInoutDetailInfoByOrderId(orderId);//获取明细信息
                return inoutInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 根据单据号获取单据详细信息
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public InoutInfo GetInoutInfoByOrderCode(string orderCode)
        {
            try
            {
                InoutInfo inoutInfo = new InoutInfo();
                DataSet ds = inoutService.GetInoutInfoByOrderCode(orderCode);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutInfo = DataTableToObject.ConvertToObject<InoutInfo>(ds.Tables[0].Rows[0]);
                    inoutInfo.InoutDetailList = GetInoutDetailInfoByOrderId(inoutInfo.order_id);
                }

                return inoutInfo;
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
        public IList<InoutDetailInfo> GetInoutDetailInfoByOrderId(string orderId)
        {
            try
            {
                IList<InoutDetailInfo> inoutDetailList = new List<InoutDetailInfo>();
                DataSet ds = inoutService.GetInoutDetailInfoByOrderId(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutDetailList = DataTableToObject.ConvertToList<InoutDetailInfo>(ds.Tables[0]);
                }
                return inoutDetailList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 根据班次获取inout pos小票集合
        /// <summary>
        /// 根据班次标识获取pos小票信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="shiftId">班次标识</param>
        /// <returns></returns>
        public IList<InoutInfo> GetInoutListByShiftId(LoggingSessionInfo loggingSessionInfo, string shiftId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("ShiftId", shiftId);
                IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
                //return cSqlMapper.Instance().QueryForList<InoutInfo>("Inout.SelectByShiftId", _ht);
                return inoutInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 批量处理上传标志
        /// <summary>
        /// 批量修改上传标志
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="if_flag">上传标志</param>
        /// <param name="InoutInfoList">单据集合</param>
        /// <param name="IsTrans">是否提交</param>
        /// <returns></returns>
        public bool SetInoutIfFlag(LoggingSessionInfo loggingSessionInfo, string if_flag, IList<InoutInfo> InoutInfoList, bool IsTrans)
        {
            //if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            try
            {
                bool bReturn = false;
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.if_flag = if_flag;
                inoutInfo.InoutInfoList = InoutInfoList;
                inoutInfo.modify_time = GetCurrentDateTime();
                inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Inout.UpdateIfflag", inoutInfo);
                //if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
                return bReturn;
            }
            catch (Exception ex)
            {
                //if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        #endregion

        #region 上传到管理平台的inout单据
        /// <summary>
        /// 获取Inout的未下载数据集合
        /// </summary>
        /// <param name="orderSearchInfo">参数对像</param>
        /// <returns></returns>
        public int GetInoutNotPackagedCountWeb(OrderSearchInfo orderSearchInfo)
        {
            //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(orderSearchInfo.customer_id);
            //return cSqlMapper.Instance(loggingManager).QueryForObject<int>("Inout.SelectUnDownloadCount", orderSearchInfo);
            return 0;
        }
        /// <summary>
        /// 获取Inout信息集合
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public IList<InoutInfo> GetInoutListPackagedWeb(OrderSearchInfo orderSearchInfo)
        {
            //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(orderSearchInfo.customer_id);
            //return cSqlMapper.Instance(loggingManager).QueryForList<InoutInfo>("Inout.SelectUnDownloadInout", orderSearchInfo);
            IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
            return inoutInfoList;
        }
        /// <summary>
        /// 下载进出库单明细
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="inoutInfoList"></param>
        /// <returns></returns>
        public IList<InoutDetailInfo> GetInoutDetailListPackageWeb(string Customer_Id, string Unit_Id, List<InoutInfo> inoutInfoList)
        {
            try
            {
                //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
                //InoutInfo inoutInfo = new InoutInfo();
                //inoutInfo.InoutInfoList = inoutInfoList;
                //return cSqlMapper.Instance(loggingManager).QueryForList<InoutDetailInfo>("InoutDetail.SelectUnDownloadInoutDetail", inoutInfo);
                IList<InoutDetailInfo> inoutDetailInfo = new List<InoutDetailInfo>();
                return inoutDetailInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 更新批次号
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="inoutInfo">订单信息</param>
        /// <returns></returns>
        public bool SetInoutUpdateUnDownloadBatIdWeb(string Customer_Id, InoutInfo inoutInfo)
        {
            //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
            //cSqlMapper.Instance(loggingManager).Update("Inout.UpdateUnDownloadBatId", inoutInfo);
            return true;

        }
        /// <summary>
        /// 更改上传标志
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <param name="inoutInfo"></param>
        /// <returns></returns>
        public bool SetInoutIfFlagInfoWeb(string Customer_Id, InoutInfo inoutInfo)
        {
            //LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
            //cSqlMapper.Instance(loggingManager).Update("Inout.UpdateUnDownloadIfFlag", inoutInfo);
            return true;
        }
        #endregion

        #region 根据区县ID + 时间戳获取这段时间内发生订单的门店ID

        public List<Entity.Unit.UnitInfo> GetUnitIdList(string cityId, string timestamp)
        {
            try
            {
                DataSet ds = inoutService.GetUnitIdList(cityId, timestamp);
                List<Entity.Unit.UnitInfo> unitIdList = DataTableToObject.ConvertToList<Entity.Unit.UnitInfo>(ds.Tables[0]);

                return unitIdList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 获取销量前3的商品
        /// <summary>
        /// 获取销量前3的商品
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public IList<InoutDetailInfo> GetInoutDetailInfoByTop3(OrderSearchInfo queryInfo)
        {
            try
            {
                IList<InoutDetailInfo> inoutDetailList = new List<InoutDetailInfo>();
                DataSet ds = inoutService.GetInoutDetailInfoByTop3(queryInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    inoutDetailList = DataTableToObject.ConvertToList<InoutDetailInfo>(ds.Tables[0]);
                }
                return inoutDetailList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region wap提交销售POS小票订单 Jermyn20130718
        /// <summary>
        /// wap提交销售POS小票订单
        /// </summary>
        /// <param name="SkuId"></param>
        /// <param name="EventId"></param>
        /// <param name="OpenId"></param>
        /// <param name="WeiXinId"></param>
        /// <param name="UserName"></param>
        /// <param name="Phone"></param>
        /// <param name="individuationInfo"></param>
        /// <param name="salesPrice">售价</param>
        /// <param name="tableNumber">桌号</param>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="strError">输出提示信息</param>
        /// <returns></returns>
        public bool SetWapPosInoutInfo(string SkuId
                                      , string EventId
                                      , string OpenId
                                      , string WeiXinId
                                      , string UserName
                                      , string Phone
                                      , string individuationInfo
                                      , string salesPrice
                                      , string tableNumber
                                      , LoggingSessionInfo loggingSessionInfo
                                      , out string strError
                                      , out string strMsg)
        {
            strMsg = "";
            #region
            if (SkuId == null || SkuId.Equals(""))
            {
                strError = "必须选择商品";
                return false;
            }
            if (OpenId == null || OpenId.Equals(""))
            {
                strError = "客户唯一码不能为空";
                return false;
            }

            if (WeiXinId == null || WeiXinId.Equals(""))
            {
                strError = "微信公众号不能为空";
                return false;
            }

            if (UserName == null || UserName.Equals(""))
            {
                strError = "用户名不能为空";
                return false;
            }

            if (Phone == null || Phone.Equals(""))
            {
                strError = "手机号码不能为空";
                return false;
            }
            if (individuationInfo == null || individuationInfo.Equals(""))
            {
                strError = "个性化信息不能为空";
                return false;
            }
            if (salesPrice == null || salesPrice.Equals(""))
            {
                strError = "销售价格不能为空";
                return false;
            }
            #endregion
            try
            {
                string unitId = "66f1300ab9464a508e9f2ac223e9ae34";
                string customerId = "e703dbedadd943abacf864531decdac1";
                string orderNo = string.Empty;
                TUnitExpandBLL unitExpandBll = new TUnitExpandBLL(loggingSessionInfo);
                orderNo = unitExpandBll.GetNowOrderNo(loggingSessionInfo, unitId);
                #region 获取客户信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.WeiXin = WeiXinId;
                vipInfo.WeiXinUserId = OpenId;
                vipInfo = vipServer.GetLjVipInfo(vipInfo);
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    strError = "不存在对应的客户信息";
                    return false;
                }
                #endregion

                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                inoutInfo.order_no = "DOlj" + orderNo;
                inoutInfo.red_flag = "1";
                inoutInfo.warehouse_id = "acafa2eff7f54206ae3516b53302df33";
                inoutInfo.order_date = System.DateTime.Now.ToString("yyyy-MM-dd");
                inoutInfo.create_unit_id = unitId;
                inoutInfo.unit_id = unitId;
                inoutInfo.total_amount = Convert.ToDecimal(salesPrice);
                inoutInfo.discount_rate = 100.00M;
                inoutInfo.actual_amount = Convert.ToDecimal(salesPrice);
                inoutInfo.print_times = 0;
                inoutInfo.total_qty = 1;
                inoutInfo.vip_no = vipInfo.VIPID;
                inoutInfo.data_from_id = "wap";
                inoutInfo.sales_unit_id = unitId;
                inoutInfo.if_flag = "0";
                inoutInfo.customer_id = customerId;
                inoutInfo.Field16 = orderNo;
                inoutInfo.Field6 = Phone;
                inoutInfo.Field17 = UserName;
                inoutInfo.Field18 = EventId;
                inoutInfo.Field19 = "未付款";
                inoutInfo.Field20 = tableNumber;
                #endregion

                InoutDetailInfo inoutDetailInfo = new InoutDetailInfo();
                #region
                inoutDetailInfo.sku_id = SkuId;
                inoutDetailInfo.unit_id = unitId;
                inoutDetailInfo.order_qty = 1;
                inoutDetailInfo.enter_qty = 1;
                inoutDetailInfo.std_price = Convert.ToDecimal(salesPrice);
                inoutDetailInfo.enter_amount = Convert.ToDecimal(salesPrice);
                inoutDetailInfo.discount_rate = 100.00M;
                inoutDetailInfo.display_index = 1;
                inoutDetailInfo.if_flag = 0;
                inoutDetailInfo.Field1 = individuationInfo;
                #endregion
                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                inoutDetailInfoList.Add(inoutDetailInfo);
                inoutInfo.InoutDetailList = inoutDetailInfoList;


                bool bReturn = SetInoutInfo(inoutInfo, true, out strError);
                if (bReturn)
                {
                    strMsg = "亲爱的会员" + vipInfo.VipName + "，您单号为" + orderNo + "的购买请求我们已经收到，请您到指定渠道下交纳钱款。谢谢再次惠顾！";
                }
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region GetInoutId
        /// <summary>
        /// 获取单据ID
        /// </summary>
        public string GetInoutId(InoutInfo queryInfo)
        {
            return inoutService.GetInoutId(queryInfo);
        }
        #endregion

        #region 已下订单数量(泸州老窖)
        public int GetHasOrderCount(string EventId)
        {
            return inoutService.GetHasOrderCount(EventId);
        }
        #endregion

        #region 获取已付款订单数(泸州老窖)
        public int GetHasPayCount(string EventId)
        {
            return inoutService.GetHasPayCount(EventId);
        }
        #endregion

        #region 获取已销售订单额(泸州老窖)
        public decimal GetHasSalesAmount(string EventId)
        {
            return inoutService.GetHasSalesAmount(EventId);
        }
        #endregion

        #region 获取所有销售订单额(泸州老窖)
        public decimal GetHasTotalAmount()
        {
            return inoutService.GetHasTotalAmount();
        }
        #endregion

        #region iAlumni
        #region 提交订单
        public bool SetiAlumniWapPosInoutInfo(string SkuId
                                      , string OrderId
                                      , string OpenId
                                      , string WeiXinId
                                      , string OrderNo
                                      , string qty
                                      , string stdPrice
                                      , string salesPrice
                                      , string totalAmount
                                      , string deliveryName
                                      , string deliveryRemark
                                      , LoggingSessionInfo loggingSessionInfo
                                      , out string strError
                                      , out string strMsg)
        {
            strMsg = "";
            #region
            if (SkuId == null || SkuId.Equals(""))
            {
                strError = "必须选择商品";
                return false;
            }
            if (OpenId == null || OpenId.Equals(""))
            {
                strError = "客户唯一码不能为空";
                return false;
            }

            if (WeiXinId == null || WeiXinId.Equals(""))
            {
                strError = "微信公众号不能为空";
                return false;
            }
            if (salesPrice == null || salesPrice.Equals(""))
            {
                strError = "销售价格不能为空";
                return false;
            }
            #endregion
            try
            {
                string unitId = "E5E125DCEC8A4F75AB7A0BA5EB141587";
                string customerId = "29E11BDC6DAC439896958CC6866FF64E";

                #region 获取客户信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.WeiXin = WeiXinId;
                vipInfo.WeiXinUserId = OpenId;
                vipInfo = vipServer.GetLjVipInfo(vipInfo);
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    strError = "不存在对应的客户信息";
                    return false;
                }
                #endregion

                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_id = OrderId;
                inoutInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                inoutInfo.order_no = OrderNo;
                inoutInfo.red_flag = "1";
                inoutInfo.warehouse_id = "67bb4c12785c42d4912aff7d34606592";
                inoutInfo.order_date = System.DateTime.Now.ToString("yyyy-MM-dd");
                inoutInfo.create_unit_id = unitId;
                inoutInfo.unit_id = unitId;
                inoutInfo.total_amount = Convert.ToDecimal(totalAmount);
                inoutInfo.discount_rate = Convert.ToDecimal(stdPrice) / Convert.ToDecimal(salesPrice);
                inoutInfo.actual_amount = Convert.ToDecimal(totalAmount);
                inoutInfo.print_times = 0;
                inoutInfo.total_qty = Convert.ToDecimal(qty);
                inoutInfo.vip_no = vipInfo.VIPID;
                inoutInfo.data_from_id = "wap";
                inoutInfo.sales_unit_id = unitId;
                inoutInfo.if_flag = "0";
                inoutInfo.customer_id = customerId;
                inoutInfo.Field19 = "已付款";
                inoutInfo.status = "10";
                inoutInfo.Field4 = deliveryRemark;
                inoutInfo.Field8 = deliveryName;
                #endregion

                InoutDetailInfo inoutDetailInfo = new InoutDetailInfo();
                #region
                inoutDetailInfo.order_id = OrderId;
                inoutDetailInfo.sku_id = SkuId;
                inoutDetailInfo.unit_id = unitId;
                inoutDetailInfo.order_qty = Convert.ToDecimal(qty);
                inoutDetailInfo.enter_qty = Convert.ToDecimal(qty);
                inoutDetailInfo.std_price = Convert.ToDecimal(stdPrice);
                inoutDetailInfo.enter_amount = Convert.ToDecimal(totalAmount);
                inoutDetailInfo.enter_price = Convert.ToDecimal(salesPrice);
                inoutDetailInfo.retail_price = Convert.ToDecimal(salesPrice);
                inoutDetailInfo.retail_amount = Convert.ToDecimal(totalAmount);
                inoutDetailInfo.discount_rate = Convert.ToDecimal(stdPrice) / Convert.ToDecimal(salesPrice);
                inoutDetailInfo.display_index = 1;
                inoutDetailInfo.if_flag = 0;
                #endregion
                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                inoutDetailInfoList.Add(inoutDetailInfo);
                inoutInfo.InoutDetailList = inoutDetailInfoList;

                bool bReturn = SetInoutInfo(inoutInfo, true, out strError);
                if (bReturn)
                {
                    strMsg = "亲爱的会员" + vipInfo.VipName + "，您单号为" + OrderNo + "的购买请求我们已经收到，请您到指定渠道下交纳钱款。谢谢再次惠顾！";
                }
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion
        #endregion

        #region 电子商城
        #region 提交订单
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <param name="strError"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool SetOrderOnlineShopping(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo
                                            , out string strError
                                            , out string strMsg)
        {
            strMsg = "";
            try
            {
                #region 获取客户信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.VIPID = SetOrderInfo.CreateBy;
                vipInfo.WeiXinUserId = SetOrderInfo.OpenId;
                vipInfo = vipServer.GetByID(vipInfo.VIPID);
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    strError = "不存在对应的客户信息";
                    return false;
                }
                #endregion

                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_id = SetOrderInfo.OrderId;
                inoutInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                inoutInfo.order_no = SetOrderInfo.OrderCode;
                inoutInfo.red_flag = "1";
                inoutInfo.warehouse_id = "67bb4c12785c42d4912aff7d34606592";
                inoutInfo.order_date = SetOrderInfo.OrderDate;
                inoutInfo.create_unit_id = SetOrderInfo.StoreId;
                inoutInfo.unit_id = SetOrderInfo.StoreId;
                inoutInfo.total_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutInfo.discount_rate = Convert.ToDecimal(SetOrderInfo.DiscountRate);
                inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutInfo.print_times = 0;
                inoutInfo.total_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                inoutInfo.vip_no = vipInfo.VIPID;
                inoutInfo.data_from_id = "wap";
                inoutInfo.sales_unit_id = SetOrderInfo.StoreId;
                inoutInfo.if_flag = "0";
                inoutInfo.customer_id = SetOrderInfo.CustomerId;
                inoutInfo.status = "10";
                inoutInfo.remark = SetOrderInfo.Remark; //支付信息
                inoutInfo.Field8 = SetOrderInfo.DeliveryId; //配送方式
                inoutInfo.send_time = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field9 = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field4 = SetOrderInfo.DeliveryAddress; //配送地址
                inoutInfo.Field6 = SetOrderInfo.Mobile;//手机
                inoutInfo.Field12 = SetOrderInfo.Email;//Email
                inoutInfo.Field13 = SetOrderInfo.OpenId;//OpenID
                inoutInfo.Field7 = SetOrderInfo.Status.ToString(); //电商订单状态
                inoutInfo.Field10 = SetOrderInfo.StatusDesc; //电商订单状态描述
                inoutInfo.data_from_id = "3";   //来源表SysVipSource ，是3 微信
                inoutInfo.Field14 = SetOrderInfo.username; //用户名
                inoutInfo.Field3 = SetOrderInfo.username; //用户名
                #endregion

                InoutDetailInfo inoutDetailInfo = new InoutDetailInfo();
                #region
                inoutDetailInfo.order_id = SetOrderInfo.OrderId;
                inoutDetailInfo.sku_id = SetOrderInfo.SkuId;
                inoutDetailInfo.unit_id = SetOrderInfo.StoreId;
                inoutDetailInfo.order_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                inoutDetailInfo.enter_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                inoutDetailInfo.std_price = Convert.ToDecimal(SetOrderInfo.StdPrice);
                inoutDetailInfo.enter_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutDetailInfo.enter_price = Convert.ToDecimal(SetOrderInfo.SalesPrice);
                inoutDetailInfo.retail_price = Convert.ToDecimal(SetOrderInfo.SalesPrice);
                inoutDetailInfo.retail_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutDetailInfo.discount_rate = Convert.ToDecimal(SetOrderInfo.DiscountRate);
                inoutDetailInfo.display_index = 1;
                inoutDetailInfo.if_flag = 0;
                #endregion

                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                inoutDetailInfoList.Add(inoutDetailInfo);
                inoutInfo.InoutDetailList = inoutDetailInfoList;

                bool bReturn = SetInoutInfo(inoutInfo, true, out strError);

                //strError = "提交订单成功.";
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 提交订单20130924 新版本
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <param name="strError"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool SetOrderOnlineShoppingNew(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo
                                            , out string strError
                                            , out string strMsg)
        {
            strMsg = "";
            try
            {
                #region 获取客户信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.VIPID = SetOrderInfo.CreateBy;
                vipInfo.WeiXinUserId = SetOrderInfo.OpenId;
                vipInfo = vipServer.GetByID(vipInfo.VIPID);
                string vipId = string.Empty;
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    //strError = "不存在对应的客户信息";
                    //return false;
                    vipId = SetOrderInfo.OpenId;
                }
                else
                {
                    vipId = vipInfo.VIPID;
                }
                #endregion

                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_id = SetOrderInfo.OrderId;
                inoutInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                inoutInfo.order_no = SetOrderInfo.OrderCode;
                inoutInfo.red_flag = "1";
                inoutInfo.warehouse_id = "67bb4c12785c42d4912aff7d34606592";
                inoutInfo.order_date = SetOrderInfo.OrderDate;
                inoutInfo.create_unit_id = SetOrderInfo.StoreId;
                inoutInfo.unit_id = SetOrderInfo.StoreId;
                inoutInfo.total_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                //inoutInfo.discount_rate = Convert.ToDecimal(SetOrderInfo.DiscountRate);
                //if (SetOrderInfo.ActualAmount == null || SetOrderInfo.ActualAmount.ToString().Equals("") || SetOrderInfo.ActualAmount < 1)
                //{
                inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                //}
                //else
                //{
                //    inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.ActualAmount);
                //    inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.ActualAmount);
                //}
                inoutInfo.print_times = 0;
                inoutInfo.total_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                inoutInfo.vip_no = vipId;
                //inoutInfo.data_from_id = "wap";
                inoutInfo.sales_unit_id = SetOrderInfo.StoreId;
                inoutInfo.if_flag = "0";
                inoutInfo.customer_id = SetOrderInfo.CustomerId;
                inoutInfo.status = "10";
                inoutInfo.remark = SetOrderInfo.Remark; //支付信息
                inoutInfo.Field8 = SetOrderInfo.DeliveryId; //配送方式
                inoutInfo.send_time = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field9 = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field4 = SetOrderInfo.DeliveryAddress; //配送地址
                inoutInfo.Field6 = SetOrderInfo.Mobile;//手机
                inoutInfo.Field12 = SetOrderInfo.Email;//Email
                inoutInfo.Field13 = SetOrderInfo.OpenId;//OpenID
                inoutInfo.Field7 = SetOrderInfo.Status.ToString(); //电商订单状态
                inoutInfo.Field10 = SetOrderInfo.StatusDesc; //电商订单状态描述
                inoutInfo.data_from_id = "3";   //来源表SysVipSource ，是3 微信
                inoutInfo.Field14 = SetOrderInfo.username; //用户名
                inoutInfo.Field3 = SetOrderInfo.username; //用户名
                inoutInfo.Field20 = SetOrderInfo.tableNumber; //桌号
                inoutInfo.Field16 = SetOrderInfo.CouponsPrompt;//Jermyn20131213
                if (SetOrderInfo.PurchaseUnitId != null)
                {
                    inoutInfo.purchase_unit_id = SetOrderInfo.PurchaseUnitId;
                }
                else
                {
                    if (SetOrderInfo.CustomerId.Equals("f6a7da3d28f74f2abedfc3ea0cf65c01"))
                    {
                        inoutInfo.purchase_unit_id = "8c41446fe80d4f2e9e3d659df01641fa";
                    }
                }
                #endregion

                #region 明细
                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                if (SetOrderInfo.OrderDetailInfoList != null && SetOrderInfo.OrderDetailInfoList.Count > 0)
                {
                    foreach (InoutDetailInfo detail in SetOrderInfo.OrderDetailInfoList)
                    {
                        detail.if_flag = 0;
                        inoutDetailInfoList.Add(detail);
                    }
                }
                //InoutDetailInfo inoutDetailInfo = new InoutDetailInfo();
                //#region
                //inoutDetailInfo.order_id = SetOrderInfo.OrderId;
                //inoutDetailInfo.sku_id = SetOrderInfo.SkuId;
                //inoutDetailInfo.unit_id = SetOrderInfo.StoreId;
                //inoutDetailInfo.order_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                //inoutDetailInfo.enter_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                //inoutDetailInfo.std_price = Convert.ToDecimal(SetOrderInfo.StdPrice);
                //inoutDetailInfo.enter_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                //inoutDetailInfo.enter_price = Convert.ToDecimal(SetOrderInfo.SalesPrice);
                //inoutDetailInfo.retail_price = Convert.ToDecimal(SetOrderInfo.SalesPrice);
                //inoutDetailInfo.retail_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                //inoutDetailInfo.discount_rate = Convert.ToDecimal(SetOrderInfo.DiscountRate);
                //inoutDetailInfo.display_index = 1;
                //inoutDetailInfo.if_flag = 0;
                //#endregion
                //inoutDetailInfoList.Add(inoutDetailInfo);
                inoutInfo.InoutDetailList = inoutDetailInfoList;
                #endregion

                bool bReturn = SetInoutInfo(inoutInfo, true, out strError);
                //Jermyn20131008 清楚购物车
                ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(this.loggingSessionInfo);
                // bReturn = shoppingCartServer.SetCancelShoppingCartByOrderId(inoutInfo.order_id, vipId);
                //strError = "提交订单成功.";
                #region Jermyn20131213 添加优惠券关系表
                //TOrderCouponMappingBLL mappingServer = new TOrderCouponMappingBLL(loggingSessionInfo);
                //mappingServer.DeleteOrderCouponMapping(SetOrderInfo.OrderId);
                //TOrderCouponMappingEntity mappingInfo = new TOrderCouponMappingEntity();
                //if (SetOrderInfo.CouponList != null && SetOrderInfo.CouponList.Count > 0)
                //{
                //    foreach (var info in SetOrderInfo.CouponList)
                //    {
                //        mappingServer.Create(info);
                //    }
                //}
                #endregion
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 提交订单20131225 新版本
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <param name="strError"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool SetOrderOnlineShoppingTwo(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo
                                            , out string strError
                                            , out string strMsg)
        {
            strMsg = "";
            try
            {
                #region 获取客户信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.VIPID = SetOrderInfo.CreateBy;
                vipInfo.WeiXinUserId = SetOrderInfo.OpenId;
                vipInfo = vipServer.GetByID(vipInfo.VIPID);
                string vipId = string.Empty;
                if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                {
                    vipId = SetOrderInfo.OpenId;
                }
                else
                {
                    vipId = vipInfo.VIPID;
                }
                #endregion

                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_id = SetOrderInfo.OrderId;
                inoutInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                inoutInfo.order_no = SetOrderInfo.OrderCode;
                inoutInfo.red_flag = "1";
                inoutInfo.warehouse_id = "67bb4c12785c42d4912aff7d34606592";
                inoutInfo.order_date = SetOrderInfo.OrderDate;
                inoutInfo.create_unit_id = SetOrderInfo.StoreId;
                inoutInfo.unit_id = SetOrderInfo.StoreId;
                inoutInfo.total_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                //inoutInfo.discount_rate = Convert.ToDecimal(SetOrderInfo.DiscountRate);
                if (SetOrderInfo.ActualAmount == null || SetOrderInfo.ActualAmount.ToString().Equals("") || SetOrderInfo.ActualAmount < 1)
                {
                    inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                    inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                }
                else
                {
                    inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.ActualAmount);
                    inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.ActualAmount);
                }
                inoutInfo.print_times = 0;
                inoutInfo.total_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                inoutInfo.vip_no = vipId;
                //inoutInfo.data_from_id = "wap";
                inoutInfo.sales_unit_id = SetOrderInfo.StoreId;
                inoutInfo.if_flag = "0";
                inoutInfo.customer_id = SetOrderInfo.CustomerId;
                inoutInfo.status = "10";
                inoutInfo.remark = SetOrderInfo.Remark; //支付信息
                inoutInfo.Field8 = SetOrderInfo.DeliveryId; //配送方式
                inoutInfo.send_time = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field9 = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field4 = SetOrderInfo.DeliveryAddress; //配送地址
                inoutInfo.Field6 = SetOrderInfo.Mobile;//手机
                inoutInfo.Field12 = SetOrderInfo.Email;//Email
                inoutInfo.Field13 = SetOrderInfo.OpenId;//OpenID
                inoutInfo.Field7 = SetOrderInfo.Status.ToString(); //电商订单状态
                inoutInfo.Field10 = SetOrderInfo.StatusDesc; //电商订单状态描述
                inoutInfo.data_from_id = "3";   //来源表SysVipSource ，是3 微信
                inoutInfo.Field14 = SetOrderInfo.username; //用户名
                inoutInfo.Field3 = SetOrderInfo.username; //用户名
                inoutInfo.Field20 = SetOrderInfo.tableNumber; //桌号
                inoutInfo.Field16 = SetOrderInfo.CouponsPrompt;//Jermyn20131213
                inoutInfo.status = SetOrderInfo.Status;
                if (SetOrderInfo.PurchaseUnitId != null)
                {
                    inoutInfo.purchase_unit_id = SetOrderInfo.PurchaseUnitId;
                }
                else
                {
                    if (SetOrderInfo.CustomerId.Equals("f6a7da3d28f74f2abedfc3ea0cf65c01"))
                    {
                        inoutInfo.purchase_unit_id = "8c41446fe80d4f2e9e3d659df01641fa";
                    }
                }
                #endregion

                #region 明细
                IList<InoutDetailInfo> inoutDetailInfoList = new List<InoutDetailInfo>();
                if (SetOrderInfo.OrderDetailInfoList != null && SetOrderInfo.OrderDetailInfoList.Count > 0)
                {
                    foreach (InoutDetailInfo detail in SetOrderInfo.OrderDetailInfoList)
                    {
                        detail.if_flag = 0;
                        inoutDetailInfoList.Add(detail);
                    }
                }
                inoutInfo.InoutDetailList = inoutDetailInfoList;
                #endregion

                bool bReturn = SetInoutInfo(inoutInfo, true, out strError);
                //Jermyn20131008 清楚购物车
                ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(this.loggingSessionInfo);
                // bReturn = shoppingCartServer.SetCancelShoppingCartByOrderId(inoutInfo.order_id, vipId);
                //strError = "提交订单成功.";
                #region Jermyn20131213 添加优惠券关系表
                //TOrderCouponMappingBLL mappingServer = new TOrderCouponMappingBLL(loggingSessionInfo);
                //mappingServer.DeleteOrderCouponMapping(SetOrderInfo.OrderId);
                //TOrderCouponMappingEntity mappingInfo = new TOrderCouponMappingEntity();
                //if (SetOrderInfo.CouponList != null && SetOrderInfo.CouponList.Count > 0)
                //{
                //    foreach (var info in SetOrderInfo.CouponList)
                //    {
                //        mappingServer.Create(info);
                //    }
                //}
                #endregion
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion


        #region 提交订单支付
        /// <summary>
        /// 提交订单支付
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <returns></returns>
        public bool SetOrderPayment(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo, out string strError)
        {
            try
            {
                inoutService.SetOrderPayment(SetOrderInfo);
                string strOrderId = string.Empty;


                strError = "支付成功.";
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 获取电商订单信息
        /// <summary>
        /// 获取电商订单详细信息
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public JIT.CPOS.BS.Entity.Interface.SetOrderEntity GetOrderOnline(string OrderId)
        {
            JIT.CPOS.BS.Entity.Interface.SetOrderEntity orderInfo = new JIT.CPOS.BS.Entity.Interface.SetOrderEntity();
            DataSet ds = new DataSet();
            ds = inoutService.GetOrderOnline(OrderId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                orderInfo = DataTableToObject.ConvertToObject<JIT.CPOS.BS.Entity.Interface.SetOrderEntity>(ds.Tables[0].Rows[0]);
            }
            return orderInfo;
        }
        #endregion

        public bool GetOrderOpenId(string order_code, out string openId, out string amount, out string orderId)
        {
            DataSet ds = new DataSet();
            ds = inoutService.GetOrderOpenId(order_code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                openId = ds.Tables[0].Rows[0]["openId"].ToString();
                amount = ds.Tables[0].Rows[0]["amount"].ToString();
                orderId = ds.Tables[0].Rows[0]["order_id"].ToString();
                return true;
            }
            else
            {
                openId = "";
                amount = "";
                orderId = "";
                return false;
            }
        }
        #endregion

        #region 修改订单配送状态
        /// <summary>
        /// 根据订单ID修改订单配送状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">状态：1未付款/2待处理/3已发货/0已取消</param>
        /// <param name="send_time">发货时间</param>
        public bool UpdateOrderDeliveryStatus(string orderId, string status, string send_time)
        {
            try
            {
                var statusDesc = string.Empty;
                switch (status)
                {
                    case "1": statusDesc = "未审核"; break;
                    case "2": statusDesc = "未付款"; break;
                    case "3": statusDesc = "未发货"; break;
                    case "4": statusDesc = "已发货"; break;
                    case "5": statusDesc = "已完成"; break;
                    case "6": statusDesc = "已取消"; break;
                }

                inoutService.UpdateOrderDeliveryStatus(orderId, status, statusDesc, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 修改订单配送信息及其用户信息
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <param name="strError"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool SetUpdateOrderDelivert(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo
                                            , out string strError
                                            , out string strMsg)
        {
            strMsg = "";
            try
            {
                //#region 获取客户信息
                //VipBLL vipServer = new VipBLL(loggingSessionInfo);
                //VipEntity vipInfo = new VipEntity();
                //vipInfo.VIPID = SetOrderInfo.CreateBy;
                //vipInfo.WeiXinUserId = SetOrderInfo.OpenId;
                //vipInfo = vipServer.GetByID(vipInfo.VIPID);
                //if (vipInfo == null || vipInfo.VIPID == null || vipInfo.VIPID.Equals(""))
                //{
                //    strError = "不存在对应的客户信息";
                //    return false;
                //}
                //#endregion

                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_id = SetOrderInfo.OrderId;
                inoutInfo.if_flag = "0";
                inoutInfo.remark = SetOrderInfo.Remark; //支付信息
                inoutInfo.Field8 = SetOrderInfo.DeliveryId; //配送方式
                inoutInfo.send_time = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field9 = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field4 = SetOrderInfo.DeliveryAddress; //配送地址
                inoutInfo.Field6 = SetOrderInfo.Mobile;//手机
                inoutInfo.Field12 = SetOrderInfo.Email;//Email
                inoutInfo.Field14 = SetOrderInfo.username; //用户名
                inoutInfo.Field3 = SetOrderInfo.username; //用户名
                inoutInfo.Field16 = SetOrderInfo.CouponsPrompt;
                inoutInfo.Field19 = SetOrderInfo.Invoice; // 发票号码
                if (SetOrderInfo.ActualAmount != null)
                {
                    inoutInfo.actual_amount = SetOrderInfo.ActualAmount;
                    inoutInfo.total_retail = SetOrderInfo.ActualAmount;
                }
                inoutInfo.modify_time = new BaseService().GetCurrentDateTime();
                if (SetOrderInfo.StoreId == null || SetOrderInfo.StoreId.Equals(""))
                {
                    if (this.loggingSessionInfo.CurrentUser.customer_id.Equals(""))
                    {
                        var tmpUnitId = GetUnitIdByVipId(SetOrderInfo.LastUpdateBy);
                        if (tmpUnitId != null && tmpUnitId.Length > 0)
                        {
                            inoutInfo.purchase_unit_id = tmpUnitId;
                        }
                        else
                        {
                            inoutInfo.purchase_unit_id = "8c41446fe80d4f2e9e3d659df01641fa";
                        }
                    }
                }
                else
                {
                    inoutInfo.purchase_unit_id = SetOrderInfo.StoreId; //到店提货的店标识
                }
                #endregion

                bool bReturn = Update(inoutInfo, out strError);

                //支付成功，发送邮件与短信
                cUserService userServer = new cUserService(loggingSessionInfo);
                userServer.SendOrderMessage(inoutInfo.order_id);

                //strError = "提交订单成功.";
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 修改订单配送
        /// <summary>
        /// 根据订单ID修改订单配送
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="carrier_id">配送商</param>
        /// <param name="Field2">配送单号</param>
        public void UpdateOrderDelivery(string orderId, string carrier_id, string Field2)
        {
            inoutService.UpdateOrderDelivery(orderId, carrier_id, Field2);
        }
        #endregion

        #region 餐饮 20131010
        #region 获取桌号信息
        /// <summary>
        /// 获取桌号信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataSet GetTableNumberList(string customerId, string unitId)
        {
            return inoutService.GetTableNumberList(customerId, unitId);
        }
        #endregion
        #endregion

        #region
        public bool Update(InoutInfo inoutInfo, out string strError)
        {
            try
            {
                strError = "更新订单信息成功.";
                return inoutService.Update(inoutInfo);
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region GetUnitIdByVipId
        public string GetUnitIdByVipId(string vipId)
        {
            return inoutService.GetUnitIdByVipId(vipId);
        }
        #endregion

        #region 统计用户信息
        /// <summary>
        /// 统计登录用户的信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetVipSummerInfo(string orderid)
        {
            var dict = new Dictionary<string, string>();
            var ds = inoutService.GetVipSummerInfo(orderid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                dict.Add("CouponCount", ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                dict.Add("CouponCount", "0");
            }
            if (ds != null && ds.Tables.Count > 1)
            {
                var values = new List<string>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (ds.Tables[1].Rows[i][0] != DBNull.Value)
                    {
                        values.Add(ds.Tables[1].Rows[i][0].ToString());
                    }
                }
                dict.Add("Tags", string.Join(",", values));
            }
            else
            {
                dict.Add("Tags", string.Empty);
            }
            if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0 && ds.Tables[2].Rows[0][0] != DBNull.Value)
            {
                dict.Add("TotalSum", ds.Tables[2].Rows[0][0].ToString());
            }
            else
            {
                dict.Add("TotalSum", "0");
            }
            if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
            {
                var dt = ds.Tables[3];
                if (dt.Rows[0]["VipCode"] != null && dt.Rows[0]["VipCode"] != DBNull.Value)
                {
                    dict.Add("VipCode", dt.Rows[0]["VipCode"].ToString());
                }
                if (dt.Rows[0]["VipName"] != null && dt.Rows[0]["VipName"] != DBNull.Value)
                {
                    dict.Add("VipName", dt.Rows[0]["VipName"].ToString());
                }
                //if (dt.Rows[0]["VipName"] != null && dt.Rows[0]["VipName"] != DBNull.Value)
                //{
                //    dict.Add("VipName", dt.Rows[0]["VipName"].ToString());
                //}
                if (dt.Rows[0]["TencentMBlog"] != null && dt.Rows[0]["TencentMBlog"] != DBNull.Value)
                {
                    dict.Add("TencentMBlog", dt.Rows[0]["TencentMBlog"].ToString());
                }
                if (dt.Rows[0]["SinaMBlog"] != null && dt.Rows[0]["SinaMBlog"] != DBNull.Value)
                {
                    dict.Add("SinaMBlog", dt.Rows[0]["SinaMBlog"].ToString());
                }
                if (dt.Rows[0]["Integration"] != null && dt.Rows[0]["Integration"] != DBNull.Value)
                {
                    dict.Add("Integration", dt.Rows[0]["Integration"].ToString());
                }
            }
            else
            {
                dict.Add("VipCode", string.Empty);
                dict.Add("VipName", string.Empty);
                dict.Add("TencentMBlog", string.Empty);
                dict.Add("SinaMBlog", string.Empty);
                dict.Add("Integration", "0");
            }
            return dict;
        }

        #endregion

        #region 保存配送信息

        public void SaveDeliveryInfo(Dictionary<string, string> dict, string order_id)
        {
            new DataAccess.Inout3Service(loggingSessionInfo).SaveDeliveryInfo(dict, order_id);
        }

        #endregion

        #region 保存付款方式

        public void SaveDefrayType(string defrayType, string order_id)
        {
            new DataAccess.Inout3Service(loggingSessionInfo).SaveDefrayType(defrayType, order_id);
        }

        #endregion
    }
}

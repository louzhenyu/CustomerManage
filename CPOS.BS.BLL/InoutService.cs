using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Transactions;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.BLL.CS;
using JIT.Utility.Log;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity.WX;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.Common;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 进出库单据服务
    /// </summary>
    public class InoutService : BaseService
    {
        JIT.CPOS.BS.DataAccess.InoutService inoutService = null;
        #region 构造函数
        public InoutService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            inoutService = new DataAccess.InoutService(loggingSessionInfo);
        }
        #endregion


        /// <summary>
        /// 设置货到付款的支付方式
        /// </summary>
        /// <returns></returns>
        public bool SetGetToPay(string pOrderId)
        {
            bool bReturn = inoutService.SetGetToPay(pOrderId);
            return bReturn;
        }

        public SqlTransaction GetTran()
        {
            return this.inoutService.GetTran();
        }

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
        /// <param name="isHotel">花间堂定制酒店参数，如非花间堂则输入false</param>
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
            string path_unit_id, string timestamp, bool isHotel)
        {
            InoutInfo inoutInfo = new InoutInfo();
            OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
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

            inoutInfo = SearchInoutInfo(orderSearchInfo, isHotel);
            return inoutInfo;
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
                inoutInfo.status = "100";
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
        public InoutInfo SearchInoutInfo(OrderSearchInfo orderSearchInfo, bool isHotel)
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
                                case "1":
                                    inoutInfo.StatusCount1 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "2":
                                    inoutInfo.StatusCount2 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "3":
                                    inoutInfo.StatusCount3 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "0":
                                    inoutInfo.StatusCount4 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "100":
                                    inoutInfo.StatusCount5 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "200":
                                    inoutInfo.StatusCount6 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "300":
                                    inoutInfo.StatusCount7 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "400":
                                    inoutInfo.StatusCount8 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "500":
                                    inoutInfo.StatusCount9 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "600":
                                    inoutInfo.StatusCount10 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "700":
                                    inoutInfo.StatusCount11 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "800":
                                    inoutInfo.StatusCount12 = Convert.ToInt32(dr["StatusCount"].ToString());
                                    break;
                                case "900":
                                    inoutInfo.StatusCount13 = Convert.ToInt32(dr["StatusCount"].ToString());
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
                ds = inoutService.SearchInoutInfo(orderSearchInfo, isHotel);
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

        #region 单个单据详细信息
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

        public DataSet GetOrderDetailByOrderId(string orderId)
        {
            IList<InoutDetailInfo> inoutDetailList = new List<InoutDetailInfo>();
            DataSet ds = inoutService.GetInoutDetailInfoByOrderId(orderId);
            return ds;
        }
        #endregion

        public DataSet GetInoutDetailGgByOrderId(string orderId)
        {
            return inoutService.GetInoutDetailGgByOrderId(orderId);
        }

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
                inoutInfo.data_from_id = SetOrderInfo.DataFromId.ToString();
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
                    vipId = SetOrderInfo.CreateBy;
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
                inoutInfo.discount_rate = Convert.ToDecimal("100");
                //}
                //else
                //{
                //    inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.ActualAmount);
                //    inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.ActualAmount);
                //}
                inoutInfo.print_times = 0;
                inoutInfo.carrier_id = SetOrderInfo.CarrierID;//提交上来的StoreID是到店提货的门店ID
                inoutInfo.total_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);
                inoutInfo.vip_no = vipId;
                inoutInfo.vip_code = SetOrderInfo.VipCardCode;
                //inoutInfo.data_from_id = "wap";
                inoutInfo.sales_unit_id = SetOrderInfo.StoreId;
                inoutInfo.purchase_unit_id = SetOrderInfo.StoreId;
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
                inoutInfo.data_from_id = SetOrderInfo.DataFromId == 0 ? "3" : SetOrderInfo.DataFromId.ToString();   //来源表SysVipSource ，是3 微信
                inoutInfo.Field14 = SetOrderInfo.username; //用户名
                inoutInfo.Field3 = SetOrderInfo.IsALD; //是否同步到阿拉丁
                inoutInfo.Field20 = SetOrderInfo.tableNumber; //桌号
                inoutInfo.Field16 = SetOrderInfo.CouponsPrompt;//Jermyn20131213
                inoutInfo.print_times = SetOrderInfo.JoinNo;
                inoutInfo.Field1 = "0";     //Jermyn20140314 配置是否支付
                inoutInfo.Field15 = SetOrderInfo.IsGroupBuy;
                inoutInfo.sales_user = SetOrderInfo.SalesUser; //销售ID add by donal 2014-9-26 14:39:45
                inoutInfo.ChannelId = SetOrderInfo.ChannelId; // 渠道ID add by donal 2014-9-28 14:39:45
                inoutInfo.ReturnCash = SetOrderInfo.ReturnCash;//佣金 add by donal 2014-12-9 10:46:33

                if (SetOrderInfo.PurchaseUnitId != null)
                {
                    inoutInfo.purchase_unit_id = SetOrderInfo.PurchaseUnitId;
                }
                //else
                //{
                //    if (SetOrderInfo.CustomerId.Equals("f6a7da3d28f74f2abedfc3ea0cf65c01"))
                //    {
                //        inoutInfo.purchase_unit_id = "8c41446fe80d4f2e9e3d659df01641fa";
                //    }
                //}
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
                List<string> skuids = new List<string> { };
                if (inoutDetailInfoList.Count > 0)
                {
                    foreach (var item in inoutDetailInfoList)
                    {
                        skuids.Add(item.sku_id);
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
                //Jermyn20131008 清除购物车
                if (SetOrderInfo.Status != "-99")
                {
                    ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(this.loggingSessionInfo);
                    bReturn = shoppingCartServer.SetCancelShoppingCartByOrderId(inoutInfo.order_id, vipId, skuids.ToArray());
                }
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

                Loggers.Debug(new DebugLogInfo() { Message = "下单成功，推送消息;bReturn=" + bReturn.ToString() + ", inoutInfo.data_from_id=" + inoutInfo.data_from_id });

                //Updated By Willie Yan on 2014-05-06   门店PAD下单成功后推送微信消息
                if (bReturn && inoutInfo.data_from_id == "2")
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["OrderDetailURL"];
                    string message = "您刚刚选购了商品：#List#，共#Quantity#件，总计#Amount#元，使用0点积分抵用0元，应付款#Amount#元。<a href='#OrderDetailURL#'>点此查看订单详情。</a>";
                    string list = string.Join("、", (from d in inoutDetailInfoList
                                                    select d.item_name + "x" + d.order_qty).ToList().ToArray());

                    string orderDetailURL = System.Web.HttpContext.Current.Server.UrlDecode(string.Format(url, vipInfo.ClientID, SetOrderInfo.OrderId, DateTime.Now.Ticks));

                    message = message.Replace("#List#", list);
                    message = message.Replace("#Quantity#", inoutInfo.total_qty.ToString());
                    message = message.Replace("#Amount#", inoutInfo.total_retail.ToString());
                    message = message.Replace("#OrderDetailURL#", orderDetailURL);

                    string code = JIT.CPOS.BS.BLL.WX.CommonBLL.SendWeixinMessage(message, "1", loggingSessionInfo, vipInfo);
                    Loggers.Debug(new DebugLogInfo() { Message = "消息推送完成，code=" + code + ", message=" + message });
                    switch (code)
                    {
                        case "103":
                            Loggers.Debug(new DebugLogInfo() { Message = vipInfo.VipName + "未查询到匹配的公众账号信息;" });
                            break;
                        case "203":
                            Loggers.Debug(new DebugLogInfo() { Message = vipInfo.VipName + "发送失败;" });
                            break;
                        default:
                            break;
                    }
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

        #region 修改订单20140312 新版本
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <param name="strError"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool SetOrderUpdate(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo
                                            , out string strError
                                            , out string strMsg)
        {
            strMsg = "";
            try
            {
                InoutInfo inoutInfo = new InoutInfo();
                #region 定义订单主表
                inoutInfo.order_id = SetOrderInfo.OrderId;
                //inoutInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                //inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                //inoutInfo.order_no = SetOrderInfo.OrderCode;
                //inoutInfo.red_flag = "1";
                //inoutInfo.warehouse_id = "67bb4c12785c42d4912aff7d34606592";
                //inoutInfo.order_date = SetOrderInfo.OrderDate;
                //inoutInfo.create_unit_id = SetOrderInfo.StoreId;
                //inoutInfo.unit_id = SetOrderInfo.StoreId;
                inoutInfo.total_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);

                inoutInfo.actual_amount = Convert.ToDecimal(SetOrderInfo.TotalAmount);
                inoutInfo.total_retail = Convert.ToDecimal(SetOrderInfo.TotalAmount);

                inoutInfo.print_times = 0;
                inoutInfo.total_qty = Convert.ToDecimal(SetOrderInfo.TotalQty);

                //inoutInfo.sales_unit_id = SetOrderInfo.StoreId;
                inoutInfo.if_flag = "0";
                inoutInfo.customer_id = SetOrderInfo.CustomerId;
                //inoutInfo.status = "10";
                inoutInfo.remark = SetOrderInfo.Remark; //支付信息
                inoutInfo.Field8 = SetOrderInfo.DeliveryId; //配送方式
                inoutInfo.send_time = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field9 = SetOrderInfo.DeliveryTime;   //配送时间
                inoutInfo.Field4 = SetOrderInfo.DeliveryAddress; //配送地址
                inoutInfo.Field6 = SetOrderInfo.Mobile;//手机
                inoutInfo.Field12 = SetOrderInfo.Email;//Email
                inoutInfo.Field13 = SetOrderInfo.OpenId;//OpenID
                inoutInfo.Field7 = SetOrderInfo.Status; //电商订单状态
                inoutInfo.Field10 = SetOrderInfo.StatusDesc; //电商订单状态描述
                //inoutInfo.data_from_id = "3";   //来源表SysVipSource ，是3 微信
                inoutInfo.Field14 = SetOrderInfo.username; //用户名
                inoutInfo.Field3 = SetOrderInfo.username; //用户名
                inoutInfo.Field20 = SetOrderInfo.tableNumber; //桌号
                inoutInfo.Field16 = SetOrderInfo.CouponsPrompt;//Jermyn20131213
                inoutInfo.print_times = SetOrderInfo.JoinNo;

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
                List<string> skuids = new List<string> { };
                if (inoutDetailInfoList.Count > 0)
                {
                    foreach (var item in inoutDetailInfoList)
                    {
                        skuids.Add(item.sku_id);
                    }
                }

                inoutInfo.InoutDetailList = inoutDetailInfoList;
                #endregion
                bool bReturn1 = Update(inoutInfo, out strError);
                bReturn1 = inoutService.UpdateInoutDetail(inoutInfo, out strError);

                //strError = "提交订单成功.";

                return bReturn1;
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
                inoutInfo.status = SetOrderInfo.Status;
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
                inoutInfo.Field3 = SetOrderInfo.IsALD; //用户名
                inoutInfo.Field20 = SetOrderInfo.tableNumber; //桌号
                inoutInfo.Field16 = SetOrderInfo.CouponsPrompt;//Jermyn20131213
                inoutInfo.status = SetOrderInfo.Status;
                inoutInfo.Field1 = "0";
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

                List<string> skuids = new List<string> { };
                if (inoutDetailInfoList.Count > 0)
                {
                    foreach (var item in inoutDetailInfoList)
                    {
                        skuids.Add(item.sku_id);
                    }
                }

                bool bReturn = SetInoutInfo(inoutInfo, true, out strError);
                //Jermyn20131008 清楚购物车
                if (SetOrderInfo.Status != "-99")
                {
                    ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(this.loggingSessionInfo);
                    bReturn = shoppingCartServer.SetCancelShoppingCartByOrderId(inoutInfo.order_id, vipId, skuids.ToArray());
                }
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
        /// <param name="tableNo">桌号</param>
        public bool UpdateOrderDeliveryStatus(string orderId, string status, string send_time, string tableNo = null, SqlTransaction tran = null)
        {
            try
            {
                var statusDesc = string.Empty;
                switch (status)
                {
                    case "0": statusDesc = "已取消"; break;
                    case "1": statusDesc = "未付款"; break;
                    case "2": statusDesc = "待处理"; break;
                    case "3": statusDesc = "已发货"; break;
                }
                if (statusDesc == null || statusDesc.Equals("") || statusDesc.Length == 0)
                {
                    OptionsBLL optionsBll = new OptionsBLL(this.loggingSessionInfo);
                    var optionsList = optionsBll.QueryByEntity(new OptionsEntity
                    {
                        OptionValue = Convert.ToInt32(status)
                        ,
                        IsDelete = 0
                        ,
                        OptionName = "TInOutStatus"
                        ,
                        CustomerID = this.loggingSessionInfo.CurrentUser.customer_id
                    }, null);
                    if (optionsList != null && optionsList.Length > 0)
                    {
                        statusDesc = optionsList[0].OptionText;
                    }
                }

                T_InoutBLL inoutBLL = new T_InoutBLL(this.loggingSessionInfo);
                if (status == "700")
                {
                    var orderInfo = inoutBLL.GetInoutInfo(orderId, this.loggingSessionInfo);
                    if (orderInfo != null && orderInfo.status != "700")//完成订单时处理积分、返现、佣金[和确认收货一致]
                        new VipIntegralBLL(this.loggingSessionInfo).OrderReward(orderInfo, tran);
                }
                inoutService.UpdateOrderDeliveryStatus(orderId, status, statusDesc, null, null, tableNo);
                if (status == "600")
                {
                    //判断是否是微信支付
                    var payId = inoutBLL.GetPayTypeByOrderId(orderId);
                    Loggers.Debug(new DebugLogInfo() { Message = "付款方式:" + payId });
                    if (payId == "DFD3E26D5C784BBC86B075090617F44B")
                    {
                        var deliverMsg = inoutBLL.GetDeliverInfoByOrderId(orderId, this.loggingSessionInfo);
                        Loggers.Debug(new DebugLogInfo() { Message = "微信发货通知接口返回值:" + deliverMsg });
                    }
                }

                //订单消息推送 update by Henry 2015-4-15 洗e客-商城不用推送商品订单
                //OrderPushMessage(orderId, status);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据订单状态，做出不同的推送消息
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="pOrderStatus">状态</param>
        public void OrderPushMessage(string orderId, string pOrderStatus)
        {
            StringBuilder StringBuilderLog = new StringBuilder();
            Loggers.Debug(new DebugLogInfo() { Message = "推送开始......" });
            IList<TOrderPushStrategyEntity> _TOrderPushStrategyEntityList = new TOrderPushStrategyBLL(loggingSessionInfo).GetTOrderPushStrategyEntityList(loggingSessionInfo.CurrentLoggingManager.Customer_Id, pOrderStatus);

            var inoutEntity = this.GetInoutInfoById(orderId);
            string CustomerName = inoutService.GetCustomerName(inoutEntity.customer_id);

            #region 生成消息

            StringBuilder strItemNameList = new StringBuilder();
            foreach (var item in inoutEntity.InoutDetailList)
            {
                strItemNameList.AppendFormat("{0},", item.item_name);
            }
            if (strItemNameList.Length > 0)
            {
                strItemNameList.Remove(strItemNameList.Length - 1, 1);
            }

            #endregion

            #region 按照策略,依次发送消息

            for (int i = 0; _TOrderPushStrategyEntityList != null && i < _TOrderPushStrategyEntityList.Count; i++)
            {
                TOrderPushStrategyEntity _TOrderPushStrategyEntity = _TOrderPushStrategyEntityList[i];

                string msg = _TOrderPushStrategyEntity.PushInfo
                    .Replace("#VipName#", inoutEntity.vip_name)//会员名称
                    .Replace("#OrderNo#", inoutEntity.order_no)//订单号码
                    .Replace("#WaybillNo#", inoutEntity.Field2)//运单号码
                    .Replace("#ItemName#", strItemNameList.ToString())
                    .Replace("#CustomerName#", CustomerName)//商品名称
                    .Replace("#DeliveryTime#", inoutEntity.Field9)	  //配送时间
                      .Replace("#carrier_name#", inoutEntity.carrier_name);	 //配送公司  

                #region 向IOS推送消息

                if (_TOrderPushStrategyEntity.IsIOSPush == 1)
                {
                    IPushMessage pushIOSMessage = new PushIOSMessage(loggingSessionInfo);
                    if (_TOrderPushStrategyEntity.PushObject == 1)//订单购买者
                    {
                        pushIOSMessage.PushMessage(inoutEntity.vip_no, msg);

                    }

                    if (_TOrderPushStrategyEntity.PushObject == 2)//客服
                    {
                        PushToUserList(pushIOSMessage, msg);
                    }

                    #region 记录日志
                    PushIOSMessageEntity _PushIOSMessageEntity = new PushIOSMessageEntity();
                    _PushIOSMessageEntity.MessageText = StringBuilderLog.ToString();
                    _PushIOSMessageEntity.CreateTime = DateTime.Now;
                    _PushIOSMessageEntity.DeviceToken = "all";
                    _PushIOSMessageEntity.IOSMessageID = NewGuid();

                    new PushIOSMessageBLL(this.loggingSessionInfo).Create(_PushIOSMessageEntity);

                    //表 PushIOSMessage
                    //Name	Code	Data Type	Length	Precision	Primary	Foreign Key	Mandatory
                    //主标识	IOSMessageID	nvarchar(50)	50		TRUE	FALSE	TRUE
                    //DeviceToken	DeviceToken	nvarchar(200)	200		FALSE	FALSE	TRUE
                    //留言方	UserID	nvarchar(50)	50		FALSE	FALSE	FALSE
                    //接受方	ConnUserID	nvarchar(50)	50		FALSE	FALSE	FALSE
                    //类型	ItemType	int			FALSE	FALSE	FALSE
                    //对象标识	ItemID	nvarchar(50)	50		FALSE	FALSE	FALSE
                    //消���内���	MessageText	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //发送次数	SendCount	int			FALSE	FALSE	FALSE
                    //���态	Status	int			FALSE	FALSE	FALSE
                    //失败原因	FailureReason	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //CreateTime	CreateTime	datetime			FALSE	FALSE	FALSE
                    //CreateBy	CreateBy	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //LastUpdateBy	LastUpdateBy	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //LastUpdateTime	LastUpdateTime	datetime			FALSE	FALSE	FALSE
                    //是���删除	IsDelete	int			FALSE	FALSE	TRUE
                    //推送内容类型	推送内容类型	int			FALSE	FALSE	FALSE
                    //版本1是否发送成功	IsVersion1	int			FALSE	FALSE	FALSE
                    //版本2是否发生成功	IsVersion2	int			FALSE	FALSE	FALSE
                    //客户标识	CustomerId	nvarchar(200)	200		FALSE	FALSE	FALSE


                    #endregion
                }

                #endregion

                #region 向Android推送消息

                if (_TOrderPushStrategyEntity.IsAndroidPush == 1)
                {
                    IPushMessage pushAndroidMessage = new PushAndroidMessage(loggingSessionInfo);
                    if (_TOrderPushStrategyEntity.PushObject == 1)//订单购买者
                    {
                        pushAndroidMessage.PushMessage(inoutEntity.vip_no, msg);
                    }

                    if (_TOrderPushStrategyEntity.PushObject == 2)//客服
                    {
                        PushToUserList(pushAndroidMessage, msg);
                    }

                    #region 记录日志
                    PushAndroidMessageEntity _PushAndroidMessageEntity = new PushAndroidMessageEntity();
                    _PushAndroidMessageEntity.Message = StringBuilderLog.ToString();
                    _PushAndroidMessageEntity.CreateTime = DateTime.Now;
                    _PushAndroidMessageEntity.CustomerId = loggingSessionInfo.UserID;
                    _PushAndroidMessageEntity.AndroidMessageID = NewGuid();
                    _PushAndroidMessageEntity.UserID = "all";
                    _PushAndroidMessageEntity.ConnUserID = "all";
                    _PushAndroidMessageEntity.UserIDBaiDu = "16";
                    _PushAndroidMessageEntity.MessageType = 1;
                    _PushAndroidMessageEntity.MessagePushType = 1;

                    new PushAndroidMessageBLL(this.loggingSessionInfo).Create(_PushAndroidMessageEntity);
                    //Name	Code	Data Type	Length	Precision	Primary	Foreign Key	Mandatory
                    //主标识	AndroidMessageID	nvarchar(50)	50		TRUE	FALSE	TRUE
                    //发送用���标识	UserID	nvarchar(50)	50		FALSE	FALSE	TRUE
                    //接收���户标识	ConnUserID	nvarchar(50)	50		FALSE	FALSE	TRUE
                    //ChannelIDBaiDu	ChannelIDBaiDu	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //百度用户标识	UserIDBaiDu	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //推送类����	PushType	int			FALSE	FALSE	FALSE
                    //设��类型	DeviceType	int			FALSE	FALSE	FALSE
                    //消息内容	Message	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //消息Key	MessageKey	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //过期时间	MessageExpires	int			FALSE	FALSE	FALSE
                    //消息标���	TagName	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //对象类型	ItemType	int			FALSE	FALSE	FALSE
                    //对象标识	ItemID	nvarchar(50)	50		FALSE	FALSE	FALSE
                    //发送次数	SendCount	int			FALSE	FALSE	FALSE
                    //应用系统标识	BaiduPushAppID	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //状态	Status	int			FALSE	FALSE	FALSE
                    //失败原因	FailureReason	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //CreateTime	CreateTime	datetime			FALSE	FALSE	FALSE
                    //CreateBy	CreateBy	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //LastUpdateBy	LastUpdateBy	nvarchar(200)	200		FALSE	FALSE	FALSE
                    //LastUpdateTime	LastUpdateTime	datetime			FALSE	FALSE	FALSE
                    //是否删除	IsDelete	int			FALSE	FALSE	TRUE
                    //消��类型	MessageType	int			FALSE	FALSE	FALSE
                    //推送内容类��	MessagePushType	int			FALSE	FALSE	FALSE
                    //客户标识	CustomerId	nvarchar(200)	200		FALSE	FALSE	FALSE

                    #endregion

                }

                #endregion

                #region 推送消息给微信用户

                var appList = new WApplicationInterfaceBLL(loggingSessionInfo).
                        QueryByEntity(new WApplicationInterfaceEntity { CustomerId = loggingSessionInfo.ClientID }, null);

                VipEntity vipEntity = new VipBLL(loggingSessionInfo).GetByID(inoutEntity.vip_no);

                if (appList != null && appList.Length > 0)
                {
                    if (vipEntity != null && !string.IsNullOrEmpty(vipEntity.WeiXinUserId))
                    {
                        CommonBLL commonBll = new CommonBLL();
                        if (_TOrderPushStrategyEntity.IsWXPush == 1)
                        {
                            SendMessageEntity messageEntity = new SendMessageEntity();
                            messageEntity.content = msg;
                            messageEntity.touser = vipEntity.WeiXinUserId;
                            messageEntity.msgtype = "text";
                            commonBll.SendMessage(messageEntity, appList.FirstOrDefault().AppID,
                                appList.FirstOrDefault().AppSecret, loggingSessionInfo);


                            #region 记录日志
                            WXSalesPushLogEntity _WXSalesPushLogEntity = new WXSalesPushLogEntity();
                            _WXSalesPushLogEntity.PushInfo = StringBuilderLog.ToString();
                            _WXSalesPushLogEntity.CreateTime = DateTime.Now;
                            _WXSalesPushLogEntity.WinXin = vipEntity.WeiXin;

                            new WXSalesPushLogBLL(this.loggingSessionInfo).Create(_WXSalesPushLogEntity);

                            #endregion
                        }


                    }
                }

                #endregion

                #region 推送消息到邮件
                bool mailOK = false;
                if (_TOrderPushStrategyEntity.IsEmailPush == 1)
                {
                    if (_TOrderPushStrategyEntity.PushObject == 1)//购买者
                    {
                        StringBuilderLog.AppendLine("向购买者推送邮件:");

                        string mailsubject = "您有新的消息";
                        string mailBody = msg;

                        if (vipEntity != null && !string.IsNullOrEmpty(vipEntity.Email))
                        {
                            mailOK = JIT.Utility.Notification.Mail.SendMail(
                            vipEntity.Email,
                          mailsubject,
                          mailBody);

                            StringBuilderLog.AppendFormat(
                                "order_id:{0},create_user_id{1},email:{2}邮件内容:{3},返回结果:{4}",
                                 inoutEntity.order_id,
                                 inoutEntity.create_user_id,
                                 vipEntity.Email,
                                 msg,
                                 mailOK.ToString()
                                );
                        }


                    }
                    if (_TOrderPushStrategyEntity.PushObject == 2)//客服
                    {
                        mailOK = PushMailToUserList(msg, StringBuilderLog);
                    }


                    if (_TOrderPushStrategyEntity.PushObject == 3)//从mailToList获取邮件列表
                    {
                        string mailsubject = "您有新的邮件";

                        string strMailKey = "mailToList";
                        if (ConfigurationManager.AppSettings.GetValues(strMailKey).Length > 0)
                        {
                            string strMails = ConfigurationManager.AppSettings[strMailKey];
                            string[] mails = strMails.Split(',');
                            foreach (string mail in mails)
                            {
                                mailOK = JIT.Utility.Notification.Mail.SendMail(
                                    mail,
                                    mailsubject,
                                    msg);

                                StringBuilderLog.AppendFormat(
                                    "order_id:{0},create_user_id{1},邮箱地址:{2},邮件内容:{3},返回结果:{4}",
                                     inoutEntity.order_id,
                                     inoutEntity.create_user_id,
                                     mail,
                                     msg,
                                     mailOK.ToString()
                                    );
                            }
                        }
                    }


                    #region 记录日志
                    OrderMessageLogBLL orderMessageLogBLL = new OrderMessageLogBLL(loggingSessionInfo);
                    OrderMessageLogEntity logObj = new OrderMessageLogEntity();
                    logObj.LogId = Utils.NewGuid();
                    logObj.VipId = loggingSessionInfo.CurrentUser.User_Id;
                    logObj.OrderId = inoutEntity.order_id;
                    logObj.IsCallSMSPush = "1";
                    logObj.MsgBody = StringBuilderLog.ToString();
                    logObj.CreateTime = DateTime.Now;
                    logObj.CreateBy = loggingSessionInfo.CurrentUser.User_Id;
                    logObj.LastUpdateTime = DateTime.Now;
                    logObj.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
                    if (mailOK)
                    {
                        logObj.MsgStatus = 1;
                        orderMessageLogBLL.Create(logObj);
                    }
                    else
                    {
                        logObj.MsgStatus = 2;
                        logObj.MsgError = "邮件发送失败";
                        orderMessageLogBLL.Create(logObj);
                    }

                    #endregion

                }

                #endregion

                #region 短信推送消息
                if (_TOrderPushStrategyEntity.IsSMSPush == 1)
                {
                    if (_TOrderPushStrategyEntity.PushObject == 1)//购买者
                    {
                        StringBuilderLog.AppendLine("向购买者推送短信:");
                        string strMessage = JIT.CPOS.Common.Utils.SMSSendOrder(vipEntity.Phone, msg);
                        StringBuilderLog.AppendFormat(
                            "order_id:{0},create_user_id{1},短信内容:{2},返回结果:{3}",
                             inoutEntity.order_id,
                             inoutEntity.create_user_id,
                             msg,
                             strMessage
                            );
                    }
                    if (_TOrderPushStrategyEntity.PushObject == 2)//客服
                    {
                        PushSMSToUserList(msg, StringBuilderLog);
                    }
                }

                #endregion

            }

            #endregion

        }


        /// <summary>
        /// 向客服推送短信
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="pStringBuilder">日志内容</param>
        private void PushSMSToUserList(string msg, StringBuilder pStringBuilder)
        {
            try
            {
                //IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
                IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByMenuCode("CustomerOrders");

                Loggers.Debug(new DebugLogInfo() { Message = "客服列表，userInfos:" + userInfos.ToJSON() });
                pStringBuilder.AppendLine("向客服列表推送邮件:");

                foreach (var userInfo in userInfos)
                {
                    //IOS推送
                    string strOrderSend = JIT.CPOS.Common.Utils.SMSSendOrder(userInfo.User_Telephone, msg);
                    pStringBuilder.AppendFormat(
                        "用户ID:{0},手机:{1},发送内容:{2},返回结果:{3}",
                        userInfo.Create_User_Id
                        , userInfo.User_Telephone
                        , msg
                        , strOrderSend);
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("推送短信异常信息: {0}", ex.ToString())
                });
            }
        }

        /// <summary>
        /// 向客服推送邮件
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="pStringBuilder">日志内容</param>
        private bool PushMailToUserList(string msg, StringBuilder pStringBuilder)
        {
            try
            {
                bool mailOK = false;
              //  IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
                IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByMenuCode("CustomerOrders");

                Loggers.Debug(new DebugLogInfo() { Message = "客服列表，userInfos:" + userInfos.ToJSON() });
                pStringBuilder.AppendLine("向客服列表推送邮件:");


                foreach (var userInfo in userInfos)
                {
                    //
                    bool bOrderSend = false;
                    string strMail = userInfo.User_Email;
                    if (userInfo.User_Email.IndexOf('@') > 0)//判断是否是合法邮箱
                    {
                        bOrderSend = JIT.Utility.Notification.Mail.SendMail(userInfo.User_Email, "您有新的邮件", msg);
                    }
                    pStringBuilder.AppendFormat(
                        "用户ID:{0},邮箱:{1},发送内容:{2},返回结果:{3}",
                        userInfo.Create_User_Id
                        , "<" + strMail + ">"
                        , msg
                        , bOrderSend);
                    mailOK = mailOK & bOrderSend;
                    pStringBuilder.AppendLine();
                }
                return mailOK;
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("推送邮件异常信息: {0}", ex.ToString())
                });
                return false;
            }
        }


        /// <summary>
        /// 推送消息给客服
        /// </summary>
        /// <param name="pPushMessage">消息内容</param>
        /// <param name="msg">推送的具体消息</param>
        private void PushToUserList(IPushMessage pPushMessage, string msg)
        {
            try
            {
              //  IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
                IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByMenuCode("CustomerOrders");
                Loggers.Debug(new DebugLogInfo() { Message = "客服列表，userInfos:" + userInfos.ToJSON() });

                foreach (var userInfo in userInfos)
                {
                    //IOS推送
                    pPushMessage.PushMessage(userInfo.User_Id, msg);
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("推送客服消息异常信息: {0}", ex.ToString())
                });
            }
        }

        /// <summary>
        /// 这是OrderPushMessage
        /// 根据订单状态，做出不同的推送消息
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">状态</param>
        public void OrderPushMessageBAK(string orderId, string status)
        {
            Loggers.Debug(new DebugLogInfo() { Message = "推送开始......" });
            IPushMessage pushIOSMessage = new PushIOSMessage(loggingSessionInfo);
            IPushMessage pushAndroidMessage = new PushAndroidMessage(loggingSessionInfo);

            Loggers.Debug(new DebugLogInfo() { Message = "订单状态，status:" + status });
            //下订单 
            if (status == "100")    //订单状态“未审核”
            {
                //推送给客服
                try
                {
                    //IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByRoleCode("CustomerService");
                    IList<UserInfo> userInfos = new cUserService(loggingSessionInfo).GetUserListByMenuCode("CustomerOrders");
                    Loggers.Debug(new DebugLogInfo() { Message = "客服列表，userInfos:" + userInfos.ToJSON() });

                    string msg = "你有一笔新订单待处理";
                    foreach (var userInfo in userInfos)
                    {
                        //IOS推送
                        pushIOSMessage.PushMessage(userInfo.User_Id, msg);
                        //Android推送
                        pushAndroidMessage.PushMessage(userInfo.User_Id, msg);
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("推送客服消息异常信息: {0}", ex.ToString())
                    });
                }
            }
            else if (status == "500" || status == "600" || status == "900")    //订单状态 "未发货","已发货","审核不通过"
            {

                //通过订单号获取订单信息
                var inoutEntity = this.GetInoutInfoById(orderId);

                if (inoutEntity != null && !string.IsNullOrEmpty(inoutEntity.vip_no))
                {

                    var msg = string.Empty;
                    switch (status)
                    {
                        case "500": msg = string.Format("亲爱的会员，您的订单{0}已通过审核，我们将以最快的速度为您发货，感谢您对我们的支持O(∩_∩)O~", inoutEntity.order_no); break;
                        case "600":
                            //遍历订单的全部商品
                            StringBuilder strb = new StringBuilder();

                            foreach (var item in inoutEntity.InoutDetailList)
                            {
                                strb.AppendFormat("{0},", item.item_name);
                            }
                            if (strb.Length > 0)
                            {
                                strb.Remove(strb.Length - 1, 1);
                            }

                            msg = string.Format("亲爱的会员，您订购的{0}已于{1}发货了，预计将在30分钟内送达，配送公司：{2}，运单号：{3}，请您注意查收，如需帮助请在线私信我", strb, inoutEntity.Field9, inoutEntity.carrier_name, inoutEntity.Field2);
                            break;
                        case "900": msg = "亲爱的会员，感谢您在微商城下单，但因您的订单还存在问题，未能通过，您可以重新下单，或私信我了解原因O(∩_∩)O~"; break;
                    }


                    #region 推送消息给微信用户

                    var appList = new WApplicationInterfaceBLL(loggingSessionInfo).
                        QueryByEntity(new WApplicationInterfaceEntity { CustomerId = loggingSessionInfo.ClientID }, null);

                    if (appList != null && appList.Length > 0)
                    {
                        VipEntity vipEntity = new VipBLL(loggingSessionInfo).GetByID(inoutEntity.vip_no);

                        if (vipEntity != null && !string.IsNullOrEmpty(vipEntity.WeiXinUserId))
                        {
                            CommonBLL commonBll = new CommonBLL();
                            SendMessageEntity messageEntity = new SendMessageEntity();
                            messageEntity.content = msg;
                            messageEntity.touser = vipEntity.WeiXinUserId;
                            messageEntity.msgtype = "text";
                            commonBll.SendMessage(messageEntity, appList.FirstOrDefault().AppID,
                                appList.FirstOrDefault().AppSecret, loggingSessionInfo);
                        }
                    }

                    #endregion

                    #region 推送消息给APP

                    //IOS推送
                    pushIOSMessage.PushMessage(inoutEntity.vip_no, msg);
                    //Android推送
                    pushAndroidMessage.PushMessage(inoutEntity.vip_no, msg);

                    #endregion
                }
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
                inoutInfo.carrier_id = SetOrderInfo.CarrierID;
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
                    inoutInfo.unit_id = SetOrderInfo.StoreId;
                    inoutInfo.sales_unit_id = SetOrderInfo.StoreId;
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

        #region Jermyn20140307 生成虚拟订单
        /// <summary>
        /// 生成虚拟订单
        /// </summary>
        /// <param name="orderId">订单标识</param>
        /// <param name="customerId">客户标识</param>
        /// <param name="unitId">门店标识</param>
        /// <param name="vipId">用户标识</param>
        /// <param name="dataFromId">来源</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        public string SetVirtualOrderInfo(string orderId
                                         , string customerId
                                         , string unitId
                                         , string vipId
                                         , string dataFromId
                                         , string amount, string OffOrderNo, string remark, string UserID)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string str = inoutService.SetVirtualOrderInfo(orderId, customerId, unitId, vipId, dataFromId, amount, OffOrderNo, remark, UserID);
            return str;
        }
        #endregion


        #region 订单修改实付金额
        /// <summary>
        /// 根据订单ID修改订单配送状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">状态：1未付款/2待处理/3已发货/0已取消</param>
        /// <param name="send_time">发货时间</param>
        /// <param name="tableNo">桌号</param>
        public bool UpdateOrderPrice(string orderId, decimal ActualPrice, string userId)
        {
            try
            {
                InoutInfo info = GetInoutInfoById(orderId);
                InoutInfo inoutInfo = new InoutInfo();
                inoutInfo.order_id = orderId;
                inoutInfo.modify_time = new BaseService().GetCurrentDateTime();
                inoutInfo.modify_user_id = userId;
                inoutInfo.actual_amount = ActualPrice;
                if (info != null && info.total_amount > 0)
                {
                    inoutInfo.discount_rate = decimal.Round(Convert.ToDecimal((ActualPrice / info.total_amount) * 100), 2);
                }
                bool bReturn = inoutService.Update(inoutInfo);
                return bReturn;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        public DataSet GetItemNameByOrderId(string orderId)
        {
            return this.inoutService.GetItemNameByOrderId(orderId);
        }

        #region 根据订单号获取订单ID 2014-10-21

        /// <summary>
        /// 根据订单号获取订单ID
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public string GetOrderIDByOrderNo(string orderNo, string customerID)
        {
            return this.inoutService.GetOrderIDByOrderNo(orderNo, customerID);

        }
        #endregion


        #region 提交虚拟订单（花间堂核销优惠券时创建使用） 2014-10-20
        /// <summary>
        /// 提交虚拟订单（花间堂核销优惠券时创建使用）
        /// </summary>
        /// <param name="SetOrderInfo"></param>
        /// <param name="strError"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool SetOrderOnlineShoppingCoupon(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo
                                            , out string strError
                                            , out string strMsg)
        {
            strMsg = "";
            try
            {
                #region 获取客户信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.VIPID = "#USERID#";
                vipInfo.WeiXinUserId = "#USERID#";
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
                inoutInfo.order_reason_id = "84F79464626F4FB7ABA6492C3C648D3C";
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
                inoutInfo.data_from_id = "6";   //来源表SysVipSource ，是3 微信
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

    }
}

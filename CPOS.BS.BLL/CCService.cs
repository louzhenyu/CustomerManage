using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 盘点单类
    /// </summary>
    public class CCService : BaseService
    {
        JIT.CPOS.BS.DataAccess.CCService ccService = null;
        #region 构造函数
        public CCService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            ccService = new DataAccess.CCService(loggingSessionInfo);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 盘点单查询
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">单据号码</param>
        /// <param name="status">状态标识</param>
        /// <param name="unit_id">单位标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="order_date_begin">单据日期开始【yyyy-MM-dd】</param>
        /// <param name="order_date_end">单据日期结束【yyyy-MM-dd】</param>
        /// <param name="complete_date_begin">完成日期开始【yyyy-MM-dd】</param>
        /// <param name="complete_date_end">完成日期结束【yyyy-MM-dd】</param>
        /// <param name="data_from_id">数据来源标识</param>
        /// <param name="maxRowCount">每页最大数量</param>
        /// <param name="startRowIndex">开始行号</param>
        /// <returns></returns>
        public CCInfo SearchCCInfo(LoggingSessionInfo loggingSessionInfo
                                        , string order_no
                                        , string status
                                        , string unit_id
                                        , string warehouse_id
                                        , string order_date_begin
                                        , string order_date_end
                                        , string complete_date_begin
                                        , string complete_date_end
                                        , string data_from_id
                                        , int maxRowCount
                                        , int startRowIndex
                                        )
        {
            try
            {
                OrderSearchInfo orderSearchInfo = new OrderSearchInfo();
                orderSearchInfo.order_no = order_no;
                orderSearchInfo.status = status;
                orderSearchInfo.unit_id = unit_id;
                orderSearchInfo.warehouse_id = warehouse_id;
                orderSearchInfo.order_date_begin = order_date_begin;
                orderSearchInfo.order_date_end = order_date_end;
                orderSearchInfo.complete_date_begin = complete_date_begin;
                orderSearchInfo.complete_date_end = complete_date_end;
                orderSearchInfo.data_from_id = data_from_id;
                orderSearchInfo.StartRow = startRowIndex;
                orderSearchInfo.EndRow = startRowIndex + maxRowCount;
                orderSearchInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;

                orderSearchInfo.order_type_id = "51BE351BFA5E49A490103669EA21BC3C";
                

                CCInfo ccInfo = new CCInfo();
                int iCount = ccService.SearchCCCount(orderSearchInfo);
                
                IList<CCInfo> ccInfoList = new List<CCInfo>();
                DataSet ds = ccService.SearchCCInfo(orderSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ccInfoList = DataTableToObject.ConvertToList<CCInfo>(ds.Tables[0]);
                }
                
                ccInfo.ICount = iCount;
                ccInfo.CCInfoList = ccInfoList;
                return ccInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 获取单个盘点单明细
        
        /// <summary>
        /// 获取单个盘点单明细
        /// </summary>
        /// <param name="orderId">订单标识【必须】</param>
        /// <param name="maxRowCount">商品明细当前页显示数量</param>
        /// <param name="startRowIndex">商品明细当前页开始行</param>
        /// <returns></returns>
        public CCInfo GetCCInfoById(string orderId, int maxRowCount, int startRowIndex)
        {
            try
            {
                CCInfo ccInfo = new CCInfo();
                DataSet ds = ccService.GetCCInfoById(orderId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ccInfo = DataTableToObject.ConvertToObject<CCInfo>(ds.Tables[0].Rows[0]);
                }
               
                CCInfo ccInfo1 = new CCInfo();
                ccInfo1 = GetCCDetailInfoByOrderId(orderId, maxRowCount, startRowIndex);
                ccInfo.CCDetail_ICount = ccInfo1.CCDetail_ICount;
                ccInfo.CCDetailInfoList = ccInfo1.CCDetailInfoList;
                return ccInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取单个盘点单sku明细
        /// </summary>
        /// <param name="loggingSessionInfo">登录model【必须】</param>
        /// <param name="orderId">订单标识【必须】</param>
        /// <param name="maxRowCount">商品明细当前页显示数量</param>
        /// <param name="startRowIndex">商品明细当前页开始行</param>
        /// <returns></returns>
        public CCInfo GetCCDetailInfoByOrderId(string orderId, int maxRowCount, int startRowIndex)
        {
            try
            {
                CCInfo ccInfo = new CCInfo();
                IList<CCDetailInfo> ccDetailList = new List<CCDetailInfo>();
                int iCount = ccService.GetCCDetailCountByOrderId(orderId);
                DataSet ds = new DataSet();
                ds = ccService.GetCCDetailInfoListByOrderId(orderId, startRowIndex, startRowIndex + maxRowCount);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ccDetailList = DataTableToObject.ConvertToList<CCDetailInfo>(ds.Tables[0]);
                }
                ccInfo.CCDetail_ICount = iCount;
                ccInfo.CCDetailInfoList = ccDetailList;

                return ccInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 获取商品明细
        /// <summary>
        /// 获取商品明细
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">订单标识</param>
        /// <param name="unit_id">组织标识</param>
        /// <param name="warehouse_id">门店标识</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns>ccDetailInfo.icount = 总数量 ccDetailInfo.CCDetailInfoList = 商品明细集合</returns>
        public CCDetailInfo GetCCDetailListStockBalance(LoggingSessionInfo loggingSessionInfo,string order_id, string unit_id, string warehouse_id, int maxRowCount, int startRowIndex)
        {
            try
            {
                CCDetailInfo ccDetailInfo = new CCDetailInfo();

                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", order_id);
                _ht.Add("UnitId", unit_id);
                _ht.Add("WarehouseId", warehouse_id);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);

                int iCount = ccService.GetStockBalanceCount(_ht);
                if (iCount > 0)
                {
                    IList<CCDetailInfo> ccDetailList = new List<CCDetailInfo>();
                    DataSet ds = new DataSet();
                    ds = ccService.GetStockBanlanceList(_ht);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ccDetailList = DataTableToObject.ConvertToList<CCDetailInfo>(ds.Tables[0]);
                    }
                    ccDetailInfo.icount = iCount;
                    ccDetailInfo.CCDetailInfoList = ccDetailList;
                }
                return ccDetailInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 处理盘点单信息（新建，修改）
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="ccInfo">盘点单model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="strError">返回错误信息</param>
        /// <returns></returns>
        public bool SetCCInfo(CCInfo ccInfo, bool IsTrans, out string strError)
        {
            ccInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
            if (ccInfo.if_flag == null || ccInfo.if_flag.ToString().Trim().Equals(""))
            {
                ccInfo.if_flag = "0";
            }
            if (ccInfo.order_type_id == null || ccInfo.order_type_id.Equals(""))
            {
                ccInfo.order_type_id = "51BE351BFA5E49A490103669EA21BC3C";
            }
            if (ccInfo.BillKindCode == null || ccInfo.BillKindCode.Equals(""))
            {
                ccInfo.BillKindCode = "CC";
            }
            if (ccInfo.order_id == null || ccInfo.order_id.Equals(""))
            {
                ccInfo.order_id = NewGuid();
            }

            if (ccInfo.data_from_id == null || ccInfo.data_from_id.Equals(""))
            {
                ccInfo.data_from_id = "B8DF5D46D3CA430ABE21E20F8D71E212";
            }

            if (ccInfo.create_user_id == null || ccInfo.create_user_id.Equals(""))
            {
                ccInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                ccInfo.create_time = GetCurrentDateTime();
            }
            if (ccInfo.modify_user_id == null || ccInfo.modify_user_id.Equals(""))
            {
                ccInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                ccInfo.modify_time = GetCurrentDateTime();
            }

            CCInfo ccInfo1 = new CCInfo();
            ccInfo1 = GetCCInfoById(ccInfo.order_id,10000,0);
            if (ccInfo1 == null || ccInfo1.order_id == null || ccInfo1.order_id.Equals("")) { ccInfo.operate = "Create"; } else { ccInfo.operate = "Modify"; }
            
            if (ccInfo.operate == null || ccInfo.operate.Equals(""))
            {
                ccInfo.operate = "Create";
            }

            if (ccInfo.CCDetailInfoList != null)
            {
                foreach (CCDetailInfo ccDetailInfo in ccInfo.CCDetailInfoList)
                {
                    if (ccDetailInfo.order_detail_id == null || ccDetailInfo.order_detail_id.Equals(""))
                    {
                        ccDetailInfo.order_detail_id = NewGuid();
                    }
                    if (ccDetailInfo.if_flag.ToString() == null || ccDetailInfo.if_flag.ToString().Equals(""))
                    {
                        ccDetailInfo.if_flag = 0;
                    }
                    ccDetailInfo.order_id = ccInfo.order_id;
                    ccDetailInfo.order_no = ccInfo.order_no;
                    ccDetailInfo.create_time = GetCurrentDateTime();
                    ccDetailInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                }
            }

            IList<CCInfo> ccInfoList = new List<CCInfo>();
            ccInfoList.Add(ccInfo);
            ccInfo.CCInfoList = ccInfoList;
            bool bReturn = ccService.SetCCInfo(ccInfo, IsTrans, out strError);
            return false;
        }
        #endregion

        #region 盘点单状态更新
        /// <summary>
        /// 订单状态修改
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="billActionType"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetCCStatusUpdate(string order_id, BillActionType billActionType, out string strError)
        {
            string strResult = string.Empty;
            try
            {
                CCInfo ccInfo = new CCInfo();
                ccInfo.order_id = order_id;
                ccInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                ccInfo.modify_time = GetCurrentDateTime(); //获取当前时间
                if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null || loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
                {

                    switch (billActionType)
                    {
                        case BillActionType.Cancel:
                            ccInfo.status = "-1";
                            ccInfo.status_desc = "删除";
                            break;
                        case BillActionType.Approve:
                            ccInfo.status = "10";
                            ccInfo.status_desc = "已审批";
                            ccInfo.approve_time = GetCurrentDateTime();
                            ccInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                            break;
                        case BillActionType.Reject:
                            ccInfo.status = "1";
                            ccInfo.status_desc = "未审批";
                            break;
                        case BillActionType.Create:
                            ccInfo.status = "1";
                            ccInfo.status_desc = "未审批";
                            break;
                        default:
                            ccInfo.status = "1";
                            ccInfo.status_desc = "未审批";
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

                        ccInfo.status = billInfo.Status;
                        ccInfo.status_desc = billInfo.BillStatusDescription;

                        if (billActionType == BillActionType.Approve)
                        {
                            ccInfo.approve_time = GetCurrentDateTime();
                            ccInfo.approve_user_id = loggingSessionInfo.CurrentUser.User_Id;
                        }
                    }
                    else
                    {
                        strError = "获取状态失败--" + strResult;
                        return false;
                    }
                }
                bool bReturn = ccService.SetCCStatusUpdate(ccInfo);
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

        #region 批量保存盘点单
        /// <summary>
        /// 批量保存盘点单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="ccInfoList"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetCCInfoList(LoggingSessionInfo loggingSessionInfo, IList<CCInfo> ccInfoList, out string strError)
        {
            foreach (CCInfo ccInfo1 in ccInfoList)
            { 
                
            }
            CCInfo ccInfo = new CCInfo();
            ccInfo.CCInfoList = ccInfoList;
            bool bReturn = SetCCInfo(ccInfo, false, out strError);
           
            return bReturn;
        }
        #endregion

        #region 判断盘点单是否能成成调整单
        /// <summary>
        /// 判断盘点单是否有生成调整单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="orderId">单据标识</param>
        /// <returns></returns>
        public bool IsExistAJOrder(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            bool bReturn = false;
            //Hashtable _ht = new Hashtable();
            //_ht.Add("OrderId", orderId);
            //int iCount = cSqlMapper.Instance().QueryForObject<int>("CC.IsExistAJ", _ht);
            //int iCount1 = cSqlMapper.Instance().QueryForObject<int>("CC.IsHaveAJCount", _ht);
            //if (iCount == 1 && iCount1 == 0) {
            //    bReturn = true;
            //}
            return bReturn;
        }
        #endregion

        #region 盘点单生产调整单
        /// <summary>
        /// 盘点单生产调整单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">盘点单标识</param>
        /// <returns></returns>
        public bool SetCCToAJ(LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            try
            {
                //Hashtable _ht = new Hashtable();
                //_ht.Add("OrderId", order_id);
                //_ht.Add("OrderNo",GetNo(loggingSessionInfo,"AJ"));
                //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("CC.SetCCToAJ", _ht);
                return true;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 盘点单是否有差异
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">盘点单标识</param>
        /// <returns>ture=有差异；false=无差异，则不能生产调整单</returns>
        public bool IsCCDifference(LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            try
            {
                bool bReturn = false;
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", order_id);

                int iCount = 0;//cSqlMapper.Instance().QueryForObject<int>("CC.IsCCDifference", _ht);
                if (iCount.ToString().Equals("0") || iCount.ToString().Equals(""))
                {
                    bReturn = false;
                }else{
                    bReturn = true;
                }
                return bReturn;
            }
            catch (Exception ex) {
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
        /// <param name="CCInfoList">单据集合</param>
        /// <param name="IsTrans">是否提交</param>
        /// <returns></returns>
        public bool SetCCIfFlag(LoggingSessionInfo loggingSessionInfo, string if_flag, IList<CCInfo> CCInfoList, bool IsTrans)
        {
            //if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
            //try
            //{
            //    bool bReturn = false;
            //    CCInfo inoutInfo = new CCInfo();
            //    inoutInfo.if_flag = if_flag;
            //    inoutInfo.CCInfoList = CCInfoList;
            //    inoutInfo.modify_time = GetCurrentDateTime();
            //    inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            //    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("CC.UpdateIfflag", inoutInfo);
            //    if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
            //    return bReturn;
            //}
            //catch (Exception ex)
            //{
            //    if (IsTrans) cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).RollBackTransaction();
            //    throw (ex);
            //}
            return false;
        }
        #endregion

    }
}

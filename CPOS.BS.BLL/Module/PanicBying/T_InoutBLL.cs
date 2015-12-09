/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:14
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.DTO.Base;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Order.Response;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.Log;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_InoutBLL
    {
        public string SetOrderInfo(SetOrderInfoReqPara para)
        {
            var loggingSessionInfo = this.CurrentUserInfo as LoggingSessionInfo;
            //��ȡ������
            TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
            string OrderCode = serviceUnitExpand.GetUnitOrderNo();

            ////���StoreIDΪ��,�������̳Ƕ���,�ظ����߼�
            //if (string.IsNullOrEmpty(para.storeId.Trim()))
            //{
            //    UnitService unitServer = new UnitService(loggingSessionInfo);
            //    para.storeId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //��ȡ�����̳ǵ��ŵ��ʶ
            //}

            //�жϻ����Ч��

            #region ���Ч��

            var eventBll = new vwItemPEventDetailBLL(loggingSessionInfo);
            var detail = eventBll.GetByEventIDAndItemID(para.eventId);
            if (detail == null)
                throw new Exception("δ�ҵ���ػ��Ʒ��Ϣ");
            //1.	��Ҫ�жϣ��ö�������Ʒ�Ƿ���ӯ��
            if (detail.RemainingQty <= 0)
                throw new Exception("���Ʒ��������,��ǰ����:0");
            //2.	�жϣ�����Ʒ��Ƿ��Ѿ���ֹ
            if (!string.IsNullOrEmpty(detail.StopReason))
                throw new Exception("���ֹͣ,ֹͣԭ��:" + detail.StopReason);
            //3.�жϹ�������Ƿ�С�ڵ���ʣ�����
            if (int.Parse(para.qty) > detail.RemainingQty)
                throw new Exception("���Ʒ�������㣬��ǰ������" + detail.RemainingQty);
            if (string.IsNullOrEmpty(para.userId))
                throw new Exception("��Ա��Ϣ������");

            #endregion

            //��������

            #region ���ݶ����������ö�������

            string order_reason_id = string.Empty;
            if (para.isGroupBy == "1") //�Ź�
                order_reason_id = "CB43DD7DD1C94853BE98C4396738E00C";
            else if (para.isPanicbuying == "1") //����
                order_reason_id = "671E724C85B847BDA1E96E0E5A62055A";
            else //��ͨ
                order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";

            #endregion

            //��������
            var tran = this._currentDAO.GetTran();
            var orderId = string.Empty;
            try
            {
                using (tran.Connection)
                {
                    T_InoutEntity entity = new T_InoutEntity()
                    {
                        #region ������ʼ��
                        order_date = DateTime.Now.ToString("yyyy-MM-dd"),
                        order_type_id = "1F0A100C42484454BAEA211D4C14B80F",
                        create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        customer_id = para.customerId,
                        status = "1",
                        total_qty = Convert.ToDecimal(para.qty),
                        unit_id = para.storeId,
                        order_no = OrderCode,
                        order_id = Guid.NewGuid().ToString("N"),
                        order_reason_id = order_reason_id, //��������:��ͨ,�Ź�,����
                        red_flag = "1",
                        warehouse_id = "67bb4c12785c42d4912aff7d34606592", //???�Ƿ������??
                        create_unit_id = para.storeId,
                        create_user_id = para.userId,
                        total_amount = Convert.ToDecimal(para.totalAmount),
                        actual_amount = Convert.ToDecimal(para.actualAmount),
                        discount_rate = para.Rate,
                        total_retail = Convert.ToDecimal(para.totalAmount),
                        print_times = Convert.ToInt32(para.joinNo),
                        vip_no = para.userId,
                        data_from_id = para.reqBy,
                        if_flag = "0",
                        remark = para.remark,
                        Field1 = "0",
                        Field3 = para.isALD,
                        Field7 = "-99",
                        Field8 = para.deliveryId,
                        send_time = para.deliveryTime,
                        Field9 = para.deliveryTime,
                        Field4 = para.deliveryAddress,
                        Field6 = para.mobile,
                        Field12 = para.email,
                        Field13 = para.openId,
                        Field10 = "δ���",
                        Field14 = para.username,
                        Field20 = para.tableNumber,
                        Field16 = para.couponsPrompt,
                        Field15 = order_reason_id,
                        sales_unit_id = para.storeId
                        #endregion
                    };
                    //����״̬
                    if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null ||
                        loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
                    {
                        entity.status = "100";
                        entity.status_desc = "δ����";
                    }
                    //��������
                    this._currentDAO.Create(entity, tran);
                    orderId = entity.order_id;
                    //����������ϸ

                    #region ������ϸ�б�

                    var detailbll = new T_Inout_DetailBLL(loggingSessionInfo);
                    foreach (var item in para.orderDetailList)
                    {
                        var detailEntity = new T_Inout_DetailEntity()
                        {
                            create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            create_user_id = para.userId,
                            discount_rate = para.Rate,
                            std_price = Convert.ToDecimal(item.salesPrice),
                            enter_price = Convert.ToDecimal(item.salesPrice),
                            sku_id = item.skuId,
                            enter_qty = Convert.ToDecimal(para.qty),
                            order_id = entity.order_id,
                            order_detail_id = Guid.NewGuid().ToString("N"),
                            order_qty = entity.total_qty,
                            enter_amount = item.Amount,
                            order_detail_status = "1",
                            retail_amount = item.Amount,
                            retail_price = item.Amount,
                            display_index = 1,
                            if_flag = 0
                        };
                        detailbll.Create(detailEntity, tran);
                    }

                    #endregion

                    //�¶������޸�������Ʒ��������Ϣ�洢����ProcPEventItemQty
                    //var eventbll = new vwItemPEventDetailBLL(loggingSessionInfo);
                    //eventbll.ExecProcPEventItemQty(para, entity, tran);
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            //
            return orderId;
        }

        public DataSet GetItemEventSalesUserList(string itemId, Guid eventId)
        {
            return this._currentDAO.GetItemEventSalesUserList(itemId, eventId);
        }

        public DataSet GetEventStoreByItemAndEvent(string itemId, Guid eventId)
        {
            return this._currentDAO.GetEventStoreByItemAndEvent(itemId, eventId);
        }

        public DataSet GetItemBrandInfo(string itemId)
        {
            return this._currentDAO.GetItemBrandInfo(itemId);
        }

        public DataSet GetItemProp1List(string skuId)
        {
            return this._currentDAO.GetItemProp1List(skuId);
        }

        /// <summary>
        /// ��ȡ����״̬ Add by Alex Tian 2014-04-16
        /// </summary>
        /// <returns></returns>
        public GetOrdersRD GetOrder(string vipno, int PageIndex, int PageSize, string customer_id, int GroupingType, string ChannelId, string UserId)
        {
            GetOrdersRD rdata = new GetOrdersRD();
            DataSet ds = null;
            //switch (GroupingType)
            //{
            //    case 1:
            //        ds = this._currentDAO.GetOrderByObligation(vipno, PageIndex, PageSize, customer_id); //��ѯ����״̬Ϊ������
            //        break;
            //    case 2:
            //        ds = this._currentDAO.GetOrderByNodelivery(vipno, PageIndex, PageSize, customer_id); //��ѯ����״̬Ϊ���ջ�/���
            //        break;
            //    case 3:
            //        ds = this._currentDAO.GetOrderBydonedeal(vipno, PageIndex, PageSize, customer_id); //����״̬Ϊ�����
            //        break;
            //    default:
            //        break;
            //}


            //�����������������ȡ�µĴ洢���� add by donal 2014-9-26 17:41:10
            if (ChannelId == "6")
                ds = this._currentDAO.GetOrderByGroupingTypeEvery(UserId, PageIndex, PageSize, customer_id, GroupingType);
            else
                ds = this._currentDAO.GetOrderByGroupingType(vipno, PageIndex, PageSize, customer_id, GroupingType);

            if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                List<T_InoutEntity> list = new List<T_InoutEntity> { };
                int count = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

                if (count <= 0)
                {
                    return rdata;
                }
                using (var rd = ds.Tables[1].CreateDataReader())
                {
                    while (rd.Read())
                    {
                        T_InoutEntity m;
                        this._currentDAO.NewLoad(rd, out m);
                        list.Add(m);
                    }
                }
                var ids = list.Select(t => t.order_id).ToArray();
                var detailBLL = new T_Inout_DetailBLL(this.CurrentUserInfo as LoggingSessionInfo);
                var detailEntitys = detailBLL.GetByIDS(ids);
                var Orderlist = new List<JIT.CPOS.DTO.Module.VIP.Order.Response.OrderInfo> { };

                #region [ ��֯orderInfo ]

                foreach (var item in list)
                {
                    JIT.CPOS.DTO.Module.VIP.Order.Response.OrderInfo orderinfo =
                        new DTO.Module.VIP.Order.Response.OrderInfo();
                    orderinfo.OrderID = item.order_id; //����ID
                    orderinfo.OrderNO = item.order_no; //��������
                    orderinfo.DeliveryTypeID = Convert.ToInt32(string.IsNullOrEmpty(item.Field8) ? "0" : item.Field8);
                    //���ͷ�ʽ���ID��1.�ͻ����š�2.������ȡ
                    orderinfo.purchase_unit_id = item.purchase_unit_id; //����ŵ�
                    orderinfo.OrderDate = item.create_time; //�ڶ������е��µ�ʱ��û��ʱ���롣����ȡcreate_time
                    orderinfo.OrderStatusDesc = item.status_desc; //����״̬����
                    orderinfo.OrderStatus = Convert.ToInt32(item.status); //����״̬
                    orderinfo.TotalQty = Convert.ToInt32(item.total_qty); //��Ʒ��������
                    orderinfo.TotalAmount = Convert.ToDecimal(item.actual_amount); //�ܽ��
                    orderinfo.PaymentTypeCode = item.Payment_Type_Code;//֧����ʽ
                    orderinfo.ReturnCash = item.ReturnCash == null ? 0.00m : Convert.ToDecimal(item.ReturnCash);//Ӷ��
                    orderinfo.IsEvaluation = item.IsEvaluation == null ? 0 : item.IsEvaluation.Value;//�Ƿ�����
                    #region ����OrderInfo��֯detail

                    var templist = detailEntitys.Where(t => t.order_id == item.order_id).ToArray();
                    var tempDetailInfos = new List<JIT.CPOS.DTO.Module.VIP.Order.Response.OrderDetailInfo> { };
                    foreach (var it in templist)
                    {
                        var detailInfo = new JIT.CPOS.DTO.Module.VIP.Order.Response.OrderDetailInfo();
                        detailInfo.ItemID = it.ItemID; //��ƷID
                        detailInfo.ItemName = it.ItemName; //��Ʒ����
                        detailInfo.SKUID = it.SKUID; //SKUID
                        detailInfo.Qty = it.Qty; //��������
                        detailInfo.SpecificationDesc = it.SpecificationDesc; //�������
                        detailInfo.SalesPrice = it.SalesPrice; //ʵ�ʵ���
                        detailInfo.ImageUrl = ImagePathUtil.GetImagePathStr(it.ImageUrl, "240"); //UrlͼƬ update by Henry 2014-12-8
                        detailInfo.ReturnCash = it.ReturnCash;

                        #region �������

                        var GGds = _currentDAO.GetInoutDetailGgByOrderId(orderinfo.OrderID);
                        if (GGds != null && GGds.Tables.Count > 0)
                        {
                            detailInfo.GG =
                                GGds.Tables[0].AsEnumerable()
                                    .Where(t => t["sku_id"].ToString() == it.SKUID.ToString())
                                    .Select(t => new JIT.CPOS.DTO.Module.VIP.Order.Response.GuiGeInfo
                                    {
                                        PropName1 = t["prop_1_name"].ToString(),
                                        PropDetailName1 = t["prop_1_detail_name"].ToString(),
                                        PropName2 = t["prop_2_name"].ToString(),
                                        PropDetailName2 = t["prop_2_detail_name"].ToString(),
                                        PropName3 = t["prop_3_name"].ToString(),
                                        PropDetailName3 = t["prop_3_detail_name"].ToString(),
                                        PropName4 = t["prop_4_name"].ToString(),
                                        PropDetailName4 = t["prop_4_detail_name"].ToString(),
                                        PropName5 = t["prop_5_name"].ToString(),
                                        PropDetailName5 = t["prop_5_detail_name"].ToString()
                                    }).FirstOrDefault();
                        }

                        #endregion

                        tempDetailInfos.Add(detailInfo);
                    }

                    #endregion

                    orderinfo.OrderDetails = tempDetailInfos.ToArray();
                    Orderlist.Add(orderinfo);
                }

                #endregion

                rdata.PageIndex = PageIndex;
                rdata.TotalPageCount = count;
                rdata.Orders = Orderlist.ToArray();
            }

            //��ò�ͬ�����µĶ�������
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                var Grouplist = new List<JIT.CPOS.DTO.Module.VIP.Order.Response.GroupingOrderCount> { };

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //������
                    var grouporder1 = new JIT.CPOS.DTO.Module.VIP.Order.Response.GroupingOrderCount();
                    grouporder1.GroupingType = 1;
                    grouporder1.OrderCount = int.Parse(ds.Tables[0].Rows[0]["RowRnt1"].ToString());
                    Grouplist.Add(grouporder1);
                    //���ջ�
                    var grouporder2 = new JIT.CPOS.DTO.Module.VIP.Order.Response.GroupingOrderCount();
                    grouporder2.GroupingType = 2;
                    grouporder2.OrderCount = int.Parse(ds.Tables[0].Rows[0]["RowRnt2"].ToString());
                    Grouplist.Add(grouporder2);
                    //�����
                    var grouporder3 = new JIT.CPOS.DTO.Module.VIP.Order.Response.GroupingOrderCount();
                    grouporder3.GroupingType = 3;
                    grouporder3.OrderCount = int.Parse(ds.Tables[0].Rows[0]["RowRnt3"].ToString());
                    Grouplist.Add(grouporder3);

                    //�Ѹ������ŵ����� Add by Henry 2014-12-18
                    if (ChannelId == "6")
                    {
                        var grouporder4 = new JIT.CPOS.DTO.Module.VIP.Order.Response.GroupingOrderCount();
                        grouporder4.GroupingType = 4;
                        grouporder4.OrderCount = int.Parse(ds.Tables[0].Rows[0]["RowRnt4"].ToString());
                        Grouplist.Add(grouporder4);
                    }
                    else
                    {
                        //δ���۶�������
                        var noEvaluationCount = new JIT.CPOS.DTO.Module.VIP.Order.Response.GroupingOrderCount();
                        noEvaluationCount.GroupingType = 6;
                        noEvaluationCount.OrderCount = int.Parse(ds.Tables[0].Rows[0]["NoEvaluationCount"].ToString());
                        Grouplist.Add(noEvaluationCount);
                    }
                }

                rdata.GroupingOrderCounts = Grouplist.ToArray();
            }

            return rdata;
        }

        /// <summary>
        /// ��ȡ�����б�Add by Alex Tian 2014-04-16
        /// </summary>
        /// <param name="pIsIncludeOrderDetails">�Ƿ��ȡ������Ʒ��ϸ</param>
        /// <param name="pOrderStatuses">����״̬</param>
        /// <param name="pOrderID">����ID</param>
        /// <param name="pVIPID">��ԱID</param>
        /// <param name="pPageSize">ÿҳ��¼����Ĭ��15</param>
        /// <param name="pPageIndex">��ǰҳ��Ĭ��Ϊ0</param>
        /// <returns></returns>
        public GetOrderListRD GetOrderList(bool pIsIncludeOrderDetails, int[] pOrderStatuses, string pOrderID,
            int pPageSize, int pPageIndex)
        {
            GetOrderListRD RD = new GetOrderListRD();
            DataSet ds = new DataSet();
            List<T_InoutEntity> list = new List<T_InoutEntity> { };
            var Orderlist = new List<JIT.CPOS.DTO.Module.Order.Order.Response.OrderInfo> { };

            ds = _currentDAO.GetOrderList(pOrderStatuses, pOrderID, pPageSize, pPageIndex);
            using (var rd = ds.Tables[0].CreateDataReader())
            {
                while (rd.Read())
                {
                    T_InoutEntity m;
                    this._currentDAO.NewLoad(rd, out m);
                    list.Add(m);
                }
            }

            if (pIsIncludeOrderDetails == false)
            {
                foreach (var item in list)
                {
                    JIT.CPOS.DTO.Module.Order.Order.Response.OrderInfo orderinfo =
                        new DTO.Module.Order.Order.Response.OrderInfo();
                    orderinfo.OrderID = item.order_id;
                    orderinfo.OrderNO = item.order_no;
                    orderinfo.DeliveryTypeID = Convert.ToInt32(string.IsNullOrEmpty(item.Field8) ? "0" : item.Field8);
                    orderinfo.OrderDate = Convert.ToDateTime(item.create_time);
                    //�µ�ʱ�䣬�����µ����е�OrderDateû��ʱ���롣����ȡ��ʱ��Ϊcreate_time
                    orderinfo.OrderStatusDesc = item.status_desc; //����״̬����
                    orderinfo.OrderStatus = Convert.ToInt32(item.status); //����״̬
                    orderinfo.TotalQty = Convert.ToInt32(item.total_qty); //��������
                    orderinfo.TotalAmount = Convert.ToDecimal(item.actual_amount); //�����ܽ��֮ǰ�õ���item.total_amount
                    orderinfo.OrderDetails = null;
                    Orderlist.Add(orderinfo);
                }
                RD.OrderList = Orderlist.ToArray();
            }
            if (pIsIncludeOrderDetails == true) //��ѯ������ϸ��Ϣ��
            {
                var ids = list.Select(t => t.order_id).ToArray();
                var detailBLL = new T_Inout_DetailBLL(this.CurrentUserInfo as LoggingSessionInfo);
                var detailEntitys = detailBLL.GetByIDS(ids);

                foreach (var item in list)
                {
                    JIT.CPOS.DTO.Module.Order.Order.Response.OrderInfo orderinfo =
                        new DTO.Module.Order.Order.Response.OrderInfo();
                    orderinfo.OrderID = item.order_id;
                    orderinfo.OrderNO = item.order_no;
                    orderinfo.DeliveryTypeID = Convert.ToInt32(string.IsNullOrEmpty(item.Field8) ? "0" : item.Field8);
                    orderinfo.OrderDate = Convert.ToDateTime(item.create_time);
                    //�µ�ʱ�䣬�����µ����е�OrderDateû��ʱ���롣����ȡ��ʱ��Ϊcreate_time
                    orderinfo.OrderStatusDesc = item.status_desc; //����״̬����
                    orderinfo.OrderStatus = Convert.ToInt32(item.status); //����״̬
                    orderinfo.TotalQty = Convert.ToInt32(item.total_qty); //��������
                    orderinfo.TotalAmount = Convert.ToDecimal(item.actual_amount); //�����ܽ��//�����ܽ��֮ǰ�õ���item.total_amount
                    var templist = detailEntitys.Where(t => t.order_id == item.order_id).ToArray();
                    var tempDetailInfos = new List<JIT.CPOS.DTO.Module.Order.Order.Response.OrderDetailInfo> { };
                    foreach (var it in templist)
                    {
                        var detailInfo = new JIT.CPOS.DTO.Module.Order.Order.Response.OrderDetailInfo();
                        detailInfo.ItemID = it.ItemID; //��ƷID
                        detailInfo.ItemName = it.ItemName; //��Ʒ����
                        detailInfo.SKUID = it.SKUID; //SKUID
                        detailInfo.Qty = it.Qty; //��������
                        detailInfo.SpecificationDesc = it.SpecificationDesc; //�������
                        detailInfo.SalesPrice = it.SalesPrice; //ʵ�ʵ���
                        detailInfo.ImageUrl = it.ImageUrl; //UrlͼƬ

                        #region �������

                        var GGds = _currentDAO.GetInoutDetailGgByOrderId(pOrderID);
                        if (GGds != null && GGds.Tables.Count > 0)
                        {
                            detailInfo.GG =
                                GGds.Tables[0].AsEnumerable()
                                    .Where(t => t["sku_id"].ToString() == it.SKUID.ToString())
                                    .Select(t => new JIT.CPOS.DTO.Module.Order.Order.Response.GuiGeInfo
                                    {
                                        PropName1 = t["prop_1_name"].ToString(),
                                        PropDetailName1 = t["prop_1_detail_name"].ToString(),
                                        PropName2 = t["prop_2_name"].ToString(),
                                        PropDetailName2 = t["prop_2_detail_name"].ToString(),
                                        PropName3 = t["prop_3_name"].ToString(),
                                        PropDetailName3 = t["prop_3_detail_name"].ToString(),
                                        PropName4 = t["prop_4_name"].ToString(),
                                        PropDetailName4 = t["prop_4_detail_name"].ToString(),
                                        PropName5 = t["prop_5_name"].ToString(),
                                        PropDetailName5 = t["prop_5_detail_name"].ToString()
                                    }).FirstOrDefault();
                        }

                        #endregion

                        tempDetailInfos.Add(detailInfo);
                    }
                    orderinfo.OrderDetails = tempDetailInfos.ToArray();
                    Orderlist.Add(orderinfo);
                }
                RD.OrderList = Orderlist.ToArray();
            }
            return RD;
        }
        //����״̬��ȡ������Ϣ
        public DataSet GetOrdersList(string orderId, string userId, string orderStatusList, string isPayment, string orderNo,
            string customerId, int? pageSize, int? pageIndex, string OrderChannelID)
        {
            return this._currentDAO.GetOrdersList(orderId, userId, orderStatusList, isPayment, orderNo, customerId, pageSize ?? 0,
                pageIndex ?? 15, OrderChannelID);
        }
        //��ȡ���ۣ����񣩶���
        public DataSet GetServiceOrderList(string order_no, string OrderChannelID, string userId, string customerId, int? pageSize, int? pageIndex)
        {
            return this._currentDAO.GetServiceOrderList(order_no, OrderChannelID, userId, customerId, pageSize ?? 0,
                pageIndex ?? 15);
        }

        //��ȡ���Ͷ���
        public DataSet GetCollectOrderList(string order_no, string OrderChannelID, string userId, string customerId, int? pageSize, int? pageIndex)
        {
            return this._currentDAO.GetCollectOrderList(order_no, OrderChannelID, userId, customerId, pageSize ?? 0,
                pageIndex ?? 15);
        }

        public System.Data.SqlClient.SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        public string GetPayTypeByOrderId(string orderId)
        {
            return this._currentDAO.GetPayTypeByOrderId(orderId);
        }

        #region ����΢�ŷ���֪ͨ
        /// <summary>
        /// ���ݶ�����ȡ����֪ͨ����
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="loggiongSessionInfo"></param>
        /// <returns>result = 'ok' Ϊ�ɹ�</returns>
        public string GetDeliverInfoByOrderId(string orderId, LoggingSessionInfo loggiongSessionInfo)
        {
            var wxDeliverInfo = new WxDeliverInfo();

            //��ȡ�������ĵĶ�����
            var tranCenterOrderId = this._currentDAO.GetTranCenterOrderId(orderId, loggiongSessionInfo.ClientID);

            var serder = new WApplicationInterfaceBLL(loggiongSessionInfo);
            //��ȡ΢�Ź��ںŵ���Ϣ
            var appEntity = serder.QueryByEntity(new WApplicationInterfaceEntity()
            {
                CustomerId = loggiongSessionInfo.ClientID
            }, null).FirstOrDefault();


            if (appEntity != null)
            {
                var appSecret = appEntity.AppSecret;
                wxDeliverInfo.appid = appEntity.AppID;
            }
            else
            {
                throw new APIException("΢�Ź��ں���ϢΪ��") { ErrorCode = 121 };
            }
            var wXPayNoticeBll = new WXPayNoticeBLL(loggiongSessionInfo);
            var transEntity = wXPayNoticeBll.QueryByEntity(new WXPayNoticeEntity()
            {
                OutTradeNo = tranCenterOrderId
            }, null).FirstOrDefault();

            if (transEntity != null)
            {
                wxDeliverInfo.openid = transEntity.OpenId;
                wxDeliverInfo.out_trade_no = transEntity.OutTradeNo;
                wxDeliverInfo.transid = transEntity.TransactionId;
                wxDeliverInfo.deliver_status = "1";
                wxDeliverInfo.deliver_msg = "ok";
            }
            else
            {
                throw new APIException("֧��֪ͨ��ϢΪ��") { ErrorCode = 122 };
            }
            //ʱ�������ǩ
            wxDeliverInfo.deliver_timestamp =
                Convert.ToString((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
            wxDeliverInfo.sign_method = "sha1";

            //��ȡ΢��֧����ר��ǩ��
            var appkey = this._currentDAO.GetAppKeyByAppId(wxDeliverInfo.appid);

            var tempDic = new Dictionary<string, object>
            {
                {"appid", wxDeliverInfo.appid},
                {"appkey", appkey},
                {"openid", wxDeliverInfo.openid},
                {"transid", wxDeliverInfo.transid},
                {"out_trade_no", wxDeliverInfo.out_trade_no},
                {"deliver_timestamp", wxDeliverInfo.deliver_timestamp},
                {"deliver_status", wxDeliverInfo.deliver_status},
                {"deliver_msg", wxDeliverInfo.deliver_msg}
            };
            //����ǩ��


            wxDeliverInfo.app_signature = Sha1(GetParametersStr(tempDic));


            var common = new CommonBLL();
            //��ȡ΢�ŵ�AccessToken
            var accessToken =
                common.GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, loggiongSessionInfo).access_token;

            //{"errcode":0,"errmsg":"ok"}
            var result = common.DeliverNotify(accessToken, loggiongSessionInfo, wxDeliverInfo.ToJSON());

            var data = result.DeserializeJSONTo<WxErrMessage>();

            Loggers.Debug(new DebugLogInfo() { Message = "΢�ŷ���֪ͨ���ؽ��:" + data.ToJSON() });

            return data.errmsg;
        }

        public static string GetParametersStr(IEnumerable<KeyValuePair<string, object>> pParameters,
            bool pUrlEncode = false)
        {
            var temp = pParameters.OrderBy(t => t.Key).Select(t => t);
            return temp.Aggregate(new StringBuilder(), (i, j) =>
            {
                i.AppendFormat("{0}={1}&", j.Key.ToLower(),
                    pUrlEncode ? HttpUtility.UrlEncode(j.Value.ToString()) : j.Value);
                return i;
            }).ToString().Trim('&');
        }


        public static String Sha1(String s)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
					'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                byte[] btInput = System.Text.Encoding.Default.GetBytes(s);
                SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

                byte[] md = sha.ComputeHash(btInput);
                // ������ת����ʮ�����Ƶ��ַ�����ʽ
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[(int)(((byte)byte0) >> 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }
        #endregion

        #region �����û�Ͷ�ߵ�άȨ��

        public string UpdateFeedback(string feedbackId, LoggingSessionInfo loggiongSessionInfo)
        {
            var wxRightOrdersBll = new WXRightOrdersBLL(loggiongSessionInfo);
            var feedbackEntity = wxRightOrdersBll.QueryByEntity(new WXRightOrdersEntity()
            {
                FeedBackId = feedbackId
            }, null).FirstOrDefault();

            if (feedbackEntity == null)
            {
                throw new APIException("��Ч��άȨ����") { ErrorCode = 122 };
            }
            var serder = new WApplicationInterfaceBLL(loggiongSessionInfo);
            //��ȡ΢�Ź��ںŵ���Ϣ
            var appEntity = serder.QueryByEntity(new WApplicationInterfaceEntity()
            {
                CustomerId = loggiongSessionInfo.ClientID
            }, null).FirstOrDefault();

            if (appEntity == null)
            {
                throw new APIException("΢�Ź��ں���ϢΪ��") { ErrorCode = 121 };
            }
            var common = new CommonBLL();
            //��ȡ΢�ŵ�AccessToken
            var accessToken =
                common.GetAccessTokenByCache(appEntity.AppID, appEntity.AppSecret, loggiongSessionInfo).access_token;

            //{"errcode":0,"errmsg":"ok"}
            var result = common.UpdatePayFeedBack(accessToken, loggiongSessionInfo, feedbackEntity.OpenId, feedbackId);

            var data = result.DeserializeJSONTo<WxErrMessage>();

            return data.errmsg;
        }

        #endregion

        public DataSet GetOrdersByVipID(string vipID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetOrdersByVipID(vipID, pageIndex, pageSize, OrderBy, sortType);
        }
    }

    public class WxDeliverInfo
    {
        public string appid;
        public string openid;
        public string transid;
        public string out_trade_no;
        public string deliver_timestamp;
        public string deliver_status;
        public string deliver_msg;
        public string app_signature;
        public string sign_method;
    }

    public class WxErrMessage
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
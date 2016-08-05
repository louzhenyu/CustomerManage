/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:26
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
using JIT.CPOS.BS.DataAccess;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_InoutBLL
    {
        /// <summary>
        /// ȡ������(Api�ͺ�̨ͨ��)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userType">0=��Ա��1=��̨�û�</param>
        /// <param name="loggingSessionInfo"></param>
        public void SetCancelOrder(string orderId, int userType, LoggingSessionInfo loggingSessionInfo)
        {
            var vipBll = new VipBLL(loggingSessionInfo);                    //��Աҵ��ʵ����
            var inoutDetailBLL = new Inout3Service(loggingSessionInfo);     //����ҵ��ʵ����
            var refundOrderBll = new T_RefundOrderBLL(loggingSessionInfo);  //�˻�ҵ��ʵ����
            var inoutBll = new T_InoutBLL(loggingSessionInfo);              //����ҵ��ʵ����
            PanicbuyingEventBLL panicbuyingEventBLL = new PanicbuyingEventBLL(loggingSessionInfo); //�����ҵ��ʵ����
            T_SuperRetailTraderItemMappingBLL superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(loggingSessionInfo); //������ҵ��ʵ����

            //��ȡ��������
            var inoutInfo = inoutBll.GetInoutInfo(orderId, loggingSessionInfo);

            //������֡������ֺ��Ż�ȯ
            vipBll.ProcSetCancelOrder(loggingSessionInfo.ClientID, inoutInfo.order_id, inoutInfo.vip_no);
            //��ȡ������ϸ
            var inoutDetailList = inoutDetailBLL.GetInoutDetailInfoByOrderId(inoutInfo.order_id, loggingSessionInfo.ClientID);

            #region �����˿�ҵ��
            if (inoutInfo != null)
            {
                //if (inoutInfo.Field1 == "1" && (inoutInfo.actual_amount - inoutInfo.DeliveryAmount) > 0)//�Ѹ���,����ʵ����-�˷�>0
                if (inoutInfo.Field1 == "1" && inoutInfo.actual_amount > 0)//�Ѹ���,����ʵ����>0,δ�������Բ��ü��˷�
                {

                    #region �����˻���(Ĭ��״̬Ϊȷ���ջ�)
                    var salesReturnBLL = new T_SalesReturnBLL(loggingSessionInfo);
                    var historyBLL = new T_SalesReturnHistoryBLL(loggingSessionInfo);
                    T_SalesReturnEntity salesReturnEntity = null;
                    T_SalesReturnHistoryEntity historyEntity = null;

                    var userBll = new T_UserBLL(loggingSessionInfo);    //��ԱBLLʵ����
                    VipEntity vipEntity = null;                         //��Ա��Ϣ

                    salesReturnEntity = new T_SalesReturnEntity();
                    salesReturnEntity.SalesReturnNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    salesReturnEntity.VipID = loggingSessionInfo.UserID;
                    salesReturnEntity.ServicesType = 1;//�˻�
                    salesReturnEntity.DeliveryType = 1;//����ͻ�;
                    salesReturnEntity.OrderID = inoutInfo.order_id;
                    var inoutDetailInfo = inoutDetailList.FirstOrDefault();
                    if (inoutDetailInfo != null)
                    {
                        salesReturnEntity.ItemID = inoutDetailInfo.item_id;
                        salesReturnEntity.SkuID = inoutDetailInfo.sku_id;
                    }
                    salesReturnEntity.Qty = 0;
                    salesReturnEntity.ActualQty = 0;
                    if (inoutInfo != null)
                    {
                        salesReturnEntity.UnitID = inoutInfo.sales_unit_id;
                        //salesReturnEntity.UnitName = para.UnitName;
                        //salesReturnEntity.UnitTel = para.UnitTel;
                        salesReturnEntity.Address = inoutInfo.Field4;
                        salesReturnEntity.Contacts = inoutInfo.Field14 != null ? inoutInfo.Field14 : "";
                        salesReturnEntity.Phone = inoutInfo.Field6 != null ? inoutInfo.Field6 : "";
                    }
                    salesReturnEntity.Reason = "ȡ������";
                    //if (inoutInfo.actual_amount - inoutInfo.DeliveryAmount > 0)
                    if (inoutInfo.actual_amount > 0)
                        salesReturnEntity.Status = 6; //����ɣ����˿
                    else
                        salesReturnEntity.Status = 7; //����ɣ����˿
                    salesReturnEntity.CustomerID = loggingSessionInfo.ClientID;
                    salesReturnBLL.Create(salesReturnEntity);

                    string userName = string.Empty;//����������
                    if (userType == 0)//��Ա����
                    {
                        vipEntity = vipBll.GetByID(loggingSessionInfo.UserID);
                        userName = vipEntity != null ? vipEntity.VipName : "";
                    }
                    else//��̨�û�����
                        userName = loggingSessionInfo.CurrentUser.User_Name;
                    historyEntity = new T_SalesReturnHistoryEntity()
                    {
                        SalesReturnID = salesReturnEntity.SalesReturnID,
                        OperationType = 14,
                        OperationDesc = "ȡ������",
                        OperatorID = loggingSessionInfo.UserID,
                        HisRemark = "ȡ������",
                        OperatorName = userName,
                        OperatorType = userType   //0=��Ա;1=�����û�
                    };
                    historyBLL.Create(historyEntity);
                    #endregion

                    #region �����˿
                    //if (inoutInfo.actual_amount - inoutInfo.DeliveryAmount > 0)
                    if (inoutInfo.actual_amount > 0)
                    {
                        T_RefundOrderEntity refundOrderEntity = new T_RefundOrderEntity()
                        {
                            RefundNo = DateTime.Now.ToString("yyyyMMddhhmmfff"),
                            VipID = inoutInfo.vip_no,
                            SalesReturnID = salesReturnEntity.SalesReturnID,
                            //ServicesType = 1,//�˻�
                            DeliveryType = 1,//����ͻ�
                            ItemID = inoutDetailInfo.item_id,
                            SkuID = inoutDetailInfo.sku_id,
                            Qty = 0,
                            ActualQty = 0,
                            UnitID = inoutInfo.sales_unit_id,
                            //salesReturnEntity.UnitName = para.UnitName;
                            //salesReturnEntity.UnitTel = para.UnitTel;
                            Address = inoutInfo.Field4,
                            Contacts = inoutInfo.Field14,
                            Phone = inoutInfo.Field6,
                            OrderID = inoutInfo.order_id,
                            PayOrderID = inoutInfo.paymentcenter_id,
                            RefundAmount = inoutInfo.total_amount,
                            ConfirmAmount = inoutInfo.total_amount,
                            //ActualRefundAmount = inoutInfo.actual_amount - inoutInfo.DeliveryAmount,//ʵ�˽��=ʵ����-�˷�
                            ActualRefundAmount = inoutInfo.actual_amount,//ʵ�˽��=ʵ����
                            Points = inoutInfo.pay_points == null ? 0 : Convert.ToInt32(inoutInfo.pay_points),
                            ReturnAmount = inoutInfo.ReturnAmount,
                            Amount = inoutInfo.VipEndAmount,
                            Status = 1,//���˿�
                            CustomerID = loggingSessionInfo.ClientID
                        };
                        refundOrderBll.Create(refundOrderEntity);
                    }
                    #endregion

                }
            }
            #endregion

            //��ͨ������洦��
            if (inoutInfo.order_reason_id == "2F6891A2194A4BBAB6F17B4C99A6C6F5")
            {
                inoutBll.SetStock(orderId, inoutDetailList, 2, loggingSessionInfo);
            }
            //�Ź�����������洦��
            if (inoutInfo.order_reason_id == "CB43DD7DD1C94853BE98C4396738E00C" || inoutInfo.order_reason_id == "671E724C85B847BDA1E96E0E5A62055A")
            {
                panicbuyingEventBLL.SetEventStock(orderId, inoutDetailList.ToList());
            }
            //���۶�����洦��
            if (inoutInfo.order_reason_id == "096419BFDF394F7FABFE0DFCA909537F")
            {
                panicbuyingEventBLL.SetKJEventStock(orderId, inoutDetailList.ToList());
            }
            //���������̿�洦��
            if (inoutInfo.data_from_id == "35" || inoutInfo.data_from_id == "36")
            {
                superRetailTraderItemMappingBll.DeleteSuperRetailTraderItemStock(inoutDetailList.ToList());
            }
        }

        /// <summary>
        /// ������Ʒ�������
        /// </summary>
        /// <param name="orderId">����ID</param>
        /// <param name="inoutDetailList">������ϸ</param>
        /// <param name="actionType">�������� 1=�ύ������2=ȡ������</param>
        /// <param name="loggingSessionInfo"></param>
        public void SetStock(string orderId, IList<InoutDetailInfo> inoutDetailList, int actionType, LoggingSessionInfo loggingSessionInfo)
        {
            var itemPropertyBLL = new T_Item_PropertyBLL(loggingSessionInfo);
            var skuPriceBLL = new T_Sku_PriceBLL(loggingSessionInfo);

            var inoutService = new InoutService(loggingSessionInfo);
            foreach (var item in inoutDetailList)
            {
                //��Ʒ�ܿ��
                var stockInfo = itemPropertyBLL.QueryByEntity(new T_Item_PropertyEntity() { item_id = item.item_id, prop_id = "34FF4445D39F49AD8174954D18BC1346" }, null).FirstOrDefault();
                if (stockInfo != null)
                {
                    decimal stock = decimal.Parse(stockInfo.prop_value);
                    if (actionType == 1)
                        stock -= item.enter_qty;
                    else if (actionType == 2)
                        stock += item.enter_qty;
                    stockInfo.prop_value = stock.ToString();
                    itemPropertyBLL.Update(stockInfo);
                }
                //��Ʒ������
                var salesCountInfo = itemPropertyBLL.QueryByEntity(new T_Item_PropertyEntity() { item_id = item.item_id, prop_id = "34FF4445D39F49AD8174954D18BC1347" }, null).FirstOrDefault();
                if (salesCountInfo != null)
                {
                    decimal salesCount = decimal.Parse(salesCountInfo.prop_value);
                    if (actionType == 1)
                        salesCount += item.enter_qty;
                    else if (actionType == 2)
                        salesCount -= item.enter_qty;

                    salesCountInfo.prop_value = salesCount.ToString();
                    itemPropertyBLL.Update(salesCountInfo);
                }
                //sku���
                var skuStockInfo = skuPriceBLL.QueryByEntity(new T_Sku_PriceEntity() { sku_id = item.sku_id, item_price_type_id = "77850286E3F24CD2AC84F80BC625859E", status = "1" }, null).FirstOrDefault();
                if (skuStockInfo != null)
                {
                    if (actionType == 1)
                        skuStockInfo.sku_price -= item.enter_qty;
                    else if (actionType == 2)
                        skuStockInfo.sku_price += item.enter_qty;

                    skuPriceBLL.Update(skuStockInfo);
                }
                //sku����
                var skuSalesCountInfo = skuPriceBLL.QueryByEntity(new T_Sku_PriceEntity() { sku_id = item.sku_id, item_price_type_id = "77850286E3F24CD2AC84F80BC625859f", status = "1" }, null).FirstOrDefault();
                if (skuSalesCountInfo != null)
                {
                    if (actionType == 1)
                        skuSalesCountInfo.sku_price += item.enter_qty;
                    else
                        skuSalesCountInfo.sku_price -= item.enter_qty;

                    skuPriceBLL.Update(skuSalesCountInfo);
                }
            }
        }
 

        /// <summary>
        /// ֧���ص�/�տ��������Ʒ����
        /// </summary>
        public void SetVirtualItem(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            var inoutBLL = new T_InoutBLL(loggingSessionInfo);
            var inoutInfo = this._currentDAO.GetByID(orderId);
            if (inoutInfo != null)
            {
                //����Ǿ����̶�����������ɺ󣬶���״̬�޸ĳ����״̬
                if (inoutInfo.data_from_id == "21")
                {
                    inoutInfo.Field7 = "700";
                    inoutInfo.status = "700";
                    inoutBLL.Update(inoutInfo);
                    InoutService inoutService = new InoutService(loggingSessionInfo);
                    T_VirtualItemTypeSettingBLL virtualItemTypeSettingBLL = new T_VirtualItemTypeSettingBLL(loggingSessionInfo);
                    VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
                    T_Inout_DetailBLL inoutDetailBLL = new T_Inout_DetailBLL(loggingSessionInfo);
                    var VipBLL=new VipBLL(loggingSessionInfo);
                    var inoutDetail = inoutService.GetInoutDetailInfoByOrderId(orderId).FirstOrDefault();
                    string itemId = inoutDetail.item_id;

                    var virtualItemTypeSettingInfo = virtualItemTypeSettingBLL.QueryByEntity(new T_VirtualItemTypeSettingEntity() { ItemId = itemId }, null).FirstOrDefault();
                    if (virtualItemTypeSettingInfo != null)
                    {
                        int objectTypeId = int.Parse(virtualItemTypeSettingInfo.ObjecetTypeId);
                        string objectNo = vipCardVipMappingBLL.BindVirtualItem(inoutInfo.vip_no, inoutInfo.VipCardCode, "", objectTypeId);
                        //����/ȯ�ı�ű��浽������ϸ
                        T_Inout_DetailEntity inoutDetailEntity = inoutDetailBLL.GetByID(inoutDetail.order_detail_id);
                        if (inoutDetailEntity != null)
                        {
                            inoutDetailEntity.Field10 = objectNo;
                            inoutDetailBLL.Update(inoutDetailEntity);
                        }
                    }
                    //��Col48��Ϊ1
                    var VipData = VipBLL.GetByID(inoutInfo.vip_no);
                    if (VipData != null)
                    {
                        VipData.Col48 = "1";
                        VipBLL.Update(VipData);
                    }
                    // �жϿͻ��Ƿ��Ƿ���Ǳ�ھ���������
                    var isCan = VipBLL.IsSetVipDealer(inoutInfo.vip_no);

                    if (isCan)
                    {
                        new RetailTraderBLL(loggingSessionInfo).CreatePrepRetailTrader(loggingSessionInfo, inoutInfo.vip_no); // ����Ǳ�ھ�����
                    }
                }
            }
        }
   
        /// <summary>
        /// ���㳬��������Ӷ�����
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderInfo"></param>
        public void CalculateSuperRetailTraderOrder(LoggingSessionInfo loggingSessionInfo,T_InoutEntity orderInfo)
        {
            
            if (orderInfo != null)
            {
                if (orderInfo.data_from_id == "35" || orderInfo.data_from_id == "36")
                {
                   


                        T_SuperRetailTraderBLL bllSuperRetailTrader = new T_SuperRetailTraderBLL(loggingSessionInfo);
                        DataSet dsAllFather = bllSuperRetailTrader.GetAllFather(orderInfo.sales_user);
                        if (dsAllFather != null && dsAllFather.Tables.Count > 0 && dsAllFather.Tables[0].Rows.Count > 0)
                        {

                            T_SuperRetailTraderProfitConfigBLL bllSuperRetailTraderProfitConfig = new T_SuperRetailTraderProfitConfigBLL(loggingSessionInfo);
                            T_SuperRetailTraderConfigBLL bllSuperRetailTraderConfig = new T_SuperRetailTraderConfigBLL(loggingSessionInfo);
                            T_SuperRetailTraderProfitDetailBLL bllSuperRetailTraderProfitDetail = new T_SuperRetailTraderProfitDetailBLL(loggingSessionInfo);

                            VipAmountBLL bllVipAmount = new VipAmountBLL(loggingSessionInfo);
                            VipAmountDetailBLL bllVipAmountDetail = new VipAmountDetailBLL(loggingSessionInfo);

                            var entitySuperRetailTraderProfitConfig = bllSuperRetailTraderProfitConfig.QueryByEntity(new T_SuperRetailTraderProfitConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0, Status = "10" }, null);
                            var entityConfig = bllSuperRetailTraderConfig.QueryByEntity(new T_SuperRetailTraderConfigEntity() { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).SingleOrDefault();
                            if (entityConfig != null && entitySuperRetailTraderProfitConfig != null)
                            {

                                //Ӷ�����
                                decimal SkuCommission = Convert.ToDecimal(entityConfig.SkuCommission) * Convert.ToDecimal(0.01);
                                //��Ʒ�������
                                decimal DistributionProfit = Convert.ToDecimal(entityConfig.DistributionProfit) * Convert.ToDecimal(0.01);


                                foreach (DataRow dr in dsAllFather.Tables[0].Rows)
                                {
                                    decimal amount = 0;
                                    string strAmountSourceId = string.Empty;
                                    T_SuperRetailTraderProfitConfigEntity singlProfitConfig = new T_SuperRetailTraderProfitConfigEntity();
                                    if (dr["level"].ToString() == "1")//Ӷ��
                                    {
                                        strAmountSourceId = "34";
                                        singlProfitConfig = entitySuperRetailTraderProfitConfig.Where(a => a.Level == Convert.ToInt16(dr["level"].ToString())).SingleOrDefault();
                                        if (singlProfitConfig != null)
                                        {
                                            if (singlProfitConfig.ProfitType == "Percent")
                                            {
                                                amount = Convert.ToDecimal(orderInfo.actual_amount) * SkuCommission;
                                            }
                                        }
                                    }
                                    else//����
                                    {
                                        strAmountSourceId = "33";
                                        singlProfitConfig = entitySuperRetailTraderProfitConfig.Where(a => a.Level == Convert.ToInt16(dr["level"].ToString())).SingleOrDefault();
                                        if (singlProfitConfig != null)
                                        {
                                            if (singlProfitConfig.ProfitType == "Percent")
                                            {
                                                amount = Convert.ToDecimal(orderInfo.actual_amount) * Convert.ToDecimal(singlProfitConfig.Profit) * Convert.ToDecimal(0.01);
                                            }
                                        }
                                    }
                                    if (amount > 0)
                                    {
                                        IDbTransaction tran = new JIT.CPOS.BS.DataAccess.Base.TransactionHelper(loggingSessionInfo).CreateTransaction();
                                        try
                                        {
                                            T_SuperRetailTraderProfitDetailEntity entitySuperRetailTraderProfitDetail = new T_SuperRetailTraderProfitDetailEntity()
                                            {
                                                SuperRetailTraderProfitConfigId = singlProfitConfig.SuperRetailTraderProfitConfigId,
                                                SuperRetailTraderID = new Guid(dr["SuperRetailTraderID"].ToString()),
                                                Level = Convert.ToInt16(dr["level"].ToString()),
                                                ProfitType = "Cash",
                                                Profit = amount,
                                                OrderType = "Order",
                                                OrderId = orderInfo.order_id,
                                                OrderDate = Convert.ToDateTime(orderInfo.order_date),
                                                VipId = orderInfo.vip_no,
                                                OrderActualAmount = orderInfo.actual_amount,
                                                SalesId = new Guid(orderInfo.sales_user),
                                                OrderNo = orderInfo.order_no,
                                                CustomerId = loggingSessionInfo.ClientID
                                            };


                                            bllSuperRetailTraderProfitDetail.Create(entitySuperRetailTraderProfitDetail, (SqlTransaction)tran);


                                            VipAmountDetailEntity entityVipAmountDetail = new VipAmountDetailEntity();
                                            VipAmountEntity entityVipAmount = new VipAmountEntity();
                                            entityVipAmountDetail = new VipAmountDetailEntity
                                            {
                                                VipAmountDetailId = Guid.NewGuid(),
                                                VipId = dr["SuperRetailTraderID"].ToString(),
                                                Amount = amount,
                                                UsedReturnAmount = 0,
                                                EffectiveDate = DateTime.Now,
                                                DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
                                                AmountSourceId = strAmountSourceId,
                                                ObjectId = orderInfo.order_id,
                                                CustomerID = loggingSessionInfo.ClientID,
                                                Reason = "����������"
                                            };
                                            bllVipAmountDetail.Create(entityVipAmountDetail, (SqlTransaction)tran);

                                            entityVipAmount = bllVipAmount.QueryByEntity(new VipAmountEntity() { VipId = dr["SuperRetailTraderID"].ToString(), IsDelete = 0, CustomerID = loggingSessionInfo.ClientID }, null).SingleOrDefault();
                                            if (entityVipAmount == null)
                                            {
                                                entityVipAmount = new VipAmountEntity
                                                {
                                                    VipId = dr["SuperRetailTraderID"].ToString(),
                                                    BeginAmount = 0,
                                                    InAmount = amount,
                                                    OutAmount = 0,
                                                    EndAmount = amount,
                                                    TotalAmount = amount,
                                                    BeginReturnAmount = 0,
                                                    InReturnAmount = 0,
                                                    OutReturnAmount = 0,
                                                    ReturnAmount = 0,
                                                    ImminentInvalidRAmount = 0,
                                                    InvalidReturnAmount = 0,
                                                    ValidReturnAmount = 0,
                                                    TotalReturnAmount = 0,
                                                    IsLocking = 0,
                                                    CustomerID = loggingSessionInfo.ClientID,
                                                    VipCardCode = ""

                                                };
                                                bllVipAmount.Create(entityVipAmount, tran);
                                            }
                                            else
                                            {

                                                entityVipAmount.InReturnAmount = (entityVipAmount.InReturnAmount == null ? 0 : entityVipAmount.InReturnAmount.Value) + amount;
                                                entityVipAmount.TotalReturnAmount = (entityVipAmount.TotalReturnAmount == null ? 0 : entityVipAmount.TotalReturnAmount.Value) + amount;

                                                entityVipAmount.ValidReturnAmount = (entityVipAmount.ValidReturnAmount == null ? 0 : entityVipAmount.ValidReturnAmount.Value) + amount;
                                                entityVipAmount.ReturnAmount = (entityVipAmount.ReturnAmount == null ? 0 : entityVipAmount.ReturnAmount.Value) + amount;

                                                bllVipAmount.Update(entityVipAmount);
                                            }
                                            tran.Commit();
                                        }
                                        catch (Exception)
                                        {
                                            tran.Rollback();
                                            throw;
                                        }
                                    }

                                }

                            }
                        }
                }
            }
        }
        /// <summary>
        /// �������ۻ�Ա������
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderInfo"></param>
        public void CalculateSalesVipCardOrder(LoggingSessionInfo loggingSessionInfo, T_InoutEntity orderInfo)
        {
            if (orderInfo != null)
            {

                if (orderInfo.Field17 != null && orderInfo.Field18 != null && orderInfo.Field17.Length > 0 && orderInfo.Field18.Length > 0)
                {


                    VipBLL bllVip = new VipBLL(loggingSessionInfo);

                    T_Inout_DetailBLL bllInoutDetail = new T_Inout_DetailBLL(loggingSessionInfo);
                    T_VirtualItemTypeSettingBLL bllVirtualItemTypeSetting = new T_VirtualItemTypeSettingBLL(loggingSessionInfo);

                    var entityInoutDetail = bllInoutDetail.QueryByEntity(new T_Inout_DetailEntity() { order_id = orderInfo.order_id }, null).FirstOrDefault();
                    var vipCardType = bllVirtualItemTypeSetting.QueryByEntity(new T_VirtualItemTypeSettingEntity() { SkuId = entityInoutDetail.sku_id, IsDelete = 0 }, null).FirstOrDefault();
                    if (vipCardType != null)
                    {
                        VipCardUpgradeRuleBLL bllVipCardUpgradeRule = new VipCardUpgradeRuleBLL(loggingSessionInfo);

                        int intVipCardTypeID = Convert.ToInt32(vipCardType.ObjecetTypeId);
                        var entityVipCardUpgradeRule = bllVipCardUpgradeRule.QueryByEntity(new VipCardUpgradeRuleEntity() { VipCardTypeID = intVipCardTypeID, IsPurchaseUpgrade = 1, IsDelete = 0 }, null).SingleOrDefault();
                        #region
                        //if (entityVipCardUpgradeRule != null)
                        //{
                        //    VipCardGradeChangeLogEntity entityVipCardGradeChangeLog = new VipCardGradeChangeLogEntity();
                        //    VipCardStatusChangeLogEntity entityVipCardStatusChangeLog = new VipCardStatusChangeLogEntity();

                        //    VipCardGradeChangeLogBLL bllVipCardGradeChangeLog = new VipCardGradeChangeLogBLL(loggingSessionInfo);
                        //    VipCardStatusChangeLogBLL bllVipCardStatusChangeLog = new VipCardStatusChangeLogBLL(loggingSessionInfo);
                        //    VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);

                        //    //��Ա�ȼ��ı��Լ�����־
                        //    DataSet dsVipInfo = bllVip.GetVipCardLevel(orderInfo.vip_no, loggingSessionInfo.ClientID);
                        //    if(dsVipInfo.Tables.Count>0 && dsVipInfo.Tables[0].Rows.Count>0)
                        //    {
                        //        //��Ա����
                        //        vipCardVipMappingBLL.BindVirtualItem(orderInfo.vip_no, orderInfo.VipCardCode, "", intVipCardTypeID);
                        //        //��־
                        //        entityVipCardGradeChangeLog = new VipCardGradeChangeLogEntity()
                        //        {
                        //            ChangeLogID=Guid.NewGuid().ToString(),
                        //            VipCardUpgradeRuleId=entityVipCardUpgradeRule.VipCardUpgradeRuleId,
                        //            OrderType = "SalesCard",
                        //            OrderId=orderInfo.order_id,
                        //            VipCardID = dsVipInfo.Tables[0].Rows[0]["VipCardID"].ToString(),
                        //            ChangeBeforeVipCardID = dsVipInfo.Tables[0].Rows[0]["VipCardID"].ToString(),
                        //            ChangeBeforeGradeID = Convert.ToInt32(dsVipInfo.Tables[0].Rows[0]["VipCardTypeID"].ToString()),
                        //            NowGradeID = intVipCardTypeID,
                        //            ChangeReason="Upgrade",
                        //            OperationType = 2,
                        //            ChangeTime=DateTime.Now,
                        //            UnitID=orderInfo.sales_unit_id,
                        //            OperationUserID=orderInfo.sales_user
                        //        };
                        //        bllVipCardGradeChangeLog.Create(entityVipCardGradeChangeLog);
                        //    }
                        //}
                        #endregion
                        //�������
                        VipCardProfitRuleBLL bllVipCardProfitRule = new VipCardProfitRuleBLL(loggingSessionInfo);
                        var entityVipCardProfitRule = bllVipCardProfitRule.QueryByEntity(new VipCardProfitRuleEntity() { VipCardTypeID = intVipCardTypeID, IsDelete = 0 }, null);
                        if (entityVipCardProfitRule != null)
                        {
                            VipAmountBLL bllVipAmount = new VipAmountBLL(loggingSessionInfo);
                            VipAmountDetailBLL bllVipAmountDetail = new VipAmountDetailBLL(loggingSessionInfo);
                            VipAmountEntity entityVipAmount = new VipAmountEntity();

                            foreach (var ProfitRule in entityVipCardProfitRule)
                            {
                                decimal amount = 0;
                                string strAmountSourceId = string.Empty;
                                string strVipId = string.Empty;
                                
                                if (ProfitRule.ProfitOwner == "Employee")
                                 {
                                     strAmountSourceId = "37";
                                     strVipId = orderInfo.sales_user;
                                 }
                                 if (ProfitRule.ProfitOwner == "Unit")
                                 {
                                     strAmountSourceId = "40";
                                     strVipId = orderInfo.sales_unit_id;
                                 }
                                 if (ProfitRule.IsApplyAllUnits == 0)
                                 {
                                     VipCardProfitRuleUnitMappingBLL bllVipCardProfitRuleUnitMapping = new VipCardProfitRuleUnitMappingBLL(loggingSessionInfo);
                                     var vipCardProfitRuleUnitMapping = bllVipCardProfitRuleUnitMapping.QueryByEntity(new VipCardProfitRuleUnitMappingEntity() { CardBuyToProfitRuleId = ProfitRule.CardBuyToProfitRuleId, UnitID = orderInfo.sales_unit_id, IsDelete = 0, CustomerID = loggingSessionInfo.ClientID }, null).SingleOrDefault();
                                     if (vipCardProfitRuleUnitMapping != null)
                                     {
                                         amount = (decimal)ProfitRule.FirstCardSalesProfitPct * (decimal)orderInfo.actual_amount * (decimal)0.01;
                                     }
                                     else
                                     {
                                         continue;
                                     }
                                 }
                                 
                                 amount = (decimal)ProfitRule.FirstCardSalesProfitPct * (decimal)orderInfo.actual_amount * (decimal)0.01;
                                 if (amount > 0)
                                 {
                                     IDbTransaction tran = new JIT.CPOS.BS.DataAccess.Base.TransactionHelper(loggingSessionInfo).CreateTransaction();

                                     VipAmountDetailEntity entityVipAmountDetail = new VipAmountDetailEntity
                                     {
                                         VipAmountDetailId = Guid.NewGuid(),
                                         VipId = strVipId,
                                         Amount = amount,
                                         UsedReturnAmount = 0,
                                         EffectiveDate = DateTime.Now,
                                         DeadlineDate = Convert.ToDateTime("9999-12-31 23:59:59"),
                                         AmountSourceId = strAmountSourceId,
                                         ObjectId = orderInfo.order_id,
                                         CustomerID = loggingSessionInfo.ClientID,
                                         Reason = "����������"
                                     };
                                     bllVipAmountDetail.Create(entityVipAmountDetail, (SqlTransaction)tran);

                                     entityVipAmount = new VipAmountEntity
                                       {
                                           VipId = strVipId,
                                           BeginAmount = 0,
                                           InAmount = amount,
                                           OutAmount = 0,
                                           EndAmount = amount,
                                           TotalAmount = amount,
                                           BeginReturnAmount = 0,
                                           InReturnAmount = 0,
                                           OutReturnAmount = 0,
                                           ReturnAmount = 0,
                                           ImminentInvalidRAmount = 0,
                                           InvalidReturnAmount = 0,
                                           ValidReturnAmount = 0,
                                           TotalReturnAmount = 0,
                                           IsLocking = 0,
                                           CustomerID = loggingSessionInfo.ClientID,
                                           VipCardCode = ""

                                       };
                                     bllVipAmount.Create(entityVipAmount, tran);
                                 }
                                 else
                                 {

                                     entityVipAmount.InReturnAmount = (entityVipAmount.InReturnAmount == null ? 0 : entityVipAmount.InReturnAmount.Value) + amount;
                                     entityVipAmount.TotalReturnAmount = (entityVipAmount.TotalReturnAmount == null ? 0 : entityVipAmount.TotalReturnAmount.Value) + amount;

                                     entityVipAmount.ValidReturnAmount = (entityVipAmount.ValidReturnAmount == null ? 0 : entityVipAmount.ValidReturnAmount.Value) + amount;
                                     entityVipAmount.ReturnAmount = (entityVipAmount.ReturnAmount == null ? 0 : entityVipAmount.ReturnAmount.Value) + amount;
                                     bllVipAmount.Update(entityVipAmount);
                                 }

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ���ݻ�Ա��ȡ���ѽ����Ϣ
        /// </summary>
        /// <param name="VipID"></param>
        /// <returns></returns>
        public decimal GetVipSumAmount(string VipID)
        {
            return _currentDAO.GetVipSumAmount(VipID);
        }
        /// <summary>
        /// ��ȡ��ǰ���Ƿ��ѹ���
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="VipID"></param>
        /// <param name="SkuID"></param>
        /// <returns></returns>
        public int GetVirtualItemStatus(string CustomerID, string VipID, string SkuID)
        {
            return _currentDAO.GetVirtualItemStatus(CustomerID, VipID,SkuID);
        }
    }
}
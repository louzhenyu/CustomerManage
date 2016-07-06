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
using System.Text;
using System.Data;
using System.Reflection;
using CPOS.BS.BLL;
using CPOS.BS.DataAccess;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_InoutBLL
    {
        private BasicUserInfo CurrentUserInfo;
        private T_InoutDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_InoutBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new T_InoutDAO(pUserInfo);
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(T_InoutEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity, pTran);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public T_InoutEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public T_InoutEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity, pTran);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(T_InoutEntity pEntity)
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_InoutEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_InoutEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(T_InoutEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(T_InoutEntity[] pEntities)
        {
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            _currentDAO.Delete(pIDs, pTran);
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public T_InoutEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return _currentDAO.Query(pWhereConditions, pOrderBys);
        }

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<T_InoutEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public T_InoutEntity[] QueryByEntity(T_InoutEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<T_InoutEntity> PagedQueryByEntity(T_InoutEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryByEntity(pQueryEntity, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion
        /// <summary>
        /// 获取主订单信息
        /// </summary>
        /// <returns></returns>
        public T_InoutEntity GetInoutInfo(string orderId, LoggingSessionInfo pUserInfo)
        {
            var inoutInfo = this._currentDAO.GetByID(orderId);
            if (inoutInfo != null)
            {
                var vipIntegralDetailBll = new VipIntegralDetailBLL(pUserInfo);
                var vipBll = new VipBLL(pUserInfo);
                var tOrderCouponMappingBll = new TOrderCouponMappingBLL(pUserInfo);
                var vipAmountDetailBll = new VipAmountDetailBLL(pUserInfo);
                var deliveryBll = new TOrderCustomerDeliveryStrategyMappingBLL(pUserInfo);
                //使用积分
                inoutInfo.pay_points = Math.Abs(vipIntegralDetailBll.GetVipIntegralByOrder(orderId, inoutInfo.vip_no));
                if (inoutInfo.pay_points > 0)
                {
                    //decimal integralAmountPre = vipBll.GetIntegralAmountPre(pUserInfo.ClientID);//获取积分金额比例
                    //积分抵扣
                    //inoutInfo.IntegralAmount = inoutInfo.pay_points.Value * (integralAmountPre > 0 ? integralAmountPre : 0.01M);
                    //积分抵扣
                    inoutInfo.IntegralAmount = vipBll.GetAmountByIntegralPer(pUserInfo.ClientID, inoutInfo.pay_points.Value);
                }
                else
                    inoutInfo.IntegralAmount = 0;//积分抵扣
                //优惠券抵扣
                var couponParValue = tOrderCouponMappingBll.GetCouponParValue(orderId);
                inoutInfo.CouponAmount = couponParValue;

                //使用的账户余额
                inoutInfo.VipEndAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId, inoutInfo.vip_no, 1));
                //使用的返现金额
                inoutInfo.ReturnAmount = Math.Abs(vipAmountDetailBll.GetVipAmountByOrderId(orderId, inoutInfo.vip_no, 13));
                //配送费 
                inoutInfo.DeliveryAmount = deliveryBll.GetDeliverAmount(orderId);
            }
            return inoutInfo;
        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="pOrderId"></param>
        /// <param name="pUserInfo"></param>
        /// <returns></returns>
        public GetOrderDetailRD GetInoutDetail(string pOrderId, LoggingSessionInfo pUserInfo)
        {
            GetOrderDetailRD rd = new GetOrderDetailRD();
            string orderId = pOrderId;
            rd.OrderListInfo = new OrderListInfo();

            #region 获取订单列表

            T_InoutBLL orderBll = new T_InoutBLL(pUserInfo);
            var orderList = orderBll.QueryByEntity(new T_InoutEntity()
            {
                order_id = orderId
            }, null);
            rd.OrderListInfo.reserveDay = orderList[0].reserveDay;
            rd.OrderListInfo.reserveQuantum = orderList[0].reserveQuantum;
            rd.OrderListInfo.reserveQuantumID = orderList[0].reserveQuantumID;
            //   rd.OrderListInfo.reserveTime=orderList[0].reserveTime;

            #endregion

            #region 获取会员信息

            string vipNo = orderList[0].vip_no;
            VipBLL vipBll = new VipBLL(pUserInfo);

            var vipList = vipBll.QueryByEntity(new VipEntity()
            {
                VIPID = vipNo
            }, null);

            #endregion

            #region 获取配方式

            string deliveryId = orderList[0].Field8;
            DeliveryBLL deliverBll = new DeliveryBLL(pUserInfo);

            var deliverList = deliverBll.QueryByEntity(new DeliveryEntity()
            {
                DeliveryId = deliveryId
            }, null);

            #endregion

            #region 获取门店信息

            string storeId = orderList[0].sales_unit_id;
            if (!string.IsNullOrEmpty(orderList[0].purchase_unit_id))//如果有发货门店，则显示发货门店信息
                storeId = orderList[0].purchase_unit_id;
            TInoutBLL tInoutBll = new TInoutBLL(pUserInfo);
            DataSet storeDs = tInoutBll.GetStoreInfo(storeId);
            rd.OrderListInfo.StoreID = storeId;

            #endregion

            //配送商
            string carrierId = orderList[0].carrier_id;

            if (!string.IsNullOrEmpty(carrierId))
            {
                //配送方式 1.送货到家;2.到店提货
                if (deliveryId == "1")
                {
                    var logisticsCompanyBLL = new T_LogisticsCompanyBLL(pUserInfo);
                    Guid m_carrierId = Guid.Parse(carrierId);
                    var logCompInfo = logisticsCompanyBLL.GetByID(m_carrierId);
                    if (logCompInfo != null)
                    {
                        rd.OrderListInfo.CarrierID = carrierId;
                        rd.OrderListInfo.CarrierName = logCompInfo.LogisticsName;
                    }
                }
                else if (deliveryId == "2")
                {
                    var unitBLL = new t_unitBLL(pUserInfo);
                    var unitInfo = unitBLL.GetByID(carrierId);
                    if (unitInfo != null)
                    {
                        rd.OrderListInfo.CarrierID = carrierId;
                        rd.OrderListInfo.CarrierName = unitInfo.unit_name;
                    }
                }
            }
            rd.OrderListInfo.CourierNumber = orderList[0].Field2;//配送单号
            rd.OrderListInfo.Invoice = orderList[0].Field19 == null ? "" : orderList[0].Field19;     //发票信息
            if (vipList.Count() > 0)
            {
                rd.OrderListInfo.VipID = vipList[0].VIPID;
                rd.OrderListInfo.Phone = vipList[0].Phone;
                rd.OrderListInfo.UserName = vipList[0].VipName;
                rd.OrderListInfo.VipRealName = vipList[0].VipRealName;
                rd.OrderListInfo.VipLevelDesc = vipList[0].VipLevelDesc;
                rd.OrderListInfo.VipCode = vipList[0].VipCode;
                rd.OrderListInfo.Email = vipList[0].Email;
                rd.OrderListInfo.VipLevel = Convert.ToInt32(vipList[0].VipLevel);
            }

            if (storeDs.Tables[0].Rows.Count > 0)
            {
                rd.OrderListInfo.StoreName = storeDs.Tables[0].Rows[0]["unit_name"].ToString();
                rd.OrderListInfo.StoreAddress = storeDs.Tables[0].Rows[0]["unit_address"].ToString();
                rd.OrderListInfo.StoreTel = storeDs.Tables[0].Rows[0]["unit_tel"].ToString();
            }

            if (orderList.Count() > 0)
            {
                rd.OrderListInfo.discount_rate = orderList[0].discount_rate ?? 100;//订单折扣
                rd.OrderListInfo.OrderID = orderList[0].order_id;
                rd.OrderListInfo.OrderCode = orderList[0].order_no;
                rd.OrderListInfo.OrderDate = orderList[0].order_date;
                rd.OrderListInfo.ReceiverName = orderList[0].Field14; //收件人             
                rd.OrderListInfo.TotalQty = Convert.ToDecimal(orderList[0].total_qty);
                rd.OrderListInfo.TotalAmount = Convert.ToDecimal(orderList[0].total_amount);
                rd.OrderListInfo.Remark = orderList[0].remark;
                rd.OrderListInfo.Status = orderList[0].status;
                rd.OrderListInfo.OrderStatus = int.Parse(orderList[0].Field7);
                rd.OrderListInfo.StatusDesc = orderList[0].status_desc;
                rd.OrderListInfo.DeliveryAddress = orderList[0].Field4;
                rd.OrderListInfo.DeliveryTime = orderList[0].Field9;
                rd.OrderListInfo.reserveDay = orderList[0].reserveDay;
                rd.OrderListInfo.ClinchTime = orderList[0].create_time;
                rd.OrderListInfo.ReceiptTime = orderList[0].accpect_time;
                rd.OrderListInfo.CouponsPrompt = orderList[0].Field16;
                rd.OrderListInfo.DeliveryID = orderList[0].Field8;
                rd.OrderListInfo.IsPayment = orderList[0].Field1;
                rd.OrderListInfo.ReceivePoints = orderList[0].receive_points;
                rd.OrderListInfo.PaymentTime = orderList[0].Field1 == "1" ? orderList[0].complete_date : null;
                rd.OrderListInfo.ActualDecimal = orderList[0].actual_amount ?? 0;

                rd.OrderListInfo.PaymentTypeCode = orderList[0].Payment_Type_Code;
                rd.OrderListInfo.PaymentTypeName = orderList[0].Payment_Type_Name;
                rd.OrderListInfo.CreateBy = orderList[0].create_user_id;

                var sysvipbll = new SysVipSourceBLL(pUserInfo);
                var orderReasonTypeBll = new T_Order_Reason_TypeBLL(pUserInfo);
                rd.OrderListInfo.SysVipSource = sysvipbll.GetByID(orderList[0].data_from_id);
                rd.OrderListInfo.OrderReasonTypeInfo = orderReasonTypeBll.GetByID(orderList[0].order_reason_id);

                var deliveryBll = new TOrderCustomerDeliveryStrategyMappingBLL(pUserInfo);
                rd.OrderListInfo.DeliveryAmount = deliveryBll.GetDeliverAmount(orderId);//配送费 add by henry***

                if (!string.IsNullOrEmpty(orderList[0].Field15) && orderList[0].Field15 != "0") //是否是团购商品 add by Henry 2014-12-22
                    rd.OrderListInfo.IsEvent = 1;   //团购商品
                else
                    rd.OrderListInfo.IsEvent = 0;   //普通商品

                #region update by changjian.tian

                rd.OrderListInfo.Mobile = orderList[0].Field6; //配送联系电话 
                rd.OrderListInfo.DeliveryRemark = orderList[0].remark;

                rd.OrderListInfo.IsEvaluation = orderList[0].IsEvaluation == null ? 0 : orderList[0].IsEvaluation.Value;//评论
                #endregion
            }

            if (deliverList.Count() > 0)
            {
                rd.OrderListInfo.DeliveryName = deliverList[0].DeliveryName;
            }

            T_Inout_DetailBLL orderDetailBll = new T_Inout_DetailBLL(pUserInfo);
            //退换货Bll实例化
            T_SalesReturnBLL salesReturnBll = new T_SalesReturnBLL(pUserInfo);

            var orderDetailList = orderDetailBll.QueryByEntity(new T_Inout_DetailEntity()
            {
                order_id = orderId
            }, null);

            var inoutService = new InoutService(pUserInfo);

            #region 根据订单ID获取订单明细

            var ds = inoutService.GetOrderDetailByOrderId(orderId);

            #endregion

            #region 获取订单详细列表中的商品规格

            var ggDs = inoutService.GetInoutDetailGgByOrderId(orderId);

            #endregion

            if (ds.Tables[0].Rows.Count > 0)
            {
                string ItemIdList =
                    ds.Tables[0].AsEnumerable().Aggregate("", (x, j) =>
                    {
                        x += string.Format("'{0}',", j["item_id"].ToString());
                        return x;
                    }).Trim(',');

                TInoutDetailBLL tInoutDetailBll = new TInoutDetailBLL(pUserInfo);
                //获取商品的图片
                DataSet imageDs = tInoutDetailBll.GetOrderDetailImageList(ItemIdList);

                var tmp = ds.Tables[0].AsEnumerable().Select(t => new OrderDetailEntity()
                {
                    IsItemOnlyBuyOnce = t["IsItemOnlyBuyOnce"].ToString(),
                    IsItemGoToShop = t["IsItemGoToShop"].ToString(),
                    ItemID = t["item_id"].ToString(),
                    ItemName = t["item_name"].ToString(),
                    SkuID = t["sku_id"].ToString(),
                    SalesReturnFlag = salesReturnBll.CheckSalesReturn(orderId, t["sku_id"].ToString()),//是否可申请退换货
                    GG = ggDs.Tables[0].AsEnumerable()
                            .Where(tt => tt["sku_id"].ToString() == t["sku_id"].ToString())
                            .Select(tt => new GuiGeInfo
                            {
                                PropName1 = tt["prop_1_name"].ToString(),
                                PropDetailName1 = tt["prop_1_detail_name"].ToString(),
                                PropName2 = tt["prop_2_name"].ToString(),
                                PropDetailName2 = tt["prop_2_detail_name"].ToString(),
                                PropName3 = tt["prop_3_name"].ToString(),
                                PropDetailName3 = tt["prop_3_detail_name"].ToString(),
                                PropName4 = tt["prop_4_name"].ToString(),
                                PropDetailName4 = tt["prop_4_detail_name"].ToString(),
                                PropName5 = tt["prop_5_name"].ToString(),
                                PropDetailName5 = tt["prop_5_detail_name"].ToString()
                            }).FirstOrDefault(),
                    SalesPrice = Convert.ToDecimal(t["enter_price"]),
                    //DiscountRate = Convert.ToDecimal(t["discount_rate"]),
                    DiscountRate = Convert.ToDecimal(t["order_discount_rate"]),
                    ItemCategoryName = t["itemCategoryName"].ToString(),
                    BeginDate = t["Field1"].ToString(),
                    EndDate = t["Field2"].ToString(),
                    DayCount = Convert.ToInt32(t["DayCount"]),
                    Qty = Convert.ToDecimal(t["enter_qty"]),
                    ImageInfo =
                        imageDs.Tables[0].AsEnumerable()
                            .Where(c => c["ObjectId"].ToString() == t["item_id"].ToString())
                            .OrderBy(c => c["displayIndex"])
                            .Select(c => new OrderDetailImage
                            {
                                ImageID = c["imageId"].ToString(),
                                ImageUrl = ImagePathUtil.GetImagePathStr(c["imageUrl"].ToString(), "240")
                            }).ToArray(),
                    IfService = Convert.ToInt32(t["IfService"]),
                    Enter_Price = decimal.Parse(t["enter_price"].ToString()),
                    Enter_Amount = decimal.Parse(t["enter_amount"].ToString()),
                    Remark = t["remark"].ToString(),
                    ItemCode = t["item_code"].ToString()
                });
                rd.OrderListInfo.OrderDetailInfo = tmp.ToArray();
            }
            string IsItemGoToShop = "0";
            var gotoShopCount = rd.OrderListInfo.OrderDetailInfo.Where(p => p.IsItemGoToShop == "1").Count();
            if (gotoShopCount > 0)
            {
                IsItemGoToShop = "1";
            }
            rd.IsItemGoToShop = IsItemGoToShop;
            var vipIntegralDetailBll = new VipIntegralDetailBLL(pUserInfo);
            rd.OrderListInfo.UseIntegralToAmount = vipBll.GetAmountByIntegralPer(CurrentUserInfo.ClientID, rd.OrderListInfo.OrderIntegral);

            var tOrderCouponMappingBll = new TOrderCouponMappingBLL(pUserInfo);

            var couponParValue = tOrderCouponMappingBll.GetCouponParValue(orderId);
            rd.OrderListInfo.CouponAmount = couponParValue;

            return rd;
        }

    }
}
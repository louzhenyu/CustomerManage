/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/13 13:44:18
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
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.Order.Response;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class TInOutStatusNodeBLL
    {
        #region 获取订单下一个状态集合 Jermyn201403013
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="paymentTypeId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <returns></returns>
        public IList<TInOutStatusNodeEntity> GetOrderStatusList(string orderId, string paymentTypeId, string deliveryMethodId = "1")
        {
            DataSet ds = new DataSet();

            //jifeng.cao 20140416
            if (string.IsNullOrEmpty(deliveryMethodId))
            {
                deliveryMethodId = "1";
            }

            ds = _currentDAO.GetOrderStatusList(orderId, paymentTypeId, deliveryMethodId);
            IList<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<TInOutStatusNodeEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region 订单支付成功修改状态 Jermyn20140313
        /// <summary>
        /// 订单支付成功修改状态
        /// </summary>
        /// <param name="orderId">订单标识</param>
        /// <param name="strError">错误提示</param>
        /// <param name="ChannelId">支付渠道标识</param>
        /// <returns></returns>
        public bool SetOrderPayment(string orderId, out string strError, string ChannelId = null, string SerialPay = null)
        //public bool SetOrderPayment(string orderId, out string strError, string ChannelId = null)
        {
            MarketSendLogBLL logSerer = new MarketSendLogBLL(this.CurrentUserInfo);
            MarketSendLogEntity logInfo = new MarketSendLogEntity();
            logInfo.LogId = BaseService.NewGuidPub();
            logInfo.VipId = "System";
            logInfo.MarketEventId = orderId;
            logInfo.Phone = ChannelId;
            logInfo.SendTypeId = "3";
            try
            {
                bool bReturn = this._currentDAO.SetOrderPayment(orderId, out strError, ChannelId);
                if (bReturn)
                {
                    //算积分
                    //var bll = new JIT.CPOS.BS.BLL.InoutService(this.CurrentUserInfo);
                    //var orderInfo = bll.GetInoutInfoById(orderId);
                    //VipIntegralBLL vipIntegralServer = new VipIntegralBLL(this.CurrentUserInfo);

                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = string.Format("SetOrderPayment-参数: sourceId={0},customerId={1},vipId={2},orderId={3}", 1, this.CurrentUserInfo.CurrentUser.customer_id, orderInfo.vip_no, orderId)
                    //});
                    //vipIntegralServer.ProcessPoint(1, this.CurrentUserInfo.CurrentUser.customer_id, orderInfo.vip_no, orderId);

                    this._currentDAO.OrderPayCallBack(orderId, SerialPay, this.CurrentUserInfo.ClientID, Convert.ToInt32(ChannelId));

                    strError = "成功.";
                    logInfo.TemplateContent = strError;
                    logSerer.Create(logInfo);

                    //记录日志  qianzhi 2014-03-17
                    //var inoutStatus = new TInoutStatusBLL(CurrentUserInfo);
                    //TInoutStatusEntity info = new TInoutStatusEntity();
                    //info.InoutStatusID = Guid.Parse(Utils.NewGuid());
                    //info.OrderID = orderId;
                    //info.CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                    //info.OrderStatus = 10000; //支付方式
                    //info.Remark = "支付成功";
                    //info.StatusRemark = "订单支付成功[操作人:" + CurrentUserInfo.CurrentUser.User_Name + "]";
                    //inoutStatus.Create(info);

                    #region 处理虚拟商品的绑定

                    var inoutBLL = new T_InoutBLL(CurrentUserInfo);
                    var inoutInfo = inoutBLL.GetByID(orderId);
                    if (inoutInfo != null)
                    {
                        //如果是经销商订单，付款完成后，订单状态修改成完成状态
                        if (inoutInfo.data_from_id == "21")
                        {
                            inoutInfo.Field7 = "700";
                            inoutInfo.status = "700";
                            inoutBLL.Update(inoutInfo);
                            InoutService inoutService = new InoutService(CurrentUserInfo);
                            T_VirtualItemTypeSettingBLL virtualItemTypeSettingBLL = new T_VirtualItemTypeSettingBLL(CurrentUserInfo);
                            VipCardVipMappingBLL vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);

                            var inoutDetail = inoutService.GetInoutDetailInfoByOrderId(orderId).FirstOrDefault();
                            string itemId = inoutDetail.item_id;

                            var virtualItemTypeSettingInfo = virtualItemTypeSettingBLL.QueryByEntity(new T_VirtualItemTypeSettingEntity() { ItemId = itemId }, null).FirstOrDefault();
                            if (virtualItemTypeSettingInfo != null)
                            {
                                int objectTypeId = int.Parse(virtualItemTypeSettingInfo.ObjecetTypeId);
                                vipCardVipMappingBLL.BindVirtualItem(inoutInfo.vip_no, inoutInfo.VipCardCode, "", objectTypeId);
                            }
                        }
                    }


                    #endregion


                    return true;
                }
                return bReturn;
            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetOrderPayment-失败: {0}", strError)
                });
                logInfo.TemplateContent = strError.Substring(0, 100);
                logInfo.IsSuccess = 0;
                logSerer.Create(logInfo);
                return false;
            }
        }


        #endregion

        #region 获取对应客户的全部订单状态 jifeng.cao 20140319
        /// <summary>
        /// 获取对应客户的全部订单状态
        /// </summary>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        public IList<string> GetAllOrderStatus(string paymentTypeId)
        {
            DataSet ds = _currentDAO.GetAllOrderStatus(paymentTypeId);
            IList<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<TInOutStatusNodeEntity>(ds.Tables[0]);
            }

            //订单值集合
            IList<string> strList = new List<string>();

            foreach (TInOutStatusNodeEntity item in list)
            {
                if (!strList.Contains(item.NodeValue))
                {
                    strList.Add(item.NodeValue);
                }
                if (!strList.Contains(item.NextValue))
                {
                    strList.Add(item.NextValue);
                }
            }

            //排序
            strList = strList.OrderBy(i => i).ToList();

            return strList;
        }
        #endregion


        #region 获取订单可执行操作 changjian.tian20140421
        /// <summary>
        /// 获取订单可执行操作 changjian.tian 20140421
        /// </summary>
        /// <param name="pOrderId"></param>
        /// <returns></returns>
        public GetOrderActionsRD GetOrderActions(string pOrderId)
        {
            GetOrderActionsRD rdData = new GetOrderActionsRD();
            DataSet ds = new DataSet();
            string pDeliveryMethod = _currentDAO.GetOrderDeliveryMethod(pOrderId);
            ds = _currentDAO.GetOrderActions(pOrderId, pDeliveryMethod);
            IList<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (var rd = ds.Tables[0].CreateDataReader())
            {
                while (rd.Read())
                {
                    TInOutStatusNodeEntity m;
                    this._currentDAO.NewLoad(rd, out m);
                    list.Add(m);
                }
            }
            var OrderAction = new List<JIT.CPOS.DTO.Module.Order.Order.Response.OrderActionInfo> { };
            foreach (var item in list)
            {
                var orderaction = new OrderActionInfo()
                {
                    ActionCode = item.NextValue,//订单可执行操作状态码
                    Text = item.ActionDesc //订单状态描述
                };
                OrderAction.Add(orderaction);
            }
            rdData.Actions = OrderAction.ToArray();
            return rdData;
        }
        #endregion

        #region 获取打印订单详情
        /// <summary>
        /// 获取所有但就信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DataSet GetPrintPickInfo(string orderID)
        {
            DataSet ds = _currentDAO.GetPrintPickingInfo(orderID);
            return ds;
        }
        #endregion

        #region 获取不同打印类型数据
        /// <summary>
        /// 获取不同打印类型数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="itemTagID"></param>
        /// <returns></returns>
        public DataSet GetPrintPickingTypeInfo(string orderID, string itemTagID)
        {
            DataSet ds = _currentDAO.GetPrintPickingTypeInfo(orderID, itemTagID);
            return ds;
        }
        #endregion

    }
}
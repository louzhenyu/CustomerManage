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
    /// ҵ����  
    /// </summary>
    public partial class TInOutStatusNodeBLL
    {
        #region ��ȡ������һ��״̬���� Jermyn201403013
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

        #region ����֧���ɹ��޸�״̬ Jermyn20140313
        /// <summary>
        /// ����֧���ɹ��޸�״̬
        /// </summary>
        /// <param name="orderId">������ʶ</param>
        /// <param name="strError">������ʾ</param>
        /// <param name="ChannelId">֧��������ʶ</param>
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
                    //�����
                    //var bll = new JIT.CPOS.BS.BLL.InoutService(this.CurrentUserInfo);
                    //var orderInfo = bll.GetInoutInfoById(orderId);
                    //VipIntegralBLL vipIntegralServer = new VipIntegralBLL(this.CurrentUserInfo);

                    //Loggers.Debug(new DebugLogInfo()
                    //{
                    //    Message = string.Format("SetOrderPayment-����: sourceId={0},customerId={1},vipId={2},orderId={3}", 1, this.CurrentUserInfo.CurrentUser.customer_id, orderInfo.vip_no, orderId)
                    //});
                    //vipIntegralServer.ProcessPoint(1, this.CurrentUserInfo.CurrentUser.customer_id, orderInfo.vip_no, orderId);

                    this._currentDAO.OrderPayCallBack(orderId, SerialPay, this.CurrentUserInfo.ClientID, Convert.ToInt32(ChannelId));

                    strError = "�ɹ�.";
                    logInfo.TemplateContent = strError;
                    logSerer.Create(logInfo);

                    //��¼��־  qianzhi 2014-03-17
                    //var inoutStatus = new TInoutStatusBLL(CurrentUserInfo);
                    //TInoutStatusEntity info = new TInoutStatusEntity();
                    //info.InoutStatusID = Guid.Parse(Utils.NewGuid());
                    //info.OrderID = orderId;
                    //info.CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
                    //info.OrderStatus = 10000; //֧����ʽ
                    //info.Remark = "֧���ɹ�";
                    //info.StatusRemark = "����֧���ɹ�[������:" + CurrentUserInfo.CurrentUser.User_Name + "]";
                    //inoutStatus.Create(info);

                    #region ����������Ʒ�İ�

                    var inoutBLL = new T_InoutBLL(CurrentUserInfo);
                    var inoutInfo = inoutBLL.GetByID(orderId);
                    if (inoutInfo != null)
                    {
                        //����Ǿ����̶�����������ɺ󣬶���״̬�޸ĳ����״̬
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
                    Message = string.Format("SetOrderPayment-ʧ��: {0}", strError)
                });
                logInfo.TemplateContent = strError.Substring(0, 100);
                logInfo.IsSuccess = 0;
                logSerer.Create(logInfo);
                return false;
            }
        }


        #endregion

        #region ��ȡ��Ӧ�ͻ���ȫ������״̬ jifeng.cao 20140319
        /// <summary>
        /// ��ȡ��Ӧ�ͻ���ȫ������״̬
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

            //����ֵ����
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

            //����
            strList = strList.OrderBy(i => i).ToList();

            return strList;
        }
        #endregion


        #region ��ȡ������ִ�в��� changjian.tian20140421
        /// <summary>
        /// ��ȡ������ִ�в��� changjian.tian 20140421
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
                    ActionCode = item.NextValue,//������ִ�в���״̬��
                    Text = item.ActionDesc //����״̬����
                };
                OrderAction.Add(orderaction);
            }
            rdData.Actions = OrderAction.ToArray();
            return rdData;
        }
        #endregion

        #region ��ȡ��ӡ��������
        /// <summary>
        /// ��ȡ���е�����Ϣ
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public DataSet GetPrintPickInfo(string orderID)
        {
            DataSet ds = _currentDAO.GetPrintPickingInfo(orderID);
            return ds;
        }
        #endregion

        #region ��ȡ��ͬ��ӡ��������
        /// <summary>
        /// ��ȡ��ͬ��ӡ��������
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
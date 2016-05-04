
/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/22 16:00:00  更新订单配送信息
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
using System.Web;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Delivery.Request;
using JIT.CPOS.DTO.Module.Order.Delivery.Response;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Delivery
{
    public class UpdateOrderDeliveryInfoAH : BaseActionHandler<UpdateOrderDeliveryInfoRP, UpdateOrderDeliveryInfoRD>
    {
        protected override UpdateOrderDeliveryInfoRD ProcessRequest(APIRequest<UpdateOrderDeliveryInfoRP> pRequest)
        {
            #region 错误码
            const int ERROR_ORDERID_NOEXISTS = 301;
            #endregion
            UpdateOrderDeliveryInfoRD rddata = new UpdateOrderDeliveryInfoRD();
            string OrderID = pRequest.Parameters.OrderID;           //订单ID
            int DeliveryTypeID = pRequest.Parameters.DeliveryTypeID;//配送方式
            string StoreID = pRequest.Parameters.StoreID;           //自提的门店ID
            string PickupBeginDateTime = pRequest.Parameters.PickupUpDateRange; //自提时间范围
            string Mobile = pRequest.Parameters.Mobile;  //手机号码
            string Email = pRequest.Parameters.Email;    //邮箱
            string ReceiverAddress = pRequest.Parameters.ReceiverAddress;  //收货人地址
            string ReceiverName = pRequest.Parameters.ReceiverName;        //收货人姓名
            string reserveDay = pRequest.Parameters.PickingDate;
            string reserveQuantum = pRequest.Parameters.PickingTime;
            T_InoutBLL _TInoutbll = new T_InoutBLL(this.CurrentUserInfo);  //订单表
            var pTran = _TInoutbll.GetTran();
            #region 更新配送方式
            using (pTran.Connection)
            {
                try
                {
                    //根据订单ID获取实例
                    var entity = _TInoutbll.GetByID(OrderID);
                    if (entity == null)
                    {
                        throw new APIException(string.Format("未找到对应OrderID：{0}订单", OrderID)) { ErrorCode = ERROR_ORDERID_NOEXISTS };
                    }
                    entity.Field8 = DeliveryTypeID.ToString().Trim(); //订单配送方式 1.送货到家。2到店提货
                    entity.carrier_id = StoreID;                      //自提的门店ID 
                    entity.purchase_unit_id = StoreID;                //自提的门店ID
                    entity.Field9 = PickupBeginDateTime;              //自提时间范围
                    entity.Field6 = Mobile;                           //联系电话
                    entity.Field5 = Email;                            //邮箱
                    entity.Field4 = ReceiverAddress;                  //配送地址
                    entity.Field14 = ReceiverName;                      //收件人姓名
                    entity.reserveDay = string.IsNullOrEmpty(reserveDay) ? "" : Convert.ToDateTime(reserveDay).ToString("yyyy-MM-dd");
                    entity.reserveQuantum = reserveQuantum;
                    #region  更新时候更新时间和更新人同时更新
                    entity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //更新时间
                    entity.modify_user_id = CurrentUserInfo.UserID;//更新人 
                    #endregion
                    _TInoutbll.Update(entity, pTran);                  //用事物更新订单表（T_Inout）表中信息
                    pTran.Commit();//提交事物
                }
                catch (Exception ex)
                {
                    pTran.Rollback();
                    throw new APIException(ex.Message);
                }
            #endregion
            }
            return rddata;
        }
    }
}
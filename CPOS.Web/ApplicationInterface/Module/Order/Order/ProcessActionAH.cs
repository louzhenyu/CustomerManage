/*
 * Author		:Alex.Tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/21 14::00
 * Description	:订单执行操作
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
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using JIT.CPOS.Common;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using System.Data;
using JIT.CPOS.BS.DataAccess.Base;
using System.Collections;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderSend;
using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward;
namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.Order
{
    public class ProcessActionAH : BaseActionHandler<ProcessActionRP, ProcessActionRD>
    {
        protected override ProcessActionRD ProcessRequest(APIRequest<ProcessActionRP> pRequest)
        {
            #region 错误码
            const int ERROR_ORDER_NOTEXISTS = 301;
            const int ERROR_ORDER_PROCESSED = 302;
            #endregion
            ProcessActionRD rd = new ProcessActionRD();
            T_InoutBLL _TInoutbll = new T_InoutBLL(this.CurrentUserInfo);                   //订单表
            TInoutStatusBLL _TInoutStatusBLL = new TInoutStatusBLL(this.CurrentUserInfo);   //日志表
            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(this.CurrentUserInfo);       //会员业务对象实例化
            var UserBLL = new T_UserBLL(this.CurrentUserInfo);                        //员工实例

            string OrderID = pRequest.Parameters.OrderID;               //订单ID
            string ActionCode = pRequest.Parameters.ActionCode;         //订单操作码(当前订单状态码作为操作码)
            string ActionParameter = pRequest.Parameters.ActionParameter;//订单操作参数，可为空
            string DeliverCompany = pRequest.Parameters.DeliverCompany;  //快递公司
            string DeliverOrder = pRequest.Parameters.DeliverOrder;      //快递单号

            string VipID = pRequest.UserID;
            if (pRequest.ChannelId != "2")
            {
                VipID = pRequest.Parameters.VipID;
            }

            var tran = _TInoutbll.GetTran();
            using (tran.Connection)//事物
            {
                try
                {

                    #region 1.根据订单ID，订单操作码更新订单表中订单状态和状态描述

                    var entity = _TInoutbll.GetInoutInfo(OrderID, this.CurrentUserInfo); //根据标识获取新的实例

                    if (entity.status == ActionCode)//判断是否重复操作
                        throw new APIException("操作已处理") { ErrorCode = ERROR_ORDER_PROCESSED };

                    #region 当状态为完成时，返现，返积分
                    if (ActionCode == "700" && entity.status != "700")
                    {
                        //确认收货时处理积分、返现、佣金[完成订单]
                        // vipIntegralBLL.OrderReward(entity, tran);
                        new SendOrderRewardMsgBLL().OrderReward(entity, this.CurrentUserInfo, tran);//存入到缓存
                    }

                    #endregion
                    if (entity == null)
                        throw new APIException(string.Format("未找到OrderID：{0}订单", pRequest.Parameters.OrderID)) { ErrorCode = ERROR_ORDER_NOTEXISTS };

                    if (entity.status == ActionCode) //如果状态以及国内是要提交的状态了，就不要再提交了
                        return rd;

                    string Updatebeforestatus = entity.status_desc; //更新之前的订单状态描述
                    entity.status = ActionCode;                     //输入的状态码
                    entity.Field7 = ActionCode;
                    entity.status_desc = GetStatusDesc(ActionCode);  //输入的状态码对应的状态描述
                    entity.Field10 = GetStatusDesc(ActionCode);     //Field10=status_desc状态描述
                    entity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //修改时间
                    entity.modify_user_id = pRequest.UserID; //修改人

                    if (ActionCode == "600" || !string.IsNullOrEmpty(DeliverOrder) || !string.IsNullOrEmpty(DeliverCompany))
                    {
                        entity.Field9 = DateTime.Now.ToSQLFormatString();
                        entity.Field2 = DeliverOrder;//快递单号
                        entity.carrier_id = DeliverCompany;//快递单号
                        //更新订单配送商及配送单号
                    }

                    if (ActionCode == "700" && pRequest.ChannelId != "2")
                    {
                        if (string.IsNullOrEmpty(entity.sales_user))
                        {
                            entity.sales_user = pRequest.UserID;//把当前用户作为服务人员。****！！
                        }
                        //更新订单支付状态
                        entity.Field1 = "1";
                        entity.accpect_time = DateTime.Now.ToString();

                        #region 提货，订单明细修改

                        T_Inout_DetailBLL inoutDetailBll = new T_Inout_DetailBLL(CurrentUserInfo);
                        VipAmountDetailBLL vipAmountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);
                        if (pRequest.Parameters.OrderItemInfoList != null)
                        {
                            foreach (var item in pRequest.Parameters.OrderItemInfoList)
                            {
                                T_Inout_DetailEntity inoutDetailEntity = inoutDetailBll.QueryByEntity(new T_Inout_DetailEntity() { order_id = OrderID, sku_id = item.SkuId }, null).FirstOrDefault();
                                var vipAmountDetailEntity = vipAmountDetailBll.QueryByEntity(new VipAmountDetailEntity() { ObjectId = OrderID, AmountSourceId = "1" }, null).FirstOrDefault();
                                //将提货金额 + 余额 = SumPrice
                                if (vipAmountDetailEntity != null)
                                {
                                    item.SumPrice += Math.Abs(Convert.ToDecimal(vipAmountDetailEntity.Amount));
                                }
                                inoutDetailEntity.enter_qty = item.EnterQty;
                                inoutDetailEntity.Field9 = "kg";
                                inoutDetailEntity.enter_amount = item.SumPrice;
                                inoutDetailEntity.enter_price = Convert.ToDecimal(Math.Round(item.SumPrice / item.EnterQty, 2));
                                entity.total_amount = entity.total_amount + Math.Round(Convert.ToDecimal(item.SumPrice / (entity.discount_rate / 100)), 2) - inoutDetailEntity.retail_amount;
                                entity.total_retail = entity.total_amount;
                                entity.actual_amount = entity.actual_amount + Math.Round(Convert.ToDecimal(item.SumPrice / (entity.discount_rate / 100)), 2) - inoutDetailEntity.retail_amount;
                                inoutDetailBll.Update(inoutDetailEntity, tran);
                            }
                        }
                        #endregion

                    }
                    entity.accpect_time = DateTime.Now.ToString();
                    _TInoutbll.Update(entity, tran); //用事物更新订单表(T_Inout)
                    #endregion



                    #region 2.根据订单ID更新订单日志表中数据

                    string CreateName = "";
                    if (pRequest.ChannelId != "2")
                    {
                        var UserData = UserBLL.GetByID(this.CurrentUserInfo.CurrentUser.User_Id);
                        CreateName = string.Format("[操作人:{0}]", (UserData == null ? "" : UserData.user_name));
                    }
                    else
                    {
                        CreateName = "[操作人:客户]";
                    }



                    var _TInoutStatusEntity = new TInoutStatusEntity()
                    {
                        InoutStatusID = Guid.NewGuid(),
                        OrderID = OrderID,                         //订单ID
                        OrderStatus = Convert.ToInt32(ActionCode),   //状态码
                        //StatusRemark = "订单状态从" + Updatebeforestatus + "变为" + GetStatusDesc(ActionCode) + "[操作人:" + CurrentUserInfo.CurrentUser.User_Name + "]",               //状态更新描述
                        StatusRemark = "订单状态从" + Updatebeforestatus + "变为" + GetStatusDesc(ActionCode) + CreateName,               //状态更新描述
                        CustomerID = CurrentUserInfo.ClientID        //客户ID
                    };
                    _TInoutStatusBLL.Create(_TInoutStatusEntity, tran);  //用事物更新，向日志表(TInoutStatus)中插入一条数据
                    #endregion

                    tran.Commit();

                    if (ActionCode == "600")
                    {
                        #region 处理订单发货发送微信模板消息
                        //获取会员信息
                        var vipBll = new VipBLL(CurrentUserInfo);
                        var vipInfo = vipBll.GetByID(entity.vip_no);
                        var inoutService = new Inout3Service(CurrentUserInfo);
                        //物流公司
                        string LogisticsName = inoutService.GetCompanyName(entity.carrier_id);

                        var InoutInfo = new InoutInfo()
                        {
                            order_no = entity.order_no,
                            carrier_name = LogisticsName,
                            vipId = entity.vip_no,
                            Field2 = entity.Field2
                        };

                        //  var CommonBLL = new CommonBLL();
                        //   CommonBLL.SentShipMessage(InoutInfo, vipInfo.WeiXinUserId, InoutInfo.vip_no, CurrentUserInfo);                     
                        new SendOrderSendMsgBLL().SentShipMessage(InoutInfo, vipInfo.WeiXinUserId, InoutInfo.vip_no, CurrentUserInfo);
                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new APIException(ex.Message) { ErrorCode = ERROR_ORDER_NOTEXISTS };
                }
            }
            return rd;
        }
        #region 获取订单对应状态描述
        /// <summary>
        /// 获取订单对应状态描述
        /// </summary>
        /// <param name="status">订单状态</param>
        /// <returns>状态描述</returns>
        private string GetStatusDesc(string status)
        {
            string str = "";
            OptionsBLL optionsBll = new OptionsBLL(CurrentUserInfo);
            var optionsList = optionsBll.QueryByEntity(new OptionsEntity
            {
                OptionValue = Convert.ToInt32(status)
                ,
                IsDelete = 0
                ,
                OptionName = "TInOutStatus"
                ,
                CustomerID = CurrentUserInfo.CurrentLoggingManager.Customer_Id
            }, null);
            if (optionsList != null && optionsList.Length > 0)
            {
                str = optionsList[0].OptionText;
            }
            return str;
        }
        #endregion
    }
}
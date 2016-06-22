using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.RedisOperationBLL.OrderNotPay
{
    //往缓存里读
    public class OrderNotPayMsgSetJobBLL
    {

        /// <summary>
        /// 所有商户
        /// </summary>
        private Dictionary<string, string> _CustomerIDList
        { get; set; }
        /// <summary>
        /// SessionInfo
        /// </summary>
        private LoggingSessionInfo _T_loggingSessionInfo
        { get; set; }
        /// <summary>
        /// 订单详情
        /// </summary>
        private Inout3Service _Inout3Service
        { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        private T_InoutBLL _T_InoutBLL
        { get; set; }
        private VipBLL _VipBLL
        { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public OrderNotPayMsgSetJobBLL()
        {
            _T_loggingSessionInfo = new LoggingSessionInfo();
            _T_loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
        }

        /// <summary>
        /// 种植 角色菜单 缓存
        /// </summary>
        public void AutoSetOrderNotPayCache()
        {
            _CustomerIDList = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in _CustomerIDList)
            {
                //
                _T_loggingSessionInfo.ClientID = customer.Key;
                _T_loggingSessionInfo.CurrentLoggingManager.Connection_String = customer.Value;

                //
                _T_InoutBLL = new T_InoutBLL(_T_loggingSessionInfo);
                _Inout3Service = new Inout3Service(_T_loggingSessionInfo);
                _VipBLL = new VipBLL(_T_loggingSessionInfo);

                //
                // var t_InoutList = new List<string>();
                try
                {
                    var t_InoutEntitys = _T_InoutBLL.QueryByEntity(new T_InoutEntity
                    {
                        customer_id = customer.Key,
                        Field1 = "0",   //未支付
                        status = "100"  //已经正式提交
                    }, null);
                    if (t_InoutEntitys == null || t_InoutEntitys.Count() <= 0)
                    {
                        continue;
                    }
                    //
                    //   roleList = roleEntities.Select(it => it.role_id).ToList();

                    string productNames = "";
                    foreach (var t_InoutInfo in t_InoutEntitys)
                    {
                        //每次都重新设置
                        productNames = "";
                        var _T_Inout_DetailList = _Inout3Service.GetInoutDetailInfoByOrderId(t_InoutInfo.order_id, _T_loggingSessionInfo.ClientID);

                        if (_T_Inout_DetailList != null && _T_Inout_DetailList.Count > 0)//获取商品信息
                        {
                            foreach (var _T_Inout_DetailInfo in _T_Inout_DetailList)
                            {
                                if (productNames == "")
                                {
                                    productNames += _T_Inout_DetailInfo.item_name + "*" + _T_Inout_DetailInfo.order_qty;
                                }
                                else
                                {
                                    productNames += "," + _T_Inout_DetailInfo.item_name + "*" + _T_Inout_DetailInfo.order_qty;
                                }
                            }

                        }
                        //获取会员的信息
                        var vip = _VipBLL.GetByID(t_InoutInfo.order_id);

                        new SendOrderNotPayMsgBLL().NotPayMessage(t_InoutInfo.actual_amount.ToString(), productNames, t_InoutInfo.Field4, t_InoutInfo.order_no, vip.WeiXinUserId, _T_loggingSessionInfo);
                    }


                }
                catch
                {
                    continue;
                }

            }
        }

    }
}

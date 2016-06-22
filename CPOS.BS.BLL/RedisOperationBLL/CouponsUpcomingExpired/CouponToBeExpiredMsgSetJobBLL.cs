using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;


namespace JIT.CPOS.BS.BLL.RedisOperationBLL.CouponsUpcomingExpired
{
    public class CouponToBeExpiredMsgSetJobBLL
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
        private VipCouponMappingBLL _VipCouponMappingBLL
        { get; set; }
        //private VipBLL _VipBLL
        // { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CouponToBeExpiredMsgSetJobBLL()
        {
            _T_loggingSessionInfo = new LoggingSessionInfo();
            _T_loggingSessionInfo.CurrentLoggingManager = new LoggingManager();
        }

        /// <summary>
        /// 种植 角色菜单 缓存
        /// </summary>
        public void AutoSetCouponToBeExpiredCache()
        {
            _CustomerIDList = CustomerBLL.Instance.GetCustomerList();
            foreach (var customer in _CustomerIDList)
            {
                //
                _T_loggingSessionInfo.ClientID = customer.Key;
                _T_loggingSessionInfo.CurrentLoggingManager.Connection_String = customer.Value;

                //
                _VipCouponMappingBLL = new VipCouponMappingBLL(_T_loggingSessionInfo);


                //
                // var t_InoutList = new List<string>();
                try
                {
                    //根据配置查找要发送消息的优惠券（如果为空，默认为3）
                    var CouponToBeExpiredDay = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CouponToBeExpiredDay"]) ? 3 : Convert.ToInt32(ConfigurationManager.AppSettings["CouponToBeExpiredDay"]);
                    //获取已经领取的，未使用的优惠券的信息，还有三天过期、（小于四天，大于三天的优惠券）
                    var GetCouponToBeExpiredList = _VipCouponMappingBLL.GetCouponToBeExpired(_T_loggingSessionInfo.ClientID, CouponToBeExpiredDay);
                   

                    if (GetCouponToBeExpiredList == null || GetCouponToBeExpiredList.Tables == null || GetCouponToBeExpiredList.Tables.Count <= 0)
                    {
                        continue;
                    }
              
                    foreach (DataRow couponInfo in GetCouponToBeExpiredList.Tables[0].Rows)
                    {

                        new CouponToBeExpiredMsgBLL().CouponsUpcomingExpiredMessage(couponInfo["CouponTypeName"].ToString(), couponInfo["CouponCode"].ToString(), couponInfo["BeginDate"].ToString(), couponInfo["EndDate"].ToString(), couponInfo["WeiXinUserId"].ToString(), _T_loggingSessionInfo);
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

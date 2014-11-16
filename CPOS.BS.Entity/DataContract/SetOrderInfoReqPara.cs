using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Entity
{
    public class SetOrderInfoReqPara
    {
        public Guid? eventId { get; set; }//活动ID
        public string qty { get; set; }//商品数量
        public string storeId { get; set; }//门店标识
        public string totalAmount { get; set; }//订单总价
        public string mobile { get; set; }//手机号码
        public string email { get; set; }//邮箱
        public string remark { get; set; }//备注
        public string deliveryId { get; set; }//配送方式标识
        public string deliveryAddress { get; set; }//配送地址
        public string deliveryTime { get; set; }//提货时间（配送时间）
        public string selfStoreID { get; set; }
        public string username { get; set; }//用户姓名
        public string tableNumber { get; set; }//桌号(餐饮系统 Jermyn20130930 对应字段Field20)
        public string couponsPrompt { get; set; }//优惠券提示语（Jermyn20131213--Field16）
        public string actualAmount { get; set; }//实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
        public string status { get; set; }//	状态
        public string reqBy { get; set; }////请求0-wap,1-手机.
        public string joinNo { get; set; }//	参加人数 (Jermyn20140312 -- print_times)
        public string isGroupBy { get; set; }//是否团购订单（Jermyn20140318—Field15，之后也会改为order_reason_type_id）
        public string isPanicbuying { get; set; }//是否抢购订单 （Jermyn20140323—订单类型order_reason_type_id）
        public string salesPrice { get; set; }
        public string stdPrice { get; set; }
        public OrderDetail[] orderDetailList { get; set; }//商品明细

        #region 公共参数的参数
        public string customerId { get; set; }
        public string userId { get; set; }
        public string openId { get; set; }
        public string isALD { get; set; }
        #endregion

        public bool IsValid(out string msg)
        {
            msg = string.Empty;
            if (eventId == null)
                msg += "活动订单：eventId不能为Null";
            return string.IsNullOrEmpty(msg);
        }

        public decimal Rate
        {
            get
            {
                try
                {
                    return (Convert.ToDecimal(salesPrice) / Convert.ToDecimal(stdPrice)) * 100;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }

    public class OrderDetail
    {
        public string skuId { get; set; }//	商品SKU标识
        public string salesPrice { get; set; }//		商品销售单价
        public string qty { get; set; }//		商品数量
        public string beginDate { get; set; }//	入住日期(yyyy-MM-dd)
        public string endDate { get; set; }//		离开日期(yyyy-MM-dd)
        public decimal Amount
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(this.qty) * Convert.ToDecimal(this.salesPrice);
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    return 0;
                }
            }
        }
    }
}
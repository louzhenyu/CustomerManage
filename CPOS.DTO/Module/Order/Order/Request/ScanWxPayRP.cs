using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Order.Request
{
    public class ScanWxPayRP : IAPIRequestParameter
    {
        
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        public string ChannelID { get; set; }

        /// <summary>
        /// 商品简单描述
        /// </summary>
        public string OrderDesc { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 支付场景;1：充值订单; 2：售卡订单
        /// </summary>
        public int PaymentScenarios { get; set; }

        /// <summary>
        /// 支付模式 0:支付宝扫码支付; 1:微信扫码支付; 
        /// </summary>
        public int? PaymentMode { get; set; }

        public void Validate()
        {

        }
    }
}

using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Response
{
    public class GetDeliveryOrderListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }

        public List<DeliveryOrderData> DeliveryOrderList { get; set; }
    }

    public class DeliveryOrderData 
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 提货时间
        /// </summary>
        public string DeliveryTime { get; set; }
        /// <summary>
        /// 服务人员ID
        /// </summary>
        public string SalesUserID { get; set; }
        /// <summary>
        /// 服务人员名称
        /// </summary>
        public string SalesUserName { get; set; }
        /// <summary>
        /// 图片Url（会员头像）
        /// </summary>
        public string HeadImgUrl { get; set; }
    }
}

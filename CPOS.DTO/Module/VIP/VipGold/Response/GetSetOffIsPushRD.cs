using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetSetOffIsPushRD : IAPIResponseData
    {
        /// <summary>
        /// 创意仓库活动是否推送或分享 返回未分享数量
        /// </summary>
        public int CTW_EventIsPush { get; set; }
        /// <summary>
        /// 集客工具是否推送或分享 返回未分享个数量
        /// </summary>
        public int SetOffToolIsPush { get; set; }
        /// <summary>
        /// 优惠券是否推送或分享 返回未分享个数量
        /// </summary>
        public int CouponIsPush { get; set; }

    }
}

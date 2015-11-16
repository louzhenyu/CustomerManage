using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipIntegral.Request
{
    public class SetVipIntegralRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        /// <summary>
        /// 会员编码
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// 积分来源（27=积分调整）
        /// </summary>
        public string IntegralSourceID { get; set; }
        /// <summary>
        /// 调整数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 调整原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 字段验证
        /// </summary>
        public void Validate()
        {

        }
    }
}

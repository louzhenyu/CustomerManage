using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipAmount.Request
{
    public class SetVipAmountRP : IAPIRequestParameter
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
        /// 余额/返现来源（23=人工调整余额；24=人工调整返现）
        /// </summary>
        public string AmountSourceID { get; set; }
        /// <summary>
        /// 调整金额
        /// </summary>
        public decimal Amount { get; set; }
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

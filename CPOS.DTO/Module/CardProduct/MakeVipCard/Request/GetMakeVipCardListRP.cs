using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request
{
    /// <summary>
    /// 获取制卡列表请求对象
    /// </summary>
    public class GetMakeVipCardListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡类型编号
        /// </summary>
        public string VipCardTypeCode { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StareDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 当前叶数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 叶索引
        /// </summary>
        public int PageIndex { get; set; }
        public void Validate()
        {

        }
    }
}

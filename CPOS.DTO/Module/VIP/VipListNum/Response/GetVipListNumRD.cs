using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipListNum.Response
{
    public class GetVipListNumRD : IAPIResponseData
    {
        /// <summary>
        /// 表单
        /// </summary>
        public VipNumInfo[] Items { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int? VipNumAll { get; set; }
        /// <summary>
        /// 当前人数
        /// </summary>
        public int? VipNumNow { get; set; }
    }

    public class VipNumInfo
    {
        /// <summary>
        /// 5分钟内，5-10分钟内，1小时内，1小时以上。以上4种类型的一种
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int? VipNum { get; set; }
        /// <summary>
        /// 比例
        /// </summary>
        public int? Proportion { get; set; }
    }
}

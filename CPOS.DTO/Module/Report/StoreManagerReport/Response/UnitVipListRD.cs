using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitVipListRD : IAPIResponseData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public Int32? TotalPageCount { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public Int32? TotalCount { get; set; }

  
        /// <summary>
        /// 会员列表
        /// </summary>
        public List<UnitVipList> VipList { get; set; }
    }

    /// <summary>
    /// 门店日销售额
    /// </summary>
    public class UnitVipList
    {
        /// <summary>
        /// 排序
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string VipName { get; set; }

        /// 会员手机
        /// </summary>
        public string VipPhone { get; set; }
    }
}

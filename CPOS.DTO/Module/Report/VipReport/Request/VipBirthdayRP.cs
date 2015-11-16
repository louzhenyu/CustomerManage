using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.VipReport.Request
{
    public class VipBirthdayRP : IAPIRequestParameter
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 门店
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 卡状态ID
        /// </summary>
        public int? VipCardStatusID { get; set; }
        /// <summary>
        /// 最近消费
        /// </summary>
        public int Consumption { get; set; }

        public void Validate() { }
    }
}

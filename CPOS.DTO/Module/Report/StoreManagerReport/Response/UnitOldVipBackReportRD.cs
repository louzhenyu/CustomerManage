using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitOldVipBackReportRD : IAPIResponseData
    {
        /// <summary>
        /// 当月复购会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitCurrentMonthOldVipBackCountList { get; set; }

        /// <summary>
        /// 当年复购会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitCurrentYearOldVipBackCountList { get; set; }
    }
}

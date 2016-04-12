using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Response
{
    public class UnitBirthdayVipReportRD : IAPIResponseData
    {
        /// <summary>
        /// 当月生日会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitCurrentMonthBirthdayVipCountList { get; set; }

        /// <summary>
        /// 当月生日未回店会员数量（按会员卡等级）
        /// </summary>
        public List<UnitVipCountByVipCardLevel> UnitCurrentMonthBirthdayVipNoBackCountList { get; set; }
    }
}

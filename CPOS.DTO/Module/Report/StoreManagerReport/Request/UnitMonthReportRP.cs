using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Request
{
    public class UnitMonthReportRP : IAPIRequestParameter
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitID { get; set; }

        /// <summary>
        /// 查询日期
        /// </summary>
        public string Date { get; set; }


        public void Validate()
        {
            if (string.IsNullOrEmpty(CustomerID))
            {
                throw new APIException("客户标识为空！");
            }
            if (string.IsNullOrEmpty(UnitID))
            {
                throw new APIException("门店标识为空！");
            }
            if (string.IsNullOrEmpty(Date))
            {
                throw new APIException("查询日期为空！");
            }
            DateTime dm;
            if (!DateTime.TryParse(Date, out dm))
            {
                throw new APIException("查询日期不是日期格式！");
            }
        }
    }
}

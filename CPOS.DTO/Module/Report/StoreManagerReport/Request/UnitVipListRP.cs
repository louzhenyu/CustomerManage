using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Report.StoreManagerReport.Request
{
    public class UnitVipListRP : IAPIRequestParameter
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

        /// <summary>
        /// 1:新增潜力会员列表
        /// 2:三个月内新增未复购会员列表
        /// 3:当月未回店生日会员列表
        /// 4:店铺全部会员列表
        /// </summary>
        public Int32 Type { get; set; }

        /// <summary>
        /// 当前页（从0开始）
        /// </summary>
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public Int32 PageSize { get; set; }

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
            if (PageIndex<0)
            {
                throw new APIException("页码错误！");
            }
            if (PageSize < 0)
            {
                throw new APIException("每页显示条数错误！");
            }
        }
    }
}

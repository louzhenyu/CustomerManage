using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.Logistics.Request
{
    public class SetLogisticsCompanyRP : IAPIRequestParameter
    {
        /// <summary>
        /// 操作类型（1=添加；2=新增；3=移除）
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 配送商ID（1、3必传）
        /// </summary>
        public string LogisticsID { get; set; }
        /// <summary>
        /// 配送商名称
        /// </summary>
        public string LogisticsName { get; set; }
        /// <summary>
        /// 配送商简称
        /// </summary>
        public string LogisticsShortName { get; set; }
        /// <summary>
        /// （1=系统预设；2=商户）
        /// </summary>
        public int IsSystem { get; set; }
        public void Validate()
        {

        }
    }
}

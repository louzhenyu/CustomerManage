using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Basic.Holiday.Response
{
    
    public class GetHolidayDetailRD : IAPIResponseData
    {
        
        /// <summary>
        /// 
        /// </summary>
        public Guid? HolidayId { get; set; }

        /// <summary>
        /// 假日名称
        /// </summary>
        public String HolidayName { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public String Model { get; set; }

        /// <summary>
        /// 工作日
        /// </summary>
        public String Workdays { get; set; }

        /// <summary>
        /// 选项(1、2、3)
        /// </summary>
        public String Options { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public String Months { get; set; }

        /// <summary>
        /// 周
        /// </summary>
        public String Weeks { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public String Days { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public Decimal? Discount { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Desciption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }


        
    }
}

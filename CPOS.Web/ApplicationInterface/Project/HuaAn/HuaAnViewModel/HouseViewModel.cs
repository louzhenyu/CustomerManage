using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 房产列表视图模型。
    /// </summary>
    public class HouseViewModel
    {
        /// <summary>
        /// 房产ID。
        /// </summary>
        public Guid HouseID { get; set; }

        /// <summary>
        /// 房产Code。
        /// </summary>
        public string HouseCode { get; set; }

        /// <summary>
        /// 房产名称。
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// 房产主图。
        /// </summary>
        public string HouseImgURL { get; set; }

        /// <summary>
        /// 已销售数量。
        /// </summary>
        public int SaleHoseNum { get; set; }

        /// <summary>
        /// 经纬度。
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// 开盘日期。
        /// </summary>
        public DateTime HouseOpenDate { get; set; }
        //开盘时间2
        public string HouseOpenDate2 { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal RealPay { get; set; }
        public decimal DeductionPay { get; set; }

        /// <summary>
        /// 楼盘详细URl。
        /// </summary>
        public string HouseDetailURL { get; set; }

        /// <summary>
        /// 楼盘详细ID。
        /// </summary>
        public Guid DetailID { get; set; }

        public int? Fundtype { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 我的房产视图模型。
    /// </summary>
    public class MyHouseViewModel
    {
        /// <summary>
        /// 楼盘ID
        /// </summary>
        public Guid HouseID { get; set; }

        /// <summary>
        /// 楼盘Code
        /// </summary>
        public string HouseCode { get; set; }

        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// 楼盘Url
        /// </summary>
        public string HouseImgURL { get; set; }

        /// <summary>
        /// 楼盘状态
        /// </summary>
        public int HouseState { get; set; }

        /// <summary>
        /// 经纬度
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// 购买时间DateTime。
        /// </summary>
        public DateTime BuyHouseDate { get; set; }

        /// <summary>
        /// 购买时间：供视图使用。
        /// </summary>
        public string HouseOpenDate { get; set; }

        /// <summary>
        /// 楼盘均价，即最低价
        /// </summary>
        public decimal LowestPrice { get; set; }

        /// <summary>
        /// 楼盘详细ID。
        /// </summary>
        public Guid DetailID { get; set; }

        /// <summary>
        /// 交易号。
        /// </summary>
        public string ThirdOrderNo { get; set; }

        /// <summary>
        /// 房产详细ID,可供一套房产多次购买情况适用
        /// </summary>
        public Guid PrePaymentID { get; set; }
    }
}
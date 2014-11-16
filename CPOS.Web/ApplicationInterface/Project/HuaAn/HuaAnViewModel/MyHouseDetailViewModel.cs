using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 我的房产详细视图模型。
    /// </summary>
    public class MyHouseDetailViewModel
    {
        /// <summary>
        /// 房产ID
        /// </summary>
        public Guid HouseID { get; set; }

        /// <summary>
        /// 房产Code
        /// </summary>
        public string HouseCode { get; set; }

        /// <summary>
        /// 房产名称
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// 房产Url
        /// </summary>
        public string HouseImgURL { get; set; }

        /// <summary>
        /// 房产状态
        /// </summary>
        public int HouseState { get; set; }

        /// <summary>
        /// 经纬度
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// /购买时间DateTime。
        /// </summary>
        public DateTime BuyHouseDate { get; set; }

        /// <summary>
        /// 购买时间：供视图使用。
        /// </summary>
        public string HouseOpenDate { get; set; }

        /// <summary>
        /// 最低价：均价
        /// </summary>
        public decimal LowestPrice { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal RealPay { get; set; }

        /// <summary>
        /// 收益
        /// </summary>
        public decimal GrandProfit { get; set; }

        /// <summary>
        /// 楼盘明细ID
        /// </summary>
        public Guid DetailID { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        public string ThirdOrderNo { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public int Fundtype { get; set; }

        /// <summary>
        /// 世联订单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 预付款标识
        /// </summary>
        public Guid PrePaymentID { get; set; }

        /// <summary>
        /// MappingID
        /// </summary>
        public Guid MappingID { get; set; }

        /// <summary>
        /// 是否显示赎回按钮 (true显示，false 不显示)
        /// </summary>
        public bool IsShowRansom { get; set; }

        /// <summary>
        /// 是否显示过户按钮(true显示，false 不显示)
        /// </summary>
        public bool IsShowTransfer { get; set; }

        /// <summary>
        /// 购买基金状态
        /// </summary>
        public int? BuyFundState { get; set; }

        /// <summary>
        /// 过户状态
        /// </summary>
        public int? PayFundState { get; set; }

        /// <summary>
        /// 赎回基金状态
        /// </summary>
        public int? RedeemFundState { get; set; }

        /// <summary>
        /// 过户按钮启用状态
        /// </summary>
        public bool DisablePay { get; set; }

        /// <summary>
        /// 赎回按钮启用状态
        /// </summary>
        public bool DisabledRedeem { get; set; }


        /// <summary>
        /// Msg
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 购买基金返回消息
        /// </summary>
        public string BuyRetMsg { get; set; }

        /// <summary>
        /// 过户消息
        /// </summary>
        public string PayRetMsg { get; set; }

        /// <summary>
        /// 赎回返回的消息
        /// </summary>
        public string RedeemRetMsg { get; set; }

    }
}
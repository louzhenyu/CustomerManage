using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 楼盘状态枚举类定义。
    /// </summary>
    public enum HouseStateEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 在售
        /// </summary>
        Sold = 1,

        /// <summary>
        /// 未售
        /// </summary>
        Unsold = 2
    }

    /// <summary>
    /// 房产购买状态。
    /// </summary>
    public enum PayHouseStateEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 购买成功
        /// </summary>
        Sold = 1,

        /// <summary>
        /// 购买失败
        /// </summary>
        Unsold = 2,

        /// <summary>
        /// 委托中
        /// </summary>
        Order = 3,

        /// <summary>
        /// 委托失败
        /// </summary>
        OrderError = 4
    }

    /// <summary>
    /// 交易类型枚举类定义
    /// </summary>
    public enum FundtypeeEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 申购
        /// </summary>
        ReservationPurchase = 2101,

        /// <summary>
        /// 赎回
        /// </summary>
        ReservationRedeem = 2201,

        /// <summary>
        /// 支付
        /// </summary>
        ReservationPay = 2001
    }

    /// <summary>
    /// 过户状态定义。
    /// </summary>
    public enum FundStateEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 失败
        /// </summary>
        Error = 2,

        /// <summary>
        /// 委托处理中
        /// </summary>
        Order = 3,

        /// <summary>
        /// 委托失败
        /// </summary>
        OrderError = 4,

        /// <summary>
        /// 撤单 
        /// </summary>
        Cancel = 9
    }
}
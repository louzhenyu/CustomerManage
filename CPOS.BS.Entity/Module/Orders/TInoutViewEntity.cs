using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体:订单表
    /// </summary>
    public partial class TInoutViewEntity : TInoutEntity
    {
        #region 构造函数
        public TInoutViewEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrdersID{get;set;}

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrdersNo { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public string OrdersDate { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string Payment { get; set; }

        /// <summary>
        /// 付款类型 是 付款方式的值
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrdersStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrdersStatusText { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 房型
        /// </summary>
        public string RoomTypeName { get; set; }

        /// <summary>
        /// 客人名称
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// 入住日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 离店日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 房间数
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 全部数量
        /// </summary>
        public int AllCount { get; set; }

        /// <summary>
        /// 待审核数量
        /// </summary>
        public int ApproveCount { get; set; }

        /// <summary>
        /// 待入住数量
        /// </summary>
        public int CheckCount { get; set; }

        /// <summary>
        /// 完成数量
        /// </summary>
        public int CompleteCount { get; set; }

        /// <summary>
        /// 取消数量
        /// </summary>
        public int CancelCount { get; set; }

        /// <summary>
        /// 审核不通过数量
        /// </summary>
        public int NotAuditCount { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string VipID { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }

        /// <summary>
        /// 会员手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 购买方式
        /// </summary>
        public string BuyWay { get; set; }

        /// <summary>
        /// 获取方式
        /// </summary>
        public string GetWay { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>
        public string Price { get; set; }
        #endregion
    }
}

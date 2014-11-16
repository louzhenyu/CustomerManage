using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体:房价 房态
    /// </summary>
    public partial class StoreItemDailyStatusViewEntity : JIT.CPOS.BS.Entity.StoreItemDailyStatusEntity
    {
        #region 构造函数
        public StoreItemDailyStatusViewEntity()
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
        #endregion
    }
}

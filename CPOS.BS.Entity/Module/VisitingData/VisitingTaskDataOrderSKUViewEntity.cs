/*
 * Author		:Gavin
 * EMail		:jianbo.chen@gmail.com
 * Company		:JIT
 * Create On	:2013/3/11 18:47:38
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// OrdersDetailViewEntity
    /// </summary>
    public class VisitingTaskDataOrderSKUViewEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDataOrderSKUViewEntity()
        {
        }
        #endregion

        /// <summary>
        /// OrdersNo
        /// </summary>
        public string OrdersNo { get; set; }

        /// <summary>
        /// OrdersType
        /// </summary>
        public string OrdersTypeName { get; set; }

        /// <summary>
        /// SKUNO
        /// </summary>
        public string SKUNo { get; set; }

        /// <summary>
        /// SKUName
        /// </summary>
        public string SKUName { get; set; }
         
        /// <summary>
        /// 订购数量
        /// </summary>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 订购价格
        /// </summary>
        public decimal? OrdersPrice { get; set; }
        
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal? TotalAmout { get; set; }
    }
}

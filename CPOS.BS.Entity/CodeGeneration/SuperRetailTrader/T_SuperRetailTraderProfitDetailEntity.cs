/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/3 16:33:11
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
    /// 实体：  
    /// </summary>
    public partial class T_SuperRetailTraderProfitDetailEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SuperRetailTraderProfitDetailEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderProfitConfigId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderID { get; set; }

		/// <summary>
		/// 这里指得是相对层级   1.是佣金   > 1.是分润
		/// </summary>
		public Int32? Level { get; set; }

		/// <summary>
		/// Cash 比例   
		/// </summary>
		public String ProfitType { get; set; }

		/// <summary>
		/// 目前是金额
		/// </summary>
		public Decimal? Profit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OrderActualAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SalesId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
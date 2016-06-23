/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    public partial class T_SuperRetailTraderProfitConfigEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SuperRetailTraderProfitConfigEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 相对级别
		/// </summary>
		public Int32? Level { get; set; }

		/// <summary>
		/// Percent 比例   
		/// </summary>
		public String ProfitType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Profit { get; set; }

		/// <summary>
		/// 10.有效   90.失效
		/// </summary>
		public String Status { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderProfitConfigId { get; set; }
        /// <summary>
        /// 相关Id
        /// </summary>
        public string RefSuperRetailTraderProfitConfigId { get; set; }
        #endregion

    }
}
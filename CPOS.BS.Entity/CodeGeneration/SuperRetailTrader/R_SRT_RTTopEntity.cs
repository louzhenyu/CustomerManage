/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/1 19:09:45
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
    public partial class R_SRT_RTTopEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public R_SRT_RTTopEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DateCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 1：30天销售排名   2：30天新增下线排名
		/// </summary>
		public Int32? BusiType { get; set; }

		/// <summary>
		/// 1：正排名
		/// </summary>
		public Int32? TopType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? SuperRetailJoinTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Idx { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AddRTCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ID { get; set; }


        #endregion

    }
}
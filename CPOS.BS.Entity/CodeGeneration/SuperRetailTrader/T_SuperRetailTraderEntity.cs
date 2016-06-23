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
    public partial class T_SuperRetailTraderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_SuperRetailTraderEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderLogin { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderPass { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderMan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderPhone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderAddress { get; set; }

		/// <summary>
		/// 分销商来源   User  员工   Vip    会员
		/// </summary>
		public String SuperRetailTraderFrom { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SuperRetailTraderFromId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? HigheSuperRetailTraderID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? JoinTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

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
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? SuperRetailTraderID { get; set; }


        #endregion

    }
}
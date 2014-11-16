/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:56
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
    public partial class EclubClientCitySequenceEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EclubClientCitySequenceEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// 主键ID
		/// </summary>
		public Guid? ClientCitySequenceID { get; set; }

		/// <summary>
        /// 1为省排序，2为市排序，3为县排序
		/// </summary>
		public Int32? CityType { get; set; }

		/// <summary>
        /// 省市县的编号
		/// </summary>
		public String CityCode { get; set; }

		/// <summary>
        /// 排序
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
        /// 客户ID
		/// </summary>
		public String CustomerId { get; set; }

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
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/20 17:28:04
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
    public partial class EclubMicroTypeEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EclubMicroTypeEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String MicroTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MicroTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TypeLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IconPath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Intro { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Sequence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Style { get; set; }

		/// <summary>
		/// 
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
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/8 11:16:03
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
    public partial class MobileBussinessDefinedEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MobileBussinessDefinedEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? MobileBussinessDefinedID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TableName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? MobilePageBlockID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ColumnDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ColumnDescEn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ColumnName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LinkageItem { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CorrelationValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ExampleValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ControlType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AuthType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinLength { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxLength { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinSelected { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxSelected { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsMustDo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EditOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ViewOrder { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public Int32? AttributeTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsTemplate { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 10:37:49
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
    public partial class TUnitEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TUnitEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitNameEn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitNameShort { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitCityID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitContact { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitTel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitFax { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitEmail { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitPostcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitRemark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UnitFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CUSTOMERLEVEL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModifyUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModifyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String StatusDesc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String BatID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IfFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Longitude { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Dimension { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FtpImagerURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WebserversURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DimensionalCodeURL { get; set; }

        ///// <summary>
        ///// 创建人
        ///// </summary>
        //public String CreateBy { get; set; }

        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //public DateTime? CreateTime { get; set; }

        ///// <summary>
        ///// 最后更新人
        ///// </summary>
        //public String LastUpdateBy { get; set; }

        ///// <summary>
        ///// 最后更新时间
        ///// </summary>
        //public DateTime? LastUpdateTime { get; set; }

        ///// <summary>
        ///// 删除标志
        ///// </summary>
        //public Int32? IsDelete { get; set; }


        #endregion

    }
}
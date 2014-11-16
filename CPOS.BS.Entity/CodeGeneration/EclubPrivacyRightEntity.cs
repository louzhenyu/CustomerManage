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
    public partial class EclubPrivacyRightEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EclubPrivacyRightEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// 主键ID
		/// </summary>
		public Guid? PrivacyRightID { get; set; }

		/// <summary>
        /// 对象ID
		/// </summary>
		public String ObjectID { get; set; }

		/// <summary>
        /// 人员ID
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
        /// 操作状态，1.仅自己、2.本班级可见、3.本课程所见、4.本协会可见、5.所有校友可见
		/// </summary>
		public String OperationStatus { get; set; }

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
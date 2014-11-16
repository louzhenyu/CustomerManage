/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/12 13:35:16
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
    public partial class WMenuEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WMenuEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Key { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DisplayColumn { get; set; }

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
		public String ImageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MaterialTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ModelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VoiceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VideoId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TextId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Text { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MenuURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? PageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PageUrlJson { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PageParamJson { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAuth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? BeLinkedType { get; set; }


        #endregion

    }
}
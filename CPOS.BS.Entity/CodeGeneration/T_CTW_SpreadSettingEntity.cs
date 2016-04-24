/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/24 16:19:43
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
    /// 实体： Share 分享   Focus 关注   Reg 注册 
    /// </summary>
    public partial class T_CTW_SpreadSettingEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CTW_SpreadSettingEntity()
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
		public String SpreadType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Summary { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PromptText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LeadPageQRCodeImageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LeadPageSharePromptText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LeadPageFocusPromptText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LeadPageRegPromptText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LogoUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? CTWEventId { get; set; }

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
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

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
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/1 15:44:23
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
    public partial class WKeywordReplyEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WKeywordReplyEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ReplyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Keyword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ReplyType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Text { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TextId { get; set; }

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
		public String ImageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ApplicationId { get; set; }

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
		public String ModelId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? KeywordType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

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
		public Int32? BeLinkedType { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:33
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
    public partial class ItemCommentEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ItemCommentEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String ItemCommentId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CommentType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CommentImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CommentVideoUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CommentContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CommentTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CommentUserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CommentUserImageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsVip { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipId { get; set; }

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
		public String ObjectId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ProductMatch { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ItemType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Topic { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Star { get; set; }


        #endregion

    }
}
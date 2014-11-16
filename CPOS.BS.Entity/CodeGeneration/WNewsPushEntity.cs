/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/15 18:13:03
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
    public partial class WNewsPushEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public WNewsPushEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String NewsPushID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String WeiXinID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MsgType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PicUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LocationX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LocationY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Scale { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Url { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerMsgType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerMusicUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String AnswerHQMusicUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? AnswerArticleCount { get; set; }

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


        #endregion

    }
}
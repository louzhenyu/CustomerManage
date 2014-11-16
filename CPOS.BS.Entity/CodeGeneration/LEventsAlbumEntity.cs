/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/23 15:21:39
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
    public partial class LEventsAlbumEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public LEventsAlbumEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 主标识
		/// </summary>
		public String AlbumId { get; set; }

		/// <summary>
		/// 模块ID
		/// </summary>
		public String ModuleId { get; set; }

		/// <summary>
		/// 模块类型 1：活动 
		/// </summary>
		public String ModuleType { get; set; }

		/// <summary>
		/// 模块名称
		/// </summary>
		public String ModuleName { get; set; }

		/// <summary>
		/// 类型  1： 相片  2： 视频
		/// </summary>
		public String Type { get; set; }

		/// <summary>
		/// 封面图片
		/// </summary>
		public String ImageUrl { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Intro { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 序号
		/// </summary>
		public Int32? SortOrder { get; set; }

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
		public String CustomerID { get; set; }


        #endregion

    }
}
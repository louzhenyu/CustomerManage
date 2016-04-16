/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 10:45:49
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
    public partial class T_CTW_LEventInteractionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CTW_LEventInteractionEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? InteractionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? CTWEventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ThemeId { get; set; }

		/// <summary>
		/// 1.吸粉   2.促销
		/// </summary>
		public Int32? InteractionType { get; set; }

		/// <summary>
		/// DZP 大转盘   HB   红包   QN  问卷   QG  抢购/秒杀   TG  团购   RX  热销
		/// </summary>
		public String DrawMethodCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? OriginalLeventId { get; set; }

		/// <summary>
		/// 这里可能是游戏活动，也有可能是促销活动
		/// </summary>
		public String LeventId { get; set; }

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
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 11:38:21
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
    public partial class MarketEventEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MarketEventEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 主标识
		/// </summary>
		public String MarketEventID { get; set; }

		/// <summary>
		/// 活动号码
		/// </summary>
		public String EventCode { get; set; }

		/// <summary>
		/// 品牌标识
		/// </summary>
		public String BrandID { get; set; }

		/// <summary>
		/// 活动类型
		/// </summary>
		public String EventType { get; set; }

		/// <summary>
		/// 活动方式
		/// </summary>
		public String EventMode { get; set; }

		/// <summary>
		/// 活动状态
		/// </summary>
		public Int32? EventStatus { get; set; }

		/// <summary>
		/// 预算总金额
		/// </summary>
		public Decimal? BudgetTotal { get; set; }

		/// <summary>
		/// 人均基本金额
		/// </summary>
		public Decimal? PerCapita { get; set; }

		/// <summary>
		/// 开始时间
		/// </summary>
		public String BeginTime { get; set; }

		/// <summary>
		/// 结束时间
		/// </summary>
		public String EndTime { get; set; }

		/// <summary>
		/// 活动描述
		/// </summary>
		public String EventDesc { get; set; }

		/// <summary>
		/// 是否有波段
		/// </summary>
		public Int32? IsWaveBand { get; set; }

		/// <summary>
		/// 门店数量
		/// </summary>
		public Int32? StoreCount { get; set; }

		/// <summary>
		/// 人群数量
		/// </summary>
		public Int32? PersonCount { get; set; }

		/// <summary>
		/// 模板标识
		/// </summary>
		public String TemplateID { get; set; }

		/// <summary>
		/// 统计标识
		/// </summary>
		public String StatisticsID { get; set; }

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
		/// 模板内容
		/// </summary>
		public String TemplateContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateContentSMS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SendTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TemplateContentAPP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}
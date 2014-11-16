/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 14:13:25
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
    /// 实体： 定时推送规则 
    /// </summary>
    public partial class TimingPushMessageRuleEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TimingPushMessageRuleEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// TimingPushMessageRuleID
		/// </summary>
		public Guid? TimingPushMessageRuleID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CSPipelineID { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// 相关活动ID
		/// </summary>
		public String EventID { get; set; }

		/// <summary>
		/// 启动时间
		/// </summary>
		public DateTime? ActiveTime { get; set; }

		/// <summary>
		/// ClientID
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// CreateBy
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// CreateTime
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// LastUpdateBy
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// LastUpdateTime
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// IsDelete
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
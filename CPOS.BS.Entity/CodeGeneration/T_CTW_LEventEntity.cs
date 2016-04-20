/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/20 11:28:19
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
    public partial class T_CTW_LEventEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CTW_LEventEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? CTWEventId { get; set; }

		/// <summary>
		/// 这个模板Id是ap库里的，引入到这边以便可以追溯
		/// </summary>
		public Guid? TemplateId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityGroupId { get; set; }

		/// <summary>
		/// 1. 吸粉   2.促销
		/// </summary>
		public Int32? InteractionType { get; set; }

		/// <summary>
		/// 引用AP库的图片URL
		/// </summary>
		public String ImageURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OnlineQRCodeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OfflineQRCodeId { get; set; }

		/// <summary>
		/// 10=待发布   20=运行中   30=暂停   40=结束
		/// </summary>
		public Int32? Status { get; set; }

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

		/// <summary>
		/// 
		/// </summary>
		public String OffLineRedirectUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String OnLineRedirectUrl { get; set; }


        #endregion

    }
}
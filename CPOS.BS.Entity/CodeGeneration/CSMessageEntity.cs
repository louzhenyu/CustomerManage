/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/27 14:13:24
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
    /// 实体： 客服消息 
    /// </summary>
    public partial class CSMessageEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CSMessageEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// CSMessageID
		/// </summary>
		public Guid? CSMessageID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CSServiceTypeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CSPipelineID { get; set; }

		/// <summary>
		/// 通道标示
		/// </summary>
		public String PipelineAccount { get; set; }

		/// <summary>
		/// 内容
		/// </summary>
		public String Content { get; set; }

		/// <summary>
		/// 会员ID
		/// </summary>
		public String MemberID { get; set; }

		/// <summary>
		/// 会员名称
		/// </summary>
		public String MemberName { get; set; }

		/// <summary>
		/// 咨询对象ID
		/// </summary>
		public String CSObjectID { get; set; }

		/// <summary>
		/// 用户设备信息
		/// </summary>
		public String DeviceToken { get; set; }

		/// <summary>
		/// 当前回话客服ID
		/// </summary>
		public String CurrentCSID { get; set; }

		/// <summary>
		/// 与当前客服建立链接时间
		/// </summary>
		public DateTime? ConnectionTime { get; set; }

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
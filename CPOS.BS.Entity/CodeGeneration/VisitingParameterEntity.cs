/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:48
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
    /// 实体： 拜访数据明细(参数定义) 
    /// </summary>
    public partial class VisitingParameterEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingParameterEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Guid? VisitingParameterID { get; set; }

		/// <summary>
		/// 参数类别(1-产品相关,2-品牌相关等)
		/// </summary>
		public int? ParameterType { get; set; }

		/// <summary>
		/// 参数名称(采集数据名称)
		/// </summary>
		public string ParameterName { get; set; }

		/// <summary>
		/// 参数名称
		/// </summary>
		public string ParameterNameEn { get; set; }

		/// <summary>
		/// 控件类型
		/// </summary>
		public int? ControlType { get; set; }

		/// <summary>
		/// 数据选项名称(通常为下拉选项名称)
		/// </summary>
		public string ControlName { get; set; }

		/// <summary>
		/// 最大值
		/// </summary>
		public decimal? MaxValue { get; set; }

		/// <summary>
		/// 最小值
		/// </summary>
		public decimal? MinValue { get; set; }

		/// <summary>
		/// 缺省值
		/// </summary>
		public string DefaultValue { get; set; }

		/// <summary>
		/// 小数位数(最大3位)
		/// </summary>
		public int? Scale { get; set; }

		/// <summary>
		/// 后缀
		/// </summary>
		public string Unit { get; set; }

		/// <summary>
		/// 权重
		/// </summary>
		public decimal? Weight { get; set; }

		/// <summary>
		/// 是否必填(0-否,1-是)
		/// </summary>
		public int? IsMustDo { get; set; }

		/// <summary>
		/// 是否记忆(0-否,1-是)
		/// </summary>
		public int? IsRemember { get; set; }

		/// <summary>
		/// 强制校验(0-否,1-是)
		/// </summary>
		public int? IsVerify { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete { get; set; }


        #endregion

    }
}
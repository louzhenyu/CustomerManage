/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-9-19 17:06:04
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
    public partial class T_Menu_FunctionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Menu_FunctionEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String FunctionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Menu_ID { get; set; }

		/// <summary>
		/// 1=保存/新建   2=删除   3=搜索/查询按钮   
		/// </summary>
		public Int32? FunctionNo { get; set; }

		/// <summary>
		/// 可用于与控件名称映射，例如BtnAdd对应新增按钮
		/// </summary>
		public String FunctionCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String FunctionName { get; set; }

		/// <summary>
		/// 0=默认   1=查看权限   2=操作权限   
		/// </summary>
		public Int32? FunctionType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 0=正常状态；1=删除状态
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    public partial class T_CTW_LEventsEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CTW_LEventsEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String EventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EventLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String URL { get; set; }

		/// <summary>
		/// 1=有   0=??有
		/// </summary>
		public Int32? IsSubEvent { get; set; }

		/// <summary>
		/// 10=未开始   20=运行中   30=暂停   40=结束   
		/// </summary>
		public Int32? EventStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

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
		public Int32? DrawMethodId { get; set; }

		/// <summary>
		/// 000000      从左起第一位：是否需要注册 1=是，0=否   从左起第二位：是否需要签到 1=是，0=否   从左起第三位：是否需要验证 1=是，0=否   从左起第4位：是否需要补充抽奖机会   从左起第5位：是否判断在现场 1=是，0=否   从左起第6位：未开始抽奖时，是否需要提示1=是（提示信息：写死），0=否 （暂时不处理）   从左起第7位：抽  结束后，是否需要提示1=是（提示信息：写死），0=否      （暂时不处理）   从左起第8位：是否可以多次中奖，1=是（无限次），0=否（只能中一次奖）  （暂时不处理）   从左起第8位：是否可以多次中奖，1=是（无限次），0=否（只能中一次奖）  （暂时不处理）   从左起第9位：是否需要填问券，1=是，0=否  （暂时不处理）   
		/// </summary>
		public String EventFlag { get; set; }


        #endregion

    }
}
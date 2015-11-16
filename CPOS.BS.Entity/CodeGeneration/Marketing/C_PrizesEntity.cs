/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 15:07:16
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
    public partial class C_PrizesEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public C_PrizesEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Guid? PrizesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 1=赠送礼券   2=单笔现金消费额满N元送   3=笔储值充值额满N元送   4=单笔储值消费额满N元送   5=日总消费额度满N元送
		/// </summary>
		public Int32? PrizesType { get; set; }

		/// <summary>
		/// 取第一张券名称
		/// </summary>
		public String PrizesName { get; set; }

		/// <summary>
		/// 所有券的总数（预留）
		/// </summary>
		public Int32? Qty { get; set; }

		/// <summary>
		/// 所有券的剩余（预留）
		/// </summary>
		public Int32? RemainingQty { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? AmountLimit { get; set; }

		/// <summary>
		/// 0=否；1=是
		/// </summary>
		public Int32? IsAutoIncrease { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

		/// <summary>
		/// 0=否；1=是
		/// </summary>
		public Int32? IsCirculation { get; set; }

		/// <summary>
		/// 节前N天，转化成时间格式保存
		/// </summary>
		public DateTime? SendDate { get; set; }

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
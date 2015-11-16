/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/22 19:32:10
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
    public partial class VipCardTransLogEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardTransLogEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? TransID { get; set; }

		/// <summary>
		/// 会员编号
		/// </summary>
		public String VipCode { get; set; }

		/// <summary>
		/// 会员姓名
		/// </summary>
		public String VipName { get; set; }

		/// <summary>
		/// 卡号
		/// </summary>
		public String VipCardCode { get; set; }

		/// <summary>
		/// 卡类型
		/// </summary>
		public String VipCardTypeCode { get; set; }

		/// <summary>
		/// 门店编码
		/// </summary>
		public String UnitCode { get; set; }

		/// <summary>
		/// 门店名称
		/// </summary>
		public String UnitName { get; set; }

		/// <summary>
		/// 订单号
		/// </summary>
		public String OrderNo { get; set; }

		/// <summary>
		/// 单据号
		/// </summary>
		public String BillNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransAction { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TableNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Person { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? NewValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 交易类型
		/// </summary>
		public String TransType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String TransActionType { get; set; }

		/// <summary>
		/// 交易终端类型
		/// </summary>
		public String TransTerminalType { get; set; }

		/// <summary>
		/// 交易终端编码
		/// </summary>
		public String TransTerminalCode { get; set; }

		/// <summary>
		/// 交易地点
		/// </summary>
		public String TransAddress { get; set; }

		/// <summary>
		/// 交易内容
		/// </summary>
		public String TransContent { get; set; }

		/// <summary>
		/// 交易时间
		/// </summary>
		public DateTime? TransTime { get; set; }

		/// <summary>
		/// 交易金额
		/// </summary>
		public Decimal? TransAmount { get; set; }

		/// <summary>
		/// 交易后余额
		/// </summary>
		public Decimal? TransBalance { get; set; }

		/// <summary>
		/// 请求参数JSON
		/// </summary>
		public String RequestJSON { get; set; }

		/// <summary>
		/// 校验串
		/// </summary>
		public String CheckCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Operator { get; set; }

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
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? OldVipCardCode { get; set; }


        #endregion

    }
}
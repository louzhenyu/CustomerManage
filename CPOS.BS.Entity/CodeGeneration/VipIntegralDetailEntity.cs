/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/11 10:11:33
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
    /// ʵ�壺  
    /// </summary>
    public partial class VipIntegralDetailEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipIntegralDetailEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String VipIntegralDetailID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VIPID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SalesAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? Integral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String IntegralSourceID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String DeadlineDate { get; set; }

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
		public DateTime? EffectiveDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsAdd { get; set; }

		/// <summary>
		/// ��Դ��Ա��ʶ
		/// </summary>
		public String FromVipID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }


        #endregion

    }
}
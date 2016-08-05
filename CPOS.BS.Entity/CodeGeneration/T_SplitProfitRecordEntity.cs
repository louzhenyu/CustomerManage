/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:28:44
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
    public partial class T_SplitProfitRecordEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SplitProfitRecordEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String ID { get; set; }

		/// <summary>
		/// Amount  ���   Integral  ����
		/// </summary>
		public String SourceType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ObjectId { get; set; }

		/// <summary>
		/// Vip   User   Unit
		/// </summary>
		public String UserType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String UserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? SplitAmount { get; set; }

		/// <summary>
		/// 00 δ����   01 ����ɹ�   09 ����ʧ��
		/// </summary>
		public String SplitSattus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

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


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/10 18:14:44
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
    public partial class t_award_poolEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public t_award_poolEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String EventId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrizePoolsID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PrizesID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Balance { get; set; }


        #endregion

    }
}
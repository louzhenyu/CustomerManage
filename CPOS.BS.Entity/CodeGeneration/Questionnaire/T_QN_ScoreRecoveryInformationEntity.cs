/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:37
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
    public partial class T_QN_ScoreRecoveryInformationEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_QN_ScoreRecoveryInformationEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? ScoreRecoveryInformationID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String QuestionnaireID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MinScore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? MaxScore { get; set; }

		/// <summary>
		/// 1,����   2,ͼƬ
		/// </summary>
		public Int32? RecoveryType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RecoveryContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String RecoveryImg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

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
		/// 0,��   1,��
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
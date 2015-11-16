/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:25
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
    public partial class SysVipCardGradeEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SysVipCardGradeEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Int32? VipCardGradeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String VipCardGradeName { get; set; }

		/// <summary>
		/// ��Ա���ȼ�(����ֵ��С��ȷ���ȼ��ߵ�)
		/// </summary>
		public Int32? VipCardGrade { get; set; }

		/// <summary>
		/// �ۼƳ�ֵ
		/// </summary>
		public Decimal? AddUpAmount { get; set; }

		/// <summary>
		/// �Ƿ���չ����Ա
		/// </summary>
		public Int32? IsExpandVip { get; set; }

		/// <summary>
		/// ��ֵ�Ż�
		/// </summary>
		public Decimal? PreferentialAmount { get; set; }

		/// <summary>
		/// �����Ż�
		/// </summary>
		public Decimal? SalesPreferentiaAmount { get; set; }

		/// <summary>
		/// ���ֱ���
		/// </summary>
		public Decimal? IntegralMultiples { get; set; }

		/// <summary>
		/// ��Ϊ��Ա
		/// </summary>
		public String BeVip { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// �ڳ�����
		/// </summary>
		public Int32? BeginIntegral { get; set; }

		/// <summary>
		/// ��ĩ����
		/// </summary>
		public Int32? EndIntegral { get; set; }

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


        #endregion

    }
}
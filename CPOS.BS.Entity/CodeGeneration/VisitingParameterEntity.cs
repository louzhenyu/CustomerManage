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
    /// ʵ�壺 �ݷ�������ϸ(��������) 
    /// </summary>
    public partial class VisitingParameterEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VisitingParameterEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Guid? VisitingParameterID { get; set; }

		/// <summary>
		/// �������(1-��Ʒ���,2-Ʒ����ص�)
		/// </summary>
		public int? ParameterType { get; set; }

		/// <summary>
		/// ��������(�ɼ���������)
		/// </summary>
		public string ParameterName { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public string ParameterNameEn { get; set; }

		/// <summary>
		/// �ؼ�����
		/// </summary>
		public int? ControlType { get; set; }

		/// <summary>
		/// ����ѡ������(ͨ��Ϊ����ѡ������)
		/// </summary>
		public string ControlName { get; set; }

		/// <summary>
		/// ���ֵ
		/// </summary>
		public decimal? MaxValue { get; set; }

		/// <summary>
		/// ��Сֵ
		/// </summary>
		public decimal? MinValue { get; set; }

		/// <summary>
		/// ȱʡֵ
		/// </summary>
		public string DefaultValue { get; set; }

		/// <summary>
		/// С��λ��(���3λ)
		/// </summary>
		public int? Scale { get; set; }

		/// <summary>
		/// ��׺
		/// </summary>
		public string Unit { get; set; }

		/// <summary>
		/// Ȩ��
		/// </summary>
		public decimal? Weight { get; set; }

		/// <summary>
		/// �Ƿ����(0-��,1-��)
		/// </summary>
		public int? IsMustDo { get; set; }

		/// <summary>
		/// �Ƿ����(0-��,1-��)
		/// </summary>
		public int? IsRemember { get; set; }

		/// <summary>
		/// ǿ��У��(0-��,1-��)
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
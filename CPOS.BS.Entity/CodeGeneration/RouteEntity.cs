/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/7 11:51:59
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
    /// ʵ�壺 �ݷ�·�߶��� 
    /// </summary>
    public partial class RouteEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public RouteEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �������
		/// </summary>
		public Guid? RouteID { get; set; }

		/// <summary>
		/// ·�߱��
		/// </summary>
		public string RouteNo { get; set; }

		/// <summary>
		/// ·������
		/// </summary>
		public string RouteName { get; set; }

		/// <summary>
		/// ·��״̬(1-����,2-ֹͣ)
		/// </summary>
		public int? Status { get; set; }

		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// �ն�����(1-�ŵ�,2-������)
		/// </summary>
		public int? POPType { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public decimal? Distance { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? TripMode { get; set; }

		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark { get; set; }

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
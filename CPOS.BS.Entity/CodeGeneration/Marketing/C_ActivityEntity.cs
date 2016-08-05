/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/7/13 11:56:02
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
    public partial class C_ActivityEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public C_ActivityEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public Guid? ActivityID { get; set; }

		/// <summary>
		/// 1=���ջ   2=��ͨ�
		/// </summary>
		public Int32? ActivityType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ActivityName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// 0=���ǣ�1=��
		/// </summary>
		public Int32? IsLongTime { get; set; }

		/// <summary>
		/// ����0��ʾ���úͱ���
		/// </summary>
		public Decimal? PointsMultiple { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendCouponQty { get; set; }

		/// <summary>
		/// 0=������   1=��ͣ��   δ��ʼ�������кͽ���״̬�����ݿ�ʼʱ��ͽ���ʱ���ж�   (1=��ͣ��δ��ʼ=2��������=3���ѽ���=4��)
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 0=���ǣ�1=��
		/// </summary>
		public Int32? IsAllVipCardType { get; set; }

		/// <summary>
		/// 0=���ǣ�1=��
		/// </summary>
		public Int32? IsVipGrouping { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? TargetCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Remark { get; set; }

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
		/// 0=����״̬��1=ɾ��״̬
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    public partial class T_CTW_LEventsEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_CTW_LEventsEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String EventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? EventLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ParentEventID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ImageURL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String URL { get; set; }

		/// <summary>
		/// 1=��   0=??��
		/// </summary>
		public Int32? IsSubEvent { get; set; }

		/// <summary>
		/// 10=δ��ʼ   20=������   30=��ͣ   40=����   
		/// </summary>
		public Int32? EventStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }

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
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DrawMethodId { get; set; }

		/// <summary>
		/// 000000      �������һλ���Ƿ���Ҫע�� 1=�ǣ�0=��   ������ڶ�λ���Ƿ���Ҫǩ�� 1=�ǣ�0=��   ���������λ���Ƿ���Ҫ��֤ 1=�ǣ�0=��   �������4λ���Ƿ���Ҫ����齱����   �������5λ���Ƿ��ж����ֳ� 1=�ǣ�0=��   �������6λ��δ��ʼ�齱ʱ���Ƿ���Ҫ��ʾ1=�ǣ���ʾ��Ϣ��д������0=�� ����ʱ������   �������7λ����  �������Ƿ���Ҫ��ʾ1=�ǣ���ʾ��Ϣ��д������0=��      ����ʱ������   �������8λ���Ƿ���Զ���н���1=�ǣ����޴Σ���0=��ֻ����һ�ν���  ����ʱ������   �������8λ���Ƿ���Զ���н���1=�ǣ����޴Σ���0=��ֻ����һ�ν���  ����ʱ������   �������9λ���Ƿ���Ҫ����ȯ��1=�ǣ�0=��  ����ʱ������   
		/// </summary>
		public String EventFlag { get; set; }


        #endregion

    }
}
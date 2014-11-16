/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/3 18:46:09
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
    public partial class GLServicePersonInfoEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public GLServicePersonInfoEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������ÿͷ���
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// �ֻ�
		/// </summary>
		public String Mobile { get; set; }

		/// <summary>
		/// ͷ��
		/// </summary>
		public String Picture { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public Int32? Star { get; set; }

		/// <summary>
		/// ��ɵ���
		/// </summary>
		public Int32? OrderCount { get; set; }

		/// <summary>
		/// CustomerID
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// ������ʱ��
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// ɾ����ʶ
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
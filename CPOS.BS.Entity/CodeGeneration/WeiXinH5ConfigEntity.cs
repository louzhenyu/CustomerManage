/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/20 11:37:39
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
    /// ʵ�壺 ΢��Html5��ҳ��������Ϣ 
    /// </summary>
    public partial class WeiXinH5ConfigEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WeiXinH5ConfigEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// ��������
		/// </summary>
		public Int32? ConfigID { get; set; }

		/// <summary>
		/// �ͻ�ID
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// �����ļ�·��
		/// </summary>
		public String ConfigFilePath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

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


        #endregion

    }
}
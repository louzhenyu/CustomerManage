/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-27 19:35:02
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
    public partial class VipCardStatusChangeLogEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardStatusChangeLogEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		public String LogID { get; set; }

		/// <summary>
		/// ��Ա����ʶ
		/// </summary>
		public String VipCardID { get; set; }

		/// <summary>
		/// ��Ա��״̬��ʶ
		/// </summary>
		public Int32? VipCardStatusID { get; set; }

		/// <summary>
		/// �ŵ��ʶ
		/// </summary>
		public String UnitID { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public String Action { get; set; }

		/// <summary>
		/// ԭ��
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// ͼƬURL
		/// </summary>
		public String PicUrl { get; set; }

		/// <summary>
		/// ԭ״̬��ʶ
		/// </summary>
		public Int32? OldStatusID { get; set; }

		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark { get; set; }

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
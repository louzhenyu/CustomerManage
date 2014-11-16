/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
    public partial class EclubVipBookMarkEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EclubVipBookMarkEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
        /// ����ID
		/// </summary>
		public Guid? VipBookMarkID { get; set; }

		/// <summary>
        /// �û�ID
		/// </summary>
		public String VipID { get; set; }

		/// <summary>
        /// �ղ�����1Ϊ�ղ�У��
		/// </summary>
		public Int32? BookMarkType { get; set; }

		/// <summary>
        /// У��ID
		/// </summary>
		public String ObjectID { get; set; }

		/// <summary>
        /// �ղ�����
		/// </summary>
		public String Description { get; set; }

		/// <summary>
        /// �ͻ�ID
		/// </summary>
		public String CustomerId { get; set; }

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
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:35:19
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
    /// ʵ�壺 Ʒ�� 
    /// </summary>
    public partial class BrandEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public BrandEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// �Զ����
		/// </summary>
		public Int32? BrandID { get; set; }

		/// <summary>
		/// Ʒ�Ʊ��
		/// </summary>
		public String BrandNo { get; set; }

		/// <summary>
		/// Ʒ������
		/// </summary>
		public String BrandName { get; set; }

		/// <summary>
		/// Ʒ������(Ӣ��)
		/// </summary>
		public String BrandNameEn { get; set; }

		/// <summary>
		/// �Ƿ�����Ʒ��(0-��,1-��)
		/// </summary>
		public Int32? IsOwner { get; set; }

		/// <summary>
		/// ������˾
		/// </summary>
		public String Firm { get; set; }

		/// <summary>
		/// Ʒ�Ƶȼ�
		/// </summary>
		public Int32? BrandLevel { get; set; }

		/// <summary>
		/// �Ƿ�Ҷ�ӽڵ�(0-��,1-��)
		/// </summary>
		public Int32? IsLeaf { get; set; }

		/// <summary>
		/// �ϼ�Ʒ�Ʊ�ʶ
		/// </summary>
		public Int32? ParentID { get; set; }

		/// <summary>
		/// ��ע
		/// </summary>
		public String Remark { get; set; }

		/// <summary>
		/// �ͻ���ʶ
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// �ͻ������̱�ʶ
		/// </summary>
		public Int32? ClientDistributorID { get; set; }

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
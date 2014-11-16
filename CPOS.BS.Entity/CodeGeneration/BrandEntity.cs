/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:32
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
        public int? BrandID { get; set; }

        /// <summary>
        /// Ʒ�Ʊ��
        /// </summary>
        public string BrandNo { get; set; }

        /// <summary>
        /// Ʒ������
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Ʒ������(Ӣ��)
        /// </summary>
        public string BrandNameEn { get; set; }

        /// <summary>
        /// �Ƿ�����Ʒ��(0-��,1-��)
        /// </summary>
        public int? IsOwner { get; set; }

        /// <summary>
        /// ������˾
        /// </summary>
        public string Firm { get; set; }

        /// <summary>
        /// Ʒ�Ƶȼ�
        /// </summary>
        public int? BrandLevel { get; set; }

        /// <summary>
        /// �Ƿ�Ҷ�ӽڵ�(0-��,1-��)
        /// </summary>
        public int? IsLeaf { get; set; }

        /// <summary>
        /// �ϼ�Ʒ�Ʊ��
        /// </summary>
        public int? ParentID { get; set; }

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
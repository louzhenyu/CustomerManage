/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:15
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
    public partial class T_Inout_DetailEntity : BaseEntity
    {
        #region ���Լ�
        /// <summary>
        /// ��ƷID
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        /// <summary>
        /// SKU ID
        /// </summary>
        public string SKUID { get; set; }
        public string SkuCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public string SpecificationDesc { get; set; }
        /// <summary>
        /// ʵ�ʵ���
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// SKUͼƬ
        /// </summary>
        public string ImageUrl { get; set; }
        #endregion
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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
    public partial class SysVipCardTypeEntity : BaseEntity 
    {
        #region ���Լ�
        public Int32? RuleID { get; set; }

        /// <summary>
        /// Ʒ�Ʊ�ʶ
        /// </summary>
        public String BrandID { get; set; }

        /// <summary>
        /// ���ۿ�
        /// </summary>
        public Decimal? CardDiscount { get; set; }

        /// <summary>
        /// ���ֱ���
        /// </summary>
        public Int32? PointsMultiple { get; set; }

        /// <summary>
        /// ��ֵ��n
        /// </summary>
        public Decimal? ChargeFull { get; set; }

        /// <summary>
        /// ��ֵ��n
        /// </summary>
        public Decimal? ChargeGive { get; set; }

        /// <summary>
        /// ����������1=����������0=��ֵ������
        /// </summary>
        public int RefillCondition { get; set; }
        #endregion
    }
}
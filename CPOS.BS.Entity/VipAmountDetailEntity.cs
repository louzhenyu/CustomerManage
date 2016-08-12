/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 11:40:34
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
    public partial class VipAmountDetailEntity : BaseEntity
    {
        #region ���Լ�
        public string AmountSourceName { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// �ֹ�����ʱ����ʾ������
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// ��ӦAmount
        /// </summary>
        public decimal UpdateCount { get; set; }
        /// <summary>
        /// ��ӦCreateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ��ӦRemark
        /// </summary>
        public string UpdateReason { get; set; }
        #endregion
    }

    public partial class SumVipAmountDetailEntity
    {
        /// <summary>
        /// ��ǰ���
        /// </summary>
        public decimal? IncomeAmount { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public decimal? ExpenditureAmount { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int TotalAmount { get; set; }
    }
}
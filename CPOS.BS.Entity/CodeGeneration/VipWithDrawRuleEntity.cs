/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 14:06:57
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
using System.Collections;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// ʵ�壺  
    /// </summary>
    public partial class VipWithDrawRuleEntity : BaseEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipWithDrawRuleEntity()
        {
        }
        #endregion

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        public Guid? VipWithDrawRuleId { get; set; }

        /// <summary>
        /// 0��ʾû���˻���
        /// </summary>
        public Int32? BeforeWithDrawDays { get; set; }

        /// <summary>
        /// 0��ʾû���������
        /// </summary>
        public Decimal? MinAmountCondition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? WithDrawMaxAmount { get; set; }

        /// <summary>
        /// 0��������   1����   2����   3����
        /// </summary>
        public Int32? WithDrawNumType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? WithDrawNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

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


        #endregion

    }
}
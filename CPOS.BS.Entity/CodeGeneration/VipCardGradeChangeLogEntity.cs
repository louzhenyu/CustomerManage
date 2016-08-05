/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:28:44
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
    public partial class VipCardGradeChangeLogEntity : BaseEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardGradeChangeLogEntity()
        {
        }
        #endregion

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        public String ChangeLogID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? VipCardUpgradeRuleId { get; set; }

        /// <summary>
        /// SalesCard ����   Recharge ��ֵ
        /// </summary>
        public String OrderType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrderId { get; set; }

        /// <summary>
        /// ��Ա����ʶ
        /// </summary>
        public String VipCardID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ChangeBeforeVipCardID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ChangeBeforeGradeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? NowGradeID { get; set; }

        /// <summary>
        /// Upgrade
        /// </summary>
        public String ChangeReason { get; set; }

        /// <summary>
        /// ��������(1=�Զ�,2=�ֶ���
        /// </summary>
        public Int32? OperationType { get; set; }

        /// <summary>
        /// �䶯ʱ��
        /// </summary>
        public DateTime? ChangeTime { get; set; }

        /// <summary>
        /// �����ŵ�
        /// </summary>
        public String UnitID { get; set; }

        /// <summary>
        /// ����Ա��
        /// </summary>
        public String OperationUserID { get; set; }

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
        /// �ͻ���ʶ
        /// </summary>
        public String CustomerID { get; set; }


        #endregion

    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/11 9:54:08
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
    /// ʵ�壺 �ݷüƻ���(����·��,���ڵ�����) 
    /// </summary>
    public class CallDayPlanningViewEntity_POP : BaseEntity
    {
        
        /// <summary>
        /// �Զ����
        /// </summary>
        public Guid? MappingID { get; set; }

        /// <summary>
        /// �ݷ�����
        /// </summary>
        public DateTime? CallDate { get; set; }

        /// <summary>
        /// ��Ա���(����ClientUser��)
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// �ŵ�ID
        /// </summary>
        public Guid? StoreID { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        public int? DistributorID { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public int? IsDelete { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public int? Sequence { get; set; }
    }
}
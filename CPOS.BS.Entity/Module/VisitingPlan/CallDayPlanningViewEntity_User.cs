/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/28 10:53:56
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
    public class CallDayPlanningViewEntity_User : BaseEntity 
    {
        /// <summary>
        /// �û��Զ����
        /// </summary>
        public int? ClientUserID { get; set; }

        /// <summary>
        /// �û����
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string StructureName { get; set; }

        /// <summary>
        /// �ն���
        /// </summary>
        public int POPCount { get; set; }

        /// <summary>
        /// ְλ��
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// ����ID������ѯ��
        /// </summary>
        public Guid? ClientStructureID { get; set; }
        /// <summary>
        /// ְλID������ѯ��
        /// </summary>
        public int? ClientPositionID { get; set; }

        /// <summary>
        /// ִ���·ݣ�����ѯ��
        /// </summary>
        public DateTime? CallDate { get; set; }
    }
}
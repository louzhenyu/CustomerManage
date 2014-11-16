/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 15:52:20
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
    public partial class ESalesEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// �����ͻ�
        /// </summary>
        public string EnterpriseCustomerName { get; set; }
        /// <summary>
        /// ���۲�Ʒ
        /// </summary>
        public string SalesProductName { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        public string ECSourceName { get; set; }
        /// <summary>
        /// �׶�
        /// </summary>
        public string StageName { get; set; }
        /// <summary>
        /// ���۸�����
        /// </summary>
        public string SalesVipName { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        public IList<ESalesVisitVipMappingEntity> ESalesVisitVipMappingList { get; set; }
        public IList<string> ESalesVisitVipMappingIds { get; set; }
        #endregion
    }
}
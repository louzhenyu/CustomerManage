/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 17:25:36
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
    public partial class vwItemPEventDetailEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ��������
        /// </summary>
        public string ServiceDescription{get;set;}
        /// <summary>
        /// ��Ʒҵ������ʶ(1=�ţ�2=�䣻3=ȯ)
        /// </summary>
        public int ItemSortId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int SalesCount { get; set; }
        #endregion
    }
}
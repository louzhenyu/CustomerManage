/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/15 15:26:36
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
    public partial class TItemTagEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// TreeGrid��Ʒ�����Ҫ����(jifeng.cao)
        /// </summary>

        /// <summary>
        /// �ڵ��Ƿ�չ��
        /// </summary>
        public bool? expanded { get; set; }
        /// <summary>
        /// �Ƿ�Ҷ�ӽڵ�
        /// </summary>
        public bool leaf { get; set; }
        /// <summary>
        /// �Ƿ�ѡ
        /// </summary>
        public bool? @checked { get; set; }
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public int? IsFirstVisit { get; set; }
        /// <summary>
        /// �ӽڵ�
        /// </summary>
        public IList<TItemTagEntity> children = new List<TItemTagEntity>();
        #endregion
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:29
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
    public partial class VipExpandEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// CarBarndName
        /// </summary>
        public string CarBarndName { get; set; }
        /// <summary>
        /// ��Ʒ��
        /// </summary>
        public string CarBrandName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string CarModelsName { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        public IList<VipExpandEntity> VipExpandInfoList { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// ҳ������
        /// </summary>
        public int maxRowCount { get; set; }
        /// <summary>
        /// ��ʼ�к�
        /// </summary>
        public int startRowIndex { get; set; }

        #endregion
    }
}
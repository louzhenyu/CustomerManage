/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 17:54:09
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
    public partial class ObjectEvaluationEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ��Ա����
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// ����ͳ��
        /// </summary>
        public int Count { get; set; }

        public string HeadImgUrl { get; set; }
        #endregion
    }
}
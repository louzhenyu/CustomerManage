/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-13 14:00:31
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
    public partial class InnerGroupNewsEntity : BaseEntity 
    {
        #region ���Լ�
        public string IsRead { get; set; }   //�Ƿ��Ѷ�
        public int PageIndex { get; set; } //����ҳ����
        #endregion
    }
}
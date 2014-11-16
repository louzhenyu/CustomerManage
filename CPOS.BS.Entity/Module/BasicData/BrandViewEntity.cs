/*
 * Author		:zhongbao.xiao
 * EMail		:zhongbao.xiao@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/6 10:02:54
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
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// ʵ�壺 Ʒ�� 
    /// </summary>
    public partial class BrandViewEntity : BrandEntity 
    {
        #region ���Լ�

        public string ParentBrandName { get; set; }
        public string BrandCompany { get; set; }
        /// <summary>
        /// OptionText
        /// </summary>
        public string OptionText { get; set; }
        /// <summary>
        /// pOptionText
        /// </summary>
        public string pOptionText { get; set; }
        /// <summary>
        /// ���Ų�Ʒ���������ID
        /// </summary>
        public int? DetailID { get; set; }

        /// <summary>
        /// ���Ų�ƷID
        /// </summary>
        public Guid? ClientStructureSKUID { get; set; }
        #endregion
    }
}
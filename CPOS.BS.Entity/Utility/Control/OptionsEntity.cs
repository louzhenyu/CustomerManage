/*
 * Author		:tiansheng.zhu
 * EMail		:tiansheng.zhu@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/1 15:59:57
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
    /// ʵ�壺 ControlOptionsEntity 
    /// </summary>
    public class ControlOptionsEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ControlOptionsEntity()
        {
        }
        #endregion     
        #region ���Լ�
		/// <summary>
		/// OptionValue
		/// </summary>
		public int? OptionValue { get; set; }

		/// <summary>
		/// OptionText
		/// </summary>
		public string OptionText { get; set; }

        /// <summary>
        /// OptionTextEn
        /// </summary>
        public string OptionTextEn { get; set; }
        #endregion
    }
}
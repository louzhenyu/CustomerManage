/*
 * Author		:tiansheng.zhu
 * EMail		:tiansheng.zhu@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/1 18:46:58
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
    /// ʵ�壺 ControlBrandEntity 
    /// </summary>
    public class ControlBrandEntity : BaseEntity 
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ControlBrandEntity()
        {
        }
        #endregion     
        #region ���Լ�
		/// <summary>
		/// BrandID
		/// </summary>
		public int? BrandID { get; set; }

		/// <summary>
		/// BrandName
		/// </summary>
		public string BrandName { get; set; }

		/// <summary>
		/// ParentID
		/// </summary>
		public int? ParentID { get; set; }


        /// <summary>
        /// IsLeaf
        /// </summary>
        public int? IsLeaf { get; set; }
        #endregion
    }
}
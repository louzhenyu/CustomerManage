/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
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
    public partial class LPrizeWinnerEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// �н�������
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string PrizeDesc { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int ICount { get; set; }

        public IList<LPrizeWinnerEntity> PrizeWinnerList { get; set; }
        #endregion
    }
}
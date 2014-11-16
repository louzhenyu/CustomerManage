/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    public partial class MarketEventResponseEntity : BaseEntity 
    {
        #region ���Լ�
        //����
        public string VipCode { get; set; }
        //�ȼ�
        public string VipLevel { get; set; }
        //����
        public string VipName { get; set; }
        //ͳ��ʱ��
        public string StatisticsTime { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// ��Ӧ��Ⱥ����
        /// </summary>
        public IList<MarketEventResponseEntity> MarketEventResponseInfoList { get; set; }
        #endregion
    }
}
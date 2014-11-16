/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:38
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
    public partial class MarketEventEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// Ʒ������
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// ״̬����
        /// </summary>
        public string StatusDesc { get; set; }
        /// <summary>
        /// ��Լ����
        /// </summary>
        //public string TemplateContent { get; set; }
        /// <summary>
        /// �ģ��
        /// </summary>
        public MarketTemplateEntity MarketTemplageInfo { get; set; }
        /// <summary>
        /// ����μ���
        /// </summary>
        public IList<MarketWaveBandEntity> MarketWaveBandInfoList { get; set; }
        /// <summary>
        /// ��ŵ꼯��
        /// </summary>
        public IList<MarketStoreEntity> MarketStoreInfoList { get; set; }
        /// <summary>
        /// ��������Ϣ��
        /// </summary>
        public IList<MarketPersonEntity> MarketPersonInfoList { get; set; }
        /// <summary>
        /// ���μ���
        /// </summary>
        public IList<MarketWaveBandEntity> MarketWaveBandList { get; set; }

        public string EventModeDesc { get; set; }
        #endregion
    }
}
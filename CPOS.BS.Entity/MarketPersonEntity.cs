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
    public partial class MarketPersonEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ������
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// ��Ⱥ����
        /// </summary>
        public IList<MarketPersonEntity> MarketPersonInfoList { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Int64 DisplayIndex { get; set; }

        public IList<VipEntity> vipInfoList { get; set; }

        public string VipCode { get; set; }
        public int VipLevel { get; set; }
        public string VipName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WeiXin { get; set; }
        public decimal Integration { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int PurchaseCount { get; set; }
        /// <summary>
        /// �Ա� ����ѯ��1=�У�2=Ů
        /// </summary>
        public Int32? Gender { get; set; }
        /// <summary>
        /// �������� ����ѯ��
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// �����ڵ���ҵ���� ����ѯ��
        /// </summary>
        public string Enterprice { get; set; }

        /// <summary>
        /// ��ҵ�ж��ټ������ŵ� ����ѯ��
        /// </summary>
        public string IsChainStores { get; set; }

        /// <summary>
        /// ���Ƿ��΢��Ӫ������Ȥ ����ѯ��
        /// </summary>
        public string IsWeiXinMarketing { get; set; }
        /// <summary>
        /// �Ա� 
        /// </summary>
        public string GenderInfo { get; set; }
        #endregion
    }
}
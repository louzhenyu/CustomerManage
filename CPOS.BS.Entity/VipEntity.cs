/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:45:29
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
    public partial class VipEntity : BaseEntity
    {
        #region ���Լ�
        /// <summary>
        /// �к�
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int ICount { get; set; }

        /// <summary>
        /// VIP����
        /// </summary>
        public IList<VipEntity> vipInfoList { get; set; }
        /// <summary>
        /// ��Դ
        /// </summary>
        public string VipSourceName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string VipLevelDesc { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        public string StatusDesc { get; set; }
        /// <summary>
        /// ��������ŵ�
        /// </summary>
        public string LastUnit { get; set; }
        /// <summary>
        /// ���ϼ����׻���
        /// </summary>
        public decimal IntegralForHightUser { get; set; }

        /// <summary>
        /// �������� ����ѯ��
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ����ԱID
        /// </summary>
        public string UserId { get; set; }

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

        /// <summary>
        /// ��������
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// �·� 
        /// </summary>
        public string yearMonth { get; set; }
        /// <summary>
        /// �·�������Ա���� 
        /// </summary>
        public int vipAddupCount { get; set; }
        /// <summary>
        /// ���� 
        /// </summary>
        public decimal vipMonthMoM { get; set; }
        /// <summary>
        /// ���� 
        /// </summary>
        public Int64 displayIndex { get; set; }
        /// <summary>
        /// ������� 
        /// </summary>
        public int vipVisitantCount { get; set; }
        /// <summary>
        /// ����������� 
        /// </summary>
        public decimal vipVisitantMonthMoM { get; set; }
        /// <summary>
        /// ΢�Ź�ע���� 
        /// </summary>
        public int vipWeiXinAddupCount { get; set; }
        /// <summary>
        /// ΢�Ź�ע���� 
        /// </summary>
        public int vipWeiXinMonthMoM { get; set; }
        /// <summary>
        /// �Ἦ��
        /// </summary>
        public string MembershipShop { get; set; }
        /// <summary>
        /// ��ˮ��
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// ��д�ı�ǩ����
        /// </summary>
        public string VipTagsShort { get; set; }

        /// <summary>
        /// �����ı�ǩ����
        /// </summary>
        public string VipTagsLong { get; set; }
        /// <summary>
        /// ��ǰ��ѯ�Ļ��֣������ǻ�Ա�ģ������ǵ���Ա�ģ��������ŵ�ģ�
        /// </summary>
        public string SearchIntegral { get; set; }
        /// <summary>
        /// �ŵ��Ա����
        /// </summary>
        public int UnitCount { get; set; }
        /// <summary>
        /// �ŵ����۽��
        /// </summary>
        public string UnitSalesAmount { get; set; }
        /// <summary>
        /// �ŵ��ʶ
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// �������ͣ���ѡ���ö��ŷָ�
        /// </summary>
        public string IntegralSourceIds { get; set; }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public string BeginDate { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// ҳ��
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// ҳ������
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// ����Ա�ƽ�����
        /// </summary>
        public int VipCount { get; set; }
        /// <summary>
        /// ��ѯ���
        /// </summary>
        public string SearchAmount { get; set; }
        /// <summary>
        /// 1δ����
        /// </summary>
        public int Status1 { get; set; }
        /// <summary>
        /// 0��ȡ��
        /// </summary>
        public int Status0 { get; set; }
        /// <summary>
        /// 2������
        /// </summary>
        public int Status2 { get; set; }
        /// <summary>
        /// 3�ѷ���
        /// </summary>
        public int Status3 { get; set; }
        /// <summary>
        /// ��Ա��ǩ����
        /// </summary>
        public int VipTagsCount { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Distance { get; set; }

        #endregion
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
    public partial class VipCardEntity : BaseEntity 
    {
        #region ���Լ�
        /// <summary>
        /// ��Ա��
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public string VipRealName { get; set; }
        /// <summary>
        /// �Ἦ��
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// �Ἦ��ID
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public string VipCardGradeName { get; set; }
        /// <summary>
        /// ��״̬
        /// </summary>
        public string VipStatusName { get; set; }
        /// <summary>
        /// VipId
        /// </summary>
        public string VipId { get; set; }
        /// <summary>
        /// ��Ա���������
        /// </summary>
        public string VipCardTypeName { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public Int64 DisplayIndex { get; set; }
        /// <summary>
        /// ������Ա������
        /// </summary>
        public IList<VipCardEntity> VipCardInfoList { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// ҳ������
        /// </summary>
        public int maxRowCount { get; set; }
        /// <summary>
        /// ��ʼ�к�
        /// </summary>
        public int startRowIndex { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string CarCode { get; set; }
        /// <summary>
        /// ״̬����
        /// </summary>
        public string VipCardStatusCode { get; set; }
        
        /// <summary>
        /// ��Ա�ֻ�����
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// ��Ա��״̬����
        /// </summary>
        public string VipCardStatusName { get; set; }
        /// <summary>
        /// ��Ա������ͼƬUrl
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// ��Ա���
        /// </summary>
        public string VipCode { get; set; }
        /// <summary>
        /// ��Ա����
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// ��Ա�Ա�
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// ��ԱID
        /// </summary>
        public string VIPID { get; set; }
        /// <summary>
        /// ��Ա��״̬�����¼����
        /// </summary>
        public List<VipCardStatusChangeLogEntity> StatusLogList { get; set; }
        #endregion
    }
}
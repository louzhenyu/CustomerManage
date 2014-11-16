/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
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
    public partial class LEventsEntity : BaseEntity
    {
        #region ���Լ�

        public string MobileModuleID { get; set; }

        /// <summary>
        /// �ѹ�ע��Ա
        /// </summary>
        public int hasVipCount { get; set; }
        /// <summary>
        /// �²ɼ���Ա
        /// </summary>
        public int newVipCount { get; set; }
        /// <summary>
        /// ���¶�����
        /// </summary>
        public int hasOrderCount { get; set; }
        /// <summary>
        /// �Ѹ������
        /// </summary>
        public int hasPayCount { get; set; }
        /// <summary>
        /// �����۶�����
        /// </summary>
        public decimal hasSalesAmount { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int signUpCount { get; set; }
        /// <summary>
        /// ���۽��
        /// </summary>
        public decimal salesAmount { get; set; }
        /// <summary>
        /// ���뿪ʼ��������
        /// </summary>
        public int distanceDays { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public IList<LEventsEntity> EventList { get; set; }


        /// <summary>
        /// ���м�����ʼ��������ڽ����У��ͷ���0
        /// </summary>
        public int IntervalDays { get; set; }

        /// <summary>
        /// ��ǰ�û��������״̬��
        /// 0��δ������1��������������2���ѱ�����ͨ������
        /// </summary>
        public int ApplyStatus { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// �û�ı����ʾ�ID��0��ʾû���ʾ�
        /// </summary>
        public string QuestionaireID { get; set; }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public int CanApply { get; set; }
        /// <summary>
        /// �Ƿ���������ԱЭ��ǩ��
        /// </summary>
        public int CanAdminCheckin { get; set; }
        /// <summary>
        /// ����δ������δ��ʼ�Ļ
        /// </summary>
        public int ButtonType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ButtonText { get; set; }
        /// <summary>
        /// ��ǰ�û��Ա���ĸ���״̬
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// ǩ��״̬
        /// </summary>
        public int CheckinStatus { get; set; }
        /// <summary>
        /// ���ʼʱ��
        /// </summary>
        public DateTime StartTimeStr { get; set; }
        /// <summary>
        /// �����ʱ��
        /// </summary>
        public DateTime EndTimeStr { get; set; }
        /// <summary>
        /// ��ʼʱ��ʱ���
        /// </summary>
        public long StartTimeUnix { get; set; }
        /// <summary>
        /// ����ʱ��ʱ���
        /// </summary>
        public long EndTimeUnix { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string EventTypeName { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// ��ʼʱ���ַ���
        /// </summary>
        public string StartTimeText { get; set; }
        /// <summary>
        /// ��ֹʱ���ַ���
        /// </summary>
        public string EndTimeText { get; set; }
        ///// <summary>
        ///// ��ʼʱ��
        ///// </summary>
        //public DateTime StartTime { get; set; }
        /////// <summary>
        /////// ��ֹʱ��
        /////// </summary>
        ////public DateTime EndTime { get; set; }

        /// <summary>
        /// ������Ա����
        /// </summary>
        public int AppliesCount { get; set; }
        /// <summary>
        /// ǩ����Ա����
        /// </summary>
        public int CheckinsCount { get; set; }
        /// <summary>
        /// ����ƽ̨
        /// </summary>
        public string WeiXinPublicID { get; set; }
        /// <summary>
        /// EventType
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        ///��Ʒ����
        /// </summary>
        public int PrizesCount { get; set; }
        /// <summary>
        ///�������
        /// </summary>
        public string ParentEventTitle { get; set; }
        /// <summary>
        /// ΢�Ź̶���ά�����
        /// </summary>
        public string QRCodeTypeId { get; set; }
        /// <summary>
        /// �ѱ�����������У�ջר�á�
        /// </summary>
        public int HaveSignedCount { get; set; }
        /// <summary>
        /// �Ƿ�������У�ջר�á�
        /// </summary>
        public int IsSignUp { get; set; }
        /// <summary>
        /// ���Ա��������� ����У�ջר�á�
        /// </summary>
        public int AllowCount { get; set; }
        /// <summary>
        /// ʣ������ ����У�ջר�á�
        /// </summary>
        public int OverCount { get; set; }
        /// <summary>
        /// ΢�Ź̶���ά��
        /// </summary>
        public string WXCode { get; set; }
        /// <summary>
        /// ΢�Ź̶���ά��ͼƬ����
        /// </summary>
        public string WXCodeImageUrl { get; set; }
        /// <summary>
        /// �ִ�����
        /// </summary>
        public int RoundCount { get; set; }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        public List<WMenuMTextMappingEntity> listMenutextMapping { set; get; }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        public List<WMaterialTextEntity> listMenutext { set; get; }

        /// <summary>
        /// �齱��ʽ
        /// </summary>
        public int? DrawMethod { set; get; }


        public string MaxWQRCod { set; get; }
        #endregion
    }
}
/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/30 11:11:11
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
    public partial class StoreBrandMappingEntity : BaseEntity 
    {
        #region ���Լ�

        public string BrandName { get; set; }       //Ʒ������
        public string BrandEngName { get; set; }    //Ʒ��Ӣ����
        public string StoreName { get; set; }       //�ŵ�����
        public Int64  DisplayIndex { get; set; }    //���
        public string Address { get; set; }
        public string Tel { get; set; }             //�ͷ��绰
        public string Fax { get; set; }            
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IList<ObjectImagesEntity> ImageList { get; set; } //�ŵ�ͼƬ����

        public string ImageUrl { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public Double Distance { get; set; }

        public IList<StoreBrandMappingEntity> StoreBrandList { get; set; }

        public int TotalCount { get; set; }
        /// <summary>
        /// �ŵ�����
        /// </summary>
        public string UnitRemark { get; set; }
        /// <summary>
        /// �ŵ�����˵��
        /// </summary>
        public string UnitTypeContent { get; set; }
        /// <summary>
        /// �۸���ʼ
        /// </summary>
        public string MinPrice { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string SupportingContent { get; set; }
        /// <summary>
        /// �ȵ�
        /// </summary>
        public string HotContent { get; set; }
        /// <summary>
        /// �Ƶ���ϸ����
        /// </summary>
        public string IntroduceContent { get; set; }
        /// <summary>
        /// �Ǽ�
        /// </summary>
        public string StarLevel { get; set; }
        /// <summary>
        /// �Ƶ�����
        /// </summary>
        public string HotelType { get; set; }
        /// <summary>
        /// �˾�
        /// </summary>
        public string PersonAvg { get; set; }
        /// <summary>
        /// �����ŵ��б�����
        /// </summary>
        public int OtherUnitCount { get; set; }

        /// <summary>
        /// �Ƿ��ṩԤԼ����
        /// </summary>
        public int? IsApp { get; set; }
        #endregion
    }
}
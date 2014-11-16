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
    /// 实体：  
    /// </summary>
    public partial class StoreBrandMappingEntity : BaseEntity 
    {
        #region 属性集

        public string BrandName { get; set; }       //品牌名称
        public string BrandEngName { get; set; }    //品牌英文名
        public string StoreName { get; set; }       //门店名称
        public Int64  DisplayIndex { get; set; }    //序号
        public string Address { get; set; }
        public string Tel { get; set; }             //客服电话
        public string Fax { get; set; }            
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IList<ObjectImagesEntity> ImageList { get; set; } //门店图片集合

        public string ImageUrl { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public Double Distance { get; set; }

        public IList<StoreBrandMappingEntity> StoreBrandList { get; set; }

        public int TotalCount { get; set; }
        /// <summary>
        /// 门店描述
        /// </summary>
        public string UnitRemark { get; set; }
        /// <summary>
        /// 门店类型说明
        /// </summary>
        public string UnitTypeContent { get; set; }
        /// <summary>
        /// 价格起始
        /// </summary>
        public string MinPrice { get; set; }
        /// <summary>
        /// 配套
        /// </summary>
        public string SupportingContent { get; set; }
        /// <summary>
        /// 热点
        /// </summary>
        public string HotContent { get; set; }
        /// <summary>
        /// 酒店详细介绍
        /// </summary>
        public string IntroduceContent { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public string StarLevel { get; set; }
        /// <summary>
        /// 酒店类型
        /// </summary>
        public string HotelType { get; set; }
        /// <summary>
        /// 人均
        /// </summary>
        public string PersonAvg { get; set; }
        /// <summary>
        /// 其它门店列表数量
        /// </summary>
        public int OtherUnitCount { get; set; }

        /// <summary>
        /// 是否提供预约服务
        /// </summary>
        public int? IsApp { get; set; }
        #endregion
    }
}
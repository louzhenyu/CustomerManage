using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response
{
    public class HomePageConfigRD : IAPIResponseData
    {
        public AdAreaInfo[] AdAreaList { get; set; }
        public ItemEventAreaInfo[] ItemEventAreaList { get; set; }
        public CategoryGroupInfo[] CategoryGroupList { get; set; }//首页分类分组信息(分组8以外的)
        public CategoryGroupInfo CategoryEntrance { get; set; } //C8区分类分组信息(新增)
    }

    public class AdAreaInfo
    {
        /// <summary>
        /// 图片链接地址 【必须】
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 对象标识		【必须】
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 对象类型 1=活动 2=资讯 3=商品 4=门店。。。【必须】
        /// </summary>
        public int? ObjectTypeID { get; set; }
        /// <summary>
        /// 排序 【必须】
        /// </summary>
        public int? DisplayIndex { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

    }

    public class ItemEventAreaInfo
    {
        /// <summary>
        /// 商品标识		【必须】
        /// </summary>
        public string ItemID { get; set; }
        /// <summary>
        /// 商品名称		【必须】
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 原图片链接地址	【必须】
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 商品原价		【必须】
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品零售价（抢购价）【必须】
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 商品折扣		【必须】
        /// </summary>
        public decimal DiscountRate { get; set; }
        /// <summary>
        /// 排序			【必须】
        /// </summary>
        public int? DisplayIndex { get; set; }
        /// <summary>
        /// 还有多少时间截止（16天5小时34分）
        /// </summary>
        public string DeadlineTime { get; set; }
        /// <summary>
        /// 还有多少秒
        /// </summary>
        public long? DeadlineSecond { get; set; }
        /// <summary>
        /// 上架时间(格式：yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string AddedTime { get; set; }
        /// <summary>
        /// 抢购开始时间(格式：yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 抢购结束时间(格式：yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 商品类别 1=团购 2=抢购,3=热销		【必须】
        /// </summary>
        public string TypeID { get; set; }

    }

    public class CategoryAreaInfo
    {
        /// <summary>
        /// 对象标识		【必须】
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 原图片链接地址	【必须】
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 排序		【必须】
        /// </summary>
        public int? DisplayIndex { get; set; }
        /// <summary>
        /// 类型 1＝分类，2＝商品
        /// </summary>
        public int? TypeID { get; set; }
        /// <summary>
        /// 群组ID
        /// </summary>
        public int? GroupID { get; set; }
    }

    public class CategoryGroupInfo
    {
        public int? ModelTypeId { get; set; }
        public string ModelTypeName { get; set; }
        public int? GroupID { get; set; }
        public CategoryAreaInfo[] CategoryAreaList { get; set; }
    }
}

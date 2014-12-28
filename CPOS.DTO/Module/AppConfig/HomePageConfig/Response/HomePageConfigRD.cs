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
        public CategoryGroupInfo[] CategoryGroupList { get; set; }//首页分类分组信息(分组8以外的)
        public CategoryGroupInfo CategoryEntrance { get; set; } //C8区分类分组信息(新增)
        public CategoryGroupInfo navList { get; set; } //导航区域，c区模块4(新增)
        public EventListEntity ItemEventAreaList { get; set; }
        public EventListEntity secondKill { get; set; }
        public MHSearchAreaEntity search { get; set; } //搜索框
        public string sortActionJson { get; set; } //整体排序字段
    }
    public class MHSearchAreaEntity
    {
        public Guid? MHSearchAreaID { get; set; }

        public string imageUrl { get; set; }        //图片链接
        public string url { get; set; }
        public string styleType { get; set; }
        public string show { get; set; }
        public string titleName { get; set; }
        public string titleStyle { get; set; }




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
        public string areaFlag { get; set; }

    }
    public class EventListEntity
    {
        public string areaFlag { get; set; }  //区域标识，eventList,secondKill
        public string shopType { get; set; }  //用与存放秒杀区的整体类型数据，便于前端获取。
        public IList<ItemEventAreaInfo> arrayList { get; set; }   //活动集合
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
        public string navName { get; set; }        //导航里各个小图片下面的文字
    }

    public class CategoryGroupInfo
    {
        public int? ModelTypeId { get; set; }
        public string ModelTypeName { get; set; }
        public int? GroupID { get; set; }
        public CategoryAreaInfo[] CategoryAreaList { get; set; }
        public String styleType { get; set; }

        public String titleName { get; set; }

        public String titleStyle { get; set; }
    }
}

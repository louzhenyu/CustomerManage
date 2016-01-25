using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;

namespace JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response
{
    public class HomePageConfigRD : IAPIResponseData
    {
        public AdAreaInfo[] adAreaList { get; set; }       
        //public CategoryGroupInfo[] CategoryGroupList { get; set; }//首页分类分组信息(分组8以外的)
        public CategoryGroupInfo categoryEntrance { get; set; } //分类组合
        public List<ProductListInfo> productList { get; set; } //商品列表
        public List<CategoryGroupInfo> originalityList { get; set; } //创意组合
        public CategoryGroupInfo navList { get; set; } //导航区域，c区模块4(新增)
        public IList<EventListEntity> eventList { get; set; }
        public IList<EventListEntity> secondKill { get; set; }//秒杀
        public IList<EventListEntity> groupBuy { get; set; }   //团购
        public IList<EventListEntity> hotBuy { get; set; }   //热销
        public MHSearchAreaEntity search { get; set; } //搜索框
        public followInfo follow { get; set; }//立即关注
        public string sortActionJson { get; set; } //整体排序字段
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
    }
    public class MaterialTextIdInfo
    {
        public string TextId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
        public string OriginalUrl { get; set; }
        public string Author { get; set; }
    }
        public class followInfo
        {
            public string HomeId { get; set; }

            public string FollowId { get; set; }
            public string Title { get; set; }
            /// <summary>
            /// 类型 3 链接 35图文信息
            /// </summary>
            public int? TypeId { get; set; }
            /// <summary>
            /// 图文信息Id 关联 WMaterialText
            /// </summary>
            public string TextId { get; set; }
            /// <summary>
            /// 图文信息Title 关联 WMaterialText
            /// </summary>
            public string TextTitle { get; set; }
            /// <summary>
            /// 自定义链接
            /// </summary>
            public string Url { get; set; }
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
        public string EventName { get; set; }
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
        ///// <summary>
        ///// 还有多少时间截止（16天5小时34分）
        ///// </summary>
        //public string DeadlineTime { get; set; }
        /// <summary>
        /// 还有多少秒
        /// </summary>
        public int? RemainingSec { get; set; }
        /// <summary>
        /// 是否开始 0:未开始 1:已开始 2:结束
        /// </summary>
        public int IsStart { get; set; } 
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
        public int TypeID { get; set; }
        public int ShowStyle { get; set; }             //显示方式

        public string areaFlag { get; set; }

     

    }
    public class EventListEntity
    {
        //public int? GroupId { get; set; }
        public string areaFlag { get; set; }  //区域标识，eventList,secondKill
        public int shopType { get; set; }  //用与存放秒杀区的整体类型数据，便于前端获取。
        public int showStyle { get; set; }  //展示的样式
        public int? displayIndex { get; set; }

        public IList<ItemEventAreaInfo> arrayList { get; set; }   //活动集合
    }
    public class CategoryAreaInfo
    {
        /// <summary>
        /// 对象标识		【必须】
        /// </summary>
        public string ObjectID { get; set; }

        public string ObjectName { get; set; }
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
        public string url { get; set; }        //导航里各个小图片下面的文字
    }

    public class CategoryGroupInfo
    {
        
        public int? modelTypeId { get; set; }
        public string modelTypeName { get; set; }
        public int? groupID { get; set; }
        public IList<CategoryAreaInfo> CategoryAreaList { get; set; }
        public String styleType { get; set; }

        public String titleName { get; set; }

        public String titleStyle { get; set; }


        public int showCount { get; set; }
        public int showName { get; set; }
        public int showPrice { get; set; }
        public int showSalesPrice { get; set; }
        public int showSalesQty { get; set; }
        public int showDiscount { get; set; }
        public int displayIndex { get; set; }
    }
    public class ProductListInfo
    {

        public int? modelTypeId { get; set; }
        public string modelTypeName { get; set; }
        public int? groupID { get; set; }
        public IList<ProductInfo> CategoryAreaList { get; set; }
        public String styleType { get; set; }

        public String titleName { get; set; }

        public String titleStyle { get; set; }


        public int showCount { get; set; }
        public int showName { get; set; }
        public int showPrice { get; set; }
        public int showSalesPrice { get; set; }
        public int showSalesQty { get; set; }
        public int showDiscount { get; set; }
        public int displayIndex { get; set; }
    }
    public class ProductInfo
    {
        /// <summary>
        /// 商品标识		【必须】
        /// </summary>
        public string ItemID { get; set; }
        /// <summary>
        /// 商品名称		【必须】
        /// </summary>
        public string ItemName { get; set; }
        private string imageurl;
        /// <summary>
        /// 原图片链接地址	【必须】
        /// </summary>
        public string ImageUrl
        {
            get { return imageurl; }  //请求图片缩略图 
            set { imageurl = ImagePathUtil.GetImagePathStr(value, "240"); }
        }
        private string imageurl2;
        public string imageUrl2
        {
            get { return imageurl2; }  //请求图片缩略图 
            set { imageurl2 = ImagePathUtil.GetImagePathStr(ImageUrl.Replace("_240", ""), "480"); }
        }
        private string imageurl3;
        public string imageUrl3
        {
            get { return imageurl3; }  //请求图片缩略图 
            set { imageurl3 = ImagePathUtil.GetImagePathStr(ImageUrl.Replace("_240", ""), "640"); }
        }
        /// <summary>
        /// 商品原价		【必须】
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品零售价（抢购价）【必须】
        /// </summary>
        public decimal SalesPrice { get; set; }
        public string SalesCount { get; set; }
        /// <summary>
        /// 商品折扣		【必须】
        /// </summary>
        public string DiscountRate { get; set; }

    }
}

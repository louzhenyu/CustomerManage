using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetPanicbuyingKJItemDetailRD : IAPIResponseData
    {
        public string ItemId { get; set; }

        public string EventSKUMappingId { get; set; }

        public string HeadImageUrl { get; set; }//会员头像

        public decimal BargainedRate { get; set; } //已砍价格比例

        public string ItemName { get; set; }

        public int SinglePurchaseQty { get; set; }

        public decimal MinPrice { get; set; } //最小原价

        public decimal MinBasePrice { get; set; } //最小底价

        public decimal BargainedPrice { get; set; } //已砍金额(帮砍操作时[type=2]必传)

        public int PromotePersonCount { get; set; } //累计参与人数

        public int CurrentQty { get; set; } //Sku累计的剩余库存

        public int SoldQty { get; set; } //Sku累计的已售数量(+保留数量)

        public List<ImageInfo> ImageList { get; set; } //图片信息

        public string EventEndTime { get; set; } //活动结束时间

        public string KJEndTime { get; set; } //砍价结束时间

        public long Seconds { get; set; } //剩余可砍时间,到秒

        public string DeliveryDesc { get; set; } //运费描述

        public List<SkuProp1> Prop1List { get; set; }

        public string PropName1 { get; set; }

        public string PropName2 { get; set; }

        public List<ItemSkuInfo> SkuInfoList { get; set; }

        public string ItemIntroduce { get; set; }

        public string WebLogo { get; set; }

        public string CustomerShortName { get; set; }

        public string QRCodeURL { get; set; }

        public int status { get; set; } //1-抢购商品详情，2-帮砍页面 没砍状态，3-帮砍页面 已砍状态

        public int isPromoted { get; set; } //0-没发起 1-发起

        public int isEventEnd { get; set; } //0活动结束 1-活动没结束
    }

    public class SkuProp1
    {
        public string Prop1DetailId { get; set; }

        public string Prop1DetailName { get; set; }

        public string skuId { get; set; }

        public int Stock { get; set; }

    }

    public class ItemSkuInfo
    {
        public string skuId { get; set; }

        public string price { get; set; }

        public string BasePrice { get; set; }

        public string SalesCount { get; set; } //销售数量

        public string skuProp1 { get; set; } //属性1名称

        public string skuProp2 { get; set; } //属性2名称

        public string Stock { get; set; } //库存
    }
    public class ImageInfo
    {
        public string imageId { get; set; }     //图片标识
        /// <summary>
        /// 图片链接地址 update by Henry 2014-12-8
        /// </summary>
        private string imageUrl;
        public string imageURL
        {
            get { return ImagePathUtil.GetImagePathStr(this.imageUrl, "640"); }  //请求图片缩略图 
            set { this.imageUrl = value; }
        }
    }

}

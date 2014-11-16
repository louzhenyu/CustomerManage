using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response
{
    public class SetEventItemRD : IAPIResponseData
    {
        //public SetEventItemInfo SetEventItemInfo { get; set; }
        public ItemEventAreaInfo[] ItemEventAreaInfo{get;set;}
    }

    //public class SetEventItemInfo
    //{
    //    /// <summary>
    //    /// 广告ID，如果为空则新增
    //    /// </summary>
    //    public Guid? AdAreaId { get; set; }

    //    /// <summary>
    //    /// 主表标识
    //    /// </summary>
    //    public Guid? HomeID { get; set; }
         
    //    /// <summary>
    //    /// 图片链接URL
    //    /// </summary>
    //    public string ImageUrl { get; set; }

    //    /// <summary>
    //    /// 对象标识
    //    /// </summary>
    //    public string ObjectID { get; set; }
    //    /// <summary>
    //    /// 对象类型：1＝活动，2＝资讯，3＝商品，4＝门店
    //    /// </summary>
    //    public int? ObjectTypeID { get; set; }
    //    /// <summary>
    //    /// 排序
    //    /// </summary>
    //    public int? DisplayIndex { get; set; }
    //    /// <summary>
    //    /// 链接
    //    /// </summary>
    //    public string Url { get; set; }   
    //}

 
}

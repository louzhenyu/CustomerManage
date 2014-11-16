using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.HomePage.Advertisement.Response
{
    public class GetAdvertisementListRD : IAPIResponseData
    {
        /// <summary>
        /// 广告列表
        /// </summary>
        public IList<AdvertisementEntity> AdvertisementList { get; set; }
    }

    public class AdvertisementEntity
    {
        /// <summary>
        /// 广告图片链接
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 广告内容
        /// </summary>
        public string Content { get; set; }
    }

}

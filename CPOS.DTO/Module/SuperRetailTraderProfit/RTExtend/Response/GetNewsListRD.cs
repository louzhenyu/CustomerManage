using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response
{
    public class GetNewsListRD : IAPIResponseData
    {
        //public GetNewsListRD(List<WMaterialTextEntity> dbList)
        //{
        //    if (dbList.Count() == 0)
        //        return;
        //    List = dbList.Select(x =>
        //        new NewsInfo()
        //        {
        //            TextId = x.TextId,
        //            Title = x.Title,
        //            CoverImageUrl = x.CoverImageUrl,
        //            Text = x.Text != null && x.Text.Length > 50 ? x.Text.Substring(0, 50) : x.Text
        //        }).ToList();
        //}
        public List<NewsInfo> List { get; set; }
    }
    public class NewsInfo
    {
        public string TextId { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response
{
    public class GetMaterialTextListRD : IAPIResponseData
    {
        public int TotalPages { get; set; }
        public MaterialTextListInfo[] MaterialTextList { get; set; }
    }

    public class MaterialTextListInfo
    {
        public string TestId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string Text { get; set; }
        public int DisplayIndex { get; set; }
        public string ApplicationId { get; set; }
        public string TypeId { get; set; }
        public string PageId { get; set; }
        public string ModuleName { get; set; }
        public string PageParamJson { get; set; }
        public string UnionTypeId { get; set; }

        public string Abstract { get; set; }

    }

}

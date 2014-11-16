using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response
{
    public class GetMaterialTextDetailRD : IAPIResponseData
    {
        public MaterialTextTitleInfo MaterialTextTitleList { get; set; }
    }

    public class MaterialTextTitleInfo
    {
        public string TextId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CoverImageUrl { get; set; }
        public string Author { get; set; }
    }
}

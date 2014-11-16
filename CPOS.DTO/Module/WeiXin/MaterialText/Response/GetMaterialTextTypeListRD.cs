using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response
{
    public class GetMaterialTextTypeListRD : IAPIResponseData
    {
        public MaterialTextTypeInfo[] MaterialTextTypeList { get; set; }

    }

    public class MaterialTextTypeInfo
    {
        public string TypeId { get; set; }
        public string TypeName { get; set; }
    }
}

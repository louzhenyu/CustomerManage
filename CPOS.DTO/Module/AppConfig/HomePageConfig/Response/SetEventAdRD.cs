using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.AppConfig.HomePageConfig.Response
{
    public class SetEventAdRD : IAPIResponseData
    {
        public AdAreaInfo[] AdAreaInfo { get; set; }
    }
}

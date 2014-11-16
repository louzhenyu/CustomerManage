using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.WX;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.Menu.Response
{
    public class PublishMenuRD : IAPIResponseData
    {
         public ResultEntity resultEntity { get; set; }
    }
}

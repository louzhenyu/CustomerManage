using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SapMessageApi.Request;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Response
{
    public class GetMsgObjRD : IAPIResponseData
    {
        public Omsg Omsg { get; set; }
        public Msg1 Msg1 { get; set; }
    }
}

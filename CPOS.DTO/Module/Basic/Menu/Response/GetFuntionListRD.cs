using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Menu.Response
{
    public class GetFuntionListRD : IAPIResponseData
    {
        public List<FunctionInfo> FunctionList { get; set; }
    }
    public class FunctionInfo
    {

        /// <summary>
        /// 功能编码
        /// </summary>
        public string FunctionCode { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
    }
}

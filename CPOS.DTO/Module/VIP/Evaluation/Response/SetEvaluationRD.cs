using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Evaluation.Response
{
    /// <summary>
    /// 增加评论请求
    /// </summary>
    public class SetEvaluationRD : IAPIResponseData
    {
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
    }

 
}

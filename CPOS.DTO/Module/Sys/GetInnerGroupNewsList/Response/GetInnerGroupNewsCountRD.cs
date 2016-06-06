using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Response
{
    public class GetInnerGroupNewsCountRD : IAPIResponseData
    {
        /// <summary>
        /// 站内未读消息个数
        /// </summary>
        public int InnerGroupNewsCount { get; set; }
        /// <summary>
        /// 集客工具消息未读个数
        /// </summary>
        public int SetoffToolsCount { get; set; }
    }
}

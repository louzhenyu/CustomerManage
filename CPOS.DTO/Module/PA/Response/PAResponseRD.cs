using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.PA.Response
{
    public class PAResponseRD : IAPIResponseData
    {

        public string ret { get; set; }
        public string msg { get; set; }
        /// <summary>
        /// 请求的标识
        /// </summary>
        public string requestId { get; set; }
        public PAResponseDetailRD data { get; set; }
        
    }

    public class PAResponseDetailRD
    {
        /// <summary>
        /// 接口返回消息
        /// </summary>
        public string MSG { get; set; }
        /// <summary>
        /// 接口返回的数据
        /// </summary>
        public object DATA { get; set; }
        /// <summary>
        /// 00表示成功    其他失败
        /// </summary>
        public string CODE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.PA
{
    public class PALogEntity
    {
        public string _id { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string ReqPara { get; set; }
        /// <summary>
        /// 响应参数
        /// </summary>
        public string ResPara { get; set; }
        public DateTime RecTime { get; set; }
    }
}

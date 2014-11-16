using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    public class GetResponseParams<T>
    {
        /// <summary>
        /// 返回结果集
        /// </summary>
        public T Params { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string Description { get; set; }

        public string Flag { get;set;}

    }
}

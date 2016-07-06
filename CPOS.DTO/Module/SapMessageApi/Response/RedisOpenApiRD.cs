using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SapMessageApi.Response
{
    public class RedisOpenApiRD<T>
    {

        /// <summary>
        /// 响应编码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 响应结果
        /// </summary>
        public T Result { get; set; }
    }

    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 数据未找到
        /// </summary>
        DataNotFound,

        /// <summary>
        /// 失败
        /// </summary>
        Fail
    }
}

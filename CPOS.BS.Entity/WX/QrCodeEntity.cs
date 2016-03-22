using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.WX
{
    /// <summary>
    /// 生成带参数的二维码
    /// </summary>
    public class QrCodeEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 二维码的有效时间，以秒为单位。最大不超过1800。
        /// </summary>
        public string expire_seconds { get; set; }
    }

    public class ShorturlEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        ///短链接。
        /// </summary>
        public string short_url { get; set; }

    }
}

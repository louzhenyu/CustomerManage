using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 门店信息 案例 {\"type\":\"3\",\"title\":\"按钮1\",\"value\":\"test1\"}
    /// </summary>
    public class PopInfo
    {
        /// <summary>
        /// Type=1为文本、Type=2为图片、Type=3为按钮,type=4 为iframe
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// Type=1为字段名、Type=2为没用、Type=3为按钮显示名,type=4 为没用
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Type=1为字段值、Type=2为图片地址、Type=3为按钮回调方法（参数为当前点属性）,type=4 为iframe地址
        /// </summary>
        public string value { get; set; }
    }
}

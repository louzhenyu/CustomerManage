using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Pos
{
    /// <summary>
    /// 终端类型
    /// </summary>
    public class PosTypeInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName
        { get; set; }
    }
}

using JIT.Utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 会员查询时对应的标签条件
    /// </summary>
    public class VipSearchTag : BaseEntity
    {
        /// <summary>
        /// 左边括号,值为空或为 '（'
        /// </summary>
        public string LeftBracket { get; set; }
        /// <summary>
        /// 等于号/不等号
        /// </summary>
        public string EqualFlag { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        public string TagId { get; set; }
        /// <summary>
        /// 右边括号,值为空或为 ')'
        /// </summary>
        public string RightBracket { get; set; }
        /// <summary>
        /// 并且/或者  符号
        /// </summary>
        public string AndOrStr { get; set; }
    }
}

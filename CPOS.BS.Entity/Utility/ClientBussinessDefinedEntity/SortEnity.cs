using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    public class SortEnity : BaseEntity
    {
        /// <summary>
        /// 排序类型
        /// </summary>
        public int? SortType { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; }
        /// <summary>
        /// 字段所属控件
        /// </summary>
        public int? ControlType { get; set; }

    }
}

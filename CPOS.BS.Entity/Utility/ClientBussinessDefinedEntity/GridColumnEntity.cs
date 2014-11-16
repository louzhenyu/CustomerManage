using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;


namespace JIT.CPOS.BS.Entity
{
    public class GridColumnEntity : BaseEntity
    {
        /// <summary>
        /// 所对应的功能模块的名称
        /// </summary>
        public int? MoudelID { get; set; }
        /// <summary>
        /// Grid显示值
        /// </summary>
        public string ColumnText { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int? ColumnWdith { get; set; }
        /// <summary>
        /// 表格列控件类型
        /// </summary>
        public int? ColumnControlType { get; set; }
        /// <summary>
        /// 对应的Store的值
        /// </summary>
        public string DataIndex { get; set; }
        /// <summary>
        /// 关联值
        /// </summary>
        public string CorrelationValue { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsMustDo { get; set; }
    }
}

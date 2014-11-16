using JIT.Utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 查询条件对应的列和其值，可以作为查询各个实体的参数
    /// </summary>
    public class SearchColumn : BaseEntity
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列值1 对于string类型的列 只取 列值1
        /// </summary>
        public string ColumnValue1 { get; set; }
        /// <summary>
        /// 列值2 对于需要一个范围值的列例如日期类型的列，取列值1 和 列值2 
        /// 其中列值1 为起始值，列值2 为结束值
        /// </summary>
        public string ColumnValue2 { get; set; }
        /// <summary>
        /// 指定列对应的是何种控件
        /// </summary>
        public int ControlType { get; set; }
    }
}

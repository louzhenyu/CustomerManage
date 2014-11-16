using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;
namespace JIT.CPOS.BS.Entity
{
    public class GridColumnModelEntity : BaseEntity
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public int? DataType { get; set; }
        /// <summary>
        /// 对应的数据字段名称
        /// </summary>
        public string DataIndex { get; set; }
       

    }
}

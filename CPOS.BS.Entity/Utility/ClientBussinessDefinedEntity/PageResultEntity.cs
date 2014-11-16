using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    public class PageResultEntity : BaseEntity
    {
        /// <summary>
        /// 表格数据
        /// </summary>
        public DataTable GridData { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int? RowsCount { get; set; }
        /// <summary>
        /// 分页数
        /// </summary>
        public int? PageCount { get; set; }
    }
}

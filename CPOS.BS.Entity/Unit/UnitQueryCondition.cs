using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Unit
{
    /// <summary>
    /// 单位查询条件
    /// </summary>
    public class UnitQueryCondition
    {
        public UnitQueryCondition()
        {
            this.SuperUnitIDs = new List<String>();
        }

        /// <summary>
        /// 上级单位列表
        /// </summary>
        public IList<string> SuperUnitIDs
        { get; set; }
    }
}

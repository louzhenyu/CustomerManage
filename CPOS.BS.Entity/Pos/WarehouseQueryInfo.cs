using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Pos
{
    public class WarehouseQueryInfo : WarehouseInfo, Exchange.IObjectQuery
    {
        #region IObjectQuery Members

        public int RecordCount
        {
            get;
            set;
        }

        #endregion
    }
}

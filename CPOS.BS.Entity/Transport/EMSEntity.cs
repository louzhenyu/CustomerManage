using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Transport
{
    public class EMSEntity
    {
        public List<EMSTrace> traces { get; set; }
    }

    public class EMSTrace
    {
        public string acceptTime { get; set; }
        public string acceptAddress { get; set; }
        public string remark { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Transport
{
    [XmlRoot("responses")]
    public class YunDaRDEntity
    {
        public YunDaRPEntityItem response { get; set; }
    }

    public class YunDaRPEntityItem
    {
        public string mailno { get; set; }
        public string result { get; set; }
        public string remark { get; set; }
        public string status { get; set; }
        public string weight { get; set; }
        [XmlArrayItem("trace")]
        public List<YunDaRPEntityItemTraces> traces { get; set; }
    }

    public class YunDaRPEntityItemTraces
    {
        public string time { get; set; }
        public string station { get; set; }
        public string status { get; set; }
        public string remark { get; set; }
    }
}

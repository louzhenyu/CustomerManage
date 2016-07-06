using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.SAP
{
    [XmlRoot("AdmInfo")]
    public class AdmInfoEntity
    {
        public string Object { get; set; }
        public int Version { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.Interface
{
    public class NewVersionEntity
    {
        [DataMember(Name = "isNewVersionAvailable")]
        public string isNewVersionAvailable { get; set; }

        [DataMember(Name = "message")]
        public string message { get; set; }

        [DataMember(Name = "canSkip")]
        public string canSkip { get; set; }

        [DataMember(Name = "updateUrl")]
        public string updateUrl { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }
    }
}

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
    [DataContract(Namespace = "www.jitmarketing.cn", Name = "data")]
    public class ReqContentEntity
    {
        [DataMember(Name = "common")]
        public CommonEntity common { get; set; }
    }

    [DataContract(Namespace = "www.jitmarketing.cn", Name = "common")]
    public class CommonEntity
    {
        [DataMember(Name = "locale")]
        public string locale { get; set; }

        [DataMember(Name = "userId")]
        public string userId { get; set; }

        [DataMember(Name = "openId")]
        public string openId { get; set; }

        [DataMember(Name = "customerId")]
        public string customerId { get; set; }

    }
}

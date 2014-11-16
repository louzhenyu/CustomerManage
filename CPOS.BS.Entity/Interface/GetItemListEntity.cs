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
    public class GetItemListEntity : ReqContentEntity
    {
        [DataMember(Name = "special")]
        public GetItemListSpecialEntity special { get; set; }
    }
    [DataContract(Namespace = "www.jitmarketing.cn", Name = "special")]
    public class GetItemListSpecialEntity
    {
        [DataMember(Name = "itemTypeId")]
        public string itemTypeId { get; set; }

        [DataMember(Name = "page")]
        public string page { get; set; }

        [DataMember(Name = "pageSize")]
        public string pageSize { get; set; }
    }
}

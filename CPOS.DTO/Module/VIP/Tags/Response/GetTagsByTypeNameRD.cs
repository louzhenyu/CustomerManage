using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Tags.Response
{
    public class GetTagsByTypeNameRD : IAPIResponseData
    {
        public List<TagsInfo> TagsList { get; set; }
    }
    public class TagsInfo
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public string TagsID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagsName { get; set; }
    }
}

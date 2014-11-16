using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;



namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Response
{
    public class GetVocationVersionMappingListRD : IAPIResponseData
    {
        public VocationVersionInfo[] VocationVersionList { get; set; }

        public int TotalPageCount { get; set; }
    }
    public class VocationVersionInfo
    {
        /// <summary>
        /// 行业版本映射ID
        /// </summary>
        public string VocaVerMappingID { get; set; }

        /// <summary>
        /// 行业描述
        /// </summary>
        public string VocationDesc { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string VersionDesc { get; set; }

    }
}

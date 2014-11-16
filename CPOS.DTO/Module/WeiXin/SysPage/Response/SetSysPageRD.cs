using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Response
{
    public class SetSysPageRD:IAPIResponseData
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid? PageId { get; set; }
    }
}

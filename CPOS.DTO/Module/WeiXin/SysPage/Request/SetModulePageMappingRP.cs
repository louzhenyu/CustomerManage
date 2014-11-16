using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.WeiXin.SysPage.Request
{
    public class SetModulePageMappingRP : IAPIRequestParameter
    {
        //页面ID
        public string PageId { get; set; }

        /// <summary>
        /// 行业版本映射关系ID
        /// </summary>
        public string[] VocaVerMappingID { get; set; }

        const int NO_EXISTS_PAGEID = 301;
        const int NO_EXISTS_VOCAVERMAPPINGID = 302;
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.PageId))
            {
                throw new APIException("PageId不能为空！") { ErrorCode = NO_EXISTS_PAGEID };
            }
            if (this.VocaVerMappingID.Length<1)
            {
                throw new APIException("VocaVerMappingID不能为空！") { ErrorCode = NO_EXISTS_VOCAVERMAPPINGID };
            }
        }
    }
}

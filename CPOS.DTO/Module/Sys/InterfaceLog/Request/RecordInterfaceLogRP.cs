using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Sys.InterfaceLog.Request
{
    public class RecordInterfaceLogRP : IAPIRequestParameter
    {
        #region 属性
        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// 执行的操作 Access  表示访问 Relay  表示转发
        /// </summary>
        public string Action { get; set; }
        #endregion

        const int ERROR_NO_PAGENAME = 301;
        const int ERROR_NO_ACTION = 302;
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.PageName))
            {
                throw new APIException("未传参数PageName") { ErrorCode = ERROR_NO_PAGENAME };
            }
            if (string.IsNullOrWhiteSpace(this.Action))
            {
                throw new APIException("未传参数Action") { ErrorCode = ERROR_NO_ACTION };
            }
        }
    }
}

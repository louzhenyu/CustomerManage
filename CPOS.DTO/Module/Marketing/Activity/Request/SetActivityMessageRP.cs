using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Response;

namespace JIT.CPOS.DTO.Module.Marketing.Activity.Request
{
    public class SetActivityMessageRP : IAPIRequestParameter
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string ActivityID { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 消息集合
        /// </summary>
        public List<ActivityMessageInfo> ActivityMessageList { get; set; }

        public void Validate() { }
    }
}

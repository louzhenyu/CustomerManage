using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.ServicesLog.Request
{
    public class AddVipFeedbackInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 图片地址数组
        /// </summary>
        public string[] ImageUrlArray { get; set; }


        public void Validate()
        {

        }
    }
}

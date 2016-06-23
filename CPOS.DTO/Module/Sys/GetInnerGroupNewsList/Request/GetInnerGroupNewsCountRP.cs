using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Sys.GetInnerGroupNewsList.Request
{
    public class GetInnerGroupNewsCountRP : IAPIRequestParameter
    {
        /// <summary>
        /// 平台编号 （1=微信用户2=APP员工）
        /// </summary>
        public int NoticePlatformTypeId { get; set; }
        /// <summary>
        /// 1=会员集客 2=员工集客 3=超级分销商
        /// </summary>
        public int SetoffType { get; set; }
        public void Validate()
        {
        }
    }
}

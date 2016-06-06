using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.Event.Bargain.Response
{
    public class GetEventItemStatusRD : IAPIResponseData
    {
        public int status { get; set; }// 0 -正常 1-用户删除了这个活动 2-活动时间结束 3-砍价活动结束 4-用户已购买，不能继续砍价
    }
}

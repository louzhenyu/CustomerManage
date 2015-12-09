using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Event.Lottery.Request
{
    public class LotteryRP : IAPIRequestParameter
    {
        public string Type { get; set; }//REG,FOLLOW,SHARE,SIGNIN
        public string EventId { get; set; }
        public string ShareUserId { get; set; }//分享者
        public void Validate()
        {
        }
    }
}

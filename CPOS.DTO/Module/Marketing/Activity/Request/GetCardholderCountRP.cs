using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Request
{
    public class GetCardholderCountRP : IAPIRequestParameter
    {
        /// <summary>
        /// 会员卡类型ID
        /// </summary>
        public List<string> VipCardTypeIDList { get; set; }
        /// <summary>
        /// 会员卡类型
        /// </summary>
        public int ActivityType { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int IsLongTime { get; set; }
        public void Validate() { }
    }
}

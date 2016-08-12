using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    public class GetSysVipCardTypeListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 卡类别{消费卡=2|会员卡=0|储值卡=1}
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 是否在线销售
        /// </summary>
        public int IsOnLineSale { get; set; }
        public void Validate()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Market.MarketSignIn.Requset
{
    public class SetMarketSignInRP : IAPIRequestParameter
    {
        /// <summary>
        /// 签到活动标志
        /// </summary>
        public string MarketEventID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        public void Validate()
        {
        }
    }
}

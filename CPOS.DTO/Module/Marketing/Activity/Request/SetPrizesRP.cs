using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Marketing.Activity.Request
{
    public class SetPrizesRP : IAPIRequestParameter
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public string ActivityID { get; set; }
        /// <summary>
        /// 奖品ID
        /// </summary>
        public string PrizesID { get; set; }
        /// <summary>
        /// 奖品类型
        /// </summary>
        public int PrizesType { get; set; }
        /// <summary>
        /// 奖品明细集合
        /// </summary>
        public List<PrizesDetailInfo> PrizesDetailList { get; set; }

        public void Validate() { }
    }
}

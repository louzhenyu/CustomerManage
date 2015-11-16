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
        /// 操作类型
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 赠值限额
        /// </summary>
        public decimal? PointsMultiple { get; set; }
        /// <summary>
        /// 奖品集合
        /// </summary>
        public List<PrizesInfo> PrizesInfoList { get; set; }

        public void Validate() { }
    }
}

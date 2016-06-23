using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request
{
    public class BeSuperRetailTraderRP : IAPIRequestParameter
    {        
        /// <summary>
        /// 成为分销商来源类型 User=员工   Vip=会员
        /// </summary>
        public string BeRYType { get; set; }
        /// <summary>
        /// 当前要成为分销商的会员ID或员工ID
        /// </summary>
        public string SuperRetailTraderFromId { get; set; }
        /// <summary>
        /// 上级超级分销商ID
        /// </summary>
        public string HigheSuperRetailTraderID { get; set; }
        public void Validate(){}
    }
}

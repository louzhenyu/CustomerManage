using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response
{
    public class BeSuperRetailTraderRD : IAPIResponseData
    {

        /// <summary>
        /// 成为分销商是否成功  0=未成为分销商(失败) 1=成为分销商(成功) 2=之前已成为分销商
        /// </summary>
        public int IsSuperRetailTrader { get; set; }
        /// <summary>
        /// 超级分销账号
        /// </summary>
        public string SuperRetailTraderLogin { get; set; }
        /// <summary>
        /// 超级分销密码
        /// </summary>
        public string SuperRetailTraderPass { get; set; }
        /// <summary>
        /// 商户编号
        /// </summary>
        public string CustomerCode { get; set; }
    }



}

using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Response
{
    public class Get_R_Withdraw_HomeRD : IAPIResponseData
    {
        public List<WithdrawInfo> List { get; set; }
        public Get_R_Withdraw_HomeRD()
        {
            List = new List<WithdrawInfo>();
        }

        public bool isConfigRule { get; set; }
    }
    public class WithdrawInfo
    {

        public int Type { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public decimal? Count { get; set; }
        /// <summary>
        /// 会员数
        /// </summary>
        public decimal? Vip { get; set; }
        /// <summary>
        /// 员工数
        /// </summary>
        public decimal? User { get; set; }
        /// <summary>
        /// 超级分销商数
        /// </summary>
        public decimal? SRT { get; set; }
    }

}

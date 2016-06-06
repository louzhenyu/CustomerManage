using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class SetOffActionRP : IAPIRequestParameter
    {

        public List<SetOffActionInfo> SetOffActionList { get; set; }
        public void Validate()
        {

        }
    }

    public class SetOffActionInfo
    {
        /// <summary>
        /// 1 会员集客 2 员工集客
        /// </summary>
        public int SetoffType { get; set; }
        /// <summary>
        /// 1:现金 2：积分
        /// </summary>
        public int SetoffRegAwardType { get; set; }
        public decimal SetoffRegPrize { get; set; }
        public decimal SetoffOrderPer { get; set; }
        /// <summary>
        /// 空或者0表示单单有效，>0表示具体限制次数
        /// </summary>
        public int SetoffOrderTimers { get; set; }
        /// <summary>
        /// 10:启用，90：失效
        /// </summary>
        public int IsEnabled { get; set; }

        public List<SetoffPosterInfo> SetoffPosterList { get; set; }

        public List<SetoffToolsInfo> SetoffTools { get; set; }


    }

    public class SetoffToolsInfo
    {
        /// <summary>
        /// CTW：创意仓库 Coupon：优惠券 SetoffPoster：集客报
        /// </summary>
        public string ToolType { get; set; }
        public string ObjectId { get; set; }
        
    }

    public class SetoffPosterInfo{
        public string Name{get;set;}
        public string ImageUrl{get;set;}
    }
}

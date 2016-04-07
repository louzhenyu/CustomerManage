using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.Dealer.Response
{
    public class GetVipFansListRD : IAPIResponseData
    {
        public List<VipFansInfo> VipFansList { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; set; }
    }

    public class VipFansInfo
    {
        public string VipId { get; set; }

        public string HeadImgUrl { get; set; }

        public string VipName { get; set; }
        /// <summary>
        /// 当前等级会员卡单价
        /// </summary>
        public string CardAmount { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// 描述（Y：已成交，N：已关注未成交）
        /// </summary>
        public string CodeDes { get; set; }
    }
}

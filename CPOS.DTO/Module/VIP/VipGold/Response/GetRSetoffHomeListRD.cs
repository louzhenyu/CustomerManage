using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetRSetoffHomeListRD : IAPIResponseData
    {
        public List<result> result { get; set; }
        public GetRSetoffHomeListRD()
        {
            result = new List<result>();
        }
    }

    public class result
    {
        public int? OnlyFansCount { get; set; } //存粉丝数量
        public int? VipCount { get; set; } //会员数量
        public decimal? VipPer { get; set; } //百分比
        public string SetoffType { get; set; } //类型
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onlyfanscount">存粉丝数量</param>
        /// <param name="vipcount">会员数量</param>
        /// <param name="vipper">会员所所占比</param>
        /// <param name="setofftype">角色类型</param>
        public result(int? onlyfanscount, int? vipcount, decimal? vipper, string setofftype)
        {
            this.OnlyFansCount = onlyfanscount;
            this.VipCount = vipcount;
            this.VipPer = vipper;
            this.SetoffType = setofftype;
        }
    }
}

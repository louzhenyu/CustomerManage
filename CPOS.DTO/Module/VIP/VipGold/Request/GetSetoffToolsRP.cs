using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetSetoffToolsRP : IAPIRequestParameter
    {

        public string ApplicationType { get; set; } //应用类型 1=微信；2= APP会员服务；3=APP客服列表
        public string BeShareVipID { get; set; }//被推送或被分享会员ID
        public string  ToolsType { get; set; }//工具类型   集客工具类型(SetoffPoster=集客海报;CTW =活动;Coupon =优惠券;)
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        {

        } 
    }
}

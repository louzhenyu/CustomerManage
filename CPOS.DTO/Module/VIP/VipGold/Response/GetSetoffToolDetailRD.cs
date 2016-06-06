using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetSetoffToolDetailRD : IAPIResponseData
    {
        public string backImgUrl { get; set; }//海报背景图片
        public string ToolType { get; set; }//工具类型 Coupon=优惠券 SetoffPoster=集客报
        public string imageUrl { get; set; }//集客工具 券或详情二维码
        public string paraTemp { get; set; }//随机生成8位随机数
        public string Name { get; set; }//活动工具名称（优惠券或集客海报名称）
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ServiceLife { get; set; }
    }

}

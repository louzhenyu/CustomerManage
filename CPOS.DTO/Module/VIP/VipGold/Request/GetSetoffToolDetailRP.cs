using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetSetoffToolDetailRP : IAPIRequestParameter
    {
        /// <summary>
        /// 二维码类型
        /// </summary>
        public int VipDCode { get; set; } // 9=永久二维码 3=临时二维码
        public string PlatformType { get; set; }//应用类型 1=微信用户 2=APP员工   
        public string ShareVipId { get; set; }//此处ID 可能是会员ID，可能是员工ID
        public string ToolType { get; set; }//集客工具类型 推出QRCode类型(SetoffPosterQRCode=集客海报;CouponQrCode =优惠券;)
        public string ObjectId { get; set; }//优惠券对象ID，集客海报ID
        public string SetoffToolID { get; set; }//活动工具ID
        public void Validate()
        {
            const int ERROR_LACK_PlatformType = 311;//应用类型不能为空 1=微信用户 2=APP员工   
            const int ERROR_LACK_TypeCode = 312;//二维码类型不能为空   集客工具类型(SetoffPosterQRCode=集客海报;CouponQrCode =优惠券;)
            const int ERROR_LACK_VipDCode = 313; // 9=永久二维码 3=临时二维码不能为空
            const int ERROR_LACK_ObjectID = 314; //优惠券对象ID，集客海报ID不能为空       
            const int ERROR_LACK_ShareVipId = 315;//此处分享人ID 可能是会员ID，可能是员工ID 不能为空
            if (string.IsNullOrEmpty(VipDCode.ToString("")))
            {
                throw new APIException("请求参数中缺少VipDCode或值为空.") { ErrorCode = ERROR_LACK_VipDCode };
            }
            if (string.IsNullOrEmpty(PlatformType))
            {
                throw new APIException("请求参数中缺少PlatformType或值为空.") { ErrorCode = ERROR_LACK_PlatformType };
            }
            if (string.IsNullOrEmpty(ToolType))
            {
                throw new APIException("请求参数中缺少ToolType或值为空.") { ErrorCode = ERROR_LACK_TypeCode };
            }
            if (string.IsNullOrEmpty(ObjectId))
            {
                throw new APIException("请求参数中缺少ObjectId或值为空.") { ErrorCode = ERROR_LACK_ObjectID };
            }
        } 
    }
}

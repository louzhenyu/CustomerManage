using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Marketing.Coupon
{
    public class DelCouponTypeAH:BaseActionHandler<DelCouponRP,EmptyResponseData>
    {

        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<DelCouponRP> pRequest)
        {
            var rd = new EmptyResponseData();

            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponTypeBLL = new CouponTypeBLL(loggingSessionInfo);
            var couponTypeInfo = couponTypeBLL.GetByID(para.CouponTypeID);
            if (couponTypeInfo != null)
            {
                DateTime dtNow = DateTime.Now;
                if (couponTypeInfo.ServiceLife == 0)//不是领券后多少天失效的券
                {
                    if (couponTypeInfo.BeginTime > dtNow || couponTypeInfo.EndTime < dtNow)//不在有效期内的券，可删除
                        couponTypeBLL.Delete(couponTypeInfo);
                }
                
            }
            return rd;
        }
    }
}
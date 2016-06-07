using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// APP获取客户服务是否推送红点
    /// </summary>
    public class GetSetOffIsPushAH : BaseActionHandler<GetSetOffIsPushRP, GetSetOffIsPushRD>
    {
        protected override GetSetOffIsPushRD ProcessRequest(APIRequest<GetSetOffIsPushRP> pRequest)
        {
            GetSetOffIsPushRD setOffIsPushRD = new GetSetOffIsPushRD();
            SetoffToolsBLL setOffToolsBLL = new SetoffToolsBLL(CurrentUserInfo);//集客工具业务处理
            var setoffEventBLL = new SetoffEventBLL(CurrentUserInfo);//集客行动业务处理            
            var para = pRequest.Parameters;
            if (string.IsNullOrEmpty(para.ShareVipType))
            {
                para.ShareVipType = "2";
            }
            var setoffEventInfo = setoffEventBLL.QueryByEntity(new SetoffEventEntity() { Status = "10", SetoffType = Convert.ToInt32(para.ShareVipType != "3" ? "2" : para.ShareVipType), CustomerId = CurrentUserInfo.CurrentUser.customer_id }, null).FirstOrDefault();
            if (setoffEventInfo!=null)
            {
                setOffIsPushRD.CTW_EventIsPush = setOffToolsBLL.GetIsPushCount(para.ShareVipType, para.BeShareVipID, "CTW",setoffEventInfo.SetoffEventID.ToString());
                setOffIsPushRD.SetOffToolIsPush = setOffToolsBLL.GetIsPushCount(para.ShareVipType, para.BeShareVipID, "SetoffPoster", setoffEventInfo.SetoffEventID.ToString());
                setOffIsPushRD.CouponIsPush = setOffToolsBLL.GetIsPushCount(para.ShareVipType, para.BeShareVipID, "Coupon", setoffEventInfo.SetoffEventID.ToString());
            }
            else
            {
                setOffIsPushRD.CTW_EventIsPush = 0;
                setOffIsPushRD.SetOffToolIsPush = 0;
                setOffIsPushRD.CouponIsPush = 0;
            }
            
            return setOffIsPushRD;
        }
    }
}
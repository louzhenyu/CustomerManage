using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Dealer.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Dealer
{
    /// <summary>
    /// 设置会员成为经销商  (col48  是否经销人 1:是 0：否)
    /// </summary>
    public class SetVipDealerAH : BaseActionHandler<SetVipDealerRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetVipDealerRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var VipBLL = new VipBLL(CurrentUserInfo);

            bool Flag = VipBLL.IsSetVipDealer(pRequest.UserID);
            try
            {
                if (Flag)
                {

                    VipEntity Entity = VipBLL.GetByID(pRequest.UserID);
                    if (Entity != null)
                    {
                        Entity.Col48 = "1";
                        VipBLL.Update(Entity);
                    }
                    else
                    {
                        throw new APIException("会员不存在!") { ErrorCode = 103 };
                    }
                }
                else
                {
                    //var Result = VipBLL.GetSetVipDealerUpset();
                    throw new APIException("您当前会员卡等级不符合！") { ErrorCode = 105 };

                }
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }

            return rd;
        }
    }
}
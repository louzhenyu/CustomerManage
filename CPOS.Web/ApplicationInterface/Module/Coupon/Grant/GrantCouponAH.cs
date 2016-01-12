using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Request;
using JIT.CPOS.DTO.Module.Marketing.Coupon.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL.WX;
namespace JIT.CPOS.Web.ApplicationInterface.Module.Coupon.Grant
{
    public class GrantCouponAH : BaseActionHandler<GrantCouponRP, GrantCouponRD>
    {
        protected override GrantCouponRD ProcessRequest(DTO.Base.APIRequest<GrantCouponRP> pRequest)
        {
            var rd = new GrantCouponRD();//返回值 
            var param=pRequest.Parameters;
            VipCouponMappingBLL bllVipCoupon = new VipCouponMappingBLL(this.CurrentUserInfo);
            try
            {
                var customerBasicSettingBLL = new CustomerBasicSettingBLL(this.CurrentUserInfo);
                var ResultList = customerBasicSettingBLL.GetBusinessBasisConfigInfo(CurrentUserInfo.ClientID);
                DataRow dr = customerBasicSettingBLL.GetCustomerInfo(CurrentUserInfo.ClientID).Tables[0].Rows[0];
                rd.CustomerName = dr["customer_name"].ToString();//商户昵称
                rd.FollowUrl = ResultList.FirstOrDefault(m => m.SettingCode.Equals("GuideLinkUrl")) == null ? "" : ResultList.FirstOrDefault(m => m.SettingCode.Equals("GuideLinkUrl")).SettingValue;//引导链接
                rd.GuideQRCode = ResultList.FirstOrDefault(m => m.SettingCode.Equals("GuideQRCode")) == null ? "" : ResultList.FirstOrDefault(m => m.SettingCode.Equals("GuideQRCode")).SettingValue;//引导二维码


                if (bllVipCoupon.HadBeGranted(param.CouponId,param.Giver) == 0)
                {
                    rd.IsAccept = 1;
                    rd.IsSuccess = false;
                    rd.Message = "手慢一步,优惠券已被高人领走！";
                    return rd;
                }
                CouponBLL bll = new CouponBLL(this.CurrentUserInfo);
                DataSet ds = bll.GetCouponDetail(pRequest.Parameters.CouponId, "");
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Status"].ToString() == "0")
                {

                    if (bllVipCoupon.GrantCoupon(param.Giver, pRequest.UserID, param.CouponId) > 0)
                    {
                        CouponGrantLogBLL bllCouponGrantLog = new CouponGrantLogBLL(this.CurrentUserInfo);
                        bllCouponGrantLog.Create(new CouponGrantLogEntity() { CouponId = param.CouponId, FromVipId = param.Giver, ToVipId = pRequest.UserID,CouponGrantLogId=Guid.NewGuid() });
                     

                        VipBLL bllVip = new VipBLL(this.CurrentUserInfo);
                        var vipInfo = bllVip.GetVipDetailByVipID(param.Giver);
                        rd.IsSuccess = true;
                        rd.Message = vipInfo.VipName + "赠送你一张" + ds.Tables[0].Rows[0]["CouponName"] + "的优惠券";
                        rd.IsAccept = 0;
                    }
                    else
                    {
                        rd.IsAccept = 1;
                        rd.IsSuccess = false;
                        rd.Message = "领取失败";
                    }
                }
                else
                {
                    rd.IsAccept = 1;
                    rd.IsSuccess = false;
                    rd.Message = "优惠券已被使用";
                }
            }
            catch (Exception ex)
            {
                rd.IsAccept = 0;
                rd.IsSuccess = false;
                rd.Message = ex.Message.ToString();
                throw;
            }

            return rd;
        }
    }
}
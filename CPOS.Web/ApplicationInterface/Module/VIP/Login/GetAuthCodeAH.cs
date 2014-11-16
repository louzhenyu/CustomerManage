using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Util.SMS;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class GetAuthCodeAH : BaseActionHandler<GetAuthCodeRP, GetAuthCodeRD>
    {
        #region 错误码
        const int ERROR_SMS_FAILD = 310;
        const int ERROR_LACK_MOBILE = 311;
        #endregion

        protected override GetAuthCodeRD ProcessRequest(DTO.Base.APIRequest<GetAuthCodeRP> pRequest)
        {
            //参数验证
            if (string.IsNullOrEmpty(pRequest.Parameters.Mobile))
            {
                throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = ERROR_LACK_MOBILE };
            }
            //
            GetAuthCodeRD rd = new GetAuthCodeRD();
            #region 发送短信
            string msg;
            var code = CharsFactory.Create6Char();
            var sign = string.Empty;
            #region 根据客户名称创建签名
            //暂时先用阿拉丁，后须改为客户名称
            //sign = "阿拉丁";
            //sign = CurrentUserInfo.ClientName;

            if (string.IsNullOrEmpty(pRequest.CustomerID))
            {
                throw new APIException("请求参数中缺少CustomerId或值为空.") { ErrorCode = 121 };
            }
            var bll = new VipBLL(CurrentUserInfo);

            sign = !string.IsNullOrEmpty(bll.GetSettingValue(CurrentUserInfo.ClientID)) ? bll.GetSettingValue(CurrentUserInfo.ClientID) : "阿拉丁";


            #endregion
            if (!SMSHelper.Send(pRequest.CustomerID,pRequest.Parameters.Mobile, code, sign, out msg))
            {
                //throw new APIException("短信发送失败:" + msg) { ErrorCode = ERROR_SMS_FAILD };
                throw new APIException(msg) { ErrorCode = ERROR_SMS_FAILD };
            }
            #endregion

            #region 保存验证码
            int ValidPeriod;
            var tempstr = System.Configuration.ConfigurationManager.AppSettings["AuthCodeValidPeriod"];
            if (string.IsNullOrEmpty(tempstr))
            {
                ValidPeriod = 30;
            }
            else
            {
                if (!int.TryParse(tempstr, out ValidPeriod))
                {
                    ValidPeriod = 30;
                }
            }
            var codebll = new RegisterValidationCodeBLL(base.CurrentUserInfo);
            codebll.DeleteByMobile(pRequest.Parameters.Mobile); //删除该手机号已有的验证码
            var codeEntity = new RegisterValidationCodeEntity()
            {
                Code = code,
                CodeID = Guid.NewGuid().ToString("N"),
                Mobile = pRequest.Parameters.Mobile,
                IsValidated = 0,
                Expires = DateTime.Now.AddMinutes(ValidPeriod)
            };
            codebll.Create(codeEntity); //以新的验证码为准
            #endregion
            return rd;

            #region 被替换
            //GetAuthCodeRD rd = new GetAuthCodeRD();
            //VipEntity entity = null;
            //#region 验证手机号，如果不存在此手机号的用户则新增会员
            //var bll = new VipBLL(base.CurrentUserInfo);
            //var vip = bll.GetByID(pRequest.UserID);
            //entity = bll.GetByMobile(pRequest.Parameters.Mobile, pRequest.CustomerID);
            //if (vip != null)
            //{
            //    if (vip.Phone != pRequest.Parameters.Mobile)
            //    {
            //        if (entity != null)
            //        {
            //            if (vip.VIPID != entity.VIPID)
            //                throw new APIException("此手机号已注册") { ErrorCode = ERROR_MOBILE_REGISTERED };
            //            else
            //            {
            //                entity = vip;
            //            }
            //        }
            //        else
            //        {
            //            entity = vip;
            //            vip.Phone = pRequest.Parameters.Mobile;
            //            bll.Update(entity);
            //        }
            //    }
            //    else
            //        entity = vip;
            //}
            //else
            //{
            //    entity = bll.GetByMobile(pRequest.Parameters.Mobile, pRequest.CustomerID);
            //    if (entity == null)
            //    {
            //        entity = new VipEntity()
            //        {
            //            Phone = pRequest.Parameters.Mobile,
            //            VipName = pRequest.Parameters.Mobile,
            //            VIPID = Guid.NewGuid().ToString("N"),
            //            Status = 1,
            //            ClientID = pRequest.CustomerID,
            //            VipSourceId = pRequest.Parameters.VipSource.ToString()
            //        };
            //        bll.Create(entity);
            //    }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(entity.VipName))
            //        {
            //            entity.VipName = pRequest.Parameters.Mobile;
            //            bll.Update(entity);
            //        }
            //    }
            //}
            //#endregion

            //#region 发送短信
            //string msg;
            //var code = CharsFactory.Create6Char();
            //var sign = string.Empty;
            //#region 根据客户名称创建签名
            ////暂时先用阿拉丁，后须改为客户名称
            //sign = "阿拉丁";
            ////sign = CurrentUserInfo.ClientName;
            //#endregion
            //if (!SMSHelper.Send(pRequest.Parameters.Mobile, code, sign, out msg))
            //{
            //    throw new APIException("短信发送失败:" + msg) { ErrorCode = ERROR_SMS_FAILD };
            //}
            //#endregion

            //#region 保存验证码
            //int ValidPeriod;
            //var tempstr = System.Configuration.ConfigurationManager.AppSettings["AuthCodeValidPeriod"];
            //if (string.IsNullOrEmpty(tempstr))
            //{
            //    ValidPeriod = 30;
            //}
            //else
            //{
            //    if (!int.TryParse(tempstr, out ValidPeriod))
            //    {
            //        ValidPeriod = 30;
            //    }
            //}
            //var codebll = new RegisterValidationCodeBLL(base.CurrentUserInfo);
            //codebll.DeleteByMobile(entity.Phone);
            //var codeEntity = new RegisterValidationCodeEntity()
            //{
            //    Code = code,
            //    CodeID = Guid.NewGuid().ToString("N"),
            //    Mobile = entity.Phone,
            //    IsValidated = 0,
            //    Expires = DateTime.Now.AddMinutes(ValidPeriod)
            //};
            //codebll.Create(codeEntity);
            //#endregion
            //return rd;
            #endregion
        }
    }
}
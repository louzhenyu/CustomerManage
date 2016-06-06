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
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL.RedisOperationBLL.Contact;
namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class AuthCodeLoginAH : BaseActionHandler<AuthCodeLoginRP, AuthCodeLoginRD>
    {
        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_VIP_SOURCE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        const int ERROR_MEMBER_REGISTERED = 314;
        #endregion

        protected override AuthCodeLoginRD ProcessRequest(DTO.Base.APIRequest<AuthCodeLoginRP> pRequest)
        {
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            //参数验证
            if (string.IsNullOrEmpty(pRequest.Parameters.Mobile))
            {
                throw new APIException("请求参数中缺少Mobile或值为空.") { ErrorCode = ERROR_LACK_MOBILE };
            }
            if (pRequest.Parameters.VipSource.HasValue == false)
            {
                pRequest.Parameters.VipSource = 1;
                //throw new APIException("请求参数中缺少VipSource或值为空.") { ErrorCode = ERROR_LACK_VIP_SOURCE };
            }
            //
            AuthCodeLoginRD rd = new AuthCodeLoginRD();
            var bll = new VipBLL(this.CurrentUserInfo);
            var codebll = new RegisterValidationCodeBLL(base.CurrentUserInfo);
            VipEntity vipByID = null;       //根据VIP ID查找出来的VIP记录
            VipEntity vipByPhone = null;    //根据手机号查找出来的VIP记录


            //var list = codebll.QueryByEntity(new RegisterValidationCodeEntity()
            //{
            //    Mobile = pRequest.Parameters.Mobile
            //}, null);
            //bool b = false;
            //if (list != null)
            //{
            //    foreach (var entity in list)
            //    {
            //        if (entity == null)
            //            throw new APIException("未找到此手机的验证信息") {ErrorCode = ERROR_AUTHCODE_NOTEXISTS};
            //        //if (entity.IsValidated.Value == 1)
            //        //    throw new APIException("此验证码已被使用") {ErrorCode = ERROR_AUTHCODE_WAS_USED};
            //        //if (entity.Expires.Value < DateTime.Now)
            //        //    throw new APIException("此验证码已失效") {ErrorCode = ERROR_AUTHCODE_INVALID};
            //        if (entity.Code == pRequest.Parameters.AuthCode)
            //            b = true;
            //    }
            //}
            //if (!b)
            //{
            //    throw new APIException("验证码不正确.") { ErrorCode = ERROR_AUTHCODE_NOT_EQUALS };
            //}

            #region 验证验证码
            var entity = codebll.GetByMobile(pRequest.Parameters.Mobile);
            if (entity == null)
                throw new APIException("未找到此手机的验证信息") { ErrorCode = ERROR_AUTHCODE_NOTEXISTS };
            if (entity.IsValidated.Value == 1)
                throw new APIException("此验证码已被使用") { ErrorCode = ERROR_AUTHCODE_WAS_USED };
            if (entity.Expires.Value < DateTime.Now)
                throw new APIException("此验证码已失效") { ErrorCode = ERROR_AUTHCODE_INVALID };
            if (entity.Code != pRequest.Parameters.AuthCode)
                throw new APIException("验证码不正确.") { ErrorCode = ERROR_AUTHCODE_NOT_EQUALS };
            #endregion

            #region 获取会员权益
            var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            var memberBenefit = customerBasicSettingBll.GetMemberBenefits(pRequest.CustomerID);
            var bllPrize = new LPrizesBLL(CurrentUserInfo);
            #endregion

            switch (pRequest.Parameters.VipSource.Value)
            {
                case 3: //来源是微信时,做自动合并
                    {
                        #region 根据手机号查找下,看下是否存在同样手机号的VIP记录
                        {
                            List<IWhereCondition> wheres = new List<IWhereCondition>();
                            wheres.Add(new MoreThanCondition() { FieldName = "status", Value = 0, IncludeEquals = false });//潜在或者正式会员
                            wheres.Add(new EqualsCondition() { FieldName = "phone", Value = pRequest.Parameters.Mobile });
                            wheres.Add(new EqualsCondition() { FieldName = "clientid", Value = pRequest.CustomerID });
                            var result = bll.Query(wheres.ToArray(), null);
                            if (result != null && result.Length > 0)
                                vipByPhone = result[0];
                            if (vipByPhone != null && vipByPhone.VipSourceId == "3" && vipByPhone.Status.Value >= 2)
                            {
                                throw new APIException("会员已经注册") { ErrorCode = ERROR_MEMBER_REGISTERED };
                            }
                        }
                        #endregion

                        #region 根据VIP ID查找下，看下是否存在该VIP的记录
                        if (!string.IsNullOrEmpty(pRequest.UserID))
                        {
                            List<IWhereCondition> wheres = new List<IWhereCondition>();
                            //wheres.Add(new MoreThanCondition() { FieldName = "status", Value = 0, IncludeEquals = false });
                            wheres.Add(new MoreThanCondition() { FieldName = "status", Value = 0, IncludeEquals = true });
                            wheres.Add(new EqualsCondition() { FieldName = "vipid", Value = pRequest.UserID });
                            wheres.Add(new EqualsCondition() { FieldName = "clientid", Value = pRequest.CustomerID });
                            var result = bll.Query(wheres.ToArray(), null);
                            if (result != null && result.Length > 0)
                                vipByID = result[0];
                        }
                        else
                        {//如果前端未指定VIP ID则后台指定
                            pRequest.UserID = Guid.NewGuid().ToString("N");
                        }
                        if (vipByID == null && vipByPhone == null)//根据vipid查不出记录，并且根据手机号也查不出记录 新增一条vip
                        {//如果不存在则首先创建一条VIP记录，补充记录
                            vipByID = new VipEntity()
                            {
                                Phone = pRequest.Parameters.Mobile,
                                VipName = pRequest.Parameters.Mobile,
                                UserName = pRequest.Parameters.Mobile,
                                VipRealName = pRequest.Parameters.VipRealName,
                                VIPID = pRequest.UserID,
                                Status = 2,
                                ClientID = pRequest.CustomerID,
                                VipCode = "Vip" + bll.GetNewVipCode(pRequest.CustomerID),
                                VipSourceId = pRequest.Parameters.VipSource.ToString(),
                                WeiXinUserId = string.IsNullOrWhiteSpace(pRequest.UserID) ? Guid.NewGuid().ToString("N") : pRequest.UserID,
                                RegistrationTime=DateTime.Now
                            };
                            bll.Create(vipByID);
                            #region 注册会员触点活动奖励
                            //bllPrize.CheckIsWinnerForShare(CurrentUserInfo.UserID, "", "Reg");

                            RedisContactBLL redisContactBll = new RedisContactBLL();
                            redisContactBll.SetRedisContact(new RedisOpenAPIClient.Models.CC.CC_Contact()
                            {
                                CustomerId = CurrentUserInfo.ClientID,
                                ContactType = "Reg",
                                VipId = CurrentUserInfo.UserID
                            });

                            #endregion

                        }
                        else if (vipByID != null)
                        {
                            vipByID.Phone = pRequest.Parameters.Mobile;
                            if (!string.IsNullOrEmpty(pRequest.Parameters.VipRealName))
                                vipByID.VipRealName = pRequest.Parameters.VipRealName;
                            vipByID.Status = 2;
                            vipByID.RegistrationTime = DateTime.Now;
                            bll.Update(vipByID);
                            #region 注册会员触点活动奖励
                            //bllPrize.CheckIsWinnerForShare(CurrentUserInfo.UserID, "", "Reg");
                            RedisContactBLL redisContactBll = new RedisContactBLL();
                            redisContactBll.SetRedisContact(new RedisOpenAPIClient.Models.CC.CC_Contact()
                            {
                                CustomerId = CurrentUserInfo.ClientID,
                                ContactType = "Reg",
                                VipId = CurrentUserInfo.UserID
                            });
                            #endregion

                            #region 会员金矿、注册集客奖励
                            bll.SetOffActionReg(vipByID);
                            #endregion
                        }

                        #endregion

                        #region 根据VIP ID及手机号查询出的结果，尝试自动合并会员
                        if (vipByPhone == null)
                        {//如果未有相同手机号的用户，则无须绑定,直接使用VIP ID对应的VIP记录作为当前注册用户的记录
                            rd.MemberInfo = new MemberInfo()
                            {
                                Mobile = vipByID.Phone
                                ,
                                VipID = vipByID.VIPID
                                ,
                                Name = vipByID.UserName
                                ,
                                VipName = vipByID.VipName
                                ,
                                VipNo = vipByID.VipCode
                                ,
                                MemberBenefits = memberBenefit
                                ,
                                IsActivate = vipByID.IsActivate.HasValue && vipByID.IsActivate.Value == 1 ? true : false
                            };
                            //处理绑卡业务 add by Henry 2015/10/28
                            vipCardVipMappingBLL.BindVipCard(vipByID.VIPID, vipByID.VipCode, vipByID.CouponInfo);
                        }
                        else
                        {//否则调用存储过程,做自动会员合并

                            //如果会员已经注册过，并且来源是微信的则表示该帐号已经被注册过不能再次绑定
                            if (vipByPhone.VipSourceId == "3" && vipByPhone.Status.Value >= 2)
                            {
                                throw new APIException("会员已经注册") { ErrorCode = ERROR_MEMBER_REGISTERED };
                            }
                            //否则做会员合并 
                            if (!bll.MergeVipInfo(pRequest.CustomerID, pRequest.UserID, pRequest.Parameters.Mobile))
                            {
                                throw new APIException("自动绑定会员信息失败") { ErrorCode = ERROR_AUTO_MERGE_MEMBER_FAILED };
                            }

                            //合并成功后重新读取信息
                            List<IWhereCondition> wheres = new List<IWhereCondition>();
                            wheres.Add(new MoreThanCondition() { FieldName = "status", Value = 0, IncludeEquals = false });
                            wheres.Add(new EqualsCondition() { FieldName = "vipid", Value = pRequest.UserID });
                            wheres.Add(new EqualsCondition() { FieldName = "clientid", Value = pRequest.CustomerID });
                            var result = bll.Query(wheres.ToArray(), null);
                            vipByID = result[0];
                            rd.MemberInfo = new MemberInfo()
                            {
                                Mobile = vipByID.Phone
                                ,
                                VipID = vipByID.VIPID
                                ,
                                Name = vipByID.UserName
                                ,
                                VipName = vipByID.VipName
                                ,
                                VipNo = vipByID.VipCode
                                ,
                                MemberBenefits = memberBenefit
                                ,
                                IsActivate = vipByID.IsActivate.HasValue && vipByID.IsActivate.Value == 1 ? true : false
                            };
                            //处理绑卡业务 add by Henry 2015/10/28
                            vipCardVipMappingBLL.BindVipCard(vipByID.VIPID, vipByID.VipCode, vipByID.CouponInfo);
                        }
                        #endregion
                    }
                    break;
                default://其他来源则为自动注册
                    {
                        #region 根据手机号查找下,看下是否存在同样手机号的VIP记录
                        {
                            List<IWhereCondition> wheres = new List<IWhereCondition>();
                            wheres.Add(new MoreThanCondition() { FieldName = "status", Value = 0, IncludeEquals = false });
                            wheres.Add(new EqualsCondition() { FieldName = "phone", Value = pRequest.Parameters.Mobile });
                            wheres.Add(new EqualsCondition() { FieldName = "clientid", Value = pRequest.CustomerID });
                            var result = bll.Query(wheres.ToArray(), null);
                            if (result != null && result.Length > 0)
                                vipByPhone = result[0];
                            if (vipByPhone != null && vipByPhone.Status.Value >= 2)
                            {
                                throw new APIException("会员已经注册") { ErrorCode = ERROR_MEMBER_REGISTERED };
                            }
                        }
                        #endregion

                        #region 没有找到相同电话的会员则自动注册
                        if (vipByPhone == null)
                        {//没有找到相同电话的会员则自动注册
                            vipByPhone = new VipEntity()
                            {
                                Phone = pRequest.Parameters.Mobile,
                                VipName = pRequest.Parameters.Mobile,
                                UserName = pRequest.Parameters.Mobile,
                                VipRealName = pRequest.Parameters.VipRealName,
                                VIPID = string.IsNullOrWhiteSpace(pRequest.UserID) ? Guid.NewGuid().ToString("N") : pRequest.UserID,
                                Status = 2, //状态为注册
                                VipCode = "Vip" + bll.GetNewVipCode(pRequest.CustomerID),
                                ClientID = pRequest.CustomerID,
                                VipSourceId = pRequest.Parameters.VipSource.ToString(),
                                WeiXinUserId = string.IsNullOrWhiteSpace(pRequest.UserID) ? Guid.NewGuid().ToString("N") : pRequest.UserID,
                                RegistrationTime=DateTime.Now
                            };
                            bll.Create(vipByPhone);
                            #region 注册会员触点活动奖励
                            //bllPrize.CheckIsWinnerForShare(CurrentUserInfo.UserID, "", "Reg");
                            RedisContactBLL redisContactBll = new RedisContactBLL();
                            redisContactBll.SetRedisContact(new RedisOpenAPIClient.Models.CC.CC_Contact()
                            {
                                CustomerId = CurrentUserInfo.ClientID,
                                ContactType = "Reg",
                                VipId = CurrentUserInfo.UserID
                            });
                            #endregion
                        }
                        #endregion

                        #region

                        decimal EndAmount = 0;
                        VipAmountBLL AmountBLL = new VipAmountBLL(this.CurrentUserInfo);
                        VipAmountEntity amountEntity = AmountBLL.GetByID(vipByPhone.VIPID);


                        if (amountEntity != null)
                        {
                            EndAmount = amountEntity.EndAmount.HasValue ? amountEntity.EndAmount ?? 0 : 0;
                        }

                        #endregion

                        #region 返回用户信息
                        rd.MemberInfo = new MemberInfo()
                        {
                            Mobile = vipByPhone.Phone
                            ,
                            VipID = vipByPhone.VIPID
                            ,
                            Name = vipByPhone.UserName
                            ,
                            VipName = vipByPhone.VipName
                            ,
                            VipRealName = vipByPhone.VipRealName
                            ,
                            VipNo = vipByPhone.VipCode
                            ,
                            MemberBenefits = memberBenefit
                            ,
                            IsActivate = false
                            ,
                            Integration = vipByPhone.Integration ?? 0
                            ,
                            Balance = EndAmount
                        };
                        #endregion
                    }
                    break;
            }

            T_LEventsRegVipLogBLL lEventRegVipLogBll = new T_LEventsRegVipLogBLL(CurrentUserInfo);
            if (!string.IsNullOrEmpty(pRequest.Parameters.CTWEventId))
            {
                lEventRegVipLogBll.CTWRegOrFocusLog(pRequest.Parameters.CTWEventId, pRequest.UserID, "",CurrentUserInfo,"Reg");
            }
            //如果是通过优惠券进来的就有couponId 新注册的需要加记录
            if (!string.IsNullOrEmpty(pRequest.Parameters.couponId))
            {
                lEventRegVipLogBll.CouponRegOrFocusLog(pRequest.Parameters.couponId, pRequest.UserID, "", CurrentUserInfo, "Reg");
            }



            #region 将验证码设置为已验证
            //entity.IsValidated = 1;
            //codebll.Update(entity);
            #endregion

            return rd;

            //AuthCodeLoginRD rd = new AuthCodeLoginRD();
            //rd.MemberInfo = new MemberInfo();
            //var codebll = new RegisterValidationCodeBLL(base.CurrentUserInfo);
            //var entity = codebll.GetByMobile(pRequest.Parameters.Mobile);
            //if (entity == null)
            //    throw new APIException("未找到此手机的验证信息") { ErrorCode = ERROR_AUTHCODE_NOTEXISTS };
            //if (entity.IsValidated.Value == 1)
            //    throw new APIException("此验证码已失效") { ErrorCode = ERROR_AUTHCODE_FAILURE };
            //if (entity.Expires.Value < DateTime.Now)
            //    throw new APIException("此验证码已失效") { ErrorCode = ERROR_AUTHCODE_FAILURE };
            //var vipbll = new VipBLL(base.CurrentUserInfo);
            //var vipinfo = vipbll.GetByMobile(pRequest.Parameters.Mobile, pRequest.CustomerID);

            //#region VIP来源更新
            //switch (pRequest.Parameters.VipSource.Value)
            //{
            //    case 4:
            //    case 9:
            //        vipinfo.VipSourceId = pRequest.Parameters.VipSource.ToString();
            //        vipbll.Update(vipinfo);
            //        break;
            //}
            //#endregion

            //if (string.IsNullOrEmpty(vipinfo.ClientID))
            //{
            //    vipinfo.ClientID = pRequest.CustomerID;
            //    vipbll.Update(vipinfo);
            //}
            //rd.MemberInfo.Mobile = vipinfo.Phone;
            //rd.MemberInfo.Name = vipinfo.UserName;
            //rd.MemberInfo.VipID = vipinfo.VIPID;
            //rd.MemberInfo.VipName = vipinfo.VipName;
            //var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            //rd.MemberInfo.MemberBenefits = customerBasicSettingBll.GetMemberBenefits(pRequest.CustomerID);
            //entity.IsValidated = 1;
            //codebll.Update(entity);
            //return rd;
        }
    }
}
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VIPCard
{
    public class OpenVipCardAH : BaseActionHandler<OpenVipCardRP, SetVipCardRD>
    {
        protected override SetVipCardRD ProcessRequest(DTO.Base.APIRequest<OpenVipCardRP> pRequest)
        {
            var rd = new SetVipCardRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var vipBLL = new VipBLL(loggingSessionInfo);                        //会员业务对象实例化
            var vipTagsMappingBLL = new VipTagsMappingBLL(loggingSessionInfo);  //会员和标签关系业务对象实例化
            var vipCardBLL = new VipCardBLL(loggingSessionInfo);                  //会员卡业务对象实例化
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);          //会员和会员卡关系业务对象实例化
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(loggingSessionInfo);//会员卡状态变更业务对象实例化
            var vipAddressBLL = new VipAddressBLL(loggingSessionInfo);             //会员地址信息业务对象实例化
            var pTran = vipCardBLL.GetTran();   //事务  

            using (pTran.Connection)
            {
                try
                {
                    //创建会员信息
                    var vipEntity = new VipEntity()
                    {
                        VIPID = Guid.NewGuid().ToString().Replace("-", ""),
                        VipName = para.VipName,
                        VipRealName = para.VipName,
                        //VipCode = "Vip" + vipBLL.GetNewVipCode(loggingSessionInfo.ClientID), //获取会员编号
                        VipCode = para.VipCardCode,                                          //会员卡号
                        CouponInfo = loggingSessionInfo.CurrentUserRole.UnitId,              //原会集店字段
                        MembershipStore = loggingSessionInfo.CurrentUserRole.UnitId,         //新增的会集店字段
                        Phone = para.Phone,
                        Birthday = para.Birthday,
                        Gender = para.Gender,
                        Email = para.Email,
                        Col18 = para.IDCard,  //身份证
                        Status = 2,         //已注册状态
                        VipSourceId = "20", //来源开卡
                        ClientID = loggingSessionInfo.ClientID
                    };
                    vipBLL.Create(vipEntity, pTran);

                    //保存年龄段标签
                    var vipTagsMappingEntity = new VipTagsMappingEntity()
                    {
                        MappingId = Guid.NewGuid().ToString().Replace("-", ""),
                        VipId = vipEntity.VIPID,
                        TagsId = para.TagsID
                    };
                    vipTagsMappingBLL.Create(vipTagsMappingEntity, pTran);

                    //保存会员地址信息
                    //var vipAddressEntity = new VipAddressEntity()
                    //{
                    //    VIPID = vipEntity.VIPID,
                    //    LinkMan = para.VipName,
                    //    LinkTel = para.Phone,
                    //    CityID = para.CityID,
                    //    Address = para.Address,
                    //    IsDefault = 1
                    //};
                    //vipAddressBLL.Create(vipAddressEntity, pTran);

                    //更新会员卡信息
                    var vipCardEntity = vipCardBLL.GetByID(para.VipCardID);
                    if (vipCardEntity != null)
                    {
                        #region 返回卡ID
                        rd.VipCardID = vipCardEntity.VipCardID;
                        #endregion
                        //vipCardEntity.VipCardStatusId = 1;
                        if (!string.IsNullOrEmpty(vipCardEntity.MembershipUnit))
                            throw new APIException("此会员卡已绑定会员") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        if (vipCardEntity.VipCardStatusId > 0)
                            throw new APIException("此会员卡状态异常") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        vipCardEntity.MembershipTime = DateTime.Now;
                        vipCardEntity.MembershipUnit = loggingSessionInfo.CurrentUserRole.UnitId;
                        vipCardEntity.IsGift = para.IsGift;
                        //vipCardEntity.SalesUserId = para.SalesUserId;
                        vipCardEntity.SalesUserName = para.SalesUserName;   //用户输入名称，后期需要根据姓名查找用户ID，更新到SalesUserId
                        vipCardEntity.CreateBy = loggingSessionInfo.UserID; //开卡人，即入会员工
                        vipCardBLL.Update(vipCardEntity, pTran);
                    }
                    else
                        throw new APIException("查不到此会员卡") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                    //新增会员卡操作状态信息
                    var vipCardStatusChangeLogEntity = new VipCardStatusChangeLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString().Replace("-", ""),
                        VipCardStatusID = vipCardEntity.VipCardStatusId,
                        VipCardID = para.VipCardID,
                        Action = "开卡",
                        //Reason = "",
                        UnitID = loggingSessionInfo.CurrentUserRole.UnitId,
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    vipCardStatusChangeLogBLL.Create(vipCardStatusChangeLogEntity, pTran);

                    //绑定会员卡和会员
                    var vipCardVipMappingEntity = new VipCardVipMappingEntity()
                    {
                        MappingID = Guid.NewGuid().ToString().Replace("-", ""),
                        VIPID = vipEntity.VIPID,
                        VipCardID = para.VipCardID,
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    vipCardVipMappingBLL.Create(vipCardVipMappingEntity, pTran);

                    pTran.Commit();  //提交事物
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                catch (Exception ex)
                {
                    pTran.Rollback();//回滚事物
                    throw new Exception(ex.Message);
                }
            }
            return rd;
        }
    }
}
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VIPCard
{
    public class GetVipCardDetailAH : BaseActionHandler<GetVIPCardRP, GetVIPCardRD>
    {
        protected override GetVIPCardRD ProcessRequest(DTO.Base.APIRequest<GetVIPCardRP> pRequest)
        {


            GetVIPCardRD Data = new GetVIPCardRD();
            var rd = new GetVIPCardListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            #region 业务对象
            //会员卡
            var VipCardBLL = new VipCardBLL(loggingSessionInfo);
            //会员
            var VipBLL = new VipBLL(loggingSessionInfo);
            //卡状态变更记录
            var VipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(loggingSessionInfo);
            //卡与Vip关系
            var VipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
            //门店
            var unitBll = new t_unitBLL(loggingSessionInfo);
            //员工
            var UserBLL = new T_UserBLL(loggingSessionInfo);
            //卡类型
            var SysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            //商户配置
            var CustomerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);
            //积分
            var VipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
            #endregion



            #region 会员卡
            VipCardEntity resultData = null;
            if (!string.IsNullOrWhiteSpace(para.VipID))
            {
                //会员编号获取详情
                VipCardVipMappingEntity m_VipMappingData = VipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = para.VipID }, null).FirstOrDefault();
                if (m_VipMappingData != null && !string.IsNullOrWhiteSpace(m_VipMappingData.VipCardID))
                    resultData = VipCardBLL.GetByID(para.VipCardID);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(para.VipCardID))
                {
                    //卡ID获取详情
                    resultData = VipCardBLL.GetByID(para.VipCardID);
                }
                else
                {
                    //刷卡获取详情
                    List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                    if (!string.IsNullOrEmpty(para.VipCardISN))
                    {
                        complexCondition.Add(new DirectCondition("(VipCardCode='" + para.VipCardISN + "' or VipCardISN='" + para.VipCardISN.ToString() + "') "));
                    }
                    resultData = VipCardBLL.Query(complexCondition.ToArray(), null).FirstOrDefault();
                }
            }

            try
            {
                if (resultData == null)
                    throw new APIException("该卡不存在！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }
            #endregion

            #region 卡关系
            VipCardVipMappingEntity VipCardVipMappingData = null;
            if (resultData != null)
            {
                VipCardVipMappingData = VipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VipCardID = resultData.VipCardID }, null).FirstOrDefault();
            }
            else
            {
                return Data;
            }
            #endregion

            #region 门店
            t_unitEntity unidData = null;
            if (resultData != null && !string.IsNullOrWhiteSpace(resultData.MembershipUnit))
            {
                unidData = unitBll.GetByID(resultData.MembershipUnit);
            }
            #endregion

            #region 会员,积分

            VipEntity VipData = null;
            VipIntegralEntity IntegralData = null;
            string VIPID = VipCardVipMappingData == null ? "" : VipCardVipMappingData.VIPID;
            if (!string.IsNullOrWhiteSpace(VIPID))
            {
                VipData = VipBLL.NewGetByID(VIPID);
                IntegralData = VipIntegralBLL.GetByID(VIPID);
            }
            #endregion

            #region 员工
            T_UserEntity UserData = null;
            if (resultData != null)//兼容Operator或CreateBy字段获取店员信息
            {
				if (!string.IsNullOrWhiteSpace(resultData.Operator))
				{
                	UserData = UserBLL.GetByID(resultData.Operator);
				}
				else if (!string.IsNullOrWhiteSpace(resultData.CreateBy))
				{
					UserData = UserBLL.GetByID(resultData.CreateBy);
				}
            }
            #endregion

            #region 响应对象赋值
            if (resultData != null)
            {
            //卡
            Data.VipCardID = resultData.VipCardID;
            Data.VipCardCode = resultData.VipCardCode;
            Data.VipCardISN = resultData.VipCardISN;
            Data.CraeteUserName = UserData == null ? "" : UserData.user_name;


            Data.VipCardStatusId = resultData.VipCardStatusId.Value;
            Data.MembershipTime = resultData.MembershipTime == null ? "" : resultData.MembershipTime.Value.ToString("yyyy-MM-dd");
            Data.MembershipUnitName = unidData == null ? "" : unidData.unit_name;
            Data.TotalAmount = resultData.RechargeTotalAmount == null ? 0 : resultData.RechargeTotalAmount.Value;
            Data.BalanceAmount = resultData.BalanceAmount == null ? 0 : resultData.BalanceAmount.Value;
            Data.TotalReturnAmount = resultData.CumulativeBonus == null ? 0 : decimal.Parse(resultData.CumulativeBonus.ToString());
            Data.ValidReturnAmount = resultData.BalanceBonus == null ? 0 : decimal.Parse(resultData.BalanceBonus.ToString());
            Data.BeginDate = resultData.BeginDate;
            Data.EndDate = "永久有效";
            Data.SalesUserName = resultData.SalesUserName == null ? "" : resultData.SalesUserName;
            #region 卡类型名称
            SysVipCardTypeEntity SysVipCardTypeData = SysVipCardTypeBLL.GetByID(resultData.VipCardTypeID);
            Data.VipCardName = SysVipCardTypeData == null ? "" : SysVipCardTypeData.VipCardTypeName;
            #endregion
            #region 会员
            if (VipData != null)
            {
                Data.VipID = VipData.VIPID;
                Data.VipCode = VipData.VipCode;
                Data.VipName = VipData.VipName;
                Data.Phone = VipData.Phone;
                Data.Birthday = VipData.Birthday == null ? "" : VipData.Birthday;
                Data.Gender = VipData.Gender;
                Data.Integration = VipData.Integration == null ? 0 : VipData.Integration.Value;
                //会员创建人姓名
                T_UserEntity VipUserData = null;
                if (!string.IsNullOrWhiteSpace(resultData.CreateBy))
                {
                    VipUserData = UserBLL.GetByID(resultData.CreateBy);
                }
                Data.VipCreateByName = VipUserData == null ? "" : VipUserData.user_name;
                Data.IDNumber = VipData.IDNumber == null ? "" : VipData.IDNumber;

                #region 会员生日是否可修改字段赋值
                //Col22 字段赋值
                CustomerBasicSettingEntity SettingData = CustomerBasicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "FSR_NotTwoUpdateVipBirthday" }, null).FirstOrDefault();
                if (SettingData != null)
                    Data.Col22 = VipData.Col22 == null ? "Y" : VipData.Col22;

                #endregion

            }
            #endregion
            #region 累计积分
            Data.CumulativeIntegral = IntegralData == null ? 0 : IntegralData.CumulativeIntegral.Value;
            #endregion
            #region 状态变更记录列表
            var VipCardStatusChangeLogArray = VipCardStatusChangeLogBLL.Query
                (new IWhereCondition[] { new EqualsCondition() { FieldName = "VipCardID", Value = resultData.VipCardID } },
                 new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).ToList();
            //转换业务对象
            Data.StatusLogList = (from t in VipCardStatusChangeLogArray
                                  select new VipCardStatusChangeLog()
                                  {
                                      CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd"),
                                      UnitName = t.UnitName == null ? "" : t.UnitName,
                                      Action = t.Action == null ? "" : t.Action,
                                      ChangeReason = t.Reason == null ? "" : t.Reason,
                                      Remark = t.Remark == null ? "" : t.Remark,
                                      CreateBy = t.CreateByName,
                                      ImageUrl = t.PicUrl == null ? "" : t.PicUrl
                                  }).ToList();
            #endregion
            }
            #region 会员
            if (VipData != null)
            {
                Data.VipID = VipData.VIPID;
                Data.VipCode = VipData.VipCode;
                if (Data.VipCardCode == null)
                    Data.VipCardCode = VipData.VipCode;
                if (!string.IsNullOrWhiteSpace(VipData.VipRealName))
                    Data.VipName = VipData.VipRealName;
                else
                    Data.VipName = VipData.VipName??"";
                Data.Phone = VipData.Phone;
                Data.Birthday = VipData.Birthday == null ? "" : VipData.Birthday;
                Data.Gender = VipData.Gender ?? 0;
                //Data.Integration = VipData.Integration == null ? 0 : VipData.Integration.Value;
                //会员创建人姓名
                T_UserEntity VipUserData = null;
                if (resultData != null && !string.IsNullOrWhiteSpace(resultData.CreateBy))
                {
                    VipUserData = UserBLL.GetByID(resultData.CreateBy);
                }
                Data.VipCreateByName = VipUserData == null ? "" : VipUserData.user_name;
                Data.IDNumber = VipData.IDNumber == null ? "" : VipData.IDNumber;

                #region 会员生日是否可修改字段赋值
                //Col22 字段赋值
                ////CustomerBasicSettingEntity SettingData = CustomerBasicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "FSR_NotTwoUpdateVipBirthday" }, null).FirstOrDefault();
                ////if (SettingData != null)
                Data.Col22 = VipData.Col22 == null ? "Y" : VipData.Col22;
                #endregion

            }
            #endregion

            #region 积分
            if (IntegralData != null)
            {
                Data.Integration = IntegralData.ValidIntegral != null ? IntegralData.ValidIntegral.Value : 0;
                Data.CumulativeIntegral = IntegralData.CumulativeIntegral != null ? IntegralData.CumulativeIntegral.Value : 0;
            }
            #endregion

            #region 余额和返现
            var vipAmountBLL = new VipAmountBLL(loggingSessionInfo);
            var vipAmountInfo = vipAmountBLL.QueryByEntity(new VipAmountEntity() { VipId = VipData.VIPID, VipCardCode = VipData.VipCode }, null).FirstOrDefault();
            if (vipAmountInfo != null)
            {
                Data.TotalAmount = vipAmountInfo.TotalAmount == null ? 0 : vipAmountInfo.TotalAmount.Value;
                Data.BalanceAmount = vipAmountInfo.EndAmount == null ? 0 : vipAmountInfo.EndAmount.Value;
                Data.ValidReturnAmount = vipAmountInfo.ValidReturnAmount == null ? 0 : vipAmountInfo.ValidReturnAmount.Value;
                Data.TotalReturnAmount = vipAmountInfo.TotalReturnAmount == null ? 0 : vipAmountInfo.TotalReturnAmount.Value;
            }
            #endregion


            #endregion
            return Data;
        }
    }
}
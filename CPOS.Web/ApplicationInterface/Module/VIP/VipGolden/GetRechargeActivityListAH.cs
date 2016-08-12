using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 获取充值活动信息列表
    /// </summary>
    public class GetRechargeActivityListAH : BaseActionHandler<GetRechargeActivityListRP, GetRechargeActivityListRD>
    {
        protected override GetRechargeActivityListRD ProcessRequest(DTO.Base.APIRequest<GetRechargeActivityListRP> pRequest)
        {
            var rd = new GetRechargeActivityListRD();
            var para = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            var vipBLL = new VipBLL(loggingSessionInfo);
            var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
            var vipCardBLL = new VipCardBLL(loggingSessionInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(loggingSessionInfo);
            var vipAmountBLL = new VipAmountBLL(loggingSessionInfo);
            //获取当前会员卡等级
            VipEntity VipInfo = null;
            int? CurVipCardLevel = 0;
            VipInfo = vipBLL.GetByID(para.VipID);
            if (VipInfo != null)
            {
                rd.HeadImgUrl = VipInfo.HeadImgUrl == null ? "" : VipInfo.HeadImgUrl;
                rd.VipName = VipInfo.VipName != null ? VipInfo.VipName : VipInfo.VipRealName != null ? VipInfo.VipRealName : "";
                //获取当前会员积分信息
                //var vipIntegralInfo = vipIntegralBLL.QueryByEntity(new VipIntegralEntity() { VipID = para.VipID, VipCardCode = VipInfo.VipCode }, null).FirstOrDefault();
                //if (vipIntegralInfo != null)
                //{
                //    rd.Integration = vipIntegralInfo.ValidIntegral != null ? vipIntegralInfo.ValidIntegral.Value : 0;
                //}
                //获取当前会员余额信息
                var vipAmountInfo = vipAmountBLL.QueryByEntity(new VipAmountEntity() { VipId=para.VipID,VipCardCode=VipInfo.VipCode},null).FirstOrDefault();
                if (vipAmountInfo != null)
                {
                    rd.VipAmount = vipAmountInfo.EndAmount != null ? vipAmountInfo.EndAmount : 0;
                }
                else
                {
                    rd.VipAmount = 0;
                }
                //获取当前关联信息
                var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = para.VipID, CustomerID = loggingSessionInfo.ClientID },
                    new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
                if (vipCardMappingInfo != null)
                {
                    //获取卡等级主标识信息
                    var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID, VipCardStatusId = 1 }, null).FirstOrDefault();
                    if (vipCardInfo != null)
                    {
                        //获取卡等级信息
                        var vipCardTypeInfo = sysVipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                        if (vipCardTypeInfo != null)
                        {
                            rd.VipCardTypeName = vipCardTypeInfo.VipCardTypeName != null ? vipCardTypeInfo.VipCardTypeName : "";
                            CurVipCardLevel = vipCardTypeInfo.VipCardLevel;
                        }
                        //获取充值活动列表
                        var rechargeStrategyBLL=new RechargeStrategyBLL(loggingSessionInfo);
                        var RechargeActivityInfo = rechargeStrategyBLL.GetRechargeActivityList(loggingSessionInfo.ClientID, vipCardInfo.VipCardTypeID.ToString(),3);
                        if (RechargeActivityInfo != null && RechargeActivityInfo.Tables[0].Rows.Count > 0)
                        {
                            rd.RechargeStrategyList = DataTableToObject.ConvertToList<RechargeStrategyInfo>(RechargeActivityInfo.Tables[0]);
                        }
                    }
                }
            }
            return rd;
        }
    }
}
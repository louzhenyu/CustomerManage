using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 微信端 获取会员权益信息 {默认level==1}
    /// </summary>
    public class GetSysVipCardTypeByLevelAH : BaseActionHandler<GetSysVipCardTypeByLevelRP, GetSysVipCardTypeByLevelRD>
    {
        protected override GetSysVipCardTypeByLevelRD ProcessRequest(DTO.Base.APIRequest<GetSysVipCardTypeByLevelRP> pRequest)
        {
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            SysVipCardTypeBLL bll = new SysVipCardTypeBLL(loggingSessionInfo);
            VipCardRuleBLL VipCardRuleService = new VipCardRuleBLL(loggingSessionInfo);
            VipCardUpgradeRewardBLL VipCardUpgradeRewardService = new VipCardUpgradeRewardBLL(loggingSessionInfo);
            var rd = new GetSysVipCardTypeByLevelRD();
            var parameter = pRequest.Parameters;
            int? CardTypeId = 0;
            //获取等级为1的会员卡
            var model = bll.QueryByEntity(new SysVipCardTypeEntity() { CustomerID = loggingSessionInfo.ClientID, VipCardLevel = parameter.Level, IsDelete = 0 }, null).FirstOrDefault();
            if (model != null)
            {
                //获取等级为1的升级规则信息
                var cardrulemodel = VipCardRuleService.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = model.VipCardTypeID }, null).FirstOrDefault();
                if (cardrulemodel != null)
                {
                    CardTypeId = cardrulemodel.VipCardTypeID;
                    WXVipCardUpgradeRewardInfo _model = new WXVipCardUpgradeRewardInfo();

                    _model.VipCardTypeName = model.VipCardTypeName;
                    _model.Type = 1;
                    if (cardrulemodel.CardDiscount > 0) //会员折扣
                    {
                        _model.Title = "会员折扣";
                        _model.ImagesUrl = "../../../images/common/vipCard/cardLegal.png";
                        _model.CouponTypeName = (Convert.ToDouble(cardrulemodel.CardDiscount) / 10.0).ToString("0.0") + "折";
                        rd.VipCardUpgradeRewardInfoList.Add(_model);
                    }

                    _model = new WXVipCardUpgradeRewardInfo();
                    _model.VipCardTypeName = model.VipCardTypeName;
                    _model.Type = 1;
                    if (cardrulemodel.PaidGivePercetPoints > 0) //消费返积分
                    {
                        _model.Title = "消费返积分";
                        _model.ImagesUrl = "../../../images/common/vipCard/cardLegal2.png";
                        _model.CouponTypeName =  Convert.ToInt32(cardrulemodel.PaidGivePercetPoints) + "%";
                        rd.VipCardUpgradeRewardInfoList.Add(_model);
                    }

                    if (cardrulemodel.PaidGivePoints > 0) //消费返积分
                    {
                        _model.Title = "消费返积分";
                        _model.ImagesUrl = "../../../images/common/vipCard/cardLegal2.png";
                        _model.CouponTypeName = "每" + Convert.ToInt32(cardrulemodel.PaidGivePoints) + "元获1积分";
                        rd.VipCardUpgradeRewardInfoList.Add(_model);
                    }

                }
            }
            //获取开卡礼
            var VipCardUpgradeRewardDs = VipCardUpgradeRewardService.GetVipCardUpgradeRewardList(CardTypeId, loggingSessionInfo.ClientID);

            if (VipCardUpgradeRewardDs != null && VipCardUpgradeRewardDs.Tables[0].Rows.Count > 0)
            {
                rd.VipCardUpgradeRewardInfoList.AddRange(DataTableToObject.ConvertToList<WXVipCardUpgradeRewardInfo>(VipCardUpgradeRewardDs.Tables[0]));
            }
            return rd;
        }
    }
}
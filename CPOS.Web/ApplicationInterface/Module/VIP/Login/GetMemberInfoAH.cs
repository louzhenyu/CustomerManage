﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using System.Collections;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class GetMemberInfoAH : BaseActionHandler<GetMemberInfoRP, GetMemberInfoRD>
    {
        protected override GetMemberInfoRD ProcessRequest(DTO.Base.APIRequest<GetMemberInfoRP> pRequest)
        {
            GetMemberInfoRD rd = new GetMemberInfoRD();
            rd.MemberInfo = new MemberInfo();
            var vipLoginBLL = new VipBLL(base.CurrentUserInfo);
            //如果有一个查询标识非空，就用查询标识查，发现没有会员就报错
            if (!string.IsNullOrEmpty(pRequest.Parameters.SearchFlag))
            {
                List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                complexCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = CurrentUserInfo.ClientID });
                var cond1 = new LikeCondition() { FieldName = "VipName", Value = "%" + pRequest.Parameters.SearchFlag + "%" };
                var cond2 = new LikeCondition() { FieldName = "VipRealName", Value = "%" + pRequest.Parameters.SearchFlag + "%" };
                var com1 = new ComplexCondition() { Left = cond1, Right = cond2, Operator = LogicalOperators.Or };

                var cond3 = new EqualsCondition() { FieldName = "Phone", Value = pRequest.Parameters.SearchFlag };
                var com2 = new ComplexCondition() { Left = com1, Right = cond3, Operator = LogicalOperators.Or };
                complexCondition.Add(com2);
                complexCondition.Add(new DirectCondition("(WeiXinUserId!='' Or WeiXinUserId IS NOT NULL)"));
                var tempVipList = vipLoginBLL.Query(complexCondition.ToArray(), null);
                if (tempVipList != null && tempVipList.Length != 0)
                {
                    pRequest.UserID = pRequest.Parameters.MemberID = tempVipList[0].VIPID;
                }
            }

            string m_MemberID = "";
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.OwnerVipID))
                m_MemberID = string.IsNullOrWhiteSpace(pRequest.Parameters.OwnerVipID) ? pRequest.UserID : pRequest.Parameters.OwnerVipID;
            else
                m_MemberID = string.IsNullOrWhiteSpace(pRequest.Parameters.MemberID) ? pRequest.UserID : pRequest.Parameters.MemberID;

            string UserID = m_MemberID;
            var VipLoginInfo = vipLoginBLL.GetByID(UserID);
            if (VipLoginInfo == null)
                throw new APIException("用户不存在") { ErrorCode = 330 };
            #region 20140909 kun.zou 发现状态为0，改为1
            if (VipLoginInfo.Status.HasValue && VipLoginInfo.Status == 0)
            {
                VipLoginInfo.Status = 1;
                vipLoginBLL.Update(VipLoginInfo, false);
                var log = new VipLogEntity()
                {
                    Action = "更新",
                    ActionRemark = "vip状态为0的改为1.",
                    CreateBy = UserID,
                    CreateTime = DateTime.Now,
                    VipID = VipLoginInfo.VIPID,
                    LogID = Guid.NewGuid().ToString("N")
                };
                var logBll = new VipLogBLL(base.CurrentUserInfo);
                logBll.Create(log);
            }
            #endregion
            int couponCount = vipLoginBLL.GetVipCoupon(UserID);

            rd.MemberInfo.Mobile = VipLoginInfo.Phone;//手机号码
            rd.MemberInfo.Name = VipLoginInfo.UserName; //姓名
            rd.MemberInfo.VipID = VipLoginInfo.VIPID;  //组标识
            rd.MemberInfo.VipName = VipLoginInfo.VipName; //会员名
            rd.MemberInfo.ImageUrl = VipLoginInfo.HeadImgUrl;//会员头像  add by Henry 2014-12-5
            rd.MemberInfo.VipRealName = VipLoginInfo.VipRealName;
            rd.MemberInfo.VipNo = VipLoginInfo.VipCode;
            rd.MemberInfo.IsDealer = VipLoginInfo.Col48 != null ? int.Parse(VipLoginInfo.Col48) : 0;
            //超级分销体系配置表 为判断是否存在分销体系使用
            var T_SuperRetailTraderConfigbll = new T_SuperRetailTraderConfigBLL(CurrentUserInfo);
            //获取分销体系信息
            var T_SuperRetailTraderConfigInfo = T_SuperRetailTraderConfigbll.QueryByEntity(new T_SuperRetailTraderConfigEntity() { IsDelete = 0, CustomerId = CurrentUserInfo.CurrentUser.customer_id }, null).FirstOrDefault();
            if (T_SuperRetailTraderConfigInfo != null)
            {
                rd.MemberInfo.CanBeSuperRetailTrader = 1;
            }
            else
            {
                rd.MemberInfo.CanBeSuperRetailTrader = 0;
            }
            rd.MemberInfo.SuperRetailTraderID = VipLoginInfo.Col26;
            //rd.MemberInfo.Integration = VipLoginInfo.Integration ?? 0;//会员积分

            #region 会员有效积分
            var vipIntegralBLL = new VipIntegralBLL(CurrentUserInfo);
            var vipIntegralInfo = vipIntegralBLL.QueryByEntity(new VipIntegralEntity() { VipID = UserID, VipCardCode = VipLoginInfo.VipCode }, null).FirstOrDefault();
            if (vipIntegralInfo != null)
                rd.MemberInfo.Integration = vipIntegralInfo.ValidIntegral != null ? vipIntegralInfo.ValidIntegral.Value : 0;
            #endregion

            //会员等级
            //rd.MemberInfo.VipLevelName = string.IsNullOrEmpty(vipLoginBLL.GetVipLeave(UserID)) ? null : vipLoginBLL.GetVipLeave(UserID);
            #region 会员卡名称
            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardTypeBLL = new SysVipCardTypeBLL(CurrentUserInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(CurrentUserInfo);
            var vipT_InoutBLL = new T_InoutBLL(CurrentUserInfo);
            VipAmountBLL vipAmountBLL = new VipAmountBLL(CurrentUserInfo);
            ShoppingCartBLL service = new ShoppingCartBLL(CurrentUserInfo);
            ShoppingCartEntity queryEntity = new ShoppingCartEntity();
            queryEntity.VipId = UserID;
            int totalQty = service.GetListQty(queryEntity);
            rd.MemberInfo.ShopCartCount = totalQty;
            //定义当前自定义等级
            int? CurVipLevel = 0;
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = UserID, CustomerID = CurrentUserInfo.ClientID },
                new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
            if (vipCardMappingInfo != null)
            {
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID, VipCardStatusId = 1 }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    var vipCardTypeInfo = vipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                    if (vipCardTypeInfo != null)
                    {
                        rd.MemberInfo.VipLevelName = vipCardTypeInfo.VipCardTypeName != null ? vipCardTypeInfo.VipCardTypeName : "";
                        rd.MemberInfo.CardTypeImageUrl = vipCardTypeInfo.PicUrl != null ? vipCardTypeInfo.PicUrl : "";
                        rd.MemberInfo.CardTypePrice = vipCardTypeInfo.Prices != null ? vipCardTypeInfo.Prices.Value : 0;
                        rd.MemberInfo.IsExtraMoney = vipCardTypeInfo.IsExtraMoney != null ? vipCardTypeInfo.IsExtraMoney : 0;
                        CurVipLevel = vipCardTypeInfo.VipCardLevel;
                        rd.MemberInfo.IsPrepaid = vipCardTypeInfo.Isprepaid;
                        if (CurVipLevel > 1)
                        {
                            var VipCardTypeSysInfo = vipCardTypeBLL.GetBindVipCardTypeInfo(CurrentUserInfo.ClientID, VipLoginInfo.Phone, pRequest.UserID, CurVipLevel);
                            if (VipCardTypeSysInfo == null || VipCardTypeSysInfo.Tables[0].Rows.Count == 0)
                            {
                                rd.MemberInfo.IsNeedCard = 2;
                            }
                        }
                        else
                        {
                            rd.MemberInfo.IsNeedCard = 1;
                        }
                        //因为卡等级不能重复，知道当前等级后查找相应的下一级是否是自动升级(消费升级)
                        var NextVipCardTypeInfo = vipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardLevel = CurVipLevel + 1, CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                        if (NextVipCardTypeInfo != null)
                        {
                            //获取当前会员的消费金额信息
                            decimal CurVipConsumptionAmount = 0;
                            decimal VipConsumptionInfo = vipT_InoutBLL.GetVipSumAmount(UserID);
                            if (VipConsumptionInfo > 0)
                            {
                                CurVipConsumptionAmount = Convert.ToDecimal(Convert.ToDecimal(VipConsumptionInfo).ToString("0.00"));
                            }
                            //知道下一等级后要判断，升级条件是否是消费升级
                            //定义累积消费金额
                            decimal AccumulatedAmount = 0;
                            var vipCardTypeUpGradeInfo = vipCardTypeBLL.GetVipCardTypeUpGradeInfo(CurrentUserInfo.ClientID, CurVipLevel);
                            if (vipCardTypeUpGradeInfo != null && vipCardTypeUpGradeInfo.Tables[0].Rows.Count > 0)
                            {
                                //判断拉取的所有卡等级里的最近的一级满足条件即可返回提示所需的值
                                int flag = 0;
                                foreach (DataRow dr in vipCardTypeUpGradeInfo.Tables[0].Rows)
                                {
                                    AccumulatedAmount = Convert.ToDecimal(Convert.ToDecimal(dr["BuyAmount"].ToString()).ToString("0.00"));
                                    //获取消费升级还需多少钱
                                    if (AccumulatedAmount > CurVipConsumptionAmount && flag == 0)
                                    {
                                        flag = 1;
                                        rd.MemberInfo.UpGradeNeedMoney = AccumulatedAmount - CurVipConsumptionAmount;
                                        if (rd.MemberInfo.UpGradeNeedMoney > 0)
                                        {
                                            rd.MemberInfo.UpgradePrompt = "还需要消费" + rd.MemberInfo.UpGradeNeedMoney + "元可以升级啦，快点通知会员！";
                                        }
                                    }
                                }
                            }


                        }

                        var VipCardRuleInfo = vipCardRuleBLL.QueryByEntity(new VipCardRuleEntity() { CustomerID = CurrentUserInfo.ClientID, VipCardTypeID = vipCardTypeInfo.VipCardTypeID }, null).FirstOrDefault();
                        if (VipCardRuleInfo != null)
                        {
                            rd.MemberInfo.CardDiscount = Convert.ToDecimal(Convert.ToDecimal((VipCardRuleInfo.CardDiscount / 10)).ToString("0.00"));
                        }
                    }
                }
            }
            else
            {
                //表示需要领卡，并展示等级为1的默认卡
                rd.MemberInfo.IsNeedCard = 0;
                var VipCardTypeData = vipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardLevel = 1, CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                if (VipCardTypeData != null)
                {
                    rd.MemberInfo.CardTypeImageUrl = VipCardTypeData.PicUrl != null ? VipCardTypeData.PicUrl : "";
                }

            }
            //获取红利/// decimal[0]=总收入(红利) decimal[2]=支出余额 decimal[1]=总提现金额
            decimal[] array = vipAmountBLL.GetVipSumAmountByCondition(UserID, CurrentUserInfo.ClientID, true);
            if (array[0] != null)
            {
                rd.MemberInfo.ProfitAmount = array[0];
            }
            //是否有付费的会员卡
            List<IWhereCondition> freeCardCon = new List<IWhereCondition> { };
            freeCardCon.Add(new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID });
            freeCardCon.Add(new DirectCondition("Prices>0"));
            var freeCardTypeInfo = vipCardTypeBLL.Query(freeCardCon.ToArray(), null).FirstOrDefault();
            if (freeCardTypeInfo != null)
                rd.MemberInfo.IsCostCardType = 1;
            else
                rd.MemberInfo.IsCostCardType = 0;

            #endregion

            rd.MemberInfo.Status = VipLoginInfo.Status.HasValue ? VipLoginInfo.Status.Value : 1;
            rd.MemberInfo.CouponsCount = couponCount;
            rd.MemberInfo.IsActivate = (VipLoginInfo.IsActivate.HasValue && VipLoginInfo.IsActivate.Value == 1 ? true : false);
            var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            rd.MemberInfo.MemberBenefits = customerBasicSettingBll.GetMemberBenefits(pRequest.CustomerID);



            //获取标签信息
            TagsBLL TagsBLL = new TagsBLL(base.CurrentUserInfo);


            var dsIdentity = TagsBLL.GetVipTagsList("", UserID);//“车主标签”  传7
            if (dsIdentity != null && dsIdentity.Tables.Count > 0 && dsIdentity.Tables[0].Rows.Count > 0)
            {
                rd.IdentityTagsList = DataTableToObject.ConvertToList<TagsInfo>(dsIdentity.Tables[0]).ToArray(); //“年龄段”  传8
            }


            #region 获取注册表单的列明和值

            var vipEntity = vipLoginBLL.QueryByEntity(new VipEntity()
            {
                VIPID = UserID
            }, null);

            if (vipEntity == null || vipEntity.Length == 0)
            {
                return rd;
            }
            else
            {
                var ds = vipLoginBLL.GetVipColumnInfo(CurrentUserInfo.ClientID, "online005");

                var vipDs = vipLoginBLL.GetVipInfo(UserID);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    var temp = ds.Tables[0].AsEnumerable().Select(t => new MemberControlInfo()
                    {
                        ColumnName = t["ColumnName"].ToString(),
                        ControlType = Convert.ToInt32(t["ControlType"]),
                        ColumnValue = vipDs.Tables[0].Rows[0][t["ColumnName"].ToString()].ToString(),
                        ColumnDesc = t["columnDesc"].ToString()

                    });

                    rd.MemberControlList = temp.ToArray();
                }
            }

            //var vipamountBll = new VipAmountBLL(this.CurrentUserInfo);

            //var vipAmountEntity = vipamountBll.GetByID(UserID);


            var unitBll = new UnitBLL(this.CurrentUserInfo);
            //Hashtable htPara = new Hashtable();
            //htPara["MemberID"] = UserID;
            //htPara["CustomerID"] = CurrentUserInfo.ClientID;
            //htPara["PageIndex"] = 1;
            //htPara["PageSize"] = 10;
            //DataSet dsMyAccount = unitBll.GetMyAccount(htPara);

            //if (dsMyAccount.Tables[0].Rows.Count > 0)
            //{
            //    //rd.AccountList = DataTableToObject.ConvertToList<AccountInfo>(dsMyAccount.Tables[0]);
            //    //rd.MemberInfo.Balance = Convert.ToDecimal(dsMyAccount.Tables[0].Rows[0]["Total"].ToString());
            //    //rd.TotalPageCount = int.Parse(dsMyAccount.Tables[0].Rows[0]["PageCount"].ToString());

            //}
            //else
            //    rd.MemberInfo.Balance = 0;

            //返现 add by Henry 2015-4-15
            var vipAmountBll = new VipAmountBLL(CurrentUserInfo);
            //var vipAmountInfo = vipAmountBll.GetByID(UserID);
            var vipAmountInfo = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = UserID, VipCardCode = VipLoginInfo.VipCode }, null).FirstOrDefault();
            decimal returnAmount = 0;
            decimal amount = 0;
            if (vipAmountInfo != null)
            {
                returnAmount = vipAmountInfo.ValidReturnAmount == null ? 0 : vipAmountInfo.ValidReturnAmount.Value;
                amount = vipAmountInfo.EndAmount == null ? 0 : vipAmountInfo.EndAmount.Value;
            }
            rd.MemberInfo.ReturnAmount = returnAmount;//返现
            rd.MemberInfo.Balance = amount;//余额
            #endregion


            //获取订单的日期和订单的里的商品名称、商品单价、商品数量
            //先获取订单列表，再获取订单详细信息
            int? pageSize = 3;//rp.Parameters.PageSize;,只取三条记录
            int? pageIndex = 1; //rp.Parameters.PageIndex;
            string OrderBy = "";
            string OrderType = "";
            T_InoutBLL T_InoutBLL = new T_InoutBLL(CurrentUserInfo);
            InoutService InoutService = new InoutService(CurrentUserInfo);
            //只取状态为700的
            //根据订单列表取订单详情
            DataSet dsOrder = T_InoutBLL.GetOrdersByVipID(rd.MemberInfo.VipID, pageIndex ?? 1, pageSize ?? 15, OrderBy, OrderType);//获取会员信息            
            if (dsOrder != null && dsOrder.Tables.Count != 0 && dsOrder.Tables[0].Rows.Count != 0)
            {
                List<JIT.CPOS.DTO.Module.VIP.Login.Response.OrderInfo> orderList = DataTableToObject.ConvertToList<JIT.CPOS.DTO.Module.VIP.Login.Response.OrderInfo>(dsOrder.Tables[1]);
                foreach (JIT.CPOS.DTO.Module.VIP.Login.Response.OrderInfo oi in orderList)
                {
                    IList<InoutDetailInfo> detailList = InoutService.GetInoutDetailInfoByOrderId(oi.order_id);
                    oi.DetailList = detailList;
                }
                rd.OrderList = orderList;
            }

            MessageInfo message = new MessageInfo();


            InnerGroupNewsBLL InnerGroupNewsService = new InnerGroupNewsBLL(CurrentUserInfo);
            SetoffToolsBLL setofftoolsService = new SetoffToolsBLL(CurrentUserInfo);

            //1=微信用户 2=APP员工
            int UnReadInnerMessageCount = InnerGroupNewsService.GetVipInnerGroupNewsUnReadCount(CurrentUserInfo.UserID, pRequest.CustomerID, 1, null, Convert.ToDateTime(VipLoginInfo.CreateTime));
            var setofftoolsMessageCount = setofftoolsService.GetUnReadSetoffToolsCount(CurrentUserInfo.UserID, pRequest.CustomerID, 1, 1);
            message.InnerGroupNewsCount = UnReadInnerMessageCount;
            message.SetoffToolsCount = setofftoolsMessageCount;
            var VipUpNewsInfo = InnerGroupNewsService.GetVipInnerNewsInfo(CurrentUserInfo.ClientID, 2, 1, 3, 0);
            if (VipUpNewsInfo != null && VipUpNewsInfo.Tables[0].Rows.Count > 0)
            {
                message.UpGradeSucess = VipUpNewsInfo.Tables[0].Rows[0]["Text"] != null ? VipUpNewsInfo.Tables[0].Rows[0]["Text"].ToString() : "";
                //获取通知信息 并更新已读状态 数据如果不是一条进行更新所有数据
                var newsUserMappingBLL = new NewsUserMappingBLL(CurrentUserInfo);
                if (VipUpNewsInfo.Tables[0].Rows.Count > 1)//数据如果不是一条进行更新所有数据
                {
                    foreach (DataRow VipNewsdr in VipUpNewsInfo.Tables[0].Rows)
                    {
                        var vipNewsInfo = newsUserMappingBLL.QueryByEntity(new NewsUserMappingEntity() { CustomerId = CurrentUserInfo.ClientID, MappingID = VipNewsdr["MappingID"].ToString() }, null).FirstOrDefault();
                        if (vipNewsInfo != null)
                        {
                            vipNewsInfo.HasRead = 1;
                            newsUserMappingBLL.Update(vipNewsInfo);
                        }
                    }
                }
                else 
                {
                    var vipNewsInfo = newsUserMappingBLL.QueryByEntity(new NewsUserMappingEntity() { CustomerId = CurrentUserInfo.ClientID, MappingID = VipUpNewsInfo.Tables[0].Rows[0]["MappingID"].ToString() }, null).FirstOrDefault();
                    if (vipNewsInfo != null)
                    {
                        vipNewsInfo.HasRead = 1;
                        newsUserMappingBLL.Update(vipNewsInfo);
                    }
                }
                
            }
            else
            {
                message.UpGradeSucess = "";
            }
            rd.MessageInfo = message;
            return rd;
        }
    }
}
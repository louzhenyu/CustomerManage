using System;
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
                var tempVipList = vipLoginBLL.Query(complexCondition.ToArray(), null);
                if (tempVipList != null && tempVipList.Length != 0)
                {
                    pRequest.UserID = pRequest.Parameters.MemberID = tempVipList[0].VIPID;
                }
            }

            string m_MemberID = "";
            if(!string.IsNullOrWhiteSpace(pRequest.Parameters.OwnerVipID))
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
            
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = CurrentUserInfo.UserID }, null).FirstOrDefault();
            if (vipCardMappingInfo != null)
            {
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    var vipCardTypeInfo = vipCardTypeBLL.QueryByEntity(new SysVipCardTypeEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                    rd.MemberInfo.VipLevelName = vipCardTypeInfo != null ? vipCardTypeInfo.VipCardTypeName : "";
                    rd.MemberInfo.CardTypeImageUrl = vipCardTypeInfo != null ? vipCardTypeInfo.PicUrl : "";
                }
            }
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



            return rd;
        }
    }
}
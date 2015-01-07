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

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Login
{
    public class GetMemberInfoAH : BaseActionHandler<GetMemberInfoRP, GetMemberInfoRD>
    {
        protected override GetMemberInfoRD ProcessRequest(DTO.Base.APIRequest<GetMemberInfoRP> pRequest)
        {
            GetMemberInfoRD rd = new GetMemberInfoRD();
            rd.MemberInfo = new MemberInfo();
            var vipLoginBLL = new VipBLL(base.CurrentUserInfo);
            string UserID =string.IsNullOrWhiteSpace(pRequest.Parameters.MemberID)?pRequest.UserID:pRequest.Parameters.MemberID;
            var VipLoginInfo = vipLoginBLL.GetByID(UserID);
            if (VipLoginInfo == null)
                throw new APIException("用户不存在") { ErrorCode = 330 };
            #region 20140909 kun.zou 发现状态为0，改为1
            if (VipLoginInfo.Status.HasValue && VipLoginInfo.Status==0)
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
            rd.MemberInfo.Integration = VipLoginInfo.Integration ?? 0;
            rd.MemberInfo.VipLevelName = string.IsNullOrEmpty(vipLoginBLL.GetVipLeave(UserID)) ? null : vipLoginBLL.GetVipLeave(UserID);

            rd.MemberInfo.Status = VipLoginInfo.Status.HasValue?VipLoginInfo.Status.Value:1;
            rd.MemberInfo.CouponsCount = couponCount;
            rd.MemberInfo.IsActivate = (VipLoginInfo.IsActivate.HasValue && VipLoginInfo.IsActivate.Value == 1 ? true : false);
            var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);
            rd.MemberInfo.MemberBenefits = customerBasicSettingBll.GetMemberBenefits(pRequest.CustomerID);


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
            Hashtable htPara = new Hashtable();
            htPara["MemberID"] = UserID;
            htPara["CustomerID"] = CurrentUserInfo.ClientID;
            htPara["PageIndex"] = 1;
            htPara["PageSize"] = 10;
            DataSet dsMyAccount = unitBll.GetMyAccount(htPara);

            if (dsMyAccount.Tables[0].Rows.Count > 0)
            {
                //rd.AccountList = DataTableToObject.ConvertToList<AccountInfo>(dsMyAccount.Tables[0]);
                rd.MemberInfo.Balance = Convert.ToDecimal(dsMyAccount.Tables[0].Rows[0]["Total"].ToString());
                //rd.TotalPageCount = int.Parse(dsMyAccount.Tables[0].Rows[0]["PageCount"].ToString());
            }
            else
                rd.MemberInfo.Balance = 0;
            #endregion
            return rd;
        }
    }
}
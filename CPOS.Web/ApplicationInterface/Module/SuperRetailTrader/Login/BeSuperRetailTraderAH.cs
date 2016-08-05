using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.Login
{
    /// <summary>
    /// 成为分销商功能接口
    /// </summary>
    public class BeSuperRetailTraderAH : BaseActionHandler<BeSuperRetailTraderRP, BeSuperRetailTraderRD>
    {
        protected override BeSuperRetailTraderRD ProcessRequest(DTO.Base.APIRequest<BeSuperRetailTraderRP> pRequest)
        {

            var rd = new BeSuperRetailTraderRD();
            var para = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            var vipBll = new VipBLL(loggingSessionInfo);
            var userBll = new T_UserBLL(loggingSessionInfo);
            var T_SuperRetailTraderEntityBll=new T_SuperRetailTraderBLL(loggingSessionInfo);
            var InnerGroupNewsbll = new InnerGroupNewsBLL(loggingSessionInfo);//目前还没有针对单对单通知的站内信
            WMenuBLL menuServer = new WMenuBLL(Default.GetAPLoggingSession(""));
            string customerCode = menuServer.GetCustomerCodeByCustomerID(loggingSessionInfo.CurrentUser.customer_id);
            if (!string.IsNullOrEmpty(para.BeRYType))
            {
                VipEntity VipInfo = null; //会员实体
                T_UserEntity T_UserInfo = null;//员工实体
                T_SuperRetailTraderEntity T_SuperRetailTraderInfo = null;//超级分销商实体
                T_SuperRetailTraderInfo=T_SuperRetailTraderEntityBll.QueryByEntity(new T_SuperRetailTraderEntity(){CustomerId=loggingSessionInfo.CurrentUser.customer_id,SuperRetailTraderFromId=para.SuperRetailTraderFromId},null).FirstOrDefault();
                //判断是否已成为分销商
                if (T_SuperRetailTraderInfo == null)
                {
                    var SuperRetailTraderEntity = new T_SuperRetailTraderEntity();
                    //不是分销商 就判断分销商来源类型
                    if (para.BeRYType == "Vip")
                    {
                        //查询会员信息
                        VipInfo = vipBll.QueryByEntity(new VipEntity() { ClientID = loggingSessionInfo.CurrentUser.customer_id, VIPID = para.SuperRetailTraderFromId }, null).FirstOrDefault();
                        
                        if (VipInfo != null)
                        {
                            try
                            {
                                SuperRetailTraderEntity.SuperRetailTraderID = Guid.NewGuid();
                                SuperRetailTraderEntity.SuperRetailTraderCode = VipInfo.VipCode;
                                SuperRetailTraderEntity.SuperRetailTraderName = VipInfo.VipRealName == null ? VipInfo.VipName : VipInfo.VipRealName;
                                SuperRetailTraderEntity.SuperRetailTraderLogin = VipInfo.Phone;
                                string strSuperPwd = StringUtil.GetRandomStr(6);//生成6位随机数,为超级分销商密码生成使用
                                SuperRetailTraderEntity.SuperRetailTraderPass = EncryptManager.Hash(strSuperPwd, HashProviderType.MD5);
                                SuperRetailTraderEntity.SuperRetailTraderPassData = strSuperPwd;
                                SuperRetailTraderEntity.SuperRetailTraderMan = VipInfo.VipRealName == null ? VipInfo.VipName : VipInfo.VipRealName;
                                SuperRetailTraderEntity.SuperRetailTraderPhone = VipInfo.Phone;
                                SuperRetailTraderEntity.SuperRetailTraderAddress = "";//目前因为没有填写详细地址的地方，给空值
                                SuperRetailTraderEntity.SuperRetailTraderFrom = para.BeRYType;
                                SuperRetailTraderEntity.SuperRetailTraderFromId = VipInfo.VIPID;
                                if (!string.IsNullOrEmpty(para.HigheSuperRetailTraderID))
                                {
                                    SuperRetailTraderEntity.HigheSuperRetailTraderID = new Guid(para.HigheSuperRetailTraderID);
                                }
                                if (!string.IsNullOrEmpty(VipInfo.Col20))
                                {
                                    SuperRetailTraderEntity.HigheSuperRetailTraderID = new Guid(VipInfo.Col20);
                                }
                                SuperRetailTraderEntity.JoinTime = System.DateTime.Now;
                                SuperRetailTraderEntity.CreateTime = System.DateTime.Now;
                                SuperRetailTraderEntity.CreateBy = VipInfo.VIPID;
                                SuperRetailTraderEntity.LastUpdateBy = VipInfo.VIPID;
                                SuperRetailTraderEntity.LastUpdateTime = System.DateTime.Now;
                                SuperRetailTraderEntity.IsDelete = 0;
                                SuperRetailTraderEntity.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                                SuperRetailTraderEntity.Status = "00";
                                T_SuperRetailTraderEntityBll.Create(SuperRetailTraderEntity);
                                rd.IsSuperRetailTrader = 1;                                
                                rd.SuperRetailTraderLogin = VipInfo.Phone;
                                rd.SuperRetailTraderPass = strSuperPwd;
                                VipInfo.Col26 = SuperRetailTraderEntity.SuperRetailTraderID.ToString();
                                vipBll.Update(VipInfo,false);
                            }
                            catch
                            {
                                rd.IsSuperRetailTrader = 0;
                            }

                        }
                    }
                    //不是分销商 就判断分销商来源类型
                    if (para.BeRYType == "User")
                    {
                        //查询员工信息
                        T_UserInfo = userBll.QueryByEntity(new T_UserEntity() { customer_id = loggingSessionInfo.CurrentUser.customer_id, user_id = para.SuperRetailTraderFromId }, null).FirstOrDefault();
                        if (T_UserInfo != null)
                        {
                            try
                            {
                                SuperRetailTraderEntity.SuperRetailTraderID = Guid.NewGuid();
                                SuperRetailTraderEntity.SuperRetailTraderCode = T_UserInfo.user_code;
                                SuperRetailTraderEntity.SuperRetailTraderName = T_UserInfo.user_name;
                                SuperRetailTraderEntity.SuperRetailTraderLogin = T_UserInfo.user_code;
                                SuperRetailTraderEntity.SuperRetailTraderPass = T_UserInfo.user_password;
                                SuperRetailTraderEntity.SuperRetailTraderMan = T_UserInfo.user_name;
                                SuperRetailTraderEntity.SuperRetailTraderPhone = T_UserInfo.user_telephone != null ? T_UserInfo.user_telephone : T_UserInfo.user_cellphone;
                                SuperRetailTraderEntity.SuperRetailTraderAddress = T_UserInfo.user_address;
                                SuperRetailTraderEntity.SuperRetailTraderFrom = para.BeRYType;
                                SuperRetailTraderEntity.SuperRetailTraderFromId = T_UserInfo.user_id;
                                if (!string.IsNullOrEmpty(para.HigheSuperRetailTraderID))
                                {
                                    SuperRetailTraderEntity.HigheSuperRetailTraderID = new Guid(para.HigheSuperRetailTraderID);
                                }
                                SuperRetailTraderEntity.JoinTime = System.DateTime.Now;
                                SuperRetailTraderEntity.CreateTime = System.DateTime.Now;
                                SuperRetailTraderEntity.CreateBy = T_UserInfo.user_id;
                                SuperRetailTraderEntity.LastUpdateBy = T_UserInfo.user_id;
                                SuperRetailTraderEntity.LastUpdateTime = System.DateTime.Now;
                                SuperRetailTraderEntity.IsDelete = 0;
                                SuperRetailTraderEntity.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                                SuperRetailTraderEntity.Status = "00";
                                T_SuperRetailTraderEntityBll.Create(SuperRetailTraderEntity);
                                rd.IsSuperRetailTrader = 1;//成功成为分销商
                            }
                            catch
                            {
                                rd.IsSuperRetailTrader = 0;//成为分销商失败
                            }

                        }
                    }
                    //如果成为分销商 则需要增加站内信通知
                    if (rd.IsSuperRetailTrader == 1)
                    {
 
                    }
                }
                else
                {
                    rd.IsSuperRetailTrader = 2;//已成为分销商
                    rd.SuperRetailTraderLogin = T_SuperRetailTraderInfo.SuperRetailTraderLogin;
                    rd.SuperRetailTraderPass = T_SuperRetailTraderInfo.SuperRetailTraderPassData;
                }
                
            }
            if (!string.IsNullOrEmpty(customerCode))
            {
                rd.CustomerCode = customerCode;
            }
            return rd;
        }
        
    }
}
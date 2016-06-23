using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipGolden
{
    /// <summary>
    /// 新增分享记录表，目前为超级分销使用
    /// </summary>
    public class SetSharePersonLogAH : BaseActionHandler<SetSharePersonLogRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetSharePersonLogRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var t_LEventsSharePersonLogBLL = new T_LEventsSharePersonLogBLL(CurrentUserInfo);
            var SharePersonLogEntity = new T_LEventsSharePersonLogEntity();//通用分享实体实例化
            SharePersonLogEntity.SharePersonLogId = Guid.NewGuid();
            string objectType = string.Empty;
            if (!string.IsNullOrEmpty(para.ToolType))//para.ToolType.ToLower() == "material"
            {
                objectType = para.ToolType;
                switch (para.ToolType.ToLower())
                {
                    case "ctw":
                        para.ToolType = "CTW";
                        break;
                    case "coupon":
                        para.ToolType = "Coupon";
                        break;
                    case "setoffposter":
                        para.ToolType = "SetoffPoster";
                        break;
                    case "goods":
                        para.ToolType = "Goods";
                        break;
                    case "material":
                        para.ToolType="SuperRetailTraderFToT";
                        break;
                    default:
                        para.ToolType = "SuperRetailTraderFToT";
                        break;
                }
            }
            SharePersonLogEntity.BusTypeCode = para.ToolType;
            SharePersonLogEntity.ObjectId = para.ObjectID;
            if (string.IsNullOrEmpty(para.ShareVipType))
            {
                para.ShareVipType = "4";
            }
            SharePersonLogEntity.ShareVipType = Convert.ToInt32(para.ShareVipType);
            SharePersonLogEntity.ShareVipID = CurrentUserInfo.CurrentUser.User_Id;
            var vipbll = new VipBLL(CurrentUserInfo);
            var vipInfo = vipbll.QueryByEntity(new VipEntity() { VIPID = CurrentUserInfo.CurrentUser.User_Id },null).FirstOrDefault();
            if (vipInfo != null && !string.IsNullOrEmpty(vipInfo.VIPID))
            {
                SharePersonLogEntity.ShareOpenID = vipInfo.WeiXinUserId;
            }
            SharePersonLogEntity.BeShareVipID = "1";
            SharePersonLogEntity.CreateTime = System.DateTime.Now;
            SharePersonLogEntity.CreateBy = CurrentUserInfo.CurrentUser.User_Id;
            SharePersonLogEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
            SharePersonLogEntity.IsDelete = 0;
            t_LEventsSharePersonLogBLL.Create(SharePersonLogEntity);
            var setoffToolUserViewBLL = new SetoffToolUserViewBLL(CurrentUserInfo);
            var setOffToolInfo = new SetoffToolsBLL(CurrentUserInfo).QueryByEntity(new SetoffToolsEntity() { SetoffToolID = new Guid(para.SetOffToolID) }, null).FirstOrDefault();
            var UserViewData = setoffToolUserViewBLL.QueryByEntity(new SetoffToolUserViewEntity() { ObjectId = para.ObjectID, UserID = CurrentUserInfo.CurrentUser.User_Id }, null);
            if (!string.IsNullOrEmpty(para.ObjectID))
            {
                UserViewData = setoffToolUserViewBLL.QueryByEntity(new SetoffToolUserViewEntity() { ObjectId = para.ObjectID, UserID = CurrentUserInfo.CurrentUser.User_Id, SetoffToolID = new Guid(para.ObjectID) }, null);
            }
            //如果不存在已读数据则在推送之前 进行已读数据入库
            if (UserViewData.Length == 0 && !string.IsNullOrEmpty(para.ObjectID) && setOffToolInfo != null)
            {
                var SetoffToolUserView = new SetoffToolUserViewEntity();
                SetoffToolUserView.SetoffToolUserViewID = Guid.NewGuid();
                SetoffToolUserView.SetoffEventID = setOffToolInfo.SetoffEventID;
                SetoffToolUserView.ObjectId = para.ObjectID;
                SetoffToolUserView.SetoffToolID = new Guid(para.SetOffToolID);
                SetoffToolUserView.ToolType = objectType;
                SetoffToolUserView.NoticePlatformType = 3;
                SetoffToolUserView.UserID = CurrentUserInfo.CurrentUser.User_Id;
                SetoffToolUserView.IsOpen = 1;
                SetoffToolUserView.OpenTime = System.DateTime.Now;
                SetoffToolUserView.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                SetoffToolUserView.CreateTime = System.DateTime.Now;
                SetoffToolUserView.CreateBy = CurrentUserInfo.CurrentUser.User_Id;
                SetoffToolUserView.LastUpdateTime = System.DateTime.Now;
                SetoffToolUserView.LastUpdateBy = CurrentUserInfo.CurrentUser.User_Id;
                SetoffToolUserView.IsDelete = 0;
                setoffToolUserViewBLL.Create(SetoffToolUserView);
            }
            return rd;
        }
    }
}
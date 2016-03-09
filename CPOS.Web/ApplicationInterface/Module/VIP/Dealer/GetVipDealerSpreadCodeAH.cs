using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Dealer.Request;
using JIT.CPOS.DTO.Module.VIP.Dealer.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Dealer
{
    /// <summary>
    /// 获取经销商推广二维码接口
    /// </summary>
    public class GetVipDealerSpreadCodeAH : BaseActionHandler<GetVipDealerSpreadCodeRP, GetVipDealerSpreadCodeRD>
    {
        protected override GetVipDealerSpreadCodeRD ProcessRequest(DTO.Base.APIRequest<GetVipDealerSpreadCodeRP> pRequest)
        {
            var rd = new GetVipDealerSpreadCodeRD();
            var VipBLL = new VipBLL(CurrentUserInfo);
            var WQRCodeManagerBLL = new WQRCodeManagerBLL(this.CurrentUserInfo);
            var WQRCodeTypeBLL = new WQRCodeTypeBLL(this.CurrentUserInfo);
            try
            {
                var UserID = pRequest.UserID;
                string imageUrl = string.Empty;
                //分享处理
                if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ShareUserId))
                {
                    UserID = pRequest.Parameters.ShareUserId;
                }
                //微信 公共平台
                var wapentity = new WApplicationInterfaceBLL(this.CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity
                {

                    CustomerId = this.CurrentUserInfo.ClientID,
                    IsDelete = 0

                }, null).FirstOrDefault();

                var VipDealerType = WQRCodeTypeBLL.QueryByEntity(new WQRCodeTypeEntity() { TypeCode = "VipDealerCode" }, null);

                var WQRCodeManagerList = WQRCodeManagerBLL.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = UserID }, null).ToList();
                if (WQRCodeManagerList.Count > 0)
                {
                    imageUrl = WQRCodeManagerList.FirstOrDefault().ImageUrl;
                }
                else
                {
                    //获取当前二维码 最大值
                    var MaxWQRCod = new WQRCodeManagerBLL(this.CurrentUserInfo).GetMaxWQRCod() + 1;
                    if (wapentity == null)
                        throw new APIException("无设置微信公众平台!");

                    #region 生成二维码
                    JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                    imageUrl = commonServer.GetQrcodeUrl(wapentity.AppID.ToString().Trim()
                                                              , wapentity.AppSecret.Trim()
                                                              , "1", MaxWQRCod
                                                              , this.CurrentUserInfo);

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        throw new APIException("二维码生成失败!");
                    }
                    else
                    {
                        string host = ConfigurationManager.AppSettings["original_url"];
                        CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                        imageUrl = downloadServer.DownloadFile(imageUrl, host);
                        if (!string.IsNullOrEmpty(UserID) && VipDealerType != null && VipDealerType.Length > 0)
                        {

                            var WQRQrCode = new WQRCodeManagerEntity();
                            WQRQrCode.QRCode = MaxWQRCod.ToString();
                            WQRQrCode.QRCodeTypeId = VipDealerType[0].QRCodeTypeId;
                            WQRQrCode.IsUse = 1;
                            WQRQrCode.CustomerId = this.CurrentUserInfo.ClientID;
                            WQRQrCode.ImageUrl = imageUrl;
                            WQRQrCode.ApplicationId = wapentity.ApplicationId;//微信公众号的编号
                            //objectId 为会员ID
                            WQRQrCode.ObjectId = UserID;
                            WQRCodeManagerBLL.Create(WQRQrCode);
                        }


                    }
                    #endregion

                }

                //二维码地址赋值
                rd.imageUrl = imageUrl;
                //会员名称，头像地址
                var VipData = VipBLL.GetByID(UserID);
                if (VipData != null)
                {
                    rd.VipName = VipData.VipName;
                    rd.HeadImgUrl = VipData.HeadImgUrl;
                }
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }

            return rd;
        }
    }
}
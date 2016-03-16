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
                //
                string imageUrl = string.Empty;
                string QRCodeId = string.Empty;
                string VipName = string.Empty;
                string HeadImgUrl = string.Empty;
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

                var VipDealerType = WQRCodeTypeBLL.QueryByEntity(new WQRCodeTypeEntity() { TypeCode = "VipDealerCode" }, null).FirstOrDefault();
                if (VipDealerType == null)
                    throw new APIException("静态二位经销商类型无此类型！");

                if (!string.IsNullOrWhiteSpace(pRequest.Parameters.QRCodeId))
                {
                    #region 分享出去的二维码
                    var WQRCodeManagerData = WQRCodeManagerBLL.GetByID(pRequest.Parameters.QRCodeId);
                    if (WQRCodeManagerData == null)
                        throw new APIException("此二维码不存在！");

                    var VipData = VipBLL.GetByID(WQRCodeManagerData.ObjectId);
                    if (VipData == null)
                        throw new APIException("此二维码会员不存在！");

                    //二维码图片地址，ID赋值,会员名称，头像地址赋值
                    imageUrl = WQRCodeManagerData.ImageUrl;
                    QRCodeId = WQRCodeManagerData.QRCodeId.ToString();
                    VipName = VipData.VipName;
                    HeadImgUrl = VipData.HeadImgUrl;
                    #endregion
                }
                else
                {
                    #region 获取去自己静态推广二维码
                    var WQRCodeManagerData = WQRCodeManagerBLL.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = UserID, QRCodeTypeId = VipDealerType.QRCodeTypeId }, null).FirstOrDefault();
                    if (WQRCodeManagerData!=null)
                    {
                        //二维码图片地址，ID赋值
                        imageUrl = WQRCodeManagerData.ImageUrl;
                        QRCodeId = WQRCodeManagerData.QRCodeId.ToString();
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
                            //二维码赋值
                            imageUrl = downloadServer.DownloadFile(imageUrl, host);
                            if (!string.IsNullOrEmpty(UserID))
                            {

                                var WQRQrCode = new WQRCodeManagerEntity();
                                WQRQrCode.QRCodeId = Guid.NewGuid();
                                WQRQrCode.QRCode = MaxWQRCod.ToString();
                                WQRQrCode.QRCodeTypeId = VipDealerType.QRCodeTypeId;
                                WQRQrCode.IsUse = 1;
                                WQRQrCode.CustomerId = this.CurrentUserInfo.ClientID;
                                WQRQrCode.ImageUrl = imageUrl;
                                WQRQrCode.ApplicationId = wapentity.ApplicationId;//微信公众号的编号
                                //objectId 为会员ID
                                WQRQrCode.ObjectId = UserID;
                                WQRCodeManagerBLL.Create(WQRQrCode);

                                //二维码ID赋值
                                QRCodeId = WQRQrCode.QRCodeId.ToString(); 
                            }


                        }
                        #endregion

                    }
                    //会员名称，头像地址
                    var VipData = VipBLL.GetByID(UserID);
                    if (VipData == null)
                        throw new APIException("会员不存在！");

                        VipName = VipData.VipName;
                        HeadImgUrl = VipData.HeadImgUrl;
                    #endregion
                }
                //响应参数赋值
                rd.imageUrl = imageUrl;
                rd.VipName = VipName;
                rd.HeadImgUrl = HeadImgUrl;
                rd.QRCodeId = QRCodeId;
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }

            return rd;
        }
    }
}
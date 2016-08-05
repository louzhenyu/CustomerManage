using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using JIT.CPOS.BLL;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.Market.CreateQRCode.Request;
using JIT.CPOS.DTO.Module.Market.CreateQRCode.Response;
using ThoughtWorks.QRCode.Codec;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Market.CreateQRCode
{
    public class DownloadQRCodeAH : BaseActionHandler<DownloadQRCodeRP, DownloadQRCodeRD>
    {
        protected override DownloadQRCodeRD ProcessRequest(DTO.Base.APIRequest<DownloadQRCodeRP> pRequest)
        {
            string content = string.Empty;
            string customerId = string.Empty;
            var RD = new DownloadQRCodeRD();

            try
            {
                #region 解析传入参数
                //解析请求字符串
                var RP = pRequest.Parameters;

                //判断客户ID是否传递

                customerId = CurrentUserInfo.CurrentUser.customer_id;
  
             
                #endregion
                var imageUrl = string.Empty;
                Random ro = new Random();
                var iUp = 100000000;
                var iDown = 50000000;

                if (string.IsNullOrEmpty(RP.VipDCode.ToString()) || RP.VipDCode == 0)
                {
                    throw new APIException("VipDCode参数不能为空");
                }
                var rpVipDCode = 0;                 //临时二维码
                var iResult = ro.Next(iDown, iUp);  //随机数

                if (RP.VipDCode == 9)    //永久二维码
                {
                    var userQRCodeBll = new WQRCodeManagerBLL(CurrentUserInfo);
                    var marketEventBll = new MarketEventBLL(CurrentUserInfo);

                    var marketEventEntity = marketEventBll.QueryByEntity(new MarketEventEntity() { EventCode = "CA00002433",CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    var userQRCode = userQRCodeBll.QueryByEntity(new WQRCodeManagerEntity() { ObjectId = marketEventEntity.MarketEventID, CustomerId = CurrentUserInfo.ClientID }, null);  //

                    if (userQRCode != null && userQRCode.Length > 0)
                    {
                        RD.imageUrl = userQRCode[0].ImageUrl;
                        return RD;
                    }

                    //获取当前二维码 最大值
                    iResult = new WQRCodeManagerBLL(CurrentUserInfo).GetMaxWQRCod() + 1;
                    rpVipDCode = 1;                 //永久

                    #region 获取微信帐号
                    //门店关联的公众号
                    var tueBll = new TUnitExpandBLL(CurrentUserInfo);
                    var tueEntity = new TUnitExpandEntity();
                    if (!string.IsNullOrEmpty(CurrentUserInfo.CurrentUserRole.UnitId))
                    {
                        tueEntity = tueBll.QueryByEntity(new TUnitExpandEntity() { UnitId = CurrentUserInfo.CurrentUserRole.UnitId }, null).FirstOrDefault();
                    }

                    var server = new WApplicationInterfaceBLL(CurrentUserInfo);
                    var wxObj = new WApplicationInterfaceEntity();
                    if (tueEntity != null && !string.IsNullOrEmpty(tueEntity.Field1))
                    {
                        wxObj = server.QueryByEntity(new WApplicationInterfaceEntity { AppID = tueEntity.Field1, CustomerId = customerId }, null).FirstOrDefault();
                    }
                    else
                    {
                        wxObj = server.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = customerId }, null).FirstOrDefault();
                    }

                    if (wxObj == null)
                    {
                        throw new APIException("没有对应公众号");
                    }
                    else
                    {
                        var commonServer = new CommonBLL();

                        imageUrl = commonServer.GetQrcodeUrl(wxObj.AppID
                            , wxObj.AppSecret
                            , rpVipDCode.ToString("")//二维码类型  0： 临时二维码  1：永久二维码
                            , iResult, CurrentUserInfo);//iResult作为场景值ID，临时二维码时为32位整型，永久二维码时只支持1--100000
                        if (imageUrl != null && !imageUrl.Equals(""))
                        {
                            CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                            string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];
                            imageUrl = downloadServer.DownloadFile(imageUrl, downloadImageUrl);
                            //如果名称不为空，就把图片放在一定的背景下面
                            //if (!string.IsNullOrEmpty(RP.Parameters.RetailTraderName))
                            //{
                            //    string apiDomain = ConfigurationManager.AppSettings["website_url"];

                            //    imageUrl = CombinImage(apiDomain + @"/HeadImage/qrcodeBack.jpg", imageUrl, RP.Parameters.RetailTraderName + "合作二维码");
                            //}
                        }
                    }

                    #endregion

                    if (!string.IsNullOrEmpty(imageUrl))    //永久二维码
                    {
                        #region 创建店员永久二维码匹配表
                        var userQrTypeBll = new WQRCodeTypeBLL(CurrentUserInfo);
                        var userQrType = userQrTypeBll.QueryByEntity(new WQRCodeTypeEntity() { TypeName = "签到" }, null);//"UserQrCode"
                        if (userQrType != null && userQrType.Length > 0)
                        {
                            var userQrCodeBll = new WQRCodeManagerBLL(CurrentUserInfo);
                            var userQrCode = new WQRCodeManagerEntity();
                            userQrCode.QRCode = iResult.ToString();//记录传过去的二维码场景值
                            userQrCode.QRCodeTypeId = userQrType[0].QRCodeTypeId;
                            userQrCode.IsUse = 1;
                            userQrCode.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                            userQrCode.ImageUrl = imageUrl;
                            userQrCode.ApplicationId = wxObj.ApplicationId;
                            //objectId 为店员ID
                            userQrCode.ObjectId = marketEventEntity.MarketEventID;
                            userQrCodeBll.Create(userQrCode);
                        }
                        #endregion
                    }
                }

                RD.imageUrl = imageUrl;
            }
            catch
            {
                throw new APIException("生成二维码错误");
            }
            return RD;
        }
    }
}
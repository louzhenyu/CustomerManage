using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;
using JIT.CPOS.DTO.Module.Item.QRCode.Request;
using JIT.CPOS.DTO.Module.Item.QRCode.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using System.Configuration;
using System.IO;
using JIT.Utility.Log;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Web.Module.WEvents.Handler;
using JIT.Utility.DataAccess.Query;
using System.Globalization;
using System.Drawing;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Item.QRCode
{
    public class GetQRCodeAH : BaseActionHandler<GetQRCodeRP, GetQRCodeRD>
    {
        protected override GetQRCodeRD ProcessRequest(DTO.Base.APIRequest<GetQRCodeRP> pRequest)
        {
            GetQRCodeRD rd = new GetQRCodeRD();
            string weixinDomain = ConfigurationManager.AppSettings["original_url"];
            string sourcePath = HttpContext.Current.Server.MapPath("/QRCodeImage/qrcode.jpg");
            string targetPath = HttpContext.Current.Server.MapPath("/QRCodeImage/");
            string currentDomain = ConfigurationManager.AppSettings["original_url"];
            string itemId = pRequest.Parameters.ItemId;//商品ID
            string itemName = pRequest.Parameters.ItemName;//商品名
            string imageURL;
            // ObjectImagesBLL objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            ////查找是否已经生成了二维码
            //ObjectImagesEntity[] objectImagesEntityArray = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = itemId, Description = "自动生成的产品二维码" }, null);

            //if (objectImagesEntityArray.Length == 0)
            //{

                imageURL = Utils.GenerateQRCode(weixinDomain + "/HtmlApps/Auth.html?pageName=GoodsDetail&rootPage=true&customerId=" + CurrentUserInfo.ClientID + "&goodsId=" + itemId, currentDomain, sourcePath, targetPath);
                //objectImagesBLL.Create(new ObjectImagesEntity()
                //{
                //    ImageId = Utils.NewGuid(),
                //    CustomerId = CurrentUserInfo.ClientID,
                //    ImageURL = imageURL,
                //    ObjectId = itemId,
                //    Title = itemName
                //    ,
                //    Description = "自动生成的产品二维码"
                //});
            //}
            //else
            //{
            //    imageURL = objectImagesEntityArray[0].ImageURL;
            //}
            Loggers.Debug(new DebugLogInfo() { Message = "二维码已生成，imageURL:" + imageURL });


            string imagePath = targetPath + imageURL.Substring(imageURL.LastIndexOf("/"));
            Loggers.Debug(new DebugLogInfo() { Message = "二维码路径，imagePath:" + imageURL });

            rd.QRCodeURL =  imageURL;
           
           return rd;

        }

    }
}
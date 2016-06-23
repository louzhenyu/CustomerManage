using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using System.Data;
using System;
using System.Configuration;
using JIT.CPOS.Common;
using JIT.Utility.Log;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.App
{
    public class CreateQRCodeForSalesAH: BaseActionHandler<CreateQRCodeForSalesRP, CreateQRCodeForSalesRD>
    {
        protected override CreateQRCodeForSalesRD ProcessRequest(APIRequest<CreateQRCodeForSalesRP> pRequest)
        {
            CreateQRCodeForSalesRP para = pRequest.Parameters;
            CreateQRCodeForSalesRD rd = new CreateQRCodeForSalesRD();
            string weixinDomain = ConfigurationManager.AppSettings["original_url"];
            string sourcePath = HttpContext.Current.Server.MapPath("/QRCodeImage/qrcode.jpg");
            string targetPath = HttpContext.Current.Server.MapPath("/QRCodeImage/");
            string currentDomain = ConfigurationManager.AppSettings["original_url"];
            string strHost = ConfigurationManager.AppSettings["website_url"].Trim();
            string strSkuId = para.SkuId;
            string strSkuPrice = para.SkuPrice;
            int strSkuQty = para.SkuQty;
            string SuperRetailTraderId = para.SuperRetailTraderId;
            string imageURL;

            string goUrl = weixinDomain + "/HtmlApps/html/public/shop/goods_order.html?channelId=11&customerId=" + CurrentUserInfo.ClientID + "&SuperRetailTraderId=" + SuperRetailTraderId + "&SkuId=" + strSkuId + "&SkuPrice=" + strSkuPrice + "&SkuQty=" + strSkuQty;
            string strOAuthUrl = strHost + "/WXOAuth/AuthUniversal.aspx?scope=snsapi_userinfo&customerId=" + CurrentUserInfo.CurrentUser.customer_id;//拼OAuth认证
            strOAuthUrl += "&goUrl=" + System.Web.HttpUtility.UrlEncode(goUrl);

            imageURL = Utils.GenerateQRCode(strOAuthUrl, currentDomain, sourcePath, targetPath);
 
            Loggers.Debug(new DebugLogInfo() { Message = "超级分销商sku二维码已生成，imageURL:" + imageURL });


            string imagePath = targetPath + imageURL.Substring(imageURL.LastIndexOf("/"));
            Loggers.Debug(new DebugLogInfo() { Message = "超级分销商sku二维码路径，imagePath:" + imageURL });

            rd.QRCodeUrl = imageURL;

            return rd;
        }
    }
}
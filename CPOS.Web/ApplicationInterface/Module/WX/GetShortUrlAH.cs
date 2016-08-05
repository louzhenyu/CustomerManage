using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.ShortUrl.Request;
using JIT.CPOS.DTO.Module.WeiXin.ShortUrl.Response;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WX {
	public class GetShortUrlAH : BaseActionHandler<ShortUrlRP, ShortUrlRD> {
		protected override ShortUrlRD ProcessRequest(APIRequest<ShortUrlRP> pRequest) {
			ShortUrlRD rd = new ShortUrlRD();
			var para = pRequest.Parameters;

			var list = new WApplicationInterfaceBLL(CurrentUserInfo).QueryByEntity(new WApplicationInterfaceEntity { CustomerId = CurrentUserInfo.ClientID, IsDelete = 0 }, null).ToList();
			if (list == null) {
				throw new Exception("该商户没有绑定微信公众号，无法生成商品二维码");
			}
			var wapentity = list.FirstOrDefault();//获取公众号信息
			JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
			var shorturl = commonServer.GetShorturl(wapentity.AppID.ToString().Trim()
													, wapentity.AppSecret.Trim()
													, para.Url
													, CurrentUserInfo);

			rd.ShortUrl = shorturl;
			return rd;
		}
	}
}
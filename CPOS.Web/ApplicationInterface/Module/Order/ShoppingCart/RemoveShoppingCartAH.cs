using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.ShoppingCart
{
    public class RemoveShoppingCartAH : BaseActionHandler<RemoveShoppingCartRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<RemoveShoppingCartRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var service = new ShoppingCartBLL(CurrentUserInfo);

            if (!string.IsNullOrWhiteSpace(para.ShoppingCardId))
            {
                var Data = service.GetByID(para.ShoppingCardId);
                if (Data != null)
                {
                    try
                    {
                        service.Delete(Data);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }

            }
            return rd;
        }
    }
}
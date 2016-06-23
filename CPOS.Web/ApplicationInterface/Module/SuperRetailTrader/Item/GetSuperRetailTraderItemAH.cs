using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.SuperRetailTrader.Item
{
    public class GetSuperRetailTraderItemAH : BaseActionHandler<GetSuperRetailTraderItemRP,GetSuperRetailTraderItemRD>
    {
        protected override GetSuperRetailTraderItemRD ProcessRequest(APIRequest<GetSuperRetailTraderItemRP> pRequest)
        {
            GetSuperRetailTraderItemRP rp = pRequest.Parameters;
            GetSuperRetailTraderItemRD rd = new GetSuperRetailTraderItemRD();

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);
            var superRetailTraderConfigBll = new T_SuperRetailTraderConfigBLL(CurrentUserInfo);
            //获取商品列表
            PagedQueryResult<T_SuperRetailTraderItemMappingEntity> ItemList = superRetailTraderItemMappingBll.GetSuperRetailTraderItemList(rp.ItemName,rp.ItemCategoryId,10,rp.PageSize,rp.PageIndex + 1);
            //获取分销商配置信息
            T_SuperRetailTraderConfigEntity superRetailTraderConfigEntity = superRetailTraderConfigBll.QueryByEntity(new T_SuperRetailTraderConfigEntity(){CustomerId = CurrentUserInfo.ClientID},null).FirstOrDefault();

            if (superRetailTraderConfigEntity != null)
            {
                decimal temp;
                rd.ItemList = ItemList.Entities.Select(n => new APISuperRetailTraderItemInfo()
                {
                    ItemId = n.ItemId,
                    ItemName = n.ItemName,
                    ImageUrl = n.ImageUrl,
                    SoldQty = Convert.ToInt32(n.SalesQty),
                    DistributerPrice = temp = Math.Round(Convert.ToDecimal(n.DistributerCostPrice / Convert.ToDecimal(superRetailTraderConfigEntity.Cost / 100)), 2, MidpointRounding.AwayFromZero),  //分销价格
                    SkuCommission = Math.Round(Convert.ToDecimal(temp * Convert.ToDecimal(superRetailTraderConfigEntity.SkuCommission / 100)), 2, MidpointRounding.AwayFromZero) //商品佣金
                }).ToList();
            }
            else
            {
                throw new APIException("商户未设置分销体系") { ErrorCode = 210 };
            }
            rd.TotalCount = ItemList.RowCount;
            rd.TotalPageCount = ItemList.PageCount;

            return rd;
        }
    }
}
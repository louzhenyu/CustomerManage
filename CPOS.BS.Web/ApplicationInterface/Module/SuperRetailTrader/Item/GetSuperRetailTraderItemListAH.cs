using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.Item
{
    public class GetSuperRetailTraderItemListAH : BaseActionHandler<GetSuperRetailTraderItemListRP, GetSuperRetailTraderItemListRD>
    {
        protected override GetSuperRetailTraderItemListRD ProcessRequest(APIRequest<GetSuperRetailTraderItemListRP> pRequest)
        {
            GetSuperRetailTraderItemListRP rp = pRequest.Parameters;
            GetSuperRetailTraderItemListRD rd = new GetSuperRetailTraderItemListRD();
            rd.SuperRetailTraderItemList = new List<SuperRetailTraderItemInfo>();

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);
            var superRetailTraderConfigBll = new T_SuperRetailTraderConfigBLL(CurrentUserInfo);

            OrderBy[] orderBy = new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } };
            PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> superRetailTraderSkuList = new PagedQueryResult<T_SuperRetailTraderSkuMappingEntity>();

            //获取分销商Sku列表
            superRetailTraderSkuList = superRetailTraderSkuMappingBll.GetSuperRetailTraderSkuList("",rp.ItemName,rp.Status,rp.PageSize,rp.PageIndex);
            //获取分销商怕配置信息
            T_SuperRetailTraderConfigEntity superRetailTraderConfigEntity = superRetailTraderConfigBll.QueryByEntity(new T_SuperRetailTraderConfigEntity(){CustomerId = CurrentUserInfo.ClientID},null).FirstOrDefault();


            decimal temp; //分销价格
            decimal Cost = 0; //成本价
            decimal CustomerProfit = 0; //商家分润
            if(superRetailTraderConfigEntity != null && superRetailTraderConfigEntity.Cost != 0 && superRetailTraderConfigEntity.Cost != null  )
            {
                Cost = Convert.ToDecimal(superRetailTraderConfigEntity.Cost); //成本价
            }  
            if(superRetailTraderConfigEntity != null && superRetailTraderConfigEntity.CustomerProfit != null && superRetailTraderConfigEntity.CustomerProfit != 0)
            {
                CustomerProfit = Convert.ToDecimal(superRetailTraderConfigEntity.CustomerProfit); //商家分润
            }
            List<SuperRetailTraderItemInfo> SkuList = superRetailTraderSkuList.Entities.Select(n => new SuperRetailTraderItemInfo()
            {
                ItemId = n.ItemId,
                ItemName = n.ItemName,
                SkuId = n.SkuId,
                PropName = superRetailTraderItemMappingBll.GetPropName(n.PropName1, n.PropName2),
                SalesPrice = n.SalesPrice,
                DistributerStock = Convert.ToInt32(n.DistributerStock) - Convert.ToInt32(n.SalesQty),
                DistributerCostPrice = Convert.ToDecimal(n.DistributerCostPrice),
                DistributePirce = temp = Cost == 0 ? 0 : Math.Round(Convert.ToDecimal(n.DistributerCostPrice / Convert.ToDecimal( Cost / 100)), 2, MidpointRounding.AwayFromZero),  //分销价格 成本价 / 成本占比
                CustomerProgit = CustomerProfit ==0 ? 0 : Math.Round(temp * Convert.ToDecimal(CustomerProfit / 100), 2, MidpointRounding.AwayFromZero), //商家分润  分销价 * 商家分润占比
                Status = Convert.ToInt32(n.Status)
            }).ToList();
            rd.SuperRetailTraderItemList.AddRange(SkuList);

            rd.TotalCount = superRetailTraderSkuList.RowCount;
            rd.TotalPageCount = superRetailTraderSkuList.PageCount;
            
            
            return rd;
        }

    }
}
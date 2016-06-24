using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.Item
{
    public class SetSuperRetailTraderItemAH : BaseActionHandler<SetSuperRetailTraderItemRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetSuperRetailTraderItemRP> pRequest)
        {
            SetSuperRetailTraderItemRP rp = pRequest.Parameters;

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);

            List<ItemIdInfo> ItemIdList = rp.ItemIdList.GroupBy(n => new{n.ItemId}).Select(n => new ItemIdInfo(){
                ItemId = n.Key.ItemId
            }).ToList();

            List<ItemIdInfo> SkuIdList = rp.ItemIdList.Select(n => new ItemIdInfo() { ItemId = n.ItemId, SkuId = n.SkuId }).ToList();
            //初始化分销商商品信息
            foreach(var item in ItemIdList)
            {
                var itemEntity = superRetailTraderItemMappingBll.QueryByEntity(new T_SuperRetailTraderItemMappingEntity { ItemId = item.ItemId }, null).FirstOrDefault();
                if (itemEntity == null)
                {
                    T_SuperRetailTraderItemMappingEntity superRetailTraderItemMappingEntity = new T_SuperRetailTraderItemMappingEntity()
                    {
                        SuperRetailTraderItemMappingId = Guid.NewGuid(),
                        ItemId = item.ItemId,
                        DistributerStock = 0,
                        SalesQty = 0,
                        DistributerCostPrice = 0,
                        DistributerPrice = 0,
                        Status = 90,
                        OnShelfDatetime = DateTime.Now,
                        CustomerID = CurrentUserInfo.ClientID
                    };
                    superRetailTraderItemMappingBll.Create(superRetailTraderItemMappingEntity);
                    itemEntity = superRetailTraderItemMappingEntity;
                }
                    //初始化分销商Sku信息
                    foreach (var sku in SkuIdList.Where(n => n.ItemId == item.ItemId))
                    {
                        var SkuEntity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity { SkuId = sku.SkuId }, null);
                        if (SkuEntity.Count() == 0)
                        {
                            T_SuperRetailTraderSkuMappingEntity superRetailTraderSkuMappingEntity = new T_SuperRetailTraderSkuMappingEntity()
                            {
                                Id = Guid.NewGuid(),
                                SuperRetailTraderItemMappingId = itemEntity.SuperRetailTraderItemMappingId,
                                ItemId = item.ItemId,
                                SkuId = sku.SkuId,
                                DistributerStock = 0,
                                SalesQty = 0,
                                DistributerCostPrice = 0,
                                Status = 90,
                                OnShelfDatetime = DateTime.Now,
                                CustomerID = CurrentUserInfo.ClientID
                            };
                            superRetailTraderSkuMappingBll.Create(superRetailTraderSkuMappingEntity);
                        }
                    }
    
            } 
            return new EmptyResponseData();
        }
    }
}
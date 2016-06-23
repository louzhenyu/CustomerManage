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
    public class SetSuperRetailTraderItemInfoAH : BaseActionHandler<SetSuperRetailTraderItemInfoRP,EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetSuperRetailTraderItemInfoRP> pRequest)
        {
            SetSuperRetailTraderItemInfoRP rp = pRequest.Parameters;
            EmptyResponseData rd = new EmptyResponseData();

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);

            List<SuperRetailTraderItem> ItemIdList = rp.ItemList.GroupBy(n => new { n.ItemId }).Select(n => new SuperRetailTraderItem()
            {
                ItemId = n.Key.ItemId,
                DistributerStock = n.Sum(t => t.DistributerStock),
                DistributerCostPrice = n.Min(t => t.DistributerCostPrice)
            }).ToList();
            List<SuperRetailTraderItem> SkuIdList = rp.ItemList.Select(n => n).ToList();
            //修改商品成本价和库存
            foreach (var item in ItemIdList)
            {
                //获取商品信息
                var entity = superRetailTraderItemMappingBll.QueryByEntity(new T_SuperRetailTraderItemMappingEntity() { ItemId = item.ItemId, CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.DistributerStock == 0 && entity.DistributerCostPrice == 0) //如果是初始化数据，直接修改
                    {
                        entity.DistributerStock = item.DistributerStock;
                        entity.DistributerCostPrice = item.DistributerCostPrice;
                        superRetailTraderItemMappingBll.Update(entity);
                    }
                    else //如果是非初始化数据，创建新的，删除旧的
                    {
                        T_SuperRetailTraderItemMappingEntity newEntity = new T_SuperRetailTraderItemMappingEntity()
                        {
                            SuperRetailTraderItemMappingId = Guid.NewGuid(),
                            ItemId = item.ItemId,
                            DistributerStock = item.DistributerStock + entity.SalesQty,
                            SalesQty = entity.SalesQty,
                            DistributerCostPrice = item.DistributerCostPrice,
                            DistributerPrice = entity.DistributerPrice,
                            Status = entity.Status,
                            OnShelfDatetime = entity.OnShelfDatetime,
                            OffShelfDatetime = entity.OffShelfDatetime,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        superRetailTraderItemMappingBll.Delete(entity);
                        superRetailTraderItemMappingBll.Create(newEntity);
                    }
                }
            }
            //修改Sku成本价和库存
            foreach (var sku in SkuIdList)
            {
                var entity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { SkuId = sku.SkuId, ItemId = sku.ItemId, CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                if (entity != null)
                {
                    if (entity.DistributerStock == 0 && entity.DistributerCostPrice == 0) //如果是初始化数据，直接修改
                    {
                        entity.DistributerStock = sku.DistributerStock;
                        entity.DistributerCostPrice = sku.DistributerCostPrice;
                        superRetailTraderSkuMappingBll.Update(entity);
                    }
                    else //如果是非初始化数据，创建新的，删除旧的
                    {
                        T_SuperRetailTraderSkuMappingEntity newEntity = new T_SuperRetailTraderSkuMappingEntity()
                        {
                            SuperRetailTraderItemMappingId = entity.SuperRetailTraderItemMappingId,
                            ItemId = entity.ItemId,
                            SkuId = entity.SkuId,
                            DistributerStock = sku.DistributerStock + entity.SalesQty,
                            SalesQty = entity.SalesQty,
                            DistributerCostPrice = sku.DistributerCostPrice,
                            RefId = entity.SuperRetailTraderItemMappingId,
                            Status = entity.Status,
                            OnShelfDatetime = entity.OnShelfDatetime,
                            OffShelfDatetime = entity.OffShelfDatetime,
                            CustomerID = CurrentUserInfo.ClientID
                        };
                        superRetailTraderSkuMappingBll.Delete(entity);
                        superRetailTraderSkuMappingBll.Create(newEntity);
                    }
                }
            }

            return rd;
        }
    }
}
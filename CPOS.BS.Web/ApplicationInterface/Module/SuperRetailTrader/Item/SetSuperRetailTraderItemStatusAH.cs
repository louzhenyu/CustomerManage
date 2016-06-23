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
    public class SetSuperRetailTraderItemStatusAH : BaseActionHandler<SetSuperRetailTraderItemStatusRP,EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<SetSuperRetailTraderItemStatusRP> pRequest)
        {
            SetSuperRetailTraderItemStatusRP rp = pRequest.Parameters;
            EmptyResponseData rd = new EmptyResponseData();

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);

            List<SuperRetailTraderItem> ItemIdList = rp.ItemIdList.GroupBy(n => new { n.ItemId }).Select(n => new SuperRetailTraderItem()
            {
                ItemId = n.Key.ItemId,
                IsAllSelected = n.Min(t => t.IsAllSelected), // IsAllSelected = 0（不是移除,上架,下架该商品的所有Sku）
            }).ToList();

            List<SuperRetailTraderItem> SkuIdList = rp.ItemIdList.Select(n => new SuperRetailTraderItem() { ItemId = n.ItemId, SkuId = n.SkuId }).ToList();

            if (rp.Status == 10) //上架操作
            {
                //商品上架
                foreach(var item in ItemIdList)
                {
                    var entity = superRetailTraderItemMappingBll.QueryByEntity(new T_SuperRetailTraderItemMappingEntity() { ItemId = item.ItemId, CustomerID = CurrentUserInfo.ClientID ,Status = 90}, null).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Status = 10;
                        entity.OffShelfDatetime = DateTime.Now;
                        superRetailTraderItemMappingBll.Update(entity);
                    }
                }
                //Sku上架
                foreach (var sku in SkuIdList)
                {
                    var entity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { SkuId = sku.SkuId, ItemId = sku.ItemId, CustomerID = CurrentUserInfo.ClientID, Status = 90 }, null).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Status = 10;
                        entity.OffShelfDatetime = DateTime.Now;
                        superRetailTraderSkuMappingBll.Update(entity);
                    }
                }
            }

            if (rp.Status == 90) //下架操作
            {
                //Sku下架
                foreach (var sku in SkuIdList)
                {
                    var entity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { SkuId = sku.SkuId, ItemId = sku.ItemId, CustomerID = CurrentUserInfo.ClientID, Status = 10 }, null).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Status = 90;
                        entity.OffShelfDatetime = DateTime.Now;
                        superRetailTraderSkuMappingBll.Update(entity);
                    }
                }
                //商品下架
                foreach (var item in ItemIdList)
                {
                    var skuEntity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { ItemId = item.ItemId, CustomerID = CurrentUserInfo.ClientID, Status = 10 }, null);
                    var entity = superRetailTraderItemMappingBll.QueryByEntity(new T_SuperRetailTraderItemMappingEntity() { ItemId = item.ItemId, CustomerID = CurrentUserInfo.ClientID, Status = 10 }, null).FirstOrDefault();
                    if (entity != null && skuEntity.Count() == 0) //所有sku下架
                    {
                        entity.Status = 90;
                        entity.OffShelfDatetime = DateTime.Now;
                        superRetailTraderItemMappingBll.Update(entity);
                    }
            
                }
            }
            if (rp.Status == 0) //移除操作
            {
                //sku移除
                foreach (var sku in SkuIdList)
                {
                    var entity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { SkuId = sku.SkuId, ItemId = sku.ItemId, CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    if (entity != null)
                    {
                        superRetailTraderSkuMappingBll.Delete(entity);
                    }
                }
                //商品移除
                foreach (var item in ItemIdList)
                {
                    var skuEntity = superRetailTraderSkuMappingBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { ItemId = item.ItemId, CustomerID = CurrentUserInfo.ClientID}, null);
                    var entity = superRetailTraderItemMappingBll.QueryByEntity(new T_SuperRetailTraderItemMappingEntity() { ItemId = item.ItemId, CustomerID = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    if (entity != null && skuEntity.Count() == 0) //sku全部移除
                    {
                        superRetailTraderItemMappingBll.Delete(entity);
                    }
                    
                }
            }
            return rd;
        }
    }
}
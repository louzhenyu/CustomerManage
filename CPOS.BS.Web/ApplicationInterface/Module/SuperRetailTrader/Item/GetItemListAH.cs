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
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Request;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.Item
{
    public class GetItemListAH : BaseActionHandler<GetItemListRP,GetItemListRD>
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override GetItemListRD ProcessRequest(DTO.Base.APIRequest<GetItemListRP> pRequest)
        {
            GetItemListRP rp = pRequest.Parameters;
            GetItemListRD rd = new GetItemListRD();

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);

            //获取商品列表
            PagedQueryResult<T_SuperRetailTraderItemMappingEntity> superRetailTraderItemMappingList = superRetailTraderItemMappingBll.GetItemList(rp.ItemName, rp.ItemCategoryId,rp.PageSize,rp.PageIndex);
            //获取Sku列表
            PagedQueryResult<T_SuperRetailTraderSkuMappingEntity> superRetailTraderSkuMappingList = superRetailTraderSkuMappingBll.GetSkuList(rp.ItemName, rp.ItemCategoryId, rp.PageSize, rp.PageIndex);

            List<BaseItemInfo> ItemList = superRetailTraderItemMappingList.Entities.Select(t => new BaseItemInfo()
            {
                ItemId = t.ItemId,
                ItemName = t.ItemName,
                DisplayIndex = (t.DisplayIndex % rp.PageSize) == 0 ? rp.PageSize : t.DisplayIndex % rp.PageSize,
                ItemCode = t.ItemCode,
                SkuId = "",
                PropName = "",
                SalesPrice = 0,
                ParentId = "-99" //无父节点
            }).ToList();

            //根据ItemId匹配相关Sku和商品
            List<BaseItemInfo> SkuList = superRetailTraderSkuMappingList.Entities.Join(superRetailTraderItemMappingList.Entities.ToList(), n => n.ItemId, t => t.ItemId, (n, t) => new BaseItemInfo()
            {
                ItemId = t.ItemId,
                ItemName = t.ItemName,
                DisplayIndex = t.DisplayIndex,
                ItemCode = t.ItemCode,
                SkuId = n.SkuId,
                PropName = superRetailTraderItemMappingBll.GetPropName(n.PropName1, n.PropName2),
                SalesPrice = n.SalesPrice,
                ParentId = t.ItemId //父节点为ItemId
            }).ToList();

            rd.ItemList = new List<BaseItemInfo>();
            rd.ItemList.AddRange(ItemList);
            rd.ItemList.AddRange(SkuList);

            rd.TotalCount = superRetailTraderItemMappingList.RowCount;
            rd.TotalPageCount = superRetailTraderItemMappingList.PageCount;

            return rd;
        }


    }


}
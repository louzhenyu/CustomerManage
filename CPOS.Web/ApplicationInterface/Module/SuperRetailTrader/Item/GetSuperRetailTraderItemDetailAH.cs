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
    public class GetSuperRetailTraderItemDetailAH : BaseActionHandler<GetSuperRetailTraderItemDetailRP,GetSuperRetailTraderItemDetailRD>
    {
        protected override GetSuperRetailTraderItemDetailRD ProcessRequest(APIRequest<GetSuperRetailTraderItemDetailRP> pRequest)
        {
            GetSuperRetailTraderItemDetailRP rp = pRequest.Parameters;
            GetSuperRetailTraderItemDetailRD rd = new GetSuperRetailTraderItemDetailRD();
            OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(CurrentUserInfo);

            var superRetailTraderItemMappingBll = new T_SuperRetailTraderItemMappingBLL(CurrentUserInfo);
            var superRetailTraderSkuMappingBll = new T_SuperRetailTraderSkuMappingBLL(CurrentUserInfo);
            var superRetailTraderConfigBll = new T_SuperRetailTraderConfigBLL(CurrentUserInfo);
            var customerBasicSettingBll = new CustomerBasicSettingBLL(CurrentUserInfo);

            //获取商品详细信息
            var ItemDetail = superRetailTraderItemMappingBll.GetSuperRetailTraderItemDetail(rp.ItemId);
            //获取Sku详细信息
            var skuList = superRetailTraderSkuMappingBll.GetSuperRetailTraderSkuDetail(rp.ItemId,pRequest.CustomerID);
            //获取分销商配置信息
             T_SuperRetailTraderConfigEntity superRetailTraderConfigEntity = superRetailTraderConfigBll.QueryByEntity(new T_SuperRetailTraderConfigEntity(){CustomerId = CurrentUserInfo.ClientID},null).FirstOrDefault();

            #region 商品图片
            var dsImages = itemService.GetItemImageList(rp.ItemId);
            if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
            {
                rd.ImageList = DataTableToObject.ConvertToList<ImageInfo>(dsImages.Tables[0]);
            }
            #endregion

            rd.ItemId = ItemDetail.ItemId;
            rd.ItemName = ItemDetail.ItemName;
            rd.Prop1 = ItemDetail.Prop1Name;
            rd.Prop2 = ItemDetail.Prop2Name;
            rd.Price = ItemDetail.Price;
            rd.DistributerPrice = Math.Round(Convert.ToDecimal(ItemDetail.DistributerCostPrice / Convert.ToDecimal(superRetailTraderConfigEntity.Cost / 100)), 2, MidpointRounding.AwayFromZero); //分销价格 成本价 / 成本占比
            if (rd.Price == 0) //价格为零，折扣为10折
            {
                rd.Discount = 1;
            }
            else
            {
                rd.Discount = Math.Round(rd.DistributerPrice / rd.Price, 2, MidpointRounding.AwayFromZero); //折扣
            }
            rd.ItemIntroduce = ItemDetail.ItemIntroduce;

            rd.SkuList = skuList.Select(n => new APISkuInfo() { 
                SkuID = n.SkuId,
                PropName1 = n.PropName1,
                PropName2 = n.PropName2,
                DistributerPrice = Math.Round(Convert.ToDecimal(n.DistributerCostPrice / Convert.ToDecimal(superRetailTraderConfigEntity.Cost / 100)), 2, MidpointRounding.AwayFromZero),  //分销价格 成本价 / 成本占比
                DistributerStock = Convert.ToInt32(n.DistributerStock) - Convert.ToInt32(n.SalesPrice),
                SoldQty = Convert.ToInt32(n.SalesQty)
            }).ToList();

            rd.DeliveryDesc = customerBasicSettingBll.GetSettingValueByCode("DeliveryStrategy");

            return rd;
        }
    }
}
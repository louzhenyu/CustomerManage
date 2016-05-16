using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.ApplicationInterface.Events;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class GetBargainDetailsAH : BaseActionHandler<GetBargainDetailsRP, GetBargainDetailsRD>
    {
        protected override GetBargainDetailsRD ProcessRequest(DTO.Base.APIRequest<GetBargainDetailsRP> pRequest)
        {
            var rd = new GetBargainDetailsRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ItemBll = new T_ItemBLL(loggingSessionInfo);
            var EventItemMappingBll = new PanicbuyingKJEventItemMappingBLL(loggingSessionInfo);
            //砍价活动sku业务对象
            var PanicbuyingKJEventSkuMappingBll = new PanicbuyingKJEventSkuMappingBLL(loggingSessionInfo);
            var SkuBll = new T_SkuBLL(loggingSessionInfo);
            //活动
            var SkuMappingBLL = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
            //
            string ItemID = string.Empty;
            var SkuMappingList = new List<PanicbuyingKJEventSkuMappingEntity>();
            if (!string.IsNullOrWhiteSpace(para.EventItemMappingID))
            {
                var EventItemData = EventItemMappingBll.GetByID(para.EventItemMappingID);
                if (EventItemData != null)
                {
                    ItemID = EventItemData.ItemID;
                    //商品信息赋值
                    rd.SinglePurchaseQty = EventItemData.SinglePurchaseQty.Value;
                    rd.ItemID = EventItemData.ItemID;
                    rd.BargaingingInterval = EventItemData.BargaingingInterval.Value;
                }
                //砍价Sku集合
                SkuMappingList = PanicbuyingKJEventSkuMappingBll.QueryByEntity(new PanicbuyingKJEventSkuMappingEntity() { EventItemMappingID = para.EventItemMappingID }, null).ToList();
            }
            else
            {
                ItemID = para.ItemId;

            }

            var ItemData = ItemBll.GetByID(ItemID);
            if (ItemData == null)
                throw new APIException("未找到相关商品，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            //商品ID、名称赋值
            rd.ItemID = ItemID;
            rd.ItemName = ItemData.item_name;

            #region sku
            rd.SkuInfoList = new List<SkuInfos>();
            //var SkuList = SkuBll.QueryByEntity(new T_SkuEntity() { item_id = ItemID }, null).ToList();
            DataSet ds = SkuMappingBLL.GetItemSku("", ItemID, "");
            var SkuList = new List<Sku>();
            if (ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                SkuList = DataTableToObject.ConvertToList<Sku>(ds.Tables[0]);
            }
            foreach (var item in SkuList)
            {
                var SkuInfos = new SkuInfos();
                SkuInfos.SkuID = item.SkuID;
                SkuInfos.SkuName = item.SkuName;
                SkuInfos.Price = item.price;
                if (!string.IsNullOrWhiteSpace(para.EventItemMappingID))
                {
                    
                    var Result = SkuMappingList.FirstOrDefault(m => m.SkuID.Equals(item.SkuID));
                    if (Result != null)
                    {
                        SkuInfos.EventSkuInfo = new EventSkuInfo();
                        SkuInfos.EventSkuInfo.EventSKUMappingId = Result.EventSKUMappingId.ToString();
                        SkuInfos.EventSkuInfo.EventItemMappingID = Result.EventItemMappingID.ToString();
                        SkuInfos.EventSkuInfo.SkuID = Result.SkuID;
                        SkuInfos.EventSkuInfo.Qty = Result.Qty.Value;
                        SkuInfos.EventSkuInfo.Price = Result.Price.Value;
                        SkuInfos.EventSkuInfo.BasePrice = Result.BasePrice.Value;
                        SkuInfos.EventSkuInfo.BargainStartPrice = Result.BargainStartPrice.Value;
                        SkuInfos.EventSkuInfo.BargainEndPrice = Result.BargainEndPrice.Value;
                    }
                }
                //
                rd.SkuInfoList.Add(SkuInfos);
            }
            #endregion

            return rd;
        }
    }
}
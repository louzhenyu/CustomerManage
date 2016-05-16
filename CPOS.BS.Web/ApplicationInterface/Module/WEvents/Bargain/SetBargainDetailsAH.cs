using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents.Bargain
{
    public class SetBargainDetailsAH : BaseActionHandler<SetBargainDetailsRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetBargainDetailsRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var ItemMappingBll = new PanicbuyingKJEventItemMappingBLL(loggingSessionInfo);
            var SkuMappingBll = new PanicbuyingKJEventSkuMappingBLL(loggingSessionInfo);
            var PanicbuyingEventBLL = new PanicbuyingEventBLL(loggingSessionInfo);
            var pTran = ItemMappingBll.GetTran();
            string m_EventItemMappingID = string.Empty;
            int SumQty = 0;
            using (pTran.Connection)
            {
                try
                {
                    #region 商品
                    if (!string.IsNullOrWhiteSpace(para.EventItemMappingID))
                    {
                        //编辑
                        var UpdateItemData = ItemMappingBll.GetByID(para.EventItemMappingID);
                        if (UpdateItemData == null)
                            throw new APIException("未找到相关砍价活动商品，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                        UpdateItemData.SinglePurchaseQty = para.SinglePurchaseQty;
                        UpdateItemData.BargaingingInterval = para.BargaingingInterval;
                        //
                        ItemMappingBll.Update(UpdateItemData, pTran);
                        //
                        m_EventItemMappingID = para.EventItemMappingID;
                    }
                    else
                    {
                        #region 创建砍价商品

                        var ItemResult = ItemMappingBll.QueryByEntity(new PanicbuyingKJEventItemMappingEntity() { EventId = new Guid(para.EventId),ItemID=para.ItemId }, null).ToList();
                        if(ItemResult.Count()>0)
                            throw new APIException("当前砍价活动已添加过相同的商品，请换一个商品添加！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            

                        var AddItemData = new PanicbuyingKJEventItemMappingEntity();
                        AddItemData.EventItemMappingID = System.Guid.NewGuid();
                        AddItemData.EventId = new Guid(para.EventId);
                        AddItemData.ItemID = para.ItemId;
                        AddItemData.MinPrice = 0;
                        AddItemData.MinBasePrice = 0;
                        AddItemData.SoldQty = 0;
                        AddItemData.Qty = 0;
                        AddItemData.KeepQty = 0;
                        AddItemData.SinglePurchaseQty = para.SinglePurchaseQty;
                        AddItemData.DiscountRate = 0;
                        AddItemData.PromotePersonCount = 0;
                        AddItemData.BargainPersonCount = 0;
                        AddItemData.PurchasePersonCount = 0;
                        AddItemData.Status = 1;
                        AddItemData.StatusReason = "";
                        AddItemData.DisplayIndex = 0;
                        AddItemData.BargaingingInterval = para.BargaingingInterval;
                        AddItemData.customerId = loggingSessionInfo.ClientID;
                        //
                        ItemMappingBll.Create(AddItemData, pTran);
                        //更新活动商品数量
                        var UpdateEventData = PanicbuyingEventBLL.GetByID(para.EventId);
                        if (UpdateEventData == null)
                            throw new APIException("未找到相关砍价活动，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        UpdateEventData.ItemQty += 1;
                        PanicbuyingEventBLL.Update(UpdateEventData, pTran);
                        //
                        m_EventItemMappingID = AddItemData.EventItemMappingID.ToString();
                        #endregion
                    }
                    #endregion
                    #region sku
                    if (para.EventSkuInfoList.Count > 0)
                    {
                        foreach (var item in para.EventSkuInfoList)
                        {
                            if (!string.IsNullOrWhiteSpace(item.EventSKUMappingId))
                            {
                                var UpdateSkuData = SkuMappingBll.GetByID(item.EventSKUMappingId);
                                if (UpdateSkuData == null)
                                    throw new APIException("未找到相关砍价活动商品规格信息，请确认参数") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                if (item.IsDelete == 1)
                                {
                                    //删除
                                    SkuMappingBll.Delete(UpdateSkuData, pTran);
                                    //
                                    //
                                    SumQty += -UpdateSkuData.Qty.Value;
                                }
                                else
                                {
                                    //
                                    SumQty += item.Qty - UpdateSkuData.Qty.Value;
                                    #region 编辑
                                    UpdateSkuData.BasePrice = item.Price;
                                    UpdateSkuData.Qty = item.Qty;
                                    UpdateSkuData.BargainStartPrice = item.BargainStartPrice;
                                    UpdateSkuData.BargainEndPrice = item.BargainEndPrice;
                                    //
                                    SkuMappingBll.Update(UpdateSkuData, pTran);
                                    #endregion
                                }
                            }
                            else
                            {
                                #region 创建

                                var SkuResult = SkuMappingBll.QueryByEntity(new PanicbuyingKJEventSkuMappingEntity() { EventItemMappingID = m_EventItemMappingID, SkuID = item.SkuID }, null).ToList();
                                if (SkuResult.Count() > 0)
                                    throw new APIException("当前砍价活动商品已添加过相同的Sku，请换一个！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                var AddSkuData = new PanicbuyingKJEventSkuMappingEntity();
                                AddSkuData.EventSKUMappingId = System.Guid.NewGuid();
                                AddSkuData.EventItemMappingID = m_EventItemMappingID;
                                AddSkuData.SkuID = item.SkuID;
                                AddSkuData.AddedTime = DateTime.Now;
                                AddSkuData.Qty = item.Qty;
                                AddSkuData.KeepQty = 0;
                                AddSkuData.SoldQty = 0;
                                AddSkuData.SinglePurchaseQty = 0;
                                AddSkuData.Price = item.Price;
                                AddSkuData.BasePrice = item.BasePrice;
                                AddSkuData.BargainStartPrice = item.BargainStartPrice;
                                AddSkuData.BargainEndPrice = item.BargainEndPrice;
                                AddSkuData.DisplayIndex = 0;
                                AddSkuData.IsFirst = 1;
                                AddSkuData.Status = 1;
                                AddSkuData.CustomerId = loggingSessionInfo.ClientID;
                                //
                                SkuMappingBll.Create(AddSkuData, pTran);
                                //
                                SumQty += AddSkuData.Qty.Value;
                                #endregion
                            }
                        }
                    }
                    #endregion
                    //提交
                    pTran.Commit();
                }
                catch (APIException ex)
                {
                    pTran.Rollback();
                    throw ex;
                }

            }
            //更新砍价商品总库存、最小底价、原价
            if (SumQty > 0)
            {
                var UpdateData = ItemMappingBll.GetByID(m_EventItemMappingID);
                UpdateData.Qty += SumQty;
                UpdateData.MinPrice = SkuMappingBll.GetConfigPrice(UpdateData.EventItemMappingID.ToString(), "MinPrice");
                UpdateData.MinBasePrice = SkuMappingBll.GetConfigPrice(UpdateData.EventItemMappingID.ToString(), "MinBasePrice");
                ItemMappingBll.Update(UpdateData);
            }

            return rd;
        }
    }
}
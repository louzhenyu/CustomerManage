using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Request;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    public class GetSalesReturnDetailAH : BaseActionHandler<GetSalesReturnDetailRP, GetSalesReturnDetailRD>
    {
        protected override GetSalesReturnDetailRD ProcessRequest(DTO.Base.APIRequest<GetSalesReturnDetailRP> pRequest)
        {
            var rd = new GetSalesReturnDetailRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var salesReturnBLL = new T_SalesReturnBLL(loggingSessionInfo);
            var historyBLL = new T_SalesReturnHistoryBLL(loggingSessionInfo);

            var inoutService = new InoutService(loggingSessionInfo);
            var tInoutDetailBll = new TInoutDetailBLL(loggingSessionInfo);
            var inoutBLL = new T_InoutBLL(CurrentUserInfo);
            var salesReturnEntity = salesReturnBLL.GetByID(para.SalesReturnID);


            if (salesReturnEntity != null)
            {
                rd.SalesReturnID = salesReturnEntity.SalesReturnID.ToString();
                rd.SalesReturnNo = salesReturnEntity.SalesReturnNo;
                rd.Status = salesReturnEntity.Status;
                rd.DeliveryType = salesReturnEntity.DeliveryType;
                rd.Reason = salesReturnEntity.Reason;
                rd.Qty = salesReturnEntity.Qty;
                rd.ActualQty = salesReturnEntity.ActualQty;
                rd.Contacts = salesReturnEntity.Contacts;
                rd.Phone = salesReturnEntity.Phone;
                rd.Address = string.Empty;
                var orderInfo = inoutBLL.GetByID(salesReturnEntity.OrderID);
                if (orderInfo != null)
                    rd.Address = orderInfo.Field4;
                rd.ServicesType = salesReturnEntity.ServicesType;

                //根据订单ID获取订单明细[复用]
                DataRow drItem = inoutService.GetOrderDetailByOrderId(salesReturnEntity.OrderID).Tables[0].Select(" item_id= '" + salesReturnEntity.ItemID + "'").FirstOrDefault();
                //获取商品的图片[复用]
                string itemImage = tInoutDetailBll.GetOrderDetailImageList("'" + salesReturnEntity.ItemID + "'").Tables[0].Rows[0]["imageUrl"].ToString();
                //获取订单详细列表中的商品规格[复用]
                DataRow[] drSku = inoutService.GetInoutDetailGgByOrderId(salesReturnEntity.OrderID).Tables[0].Select(" sku_id='" + salesReturnEntity.SkuID + "'");

                //订单的商品信息
                var orderDetail = new OrderInfoDetail();

                orderDetail.ItemName = drItem["item_name"].ToString();
                orderDetail.SalesPrice = Convert.ToDecimal(drItem["enter_price"]);
                orderDetail.Qty = Convert.ToInt32(drItem["enter_qty"]);
                orderDetail.ImageUrl = ImagePathUtil.GetImagePathStr(itemImage, "240");
                orderDetail.PayTypeName = salesReturnEntity.PayTypeName;
                orderDetail.RefundAmount = salesReturnEntity.RefundAmount == null ? 0 : salesReturnEntity.RefundAmount.Value;
                orderDetail.ConfirmAmount = salesReturnEntity.ConfirmAmount == null ? 0 : salesReturnEntity.ConfirmAmount.Value;
                rd.OrderDetail = orderDetail;
                //订单的商品规格
                if (drSku.Count() > 0)
                {
                    SkuDetailInfo skuDetail = new SkuDetailInfo();
                    skuDetail.PropName1 = drSku[0]["prop_1_name"].ToString();
                    skuDetail.PropDetailName1 = drSku[0]["prop_1_detail_name"].ToString();
                    skuDetail.PropName2 = drSku[0]["prop_2_name"].ToString();
                    skuDetail.PropDetailName2 = drSku[0]["prop_2_detail_name"].ToString();
                    skuDetail.PropName3 = drSku[0]["prop_3_name"].ToString();
                    skuDetail.PropDetailName3 = drSku[0]["prop_3_detail_name"].ToString();
                    rd.OrderDetail.SkuDetail = skuDetail;
                }
                var history = historyBLL.QueryByEntity(new T_SalesReturnHistoryEntity() { SalesReturnID = salesReturnEntity.SalesReturnID }, new[] { new OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc } });
                rd.HistoryList = history.Select(t => new HistoryInfo() { HistoryID = t.HistoryID.ToString(), OperationDesc = t.OperationDesc, HisRemark = t.HisRemark, OperatorName = t.OperatorName, CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd HH:mm") }).ToArray();
            }
            return rd;
        }
    }
}
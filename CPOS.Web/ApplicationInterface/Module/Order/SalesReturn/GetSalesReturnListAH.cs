using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Request;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    public class GetSalesReturnListAH : BaseActionHandler<GetSalesReturnListRP, GetSalesReturnListRD>
    {

        protected override GetSalesReturnListRD ProcessRequest(DTO.Base.APIRequest<GetSalesReturnListRP> pRequest)
        {
            var rd = new GetSalesReturnListRD();
            var para = pRequest.Parameters;
            var salesReturnBLL = new T_SalesReturnBLL(CurrentUserInfo);

            var inoutService = new InoutService(CurrentUserInfo);
            var tInoutDetailBll = new TInoutDetailBLL(CurrentUserInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "r.VipID", Value = CurrentUserInfo.UserID });

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "r.CreateTime", Direction = OrderByDirections.Desc });

            var tempList = salesReturnBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex+1);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;

            List<SalesReturnInfo> salesReturnList = new List<SalesReturnInfo>{ };
            SalesReturnInfo salesReturnInfo = null;
            SkuDetailInfo skuDetail=null;

            foreach (var item in tempList.Entities)
            {
                salesReturnInfo = new SalesReturnInfo();

                //根据订单ID获取订单明细[复用]
                DataRow drItem = inoutService.GetOrderDetailByOrderId(item.OrderID).Tables[0].Select(" item_id= '" + item.ItemID+"'").FirstOrDefault();
                //获取商品的图片[复用]
                string itemImage = tInoutDetailBll.GetOrderDetailImageList("'"+item.ItemID+"'").Tables[0].Rows[0]["imageUrl"].ToString();
                //获取订单详细列表中的商品规格[复用]
                DataRow[] drSku = inoutService.GetInoutDetailGgByOrderId(item.OrderID).Tables[0].Select(" sku_id='" + item.SkuID + "'");

                salesReturnInfo.SalesReturnID = item.SalesReturnID.ToString();
                salesReturnInfo.SalesReturnNo = item.SalesReturnNo; 
                salesReturnInfo.ItemName = drItem["item_name"].ToString();
                salesReturnInfo.SalesPrice = Convert.ToDecimal(drItem["enter_price"]);
                salesReturnInfo.Qty = item.Qty;
                salesReturnInfo.Status = item.Status;
                salesReturnInfo.ImageUrl = ImagePathUtil.GetImagePathStr(itemImage, "240");
                salesReturnInfo.ServicesType = item.ServicesType;

                if (drSku.Count() > 0)
                {
                    skuDetail = new SkuDetailInfo();
                    skuDetail.PropName1 = drSku[0]["prop_1_name"].ToString();
                    skuDetail.PropDetailName1 = drSku[0]["prop_1_detail_name"].ToString();
                    skuDetail.PropName2 = drSku[0]["prop_2_name"].ToString();
                    skuDetail.PropDetailName2 = drSku[0]["prop_2_detail_name"].ToString();
                    skuDetail.PropName3 = drSku[0]["prop_3_name"].ToString();
                    skuDetail.PropDetailName3 = drSku[0]["prop_3_detail_name"].ToString();
                    salesReturnInfo.SkuDetail = skuDetail;
                }
                salesReturnList.Add(salesReturnInfo);
            }

            rd.SalesReturnList = salesReturnList.ToArray();
            rd.TotalCount = tempList.RowCount;
            rd.TotalPageCount = tempList.PageCount;
            return rd;
        }
    }
}
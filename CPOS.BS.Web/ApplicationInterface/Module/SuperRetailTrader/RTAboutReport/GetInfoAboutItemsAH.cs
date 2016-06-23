using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.RTAboutReport
{
    /// <summary>
    /// 商品数据分析接口
    /// </summary>
    public class GetInfoAboutItemsAH : BaseActionHandler<EmptyRequestParameter, GetInfoAboutItemsRD>
    {
        protected override GetInfoAboutItemsRD ProcessRequest(DTO.Base.APIRequest<EmptyRequestParameter> pRequest)
        {
            var rd = new GetInfoAboutItemsRD();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var R_SRT_RTProductHomebll=new R_SRT_RTProductHomeBLL(loggingSessionInfo);
            var R_SRT_RTProductTopbll = new R_SRT_RTProductTopBLL(loggingSessionInfo);
            //超级分销 分销商品首页
            var R_SRT_RTProductHomeInfo = R_SRT_RTProductHomebll.QueryByEntity(new R_SRT_RTProductHomeEntity() {CustomerId=loggingSessionInfo.CurrentUser.customer_id },new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();
            if (R_SRT_RTProductHomeInfo != null)
            {
                //近30天商品分享信息
                var sharedRTProductInfo = new SharedRTProductInfo();
                sharedRTProductInfo.Day30SharedRTProductCount = R_SRT_RTProductHomeInfo.Day30SharedRTProductCount;
                sharedRTProductInfo.Day30NoSharedRTProductCount = R_SRT_RTProductHomeInfo.Day30NoSharedRTProductCount;
                rd.SharedRTProduct = sharedRTProductInfo;
                rd.DateCode = R_SRT_RTProductHomeInfo.DateCode.ToString();
                //近30天商品销售信息
                var salesRTProductInfo = new SalesRTProductInfo();
                salesRTProductInfo.Day30F2FSalesRTProductCount = R_SRT_RTProductHomeInfo.Day30F2FSalesRTProductCount;
                salesRTProductInfo.Day30ShareSalesRTProductCount = R_SRT_RTProductHomeInfo.Day30ShareSalesRTProductCount;
                rd.SalesRTProduct = salesRTProductInfo;
                //近28天商品转化率
                var rTProductCRateInfo = new RTProductCRateInfo();
                rTProductCRateInfo.Day7RTProductCRate = R_SRT_RTProductHomeInfo.Day7RTProductCRate;
                rTProductCRateInfo.LastDay7RTProductCRate = R_SRT_RTProductHomeInfo.LastDay7RTProductCRate;
                rTProductCRateInfo.Last2Day7RTProductCRate = R_SRT_RTProductHomeInfo.Last2Day7RTProductCRate;
                rTProductCRateInfo.Last3Day7RTProductCRate = R_SRT_RTProductHomeInfo.Last3Day7RTProductCRate;
                rd.RTProductCRate = rTProductCRateInfo;
            }
            var AboutItemsInfoList = R_SRT_RTProductTopbll.GetSRT_RTProductTopList(loggingSessionInfo.ClientID, "Idx","ASC");
            
            var SalesMoreItemesList = new List<R_SRT_RTProductTopEntity>();
            var ShareLessItemsList = new List<R_SRT_RTProductTopEntity>();
            var SalesLessItemesList = new List<R_SRT_RTProductTopEntity>();
            if (AboutItemsInfoList.Count>0 )
            {
                //分享商品次数最多
                var shareMoreItemsList  = AboutItemsInfoList.Where(b => b.TopType == 1&&b.BusiType==1).Select(m=>new RTItemInfo(){
                    ItemImgUrl=m.ItemImageUrl,
                    ItemId=m.ItemId,
                    ItemName=m.ItemName,
                    ShareCount=m.ShareCount,
                    OrderCount=m.OrderCount,
                    CRate=m.CRate
                }).ToList<RTItemInfo>();
                rd.ShareMoreItemsList = shareMoreItemsList;
                //销售商品次数最多
                var slesMoreItemesList = AboutItemsInfoList.Where(b => b.TopType == 1 && b.BusiType == 2).Select(m => new RTItemInfo()
                {
                    ItemImgUrl = m.ItemImageUrl,
                    ItemId = m.ItemId,
                    ItemName = m.ItemName,
                    ShareCount = m.ShareCount,
                    OrderCount = m.OrderCount,
                    CRate = m.CRate
                }).ToList<RTItemInfo>();
                rd.SalesMoreItemesList = slesMoreItemesList;
                //分享商品次数最少
                var shareLessItemsList = AboutItemsInfoList.Where(b => b.TopType == 2 && b.BusiType == 1).Select(m => new RTItemInfo()
                {
                    ItemImgUrl = m.ItemImageUrl,
                    ItemId = m.ItemId,
                    ItemName = m.ItemName,
                    ShareCount = m.ShareCount,
                    OrderCount = m.OrderCount,
                    CRate = m.CRate
                }).ToList<RTItemInfo>();
                rd.ShareLessItemsList = shareLessItemsList;
                //销量最少的商品排行信息
                var salesLessItemesList = AboutItemsInfoList.Where(b => b.TopType == 2 && b.BusiType == 2).Select(m => new RTItemInfo()
                {
                    ItemImgUrl = m.ItemImageUrl,
                    ItemId = m.ItemId,
                    ItemName = m.ItemName,
                    ShareCount = m.ShareCount,
                    OrderCount = m.OrderCount,
                    CRate = m.CRate
                }).ToList<RTItemInfo>();
                rd.SalesLessItemesList = salesLessItemesList;
            }
            
            return rd;
        }
    }
}
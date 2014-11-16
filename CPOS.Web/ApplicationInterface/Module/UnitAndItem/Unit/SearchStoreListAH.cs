/*
 * Author		:Alex.tian
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/14 17:30:00
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Request;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit.Response;
using JIT.CPOS.DTO.Module.UnitAndItem.Unit;
using JIT.CPOS.BS.BLL;
namespace JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Unit
{
    public class SearchStoreListAH : BaseActionHandler<SearchStoreListRP, SearchStoreListRD>
    {
        protected override SearchStoreListRD ProcessRequest(APIRequest<SearchStoreListRP> pRequest)
        {
            SearchStoreListRD rd = new SearchStoreListRD();
            var UnitServiceBll = new UnitService(base.CurrentUserInfo);
            //查询全部或查询附近 根据CityCode判断
            string customerID = pRequest.CustomerID;
             CustomerBasicSettingBLL customerBasic = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            //获取配置表中指定的附近范围值。
            double RangeAccessoriesStores = customerBasic.SearchRangeAccessoriesStores();
            //执行查询
            var list = UnitServiceBll.FuzzyQueryStores(pRequest.Parameters.NameLike, pRequest.Parameters.CityCode, pRequest.Parameters.Position, pRequest.Parameters.PageIndex, pRequest.Parameters.PageSize, pRequest.Parameters.StoreID, pRequest.Parameters.IncludeHQ, customerID, RangeAccessoriesStores);
            foreach (var item in list)
            {
                if (item.Distance.HasValue)
                {
                    if (item.Distance.Value <= 500)
                    {
                        item.DistanceDesc = string.Format("{0}米", item.Distance.Value.ToString("f0"));
                    }
                    else
                    {
                        var km = item.Distance.Value / 1000;
                        item.DistanceDesc = string.Format("{0}公里", km.ToString("f1"));
                    }
                }
            }
            rd.StoreListInfo = list;
            return rd;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Request;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingActivity.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Notification;
using JIT.CPOS.BS.Web.Session;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingActivity
{
    public class GetSkuInfoByCategoryOrNameAH : BaseActionHandler<SkuInfoRP, SkuInfoRD>
    {

        protected override SkuInfoRD ProcessRequest(APIRequest<SkuInfoRP> pRequest)
        {
            var rd = new SkuInfoRD();

            var para = pRequest.Parameters;
            var bllItem = new ItemService(this.CurrentUserInfo);
            DataSet ds = bllItem.GetItemSkuInfoByCategory(para.CategoryId, para.ItemName, para.BatId);
            if (ds != null && ds.Tables.Count > 0)
            {
                rd.ItemSkuInfoList = DataTableToObject.ConvertToList<ItemSkuInfo>(ds.Tables[0]);
            }

            return rd;
        }
    }
}
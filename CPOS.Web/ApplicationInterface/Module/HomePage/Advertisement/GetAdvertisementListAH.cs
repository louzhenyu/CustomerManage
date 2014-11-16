using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.HomePage.Advertisement.Request;
using JIT.CPOS.DTO.Module.HomePage.Advertisement.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Reflection;

namespace JIT.CPOS.Web.ApplicationInterface.Module.HomePage.Advertisement
{
    /// <summary>
    /// 查询广告列表
    /// </summary>
    public class GetAdvertisementListAH : BaseActionHandler<GetAdvertisementListRP, GetAdvertisementListRD>
    {

        #region 错误码
        const int ERROR_ADVERTISEMENT_FAILURE = 330;
        const int ERROR_ADVERTISEMENT_NOCUSTOMERID = 110;
        #endregion

        protected override GetAdvertisementListRD ProcessRequest(DTO.Base.APIRequest<GetAdvertisementListRP> pRequest)
        {
            GetAdvertisementListRD rd = new GetAdvertisementListRD();

            if (string.IsNullOrEmpty(pRequest.CustomerID))
                throw new APIException("客户ID为空") { ErrorCode = ERROR_ADVERTISEMENT_NOCUSTOMERID };

            #region 查询广告列表
            try
            {
                var ds = new AdvertisementBLL(base.CurrentUserInfo).GetAdvertisementList(pRequest.CustomerID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.AdvertisementList = DataLoader.LoadFrom<AdvertisementEntity>(ds.Tables[0]);
                }
            }
            catch (Exception)
            {
                throw new APIException("查询数据错误") { ErrorCode = ERROR_ADVERTISEMENT_FAILURE };
            }
            #endregion

            return rd;
        }

    }
}
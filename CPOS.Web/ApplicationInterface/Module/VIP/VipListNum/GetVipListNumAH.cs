using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.VIP.VipListNum.Request;
using JIT.CPOS.DTO.Module.VIP.VipListNum.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using System.Configuration;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipListNum
{
    /// <summary>
    /// 获取表单列表
    /// </summary>
    public class GetVipListNumAH : BaseActionHandler<GetVipListNumRP, GetVipListNumRD>
    {

        #region 错误码
        const int ERROR_LNEWS_FAILURE = 330;
        #endregion

        protected override GetVipListNumRD ProcessRequest(DTO.Base.APIRequest<GetVipListNumRP> pRequest)
        {
            GetVipListNumRD rd = new GetVipListNumRD();
            var items = new List<VipNumInfo>();

            pRequest.CustomerID = ConfigurationManager.AppSettings["WiFiCustomerID"].Trim();

            if (string.IsNullOrEmpty(pRequest.Parameters.UnitID))
            {
                //门店ID，前期默认，可以不传(上海越洋广场店)
                pRequest.Parameters.UnitID = "9678d0e66d8d411baf8a6027c3e623f9";
            }

            #region 获取表单列表
            try
            {
                var bll = new WiFiUserVisitBLL(base.CurrentUserInfo);

                rd.VipNumNow = bll.GetVipNumAllOrNow(pRequest.Parameters.UnitID, true);
                rd.VipNumAll = bll.GetVipNumAllOrNow(pRequest.Parameters.UnitID, false);


                VipNumInfo info1 = new VipNumInfo();
                info1.TypeName = "5分钟内";
                info1.VipNum = bll.GetVipNum(pRequest.Parameters.UnitID, 0, 5);
                info1.Proportion = rd.VipNumAll > 0 ? info1.VipNum * 100 / rd.VipNumAll : 0;
                items.Add(info1);

                VipNumInfo info2 = new VipNumInfo();
                info2.TypeName = "5-10分钟内";
                info2.VipNum = bll.GetVipNum(pRequest.Parameters.UnitID, 5, 10);
                info2.Proportion = rd.VipNumAll > 0 ? info2.VipNum * 100 / rd.VipNumAll : 0;
                items.Add(info2);

                VipNumInfo info3 = new VipNumInfo();
                info3.TypeName = "10分钟到1小时内";
                info3.VipNum = bll.GetVipNum(pRequest.Parameters.UnitID, 10, 60);
                info3.Proportion = rd.VipNumAll > 0 ? info3.VipNum * 100 / rd.VipNumAll : 0;
                items.Add(info3);

                VipNumInfo info4 = new VipNumInfo();
                info4.TypeName = "1小时以上";
                info4.VipNum = bll.GetVipNum(pRequest.Parameters.UnitID, 60, -1);
                info4.Proportion = rd.VipNumAll > 0 ? info4.VipNum * 100 / rd.VipNumAll : 0;
                items.Add(info4);


                rd.Items = items.ToArray();
            }
            catch (Exception)
            {
                throw new APIException("查询数据错误") { ErrorCode = ERROR_LNEWS_FAILURE };
            }
            #endregion

            return rd;
        }

    }
}
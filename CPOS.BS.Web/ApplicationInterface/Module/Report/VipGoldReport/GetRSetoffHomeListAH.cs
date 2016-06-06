using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Report.VipGoldReport
{
    /// <summary>
    /// 会员金矿{集客行动 首页 饼状图}
    /// </summary>
    public class GetRSetoffHomeListAH : BaseActionHandler<GetRSetoffHomeListRP, GetRSetoffHomeListRD>
    {
        protected override GetRSetoffHomeListRD ProcessRequest(APIRequest<GetRSetoffHomeListRP> pRequest)
        {

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var parameter = pRequest.Parameters;
            var rd = new GetRSetoffHomeListRD();

            R_SetoffHome2BLL bll = new R_SetoffHome2BLL(loggingSessionInfo);

            List<string> lst = new List<string>() { "1", "2" };
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { new OrderBy() { FieldName = "DateCode", Direction = OrderByDirections.Desc } };
            foreach (var item in lst)
            {

                R_SetoffHome2Entity entity = new R_SetoffHome2Entity() { CustomerId = loggingSessionInfo.ClientID, SetoffType =Convert.ToInt32(item) };
                var _model = bll.PagedQueryByEntity(entity,lstOrder.ToArray(), 1, 1);
             
                string itemName = "会员集客";
                if (item == "2")
                    itemName = "员工集客";

                if (_model.Entities != null && _model.Entities.FirstOrDefault() != null)
                {
                    rd.result.Add(new result(_model.Entities.FirstOrDefault().OnlyFansCount, _model.Entities.FirstOrDefault().VipCount, _model.Entities.FirstOrDefault().VipPer, itemName));
                }
                else
                {
                    rd.result.Add(new result(0, 0, 0, itemName));
                }
            }
            return rd;
        }
    }
}
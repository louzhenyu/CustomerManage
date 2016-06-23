using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.SuperRetailTrader.RTExtend
{
    /// <summary>
    /// 得到 扩展工具集合
    /// </summary>
    public class GetSetoffToolListAH : BaseActionHandler<EmptyRequestParameter, GetExtendListRD>
    {
        protected override GetExtendListRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            //throw new NotImplementedException();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
            SetoffEventBLL bll = new SetoffEventBLL(loggingSessionInfo);
            //return bll.GetSetoffToolList();

            //SetoffEventBLL bll = new SetoffEventBLL(loggingSessionInfo);
            var result = new GetExtendListRD();
            //超级分销主数据
            var dbResult1 = bll.QueryByEntity(new SetoffEventEntity() { Status = "10", SetoffType = 3, CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
            if (dbResult1 == null)
                return result;
            result.SetoffEventID = dbResult1.SetoffEventID;
            result.List = new List<GetExtendInfo>();
            var dbResult2 = bll.GetSetoffToolList(dbResult1.SetoffEventID);
            if (dbResult2 != null)
                result.List = DataTableToObject.ConvertToList<GetExtendInfo>(dbResult2.Tables[0]);
            foreach (var m in result.List)
            {
                var tmpD1 = new DateTime();
                if (DateTime.TryParse(m.BeginData, out tmpD1))
                {
                    m.BeginData = Convert.ToDateTime(m.BeginData).ToString("yyyy-MM-dd");
                }
                if (DateTime.TryParse(m.EndData, out tmpD1))
                {
                    m.EndData = Convert.ToDateTime(m.EndData).ToString("yyyy-MM-dd");
                }
            }
            return result;

        }
    }
}
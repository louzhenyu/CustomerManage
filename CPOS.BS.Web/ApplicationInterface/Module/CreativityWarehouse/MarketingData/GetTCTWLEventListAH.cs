﻿using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.CreativityWarehouse.MarketingData.Request;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.CreativityWarehouse.MarketingData
{
    public class GetTCTWLEventListAH : BaseActionHandler<GetTCTWLEventListRP, GetTCTWLEventListRD>
    {

        protected override GetTCTWLEventListRD ProcessRequest(APIRequest<GetTCTWLEventListRP> pRequest)
        {
            var rd = new GetTCTWLEventListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;//后台管理系统，从session里取数据

            T_CTW_LEventBLL _T_CTW_LEventBLL = new JIT.CPOS.BS.BLL.T_CTW_LEventBLL(loggingSessionInfo);

            DataSet ds = _T_CTW_LEventBLL.GetT_CTW_LEventList(
       pRequest.Parameters.EventName,
          pRequest.Parameters.BeginTime,
                   pRequest.Parameters.EndTime,
              pRequest.Parameters.EventStatus,
                   pRequest.Parameters.ActivityGroupId,              
                    pRequest.Parameters.PageSize,
                 pRequest.Parameters.PageIndex,
          CurrentUserInfo.ClientID);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Columns.Count > 0)
                {
                    rd.TotalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
                    int mod=  rd.TotalCount % pRequest.Parameters.PageSize;
                    rd.TotalPage = rd.TotalCount / pRequest.Parameters.PageSize +( mod>0?1:0);
                }
                if (ds.Tables[1] != null && ds.Tables[1].Columns.Count > 0)
                {
                    rd.T_CTW_LEventList = DataTableToObject.ConvertToList<T_CTW_LEventInfo>(ds.Tables[1]);
                }

            }
            return rd;

        }
    }
}
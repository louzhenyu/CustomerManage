using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.UnitAndItem.Area.Response;
using JIT.CPOS.DTO.Module.UnitAndItem.Area;
using JIT.CPOS.BS.BLL;
using System.Data.SqlClient;
using System.Data;


namespace JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Area
{
    public class GetCityListAH : BaseActionHandler<GetCityListRP, GetCityListRD>
    {
        protected override GetCityListRD ProcessRequest(DTO.Base.APIRequest<GetCityListRP> pRequest)
        {
            GetCityListRD rd = new GetCityListRD();
            string customerId = this.CurrentUserInfo.ClientID;
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
             CityService service = new CityService(loggingSessionInfo);
            var dr = service.GetCityNameList(customerId);
       
            List<GetCityListInfo> list = new List<GetCityListInfo>{};
            using(SqlDataReader rdr = dr)
            {
                while(rdr.Read())
                {
                    GetCityListInfo m = null;
                    this.Load(rdr,out m);
                    list.Add(m);
                }
            }
            rd.CityList =list.ToArray();
            return rd;
        }

        protected void Load(IDataReader pReader, out GetCityListInfo pInstance)
        {
            pInstance = new GetCityListInfo();
            if(pReader["CityName"] != DBNull.Value)
            {
                pInstance.CityName=Convert.ToString(pReader["CityName"]);
            }
            if (pReader["Code"] != DBNull.Value)
            {
                pInstance.Code = Convert.ToString(pReader["Code"]);
            }
        }
    }
}
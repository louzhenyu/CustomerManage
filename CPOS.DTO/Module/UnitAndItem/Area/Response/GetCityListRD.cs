using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.UnitAndItem.Area.Response
{
    public class GetCityListRD : IAPIResponseData
    {
        public GetCityListInfo[] CityList { get; set; }
    }
    public class GetCityListInfo
    {
        public string CityName { get; set; }
        public string Code { get; set; }
    }
}

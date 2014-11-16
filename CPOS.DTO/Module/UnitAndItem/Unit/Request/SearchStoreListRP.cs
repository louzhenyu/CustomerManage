using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.UnitAndItem.Unit.Request
{
    public class SearchStoreListRP : IAPIRequestParameter
    {
        public string NameLike { get; set; }			//模糊查询用参数
        public string CityCode { get; set; }			//城市Code
        public string Position { get; set; }			//坐标
        public int? PageIndex { get; set; }		//页码
        public int? PageSize { get; set; }		//页大小 
        public string StoreID { get; set; }			//门店ID,为空或者NULL时为所有门店
        public bool? IncludeHQ { get; set; }			//0不包括,1包括(默认为1)

        /// <summary>
        /// 查询方式为附近时，未传位置
        /// </summary>
        const int ERROR_CODE_NO_POSITION = 301;

        public void Validate()
        {
            if (this.CityCode == "-00")
            {
                if (string.IsNullOrWhiteSpace(this.Position))
                {
                    throw new APIException("查询方式为附近时，未传位置") { ErrorCode = ERROR_CODE_NO_POSITION };
                }
            }
        }
    }
}
